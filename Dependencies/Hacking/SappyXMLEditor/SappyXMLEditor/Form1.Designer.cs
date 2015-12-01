namespace SappyXMLEditor
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelSappyInfo = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonDeleteROM = new System.Windows.Forms.Button();
            this.buttonNewROM = new System.Windows.Forms.Button();
            this.panelROM = new System.Windows.Forms.Panel();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDeletePlaylist = new System.Windows.Forms.Button();
            this.buttonNewPlaylist = new System.Windows.Forms.Button();
            this.panelPlaylist = new System.Windows.Forms.Panel();
            this.buttonDeleteSong2 = new System.Windows.Forms.Button();
            this.textBoxSongName2 = new System.Windows.Forms.TextBox();
            this.numericUpDownSongNumber2 = new System.Windows.Forms.NumericUpDown();
            this.buttonAddNewSong = new System.Windows.Forms.Button();
            this.buttonDeleteSong1 = new System.Windows.Forms.Button();
            this.textBoxSongName1 = new System.Windows.Forms.TextBox();
            this.numericUpDownSongNumber1 = new System.Windows.Forms.NumericUpDown();
            this.comboBoxPlaylist = new System.Windows.Forms.ComboBox();
            this.textBoxTagger = new System.Windows.Forms.TextBox();
            this.textBoxScreenShot = new System.Windows.Forms.TextBox();
            this.textBoxCreator = new System.Windows.Forms.TextBox();
            this.numericUpDownSongList = new System.Windows.Forms.NumericUpDown();
            this.comboBoxRom = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.panelSappyInfo.SuspendLayout();
            this.panelROM.SuspendLayout();
            this.panelPlaylist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSongNumber2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSongNumber1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSongList)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "XML files|*.xml|All files|*";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "XML files|*.xml|All files|*";
            this.saveFileDialog1.RestoreDirectory = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(271, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.openToolStripMenuItem.Text = "Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // panelSappyInfo
            // 
            this.panelSappyInfo.Controls.Add(this.label7);
            this.panelSappyInfo.Controls.Add(this.buttonDeleteROM);
            this.panelSappyInfo.Controls.Add(this.buttonNewROM);
            this.panelSappyInfo.Controls.Add(this.panelROM);
            this.panelSappyInfo.Controls.Add(this.comboBoxRom);
            this.panelSappyInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSappyInfo.Location = new System.Drawing.Point(0, 24);
            this.panelSappyInfo.Name = "panelSappyInfo";
            this.panelSappyInfo.Size = new System.Drawing.Size(271, 408);
            this.panelSappyInfo.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Rom";
            // 
            // buttonDeleteROM
            // 
            this.buttonDeleteROM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteROM.Location = new System.Drawing.Point(165, 382);
            this.buttonDeleteROM.Name = "buttonDeleteROM";
            this.buttonDeleteROM.Size = new System.Drawing.Size(103, 23);
            this.buttonDeleteROM.TabIndex = 4;
            this.buttonDeleteROM.Text = "Delete ROM";
            this.buttonDeleteROM.UseVisualStyleBackColor = true;
            this.buttonDeleteROM.Click += new System.EventHandler(this.buttonDeleteROM_Click);
            // 
            // buttonNewROM
            // 
            this.buttonNewROM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNewROM.Location = new System.Drawing.Point(3, 382);
            this.buttonNewROM.Name = "buttonNewROM";
            this.buttonNewROM.Size = new System.Drawing.Size(103, 23);
            this.buttonNewROM.TabIndex = 3;
            this.buttonNewROM.Text = "Add new ROM";
            this.buttonNewROM.UseVisualStyleBackColor = true;
            this.buttonNewROM.Click += new System.EventHandler(this.buttonNewROM_Click);
            // 
            // panelROM
            // 
            this.panelROM.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelROM.Controls.Add(this.textBoxCode);
            this.panelROM.Controls.Add(this.label6);
            this.panelROM.Controls.Add(this.label5);
            this.panelROM.Controls.Add(this.label4);
            this.panelROM.Controls.Add(this.label3);
            this.panelROM.Controls.Add(this.label2);
            this.panelROM.Controls.Add(this.label1);
            this.panelROM.Controls.Add(this.buttonDeletePlaylist);
            this.panelROM.Controls.Add(this.buttonNewPlaylist);
            this.panelROM.Controls.Add(this.panelPlaylist);
            this.panelROM.Controls.Add(this.comboBoxPlaylist);
            this.panelROM.Controls.Add(this.textBoxTagger);
            this.panelROM.Controls.Add(this.textBoxScreenShot);
            this.panelROM.Controls.Add(this.textBoxCreator);
            this.panelROM.Controls.Add(this.numericUpDownSongList);
            this.panelROM.Location = new System.Drawing.Point(3, 31);
            this.panelROM.Name = "panelROM";
            this.panelROM.Size = new System.Drawing.Size(265, 345);
            this.panelROM.TabIndex = 2;
            // 
            // textBoxCode
            // 
            this.textBoxCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCode.Location = new System.Drawing.Point(86, 4);
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(176, 20);
            this.textBoxCode.TabIndex = 15;
            this.textBoxCode.TextChanged += new System.EventHandler(this.textBoxCode_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(39, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Playlist";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Tagger";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Screenshot";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Creator";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Song list offset";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Code";
            // 
            // buttonDeletePlaylist
            // 
            this.buttonDeletePlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeletePlaylist.Location = new System.Drawing.Point(153, 319);
            this.buttonDeletePlaylist.Name = "buttonDeletePlaylist";
            this.buttonDeletePlaylist.Size = new System.Drawing.Size(109, 23);
            this.buttonDeletePlaylist.TabIndex = 8;
            this.buttonDeletePlaylist.Text = "Delete playlist";
            this.buttonDeletePlaylist.UseVisualStyleBackColor = true;
            this.buttonDeletePlaylist.Click += new System.EventHandler(this.buttonDeletePlaylist_Click);
            // 
            // buttonNewPlaylist
            // 
            this.buttonNewPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonNewPlaylist.Location = new System.Drawing.Point(4, 319);
            this.buttonNewPlaylist.Name = "buttonNewPlaylist";
            this.buttonNewPlaylist.Size = new System.Drawing.Size(98, 23);
            this.buttonNewPlaylist.TabIndex = 7;
            this.buttonNewPlaylist.Text = "Add new playlist";
            this.buttonNewPlaylist.UseVisualStyleBackColor = true;
            this.buttonNewPlaylist.Click += new System.EventHandler(this.buttonNewPlaylist_Click);
            // 
            // panelPlaylist
            // 
            this.panelPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPlaylist.AutoScroll = true;
            this.panelPlaylist.Controls.Add(this.buttonDeleteSong2);
            this.panelPlaylist.Controls.Add(this.textBoxSongName2);
            this.panelPlaylist.Controls.Add(this.numericUpDownSongNumber2);
            this.panelPlaylist.Controls.Add(this.buttonAddNewSong);
            this.panelPlaylist.Controls.Add(this.buttonDeleteSong1);
            this.panelPlaylist.Controls.Add(this.textBoxSongName1);
            this.panelPlaylist.Controls.Add(this.numericUpDownSongNumber1);
            this.panelPlaylist.Location = new System.Drawing.Point(4, 161);
            this.panelPlaylist.Name = "panelPlaylist";
            this.panelPlaylist.Size = new System.Drawing.Size(258, 152);
            this.panelPlaylist.TabIndex = 6;
            // 
            // buttonDeleteSong2
            // 
            this.buttonDeleteSong2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteSong2.Location = new System.Drawing.Point(206, 56);
            this.buttonDeleteSong2.Name = "buttonDeleteSong2";
            this.buttonDeleteSong2.Size = new System.Drawing.Size(46, 23);
            this.buttonDeleteSong2.TabIndex = 6;
            this.buttonDeleteSong2.Text = "Delete";
            this.buttonDeleteSong2.UseVisualStyleBackColor = true;
            // 
            // textBoxSongName2
            // 
            this.textBoxSongName2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSongName2.Location = new System.Drawing.Point(53, 58);
            this.textBoxSongName2.Name = "textBoxSongName2";
            this.textBoxSongName2.Size = new System.Drawing.Size(147, 20);
            this.textBoxSongName2.TabIndex = 5;
            // 
            // numericUpDownSongNumber2
            // 
            this.numericUpDownSongNumber2.Location = new System.Drawing.Point(5, 58);
            this.numericUpDownSongNumber2.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownSongNumber2.Name = "numericUpDownSongNumber2";
            this.numericUpDownSongNumber2.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSongNumber2.TabIndex = 4;
            // 
            // buttonAddNewSong
            // 
            this.buttonAddNewSong.Location = new System.Drawing.Point(5, 3);
            this.buttonAddNewSong.Name = "buttonAddNewSong";
            this.buttonAddNewSong.Size = new System.Drawing.Size(94, 23);
            this.buttonAddNewSong.TabIndex = 3;
            this.buttonAddNewSong.Text = "Add new song";
            this.buttonAddNewSong.UseVisualStyleBackColor = true;
            this.buttonAddNewSong.Click += new System.EventHandler(this.buttonAddNewSong_Click);
            // 
            // buttonDeleteSong1
            // 
            this.buttonDeleteSong1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDeleteSong1.Location = new System.Drawing.Point(206, 28);
            this.buttonDeleteSong1.Name = "buttonDeleteSong1";
            this.buttonDeleteSong1.Size = new System.Drawing.Size(46, 23);
            this.buttonDeleteSong1.TabIndex = 2;
            this.buttonDeleteSong1.Text = "Delete";
            this.buttonDeleteSong1.UseVisualStyleBackColor = true;
            // 
            // textBoxSongName1
            // 
            this.textBoxSongName1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSongName1.Location = new System.Drawing.Point(52, 31);
            this.textBoxSongName1.Name = "textBoxSongName1";
            this.textBoxSongName1.Size = new System.Drawing.Size(148, 20);
            this.textBoxSongName1.TabIndex = 1;
            // 
            // numericUpDownSongNumber1
            // 
            this.numericUpDownSongNumber1.Location = new System.Drawing.Point(5, 32);
            this.numericUpDownSongNumber1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownSongNumber1.Name = "numericUpDownSongNumber1";
            this.numericUpDownSongNumber1.Size = new System.Drawing.Size(42, 20);
            this.numericUpDownSongNumber1.TabIndex = 0;
            // 
            // comboBoxPlaylist
            // 
            this.comboBoxPlaylist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPlaylist.FormattingEnabled = true;
            this.comboBoxPlaylist.Location = new System.Drawing.Point(86, 134);
            this.comboBoxPlaylist.Name = "comboBoxPlaylist";
            this.comboBoxPlaylist.Size = new System.Drawing.Size(176, 21);
            this.comboBoxPlaylist.TabIndex = 5;
            this.comboBoxPlaylist.SelectedIndexChanged += new System.EventHandler(this.comboBoxPlaylist_SelectedIndexChanged);
            // 
            // textBoxTagger
            // 
            this.textBoxTagger.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTagger.Location = new System.Drawing.Point(86, 108);
            this.textBoxTagger.Name = "textBoxTagger";
            this.textBoxTagger.Size = new System.Drawing.Size(176, 20);
            this.textBoxTagger.TabIndex = 4;
            this.textBoxTagger.TextChanged += new System.EventHandler(this.textBoxTagger_TextChanged);
            // 
            // textBoxScreenShot
            // 
            this.textBoxScreenShot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxScreenShot.Location = new System.Drawing.Point(86, 82);
            this.textBoxScreenShot.Name = "textBoxScreenShot";
            this.textBoxScreenShot.Size = new System.Drawing.Size(176, 20);
            this.textBoxScreenShot.TabIndex = 3;
            this.textBoxScreenShot.TextChanged += new System.EventHandler(this.textBoxScreenShot_TextChanged);
            // 
            // textBoxCreator
            // 
            this.textBoxCreator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCreator.Location = new System.Drawing.Point(86, 56);
            this.textBoxCreator.Name = "textBoxCreator";
            this.textBoxCreator.Size = new System.Drawing.Size(176, 20);
            this.textBoxCreator.TabIndex = 2;
            this.textBoxCreator.TextChanged += new System.EventHandler(this.textBoxCreator_TextChanged);
            // 
            // numericUpDownSongList
            // 
            this.numericUpDownSongList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownSongList.Hexadecimal = true;
            this.numericUpDownSongList.Location = new System.Drawing.Point(86, 30);
            this.numericUpDownSongList.Maximum = new decimal(new int[] {
            33554431,
            0,
            0,
            0});
            this.numericUpDownSongList.Name = "numericUpDownSongList";
            this.numericUpDownSongList.Size = new System.Drawing.Size(176, 20);
            this.numericUpDownSongList.TabIndex = 1;
            this.numericUpDownSongList.ValueChanged += new System.EventHandler(this.numericUpDownSongList_ValueChanged);
            // 
            // comboBoxRom
            // 
            this.comboBoxRom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxRom.Location = new System.Drawing.Point(89, 3);
            this.comboBoxRom.Name = "comboBoxRom";
            this.comboBoxRom.Size = new System.Drawing.Size(177, 21);
            this.comboBoxRom.TabIndex = 1;
            this.comboBoxRom.SelectedIndexChanged += new System.EventHandler(this.comboBoxRom_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(271, 432);
            this.Controls.Add(this.panelSappyInfo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Sappy XML file editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelSappyInfo.ResumeLayout(false);
            this.panelSappyInfo.PerformLayout();
            this.panelROM.ResumeLayout(false);
            this.panelROM.PerformLayout();
            this.panelPlaylist.ResumeLayout(false);
            this.panelPlaylist.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSongNumber2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSongNumber1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSongList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel panelSappyInfo;
        private System.Windows.Forms.ComboBox comboBoxRom;
        private System.Windows.Forms.Button buttonDeleteROM;
        private System.Windows.Forms.Button buttonNewROM;
        private System.Windows.Forms.Panel panelROM;
        private System.Windows.Forms.NumericUpDown numericUpDownSongList;
        private System.Windows.Forms.Button buttonDeletePlaylist;
        private System.Windows.Forms.Button buttonNewPlaylist;
        private System.Windows.Forms.Panel panelPlaylist;
        private System.Windows.Forms.NumericUpDown numericUpDownSongNumber1;
        private System.Windows.Forms.ComboBox comboBoxPlaylist;
        private System.Windows.Forms.TextBox textBoxTagger;
        private System.Windows.Forms.TextBox textBoxScreenShot;
        private System.Windows.Forms.TextBox textBoxCreator;
        private System.Windows.Forms.Button buttonAddNewSong;
        private System.Windows.Forms.Button buttonDeleteSong1;
        private System.Windows.Forms.TextBox textBoxSongName1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonDeleteSong2;
        private System.Windows.Forms.TextBox textBoxSongName2;
        private System.Windows.Forms.NumericUpDown numericUpDownSongNumber2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxCode;
    }
}

