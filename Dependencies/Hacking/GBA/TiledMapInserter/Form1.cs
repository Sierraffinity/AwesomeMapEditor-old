using System;
using System.Collections.Generic;

using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TiledMapInserter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            mapOffset.Maximum = 0x2000000;
            mapPtrOffset.Maximum = 0x2000000;
            mapChangePtrOffset.Maximum = 0x2000000;
            this.MinimumSize = this.Size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TiledFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = TiledFileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ROMFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = ROMFileDialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
                string message;
                try
                {
                    Program.Run(textBox1.Text, textBox2.Text, 
                        (int)mapOffset.Value, 
                        writeMapPointer.Checked ? (int?)mapPtrOffset.Value : null,
                        insertMapChanges.Checked,
                        writeMapChangePtr.Checked ? (int?)mapChangePtrOffset.Value : null);
                    message = "Finished.";
                }
                catch (Exception ex)
                {
                    message = ex.Message + "\nNo data written to ROM.";
                }
                MessageBox.Show(message);

            }
            else
            {
                MessageBox.Show("Please select both ROM and MAR file before proceeding.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mapPtrOffset_CheckedChanged(object sender, EventArgs e)
        {
            this.mapPtrOffset.Enabled = writeMapPointer.Checked;
        }

        private void mapChangePtrOffset_CheckedChanged(object sender, EventArgs e)
        {
            this.mapChangePtrOffset.Enabled = writeMapChangePtr.Checked && insertMapChanges.Checked;

        }

        private void insertMapChanges_CheckedChanged(object sender, EventArgs e)
        {
            if (!insertMapChanges.Checked)
            {
                writeMapChangePtr.Checked = false;                
            }
            writeMapChangePtr.Enabled = insertMapChanges.Checked;
            this.mapChangePtrOffset.Enabled = writeMapChangePtr.Checked && insertMapChanges.Checked;
        }
    }
}