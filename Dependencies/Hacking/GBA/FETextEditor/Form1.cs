using System;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Nintenlord.Forms;

namespace FETextEditor
{
    public delegate void InvokeDelegate();

    public partial class Form1 : Form, IUserInterface
    {
        IApplication application;

        public Form1(IApplication app)
        {
            this.application = app;
            InitializeComponent();
        }
        
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            application.LoadText((int)numericUpDown1.Value);
        }        

        private void openROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = true;
            if (openRomDialog.ShowDialog() == DialogResult.OK)
            {
                LoadROM();
            }
        }

        private void LoadROM()
        {
            using (ControlsDisabler state = new ControlsDisabler(
                new Control[] {
                    numericUpDown1,
                    insertTextButton}))
                using (ToolStripsDisabler state2 = new ToolStripsDisabler(
                    new ToolStripItem[]{
                        saveAsToolStripMenuItem,
                        saveToolStripMenuItem,
                        reloadROMToolStripMenuItem
                }))
                {
                    application.LoadROM(openRomDialog.FileName);
                    application.LoadText((int)numericUpDown1.Value);
                }
        }

        private void chooseFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Font = fontDialog1.Font;
            }
        }

        private void reloadROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            application.LoadText((int)numericUpDown1.Value);
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.WordWrap = wordWrapToolStripMenuItem.Checked;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void insertTextButton_Click(object sender, EventArgs e)
        {
            application.SetText(textBox1.Text, (int)numericUpDown1.Value);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            application.SaveROM();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveRomAsDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                application.SaveROM(saveRomAsDialog.FileName);
            }
        }

        private void insertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (importScriptFileDialog.ShowDialog() == DialogResult.OK)
            {
                application.InsertScript(importScriptFileDialog.FileName);
            }
        }

        private void dumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dumpScriptFileDialog.ShowDialog() == DialogResult.OK)
            {
                application.DumbScript(dumpScriptFileDialog.FileName);
            }

        }

        #region IUserInterface Members

        public int MaxIndex
        {
            get
            {
                return (int)numericUpDown1.Maximum;
            }
            set
            {
                numericUpDown1.Maximum = value;
            }
        }

        public void SetDisplayText(string text)
        {
            textBox1.Text = text;
        }

        #endregion
    }
}
