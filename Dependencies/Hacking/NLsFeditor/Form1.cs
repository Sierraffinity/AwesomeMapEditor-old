using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Nintenlord.Feditor.Core.GameData;
using Nintenlord.Feditor.Core.MemoryManagement;
using Nintenlord.Feditor.Core.Public_API;

namespace Nintenlord.Feditor
{
    public partial class Form1 : Form
    {
        Dictionary<string, IROMEditor> editors;
        IMemoryManager memoryManager;
        IROM romToEdit;


        public Form1()
        {
            InitializeComponent();
            editors = Program.LoadAssemblies();
            editorsToolStripMenuItem.Enabled = editors.Count > 0;
            foreach (var item in editors)
            {
                var tooStripItem = 
                    editorsToolStripMenuItem.DropDownItems.Add(item.Key);

                tooStripItem.Click += new EventHandler(tooStripItem_Click);
            }
        }

        void tooStripItem_Click(object sender, EventArgs e)
        {
            var realSender = sender as ToolStripItem;
            editors[realSender.Text].EditorForm.Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openROMToEdit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                OpenROM(openROMToEdit.FileName);

                foreach (var item in editors)
                {
                    item.Value.ChangeROM(memoryManager, romToEdit);
                }
            }
        }

        private void OpenROM(string file)
        {
            var newRomToEdit = new SimpleGBAROM();            
            textBox1.Text = Path.GetFileName(file);
            newRomToEdit.OpenROM(file);
            romToEdit = newRomToEdit;
            memoryManager = new Lazy();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}