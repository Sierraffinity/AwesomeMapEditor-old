using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;

namespace TiledPipelineExtensions
{
    // copied here to remove the dependency on the Windows DLL
    public enum Orientation : byte
    {
        Orthogonal,
        Isometric,
    }

    public class Map
    {
        public string Filename;
        public string Directory;

        public string Version = string.Empty;
        public Orientation Orientation;
        public int Width;
        public int Height;
        public int TileWidth;
        public int TileHeight;
        public List<Property> Properties = new List<Property>();
        public bool MakeTilesUnique = true;

        public List<TileSet> TileSets = new List<TileSet>();
        public List<Layer> Layers = new List<Layer>();

        public Map(XmlDocument document)
        {
            XmlNode mapNode = document["map"];

            Version = mapNode.Attributes["version"].Value;
            Orientation = (Orientation)Enum.Parse(typeof(Orientation), mapNode.Attributes["orientation"].Value, true);
            Width = int.Parse(mapNode.Attributes["width"].Value, CultureInfo.InvariantCulture);
            Height = int.Parse(mapNode.Attributes["height"].Value, CultureInfo.InvariantCulture);
            TileWidth = int.Parse(mapNode.Attributes["tilewidth"].Value, CultureInfo.InvariantCulture);
            TileHeight = int.Parse(mapNode.Attributes["tileheight"].Value, CultureInfo.InvariantCulture);

            XmlNode propertiesNode = document.SelectSingleNode("map/properties");
            if (propertiesNode != null)
            {
                Properties = Property.ReadProperties(propertiesNode);
            }

            foreach (XmlNode tileSet in document.SelectNodes("map/tileset"))
            {
                if (tileSet.Attributes["source"] != null)
                {
                    //TileSets.Add(new ExternalTileSetContent(tileSet));
                }
                else
                {
                    TileSets.Add(new TileSet(tileSet));
                }
            }

            foreach (XmlNode layerNode in document.SelectNodes("map/layer|map/objectgroup"))
            {
                Layer layerContent;

                if (layerNode.Name == "layer")
                {
                    layerContent = new TileLayer(layerNode);
                }
                else if (layerNode.Name == "objectgroup")
                {
                    //layerContent = new MapObjectLayerContent(layerNode);
                    throw new NotSupportedException("object layer not supported");
                }
                else
                {
                    throw new Exception("Unknown layer name: " + layerNode.Name);
                }

                // Layer names need to be unique for our lookup system, but Tiled
                // doesn't require unique names.
                string layerName = layerContent.Name;
                int duplicateCount = 2;

                // if a layer already has the same name...
                if (Layers.Find(l => l.Name == layerName) != null)
                {
                    // figure out a layer name that does work
                    do
                    {
                        layerName = string.Format("{0}{1}", layerContent.Name, duplicateCount);
                        duplicateCount++;
                    } while (Layers.Find(l => l.Name == layerName) != null);

                    // save that name
                    layerContent.Name = layerName;
                }

                Layers.Add(layerContent);
            }
        }
    }
}
