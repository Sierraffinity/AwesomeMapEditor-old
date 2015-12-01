using System;
using System.IO;
using System.Windows.Forms;
using Nintenlord.Event_Assembler.Core;

namespace Nintenlord.Event_Assembler.UserInterface
{    
    public partial class MainForm : Form
    {
        String textFile, binaryFile;
        
        public MainForm(string[] args)
        {
            InitializeComponent();
            if (args.Length > 0 && File.Exists(args[0]))
            {
                this.textBox1.Text = Path.GetFullPath(args[0]);
                if (args.Length > 1)
                {
                    this.textBox2.Text = Path.GetFullPath(args[1]);
                }
            }
            this.OffsetControl.Maximum = 0x20000000;
            this.LengthControl.Maximum = 0x20000000;
            this.MinimumSize = this.Size;
#if DEBUG
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(MainForm_KeyDown);
#endif
            this.Move += new EventHandler((x, y) => FormHelpers.ClingToScreenEndges(x as Form, 20));
            this.Load += new EventHandler(MainForm_Load);
        }
        
#if DEBUG
        void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && (e.KeyCode == Keys.P))
            {
                Program.Preprocess(textFile, binaryFile, GetChosenGame());
            }
        }
#endif

        void MainForm_Load(object sender, EventArgs e)
        {
            Program.LoadCodes();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textFile = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            binaryFile = textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Title = "Open text file...";
                openDialog.Filter = "Text files|*.txt|All files|*";
                if (textFile != null)
                {
                    openDialog.InitialDirectory = Path.GetDirectoryName(textFile);
                    openDialog.FileName = Path.GetFileName(textFile);
                }
                openDialog.Multiselect = false;
                openDialog.CheckFileExists = false;
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    textFile = openDialog.FileName;
                    textBox1.Text = textFile;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openDialog = new OpenFileDialog())
            {
                openDialog.Title = "Open ROM file...";
                openDialog.Filter = "GBA ROMs|*.gba|Binary files|*.bin|All files|*";
                if (binaryFile != null)
                {
                    openDialog.InitialDirectory = Path.GetDirectoryName(binaryFile);
                    openDialog.FileName = Path.GetFileName(binaryFile);
                }
                openDialog.Multiselect = false;
                openDialog.CheckFileExists = false;
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    binaryFile = openDialog.FileName;
                    textBox2.Text = binaryFile;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string game = GetChosenGame();
            if (textFile == null || binaryFile == null)
            {
                MessageBox.Show("Choose both files");
            }
            else if (!File.Exists(textFile))
            {
                MessageBox.Show("Text file doesn't exist.");
            }
            else if (!Program.CodesLoaded)
            {
                MessageBox.Show("Raws not loaded");
            }
            else
            {
                Program.Assemble(textFile, binaryFile, game);
            }
        }

        private string GetChosenGame()
        {
            if (radioButton1.Checked)
            {
                return radioButton1.Text;
            }
            if (radioButton2.Checked)
            {
                return radioButton2.Text;
            }
            if (radioButton3.Checked)
            {
                return radioButton3.Text;
            }
            throw new Exception("Unknown game");
        }

        private DisassemblyMode GetChosenMode()
        {
            if (radioButton4.Checked)
            {
                return DisassemblyMode.Block;
            }
            if (radioButton5.Checked)
            {
                return DisassemblyMode.ToEnd;
            }
            if (radioButton6.Checked)
            {
                return DisassemblyMode.Structure;
            }
            throw new Exception("Unknown enum");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string game = GetChosenGame();
            DisassemblyMode mode = GetChosenMode();
            if (textFile == null || binaryFile == null)
            {
                MessageBox.Show("Choose both files");
            }
            else if (!File.Exists(binaryFile))
            {
                MessageBox.Show("ROM file doesn't exist.");
            }
            else if (!Program.CodesLoaded)
            {
                MessageBox.Show("Raws not loaded");
            }
            else
            {
                Program.Disassemble(binaryFile, textFile, game,
                    (int)OffsetControl.Value, (int)LengthControl.Value,
                    mode, this.checkBoxEnder.Checked);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Program.LoadCodes();
        }


        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            LengthControl.Enabled = true;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            LengthControl.Enabled = false;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            LengthControl.Enabled = false;
        }
    }
}