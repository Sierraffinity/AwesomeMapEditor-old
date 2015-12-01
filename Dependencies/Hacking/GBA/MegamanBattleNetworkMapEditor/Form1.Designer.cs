using T = System.Windows.Forms.Panel; //Nintenlord.MegamanBattleNetworkMapEditor.ResizeDrawPanel;

namespace Nintenlord.MegamanBattleNetworkMapEditor
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tileView1 = new Nintenlord.MegamanBattleNetworkMapEditor.TileView();
            this.mapEditor1 = new Nintenlord.MegamanBattleNetworkMapEditor.MapEditor();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.tileView1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(236, 512);
            this.panel2.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.mapEditor1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(242, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 512);
            this.panel1.TabIndex = 2;
            // 
            // tileView1
            // 
            this.tileView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileView1.Location = new System.Drawing.Point(0, 0);
            this.tileView1.Name = "tileView1";
            this.tileView1.OrderOfTiles = Nintenlord.MegamanBattleNetworkMapEditor.TileOrder.LeftToRight;
            this.tileView1.OrderToDraw = Nintenlord.MegamanBattleNetworkMapEditor.TileOrder.LeftToRight;
            this.tileView1.Size = new System.Drawing.Size(232, 508);
            this.tileView1.TabIndex = 0;
            this.tileView1.Tiles = null;
            this.tileView1.TileScale = 1F;
            this.tileView1.TileSize = new System.Drawing.Size(0, 0);
            // 
            // mapEditor1
            // 
            this.mapEditor1.AllowDrop = true;
            this.mapEditor1.Location = new System.Drawing.Point(0, 0);
            this.mapEditor1.Map = null;
            this.mapEditor1.MapSize = new System.Drawing.Size(0, 0);
            this.mapEditor1.Name = "mapEditor1";
            this.mapEditor1.Size = new System.Drawing.Size(260, 508);
            this.mapEditor1.TabIndex = 1;
            this.mapEditor1.TileView = null;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 512);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TileView tileView1;
        private MapEditor mapEditor1;
        private T panel1;
        private T panel2;


    }
}

