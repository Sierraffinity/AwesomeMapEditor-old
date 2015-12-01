using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Nintenlord.Feditor.Core.Public_API;
using Nintenlord.Utility;

namespace Nintenlord.Feditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static Dictionary<string, IROMEditor> LoadAssemblies()
        {
            var files = Directory.EnumerateFiles("Editors", "*.exe", SearchOption.TopDirectoryOnly);
            List<IROMEditor> editors = new List<IROMEditor>();
            
            foreach (var item in files)
            {
                var ass = Assembly.LoadFile(Path.GetFullPath(item));
                var types = ass.GetTypes();
                foreach (var type in types)
                {
                    if (type.GetInterfaces().Contains(typeof(IROMEditor)))
                    {
                        editors.Add(type.GetConstructor(Type.EmptyTypes).Invoke(null) as IROMEditor);
                    }
                }
            }
            var dic = editors.GetDictionary<string, IROMEditor>();

            //Add built-in editors
            var portraitEditor = new PortraitInserter.Program();
            dic.Add(portraitEditor.Name, portraitEditor);

            return dic;
        }
    }
}
