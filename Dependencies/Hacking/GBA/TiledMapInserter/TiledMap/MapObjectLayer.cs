using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Drawing;

namespace TiledPipelineExtensions
{
    public class MapObjectLayer : Layer
    {
        public List<MapObject> Objects = new List<MapObject>();
        public Color Color = Color.White;

        public MapObjectLayer(XmlNode node)
            : base(node)
        {
            if (node.Attributes["color"] != null)
            {
                // get the color string, removing the leading #
                string color = node.Attributes["color"].Value.Substring(1);

                // get the RGB individually
                string r = color.Substring(0, 2);
                string g = color.Substring(2, 2);
                string b = color.Substring(4, 2);

                // convert to the color
                Color = Color.FromArgb(
                    (byte)int.Parse(r, NumberStyles.AllowHexSpecifier),
                    (byte)int.Parse(g, NumberStyles.AllowHexSpecifier),
                    (byte)int.Parse(b, NumberStyles.AllowHexSpecifier));
            }

            foreach (XmlNode objectNode in node.SelectNodes("object"))
            {
                MapObject mapObjectContent = new MapObject(objectNode);

                // Object names need to be unique for our lookup system, but Tiled
                // doesn't require unique names.
                string objectName = mapObjectContent.Name;
                int duplicateCount = 2;

                // if a object already has the same name...
                if (Objects.Find(o => o.Name == objectName) != null)
                {
                    // figure out a object name that does work
                    do
                    {
                        objectName = string.Format("{0}{1}", mapObjectContent.Name, duplicateCount);
                        duplicateCount++;
                    } while (Objects.Find(o => o.Name == objectName) != null);
                    
                    // save that name
                    mapObjectContent.Name = objectName;
                }

                Objects.Add(mapObjectContent);
            }
        }
    }
}
