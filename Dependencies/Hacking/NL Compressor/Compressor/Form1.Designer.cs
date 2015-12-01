namespace Nintenlord.Compressor
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.complengthRB = new System.Windows.Forms.RadioButton();
            this.decompressableRB = new System.Windows.Forms.RadioButton();
            this.scanRB = new System.Windows.Forms.RadioButton();
            this.decompressRB = new System.Windows.Forms.RadioButton();
            this.compressRB = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.inputFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.decomplengthRB = new System.Windows.Forms.RadioButton();
            this.outputFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(265, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Browse...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(265, 70);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Browse...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(247, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = "Inputfile";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 72);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(247, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "Outputfile";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(176, 252);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(77, 25);
            this.button3.TabIndex = 4;
            this.button3.Text = "Run";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(265, 252);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 25);
            this.button4.TabIndex = 5;
            this.button4.Text = "Exit";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.decomplengthRB);
            this.groupBox1.Controls.Add(this.complengthRB);
            this.groupBox1.Controls.Add(this.decompressableRB);
            this.groupBox1.Controls.Add(this.scanRB);
            this.groupBox1.Controls.Add(this.decompressRB);
            this.groupBox1.Controls.Add(this.compressRB);
            this.groupBox1.Location = new System.Drawing.Point(12, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 179);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Action";
            // 
            // complengthRB
            // 
            this.complengthRB.AutoSize = true;
            this.complengthRB.Enabled = false;
            this.complengthRB.Location = new System.Drawing.Point(7, 115);
            this.complengthRB.Name = "complengthRB";
            this.complengthRB.Size = new System.Drawing.Size(124, 17);
            this.complengthRB.TabIndex = 4;
            this.complengthRB.Text = "Lenght (compressed)";
            this.complengthRB.UseVisualStyleBackColor = true;
            // 
            // decompressableRB
            // 
            this.decompressableRB.AutoSize = true;
            this.decompressableRB.Enabled = false;
            this.decompressableRB.Location = new System.Drawing.Point(7, 91);
            this.decompressableRB.Name = "decompressableRB";
            this.decompressableRB.Size = new System.Drawing.Size(104, 17);
            this.decompressableRB.TabIndex = 3;
            this.decompressableRB.Text = "Decompressable";
            this.decompressableRB.UseVisualStyleBackColor = true;
            // 
            // scanRB
            // 
            this.scanRB.AutoSize = true;
            this.scanRB.Enabled = false;
            this.scanRB.Location = new System.Drawing.Point(7, 67);
            this.scanRB.Name = "scanRB";
            this.scanRB.Size = new System.Drawing.Size(50, 17);
            this.scanRB.TabIndex = 2;
            this.scanRB.Text = "Scan";
            this.scanRB.UseVisualStyleBackColor = true;
            // 
            // decompressRB
            // 
            this.decompressRB.AutoSize = true;
            this.decompressRB.Enabled = false;
            this.decompressRB.Location = new System.Drawing.Point(7, 44);
            this.decompressRB.Name = "decompressRB";
            this.decompressRB.Size = new System.Drawing.Size(84, 17);
            this.decompressRB.TabIndex = 1;
            this.decompressRB.Text = "Decompress";
            this.decompressRB.UseVisualStyleBackColor = true;
            // 
            // compressRB
            // 
            this.compressRB.AutoSize = true;
            this.compressRB.Checked = true;
            this.compressRB.Enabled = false;
            this.compressRB.Location = new System.Drawing.Point(7, 20);
            this.compressRB.Name = "compressRB";
            this.compressRB.Size = new System.Drawing.Size(71, 17);
            this.compressRB.TabIndex = 0;
            this.compressRB.TabStop = true;
            this.compressRB.Text = "Compress";
            this.compressRB.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDown2);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(182, 167);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(158, 79);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Input info";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Hexadecimal = true;
            this.numericUpDown2.Location = new System.Drawing.Point(47, 49);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            33554432,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(105, 20);
            this.numericUpDown2.TabIndex = 5;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Hexadecimal = true;
            this.numericUpDown1.Location = new System.Drawing.Point(47, 21);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            33554432,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(105, 20);
            this.numericUpDown1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Size";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Offset";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numericUpDown3);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(176, 98);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(164, 62);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Output Info";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Hexadecimal = true;
            this.numericUpDown3.Location = new System.Drawing.Point(63, 24);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            33554432,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(91, 20);
            this.numericUpDown3.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Offset";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(183, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(157, 21);
            this.comboBox1.TabIndex = 10;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Compression";
            // 
            // inputFileDialog
            // 
            this.inputFileDialog.RestoreDirectory = true;
            this.inputFileDialog.Title = "Open input file";
            // 
            // decomplengthRB
            // 
            this.decomplengthRB.AutoSize = true;
            this.decomplengthRB.Location = new System.Drawing.Point(7, 139);
            this.decomplengthRB.Name = "decomplengthRB";
            this.decomplengthRB.Size = new System.Drawing.Size(136, 17);
            this.decomplengthRB.TabIndex = 5;
            this.decomplengthRB.TabStop = true;
            this.decomplengthRB.Text = "Length (decompressed)";
            this.decomplengthRB.UseVisualStyleBackColor = true;
            // 
            // outputFileDialog
            // 
            this.outputFileDialog.OverwritePrompt = false;
            this.outputFileDialog.RestoreDirectory = true;
            this.outputFileDialog.Title = "Open output file";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 285);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "NL Compressor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton scanRB;
        private System.Windows.Forms.RadioButton decompressRB;
        private System.Windows.Forms.RadioButton compressRB;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton complengthRB;
        private System.Windows.Forms.RadioButton decompressableRB;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog inputFileDialog;
        private System.Windows.Forms.RadioButton decomplengthRB;
        private System.Windows.Forms.SaveFileDialog outputFileDialog;
    }
}

