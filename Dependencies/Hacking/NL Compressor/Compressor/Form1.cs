using System;
using System.Collections.Generic;

using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Nintenlord.Compressor.Compressions;

namespace Nintenlord.Compressor
{
    public partial class Form1 : Form, IUserInterface
    {
        public Form1()
        {
            InitializeComponent();
            this.MinimumSize = this.Size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (inputFileDialog.FileNames.Length > 0)
            {
                inputFileDialog.FileName = Path.GetFileName(inputFileDialog.FileName);
            }
            if (inputFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = inputFileDialog.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (outputFileDialog.FileNames.Length > 0)
            {
                outputFileDialog.FileName = Path.GetFileName(outputFileDialog.FileName);
            }
            if (outputFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = outputFileDialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (OnRun != null)
            {
                if (compressRB.Checked)
                {
                    OnRun(CompressionOperation.Compress);
                }
                else if (decompressRB.Checked)
                {
                    OnRun(CompressionOperation.Decompress);
                }
                else if (scanRB.Checked)
                {
                    OnRun(CompressionOperation.Scan);
                }
                else if (decompressableRB.Checked)
                {
                    OnRun(CompressionOperation.Check);
                }
                else if (complengthRB.Checked)
                {
                    OnRun(CompressionOperation.CompLength);
                }
                else if (decomplengthRB.Checked)
                {
                    OnRun(CompressionOperation.DecompLength);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OnCompressionChanged != null)
            {
                OnCompressionChanged(comboBox1.SelectedIndex);
            }
        }

        #region IUserInterface Members

        public event Action<CompressionOperation> OnRun;

        public event Action<int> OnCompressionChanged;

        public string InputFile
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        public string OutputFile
        {
            get
            {
                return textBox2.Text;
            }
            set
            {
                textBox2.Text = value;
            }
        }

        public int InputOffset
        {
            get
            {
                return (int)numericUpDown1.Value;
            }
            set
            {
                numericUpDown1.Value = value;
            }
        }

        public int InputLength
        {
            get
            {
                return (int)numericUpDown2.Value;
            }
            set
            {
                numericUpDown2.Value = value;
            }
        }

        public int OutputOffset
        {
            get
            {
                return (int)numericUpDown3.Value;
            }
            set
            {
                numericUpDown3.Value = value;
            }
        }

        public bool DecompressEnabled
        {
            get
            {
                return decompressRB.Enabled;
            }
            set
            {
                decompressRB.Enabled = value;
            }
        }

        public bool CompressEnabled
        {
            get
            {
                return compressRB.Enabled;
            }
            set
            {
                compressRB.Enabled = value;
            }
        }

        public bool ScanEnabled
        {
            get
            {
                return scanRB.Enabled;
            }
            set
            {
                scanRB.Enabled = value;
            }
        }

        public bool CheckEnabled
        {
            get
            {
                return decompressableRB.Enabled;
            }
            set
            {
                decompressableRB.Enabled = value;
            }
        }

        public bool LengthEnabled
        {
            get
            {
                return complengthRB.Enabled;
            }
            set
            {
                complengthRB.Enabled = value;
            }
        }

        public bool DecompLenghtEnabled
        {
            get
            {
                return decomplengthRB.Enabled;
            }
            set
            {
                decomplengthRB.Enabled = value;
            }
        }

        public void SetDecompressionNames(string[] names)
        {
            for (int i = 0; i < names.Length; i++)
            {
                names[i] = names[i].Replace('_', ' ');
            }
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(names);
            comboBox1.SelectedIndex = 0;
        }

        public void SetFileExtensions(string[] extensions)
        {
            StringBuilder builder = new StringBuilder("Compression files|");
            for (int i = 0; i < extensions.Length; i++)
            {
                builder.Append("*" + extensions[i]);
                if (i < extensions.Length - 1)
                {
                    builder.Append(";");
                }
            }
            inputFileDialog.Filter = builder.ToString() + "|All Files|*";
            outputFileDialog.Filter = builder.ToString() + "|Text files|*.txt|All Files|*";
        }

        #endregion
    }
}