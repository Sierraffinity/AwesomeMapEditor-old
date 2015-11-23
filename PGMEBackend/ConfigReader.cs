using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace PGMEBackend
{
    static class ConfigReader
    {
        public static void ReadConfigYAML()
        {
            var input = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"/AME.yaml");

            var deserializer = new Deserializer();

            var reader = new EventReader(new Parser(input));

            // Deserialize the document
            reader.Expect<StreamStart>();
            reader.Accept<DocumentStart>();
            var settings = deserializer.Deserialize<AMESettings>(reader);

            reader.Accept<DocumentStart>();
            var behaviorManifests = deserializer.Deserialize<BehaviorManifests>(reader);
            

            foreach (var behavior in behaviorManifests.FRLGBehaviors)
            {
                if(behavior.Value[0] == '&')
                    behaviorManifests.FRLGBehaviors[behavior.Key] = InternalStrings.ResourceManager.GetString(behavior.Value);
                Console.WriteLine("[{0}] {1}", behavior.Key.ToString("X"), behavior.Value);
            }
        }

        public class AMESettings
        {
            public bool ShowSprites { get; set; }
            public bool UsePlugins { get; set; }
            public string ScriptEditor { get; set; }
            public int PokemonCount { get; set; }
            public float PermissionTranslucency { get; set; }
            public string Language { get; set; }
        }

        public class BehaviorManifests
        {
            public Dictionary<int, string> RSEBehaviors { get; set; }
            public Dictionary<int, string> FRLGBehaviors { get; set; }
        }
    }
}
