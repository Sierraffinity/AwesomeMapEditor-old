namespace PGMEWindowsUI
{
    partial class SettingsDialog
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
            this.components = new System.ComponentModel.Container();
            this.trackBarPermTrans = new System.Windows.Forms.TrackBar();
            this.labelPermTrans = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.textBoxOtherPrefix = new System.Windows.Forms.TextBox();
            this.radioButtonOtherPrefix = new System.Windows.Forms.RadioButton();
            this.radioButtonAndH = new System.Windows.Forms.RadioButton();
            this.radioButtonDollar = new System.Windows.Forms.RadioButton();
            this.radioButton0x = new System.Windows.Forms.RadioButton();
            this.groupBoxPermTrans = new System.Windows.Forms.GroupBox();
            this.groupBoxHexPrefix = new System.Windows.Forms.GroupBox();
            this.groupBoxScriptEditor = new System.Windows.Forms.GroupBox();
            this.labelScriptEditorName = new System.Windows.Forms.Label();
            this.btnChooseScriptEditor = new System.Windows.Forms.Button();
            this.labelScriptEditor = new System.Windows.Forms.Label();
            this.pictureBoxScriptEditorIcon = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonLanguage = new wyDay.Controls.MenuButton();
            this.cmsLanguages = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.espanolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.françaisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deutschToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxBackUpROM = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPermTrans)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBoxPermTrans.SuspendLayout();
            this.groupBoxHexPrefix.SuspendLayout();
            this.groupBoxScriptEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScriptEditorIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.cmsLanguages.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBarPermTrans
            // 
            this.trackBarPermTrans.LargeChange = 25;
            this.trackBarPermTrans.Location = new System.Drawing.Point(6, 19);
            this.trackBarPermTrans.Maximum = 100;
            this.trackBarPermTrans.Name = "trackBarPermTrans";
            this.trackBarPermTrans.Size = new System.Drawing.Size(260, 45);
            this.trackBarPermTrans.SmallChange = 5;
            this.trackBarPermTrans.TabIndex = 0;
            this.trackBarPermTrans.TickFrequency = 5;
            this.trackBarPermTrans.Scroll += new System.EventHandler(this.trackBarPermTrans_Scroll);
            this.trackBarPermTrans.ValueChanged += new System.EventHandler(this.trackBarPermTrans_ValueChanged);
            // 
            // labelPermTrans
            // 
            this.labelPermTrans.Location = new System.Drawing.Point(6, 51);
            this.labelPermTrans.Name = "labelPermTrans";
            this.labelPermTrans.Size = new System.Drawing.Size(260, 13);
            this.labelPermTrans.TabIndex = 1;
            this.labelPermTrans.Text = "0%";
            this.labelPermTrans.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(257, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(88, 26);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(351, 3);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(88, 26);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "&Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.buttonCancel);
            this.flowLayoutPanel1.Controls.Add(this.buttonOK);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 165);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(442, 34);
            this.flowLayoutPanel1.TabIndex = 4;
            // 
            // textBoxOtherPrefix
            // 
            this.textBoxOtherPrefix.Location = new System.Drawing.Point(26, 85);
            this.textBoxOtherPrefix.MaxLength = 2;
            this.textBoxOtherPrefix.Name = "textBoxOtherPrefix";
            this.textBoxOtherPrefix.Size = new System.Drawing.Size(29, 20);
            this.textBoxOtherPrefix.TabIndex = 14;
            // 
            // radioButtonOtherPrefix
            // 
            this.radioButtonOtherPrefix.AutoSize = true;
            this.radioButtonOtherPrefix.Location = new System.Drawing.Point(6, 88);
            this.radioButtonOtherPrefix.Name = "radioButtonOtherPrefix";
            this.radioButtonOtherPrefix.Size = new System.Drawing.Size(14, 13);
            this.radioButtonOtherPrefix.TabIndex = 13;
            this.radioButtonOtherPrefix.TabStop = true;
            this.radioButtonOtherPrefix.UseVisualStyleBackColor = true;
            this.radioButtonOtherPrefix.CheckedChanged += new System.EventHandler(this.radioButtonPrefix_CheckedChanged);
            // 
            // radioButtonAndH
            // 
            this.radioButtonAndH.AutoSize = true;
            this.radioButtonAndH.Location = new System.Drawing.Point(6, 65);
            this.radioButtonAndH.Name = "radioButtonAndH";
            this.radioButtonAndH.Size = new System.Drawing.Size(39, 17);
            this.radioButtonAndH.TabIndex = 12;
            this.radioButtonAndH.TabStop = true;
            this.radioButtonAndH.Text = "&H";
            this.radioButtonAndH.UseMnemonic = false;
            this.radioButtonAndH.UseVisualStyleBackColor = true;
            this.radioButtonAndH.CheckedChanged += new System.EventHandler(this.radioButtonPrefix_CheckedChanged);
            // 
            // radioButtonDollar
            // 
            this.radioButtonDollar.AutoSize = true;
            this.radioButtonDollar.Location = new System.Drawing.Point(6, 42);
            this.radioButtonDollar.Name = "radioButtonDollar";
            this.radioButtonDollar.Size = new System.Drawing.Size(31, 17);
            this.radioButtonDollar.TabIndex = 11;
            this.radioButtonDollar.TabStop = true;
            this.radioButtonDollar.Text = "$";
            this.radioButtonDollar.UseVisualStyleBackColor = true;
            this.radioButtonDollar.CheckedChanged += new System.EventHandler(this.radioButtonPrefix_CheckedChanged);
            // 
            // radioButton0x
            // 
            this.radioButton0x.AutoSize = true;
            this.radioButton0x.Location = new System.Drawing.Point(6, 19);
            this.radioButton0x.Name = "radioButton0x";
            this.radioButton0x.Size = new System.Drawing.Size(36, 17);
            this.radioButton0x.TabIndex = 10;
            this.radioButton0x.TabStop = true;
            this.radioButton0x.Text = "0x";
            this.radioButton0x.UseVisualStyleBackColor = true;
            this.radioButton0x.CheckedChanged += new System.EventHandler(this.radioButtonPrefix_CheckedChanged);
            // 
            // groupBoxPermTrans
            // 
            this.groupBoxPermTrans.Controls.Add(this.labelPermTrans);
            this.groupBoxPermTrans.Controls.Add(this.trackBarPermTrans);
            this.groupBoxPermTrans.Location = new System.Drawing.Point(12, 12);
            this.groupBoxPermTrans.Name = "groupBoxPermTrans";
            this.groupBoxPermTrans.Size = new System.Drawing.Size(272, 70);
            this.groupBoxPermTrans.TabIndex = 15;
            this.groupBoxPermTrans.TabStop = false;
            this.groupBoxPermTrans.Text = "Permission Transparency";
            // 
            // groupBoxHexPrefix
            // 
            this.groupBoxHexPrefix.Controls.Add(this.radioButton0x);
            this.groupBoxHexPrefix.Controls.Add(this.radioButtonDollar);
            this.groupBoxHexPrefix.Controls.Add(this.textBoxOtherPrefix);
            this.groupBoxHexPrefix.Controls.Add(this.radioButtonAndH);
            this.groupBoxHexPrefix.Controls.Add(this.radioButtonOtherPrefix);
            this.groupBoxHexPrefix.Location = new System.Drawing.Point(290, 12);
            this.groupBoxHexPrefix.Name = "groupBoxHexPrefix";
            this.groupBoxHexPrefix.Size = new System.Drawing.Size(70, 113);
            this.groupBoxHexPrefix.TabIndex = 16;
            this.groupBoxHexPrefix.TabStop = false;
            this.groupBoxHexPrefix.Text = "Hex Prefix";
            // 
            // groupBoxScriptEditor
            // 
            this.groupBoxScriptEditor.Controls.Add(this.labelScriptEditorName);
            this.groupBoxScriptEditor.Controls.Add(this.btnChooseScriptEditor);
            this.groupBoxScriptEditor.Controls.Add(this.labelScriptEditor);
            this.groupBoxScriptEditor.Controls.Add(this.pictureBoxScriptEditorIcon);
            this.groupBoxScriptEditor.Location = new System.Drawing.Point(12, 88);
            this.groupBoxScriptEditor.Name = "groupBoxScriptEditor";
            this.groupBoxScriptEditor.Size = new System.Drawing.Size(272, 73);
            this.groupBoxScriptEditor.TabIndex = 17;
            this.groupBoxScriptEditor.TabStop = false;
            this.groupBoxScriptEditor.Text = "Script Editor";
            // 
            // labelScriptEditorName
            // 
            this.labelScriptEditorName.AutoEllipsis = true;
            this.labelScriptEditorName.Location = new System.Drawing.Point(60, 32);
            this.labelScriptEditorName.Name = "labelScriptEditorName";
            this.labelScriptEditorName.Size = new System.Drawing.Size(112, 35);
            this.labelScriptEditorName.TabIndex = 4;
            // 
            // btnChooseScriptEditor
            // 
            this.btnChooseScriptEditor.Location = new System.Drawing.Point(178, 41);
            this.btnChooseScriptEditor.Name = "btnChooseScriptEditor";
            this.btnChooseScriptEditor.Size = new System.Drawing.Size(88, 26);
            this.btnChooseScriptEditor.TabIndex = 3;
            this.btnChooseScriptEditor.Text = "Choose";
            this.btnChooseScriptEditor.UseVisualStyleBackColor = true;
            this.btnChooseScriptEditor.Click += new System.EventHandler(this.btnChooseScriptEditor_Click);
            // 
            // labelScriptEditor
            // 
            this.labelScriptEditor.AutoEllipsis = true;
            this.labelScriptEditor.Location = new System.Drawing.Point(60, 19);
            this.labelScriptEditor.Name = "labelScriptEditor";
            this.labelScriptEditor.Size = new System.Drawing.Size(206, 13);
            this.labelScriptEditor.TabIndex = 1;
            this.labelScriptEditor.Text = "No script editor selected.";
            // 
            // pictureBoxScriptEditorIcon
            // 
            this.pictureBoxScriptEditorIcon.Image = global::PGMEWindowsUI.Properties.Resources.Question_mark;
            this.pictureBoxScriptEditorIcon.Location = new System.Drawing.Point(6, 19);
            this.pictureBoxScriptEditorIcon.Name = "pictureBoxScriptEditorIcon";
            this.pictureBoxScriptEditorIcon.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxScriptEditorIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxScriptEditorIcon.TabIndex = 0;
            this.pictureBoxScriptEditorIcon.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonLanguage);
            this.groupBox1.Location = new System.Drawing.Point(367, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(87, 48);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Language";
            // 
            // buttonLanguage
            // 
            this.buttonLanguage.AutoSize = true;
            this.buttonLanguage.ContextMenuStrip = this.cmsLanguages;
            this.buttonLanguage.Location = new System.Drawing.Point(6, 19);
            this.buttonLanguage.Name = "buttonLanguage";
            this.buttonLanguage.Size = new System.Drawing.Size(75, 23);
            this.buttonLanguage.SplitMenuStrip = this.cmsLanguages;
            this.buttonLanguage.TabIndex = 0;
            this.buttonLanguage.Text = "English";
            this.buttonLanguage.UseVisualStyleBackColor = true;
            // 
            // cmsLanguages
            // 
            this.cmsLanguages.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.espanolToolStripMenuItem,
            this.françaisToolStripMenuItem,
            this.deutschToolStripMenuItem});
            this.cmsLanguages.Name = "cmsLanguages";
            this.cmsLanguages.Size = new System.Drawing.Size(118, 92);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.CheckOnClick = true;
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.englishToolStripMenuItem.Tag = "en";
            this.englishToolStripMenuItem.Text = "&English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.languageToolStripMenuItem_Click);
            // 
            // espanolToolStripMenuItem
            // 
            this.espanolToolStripMenuItem.CheckOnClick = true;
            this.espanolToolStripMenuItem.Name = "espanolToolStripMenuItem";
            this.espanolToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.espanolToolStripMenuItem.Tag = "es";
            this.espanolToolStripMenuItem.Text = "E&spañol";
            this.espanolToolStripMenuItem.Click += new System.EventHandler(this.languageToolStripMenuItem_Click);
            // 
            // françaisToolStripMenuItem
            // 
            this.françaisToolStripMenuItem.CheckOnClick = true;
            this.françaisToolStripMenuItem.Name = "françaisToolStripMenuItem";
            this.françaisToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.françaisToolStripMenuItem.Tag = "fr";
            this.françaisToolStripMenuItem.Text = "&Français";
            this.françaisToolStripMenuItem.Click += new System.EventHandler(this.languageToolStripMenuItem_Click);
            // 
            // deutschToolStripMenuItem
            // 
            this.deutschToolStripMenuItem.CheckOnClick = true;
            this.deutschToolStripMenuItem.Name = "deutschToolStripMenuItem";
            this.deutschToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.deutschToolStripMenuItem.Tag = "de";
            this.deutschToolStripMenuItem.Text = "&Deutsch";
            this.deutschToolStripMenuItem.Click += new System.EventHandler(this.languageToolStripMenuItem_Click);
            // 
            // checkBoxBackUpROM
            // 
            this.checkBoxBackUpROM.AutoSize = true;
            this.checkBoxBackUpROM.Location = new System.Drawing.Point(290, 135);
            this.checkBoxBackUpROM.Name = "checkBoxBackUpROM";
            this.checkBoxBackUpROM.Size = new System.Drawing.Size(142, 17);
            this.checkBoxBackUpROM.TabIndex = 19;
            this.checkBoxBackUpROM.Text = "Back Up ROM On Open";
            this.checkBoxBackUpROM.UseVisualStyleBackColor = true;
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(466, 205);
            this.Controls.Add(this.checkBoxBackUpROM);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxScriptEditor);
            this.Controls.Add(this.groupBoxHexPrefix);
            this.Controls.Add(this.groupBoxPermTrans);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPermTrans)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBoxPermTrans.ResumeLayout(false);
            this.groupBoxPermTrans.PerformLayout();
            this.groupBoxHexPrefix.ResumeLayout(false);
            this.groupBoxHexPrefix.PerformLayout();
            this.groupBoxScriptEditor.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScriptEditorIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cmsLanguages.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarPermTrans;
        private System.Windows.Forms.Label labelPermTrans;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxOtherPrefix;
        private System.Windows.Forms.RadioButton radioButtonOtherPrefix;
        private System.Windows.Forms.RadioButton radioButtonAndH;
        private System.Windows.Forms.RadioButton radioButtonDollar;
        private System.Windows.Forms.RadioButton radioButton0x;
        private System.Windows.Forms.GroupBox groupBoxPermTrans;
        private System.Windows.Forms.GroupBox groupBoxHexPrefix;
        private System.Windows.Forms.GroupBox groupBoxScriptEditor;
        private System.Windows.Forms.PictureBox pictureBoxScriptEditorIcon;
        private System.Windows.Forms.Button btnChooseScriptEditor;
        private System.Windows.Forms.Label labelScriptEditor;
        private System.Windows.Forms.Label labelScriptEditorName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip cmsLanguages;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem espanolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem françaisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deutschToolStripMenuItem;
        private wyDay.Controls.MenuButton buttonLanguage;
        private System.Windows.Forms.CheckBox checkBoxBackUpROM;
    }
}