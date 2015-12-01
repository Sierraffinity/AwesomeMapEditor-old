namespace TiledMapInserter
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.TiledFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.ROMFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mapOffset = new System.Windows.Forms.NumericUpDown();
            this.writeMapPointer = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mapChangePtrOffset = new System.Windows.Forms.NumericUpDown();
            this.writeMapChangePtr = new System.Windows.Forms.CheckBox();
            this.mapPtrOffset = new System.Windows.Forms.NumericUpDown();
            this.insertMapChanges = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.mapOffset)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapChangePtrOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapPtrOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(58, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(180, 20);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(58, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(180, 20);
            this.textBox2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tiled file";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ROM";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(244, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Browse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(244, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Offset";
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(244, 131);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Run";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Location = new System.Drawing.Point(244, 160);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 8;
            this.button4.Text = "Exit";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // TiledFileDialog
            // 
            this.TiledFileDialog.Filter = "Tiled map files|*.tmx|All files|*.*";
            this.TiledFileDialog.RestoreDirectory = true;
            // 
            // ROMFileDialog
            // 
            this.ROMFileDialog.Filter = "ROM Files|*.gba|Binary files|*.bin|All files|*.*";
            this.ROMFileDialog.OverwritePrompt = false;
            this.ROMFileDialog.RestoreDirectory = true;
            // 
            // mapOffset
            // 
            this.mapOffset.Hexadecimal = true;
            this.mapOffset.Location = new System.Drawing.Point(58, 62);
            this.mapOffset.Name = "mapOffset";
            this.mapOffset.Size = new System.Drawing.Size(143, 20);
            this.mapOffset.TabIndex = 12;
            // 
            // writeMapPointer
            // 
            this.writeMapPointer.AutoSize = true;
            this.writeMapPointer.Location = new System.Drawing.Point(6, 19);
            this.writeMapPointer.Name = "writeMapPointer";
            this.writeMapPointer.Size = new System.Drawing.Size(127, 17);
            this.writeMapPointer.TabIndex = 13;
            this.writeMapPointer.Text = "Write map pointer to?";
            this.writeMapPointer.UseVisualStyleBackColor = true;
            this.writeMapPointer.CheckedChanged += new System.EventHandler(this.mapPtrOffset_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mapChangePtrOffset);
            this.groupBox1.Controls.Add(this.writeMapChangePtr);
            this.groupBox1.Controls.Add(this.mapPtrOffset);
            this.groupBox1.Controls.Add(this.writeMapPointer);
            this.groupBox1.Location = new System.Drawing.Point(8, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(230, 73);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pointer Options";
            // 
            // mapChangePtrOffset
            // 
            this.mapChangePtrOffset.Enabled = false;
            this.mapChangePtrOffset.Hexadecimal = true;
            this.mapChangePtrOffset.Location = new System.Drawing.Point(150, 45);
            this.mapChangePtrOffset.Name = "mapChangePtrOffset";
            this.mapChangePtrOffset.Size = new System.Drawing.Size(74, 20);
            this.mapChangePtrOffset.TabIndex = 16;
            // 
            // writeMapChangePtr
            // 
            this.writeMapChangePtr.AutoSize = true;
            this.writeMapChangePtr.Enabled = false;
            this.writeMapChangePtr.Location = new System.Drawing.Point(6, 45);
            this.writeMapChangePtr.Name = "writeMapChangePtr";
            this.writeMapChangePtr.Size = new System.Drawing.Size(146, 17);
            this.writeMapChangePtr.TabIndex = 15;
            this.writeMapChangePtr.Text = "Write map change ptr to?";
            this.writeMapChangePtr.UseVisualStyleBackColor = true;
            this.writeMapChangePtr.CheckedChanged += new System.EventHandler(this.mapChangePtrOffset_CheckedChanged);
            // 
            // mapPtrOffset
            // 
            this.mapPtrOffset.Enabled = false;
            this.mapPtrOffset.Hexadecimal = true;
            this.mapPtrOffset.Location = new System.Drawing.Point(150, 19);
            this.mapPtrOffset.Name = "mapPtrOffset";
            this.mapPtrOffset.Size = new System.Drawing.Size(74, 20);
            this.mapPtrOffset.TabIndex = 14;
            // 
            // insertMapChanges
            // 
            this.insertMapChanges.AutoSize = true;
            this.insertMapChanges.Location = new System.Drawing.Point(58, 89);
            this.insertMapChanges.Name = "insertMapChanges";
            this.insertMapChanges.Size = new System.Drawing.Size(125, 17);
            this.insertMapChanges.TabIndex = 17;
            this.insertMapChanges.Text = "Insert map changes?";
            this.insertMapChanges.UseVisualStyleBackColor = true;
            this.insertMapChanges.CheckedChanged += new System.EventHandler(this.insertMapChanges_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 190);
            this.Controls.Add(this.insertMapChanges);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mapOffset);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Tiled map inserter";
            ((System.ComponentModel.ISupportInitialize)(this.mapOffset)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mapChangePtrOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapPtrOffset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.OpenFileDialog TiledFileDialog;
        private System.Windows.Forms.SaveFileDialog ROMFileDialog;
        private System.Windows.Forms.NumericUpDown mapOffset;
        private System.Windows.Forms.CheckBox writeMapPointer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown mapPtrOffset;
        private System.Windows.Forms.CheckBox insertMapChanges;
        private System.Windows.Forms.NumericUpDown mapChangePtrOffset;
        private System.Windows.Forms.CheckBox writeMapChangePtr;
    }
}

