namespace PortraitInserter
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
            this.portraitPictureBox = new System.Windows.Forms.PictureBox();
            this.portraitIndex = new System.Windows.Forms.NumericUpDown();
            this.eyesClosedCheckBox = new System.Windows.Forms.CheckBox();
            this.savePortraitButton = new System.Windows.Forms.Button();
            this.loadPortraitButton = new System.Windows.Forms.Button();
            this.newPortraitButton = new System.Windows.Forms.Button();
            this.copyToNewPortrait = new System.Windows.Forms.Button();
            this.eyeXnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.eyeYnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.mouthXnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.mouthYnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.savePortraitDialog = new System.Windows.Forms.SaveFileDialog();
            this.openPortraitDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.portraitPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.portraitIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eyeXnumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eyeYnumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mouthXnumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mouthYnumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // portraitPictureBox
            // 
            this.portraitPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.portraitPictureBox.Location = new System.Drawing.Point(12, 12);
            this.portraitPictureBox.Name = "portraitPictureBox";
            this.portraitPictureBox.Size = new System.Drawing.Size(256, 224);
            this.portraitPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.portraitPictureBox.TabIndex = 0;
            this.portraitPictureBox.TabStop = false;
            // 
            // portraitIndex
            // 
            this.portraitIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.portraitIndex.Hexadecimal = true;
            this.portraitIndex.Location = new System.Drawing.Point(274, 12);
            this.portraitIndex.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.portraitIndex.Name = "portraitIndex";
            this.portraitIndex.Size = new System.Drawing.Size(69, 20);
            this.portraitIndex.TabIndex = 1;
            this.portraitIndex.ValueChanged += new System.EventHandler(this.portraitIndex_ValueChanged);
            // 
            // eyesClosedCheckBox
            // 
            this.eyesClosedCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eyesClosedCheckBox.AutoSize = true;
            this.eyesClosedCheckBox.Location = new System.Drawing.Point(273, 38);
            this.eyesClosedCheckBox.Name = "eyesClosedCheckBox";
            this.eyesClosedCheckBox.Size = new System.Drawing.Size(118, 17);
            this.eyesClosedCheckBox.TabIndex = 2;
            this.eyesClosedCheckBox.Text = "Eyes always closed";
            this.eyesClosedCheckBox.UseVisualStyleBackColor = true;
            this.eyesClosedCheckBox.CheckedChanged += new System.EventHandler(this.eyesClosedCheckBox_CheckedChanged);
            // 
            // savePortraitButton
            // 
            this.savePortraitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.savePortraitButton.Location = new System.Drawing.Point(273, 128);
            this.savePortraitButton.Name = "savePortraitButton";
            this.savePortraitButton.Size = new System.Drawing.Size(91, 23);
            this.savePortraitButton.TabIndex = 3;
            this.savePortraitButton.Text = "Save portrait...";
            this.savePortraitButton.UseVisualStyleBackColor = true;
            this.savePortraitButton.Click += new System.EventHandler(this.savePortraitButton_Click);
            // 
            // loadPortraitButton
            // 
            this.loadPortraitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadPortraitButton.Location = new System.Drawing.Point(273, 155);
            this.loadPortraitButton.Name = "loadPortraitButton";
            this.loadPortraitButton.Size = new System.Drawing.Size(91, 23);
            this.loadPortraitButton.TabIndex = 4;
            this.loadPortraitButton.Text = "Load portrait...";
            this.loadPortraitButton.UseVisualStyleBackColor = true;
            this.loadPortraitButton.Click += new System.EventHandler(this.loadPortraitButton_Click);
            // 
            // newPortraitButton
            // 
            this.newPortraitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newPortraitButton.Location = new System.Drawing.Point(273, 184);
            this.newPortraitButton.Name = "newPortraitButton";
            this.newPortraitButton.Size = new System.Drawing.Size(117, 23);
            this.newPortraitButton.TabIndex = 5;
            this.newPortraitButton.Text = "Add new portrait...";
            this.newPortraitButton.UseVisualStyleBackColor = true;
            this.newPortraitButton.Click += new System.EventHandler(this.newPortraitButton_Click);
            // 
            // copyToNewPortrait
            // 
            this.copyToNewPortrait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.copyToNewPortrait.Location = new System.Drawing.Point(273, 213);
            this.copyToNewPortrait.Name = "copyToNewPortrait";
            this.copyToNewPortrait.Size = new System.Drawing.Size(117, 23);
            this.copyToNewPortrait.TabIndex = 6;
            this.copyToNewPortrait.Text = "Copy to new portrait";
            this.copyToNewPortrait.UseVisualStyleBackColor = true;
            this.copyToNewPortrait.Click += new System.EventHandler(this.copyToNewPortrait_Click);
            // 
            // numericUpDown1
            // 
            this.eyeXnumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eyeXnumericUpDown.Location = new System.Drawing.Point(270, 61);
            this.eyeXnumericUpDown.Name = "numericUpDown1";
            this.eyeXnumericUpDown.Size = new System.Drawing.Size(37, 20);
            this.eyeXnumericUpDown.TabIndex = 7;
            this.eyeXnumericUpDown.ValueChanged += new System.EventHandler(this.eyeXnumericUpDown_ValueChanged);
            // 
            // numericUpDown2
            // 
            this.eyeYnumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.eyeYnumericUpDown.Location = new System.Drawing.Point(313, 61);
            this.eyeYnumericUpDown.Name = "numericUpDown2";
            this.eyeYnumericUpDown.Size = new System.Drawing.Size(37, 20);
            this.eyeYnumericUpDown.TabIndex = 8;
            this.eyeYnumericUpDown.ValueChanged += new System.EventHandler(this.eyeYnumericUpDown_ValueChanged);
            // 
            // numericUpDown3
            // 
            this.mouthXnumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mouthXnumericUpDown.Location = new System.Drawing.Point(270, 87);
            this.mouthXnumericUpDown.Name = "numericUpDown3";
            this.mouthXnumericUpDown.Size = new System.Drawing.Size(37, 20);
            this.mouthXnumericUpDown.TabIndex = 9;
            this.mouthXnumericUpDown.ValueChanged += new System.EventHandler(this.mouthXnumericUpDown_ValueChanged);
            // 
            // numericUpDown4
            // 
            this.mouthYnumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mouthYnumericUpDown.Location = new System.Drawing.Point(313, 87);
            this.mouthYnumericUpDown.Name = "numericUpDown4";
            this.mouthYnumericUpDown.Size = new System.Drawing.Size(37, 20);
            this.mouthYnumericUpDown.TabIndex = 10;
            this.mouthYnumericUpDown.ValueChanged += new System.EventHandler(this.mouthYnumericUpDown_ValueChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Location = new System.Drawing.Point(12, 242);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 32);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // savePortraitDialog
            // 
            this.savePortraitDialog.Filter = "PNG files|*.png|GIF files|*.gif|Bitmap files|*.bmp|All files|*.*";
            // 
            // openPortraitDialog
            // 
            this.openPortraitDialog.Filter = "PNG files|*.png|GIF files|*.gif|Bitmap files|*.bmp|All files|*.*";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 286);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.mouthYnumericUpDown);
            this.Controls.Add(this.mouthXnumericUpDown);
            this.Controls.Add(this.eyeYnumericUpDown);
            this.Controls.Add(this.eyeXnumericUpDown);
            this.Controls.Add(this.copyToNewPortrait);
            this.Controls.Add(this.newPortraitButton);
            this.Controls.Add(this.loadPortraitButton);
            this.Controls.Add(this.savePortraitButton);
            this.Controls.Add(this.eyesClosedCheckBox);
            this.Controls.Add(this.portraitIndex);
            this.Controls.Add(this.portraitPictureBox);
            this.Name = "Form1";
            this.Text = "Portrait editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.portraitPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.portraitIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eyeXnumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eyeYnumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mouthXnumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mouthYnumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox portraitPictureBox;
        private System.Windows.Forms.NumericUpDown portraitIndex;
        private System.Windows.Forms.CheckBox eyesClosedCheckBox;
        private System.Windows.Forms.Button savePortraitButton;
        private System.Windows.Forms.Button loadPortraitButton;
        private System.Windows.Forms.Button newPortraitButton;
        private System.Windows.Forms.Button copyToNewPortrait;
        private System.Windows.Forms.NumericUpDown eyeXnumericUpDown;
        private System.Windows.Forms.NumericUpDown eyeYnumericUpDown;
        private System.Windows.Forms.NumericUpDown mouthXnumericUpDown;
        private System.Windows.Forms.NumericUpDown mouthYnumericUpDown;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.SaveFileDialog savePortraitDialog;
        private System.Windows.Forms.OpenFileDialog openPortraitDialog;
    }
}

