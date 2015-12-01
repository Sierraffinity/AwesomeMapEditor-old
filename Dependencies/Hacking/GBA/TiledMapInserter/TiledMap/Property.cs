using System;
using System.Collections.Generic;
using System.Xml;

namespace TiledPipelineExtensions
{
    public class Property
    {
        public string Name;
        public string Value;

        public Property(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public static List<Property> ReadProperties(XmlNode node)
        {
            List<Property> properties = new List<Property>();

            foreach (XmlNode property in node.ChildNodes)
            {
                string name = property.Attributes["name"].Value;
                string value = property.Attributes["value"].Value;
                bool foundCopy = false;

                /* 
                 * A bug in Tiled will sometimes cause the file to contain identical copies of properties.
                 * I would fix it, but I'd have to dig into the Tiled code. instead, we'll detect exact
                 * duplicates here and log some warnings, failing only if the value is actually different.
                 * 
                 * To repro the bug, create two maps that use the same tileset. Open the first file in Tiled
                 * and set a property on a tile. Then open the second map and open the first back up. Look
                 * at the propertes on the tile. It will have two or three copies of the same property.
                 * 
                 * If you encounter the bug, you can remedy it in Tiled by closing the current file (Ctrl-F4
                 * or use Close from the File menu) and then reopen it. The tile will no longer have the
                 * copies of the property.
                 */
                foreach (var p in properties)
                {
                    if (p.Name == name)
                    {
                        if (p.Value == value)
                        {
                            foundCopy = true;
                        }
                        else
                        {
                            throw new Exception(string.Format("Multiple properties of name {0} exist with different values: {1} and {2}", name, value, p.Value));
                        }
                    }
                }

                // we only want to add one copy of any duplicate properties
                if (!foundCopy)
                    properties.Add(new Property(name, value));
            }

            return properties;
        }
    }
}
