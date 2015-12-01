using System;
using System.Windows.Forms;
using Nintenlord.Feditor.Core.GameData;
using Nintenlord.Feditor.Core.MemoryManagement;
using Nintenlord.Feditor.Core.Public_API;

namespace NLightmare
{
    public class Program : IROMEditor
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(new Program()));
        }
        IMemoryManager memman;
        IROM rom;

        public Program()
        {

        }

        #region IROMEditor Members

        public Form EditorForm
        {
            get { return new Form1(this); }
        }

        public string Name
        {
            get { return "NLightmare"; }
        }

        public string[] CreatorNames
        {
            get { return new string[] { "Nintenlord" }; }
        }

        public IMemoryManager MemoryManager
        {
            get
            {
                return memman;
            }
            set
            {
                memman = value;
            }
        }

        public IROM ROM
        {
            get
            {
                return rom;
            }
            set
            {
                rom = value;
            }
        }
        
        public void ChangeROM(IMemoryManager memoryManager, IROM rom)
        {
            throw new NotImplementedException();
        }

        public bool SupportGame(GameEnum game)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
