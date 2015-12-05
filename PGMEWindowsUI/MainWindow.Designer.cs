// Awesome Map Editor
// A map editor for GBA Pokémon games.

// Copyright (C) 2015 Diegoisawesome

// This file is part of Awesome Map Editor.
// Awesome Map Editor is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// Awesome Map Editor is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with Awesome Map Editor. If not, see <http://www.gnu.org/licenses/>.

namespace PGMEWindowsUI
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.splitMapListAndPaint = new System.Windows.Forms.SplitContainer();
            this.tsMapListTree = new System.Windows.Forms.ToolStrip();
            this.tsddbMapSortOrder = new System.Windows.Forms.ToolStripDropDownButton();
            this.mapNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapBankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapTilesetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapListTreeView = new System.Windows.Forms.TreeView();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.mapTabPage = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.borderBlocksBox = new System.Windows.Forms.GroupBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.mapEditorPanel = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.glControlMapEditor = new OpenTK.GLControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cboTimeofDayMap = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton13 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton14 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
            this.paintTabControl = new System.Windows.Forms.TabControl();
            this.blocksTabPage = new System.Windows.Forms.TabPage();
            this.blockPaintPanel = new System.Windows.Forms.Panel();
            this.glControlBlocks = new OpenTK.GLControl();
            this.movementTabPage = new System.Windows.Forms.TabPage();
            this.movementPaintPanel = new System.Windows.Forms.Panel();
            this.eventsTabPage = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboEventTypes = new System.Windows.Forms.ComboBox();
            this.numericUpDown10 = new System.Windows.Forms.NumericUpDown();
            this.panelSpriteEvent = new System.Windows.Forms.Panel();
            this.textBox19 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numericUpDown7 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.panelSignEvent = new System.Windows.Forms.Panel();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textBox26 = new System.Windows.Forms.TextBox();
            this.label33 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.comboBox9 = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.textBox27 = new System.Windows.Forms.TextBox();
            this.textBox28 = new System.Windows.Forms.TextBox();
            this.panelScriptEvent = new System.Windows.Forms.Panel();
            this.label37 = new System.Windows.Forms.Label();
            this.textBox29 = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.textBox20 = new System.Windows.Forms.TextBox();
            this.label32 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.textBox24 = new System.Windows.Forms.TextBox();
            this.textBox25 = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.textBox23 = new System.Windows.Forms.TextBox();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.textBox21 = new System.Windows.Forms.TextBox();
            this.textBox22 = new System.Windows.Forms.TextBox();
            this.panelWarpEvent = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.cboTimeofDayEvents = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripButton16 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton17 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.wildTabPage = new System.Windows.Forms.TabPage();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button10 = new System.Windows.Forms.Button();
            this.cboEncounterTypes = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.grbGrassEncounters = new System.Windows.Forms.GroupBox();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.pictureBox14 = new System.Windows.Forms.PictureBox();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label50 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.comboBox16 = new System.Windows.Forms.ComboBox();
            this.textBox39 = new System.Windows.Forms.TextBox();
            this.numericUpDown24 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown25 = new System.Windows.Forms.NumericUpDown();
            this.comboBox17 = new System.Windows.Forms.ComboBox();
            this.textBox40 = new System.Windows.Forms.TextBox();
            this.numericUpDown26 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown27 = new System.Windows.Forms.NumericUpDown();
            this.comboBox18 = new System.Windows.Forms.ComboBox();
            this.textBox41 = new System.Windows.Forms.TextBox();
            this.numericUpDown28 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown29 = new System.Windows.Forms.NumericUpDown();
            this.comboBox19 = new System.Windows.Forms.ComboBox();
            this.textBox42 = new System.Windows.Forms.TextBox();
            this.numericUpDown30 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown31 = new System.Windows.Forms.NumericUpDown();
            this.comboBox12 = new System.Windows.Forms.ComboBox();
            this.textBox35 = new System.Windows.Forms.TextBox();
            this.numericUpDown16 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown17 = new System.Windows.Forms.NumericUpDown();
            this.comboBox13 = new System.Windows.Forms.ComboBox();
            this.textBox36 = new System.Windows.Forms.TextBox();
            this.numericUpDown18 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown19 = new System.Windows.Forms.NumericUpDown();
            this.comboBox14 = new System.Windows.Forms.ComboBox();
            this.textBox37 = new System.Windows.Forms.TextBox();
            this.numericUpDown20 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown21 = new System.Windows.Forms.NumericUpDown();
            this.comboBox15 = new System.Windows.Forms.ComboBox();
            this.textBox38 = new System.Windows.Forms.TextBox();
            this.numericUpDown22 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown23 = new System.Windows.Forms.NumericUpDown();
            this.comboBox11 = new System.Windows.Forms.ComboBox();
            this.textBox34 = new System.Windows.Forms.TextBox();
            this.numericUpDown14 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown15 = new System.Windows.Forms.NumericUpDown();
            this.comboBox10 = new System.Windows.Forms.ComboBox();
            this.textBox33 = new System.Windows.Forms.TextBox();
            this.numericUpDown12 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown13 = new System.Windows.Forms.NumericUpDown();
            this.comboBox8 = new System.Windows.Forms.ComboBox();
            this.textBox32 = new System.Windows.Forms.TextBox();
            this.numericUpDown9 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown11 = new System.Windows.Forms.NumericUpDown();
            this.label43 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.textBox31 = new System.Windows.Forms.TextBox();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown8 = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label39 = new System.Windows.Forms.Label();
            this.textBox30 = new System.Windows.Forms.TextBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.grbFishingRodEncounters = new System.Windows.Forms.GroupBox();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.pictureBox28 = new System.Windows.Forms.PictureBox();
            this.pictureBox32 = new System.Windows.Forms.PictureBox();
            this.pictureBox29 = new System.Windows.Forms.PictureBox();
            this.numericUpDown75 = new System.Windows.Forms.NumericUpDown();
            this.pictureBox30 = new System.Windows.Forms.PictureBox();
            this.numericUpDown74 = new System.Windows.Forms.NumericUpDown();
            this.pictureBox31 = new System.Windows.Forms.PictureBox();
            this.textBox73 = new System.Windows.Forms.TextBox();
            this.comboBox46 = new System.Windows.Forms.ComboBox();
            this.label75 = new System.Windows.Forms.Label();
            this.numericUpDown73 = new System.Windows.Forms.NumericUpDown();
            this.label76 = new System.Windows.Forms.Label();
            this.numericUpDown72 = new System.Windows.Forms.NumericUpDown();
            this.label95 = new System.Windows.Forms.Label();
            this.textBox69 = new System.Windows.Forms.TextBox();
            this.label96 = new System.Windows.Forms.Label();
            this.comboBox45 = new System.Windows.Forms.ComboBox();
            this.label97 = new System.Windows.Forms.Label();
            this.numericUpDown71 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown70 = new System.Windows.Forms.NumericUpDown();
            this.comboBox42 = new System.Windows.Forms.ComboBox();
            this.textBox68 = new System.Windows.Forms.TextBox();
            this.textBox66 = new System.Windows.Forms.TextBox();
            this.comboBox44 = new System.Windows.Forms.ComboBox();
            this.numericUpDown66 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown69 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown67 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown68 = new System.Windows.Forms.NumericUpDown();
            this.comboBox43 = new System.Windows.Forms.ComboBox();
            this.textBox67 = new System.Windows.Forms.TextBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.pictureBox35 = new System.Windows.Forms.PictureBox();
            this.numericUpDown81 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown80 = new System.Windows.Forms.NumericUpDown();
            this.textBox83 = new System.Windows.Forms.TextBox();
            this.comboBox49 = new System.Windows.Forms.ComboBox();
            this.pictureBox33 = new System.Windows.Forms.PictureBox();
            this.numericUpDown79 = new System.Windows.Forms.NumericUpDown();
            this.pictureBox34 = new System.Windows.Forms.PictureBox();
            this.numericUpDown78 = new System.Windows.Forms.NumericUpDown();
            this.textBox76 = new System.Windows.Forms.TextBox();
            this.comboBox48 = new System.Windows.Forms.ComboBox();
            this.numericUpDown77 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown76 = new System.Windows.Forms.NumericUpDown();
            this.textBox74 = new System.Windows.Forms.TextBox();
            this.comboBox47 = new System.Windows.Forms.ComboBox();
            this.label98 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.pictureBox37 = new System.Windows.Forms.PictureBox();
            this.numericUpDown85 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown84 = new System.Windows.Forms.NumericUpDown();
            this.textBox85 = new System.Windows.Forms.TextBox();
            this.comboBox51 = new System.Windows.Forms.ComboBox();
            this.numericUpDown83 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown82 = new System.Windows.Forms.NumericUpDown();
            this.textBox84 = new System.Windows.Forms.TextBox();
            this.pictureBox36 = new System.Windows.Forms.PictureBox();
            this.comboBox50 = new System.Windows.Forms.ComboBox();
            this.label103 = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.label115 = new System.Windows.Forms.Label();
            this.label117 = new System.Windows.Forms.Label();
            this.label118 = new System.Windows.Forms.Label();
            this.label119 = new System.Windows.Forms.Label();
            this.label120 = new System.Windows.Forms.Label();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.label121 = new System.Windows.Forms.Label();
            this.trackBar4 = new System.Windows.Forms.TrackBar();
            this.textBox82 = new System.Windows.Forms.TextBox();
            this.grbOtherEncounters = new System.Windows.Forms.GroupBox();
            this.pictureBox16 = new System.Windows.Forms.PictureBox();
            this.pictureBox17 = new System.Windows.Forms.PictureBox();
            this.pictureBox18 = new System.Windows.Forms.PictureBox();
            this.pictureBox19 = new System.Windows.Forms.PictureBox();
            this.pictureBox20 = new System.Windows.Forms.PictureBox();
            this.label57 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.comboBox30 = new System.Windows.Forms.ComboBox();
            this.textBox43 = new System.Windows.Forms.TextBox();
            this.numericUpDown32 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown33 = new System.Windows.Forms.NumericUpDown();
            this.comboBox31 = new System.Windows.Forms.ComboBox();
            this.textBox60 = new System.Windows.Forms.TextBox();
            this.numericUpDown34 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown35 = new System.Windows.Forms.NumericUpDown();
            this.comboBox32 = new System.Windows.Forms.ComboBox();
            this.textBox63 = new System.Windows.Forms.TextBox();
            this.numericUpDown46 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown47 = new System.Windows.Forms.NumericUpDown();
            this.comboBox33 = new System.Windows.Forms.ComboBox();
            this.textBox70 = new System.Windows.Forms.TextBox();
            this.numericUpDown48 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown49 = new System.Windows.Forms.NumericUpDown();
            this.label85 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.comboBox34 = new System.Windows.Forms.ComboBox();
            this.textBox71 = new System.Windows.Forms.TextBox();
            this.numericUpDown50 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown51 = new System.Windows.Forms.NumericUpDown();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.label94 = new System.Windows.Forms.Label();
            this.textBox72 = new System.Windows.Forms.TextBox();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.grbWaterEncounters = new System.Windows.Forms.GroupBox();
            this.pictureBox23 = new System.Windows.Forms.PictureBox();
            this.pictureBox24 = new System.Windows.Forms.PictureBox();
            this.pictureBox25 = new System.Windows.Forms.PictureBox();
            this.pictureBox26 = new System.Windows.Forms.PictureBox();
            this.pictureBox27 = new System.Windows.Forms.PictureBox();
            this.label93 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.label110 = new System.Windows.Forms.Label();
            this.label122 = new System.Windows.Forms.Label();
            this.label123 = new System.Windows.Forms.Label();
            this.label124 = new System.Windows.Forms.Label();
            this.comboBox37 = new System.Windows.Forms.ComboBox();
            this.textBox87 = new System.Windows.Forms.TextBox();
            this.numericUpDown56 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown57 = new System.Windows.Forms.NumericUpDown();
            this.comboBox38 = new System.Windows.Forms.ComboBox();
            this.textBox88 = new System.Windows.Forms.TextBox();
            this.numericUpDown58 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown59 = new System.Windows.Forms.NumericUpDown();
            this.comboBox39 = new System.Windows.Forms.ComboBox();
            this.textBox89 = new System.Windows.Forms.TextBox();
            this.numericUpDown60 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown61 = new System.Windows.Forms.NumericUpDown();
            this.comboBox40 = new System.Windows.Forms.ComboBox();
            this.textBox90 = new System.Windows.Forms.TextBox();
            this.numericUpDown62 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown63 = new System.Windows.Forms.NumericUpDown();
            this.label125 = new System.Windows.Forms.Label();
            this.label126 = new System.Windows.Forms.Label();
            this.label127 = new System.Windows.Forms.Label();
            this.label128 = new System.Windows.Forms.Label();
            this.comboBox41 = new System.Windows.Forms.ComboBox();
            this.textBox91 = new System.Windows.Forms.TextBox();
            this.numericUpDown64 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown65 = new System.Windows.Forms.NumericUpDown();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.label129 = new System.Windows.Forms.Label();
            this.textBox92 = new System.Windows.Forms.TextBox();
            this.trackBar5 = new System.Windows.Forms.TrackBar();
            this.headerTabPage = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbHeaderTabMapHeader = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbHeaderTabRawMapHeader = new System.Windows.Forms.GroupBox();
            this.hexViewerRawMapHeader = new Be.Windows.Forms.HexBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.hexPrefixBox7 = new System.Windows.Forms.TextBox();
            this.hexNumberBoxHeaderTabLayoutHeaderPointer = new System.Windows.Forms.TextBox();
            this.hexPrefixBox4 = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.hexPrefixBox5 = new System.Windows.Forms.TextBox();
            this.hexNumberBoxHeaderTabMapNames = new System.Windows.Forms.TextBox();
            this.button11 = new System.Windows.Forms.Button();
            this.cbHeaderTabMapNames = new System.Windows.Forms.ComboBox();
            this.hexPrefixBox2 = new System.Windows.Forms.TextBox();
            this.cbHeaderTabMusic = new System.Windows.Forms.ComboBox();
            this.hexPrefixBox3 = new System.Windows.Forms.TextBox();
            this.hexNumberBoxHeaderTabMusic = new System.Windows.Forms.TextBox();
            this.hexPrefixBox1 = new System.Windows.Forms.TextBox();
            this.label64 = new System.Windows.Forms.Label();
            this.cbHeaderTabCanEscape = new System.Windows.Forms.CheckBox();
            this.cbHeaderTabVisibility = new System.Windows.Forms.ComboBox();
            this.cbHeaderTabCanRideBike = new System.Windows.Forms.CheckBox();
            this.cbHeaderTabWeather = new System.Windows.Forms.ComboBox();
            this.cbHeaderTabCanRun = new System.Windows.Forms.CheckBox();
            this.label65 = new System.Windows.Forms.Label();
            this.cbHeaderTabShowsName = new System.Windows.Forms.CheckBox();
            this.label66 = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.hexPrefixBox6 = new System.Windows.Forms.TextBox();
            this.button12 = new System.Windows.Forms.Button();
            this.hexNumberBoxHeaderTabLayoutIndex = new System.Windows.Forms.TextBox();
            this.cbHeaderTabMapType = new System.Windows.Forms.ComboBox();
            this.hexNumberBoxHeaderTabConnectionDataPointer = new System.Windows.Forms.TextBox();
            this.cbHeaderTabBattleTransition = new System.Windows.Forms.ComboBox();
            this.label72 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.hexNumberBoxHeaderTabEventDataPointer = new System.Windows.Forms.TextBox();
            this.label67 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.hexNumberBoxHeaderTabLevelScriptPointer = new System.Windows.Forms.TextBox();
            this.label70 = new System.Windows.Forms.Label();
            this.cbHeaderTabShowRawMapHeader = new System.Windows.Forms.CheckBox();
            this.gbHeaderTabLayoutHeader = new System.Windows.Forms.GroupBox();
            this.cbHeaderTabShowRawLayoutHeader = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbHeaderTabRawLayoutHeader = new System.Windows.Forms.GroupBox();
            this.hexViewerRawLayoutHeader = new Be.Windows.Forms.HexBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.hexPrefixBox11 = new System.Windows.Forms.TextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label81 = new System.Windows.Forms.Label();
            this.tbHeaderTabMapWidth = new System.Windows.Forms.TextBox();
            this.tbHeaderTabMapHeight = new System.Windows.Forms.TextBox();
            this.label79 = new System.Windows.Forms.Label();
            this.hexPrefixBox9 = new System.Windows.Forms.TextBox();
            this.label80 = new System.Windows.Forms.Label();
            this.hexPrefixBox10 = new System.Windows.Forms.TextBox();
            this.hexNumberBoxHeaderTabBorderPointer = new System.Windows.Forms.TextBox();
            this.hexPrefixBox8 = new System.Windows.Forms.TextBox();
            this.label88 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label90 = new System.Windows.Forms.Label();
            this.tbHeaderTabBorderWidth = new System.Windows.Forms.TextBox();
            this.tbHeaderTabBorderHeight = new System.Windows.Forms.TextBox();
            this.label91 = new System.Windows.Forms.Label();
            this.hexNumberBoxHeaderTabGlobalTilesetPointer = new System.Windows.Forms.TextBox();
            this.label87 = new System.Windows.Forms.Label();
            this.hexNumberBoxHeaderTabLocalTilesetPointer = new System.Windows.Forms.TextBox();
            this.hexNumberBoxHeaderTabMapPointer = new System.Windows.Forms.TextBox();
            this.label73 = new System.Windows.Forms.Label();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemOpenROM = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReloadROM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemSaveROM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSaveROMAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTilesetEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemConnectionEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemWorldMapEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.espanolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.françaisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deutschToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemScriptEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.backupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createOnOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameFormatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hexPrefixToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemReadme = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBlockEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripConnectionEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripWorldMapEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripPluginManager = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslLoadingStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitMapListAndPaint)).BeginInit();
            this.splitMapListAndPaint.Panel1.SuspendLayout();
            this.splitMapListAndPaint.Panel2.SuspendLayout();
            this.splitMapListAndPaint.SuspendLayout();
            this.tsMapListTree.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.mapTabPage.SuspendLayout();
            this.panel2.SuspendLayout();
            this.borderBlocksBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.mapEditorPanel.SuspendLayout();
            this.panel8.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.paintTabControl.SuspendLayout();
            this.blocksTabPage.SuspendLayout();
            this.blockPaintPanel.SuspendLayout();
            this.movementTabPage.SuspendLayout();
            this.eventsTabPage.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown10)).BeginInit();
            this.panelSpriteEvent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            this.panelSignEvent.SuspendLayout();
            this.panelScriptEvent.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panelWarpEvent.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.toolStrip3.SuspendLayout();
            this.wildTabPage.SuspendLayout();
            this.panel6.SuspendLayout();
            this.grbGrassEncounters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.grbFishingRodEncounters.SuspendLayout();
            this.groupBox16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown75)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown74)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown73)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown72)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown71)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown70)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown66)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown69)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown67)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown68)).BeginInit();
            this.groupBox15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox35)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown81)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown80)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown79)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox34)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown78)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown77)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown76)).BeginInit();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox37)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown85)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown84)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown83)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown82)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox36)).BeginInit();
            this.groupBox13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).BeginInit();
            this.grbOtherEncounters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown34)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown35)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown46)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown47)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown48)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown49)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown50)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown51)).BeginInit();
            this.groupBox12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            this.grbWaterEncounters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown56)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown57)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown58)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown59)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown60)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown61)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown62)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown63)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown64)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown65)).BeginInit();
            this.groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).BeginInit();
            this.headerTabPage.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbHeaderTabMapHeader.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.gbHeaderTabRawMapHeader.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.gbHeaderTabLayoutHeader.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.gbHeaderTabRawLayoutHeader.SuspendLayout();
            this.panel7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitMapListAndPaint
            // 
            resources.ApplyResources(this.splitMapListAndPaint, "splitMapListAndPaint");
            this.splitMapListAndPaint.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitMapListAndPaint.Name = "splitMapListAndPaint";
            // 
            // splitMapListAndPaint.Panel1
            // 
            this.splitMapListAndPaint.Panel1.Controls.Add(this.tsMapListTree);
            this.splitMapListAndPaint.Panel1.Controls.Add(this.mapListTreeView);
            resources.ApplyResources(this.splitMapListAndPaint.Panel1, "splitMapListAndPaint.Panel1");
            // 
            // splitMapListAndPaint.Panel2
            // 
            this.splitMapListAndPaint.Panel2.Controls.Add(this.mainTabControl);
            // 
            // tsMapListTree
            // 
            resources.ApplyResources(this.tsMapListTree, "tsMapListTree");
            this.tsMapListTree.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMapListTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddbMapSortOrder});
            this.tsMapListTree.Name = "tsMapListTree";
            // 
            // tsddbMapSortOrder
            // 
            this.tsddbMapSortOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsddbMapSortOrder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapNameToolStripMenuItem,
            this.mapBankToolStripMenuItem,
            this.mapLayoutToolStripMenuItem,
            this.mapTilesetToolStripMenuItem});
            resources.ApplyResources(this.tsddbMapSortOrder, "tsddbMapSortOrder");
            this.tsddbMapSortOrder.Name = "tsddbMapSortOrder";
            this.tsddbMapSortOrder.Click += new System.EventHandler(this.tsddbMapSortOrder_Click);
            // 
            // mapNameToolStripMenuItem
            // 
            this.mapNameToolStripMenuItem.CheckOnClick = true;
            resources.ApplyResources(this.mapNameToolStripMenuItem, "mapNameToolStripMenuItem");
            this.mapNameToolStripMenuItem.Name = "mapNameToolStripMenuItem";
            this.mapNameToolStripMenuItem.Click += new System.EventHandler(this.mapNameToolStripMenuItem_Click);
            // 
            // mapBankToolStripMenuItem
            // 
            this.mapBankToolStripMenuItem.CheckOnClick = true;
            resources.ApplyResources(this.mapBankToolStripMenuItem, "mapBankToolStripMenuItem");
            this.mapBankToolStripMenuItem.Name = "mapBankToolStripMenuItem";
            this.mapBankToolStripMenuItem.Click += new System.EventHandler(this.mapBankToolStripMenuItem_Click);
            // 
            // mapLayoutToolStripMenuItem
            // 
            this.mapLayoutToolStripMenuItem.Image = global::PGMEWindowsUI.Properties.Resources.sort_map_16x16;
            this.mapLayoutToolStripMenuItem.Name = "mapLayoutToolStripMenuItem";
            resources.ApplyResources(this.mapLayoutToolStripMenuItem, "mapLayoutToolStripMenuItem");
            this.mapLayoutToolStripMenuItem.Click += new System.EventHandler(this.mapLayoutToolStripMenuItem_Click);
            // 
            // mapTilesetToolStripMenuItem
            // 
            this.mapTilesetToolStripMenuItem.CheckOnClick = true;
            resources.ApplyResources(this.mapTilesetToolStripMenuItem, "mapTilesetToolStripMenuItem");
            this.mapTilesetToolStripMenuItem.Name = "mapTilesetToolStripMenuItem";
            this.mapTilesetToolStripMenuItem.Click += new System.EventHandler(this.mapTilesetToolStripMenuItem_Click);
            // 
            // mapListTreeView
            // 
            resources.ApplyResources(this.mapListTreeView, "mapListTreeView");
            this.mapListTreeView.Name = "mapListTreeView";
            this.mapListTreeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.mapListTreeView_BeforeCollapse);
            this.mapListTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.mapListTreeView_BeforeExpand);
            this.mapListTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.mapListTreeView_NodeMouseDoubleClick);
            this.mapListTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mapListTreeView_KeyDown);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.mapTabPage);
            this.mainTabControl.Controls.Add(this.eventsTabPage);
            this.mainTabControl.Controls.Add(this.wildTabPage);
            this.mainTabControl.Controls.Add(this.headerTabPage);
            resources.ApplyResources(this.mainTabControl, "mainTabControl");
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            // 
            // mapTabPage
            // 
            this.mapTabPage.Controls.Add(this.panel2);
            resources.ApplyResources(this.mapTabPage, "mapTabPage");
            this.mapTabPage.Name = "mapTabPage";
            this.mapTabPage.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.borderBlocksBox);
            this.panel2.Controls.Add(this.mapEditorPanel);
            this.panel2.Controls.Add(this.paintTabControl);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // borderBlocksBox
            // 
            resources.ApplyResources(this.borderBlocksBox, "borderBlocksBox");
            this.borderBlocksBox.Controls.Add(this.pictureBox3);
            this.borderBlocksBox.Name = "borderBlocksBox";
            this.borderBlocksBox.TabStop = false;
            // 
            // pictureBox3
            // 
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // mapEditorPanel
            // 
            resources.ApplyResources(this.mapEditorPanel, "mapEditorPanel");
            this.mapEditorPanel.Controls.Add(this.panel8);
            this.mapEditorPanel.Controls.Add(this.toolStrip1);
            this.mapEditorPanel.Name = "mapEditorPanel";
            // 
            // panel8
            // 
            resources.ApplyResources(this.panel8, "panel8");
            this.panel8.BackColor = System.Drawing.Color.Transparent;
            this.panel8.Controls.Add(this.glControlMapEditor);
            this.panel8.Name = "panel8";
            this.panel8.Scroll += new System.Windows.Forms.ScrollEventHandler(this.panel8_Scroll);
            // 
            // glControlMapEditor
            // 
            this.glControlMapEditor.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.glControlMapEditor.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.glControlMapEditor, "glControlMapEditor");
            this.glControlMapEditor.Name = "glControlMapEditor";
            this.glControlMapEditor.VSync = false;
            this.glControlMapEditor.Load += new System.EventHandler(this.glControlMapEditor_Load);
            this.glControlMapEditor.Paint += new System.Windows.Forms.PaintEventHandler(this.glControlMapEditor_Paint);
            this.glControlMapEditor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControlMapEditor_MouseDown);
            this.glControlMapEditor.MouseEnter += new System.EventHandler(this.glControlMapEditor_MouseEnter);
            this.glControlMapEditor.MouseLeave += new System.EventHandler(this.glControlMapEditor_MouseLeave);
            this.glControlMapEditor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControlMapEditor_MouseMove);
            this.glControlMapEditor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControlMapEditor_MouseUp);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboTimeofDayMap,
            this.toolStripButton5,
            this.toolStripButton6,
            this.toolStripButton10,
            this.toolStripButton11,
            this.toolStripButton12,
            this.toolStripButton13,
            this.toolStripButton14,
            this.toolStripButton15});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // cboTimeofDayMap
            // 
            this.cboTimeofDayMap.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cboTimeofDayMap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeofDayMap.Items.AddRange(new object[] {
            resources.GetString("cboTimeofDayMap.Items"),
            resources.GetString("cboTimeofDayMap.Items1"),
            resources.GetString("cboTimeofDayMap.Items2"),
            resources.GetString("cboTimeofDayMap.Items3"),
            resources.GetString("cboTimeofDayMap.Items4")});
            this.cboTimeofDayMap.Name = "cboTimeofDayMap";
            resources.ApplyResources(this.cboTimeofDayMap, "cboTimeofDayMap");
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton5, "toolStripButton5");
            this.toolStripButton5.Name = "toolStripButton5";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton6, "toolStripButton6");
            this.toolStripButton6.Name = "toolStripButton6";
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton10, "toolStripButton10");
            this.toolStripButton10.Name = "toolStripButton10";
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton11, "toolStripButton11");
            this.toolStripButton11.Name = "toolStripButton11";
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton12, "toolStripButton12");
            this.toolStripButton12.Name = "toolStripButton12";
            // 
            // toolStripButton13
            // 
            this.toolStripButton13.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton13, "toolStripButton13");
            this.toolStripButton13.Name = "toolStripButton13";
            // 
            // toolStripButton14
            // 
            this.toolStripButton14.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton14, "toolStripButton14");
            this.toolStripButton14.Name = "toolStripButton14";
            // 
            // toolStripButton15
            // 
            this.toolStripButton15.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton15.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton15, "toolStripButton15");
            this.toolStripButton15.Name = "toolStripButton15";
            // 
            // paintTabControl
            // 
            resources.ApplyResources(this.paintTabControl, "paintTabControl");
            this.paintTabControl.Controls.Add(this.blocksTabPage);
            this.paintTabControl.Controls.Add(this.movementTabPage);
            this.paintTabControl.Name = "paintTabControl";
            this.paintTabControl.SelectedIndex = 0;
            // 
            // blocksTabPage
            // 
            this.blocksTabPage.Controls.Add(this.blockPaintPanel);
            resources.ApplyResources(this.blocksTabPage, "blocksTabPage");
            this.blocksTabPage.Name = "blocksTabPage";
            this.blocksTabPage.UseVisualStyleBackColor = true;
            // 
            // blockPaintPanel
            // 
            resources.ApplyResources(this.blockPaintPanel, "blockPaintPanel");
            this.blockPaintPanel.BackColor = System.Drawing.Color.Transparent;
            this.blockPaintPanel.Controls.Add(this.glControlBlocks);
            this.blockPaintPanel.Name = "blockPaintPanel";
            // 
            // glControlBlocks
            // 
            this.glControlBlocks.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.glControlBlocks, "glControlBlocks");
            this.glControlBlocks.Name = "glControlBlocks";
            this.glControlBlocks.VSync = false;
            this.glControlBlocks.Load += new System.EventHandler(this.glControlBlocks_Load);
            this.glControlBlocks.Paint += new System.Windows.Forms.PaintEventHandler(this.glControlBlocks_Paint);
            this.glControlBlocks.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glControlBlocks_MouseDown);
            this.glControlBlocks.MouseEnter += new System.EventHandler(this.glControlBlocks_MouseEnter);
            this.glControlBlocks.MouseLeave += new System.EventHandler(this.glControlBlocks_MouseLeave);
            this.glControlBlocks.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControlBlocks_MouseMove);
            this.glControlBlocks.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glControlBlocks_MouseUp);
            // 
            // movementTabPage
            // 
            this.movementTabPage.Controls.Add(this.movementPaintPanel);
            resources.ApplyResources(this.movementTabPage, "movementTabPage");
            this.movementTabPage.Name = "movementTabPage";
            this.movementTabPage.UseVisualStyleBackColor = true;
            // 
            // movementPaintPanel
            // 
            resources.ApplyResources(this.movementPaintPanel, "movementPaintPanel");
            this.movementPaintPanel.Name = "movementPaintPanel";
            // 
            // eventsTabPage
            // 
            this.eventsTabPage.Controls.Add(this.panel3);
            this.eventsTabPage.Controls.Add(this.panel1);
            resources.ApplyResources(this.eventsTabPage, "eventsTabPage");
            this.eventsTabPage.Name = "eventsTabPage";
            this.eventsTabPage.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Controls.Add(this.cboEventTypes);
            this.panel3.Controls.Add(this.numericUpDown10);
            this.panel3.Controls.Add(this.panelSpriteEvent);
            this.panel3.Controls.Add(this.panelSignEvent);
            this.panel3.Controls.Add(this.panelScriptEvent);
            this.panel3.Controls.Add(this.panelWarpEvent);
            this.panel3.Name = "panel3";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.numericUpDown4);
            this.groupBox1.Controls.Add(this.numericUpDown3);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // button3
            // 
            resources.ApplyResources(this.button3, "button3");
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // numericUpDown4
            // 
            resources.ApplyResources(this.numericUpDown4, "numericUpDown4");
            this.numericUpDown4.Name = "numericUpDown4";
            // 
            // numericUpDown3
            // 
            resources.ApplyResources(this.numericUpDown3, "numericUpDown3");
            this.numericUpDown3.Name = "numericUpDown3";
            // 
            // numericUpDown2
            // 
            resources.ApplyResources(this.numericUpDown2, "numericUpDown2");
            this.numericUpDown2.Name = "numericUpDown2";
            // 
            // numericUpDown1
            // 
            resources.ApplyResources(this.numericUpDown1, "numericUpDown1");
            this.numericUpDown1.Name = "numericUpDown1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cboEventTypes
            // 
            this.cboEventTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEventTypes.FormattingEnabled = true;
            this.cboEventTypes.Items.AddRange(new object[] {
            resources.GetString("cboEventTypes.Items"),
            resources.GetString("cboEventTypes.Items1"),
            resources.GetString("cboEventTypes.Items2"),
            resources.GetString("cboEventTypes.Items3")});
            resources.ApplyResources(this.cboEventTypes, "cboEventTypes");
            this.cboEventTypes.Name = "cboEventTypes";
            this.cboEventTypes.SelectedIndexChanged += new System.EventHandler(this.comboBox6_SelectedIndexChanged);
            // 
            // numericUpDown10
            // 
            resources.ApplyResources(this.numericUpDown10, "numericUpDown10");
            this.numericUpDown10.Name = "numericUpDown10";
            // 
            // panelSpriteEvent
            // 
            resources.ApplyResources(this.panelSpriteEvent, "panelSpriteEvent");
            this.panelSpriteEvent.Controls.Add(this.textBox19);
            this.panelSpriteEvent.Controls.Add(this.label26);
            this.panelSpriteEvent.Controls.Add(this.textBox11);
            this.panelSpriteEvent.Controls.Add(this.label18);
            this.panelSpriteEvent.Controls.Add(this.label17);
            this.panelSpriteEvent.Controls.Add(this.textBox10);
            this.panelSpriteEvent.Controls.Add(this.button2);
            this.panelSpriteEvent.Controls.Add(this.button1);
            this.panelSpriteEvent.Controls.Add(this.textBox9);
            this.panelSpriteEvent.Controls.Add(this.label16);
            this.panelSpriteEvent.Controls.Add(this.textBox8);
            this.panelSpriteEvent.Controls.Add(this.label15);
            this.panelSpriteEvent.Controls.Add(this.textBox2);
            this.panelSpriteEvent.Controls.Add(this.label8);
            this.panelSpriteEvent.Controls.Add(this.textBox7);
            this.panelSpriteEvent.Controls.Add(this.label14);
            this.panelSpriteEvent.Controls.Add(this.textBox6);
            this.panelSpriteEvent.Controls.Add(this.label13);
            this.panelSpriteEvent.Controls.Add(this.textBox5);
            this.panelSpriteEvent.Controls.Add(this.label12);
            this.panelSpriteEvent.Controls.Add(this.label11);
            this.panelSpriteEvent.Controls.Add(this.comboBox3);
            this.panelSpriteEvent.Controls.Add(this.comboBox2);
            this.panelSpriteEvent.Controls.Add(this.label10);
            this.panelSpriteEvent.Controls.Add(this.label9);
            this.panelSpriteEvent.Controls.Add(this.textBox4);
            this.panelSpriteEvent.Controls.Add(this.label7);
            this.panelSpriteEvent.Controls.Add(this.label6);
            this.panelSpriteEvent.Controls.Add(this.label5);
            this.panelSpriteEvent.Controls.Add(this.textBox3);
            this.panelSpriteEvent.Controls.Add(this.textBox1);
            this.panelSpriteEvent.Controls.Add(this.numericUpDown7);
            this.panelSpriteEvent.Controls.Add(this.numericUpDown6);
            this.panelSpriteEvent.Name = "panelSpriteEvent";
            // 
            // textBox19
            // 
            resources.ApplyResources(this.textBox19, "textBox19");
            this.textBox19.Name = "textBox19";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // textBox11
            // 
            resources.ApplyResources(this.textBox11, "textBox11");
            this.textBox11.Name = "textBox11";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // textBox10
            // 
            resources.ApplyResources(this.textBox10, "textBox10");
            this.textBox10.Name = "textBox10";
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox9
            // 
            resources.ApplyResources(this.textBox9, "textBox9");
            this.textBox9.Name = "textBox9";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // textBox8
            // 
            resources.ApplyResources(this.textBox8, "textBox8");
            this.textBox8.Name = "textBox8";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // textBox7
            // 
            resources.ApplyResources(this.textBox7, "textBox7");
            this.textBox7.Name = "textBox7";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // textBox6
            // 
            resources.ApplyResources(this.textBox6, "textBox6");
            this.textBox6.Name = "textBox6";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // textBox5
            // 
            resources.ApplyResources(this.textBox5, "textBox5");
            this.textBox5.Name = "textBox5";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox3, "comboBox3");
            this.comboBox3.Name = "comboBox3";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox2, "comboBox2");
            this.comboBox2.Name = "comboBox2";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // textBox4
            // 
            resources.ApplyResources(this.textBox4, "textBox4");
            this.textBox4.Name = "textBox4";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBox3
            // 
            resources.ApplyResources(this.textBox3, "textBox3");
            this.textBox3.Name = "textBox3";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // numericUpDown7
            // 
            resources.ApplyResources(this.numericUpDown7, "numericUpDown7");
            this.numericUpDown7.Name = "numericUpDown7";
            // 
            // numericUpDown6
            // 
            resources.ApplyResources(this.numericUpDown6, "numericUpDown6");
            this.numericUpDown6.Name = "numericUpDown6";
            // 
            // panelSignEvent
            // 
            resources.ApplyResources(this.panelSignEvent, "panelSignEvent");
            this.panelSignEvent.Controls.Add(this.button8);
            this.panelSignEvent.Controls.Add(this.button9);
            this.panelSignEvent.Controls.Add(this.textBox18);
            this.panelSignEvent.Controls.Add(this.label23);
            this.panelSignEvent.Controls.Add(this.textBox26);
            this.panelSignEvent.Controls.Add(this.label33);
            this.panelSignEvent.Controls.Add(this.label34);
            this.panelSignEvent.Controls.Add(this.comboBox4);
            this.panelSignEvent.Controls.Add(this.comboBox9);
            this.panelSignEvent.Controls.Add(this.label35);
            this.panelSignEvent.Controls.Add(this.label36);
            this.panelSignEvent.Controls.Add(this.textBox27);
            this.panelSignEvent.Controls.Add(this.textBox28);
            this.panelSignEvent.Name = "panelSignEvent";
            // 
            // button8
            // 
            this.button8.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.button8, "button8");
            this.button8.Name = "button8";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            resources.ApplyResources(this.button9, "button9");
            this.button9.Name = "button9";
            this.button9.UseVisualStyleBackColor = true;
            // 
            // textBox18
            // 
            resources.ApplyResources(this.textBox18, "textBox18");
            this.textBox18.Name = "textBox18";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // textBox26
            // 
            resources.ApplyResources(this.textBox26, "textBox26");
            this.textBox26.Name = "textBox26";
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.Name = "label33";
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.Name = "label34";
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox4, "comboBox4");
            this.comboBox4.Name = "comboBox4";
            // 
            // comboBox9
            // 
            this.comboBox9.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox9, "comboBox9");
            this.comboBox9.Name = "comboBox9";
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.Name = "label35";
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.Name = "label36";
            // 
            // textBox27
            // 
            resources.ApplyResources(this.textBox27, "textBox27");
            this.textBox27.Name = "textBox27";
            // 
            // textBox28
            // 
            resources.ApplyResources(this.textBox28, "textBox28");
            this.textBox28.Name = "textBox28";
            // 
            // panelScriptEvent
            // 
            resources.ApplyResources(this.panelScriptEvent, "panelScriptEvent");
            this.panelScriptEvent.Controls.Add(this.label37);
            this.panelScriptEvent.Controls.Add(this.textBox29);
            this.panelScriptEvent.Controls.Add(this.label27);
            this.panelScriptEvent.Controls.Add(this.textBox20);
            this.panelScriptEvent.Controls.Add(this.label32);
            this.panelScriptEvent.Controls.Add(this.groupBox3);
            this.panelScriptEvent.Controls.Add(this.label19);
            this.panelScriptEvent.Controls.Add(this.button6);
            this.panelScriptEvent.Controls.Add(this.textBox12);
            this.panelScriptEvent.Controls.Add(this.button7);
            this.panelScriptEvent.Controls.Add(this.textBox23);
            this.panelScriptEvent.Controls.Add(this.comboBox7);
            this.panelScriptEvent.Controls.Add(this.label29);
            this.panelScriptEvent.Controls.Add(this.label30);
            this.panelScriptEvent.Controls.Add(this.textBox21);
            this.panelScriptEvent.Controls.Add(this.textBox22);
            this.panelScriptEvent.Name = "panelScriptEvent";
            // 
            // label37
            // 
            resources.ApplyResources(this.label37, "label37");
            this.label37.Name = "label37";
            // 
            // textBox29
            // 
            resources.ApplyResources(this.textBox29, "textBox29");
            this.textBox29.Name = "textBox29";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // textBox20
            // 
            resources.ApplyResources(this.textBox20, "textBox20");
            this.textBox20.Name = "textBox20";
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.Name = "label32";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label28);
            this.groupBox3.Controls.Add(this.label31);
            this.groupBox3.Controls.Add(this.textBox24);
            this.groupBox3.Controls.Add(this.textBox25);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // textBox24
            // 
            resources.ApplyResources(this.textBox24, "textBox24");
            this.textBox24.Name = "textBox24";
            // 
            // textBox25
            // 
            resources.ApplyResources(this.textBox25, "textBox25");
            this.textBox25.Name = "textBox25";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // button6
            // 
            this.button6.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.button6, "button6");
            this.button6.Name = "button6";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // textBox12
            // 
            resources.ApplyResources(this.textBox12, "textBox12");
            this.textBox12.Name = "textBox12";
            // 
            // button7
            // 
            resources.ApplyResources(this.button7, "button7");
            this.button7.Name = "button7";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // textBox23
            // 
            resources.ApplyResources(this.textBox23, "textBox23");
            this.textBox23.Name = "textBox23";
            // 
            // comboBox7
            // 
            this.comboBox7.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox7, "comboBox7");
            this.comboBox7.Name = "comboBox7";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // textBox21
            // 
            resources.ApplyResources(this.textBox21, "textBox21");
            this.textBox21.Name = "textBox21";
            // 
            // textBox22
            // 
            resources.ApplyResources(this.textBox22, "textBox22");
            this.textBox22.Name = "textBox22";
            // 
            // panelWarpEvent
            // 
            resources.ApplyResources(this.panelWarpEvent, "panelWarpEvent");
            this.panelWarpEvent.Controls.Add(this.groupBox2);
            this.panelWarpEvent.Controls.Add(this.button4);
            this.panelWarpEvent.Controls.Add(this.button5);
            this.panelWarpEvent.Controls.Add(this.comboBox5);
            this.panelWarpEvent.Controls.Add(this.label24);
            this.panelWarpEvent.Controls.Add(this.label25);
            this.panelWarpEvent.Controls.Add(this.textBox16);
            this.panelWarpEvent.Controls.Add(this.textBox17);
            this.panelWarpEvent.Name = "panelWarpEvent";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.textBox13);
            this.groupBox2.Controls.Add(this.textBox15);
            this.groupBox2.Controls.Add(this.textBox14);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // textBox13
            // 
            resources.ApplyResources(this.textBox13, "textBox13");
            this.textBox13.Name = "textBox13";
            // 
            // textBox15
            // 
            resources.ApplyResources(this.textBox15, "textBox15");
            this.textBox15.Name = "textBox15";
            // 
            // textBox14
            // 
            resources.ApplyResources(this.textBox14, "textBox14");
            this.textBox14.Name = "textBox14";
            // 
            // button4
            // 
            this.button4.ForeColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.button4, "button4");
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            resources.ApplyResources(this.button5, "button5");
            this.button5.Name = "button5";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox5, "comboBox5");
            this.comboBox5.Name = "comboBox5";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // textBox16
            // 
            resources.ApplyResources(this.textBox16, "textBox16");
            this.textBox16.Name = "textBox16";
            // 
            // textBox17
            // 
            resources.ApplyResources(this.textBox17, "textBox17");
            this.textBox17.Name = "textBox17";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.toolStrip3);
            this.panel1.Name = "panel1";
            // 
            // panel4
            // 
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Controls.Add(this.pictureBox2);
            this.panel4.Name = "panel4";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // toolStrip3
            // 
            this.toolStrip3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboTimeofDayEvents,
            this.toolStripButton16,
            this.toolStripButton17,
            this.toolStripDropDownButton1});
            resources.ApplyResources(this.toolStrip3, "toolStrip3");
            this.toolStrip3.Name = "toolStrip3";
            // 
            // cboTimeofDayEvents
            // 
            this.cboTimeofDayEvents.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cboTimeofDayEvents.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeofDayEvents.Items.AddRange(new object[] {
            resources.GetString("cboTimeofDayEvents.Items"),
            resources.GetString("cboTimeofDayEvents.Items1"),
            resources.GetString("cboTimeofDayEvents.Items2"),
            resources.GetString("cboTimeofDayEvents.Items3"),
            resources.GetString("cboTimeofDayEvents.Items4")});
            this.cboTimeofDayEvents.Name = "cboTimeofDayEvents";
            resources.ApplyResources(this.cboTimeofDayEvents, "cboTimeofDayEvents");
            this.cboTimeofDayEvents.Click += new System.EventHandler(this.groupBox2_Enter);
            // 
            // toolStripButton16
            // 
            this.toolStripButton16.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton16, "toolStripButton16");
            this.toolStripButton16.Name = "toolStripButton16";
            // 
            // toolStripButton17
            // 
            this.toolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton17, "toolStripButton17");
            this.toolStripButton17.Name = "toolStripButton17";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.Image = global::PGMEWindowsUI.Properties.Resources.viewsprites_16x16;
            resources.ApplyResources(this.toolStripDropDownButton1, "toolStripDropDownButton1");
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            // 
            // wildTabPage
            // 
            this.wildTabPage.Controls.Add(this.panel6);
            resources.ApplyResources(this.wildTabPage, "wildTabPage");
            this.wildTabPage.Name = "wildTabPage";
            this.wildTabPage.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Controls.Add(this.button10);
            this.panel6.Controls.Add(this.cboEncounterTypes);
            this.panel6.Controls.Add(this.label38);
            this.panel6.Controls.Add(this.grbGrassEncounters);
            this.panel6.Controls.Add(this.grbFishingRodEncounters);
            this.panel6.Controls.Add(this.grbOtherEncounters);
            this.panel6.Controls.Add(this.grbWaterEncounters);
            this.panel6.Name = "panel6";
            // 
            // button10
            // 
            resources.ApplyResources(this.button10, "button10");
            this.button10.Name = "button10";
            this.button10.UseVisualStyleBackColor = true;
            // 
            // cboEncounterTypes
            // 
            this.cboEncounterTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEncounterTypes.FormattingEnabled = true;
            this.cboEncounterTypes.Items.AddRange(new object[] {
            resources.GetString("cboEncounterTypes.Items"),
            resources.GetString("cboEncounterTypes.Items1"),
            resources.GetString("cboEncounterTypes.Items2"),
            resources.GetString("cboEncounterTypes.Items3")});
            resources.ApplyResources(this.cboEncounterTypes, "cboEncounterTypes");
            this.cboEncounterTypes.Name = "cboEncounterTypes";
            this.cboEncounterTypes.SelectedIndexChanged += new System.EventHandler(this.cboEncounterType_SelectedIndexChanged);
            // 
            // label38
            // 
            resources.ApplyResources(this.label38, "label38");
            this.label38.Name = "label38";
            // 
            // grbGrassEncounters
            // 
            this.grbGrassEncounters.Controls.Add(this.pictureBox12);
            this.grbGrassEncounters.Controls.Add(this.pictureBox13);
            this.grbGrassEncounters.Controls.Add(this.pictureBox14);
            this.grbGrassEncounters.Controls.Add(this.pictureBox15);
            this.grbGrassEncounters.Controls.Add(this.pictureBox8);
            this.grbGrassEncounters.Controls.Add(this.pictureBox9);
            this.grbGrassEncounters.Controls.Add(this.pictureBox10);
            this.grbGrassEncounters.Controls.Add(this.pictureBox11);
            this.grbGrassEncounters.Controls.Add(this.pictureBox7);
            this.grbGrassEncounters.Controls.Add(this.pictureBox6);
            this.grbGrassEncounters.Controls.Add(this.pictureBox5);
            this.grbGrassEncounters.Controls.Add(this.pictureBox4);
            this.grbGrassEncounters.Controls.Add(this.label51);
            this.grbGrassEncounters.Controls.Add(this.label52);
            this.grbGrassEncounters.Controls.Add(this.label53);
            this.grbGrassEncounters.Controls.Add(this.label54);
            this.grbGrassEncounters.Controls.Add(this.label55);
            this.grbGrassEncounters.Controls.Add(this.label56);
            this.grbGrassEncounters.Controls.Add(this.label49);
            this.grbGrassEncounters.Controls.Add(this.label50);
            this.grbGrassEncounters.Controls.Add(this.label48);
            this.grbGrassEncounters.Controls.Add(this.label47);
            this.grbGrassEncounters.Controls.Add(this.label46);
            this.grbGrassEncounters.Controls.Add(this.label45);
            this.grbGrassEncounters.Controls.Add(this.label44);
            this.grbGrassEncounters.Controls.Add(this.comboBox16);
            this.grbGrassEncounters.Controls.Add(this.textBox39);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown24);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown25);
            this.grbGrassEncounters.Controls.Add(this.comboBox17);
            this.grbGrassEncounters.Controls.Add(this.textBox40);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown26);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown27);
            this.grbGrassEncounters.Controls.Add(this.comboBox18);
            this.grbGrassEncounters.Controls.Add(this.textBox41);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown28);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown29);
            this.grbGrassEncounters.Controls.Add(this.comboBox19);
            this.grbGrassEncounters.Controls.Add(this.textBox42);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown30);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown31);
            this.grbGrassEncounters.Controls.Add(this.comboBox12);
            this.grbGrassEncounters.Controls.Add(this.textBox35);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown16);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown17);
            this.grbGrassEncounters.Controls.Add(this.comboBox13);
            this.grbGrassEncounters.Controls.Add(this.textBox36);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown18);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown19);
            this.grbGrassEncounters.Controls.Add(this.comboBox14);
            this.grbGrassEncounters.Controls.Add(this.textBox37);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown20);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown21);
            this.grbGrassEncounters.Controls.Add(this.comboBox15);
            this.grbGrassEncounters.Controls.Add(this.textBox38);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown22);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown23);
            this.grbGrassEncounters.Controls.Add(this.comboBox11);
            this.grbGrassEncounters.Controls.Add(this.textBox34);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown14);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown15);
            this.grbGrassEncounters.Controls.Add(this.comboBox10);
            this.grbGrassEncounters.Controls.Add(this.textBox33);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown12);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown13);
            this.grbGrassEncounters.Controls.Add(this.comboBox8);
            this.grbGrassEncounters.Controls.Add(this.textBox32);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown9);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown11);
            this.grbGrassEncounters.Controls.Add(this.label43);
            this.grbGrassEncounters.Controls.Add(this.label42);
            this.grbGrassEncounters.Controls.Add(this.label41);
            this.grbGrassEncounters.Controls.Add(this.label40);
            this.grbGrassEncounters.Controls.Add(this.comboBox6);
            this.grbGrassEncounters.Controls.Add(this.textBox31);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown5);
            this.grbGrassEncounters.Controls.Add(this.numericUpDown8);
            this.grbGrassEncounters.Controls.Add(this.groupBox4);
            resources.ApplyResources(this.grbGrassEncounters, "grbGrassEncounters");
            this.grbGrassEncounters.Name = "grbGrassEncounters";
            this.grbGrassEncounters.TabStop = false;
            // 
            // pictureBox12
            // 
            this.pictureBox12.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox12.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox12, "pictureBox12");
            this.pictureBox12.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.TabStop = false;
            // 
            // pictureBox13
            // 
            this.pictureBox13.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox13.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox13, "pictureBox13");
            this.pictureBox13.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.TabStop = false;
            // 
            // pictureBox14
            // 
            this.pictureBox14.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox14.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox14, "pictureBox14");
            this.pictureBox14.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox14.Name = "pictureBox14";
            this.pictureBox14.TabStop = false;
            // 
            // pictureBox15
            // 
            this.pictureBox15.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox15.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox15, "pictureBox15");
            this.pictureBox15.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox8.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox8, "pictureBox8");
            this.pictureBox8.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox9
            // 
            this.pictureBox9.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox9.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox9, "pictureBox9");
            this.pictureBox9.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.TabStop = false;
            // 
            // pictureBox10
            // 
            this.pictureBox10.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox10.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox10, "pictureBox10");
            this.pictureBox10.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.TabStop = false;
            // 
            // pictureBox11
            // 
            this.pictureBox11.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox11.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox11, "pictureBox11");
            this.pictureBox11.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox7.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox7, "pictureBox7");
            this.pictureBox7.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox6.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox6, "pictureBox6");
            this.pictureBox6.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox5.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox5, "pictureBox5");
            this.pictureBox5.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox4.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox4, "pictureBox4");
            this.pictureBox4.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.TabStop = false;
            // 
            // label51
            // 
            resources.ApplyResources(this.label51, "label51");
            this.label51.Name = "label51";
            // 
            // label52
            // 
            resources.ApplyResources(this.label52, "label52");
            this.label52.Name = "label52";
            // 
            // label53
            // 
            resources.ApplyResources(this.label53, "label53");
            this.label53.Name = "label53";
            // 
            // label54
            // 
            resources.ApplyResources(this.label54, "label54");
            this.label54.Name = "label54";
            // 
            // label55
            // 
            resources.ApplyResources(this.label55, "label55");
            this.label55.Name = "label55";
            this.label55.Click += new System.EventHandler(this.label55_Click);
            // 
            // label56
            // 
            resources.ApplyResources(this.label56, "label56");
            this.label56.Name = "label56";
            // 
            // label49
            // 
            resources.ApplyResources(this.label49, "label49");
            this.label49.Name = "label49";
            // 
            // label50
            // 
            resources.ApplyResources(this.label50, "label50");
            this.label50.Name = "label50";
            // 
            // label48
            // 
            resources.ApplyResources(this.label48, "label48");
            this.label48.Name = "label48";
            // 
            // label47
            // 
            resources.ApplyResources(this.label47, "label47");
            this.label47.Name = "label47";
            // 
            // label46
            // 
            resources.ApplyResources(this.label46, "label46");
            this.label46.Name = "label46";
            // 
            // label45
            // 
            resources.ApplyResources(this.label45, "label45");
            this.label45.Name = "label45";
            // 
            // label44
            // 
            resources.ApplyResources(this.label44, "label44");
            this.label44.Name = "label44";
            // 
            // comboBox16
            // 
            this.comboBox16.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox16.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox16, "comboBox16");
            this.comboBox16.Name = "comboBox16";
            // 
            // textBox39
            // 
            resources.ApplyResources(this.textBox39, "textBox39");
            this.textBox39.Name = "textBox39";
            // 
            // numericUpDown24
            // 
            resources.ApplyResources(this.numericUpDown24, "numericUpDown24");
            this.numericUpDown24.Name = "numericUpDown24";
            this.numericUpDown24.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown25
            // 
            resources.ApplyResources(this.numericUpDown25, "numericUpDown25");
            this.numericUpDown25.Name = "numericUpDown25";
            this.numericUpDown25.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox17
            // 
            this.comboBox17.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox17.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox17, "comboBox17");
            this.comboBox17.Name = "comboBox17";
            // 
            // textBox40
            // 
            resources.ApplyResources(this.textBox40, "textBox40");
            this.textBox40.Name = "textBox40";
            // 
            // numericUpDown26
            // 
            resources.ApplyResources(this.numericUpDown26, "numericUpDown26");
            this.numericUpDown26.Name = "numericUpDown26";
            this.numericUpDown26.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown27
            // 
            resources.ApplyResources(this.numericUpDown27, "numericUpDown27");
            this.numericUpDown27.Name = "numericUpDown27";
            this.numericUpDown27.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox18
            // 
            this.comboBox18.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox18.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox18, "comboBox18");
            this.comboBox18.Name = "comboBox18";
            // 
            // textBox41
            // 
            resources.ApplyResources(this.textBox41, "textBox41");
            this.textBox41.Name = "textBox41";
            // 
            // numericUpDown28
            // 
            resources.ApplyResources(this.numericUpDown28, "numericUpDown28");
            this.numericUpDown28.Name = "numericUpDown28";
            this.numericUpDown28.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown29
            // 
            resources.ApplyResources(this.numericUpDown29, "numericUpDown29");
            this.numericUpDown29.Name = "numericUpDown29";
            this.numericUpDown29.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox19
            // 
            this.comboBox19.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox19.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox19, "comboBox19");
            this.comboBox19.Name = "comboBox19";
            // 
            // textBox42
            // 
            resources.ApplyResources(this.textBox42, "textBox42");
            this.textBox42.Name = "textBox42";
            // 
            // numericUpDown30
            // 
            resources.ApplyResources(this.numericUpDown30, "numericUpDown30");
            this.numericUpDown30.Name = "numericUpDown30";
            this.numericUpDown30.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown31
            // 
            resources.ApplyResources(this.numericUpDown31, "numericUpDown31");
            this.numericUpDown31.Name = "numericUpDown31";
            this.numericUpDown31.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox12
            // 
            this.comboBox12.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox12.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox12, "comboBox12");
            this.comboBox12.Name = "comboBox12";
            // 
            // textBox35
            // 
            resources.ApplyResources(this.textBox35, "textBox35");
            this.textBox35.Name = "textBox35";
            // 
            // numericUpDown16
            // 
            resources.ApplyResources(this.numericUpDown16, "numericUpDown16");
            this.numericUpDown16.Name = "numericUpDown16";
            this.numericUpDown16.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown17
            // 
            resources.ApplyResources(this.numericUpDown17, "numericUpDown17");
            this.numericUpDown17.Name = "numericUpDown17";
            this.numericUpDown17.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox13
            // 
            this.comboBox13.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox13.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox13, "comboBox13");
            this.comboBox13.Name = "comboBox13";
            // 
            // textBox36
            // 
            resources.ApplyResources(this.textBox36, "textBox36");
            this.textBox36.Name = "textBox36";
            // 
            // numericUpDown18
            // 
            resources.ApplyResources(this.numericUpDown18, "numericUpDown18");
            this.numericUpDown18.Name = "numericUpDown18";
            this.numericUpDown18.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown19
            // 
            resources.ApplyResources(this.numericUpDown19, "numericUpDown19");
            this.numericUpDown19.Name = "numericUpDown19";
            this.numericUpDown19.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox14
            // 
            this.comboBox14.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox14.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox14, "comboBox14");
            this.comboBox14.Name = "comboBox14";
            // 
            // textBox37
            // 
            resources.ApplyResources(this.textBox37, "textBox37");
            this.textBox37.Name = "textBox37";
            // 
            // numericUpDown20
            // 
            resources.ApplyResources(this.numericUpDown20, "numericUpDown20");
            this.numericUpDown20.Name = "numericUpDown20";
            this.numericUpDown20.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown21
            // 
            resources.ApplyResources(this.numericUpDown21, "numericUpDown21");
            this.numericUpDown21.Name = "numericUpDown21";
            this.numericUpDown21.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox15
            // 
            this.comboBox15.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox15.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox15, "comboBox15");
            this.comboBox15.Name = "comboBox15";
            // 
            // textBox38
            // 
            resources.ApplyResources(this.textBox38, "textBox38");
            this.textBox38.Name = "textBox38";
            // 
            // numericUpDown22
            // 
            resources.ApplyResources(this.numericUpDown22, "numericUpDown22");
            this.numericUpDown22.Name = "numericUpDown22";
            this.numericUpDown22.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown23
            // 
            resources.ApplyResources(this.numericUpDown23, "numericUpDown23");
            this.numericUpDown23.Name = "numericUpDown23";
            this.numericUpDown23.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox11
            // 
            this.comboBox11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox11.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox11, "comboBox11");
            this.comboBox11.Name = "comboBox11";
            // 
            // textBox34
            // 
            resources.ApplyResources(this.textBox34, "textBox34");
            this.textBox34.Name = "textBox34";
            // 
            // numericUpDown14
            // 
            resources.ApplyResources(this.numericUpDown14, "numericUpDown14");
            this.numericUpDown14.Name = "numericUpDown14";
            this.numericUpDown14.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown15
            // 
            resources.ApplyResources(this.numericUpDown15, "numericUpDown15");
            this.numericUpDown15.Name = "numericUpDown15";
            this.numericUpDown15.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox10
            // 
            this.comboBox10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox10.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox10, "comboBox10");
            this.comboBox10.Name = "comboBox10";
            // 
            // textBox33
            // 
            resources.ApplyResources(this.textBox33, "textBox33");
            this.textBox33.Name = "textBox33";
            // 
            // numericUpDown12
            // 
            resources.ApplyResources(this.numericUpDown12, "numericUpDown12");
            this.numericUpDown12.Name = "numericUpDown12";
            this.numericUpDown12.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown13
            // 
            resources.ApplyResources(this.numericUpDown13, "numericUpDown13");
            this.numericUpDown13.Name = "numericUpDown13";
            this.numericUpDown13.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox8
            // 
            this.comboBox8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox8.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox8, "comboBox8");
            this.comboBox8.Name = "comboBox8";
            // 
            // textBox32
            // 
            resources.ApplyResources(this.textBox32, "textBox32");
            this.textBox32.Name = "textBox32";
            // 
            // numericUpDown9
            // 
            resources.ApplyResources(this.numericUpDown9, "numericUpDown9");
            this.numericUpDown9.Name = "numericUpDown9";
            this.numericUpDown9.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown11
            // 
            resources.ApplyResources(this.numericUpDown11, "numericUpDown11");
            this.numericUpDown11.Name = "numericUpDown11";
            this.numericUpDown11.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label43
            // 
            resources.ApplyResources(this.label43, "label43");
            this.label43.Name = "label43";
            // 
            // label42
            // 
            resources.ApplyResources(this.label42, "label42");
            this.label42.Name = "label42";
            // 
            // label41
            // 
            resources.ApplyResources(this.label41, "label41");
            this.label41.Name = "label41";
            // 
            // label40
            // 
            resources.ApplyResources(this.label40, "label40");
            this.label40.Name = "label40";
            // 
            // comboBox6
            // 
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox6, "comboBox6");
            this.comboBox6.Name = "comboBox6";
            // 
            // textBox31
            // 
            resources.ApplyResources(this.textBox31, "textBox31");
            this.textBox31.Name = "textBox31";
            // 
            // numericUpDown5
            // 
            resources.ApplyResources(this.numericUpDown5, "numericUpDown5");
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown8
            // 
            resources.ApplyResources(this.numericUpDown8, "numericUpDown8");
            this.numericUpDown8.Name = "numericUpDown8";
            this.numericUpDown8.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label39);
            this.groupBox4.Controls.Add(this.textBox30);
            this.groupBox4.Controls.Add(this.trackBar1);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // label39
            // 
            resources.ApplyResources(this.label39, "label39");
            this.label39.Name = "label39";
            // 
            // textBox30
            // 
            resources.ApplyResources(this.textBox30, "textBox30");
            this.textBox30.Name = "textBox30";
            // 
            // trackBar1
            // 
            resources.ApplyResources(this.trackBar1, "trackBar1");
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.TickFrequency = 10;
            // 
            // grbFishingRodEncounters
            // 
            this.grbFishingRodEncounters.Controls.Add(this.groupBox16);
            this.grbFishingRodEncounters.Controls.Add(this.groupBox15);
            this.grbFishingRodEncounters.Controls.Add(this.groupBox14);
            this.grbFishingRodEncounters.Controls.Add(this.label115);
            this.grbFishingRodEncounters.Controls.Add(this.label117);
            this.grbFishingRodEncounters.Controls.Add(this.label118);
            this.grbFishingRodEncounters.Controls.Add(this.label119);
            this.grbFishingRodEncounters.Controls.Add(this.label120);
            this.grbFishingRodEncounters.Controls.Add(this.groupBox13);
            resources.ApplyResources(this.grbFishingRodEncounters, "grbFishingRodEncounters");
            this.grbFishingRodEncounters.Name = "grbFishingRodEncounters";
            this.grbFishingRodEncounters.TabStop = false;
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.pictureBox28);
            this.groupBox16.Controls.Add(this.pictureBox32);
            this.groupBox16.Controls.Add(this.pictureBox29);
            this.groupBox16.Controls.Add(this.numericUpDown75);
            this.groupBox16.Controls.Add(this.pictureBox30);
            this.groupBox16.Controls.Add(this.numericUpDown74);
            this.groupBox16.Controls.Add(this.pictureBox31);
            this.groupBox16.Controls.Add(this.textBox73);
            this.groupBox16.Controls.Add(this.comboBox46);
            this.groupBox16.Controls.Add(this.label75);
            this.groupBox16.Controls.Add(this.numericUpDown73);
            this.groupBox16.Controls.Add(this.label76);
            this.groupBox16.Controls.Add(this.numericUpDown72);
            this.groupBox16.Controls.Add(this.label95);
            this.groupBox16.Controls.Add(this.textBox69);
            this.groupBox16.Controls.Add(this.label96);
            this.groupBox16.Controls.Add(this.comboBox45);
            this.groupBox16.Controls.Add(this.label97);
            this.groupBox16.Controls.Add(this.numericUpDown71);
            this.groupBox16.Controls.Add(this.numericUpDown70);
            this.groupBox16.Controls.Add(this.comboBox42);
            this.groupBox16.Controls.Add(this.textBox68);
            this.groupBox16.Controls.Add(this.textBox66);
            this.groupBox16.Controls.Add(this.comboBox44);
            this.groupBox16.Controls.Add(this.numericUpDown66);
            this.groupBox16.Controls.Add(this.numericUpDown69);
            this.groupBox16.Controls.Add(this.numericUpDown67);
            this.groupBox16.Controls.Add(this.numericUpDown68);
            this.groupBox16.Controls.Add(this.comboBox43);
            this.groupBox16.Controls.Add(this.textBox67);
            resources.ApplyResources(this.groupBox16, "groupBox16");
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.TabStop = false;
            // 
            // pictureBox28
            // 
            this.pictureBox28.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox28.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox28, "pictureBox28");
            this.pictureBox28.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox28.Name = "pictureBox28";
            this.pictureBox28.TabStop = false;
            // 
            // pictureBox32
            // 
            this.pictureBox32.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox32.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox32, "pictureBox32");
            this.pictureBox32.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox32.Name = "pictureBox32";
            this.pictureBox32.TabStop = false;
            // 
            // pictureBox29
            // 
            this.pictureBox29.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox29.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox29, "pictureBox29");
            this.pictureBox29.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox29.Name = "pictureBox29";
            this.pictureBox29.TabStop = false;
            // 
            // numericUpDown75
            // 
            resources.ApplyResources(this.numericUpDown75, "numericUpDown75");
            this.numericUpDown75.Name = "numericUpDown75";
            this.numericUpDown75.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // pictureBox30
            // 
            this.pictureBox30.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox30.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox30, "pictureBox30");
            this.pictureBox30.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox30.Name = "pictureBox30";
            this.pictureBox30.TabStop = false;
            // 
            // numericUpDown74
            // 
            resources.ApplyResources(this.numericUpDown74, "numericUpDown74");
            this.numericUpDown74.Name = "numericUpDown74";
            this.numericUpDown74.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // pictureBox31
            // 
            this.pictureBox31.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox31.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox31, "pictureBox31");
            this.pictureBox31.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox31.Name = "pictureBox31";
            this.pictureBox31.TabStop = false;
            // 
            // textBox73
            // 
            resources.ApplyResources(this.textBox73, "textBox73");
            this.textBox73.Name = "textBox73";
            // 
            // comboBox46
            // 
            this.comboBox46.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox46.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox46, "comboBox46");
            this.comboBox46.Name = "comboBox46";
            // 
            // label75
            // 
            resources.ApplyResources(this.label75, "label75");
            this.label75.Name = "label75";
            // 
            // numericUpDown73
            // 
            resources.ApplyResources(this.numericUpDown73, "numericUpDown73");
            this.numericUpDown73.Name = "numericUpDown73";
            this.numericUpDown73.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label76
            // 
            resources.ApplyResources(this.label76, "label76");
            this.label76.Name = "label76";
            // 
            // numericUpDown72
            // 
            resources.ApplyResources(this.numericUpDown72, "numericUpDown72");
            this.numericUpDown72.Name = "numericUpDown72";
            this.numericUpDown72.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label95
            // 
            resources.ApplyResources(this.label95, "label95");
            this.label95.Name = "label95";
            // 
            // textBox69
            // 
            resources.ApplyResources(this.textBox69, "textBox69");
            this.textBox69.Name = "textBox69";
            // 
            // label96
            // 
            resources.ApplyResources(this.label96, "label96");
            this.label96.Name = "label96";
            // 
            // comboBox45
            // 
            this.comboBox45.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox45.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox45, "comboBox45");
            this.comboBox45.Name = "comboBox45";
            // 
            // label97
            // 
            resources.ApplyResources(this.label97, "label97");
            this.label97.Name = "label97";
            // 
            // numericUpDown71
            // 
            resources.ApplyResources(this.numericUpDown71, "numericUpDown71");
            this.numericUpDown71.Name = "numericUpDown71";
            this.numericUpDown71.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown70
            // 
            resources.ApplyResources(this.numericUpDown70, "numericUpDown70");
            this.numericUpDown70.Name = "numericUpDown70";
            this.numericUpDown70.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox42
            // 
            this.comboBox42.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox42.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox42, "comboBox42");
            this.comboBox42.Name = "comboBox42";
            // 
            // textBox68
            // 
            resources.ApplyResources(this.textBox68, "textBox68");
            this.textBox68.Name = "textBox68";
            // 
            // textBox66
            // 
            resources.ApplyResources(this.textBox66, "textBox66");
            this.textBox66.Name = "textBox66";
            // 
            // comboBox44
            // 
            this.comboBox44.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox44.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox44, "comboBox44");
            this.comboBox44.Name = "comboBox44";
            // 
            // numericUpDown66
            // 
            resources.ApplyResources(this.numericUpDown66, "numericUpDown66");
            this.numericUpDown66.Name = "numericUpDown66";
            this.numericUpDown66.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown69
            // 
            resources.ApplyResources(this.numericUpDown69, "numericUpDown69");
            this.numericUpDown69.Name = "numericUpDown69";
            this.numericUpDown69.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown67
            // 
            resources.ApplyResources(this.numericUpDown67, "numericUpDown67");
            this.numericUpDown67.Name = "numericUpDown67";
            this.numericUpDown67.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown68
            // 
            resources.ApplyResources(this.numericUpDown68, "numericUpDown68");
            this.numericUpDown68.Name = "numericUpDown68";
            this.numericUpDown68.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox43
            // 
            this.comboBox43.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox43.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox43, "comboBox43");
            this.comboBox43.Name = "comboBox43";
            // 
            // textBox67
            // 
            resources.ApplyResources(this.textBox67, "textBox67");
            this.textBox67.Name = "textBox67";
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.pictureBox35);
            this.groupBox15.Controls.Add(this.numericUpDown81);
            this.groupBox15.Controls.Add(this.numericUpDown80);
            this.groupBox15.Controls.Add(this.textBox83);
            this.groupBox15.Controls.Add(this.comboBox49);
            this.groupBox15.Controls.Add(this.pictureBox33);
            this.groupBox15.Controls.Add(this.numericUpDown79);
            this.groupBox15.Controls.Add(this.pictureBox34);
            this.groupBox15.Controls.Add(this.numericUpDown78);
            this.groupBox15.Controls.Add(this.textBox76);
            this.groupBox15.Controls.Add(this.comboBox48);
            this.groupBox15.Controls.Add(this.numericUpDown77);
            this.groupBox15.Controls.Add(this.numericUpDown76);
            this.groupBox15.Controls.Add(this.textBox74);
            this.groupBox15.Controls.Add(this.comboBox47);
            this.groupBox15.Controls.Add(this.label98);
            this.groupBox15.Controls.Add(this.label100);
            this.groupBox15.Controls.Add(this.label99);
            resources.ApplyResources(this.groupBox15, "groupBox15");
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.TabStop = false;
            // 
            // pictureBox35
            // 
            this.pictureBox35.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox35.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox35, "pictureBox35");
            this.pictureBox35.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox35.Name = "pictureBox35";
            this.pictureBox35.TabStop = false;
            // 
            // numericUpDown81
            // 
            resources.ApplyResources(this.numericUpDown81, "numericUpDown81");
            this.numericUpDown81.Name = "numericUpDown81";
            this.numericUpDown81.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown80
            // 
            resources.ApplyResources(this.numericUpDown80, "numericUpDown80");
            this.numericUpDown80.Name = "numericUpDown80";
            this.numericUpDown80.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // textBox83
            // 
            resources.ApplyResources(this.textBox83, "textBox83");
            this.textBox83.Name = "textBox83";
            // 
            // comboBox49
            // 
            this.comboBox49.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox49.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox49, "comboBox49");
            this.comboBox49.Name = "comboBox49";
            // 
            // pictureBox33
            // 
            this.pictureBox33.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox33.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox33, "pictureBox33");
            this.pictureBox33.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox33.Name = "pictureBox33";
            this.pictureBox33.TabStop = false;
            // 
            // numericUpDown79
            // 
            resources.ApplyResources(this.numericUpDown79, "numericUpDown79");
            this.numericUpDown79.Name = "numericUpDown79";
            this.numericUpDown79.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // pictureBox34
            // 
            this.pictureBox34.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox34.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox34, "pictureBox34");
            this.pictureBox34.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox34.Name = "pictureBox34";
            this.pictureBox34.TabStop = false;
            // 
            // numericUpDown78
            // 
            resources.ApplyResources(this.numericUpDown78, "numericUpDown78");
            this.numericUpDown78.Name = "numericUpDown78";
            this.numericUpDown78.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // textBox76
            // 
            resources.ApplyResources(this.textBox76, "textBox76");
            this.textBox76.Name = "textBox76";
            // 
            // comboBox48
            // 
            this.comboBox48.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox48.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox48, "comboBox48");
            this.comboBox48.Name = "comboBox48";
            // 
            // numericUpDown77
            // 
            resources.ApplyResources(this.numericUpDown77, "numericUpDown77");
            this.numericUpDown77.Name = "numericUpDown77";
            this.numericUpDown77.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown76
            // 
            resources.ApplyResources(this.numericUpDown76, "numericUpDown76");
            this.numericUpDown76.Name = "numericUpDown76";
            this.numericUpDown76.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // textBox74
            // 
            resources.ApplyResources(this.textBox74, "textBox74");
            this.textBox74.Name = "textBox74";
            // 
            // comboBox47
            // 
            this.comboBox47.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox47.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox47, "comboBox47");
            this.comboBox47.Name = "comboBox47";
            // 
            // label98
            // 
            resources.ApplyResources(this.label98, "label98");
            this.label98.Name = "label98";
            // 
            // label100
            // 
            resources.ApplyResources(this.label100, "label100");
            this.label100.Name = "label100";
            // 
            // label99
            // 
            resources.ApplyResources(this.label99, "label99");
            this.label99.Name = "label99";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.pictureBox37);
            this.groupBox14.Controls.Add(this.numericUpDown85);
            this.groupBox14.Controls.Add(this.numericUpDown84);
            this.groupBox14.Controls.Add(this.textBox85);
            this.groupBox14.Controls.Add(this.comboBox51);
            this.groupBox14.Controls.Add(this.numericUpDown83);
            this.groupBox14.Controls.Add(this.numericUpDown82);
            this.groupBox14.Controls.Add(this.textBox84);
            this.groupBox14.Controls.Add(this.pictureBox36);
            this.groupBox14.Controls.Add(this.comboBox50);
            this.groupBox14.Controls.Add(this.label103);
            this.groupBox14.Controls.Add(this.label101);
            resources.ApplyResources(this.groupBox14, "groupBox14");
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.TabStop = false;
            // 
            // pictureBox37
            // 
            this.pictureBox37.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox37.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox37, "pictureBox37");
            this.pictureBox37.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox37.Name = "pictureBox37";
            this.pictureBox37.TabStop = false;
            // 
            // numericUpDown85
            // 
            resources.ApplyResources(this.numericUpDown85, "numericUpDown85");
            this.numericUpDown85.Name = "numericUpDown85";
            this.numericUpDown85.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown84
            // 
            resources.ApplyResources(this.numericUpDown84, "numericUpDown84");
            this.numericUpDown84.Name = "numericUpDown84";
            this.numericUpDown84.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // textBox85
            // 
            resources.ApplyResources(this.textBox85, "textBox85");
            this.textBox85.Name = "textBox85";
            // 
            // comboBox51
            // 
            this.comboBox51.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox51.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox51, "comboBox51");
            this.comboBox51.Name = "comboBox51";
            // 
            // numericUpDown83
            // 
            resources.ApplyResources(this.numericUpDown83, "numericUpDown83");
            this.numericUpDown83.Name = "numericUpDown83";
            this.numericUpDown83.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown82
            // 
            resources.ApplyResources(this.numericUpDown82, "numericUpDown82");
            this.numericUpDown82.Name = "numericUpDown82";
            this.numericUpDown82.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // textBox84
            // 
            resources.ApplyResources(this.textBox84, "textBox84");
            this.textBox84.Name = "textBox84";
            // 
            // pictureBox36
            // 
            this.pictureBox36.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox36.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox36, "pictureBox36");
            this.pictureBox36.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox36.Name = "pictureBox36";
            this.pictureBox36.TabStop = false;
            // 
            // comboBox50
            // 
            this.comboBox50.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox50.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox50, "comboBox50");
            this.comboBox50.Name = "comboBox50";
            // 
            // label103
            // 
            resources.ApplyResources(this.label103, "label103");
            this.label103.Name = "label103";
            // 
            // label101
            // 
            resources.ApplyResources(this.label101, "label101");
            this.label101.Name = "label101";
            // 
            // label115
            // 
            resources.ApplyResources(this.label115, "label115");
            this.label115.Name = "label115";
            // 
            // label117
            // 
            resources.ApplyResources(this.label117, "label117");
            this.label117.Name = "label117";
            // 
            // label118
            // 
            resources.ApplyResources(this.label118, "label118");
            this.label118.Name = "label118";
            // 
            // label119
            // 
            resources.ApplyResources(this.label119, "label119");
            this.label119.Name = "label119";
            // 
            // label120
            // 
            resources.ApplyResources(this.label120, "label120");
            this.label120.Name = "label120";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label121);
            this.groupBox13.Controls.Add(this.trackBar4);
            this.groupBox13.Controls.Add(this.textBox82);
            resources.ApplyResources(this.groupBox13, "groupBox13");
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.TabStop = false;
            // 
            // label121
            // 
            resources.ApplyResources(this.label121, "label121");
            this.label121.Name = "label121";
            // 
            // trackBar4
            // 
            resources.ApplyResources(this.trackBar4, "trackBar4");
            this.trackBar4.Maximum = 100;
            this.trackBar4.Name = "trackBar4";
            this.trackBar4.TickFrequency = 10;
            // 
            // textBox82
            // 
            resources.ApplyResources(this.textBox82, "textBox82");
            this.textBox82.Name = "textBox82";
            // 
            // grbOtherEncounters
            // 
            this.grbOtherEncounters.Controls.Add(this.pictureBox16);
            this.grbOtherEncounters.Controls.Add(this.pictureBox17);
            this.grbOtherEncounters.Controls.Add(this.pictureBox18);
            this.grbOtherEncounters.Controls.Add(this.pictureBox19);
            this.grbOtherEncounters.Controls.Add(this.pictureBox20);
            this.grbOtherEncounters.Controls.Add(this.label57);
            this.grbOtherEncounters.Controls.Add(this.label77);
            this.grbOtherEncounters.Controls.Add(this.label78);
            this.grbOtherEncounters.Controls.Add(this.label82);
            this.grbOtherEncounters.Controls.Add(this.label83);
            this.grbOtherEncounters.Controls.Add(this.label84);
            this.grbOtherEncounters.Controls.Add(this.comboBox30);
            this.grbOtherEncounters.Controls.Add(this.textBox43);
            this.grbOtherEncounters.Controls.Add(this.numericUpDown32);
            this.grbOtherEncounters.Controls.Add(this.numericUpDown33);
            this.grbOtherEncounters.Controls.Add(this.comboBox31);
            this.grbOtherEncounters.Controls.Add(this.textBox60);
            this.grbOtherEncounters.Controls.Add(this.numericUpDown34);
            this.grbOtherEncounters.Controls.Add(this.numericUpDown35);
            this.grbOtherEncounters.Controls.Add(this.comboBox32);
            this.grbOtherEncounters.Controls.Add(this.textBox63);
            this.grbOtherEncounters.Controls.Add(this.numericUpDown46);
            this.grbOtherEncounters.Controls.Add(this.numericUpDown47);
            this.grbOtherEncounters.Controls.Add(this.comboBox33);
            this.grbOtherEncounters.Controls.Add(this.textBox70);
            this.grbOtherEncounters.Controls.Add(this.numericUpDown48);
            this.grbOtherEncounters.Controls.Add(this.numericUpDown49);
            this.grbOtherEncounters.Controls.Add(this.label85);
            this.grbOtherEncounters.Controls.Add(this.label86);
            this.grbOtherEncounters.Controls.Add(this.label89);
            this.grbOtherEncounters.Controls.Add(this.label92);
            this.grbOtherEncounters.Controls.Add(this.comboBox34);
            this.grbOtherEncounters.Controls.Add(this.textBox71);
            this.grbOtherEncounters.Controls.Add(this.numericUpDown50);
            this.grbOtherEncounters.Controls.Add(this.numericUpDown51);
            this.grbOtherEncounters.Controls.Add(this.groupBox12);
            resources.ApplyResources(this.grbOtherEncounters, "grbOtherEncounters");
            this.grbOtherEncounters.Name = "grbOtherEncounters";
            this.grbOtherEncounters.TabStop = false;
            // 
            // pictureBox16
            // 
            this.pictureBox16.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox16.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox16, "pictureBox16");
            this.pictureBox16.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox16.Name = "pictureBox16";
            this.pictureBox16.TabStop = false;
            // 
            // pictureBox17
            // 
            this.pictureBox17.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox17.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox17, "pictureBox17");
            this.pictureBox17.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox17.Name = "pictureBox17";
            this.pictureBox17.TabStop = false;
            // 
            // pictureBox18
            // 
            this.pictureBox18.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox18.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox18, "pictureBox18");
            this.pictureBox18.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox18.Name = "pictureBox18";
            this.pictureBox18.TabStop = false;
            // 
            // pictureBox19
            // 
            this.pictureBox19.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox19.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox19, "pictureBox19");
            this.pictureBox19.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox19.Name = "pictureBox19";
            this.pictureBox19.TabStop = false;
            // 
            // pictureBox20
            // 
            this.pictureBox20.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox20.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox20, "pictureBox20");
            this.pictureBox20.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox20.Name = "pictureBox20";
            this.pictureBox20.TabStop = false;
            // 
            // label57
            // 
            resources.ApplyResources(this.label57, "label57");
            this.label57.Name = "label57";
            // 
            // label77
            // 
            resources.ApplyResources(this.label77, "label77");
            this.label77.Name = "label77";
            // 
            // label78
            // 
            resources.ApplyResources(this.label78, "label78");
            this.label78.Name = "label78";
            // 
            // label82
            // 
            resources.ApplyResources(this.label82, "label82");
            this.label82.Name = "label82";
            // 
            // label83
            // 
            resources.ApplyResources(this.label83, "label83");
            this.label83.Name = "label83";
            // 
            // label84
            // 
            resources.ApplyResources(this.label84, "label84");
            this.label84.Name = "label84";
            // 
            // comboBox30
            // 
            this.comboBox30.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox30.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox30, "comboBox30");
            this.comboBox30.Name = "comboBox30";
            // 
            // textBox43
            // 
            resources.ApplyResources(this.textBox43, "textBox43");
            this.textBox43.Name = "textBox43";
            // 
            // numericUpDown32
            // 
            resources.ApplyResources(this.numericUpDown32, "numericUpDown32");
            this.numericUpDown32.Name = "numericUpDown32";
            this.numericUpDown32.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown33
            // 
            resources.ApplyResources(this.numericUpDown33, "numericUpDown33");
            this.numericUpDown33.Name = "numericUpDown33";
            this.numericUpDown33.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox31
            // 
            this.comboBox31.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox31.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox31, "comboBox31");
            this.comboBox31.Name = "comboBox31";
            // 
            // textBox60
            // 
            resources.ApplyResources(this.textBox60, "textBox60");
            this.textBox60.Name = "textBox60";
            // 
            // numericUpDown34
            // 
            resources.ApplyResources(this.numericUpDown34, "numericUpDown34");
            this.numericUpDown34.Name = "numericUpDown34";
            this.numericUpDown34.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown35
            // 
            resources.ApplyResources(this.numericUpDown35, "numericUpDown35");
            this.numericUpDown35.Name = "numericUpDown35";
            this.numericUpDown35.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox32
            // 
            this.comboBox32.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox32.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox32, "comboBox32");
            this.comboBox32.Name = "comboBox32";
            // 
            // textBox63
            // 
            resources.ApplyResources(this.textBox63, "textBox63");
            this.textBox63.Name = "textBox63";
            // 
            // numericUpDown46
            // 
            resources.ApplyResources(this.numericUpDown46, "numericUpDown46");
            this.numericUpDown46.Name = "numericUpDown46";
            this.numericUpDown46.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown47
            // 
            resources.ApplyResources(this.numericUpDown47, "numericUpDown47");
            this.numericUpDown47.Name = "numericUpDown47";
            this.numericUpDown47.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox33
            // 
            this.comboBox33.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox33.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox33, "comboBox33");
            this.comboBox33.Name = "comboBox33";
            // 
            // textBox70
            // 
            resources.ApplyResources(this.textBox70, "textBox70");
            this.textBox70.Name = "textBox70";
            // 
            // numericUpDown48
            // 
            resources.ApplyResources(this.numericUpDown48, "numericUpDown48");
            this.numericUpDown48.Name = "numericUpDown48";
            this.numericUpDown48.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown49
            // 
            resources.ApplyResources(this.numericUpDown49, "numericUpDown49");
            this.numericUpDown49.Name = "numericUpDown49";
            this.numericUpDown49.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label85
            // 
            resources.ApplyResources(this.label85, "label85");
            this.label85.Name = "label85";
            // 
            // label86
            // 
            resources.ApplyResources(this.label86, "label86");
            this.label86.Name = "label86";
            // 
            // label89
            // 
            resources.ApplyResources(this.label89, "label89");
            this.label89.Name = "label89";
            // 
            // label92
            // 
            resources.ApplyResources(this.label92, "label92");
            this.label92.Name = "label92";
            // 
            // comboBox34
            // 
            this.comboBox34.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox34.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox34, "comboBox34");
            this.comboBox34.Name = "comboBox34";
            // 
            // textBox71
            // 
            resources.ApplyResources(this.textBox71, "textBox71");
            this.textBox71.Name = "textBox71";
            // 
            // numericUpDown50
            // 
            resources.ApplyResources(this.numericUpDown50, "numericUpDown50");
            this.numericUpDown50.Name = "numericUpDown50";
            this.numericUpDown50.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown51
            // 
            resources.ApplyResources(this.numericUpDown51, "numericUpDown51");
            this.numericUpDown51.Name = "numericUpDown51";
            this.numericUpDown51.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.label94);
            this.groupBox12.Controls.Add(this.textBox72);
            this.groupBox12.Controls.Add(this.trackBar2);
            resources.ApplyResources(this.groupBox12, "groupBox12");
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.TabStop = false;
            // 
            // label94
            // 
            resources.ApplyResources(this.label94, "label94");
            this.label94.Name = "label94";
            // 
            // textBox72
            // 
            resources.ApplyResources(this.textBox72, "textBox72");
            this.textBox72.Name = "textBox72";
            // 
            // trackBar2
            // 
            resources.ApplyResources(this.trackBar2, "trackBar2");
            this.trackBar2.Maximum = 100;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.TickFrequency = 10;
            // 
            // grbWaterEncounters
            // 
            this.grbWaterEncounters.Controls.Add(this.pictureBox23);
            this.grbWaterEncounters.Controls.Add(this.pictureBox24);
            this.grbWaterEncounters.Controls.Add(this.pictureBox25);
            this.grbWaterEncounters.Controls.Add(this.pictureBox26);
            this.grbWaterEncounters.Controls.Add(this.pictureBox27);
            this.grbWaterEncounters.Controls.Add(this.label93);
            this.grbWaterEncounters.Controls.Add(this.label109);
            this.grbWaterEncounters.Controls.Add(this.label110);
            this.grbWaterEncounters.Controls.Add(this.label122);
            this.grbWaterEncounters.Controls.Add(this.label123);
            this.grbWaterEncounters.Controls.Add(this.label124);
            this.grbWaterEncounters.Controls.Add(this.comboBox37);
            this.grbWaterEncounters.Controls.Add(this.textBox87);
            this.grbWaterEncounters.Controls.Add(this.numericUpDown56);
            this.grbWaterEncounters.Controls.Add(this.numericUpDown57);
            this.grbWaterEncounters.Controls.Add(this.comboBox38);
            this.grbWaterEncounters.Controls.Add(this.textBox88);
            this.grbWaterEncounters.Controls.Add(this.numericUpDown58);
            this.grbWaterEncounters.Controls.Add(this.numericUpDown59);
            this.grbWaterEncounters.Controls.Add(this.comboBox39);
            this.grbWaterEncounters.Controls.Add(this.textBox89);
            this.grbWaterEncounters.Controls.Add(this.numericUpDown60);
            this.grbWaterEncounters.Controls.Add(this.numericUpDown61);
            this.grbWaterEncounters.Controls.Add(this.comboBox40);
            this.grbWaterEncounters.Controls.Add(this.textBox90);
            this.grbWaterEncounters.Controls.Add(this.numericUpDown62);
            this.grbWaterEncounters.Controls.Add(this.numericUpDown63);
            this.grbWaterEncounters.Controls.Add(this.label125);
            this.grbWaterEncounters.Controls.Add(this.label126);
            this.grbWaterEncounters.Controls.Add(this.label127);
            this.grbWaterEncounters.Controls.Add(this.label128);
            this.grbWaterEncounters.Controls.Add(this.comboBox41);
            this.grbWaterEncounters.Controls.Add(this.textBox91);
            this.grbWaterEncounters.Controls.Add(this.numericUpDown64);
            this.grbWaterEncounters.Controls.Add(this.numericUpDown65);
            this.grbWaterEncounters.Controls.Add(this.groupBox17);
            resources.ApplyResources(this.grbWaterEncounters, "grbWaterEncounters");
            this.grbWaterEncounters.Name = "grbWaterEncounters";
            this.grbWaterEncounters.TabStop = false;
            // 
            // pictureBox23
            // 
            this.pictureBox23.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox23.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox23, "pictureBox23");
            this.pictureBox23.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox23.Name = "pictureBox23";
            this.pictureBox23.TabStop = false;
            // 
            // pictureBox24
            // 
            this.pictureBox24.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox24.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox24, "pictureBox24");
            this.pictureBox24.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox24.Name = "pictureBox24";
            this.pictureBox24.TabStop = false;
            // 
            // pictureBox25
            // 
            this.pictureBox25.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox25.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox25, "pictureBox25");
            this.pictureBox25.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox25.Name = "pictureBox25";
            this.pictureBox25.TabStop = false;
            // 
            // pictureBox26
            // 
            this.pictureBox26.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox26.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox26, "pictureBox26");
            this.pictureBox26.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox26.Name = "pictureBox26";
            this.pictureBox26.TabStop = false;
            // 
            // pictureBox27
            // 
            this.pictureBox27.ErrorImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox27.Image = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            resources.ApplyResources(this.pictureBox27, "pictureBox27");
            this.pictureBox27.InitialImage = global::PGMEWindowsUI.Properties.Resources.PKMNQuestionMark_32x32;
            this.pictureBox27.Name = "pictureBox27";
            this.pictureBox27.TabStop = false;
            // 
            // label93
            // 
            resources.ApplyResources(this.label93, "label93");
            this.label93.Name = "label93";
            // 
            // label109
            // 
            resources.ApplyResources(this.label109, "label109");
            this.label109.Name = "label109";
            // 
            // label110
            // 
            resources.ApplyResources(this.label110, "label110");
            this.label110.Name = "label110";
            // 
            // label122
            // 
            resources.ApplyResources(this.label122, "label122");
            this.label122.Name = "label122";
            // 
            // label123
            // 
            resources.ApplyResources(this.label123, "label123");
            this.label123.Name = "label123";
            // 
            // label124
            // 
            resources.ApplyResources(this.label124, "label124");
            this.label124.Name = "label124";
            // 
            // comboBox37
            // 
            this.comboBox37.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox37.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox37, "comboBox37");
            this.comboBox37.Name = "comboBox37";
            // 
            // textBox87
            // 
            resources.ApplyResources(this.textBox87, "textBox87");
            this.textBox87.Name = "textBox87";
            // 
            // numericUpDown56
            // 
            resources.ApplyResources(this.numericUpDown56, "numericUpDown56");
            this.numericUpDown56.Name = "numericUpDown56";
            this.numericUpDown56.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown57
            // 
            resources.ApplyResources(this.numericUpDown57, "numericUpDown57");
            this.numericUpDown57.Name = "numericUpDown57";
            this.numericUpDown57.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox38
            // 
            this.comboBox38.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox38.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox38, "comboBox38");
            this.comboBox38.Name = "comboBox38";
            // 
            // textBox88
            // 
            resources.ApplyResources(this.textBox88, "textBox88");
            this.textBox88.Name = "textBox88";
            // 
            // numericUpDown58
            // 
            resources.ApplyResources(this.numericUpDown58, "numericUpDown58");
            this.numericUpDown58.Name = "numericUpDown58";
            this.numericUpDown58.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown59
            // 
            resources.ApplyResources(this.numericUpDown59, "numericUpDown59");
            this.numericUpDown59.Name = "numericUpDown59";
            this.numericUpDown59.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox39
            // 
            this.comboBox39.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox39.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox39, "comboBox39");
            this.comboBox39.Name = "comboBox39";
            // 
            // textBox89
            // 
            resources.ApplyResources(this.textBox89, "textBox89");
            this.textBox89.Name = "textBox89";
            // 
            // numericUpDown60
            // 
            resources.ApplyResources(this.numericUpDown60, "numericUpDown60");
            this.numericUpDown60.Name = "numericUpDown60";
            this.numericUpDown60.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown61
            // 
            resources.ApplyResources(this.numericUpDown61, "numericUpDown61");
            this.numericUpDown61.Name = "numericUpDown61";
            this.numericUpDown61.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // comboBox40
            // 
            this.comboBox40.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox40.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox40, "comboBox40");
            this.comboBox40.Name = "comboBox40";
            // 
            // textBox90
            // 
            resources.ApplyResources(this.textBox90, "textBox90");
            this.textBox90.Name = "textBox90";
            // 
            // numericUpDown62
            // 
            resources.ApplyResources(this.numericUpDown62, "numericUpDown62");
            this.numericUpDown62.Name = "numericUpDown62";
            this.numericUpDown62.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown63
            // 
            resources.ApplyResources(this.numericUpDown63, "numericUpDown63");
            this.numericUpDown63.Name = "numericUpDown63";
            this.numericUpDown63.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label125
            // 
            resources.ApplyResources(this.label125, "label125");
            this.label125.Name = "label125";
            // 
            // label126
            // 
            resources.ApplyResources(this.label126, "label126");
            this.label126.Name = "label126";
            // 
            // label127
            // 
            resources.ApplyResources(this.label127, "label127");
            this.label127.Name = "label127";
            // 
            // label128
            // 
            resources.ApplyResources(this.label128, "label128");
            this.label128.Name = "label128";
            // 
            // comboBox41
            // 
            this.comboBox41.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox41.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox41, "comboBox41");
            this.comboBox41.Name = "comboBox41";
            // 
            // textBox91
            // 
            resources.ApplyResources(this.textBox91, "textBox91");
            this.textBox91.Name = "textBox91";
            // 
            // numericUpDown64
            // 
            resources.ApplyResources(this.numericUpDown64, "numericUpDown64");
            this.numericUpDown64.Name = "numericUpDown64";
            this.numericUpDown64.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown65
            // 
            resources.ApplyResources(this.numericUpDown65, "numericUpDown65");
            this.numericUpDown65.Name = "numericUpDown65";
            this.numericUpDown65.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.label129);
            this.groupBox17.Controls.Add(this.textBox92);
            this.groupBox17.Controls.Add(this.trackBar5);
            resources.ApplyResources(this.groupBox17, "groupBox17");
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.TabStop = false;
            // 
            // label129
            // 
            resources.ApplyResources(this.label129, "label129");
            this.label129.Name = "label129";
            // 
            // textBox92
            // 
            resources.ApplyResources(this.textBox92, "textBox92");
            this.textBox92.Name = "textBox92";
            // 
            // trackBar5
            // 
            resources.ApplyResources(this.trackBar5, "trackBar5");
            this.trackBar5.Maximum = 100;
            this.trackBar5.Name = "trackBar5";
            this.trackBar5.TickFrequency = 10;
            // 
            // headerTabPage
            // 
            resources.ApplyResources(this.headerTabPage, "headerTabPage");
            this.headerTabPage.Controls.Add(this.flowLayoutPanel1);
            this.headerTabPage.Name = "headerTabPage";
            this.headerTabPage.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.gbHeaderTabMapHeader);
            this.flowLayoutPanel1.Controls.Add(this.gbHeaderTabLayoutHeader);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // gbHeaderTabMapHeader
            // 
            resources.ApplyResources(this.gbHeaderTabMapHeader, "gbHeaderTabMapHeader");
            this.gbHeaderTabMapHeader.Controls.Add(this.flowLayoutPanel2);
            this.gbHeaderTabMapHeader.Controls.Add(this.cbHeaderTabShowRawMapHeader);
            this.gbHeaderTabMapHeader.Name = "gbHeaderTabMapHeader";
            this.gbHeaderTabMapHeader.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Controls.Add(this.gbHeaderTabRawMapHeader);
            this.flowLayoutPanel2.Controls.Add(this.panel5);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // gbHeaderTabRawMapHeader
            // 
            this.gbHeaderTabRawMapHeader.Controls.Add(this.hexViewerRawMapHeader);
            resources.ApplyResources(this.gbHeaderTabRawMapHeader, "gbHeaderTabRawMapHeader");
            this.gbHeaderTabRawMapHeader.Name = "gbHeaderTabRawMapHeader";
            this.gbHeaderTabRawMapHeader.TabStop = false;
            // 
            // hexViewerRawMapHeader
            // 
            resources.ApplyResources(this.hexViewerRawMapHeader, "hexViewerRawMapHeader");
            this.hexViewerRawMapHeader.GroupSeparatorVisible = true;
            this.hexViewerRawMapHeader.Name = "hexViewerRawMapHeader";
            this.hexViewerRawMapHeader.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.hexViewerRawMapHeader.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.hexViewerRawMapHeader.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexViewerRawMapHeader.TabStop = false;
            this.hexViewerRawMapHeader.UseFixedBytesPerLine = true;
            this.hexViewerRawMapHeader.Validating += new System.ComponentModel.CancelEventHandler(this.hexViewerRawMapHeader_Validating);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.hexPrefixBox7);
            this.panel5.Controls.Add(this.hexNumberBoxHeaderTabLayoutHeaderPointer);
            this.panel5.Controls.Add(this.hexPrefixBox4);
            this.panel5.Controls.Add(this.groupBox5);
            this.panel5.Controls.Add(this.hexPrefixBox2);
            this.panel5.Controls.Add(this.cbHeaderTabMusic);
            this.panel5.Controls.Add(this.hexPrefixBox3);
            this.panel5.Controls.Add(this.hexNumberBoxHeaderTabMusic);
            this.panel5.Controls.Add(this.hexPrefixBox1);
            this.panel5.Controls.Add(this.label64);
            this.panel5.Controls.Add(this.cbHeaderTabCanEscape);
            this.panel5.Controls.Add(this.cbHeaderTabVisibility);
            this.panel5.Controls.Add(this.cbHeaderTabCanRideBike);
            this.panel5.Controls.Add(this.cbHeaderTabWeather);
            this.panel5.Controls.Add(this.cbHeaderTabCanRun);
            this.panel5.Controls.Add(this.label65);
            this.panel5.Controls.Add(this.cbHeaderTabShowsName);
            this.panel5.Controls.Add(this.label66);
            this.panel5.Controls.Add(this.groupBox10);
            this.panel5.Controls.Add(this.cbHeaderTabMapType);
            this.panel5.Controls.Add(this.hexNumberBoxHeaderTabConnectionDataPointer);
            this.panel5.Controls.Add(this.cbHeaderTabBattleTransition);
            this.panel5.Controls.Add(this.label72);
            this.panel5.Controls.Add(this.label68);
            this.panel5.Controls.Add(this.hexNumberBoxHeaderTabEventDataPointer);
            this.panel5.Controls.Add(this.label67);
            this.panel5.Controls.Add(this.label71);
            this.panel5.Controls.Add(this.label69);
            this.panel5.Controls.Add(this.hexNumberBoxHeaderTabLevelScriptPointer);
            this.panel5.Controls.Add(this.label70);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // hexPrefixBox7
            // 
            resources.ApplyResources(this.hexPrefixBox7, "hexPrefixBox7");
            this.hexPrefixBox7.Name = "hexPrefixBox7";
            this.hexPrefixBox7.ReadOnly = true;
            this.hexPrefixBox7.TabStop = false;
            // 
            // hexNumberBoxHeaderTabLayoutHeaderPointer
            // 
            this.hexNumberBoxHeaderTabLayoutHeaderPointer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.hexNumberBoxHeaderTabLayoutHeaderPointer, "hexNumberBoxHeaderTabLayoutHeaderPointer");
            this.hexNumberBoxHeaderTabLayoutHeaderPointer.Name = "hexNumberBoxHeaderTabLayoutHeaderPointer";
            this.hexNumberBoxHeaderTabLayoutHeaderPointer.Validated += new System.EventHandler(this.hexNumberBoxHeaderTabLayoutHeaderPointer_Validated);
            // 
            // hexPrefixBox4
            // 
            resources.ApplyResources(this.hexPrefixBox4, "hexPrefixBox4");
            this.hexPrefixBox4.Name = "hexPrefixBox4";
            this.hexPrefixBox4.ReadOnly = true;
            this.hexPrefixBox4.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.hexPrefixBox5);
            this.groupBox5.Controls.Add(this.hexNumberBoxHeaderTabMapNames);
            this.groupBox5.Controls.Add(this.button11);
            this.groupBox5.Controls.Add(this.cbHeaderTabMapNames);
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // hexPrefixBox5
            // 
            resources.ApplyResources(this.hexPrefixBox5, "hexPrefixBox5");
            this.hexPrefixBox5.Name = "hexPrefixBox5";
            this.hexPrefixBox5.ReadOnly = true;
            this.hexPrefixBox5.TabStop = false;
            // 
            // hexNumberBoxHeaderTabMapNames
            // 
            this.hexNumberBoxHeaderTabMapNames.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.hexNumberBoxHeaderTabMapNames, "hexNumberBoxHeaderTabMapNames");
            this.hexNumberBoxHeaderTabMapNames.Name = "hexNumberBoxHeaderTabMapNames";
            this.hexNumberBoxHeaderTabMapNames.TextChanged += new System.EventHandler(this.tbHeaderTabMapName_TextChanged);
            this.hexNumberBoxHeaderTabMapNames.Validated += new System.EventHandler(this.hexNumberBoxHeaderTabMapNames_Validated);
            // 
            // button11
            // 
            resources.ApplyResources(this.button11, "button11");
            this.button11.Name = "button11";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // cbHeaderTabMapNames
            // 
            this.cbHeaderTabMapNames.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbHeaderTabMapNames.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbHeaderTabMapNames.FormattingEnabled = true;
            resources.ApplyResources(this.cbHeaderTabMapNames, "cbHeaderTabMapNames");
            this.cbHeaderTabMapNames.Name = "cbHeaderTabMapNames";
            this.cbHeaderTabMapNames.SelectionChangeCommitted += new System.EventHandler(this.cbHeaderTabMapNames_SelectionChangeCommitted);
            this.cbHeaderTabMapNames.Validated += new System.EventHandler(this.cbHeaderTabMapNames_Validated);
            // 
            // hexPrefixBox2
            // 
            resources.ApplyResources(this.hexPrefixBox2, "hexPrefixBox2");
            this.hexPrefixBox2.Name = "hexPrefixBox2";
            this.hexPrefixBox2.ReadOnly = true;
            this.hexPrefixBox2.TabStop = false;
            // 
            // cbHeaderTabMusic
            // 
            this.cbHeaderTabMusic.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbHeaderTabMusic.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbHeaderTabMusic.FormattingEnabled = true;
            resources.ApplyResources(this.cbHeaderTabMusic, "cbHeaderTabMusic");
            this.cbHeaderTabMusic.Name = "cbHeaderTabMusic";
            this.cbHeaderTabMusic.SelectionChangeCommitted += new System.EventHandler(this.cbHeaderTabMusic_SelectionChangeCommitted);
            // 
            // hexPrefixBox3
            // 
            resources.ApplyResources(this.hexPrefixBox3, "hexPrefixBox3");
            this.hexPrefixBox3.Name = "hexPrefixBox3";
            this.hexPrefixBox3.ReadOnly = true;
            this.hexPrefixBox3.TabStop = false;
            // 
            // hexNumberBoxHeaderTabMusic
            // 
            this.hexNumberBoxHeaderTabMusic.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.hexNumberBoxHeaderTabMusic, "hexNumberBoxHeaderTabMusic");
            this.hexNumberBoxHeaderTabMusic.Name = "hexNumberBoxHeaderTabMusic";
            this.hexNumberBoxHeaderTabMusic.TextChanged += new System.EventHandler(this.hexNumberBoxHeaderTabMusic_TextChanged);
            // 
            // hexPrefixBox1
            // 
            resources.ApplyResources(this.hexPrefixBox1, "hexPrefixBox1");
            this.hexPrefixBox1.Name = "hexPrefixBox1";
            this.hexPrefixBox1.ReadOnly = true;
            this.hexPrefixBox1.TabStop = false;
            // 
            // label64
            // 
            resources.ApplyResources(this.label64, "label64");
            this.label64.Name = "label64";
            // 
            // cbHeaderTabCanEscape
            // 
            resources.ApplyResources(this.cbHeaderTabCanEscape, "cbHeaderTabCanEscape");
            this.cbHeaderTabCanEscape.Name = "cbHeaderTabCanEscape";
            this.cbHeaderTabCanEscape.UseVisualStyleBackColor = true;
            this.cbHeaderTabCanEscape.CheckedChanged += new System.EventHandler(this.cbHeaderTabCanEscape_CheckedChanged);
            // 
            // cbHeaderTabVisibility
            // 
            this.cbHeaderTabVisibility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHeaderTabVisibility.FormattingEnabled = true;
            this.cbHeaderTabVisibility.Items.AddRange(new object[] {
            resources.GetString("cbHeaderTabVisibility.Items"),
            resources.GetString("cbHeaderTabVisibility.Items1"),
            resources.GetString("cbHeaderTabVisibility.Items2")});
            resources.ApplyResources(this.cbHeaderTabVisibility, "cbHeaderTabVisibility");
            this.cbHeaderTabVisibility.Name = "cbHeaderTabVisibility";
            // 
            // cbHeaderTabCanRideBike
            // 
            resources.ApplyResources(this.cbHeaderTabCanRideBike, "cbHeaderTabCanRideBike");
            this.cbHeaderTabCanRideBike.Name = "cbHeaderTabCanRideBike";
            this.cbHeaderTabCanRideBike.UseVisualStyleBackColor = true;
            this.cbHeaderTabCanRideBike.CheckedChanged += new System.EventHandler(this.cbHeaderTabCanRideBike_CheckedChanged);
            // 
            // cbHeaderTabWeather
            // 
            this.cbHeaderTabWeather.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHeaderTabWeather.FormattingEnabled = true;
            this.cbHeaderTabWeather.Items.AddRange(new object[] {
            resources.GetString("cbHeaderTabWeather.Items"),
            resources.GetString("cbHeaderTabWeather.Items1"),
            resources.GetString("cbHeaderTabWeather.Items2"),
            resources.GetString("cbHeaderTabWeather.Items3"),
            resources.GetString("cbHeaderTabWeather.Items4"),
            resources.GetString("cbHeaderTabWeather.Items5"),
            resources.GetString("cbHeaderTabWeather.Items6"),
            resources.GetString("cbHeaderTabWeather.Items7"),
            resources.GetString("cbHeaderTabWeather.Items8"),
            resources.GetString("cbHeaderTabWeather.Items9"),
            resources.GetString("cbHeaderTabWeather.Items10"),
            resources.GetString("cbHeaderTabWeather.Items11"),
            resources.GetString("cbHeaderTabWeather.Items12"),
            resources.GetString("cbHeaderTabWeather.Items13"),
            resources.GetString("cbHeaderTabWeather.Items14"),
            resources.GetString("cbHeaderTabWeather.Items15")});
            resources.ApplyResources(this.cbHeaderTabWeather, "cbHeaderTabWeather");
            this.cbHeaderTabWeather.Name = "cbHeaderTabWeather";
            // 
            // cbHeaderTabCanRun
            // 
            resources.ApplyResources(this.cbHeaderTabCanRun, "cbHeaderTabCanRun");
            this.cbHeaderTabCanRun.Name = "cbHeaderTabCanRun";
            this.cbHeaderTabCanRun.UseVisualStyleBackColor = true;
            this.cbHeaderTabCanRun.CheckedChanged += new System.EventHandler(this.cbHeaderTabCanRun_CheckedChanged);
            // 
            // label65
            // 
            resources.ApplyResources(this.label65, "label65");
            this.label65.Name = "label65";
            // 
            // cbHeaderTabShowsName
            // 
            resources.ApplyResources(this.cbHeaderTabShowsName, "cbHeaderTabShowsName");
            this.cbHeaderTabShowsName.Name = "cbHeaderTabShowsName";
            this.cbHeaderTabShowsName.UseVisualStyleBackColor = true;
            this.cbHeaderTabShowsName.CheckedChanged += new System.EventHandler(this.cbHeaderTabShowsName_CheckedChanged);
            // 
            // label66
            // 
            resources.ApplyResources(this.label66, "label66");
            this.label66.Name = "label66";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.hexPrefixBox6);
            this.groupBox10.Controls.Add(this.button12);
            this.groupBox10.Controls.Add(this.hexNumberBoxHeaderTabLayoutIndex);
            resources.ApplyResources(this.groupBox10, "groupBox10");
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.TabStop = false;
            // 
            // hexPrefixBox6
            // 
            resources.ApplyResources(this.hexPrefixBox6, "hexPrefixBox6");
            this.hexPrefixBox6.Name = "hexPrefixBox6";
            this.hexPrefixBox6.ReadOnly = true;
            this.hexPrefixBox6.TabStop = false;
            // 
            // button12
            // 
            resources.ApplyResources(this.button12, "button12");
            this.button12.Name = "button12";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // hexNumberBoxHeaderTabLayoutIndex
            // 
            this.hexNumberBoxHeaderTabLayoutIndex.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.hexNumberBoxHeaderTabLayoutIndex, "hexNumberBoxHeaderTabLayoutIndex");
            this.hexNumberBoxHeaderTabLayoutIndex.Name = "hexNumberBoxHeaderTabLayoutIndex";
            this.hexNumberBoxHeaderTabLayoutIndex.Validated += new System.EventHandler(this.hexNumberBoxHeaderTabLayoutIndex_Validated);
            // 
            // cbHeaderTabMapType
            // 
            this.cbHeaderTabMapType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHeaderTabMapType.FormattingEnabled = true;
            this.cbHeaderTabMapType.Items.AddRange(new object[] {
            resources.GetString("cbHeaderTabMapType.Items"),
            resources.GetString("cbHeaderTabMapType.Items1"),
            resources.GetString("cbHeaderTabMapType.Items2"),
            resources.GetString("cbHeaderTabMapType.Items3"),
            resources.GetString("cbHeaderTabMapType.Items4"),
            resources.GetString("cbHeaderTabMapType.Items5"),
            resources.GetString("cbHeaderTabMapType.Items6"),
            resources.GetString("cbHeaderTabMapType.Items7"),
            resources.GetString("cbHeaderTabMapType.Items8"),
            resources.GetString("cbHeaderTabMapType.Items9")});
            resources.ApplyResources(this.cbHeaderTabMapType, "cbHeaderTabMapType");
            this.cbHeaderTabMapType.Name = "cbHeaderTabMapType";
            // 
            // hexNumberBoxHeaderTabConnectionDataPointer
            // 
            this.hexNumberBoxHeaderTabConnectionDataPointer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.hexNumberBoxHeaderTabConnectionDataPointer, "hexNumberBoxHeaderTabConnectionDataPointer");
            this.hexNumberBoxHeaderTabConnectionDataPointer.Name = "hexNumberBoxHeaderTabConnectionDataPointer";
            this.hexNumberBoxHeaderTabConnectionDataPointer.Validated += new System.EventHandler(this.hexNumberBoxHeaderTabConnectionDataPointer_Validated);
            // 
            // cbHeaderTabBattleTransition
            // 
            this.cbHeaderTabBattleTransition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHeaderTabBattleTransition.FormattingEnabled = true;
            this.cbHeaderTabBattleTransition.Items.AddRange(new object[] {
            resources.GetString("cbHeaderTabBattleTransition.Items"),
            resources.GetString("cbHeaderTabBattleTransition.Items1"),
            resources.GetString("cbHeaderTabBattleTransition.Items2"),
            resources.GetString("cbHeaderTabBattleTransition.Items3"),
            resources.GetString("cbHeaderTabBattleTransition.Items4"),
            resources.GetString("cbHeaderTabBattleTransition.Items5"),
            resources.GetString("cbHeaderTabBattleTransition.Items6"),
            resources.GetString("cbHeaderTabBattleTransition.Items7"),
            resources.GetString("cbHeaderTabBattleTransition.Items8")});
            resources.ApplyResources(this.cbHeaderTabBattleTransition, "cbHeaderTabBattleTransition");
            this.cbHeaderTabBattleTransition.Name = "cbHeaderTabBattleTransition";
            this.cbHeaderTabBattleTransition.SelectedIndexChanged += new System.EventHandler(this.cbHeaderTabBattleTransition_SelectedIndexChanged);
            // 
            // label72
            // 
            resources.ApplyResources(this.label72, "label72");
            this.label72.Name = "label72";
            // 
            // label68
            // 
            resources.ApplyResources(this.label68, "label68");
            this.label68.Name = "label68";
            // 
            // hexNumberBoxHeaderTabEventDataPointer
            // 
            this.hexNumberBoxHeaderTabEventDataPointer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.hexNumberBoxHeaderTabEventDataPointer, "hexNumberBoxHeaderTabEventDataPointer");
            this.hexNumberBoxHeaderTabEventDataPointer.Name = "hexNumberBoxHeaderTabEventDataPointer";
            this.hexNumberBoxHeaderTabEventDataPointer.Validated += new System.EventHandler(this.hexNumberBoxHeaderTabEventDataPointer_Validated);
            // 
            // label67
            // 
            resources.ApplyResources(this.label67, "label67");
            this.label67.Name = "label67";
            // 
            // label71
            // 
            resources.ApplyResources(this.label71, "label71");
            this.label71.Name = "label71";
            // 
            // label69
            // 
            resources.ApplyResources(this.label69, "label69");
            this.label69.Name = "label69";
            // 
            // hexNumberBoxHeaderTabLevelScriptPointer
            // 
            this.hexNumberBoxHeaderTabLevelScriptPointer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.hexNumberBoxHeaderTabLevelScriptPointer, "hexNumberBoxHeaderTabLevelScriptPointer");
            this.hexNumberBoxHeaderTabLevelScriptPointer.Name = "hexNumberBoxHeaderTabLevelScriptPointer";
            this.hexNumberBoxHeaderTabLevelScriptPointer.Validated += new System.EventHandler(this.hexNumberBoxHeaderTabLevelScriptPointer_Validated);
            // 
            // label70
            // 
            resources.ApplyResources(this.label70, "label70");
            this.label70.Name = "label70";
            // 
            // cbHeaderTabShowRawMapHeader
            // 
            resources.ApplyResources(this.cbHeaderTabShowRawMapHeader, "cbHeaderTabShowRawMapHeader");
            this.cbHeaderTabShowRawMapHeader.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbHeaderTabShowRawMapHeader.Name = "cbHeaderTabShowRawMapHeader";
            this.cbHeaderTabShowRawMapHeader.UseVisualStyleBackColor = false;
            this.cbHeaderTabShowRawMapHeader.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // gbHeaderTabLayoutHeader
            // 
            resources.ApplyResources(this.gbHeaderTabLayoutHeader, "gbHeaderTabLayoutHeader");
            this.gbHeaderTabLayoutHeader.Controls.Add(this.cbHeaderTabShowRawLayoutHeader);
            this.gbHeaderTabLayoutHeader.Controls.Add(this.flowLayoutPanel3);
            this.gbHeaderTabLayoutHeader.Name = "gbHeaderTabLayoutHeader";
            this.gbHeaderTabLayoutHeader.TabStop = false;
            // 
            // cbHeaderTabShowRawLayoutHeader
            // 
            resources.ApplyResources(this.cbHeaderTabShowRawLayoutHeader, "cbHeaderTabShowRawLayoutHeader");
            this.cbHeaderTabShowRawLayoutHeader.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbHeaderTabShowRawLayoutHeader.Name = "cbHeaderTabShowRawLayoutHeader";
            this.cbHeaderTabShowRawLayoutHeader.UseVisualStyleBackColor = false;
            this.cbHeaderTabShowRawLayoutHeader.CheckedChanged += new System.EventHandler(this.cbHeaderTabShowRawLayoutHeader_CheckedChanged);
            // 
            // flowLayoutPanel3
            // 
            resources.ApplyResources(this.flowLayoutPanel3, "flowLayoutPanel3");
            this.flowLayoutPanel3.Controls.Add(this.gbHeaderTabRawLayoutHeader);
            this.flowLayoutPanel3.Controls.Add(this.panel7);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // gbHeaderTabRawLayoutHeader
            // 
            this.gbHeaderTabRawLayoutHeader.Controls.Add(this.hexViewerRawLayoutHeader);
            resources.ApplyResources(this.gbHeaderTabRawLayoutHeader, "gbHeaderTabRawLayoutHeader");
            this.gbHeaderTabRawLayoutHeader.Name = "gbHeaderTabRawLayoutHeader";
            this.gbHeaderTabRawLayoutHeader.TabStop = false;
            // 
            // hexViewerRawLayoutHeader
            // 
            resources.ApplyResources(this.hexViewerRawLayoutHeader, "hexViewerRawLayoutHeader");
            this.hexViewerRawLayoutHeader.GroupSeparatorVisible = true;
            this.hexViewerRawLayoutHeader.Name = "hexViewerRawLayoutHeader";
            this.hexViewerRawLayoutHeader.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.hexViewerRawLayoutHeader.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.hexViewerRawLayoutHeader.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexViewerRawLayoutHeader.UseFixedBytesPerLine = true;
            this.hexViewerRawLayoutHeader.Validating += new System.ComponentModel.CancelEventHandler(this.hexViewerRawLayoutHeader_Validating);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.hexPrefixBox11);
            this.panel7.Controls.Add(this.groupBox8);
            this.panel7.Controls.Add(this.hexPrefixBox9);
            this.panel7.Controls.Add(this.label80);
            this.panel7.Controls.Add(this.hexPrefixBox10);
            this.panel7.Controls.Add(this.hexNumberBoxHeaderTabBorderPointer);
            this.panel7.Controls.Add(this.hexPrefixBox8);
            this.panel7.Controls.Add(this.label88);
            this.panel7.Controls.Add(this.groupBox9);
            this.panel7.Controls.Add(this.hexNumberBoxHeaderTabGlobalTilesetPointer);
            this.panel7.Controls.Add(this.label87);
            this.panel7.Controls.Add(this.hexNumberBoxHeaderTabLocalTilesetPointer);
            this.panel7.Controls.Add(this.hexNumberBoxHeaderTabMapPointer);
            this.panel7.Controls.Add(this.label73);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            // 
            // hexPrefixBox11
            // 
            resources.ApplyResources(this.hexPrefixBox11, "hexPrefixBox11");
            this.hexPrefixBox11.Name = "hexPrefixBox11";
            this.hexPrefixBox11.ReadOnly = true;
            this.hexPrefixBox11.TabStop = false;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label81);
            this.groupBox8.Controls.Add(this.tbHeaderTabMapWidth);
            this.groupBox8.Controls.Add(this.tbHeaderTabMapHeight);
            this.groupBox8.Controls.Add(this.label79);
            resources.ApplyResources(this.groupBox8, "groupBox8");
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.TabStop = false;
            // 
            // label81
            // 
            resources.ApplyResources(this.label81, "label81");
            this.label81.Name = "label81";
            // 
            // tbHeaderTabMapWidth
            // 
            resources.ApplyResources(this.tbHeaderTabMapWidth, "tbHeaderTabMapWidth");
            this.tbHeaderTabMapWidth.Name = "tbHeaderTabMapWidth";
            this.tbHeaderTabMapWidth.Validated += new System.EventHandler(this.tbHeaderTabMapWidth_Validated);
            // 
            // tbHeaderTabMapHeight
            // 
            resources.ApplyResources(this.tbHeaderTabMapHeight, "tbHeaderTabMapHeight");
            this.tbHeaderTabMapHeight.Name = "tbHeaderTabMapHeight";
            this.tbHeaderTabMapHeight.Validated += new System.EventHandler(this.tbHeaderTabMapHeight_Validated);
            // 
            // label79
            // 
            resources.ApplyResources(this.label79, "label79");
            this.label79.Name = "label79";
            // 
            // hexPrefixBox9
            // 
            resources.ApplyResources(this.hexPrefixBox9, "hexPrefixBox9");
            this.hexPrefixBox9.Name = "hexPrefixBox9";
            this.hexPrefixBox9.ReadOnly = true;
            this.hexPrefixBox9.TabStop = false;
            // 
            // label80
            // 
            resources.ApplyResources(this.label80, "label80");
            this.label80.Name = "label80";
            // 
            // hexPrefixBox10
            // 
            resources.ApplyResources(this.hexPrefixBox10, "hexPrefixBox10");
            this.hexPrefixBox10.Name = "hexPrefixBox10";
            this.hexPrefixBox10.ReadOnly = true;
            this.hexPrefixBox10.TabStop = false;
            // 
            // hexNumberBoxHeaderTabBorderPointer
            // 
            this.hexNumberBoxHeaderTabBorderPointer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.hexNumberBoxHeaderTabBorderPointer, "hexNumberBoxHeaderTabBorderPointer");
            this.hexNumberBoxHeaderTabBorderPointer.Name = "hexNumberBoxHeaderTabBorderPointer";
            this.hexNumberBoxHeaderTabBorderPointer.Validated += new System.EventHandler(this.hexNumberBoxHeaderTabBorderPointer_Validated);
            // 
            // hexPrefixBox8
            // 
            resources.ApplyResources(this.hexPrefixBox8, "hexPrefixBox8");
            this.hexPrefixBox8.Name = "hexPrefixBox8";
            this.hexPrefixBox8.ReadOnly = true;
            this.hexPrefixBox8.TabStop = false;
            // 
            // label88
            // 
            resources.ApplyResources(this.label88, "label88");
            this.label88.Name = "label88";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label90);
            this.groupBox9.Controls.Add(this.tbHeaderTabBorderWidth);
            this.groupBox9.Controls.Add(this.tbHeaderTabBorderHeight);
            this.groupBox9.Controls.Add(this.label91);
            resources.ApplyResources(this.groupBox9, "groupBox9");
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.TabStop = false;
            // 
            // label90
            // 
            resources.ApplyResources(this.label90, "label90");
            this.label90.Name = "label90";
            // 
            // tbHeaderTabBorderWidth
            // 
            resources.ApplyResources(this.tbHeaderTabBorderWidth, "tbHeaderTabBorderWidth");
            this.tbHeaderTabBorderWidth.Name = "tbHeaderTabBorderWidth";
            this.tbHeaderTabBorderWidth.Validated += new System.EventHandler(this.tbHeaderTabBorderWidth_Validated);
            // 
            // tbHeaderTabBorderHeight
            // 
            resources.ApplyResources(this.tbHeaderTabBorderHeight, "tbHeaderTabBorderHeight");
            this.tbHeaderTabBorderHeight.Name = "tbHeaderTabBorderHeight";
            this.tbHeaderTabBorderHeight.Validated += new System.EventHandler(this.tbHeaderTabBorderHeight_Validated);
            // 
            // label91
            // 
            resources.ApplyResources(this.label91, "label91");
            this.label91.Name = "label91";
            // 
            // hexNumberBoxHeaderTabGlobalTilesetPointer
            // 
            this.hexNumberBoxHeaderTabGlobalTilesetPointer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.hexNumberBoxHeaderTabGlobalTilesetPointer, "hexNumberBoxHeaderTabGlobalTilesetPointer");
            this.hexNumberBoxHeaderTabGlobalTilesetPointer.Name = "hexNumberBoxHeaderTabGlobalTilesetPointer";
            this.hexNumberBoxHeaderTabGlobalTilesetPointer.Validated += new System.EventHandler(this.hexNumberBoxHeaderTabGlobalTilesetPointer_Validated);
            // 
            // label87
            // 
            resources.ApplyResources(this.label87, "label87");
            this.label87.Name = "label87";
            // 
            // hexNumberBoxHeaderTabLocalTilesetPointer
            // 
            this.hexNumberBoxHeaderTabLocalTilesetPointer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.hexNumberBoxHeaderTabLocalTilesetPointer, "hexNumberBoxHeaderTabLocalTilesetPointer");
            this.hexNumberBoxHeaderTabLocalTilesetPointer.Name = "hexNumberBoxHeaderTabLocalTilesetPointer";
            this.hexNumberBoxHeaderTabLocalTilesetPointer.Validated += new System.EventHandler(this.hexNumberBoxHeaderTabLocalTilesetPointer_Validated);
            // 
            // hexNumberBoxHeaderTabMapPointer
            // 
            this.hexNumberBoxHeaderTabMapPointer.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            resources.ApplyResources(this.hexNumberBoxHeaderTabMapPointer, "hexNumberBoxHeaderTabMapPointer");
            this.hexNumberBoxHeaderTabMapPointer.Name = "hexNumberBoxHeaderTabMapPointer";
            this.hexNumberBoxHeaderTabMapPointer.Validated += new System.EventHandler(this.hexNumberBoxHeaderTabMapPointer_Validated);
            // 
            // label73
            // 
            resources.ApplyResources(this.label73, "label73");
            this.label73.Name = "label73";
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.mainMenuStrip.GripMargin = new System.Windows.Forms.Padding(2);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.mainMenuStrip, "mainMenuStrip");
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemOpenROM,
            this.tsmiReloadROM,
            this.toolStripSeparator6,
            this.toolStripMenuItemSaveROM,
            this.toolStripMenuItemSaveROMAs,
            this.toolStripSeparator7,
            this.tsmiExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // toolStripMenuItemOpenROM
            // 
            resources.ApplyResources(this.toolStripMenuItemOpenROM, "toolStripMenuItemOpenROM");
            this.toolStripMenuItemOpenROM.Name = "toolStripMenuItemOpenROM";
            this.toolStripMenuItemOpenROM.Click += new System.EventHandler(this.toolStripMenuItemOpenROM_Click);
            // 
            // tsmiReloadROM
            // 
            resources.ApplyResources(this.tsmiReloadROM, "tsmiReloadROM");
            this.tsmiReloadROM.Name = "tsmiReloadROM";
            this.tsmiReloadROM.Click += new System.EventHandler(this.tsmiReloadROM_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // toolStripMenuItemSaveROM
            // 
            resources.ApplyResources(this.toolStripMenuItemSaveROM, "toolStripMenuItemSaveROM");
            this.toolStripMenuItemSaveROM.Name = "toolStripMenuItemSaveROM";
            // 
            // toolStripMenuItemSaveROMAs
            // 
            resources.ApplyResources(this.toolStripMenuItemSaveROMAs, "toolStripMenuItemSaveROMAs");
            this.toolStripMenuItemSaveROMAs.Name = "toolStripMenuItemSaveROMAs";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            resources.ApplyResources(this.tsmiExit, "tsmiExit");
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemTilesetEditor,
            this.toolStripMenuItemConnectionEditor,
            this.toolStripMenuItemWorldMapEditor});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            resources.ApplyResources(this.toolsToolStripMenuItem, "toolsToolStripMenuItem");
            // 
            // toolStripMenuItemTilesetEditor
            // 
            resources.ApplyResources(this.toolStripMenuItemTilesetEditor, "toolStripMenuItemTilesetEditor");
            this.toolStripMenuItemTilesetEditor.Name = "toolStripMenuItemTilesetEditor";
            this.toolStripMenuItemTilesetEditor.Click += new System.EventHandler(this.blockEditorToolStripMenuItem_Click);
            // 
            // toolStripMenuItemConnectionEditor
            // 
            resources.ApplyResources(this.toolStripMenuItemConnectionEditor, "toolStripMenuItemConnectionEditor");
            this.toolStripMenuItemConnectionEditor.Name = "toolStripMenuItemConnectionEditor";
            // 
            // toolStripMenuItemWorldMapEditor
            // 
            resources.ApplyResources(this.toolStripMenuItemWorldMapEditor, "toolStripMenuItemWorldMapEditor");
            this.toolStripMenuItemWorldMapEditor.Name = "toolStripMenuItemWorldMapEditor";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemLanguage,
            this.toolStripMenuItemScriptEditor,
            this.backupsToolStripMenuItem,
            this.hexPrefixToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            // 
            // toolStripMenuItemLanguage
            // 
            this.toolStripMenuItemLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.espanolToolStripMenuItem,
            this.françaisToolStripMenuItem,
            this.deutschToolStripMenuItem});
            resources.ApplyResources(this.toolStripMenuItemLanguage, "toolStripMenuItemLanguage");
            this.toolStripMenuItemLanguage.Name = "toolStripMenuItemLanguage";
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.CheckOnClick = true;
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Tag = "en";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // espanolToolStripMenuItem
            // 
            this.espanolToolStripMenuItem.CheckOnClick = true;
            this.espanolToolStripMenuItem.Name = "espanolToolStripMenuItem";
            resources.ApplyResources(this.espanolToolStripMenuItem, "espanolToolStripMenuItem");
            this.espanolToolStripMenuItem.Tag = "es";
            this.espanolToolStripMenuItem.Click += new System.EventHandler(this.espanolToolStripMenuItem_Click);
            // 
            // françaisToolStripMenuItem
            // 
            this.françaisToolStripMenuItem.CheckOnClick = true;
            this.françaisToolStripMenuItem.Name = "françaisToolStripMenuItem";
            resources.ApplyResources(this.françaisToolStripMenuItem, "françaisToolStripMenuItem");
            this.françaisToolStripMenuItem.Tag = "fr";
            this.françaisToolStripMenuItem.Click += new System.EventHandler(this.françaisToolStripMenuItem_Click);
            // 
            // deutschToolStripMenuItem
            // 
            this.deutschToolStripMenuItem.CheckOnClick = true;
            this.deutschToolStripMenuItem.Name = "deutschToolStripMenuItem";
            resources.ApplyResources(this.deutschToolStripMenuItem, "deutschToolStripMenuItem");
            this.deutschToolStripMenuItem.Tag = "de";
            this.deutschToolStripMenuItem.Click += new System.EventHandler(this.deutschToolStripMenuItem_Click);
            // 
            // toolStripMenuItemScriptEditor
            // 
            resources.ApplyResources(this.toolStripMenuItemScriptEditor, "toolStripMenuItemScriptEditor");
            this.toolStripMenuItemScriptEditor.Name = "toolStripMenuItemScriptEditor";
            // 
            // backupsToolStripMenuItem
            // 
            this.backupsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createOnOpenToolStripMenuItem,
            this.nameFormatToolStripMenuItem});
            this.backupsToolStripMenuItem.Name = "backupsToolStripMenuItem";
            resources.ApplyResources(this.backupsToolStripMenuItem, "backupsToolStripMenuItem");
            // 
            // createOnOpenToolStripMenuItem
            // 
            this.createOnOpenToolStripMenuItem.CheckOnClick = true;
            this.createOnOpenToolStripMenuItem.Name = "createOnOpenToolStripMenuItem";
            resources.ApplyResources(this.createOnOpenToolStripMenuItem, "createOnOpenToolStripMenuItem");
            // 
            // nameFormatToolStripMenuItem
            // 
            this.nameFormatToolStripMenuItem.Name = "nameFormatToolStripMenuItem";
            resources.ApplyResources(this.nameFormatToolStripMenuItem, "nameFormatToolStripMenuItem");
            // 
            // hexPrefixToolStripMenuItem
            // 
            this.hexPrefixToolStripMenuItem.Name = "hexPrefixToolStripMenuItem";
            resources.ApplyResources(this.hexPrefixToolStripMenuItem, "hexPrefixToolStripMenuItem");
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemReadme,
            this.tsmiAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // toolStripMenuItemReadme
            // 
            resources.ApplyResources(this.toolStripMenuItemReadme, "toolStripMenuItemReadme");
            this.toolStripMenuItemReadme.Name = "toolStripMenuItemReadme";
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            resources.ApplyResources(this.tsmiAbout, "tsmiAbout");
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripOpen,
            this.toolStripSave,
            this.toolStripSeparator1,
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripSeparator2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripSeparator3,
            this.toolStripBlockEditor,
            this.toolStripConnectionEditor,
            this.toolStripWorldMapEditor,
            this.toolStripSeparator4,
            this.toolStripPluginManager,
            this.toolStripSeparator5,
            this.toolStripButton9});
            resources.ApplyResources(this.mainToolStrip, "mainToolStrip");
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Stretch = true;
            this.mainToolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripOpen
            // 
            this.toolStripOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripOpen, "toolStripOpen");
            this.toolStripOpen.Name = "toolStripOpen";
            this.toolStripOpen.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSave
            // 
            this.toolStripSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripSave, "toolStripSave");
            this.toolStripSave.Name = "toolStripSave";
            this.toolStripSave.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.Name = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton2, "toolStripButton2");
            this.toolStripButton2.Name = "toolStripButton2";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton3, "toolStripButton3");
            this.toolStripButton3.Name = "toolStripButton3";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton4, "toolStripButton4");
            this.toolStripButton4.Name = "toolStripButton4";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStripBlockEditor
            // 
            this.toolStripBlockEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripBlockEditor, "toolStripBlockEditor");
            this.toolStripBlockEditor.Name = "toolStripBlockEditor";
            // 
            // toolStripConnectionEditor
            // 
            this.toolStripConnectionEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripConnectionEditor, "toolStripConnectionEditor");
            this.toolStripConnectionEditor.Name = "toolStripConnectionEditor";
            // 
            // toolStripWorldMapEditor
            // 
            this.toolStripWorldMapEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripWorldMapEditor, "toolStripWorldMapEditor");
            this.toolStripWorldMapEditor.Name = "toolStripWorldMapEditor";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // toolStripPluginManager
            // 
            this.toolStripPluginManager.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripPluginManager, "toolStripPluginManager");
            this.toolStripPluginManager.Name = "toolStripPluginManager";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButton9, "toolStripButton9");
            this.toolStripButton9.Name = "toolStripButton9";
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslLoadingStatus,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4});
            resources.ApplyResources(this.mainStatusStrip, "mainStatusStrip");
            this.mainStatusStrip.Name = "mainStatusStrip";
            // 
            // tsslLoadingStatus
            // 
            this.tsslLoadingStatus.Name = "tsslLoadingStatus";
            resources.ApplyResources(this.tsslLoadingStatus, "tsslLoadingStatus");
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            resources.ApplyResources(this.toolStripStatusLabel2, "toolStripStatusLabel2");
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            resources.ApplyResources(this.toolStripStatusLabel3, "toolStripStatusLabel3");
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.IsLink = true;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            resources.ApplyResources(this.toolStripStatusLabel4, "toolStripStatusLabel4");
            // 
            // MainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMapListAndPaint);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.splitMapListAndPaint.Panel1.ResumeLayout(false);
            this.splitMapListAndPaint.Panel1.PerformLayout();
            this.splitMapListAndPaint.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMapListAndPaint)).EndInit();
            this.splitMapListAndPaint.ResumeLayout(false);
            this.tsMapListTree.ResumeLayout(false);
            this.tsMapListTree.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.mapTabPage.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.borderBlocksBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.mapEditorPanel.ResumeLayout(false);
            this.mapEditorPanel.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.paintTabControl.ResumeLayout(false);
            this.blocksTabPage.ResumeLayout(false);
            this.blockPaintPanel.ResumeLayout(false);
            this.movementTabPage.ResumeLayout(false);
            this.eventsTabPage.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown10)).EndInit();
            this.panelSpriteEvent.ResumeLayout(false);
            this.panelSpriteEvent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            this.panelSignEvent.ResumeLayout(false);
            this.panelSignEvent.PerformLayout();
            this.panelScriptEvent.ResumeLayout(false);
            this.panelScriptEvent.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panelWarpEvent.ResumeLayout(false);
            this.panelWarpEvent.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.wildTabPage.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.grbGrassEncounters.ResumeLayout(false);
            this.grbGrassEncounters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown8)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.grbFishingRodEncounters.ResumeLayout(false);
            this.grbFishingRodEncounters.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown75)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown74)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown73)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown72)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown71)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown70)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown66)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown69)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown67)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown68)).EndInit();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox35)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown81)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown80)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown79)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox34)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown78)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown77)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown76)).EndInit();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox37)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown85)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown84)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown83)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown82)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox36)).EndInit();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar4)).EndInit();
            this.grbOtherEncounters.ResumeLayout(false);
            this.grbOtherEncounters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown34)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown35)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown46)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown47)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown48)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown49)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown50)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown51)).EndInit();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            this.grbWaterEncounters.ResumeLayout(false);
            this.grbWaterEncounters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown56)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown57)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown58)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown59)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown60)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown61)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown62)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown63)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown64)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown65)).EndInit();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).EndInit();
            this.headerTabPage.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.gbHeaderTabMapHeader.ResumeLayout(false);
            this.gbHeaderTabMapHeader.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.gbHeaderTabRawMapHeader.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.gbHeaderTabLayoutHeader.ResumeLayout(false);
            this.gbHeaderTabLayoutHeader.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.gbHeaderTabRawLayoutHeader.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemOpenROM;
        private System.Windows.Forms.ToolStripMenuItem tsmiReloadROM;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton toolStripOpen;
        private System.Windows.Forms.ToolStripButton toolStripSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripBlockEditor;
        private System.Windows.Forms.ToolStripButton toolStripConnectionEditor;
        private System.Windows.Forms.ToolStripButton toolStripWorldMapEditor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripPluginManager;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton toolStripButton9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveROM;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveROMAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemScriptEditor;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTilesetEditor;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemReadme;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemConnectionEditor;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemWorldMapEditor;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.SplitContainer splitMapListAndPaint;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage mapTabPage;
        private System.Windows.Forms.TabPage eventsTabPage;
        private System.Windows.Forms.TabPage wildTabPage;
        private System.Windows.Forms.TabPage headerTabPage;
        private System.Windows.Forms.Panel mapEditorPanel;
        private System.Windows.Forms.TabControl paintTabControl;
        private System.Windows.Forms.TabPage blocksTabPage;
        private System.Windows.Forms.Panel blockPaintPanel;
        private System.Windows.Forms.TabPage movementTabPage;
        private System.Windows.Forms.Panel movementPaintPanel;
        private System.Windows.Forms.GroupBox borderBlocksBox;
        private System.Windows.Forms.ToolStripStatusLabel tsslLoadingStatus;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox cboTimeofDayMap;
        private System.Windows.Forms.ToolStrip tsMapListTree;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripComboBox cboTimeofDayEvents;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panelSpriteEvent;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown7;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelWarpEvent;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.NumericUpDown numericUpDown10;
        private System.Windows.Forms.ComboBox cboEventTypes;
        private System.Windows.Forms.Panel panelScriptEvent;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox textBox29;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox textBox20;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox textBox24;
        private System.Windows.Forms.TextBox textBox25;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox textBox23;
        private System.Windows.Forms.ComboBox comboBox7;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox textBox21;
        private System.Windows.Forms.TextBox textBox22;
        private System.Windows.Forms.TextBox textBox19;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Panel panelSignEvent;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox textBox18;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBox26;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox9;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox textBox27;
        private System.Windows.Forms.TextBox textBox28;
        private System.Windows.Forms.ToolStripDropDownButton tsddbMapSortOrder;
        private System.Windows.Forms.ToolStripMenuItem mapNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapBankToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapTilesetToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private System.Windows.Forms.ToolStripButton toolStripButton11;
        private System.Windows.Forms.ToolStripButton toolStripButton12;
        private System.Windows.Forms.ToolStripButton toolStripButton13;
        private System.Windows.Forms.ToolStripButton toolStripButton14;
        private System.Windows.Forms.ToolStripButton toolStripButton15;
        private System.Windows.Forms.ToolStripButton toolStripButton16;
        private System.Windows.Forms.ToolStripButton toolStripButton17;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ComboBox cboEncounterTypes;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.GroupBox grbGrassEncounters;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label50;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.ComboBox comboBox16;
        private System.Windows.Forms.TextBox textBox39;
        private System.Windows.Forms.NumericUpDown numericUpDown24;
        private System.Windows.Forms.NumericUpDown numericUpDown25;
        private System.Windows.Forms.ComboBox comboBox17;
        private System.Windows.Forms.TextBox textBox40;
        private System.Windows.Forms.NumericUpDown numericUpDown26;
        private System.Windows.Forms.NumericUpDown numericUpDown27;
        private System.Windows.Forms.ComboBox comboBox18;
        private System.Windows.Forms.TextBox textBox41;
        private System.Windows.Forms.NumericUpDown numericUpDown28;
        private System.Windows.Forms.NumericUpDown numericUpDown29;
        private System.Windows.Forms.ComboBox comboBox19;
        private System.Windows.Forms.TextBox textBox42;
        private System.Windows.Forms.NumericUpDown numericUpDown30;
        private System.Windows.Forms.NumericUpDown numericUpDown31;
        private System.Windows.Forms.ComboBox comboBox12;
        private System.Windows.Forms.TextBox textBox35;
        private System.Windows.Forms.NumericUpDown numericUpDown16;
        private System.Windows.Forms.NumericUpDown numericUpDown17;
        private System.Windows.Forms.ComboBox comboBox13;
        private System.Windows.Forms.TextBox textBox36;
        private System.Windows.Forms.NumericUpDown numericUpDown18;
        private System.Windows.Forms.NumericUpDown numericUpDown19;
        private System.Windows.Forms.ComboBox comboBox14;
        private System.Windows.Forms.TextBox textBox37;
        private System.Windows.Forms.NumericUpDown numericUpDown20;
        private System.Windows.Forms.NumericUpDown numericUpDown21;
        private System.Windows.Forms.ComboBox comboBox15;
        private System.Windows.Forms.TextBox textBox38;
        private System.Windows.Forms.NumericUpDown numericUpDown22;
        private System.Windows.Forms.NumericUpDown numericUpDown23;
        private System.Windows.Forms.ComboBox comboBox11;
        private System.Windows.Forms.TextBox textBox34;
        private System.Windows.Forms.NumericUpDown numericUpDown14;
        private System.Windows.Forms.NumericUpDown numericUpDown15;
        private System.Windows.Forms.ComboBox comboBox10;
        private System.Windows.Forms.TextBox textBox33;
        private System.Windows.Forms.NumericUpDown numericUpDown12;
        private System.Windows.Forms.NumericUpDown numericUpDown13;
        private System.Windows.Forms.ComboBox comboBox8;
        private System.Windows.Forms.TextBox textBox32;
        private System.Windows.Forms.NumericUpDown numericUpDown9;
        private System.Windows.Forms.NumericUpDown numericUpDown11;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.ComboBox comboBox6;
        private System.Windows.Forms.TextBox textBox31;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.NumericUpDown numericUpDown8;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox textBox30;
        private System.Windows.Forms.TreeView mapListTreeView;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.TextBox tbHeaderTabBorderWidth;
        private System.Windows.Forms.TextBox tbHeaderTabBorderHeight;
        private System.Windows.Forms.Label label91;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.TextBox tbHeaderTabMapWidth;
        private System.Windows.Forms.TextBox tbHeaderTabMapHeight;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.TextBox hexNumberBoxHeaderTabLocalTilesetPointer;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.TextBox hexNumberBoxHeaderTabMapPointer;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.TextBox hexNumberBoxHeaderTabGlobalTilesetPointer;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.TextBox hexNumberBoxHeaderTabBorderPointer;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.TextBox hexNumberBoxHeaderTabConnectionDataPointer;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.TextBox hexNumberBoxHeaderTabEventDataPointer;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.TextBox hexNumberBoxHeaderTabLevelScriptPointer;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label69;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.ComboBox cbHeaderTabBattleTransition;
        private System.Windows.Forms.ComboBox cbHeaderTabMapType;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.ComboBox cbHeaderTabWeather;
        private System.Windows.Forms.ComboBox cbHeaderTabVisibility;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.TextBox hexNumberBoxHeaderTabMusic;
        private System.Windows.Forms.ComboBox cbHeaderTabMusic;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox hexNumberBoxHeaderTabMapNames;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.ComboBox cbHeaderTabMapNames;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.PictureBox pictureBox13;
        private System.Windows.Forms.PictureBox pictureBox14;
        private System.Windows.Forms.PictureBox pictureBox15;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.PictureBox pictureBox11;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.GroupBox grbFishingRodEncounters;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.PictureBox pictureBox28;
        private System.Windows.Forms.PictureBox pictureBox32;
        private System.Windows.Forms.PictureBox pictureBox29;
        private System.Windows.Forms.NumericUpDown numericUpDown75;
        private System.Windows.Forms.PictureBox pictureBox30;
        private System.Windows.Forms.NumericUpDown numericUpDown74;
        private System.Windows.Forms.PictureBox pictureBox31;
        private System.Windows.Forms.TextBox textBox73;
        private System.Windows.Forms.ComboBox comboBox46;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.NumericUpDown numericUpDown73;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.NumericUpDown numericUpDown72;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.TextBox textBox69;
        private System.Windows.Forms.Label label96;
        private System.Windows.Forms.ComboBox comboBox45;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.NumericUpDown numericUpDown71;
        private System.Windows.Forms.NumericUpDown numericUpDown70;
        private System.Windows.Forms.ComboBox comboBox42;
        private System.Windows.Forms.TextBox textBox68;
        private System.Windows.Forms.TextBox textBox66;
        private System.Windows.Forms.ComboBox comboBox44;
        private System.Windows.Forms.NumericUpDown numericUpDown66;
        private System.Windows.Forms.NumericUpDown numericUpDown69;
        private System.Windows.Forms.NumericUpDown numericUpDown67;
        private System.Windows.Forms.NumericUpDown numericUpDown68;
        private System.Windows.Forms.ComboBox comboBox43;
        private System.Windows.Forms.TextBox textBox67;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.PictureBox pictureBox35;
        private System.Windows.Forms.NumericUpDown numericUpDown81;
        private System.Windows.Forms.NumericUpDown numericUpDown80;
        private System.Windows.Forms.TextBox textBox83;
        private System.Windows.Forms.ComboBox comboBox49;
        private System.Windows.Forms.PictureBox pictureBox33;
        private System.Windows.Forms.NumericUpDown numericUpDown79;
        private System.Windows.Forms.PictureBox pictureBox34;
        private System.Windows.Forms.NumericUpDown numericUpDown78;
        private System.Windows.Forms.TextBox textBox76;
        private System.Windows.Forms.ComboBox comboBox48;
        private System.Windows.Forms.NumericUpDown numericUpDown77;
        private System.Windows.Forms.NumericUpDown numericUpDown76;
        private System.Windows.Forms.TextBox textBox74;
        private System.Windows.Forms.ComboBox comboBox47;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.PictureBox pictureBox37;
        private System.Windows.Forms.NumericUpDown numericUpDown85;
        private System.Windows.Forms.NumericUpDown numericUpDown84;
        private System.Windows.Forms.TextBox textBox85;
        private System.Windows.Forms.ComboBox comboBox51;
        private System.Windows.Forms.NumericUpDown numericUpDown83;
        private System.Windows.Forms.NumericUpDown numericUpDown82;
        private System.Windows.Forms.TextBox textBox84;
        private System.Windows.Forms.PictureBox pictureBox36;
        private System.Windows.Forms.ComboBox comboBox50;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.Label label118;
        private System.Windows.Forms.Label label119;
        private System.Windows.Forms.Label label120;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Label label121;
        private System.Windows.Forms.TrackBar trackBar4;
        private System.Windows.Forms.TextBox textBox82;
        private System.Windows.Forms.GroupBox grbOtherEncounters;
        private System.Windows.Forms.PictureBox pictureBox16;
        private System.Windows.Forms.PictureBox pictureBox17;
        private System.Windows.Forms.PictureBox pictureBox18;
        private System.Windows.Forms.PictureBox pictureBox19;
        private System.Windows.Forms.PictureBox pictureBox20;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label83;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.ComboBox comboBox30;
        private System.Windows.Forms.TextBox textBox43;
        private System.Windows.Forms.NumericUpDown numericUpDown32;
        private System.Windows.Forms.NumericUpDown numericUpDown33;
        private System.Windows.Forms.ComboBox comboBox31;
        private System.Windows.Forms.TextBox textBox60;
        private System.Windows.Forms.NumericUpDown numericUpDown34;
        private System.Windows.Forms.NumericUpDown numericUpDown35;
        private System.Windows.Forms.ComboBox comboBox32;
        private System.Windows.Forms.TextBox textBox63;
        private System.Windows.Forms.NumericUpDown numericUpDown46;
        private System.Windows.Forms.NumericUpDown numericUpDown47;
        private System.Windows.Forms.ComboBox comboBox33;
        private System.Windows.Forms.TextBox textBox70;
        private System.Windows.Forms.NumericUpDown numericUpDown48;
        private System.Windows.Forms.NumericUpDown numericUpDown49;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.ComboBox comboBox34;
        private System.Windows.Forms.TextBox textBox71;
        private System.Windows.Forms.NumericUpDown numericUpDown50;
        private System.Windows.Forms.NumericUpDown numericUpDown51;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.TextBox textBox72;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.GroupBox grbWaterEncounters;
        private System.Windows.Forms.PictureBox pictureBox23;
        private System.Windows.Forms.PictureBox pictureBox24;
        private System.Windows.Forms.PictureBox pictureBox25;
        private System.Windows.Forms.PictureBox pictureBox26;
        private System.Windows.Forms.PictureBox pictureBox27;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.Label label122;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.ComboBox comboBox37;
        private System.Windows.Forms.TextBox textBox87;
        private System.Windows.Forms.NumericUpDown numericUpDown56;
        private System.Windows.Forms.NumericUpDown numericUpDown57;
        private System.Windows.Forms.ComboBox comboBox38;
        private System.Windows.Forms.TextBox textBox88;
        private System.Windows.Forms.NumericUpDown numericUpDown58;
        private System.Windows.Forms.NumericUpDown numericUpDown59;
        private System.Windows.Forms.ComboBox comboBox39;
        private System.Windows.Forms.TextBox textBox89;
        private System.Windows.Forms.NumericUpDown numericUpDown60;
        private System.Windows.Forms.NumericUpDown numericUpDown61;
        private System.Windows.Forms.ComboBox comboBox40;
        private System.Windows.Forms.TextBox textBox90;
        private System.Windows.Forms.NumericUpDown numericUpDown62;
        private System.Windows.Forms.NumericUpDown numericUpDown63;
        private System.Windows.Forms.Label label125;
        private System.Windows.Forms.Label label126;
        private System.Windows.Forms.Label label127;
        private System.Windows.Forms.Label label128;
        private System.Windows.Forms.ComboBox comboBox41;
        private System.Windows.Forms.TextBox textBox91;
        private System.Windows.Forms.NumericUpDown numericUpDown64;
        private System.Windows.Forms.NumericUpDown numericUpDown65;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.Label label129;
        private System.Windows.Forms.TextBox textBox92;
        private System.Windows.Forms.TrackBar trackBar5;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.TextBox hexNumberBoxHeaderTabLayoutIndex;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLanguage;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem espanolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem françaisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deutschToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem backupsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createOnOpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nameFormatToolStripMenuItem;
        private System.Windows.Forms.TextBox hexNumberBoxHeaderTabLayoutHeaderPointer;
        private System.Windows.Forms.CheckBox cbHeaderTabCanEscape;
        private System.Windows.Forms.CheckBox cbHeaderTabCanRideBike;
        private System.Windows.Forms.CheckBox cbHeaderTabCanRun;
        private System.Windows.Forms.CheckBox cbHeaderTabShowsName;
        private System.Windows.Forms.TextBox hexPrefixBox11;
        private System.Windows.Forms.TextBox hexPrefixBox9;
        private System.Windows.Forms.TextBox hexPrefixBox10;
        private System.Windows.Forms.TextBox hexPrefixBox8;
        private System.Windows.Forms.TextBox hexPrefixBox7;
        private System.Windows.Forms.TextBox hexPrefixBox4;
        private System.Windows.Forms.TextBox hexPrefixBox2;
        private System.Windows.Forms.TextBox hexPrefixBox3;
        private System.Windows.Forms.TextBox hexPrefixBox1;
        private System.Windows.Forms.TextBox hexPrefixBox6;
        private System.Windows.Forms.TextBox hexPrefixBox5;
        private System.Windows.Forms.ToolStripMenuItem hexPrefixToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapLayoutToolStripMenuItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox gbHeaderTabRawLayoutHeader;
        private Be.Windows.Forms.HexBox hexViewerRawLayoutHeader;
        private System.Windows.Forms.GroupBox gbHeaderTabMapHeader;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.GroupBox gbHeaderTabRawMapHeader;
        private Be.Windows.Forms.HexBox hexViewerRawMapHeader;
        private System.Windows.Forms.GroupBox gbHeaderTabLayoutHeader;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.CheckBox cbHeaderTabShowRawMapHeader;
        private System.Windows.Forms.CheckBox cbHeaderTabShowRawLayoutHeader;
        private OpenTK.GLControl glControlMapEditor;
        private System.Windows.Forms.Panel panel8;
        private OpenTK.GLControl glControlBlocks;
    }
}

