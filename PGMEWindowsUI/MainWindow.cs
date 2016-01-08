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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PGMEBackend;
using static PGMEBackend.Config;
using System.Resources;
using System.Text.RegularExpressions;
using Be.Windows.Forms;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing.Imaging;
using System.IO;
using PGMEBackend.Entities;
using PGMEBackend.GLControls;

namespace PGMEWindowsUI
{
    public partial class MainWindow : Form, UIInteractionLayer
    {
        bool editorTabLoaded = false;
        bool entityTabLoaded = false;
        bool wildTabLoaded = false;
        bool headerTabLoaded = false;

        static ImageList _imageListMapTree;
        public static ImageList imageListMapTree
        {
            get
            {
                if (_imageListMapTree == null)
                {
                    _imageListMapTree = new ImageList();
                    _imageListMapTree.Images.Add("Map", Properties.Resources.map_16x16);
                    _imageListMapTree.Images.Add("Map Selected", Properties.Resources.image_16x16);
                    _imageListMapTree.Images.Add("Map Folder Closed", Properties.Resources.folder_closed_map_16x16);
                    _imageListMapTree.Images.Add("Map Folder Open", Properties.Resources.folder_map_16x16);
                    _imageListMapTree.Images.Add("Folder Closed", Properties.Resources.folder_closed_16x16);
                    _imageListMapTree.ColorDepth = ColorDepth.Depth32Bit;
                }
                return _imageListMapTree;
            }
        }

        static ImageList _imageListTabControl;
        public static ImageList imageListTabControl
        {
            get
            {
                if (_imageListTabControl == null)
                {
                    _imageListTabControl = new ImageList();
                    _imageListTabControl.Images.Add(Properties.Resources.map_16x16);
                    _imageListTabControl.Images.Add(Properties.Resources.viewsprites_16x16);
                    _imageListTabControl.Images.Add(Properties.Resources.wildgrass_16x16);
                    _imageListTabControl.Images.Add(Properties.Resources.map_header_16x16);
                    _imageListTabControl.ColorDepth = ColorDepth.Depth32Bit;
                }
                return _imageListTabControl;
            }
        }

        public Dictionary<int, TreeNode> mapTreeNodes;
        TreeNode currentTreeNode;

        public MainWindow()
        {
            PGMEBackend.Program.Initialize(this);
            if (ReadConfig() != 0)
            {
                QuitApplication(0);
            }

            if (!string.IsNullOrEmpty(settings.Language))
            {
                // Sets the culture
                Thread.CurrentThread.CurrentCulture = new CultureInfo(settings.Language);
                // Sets the UI culture
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(settings.Language);
            }
            else
            {
                settings.Language = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                WriteConfig();
            }

            InitializeComponent();
            LoadConfig();

            mapListTreeView.ImageList = imageListMapTree;
            mainTabControl.ImageList = imageListTabControl;
            for (int i = 0; i < mainTabControl.TabPages.Count; i++)
                mainTabControl.TabPages[i].ImageIndex = i;
            PGMEBackend.Program.SetMainGUITitle(this.Text);
            SetMapSortOrder(settings.MapSortOrder);
            mapTreeNodes = new Dictionary<int, TreeNode>();
        }

        private void LoadConfig()
        {
            foreach (TextBox hexPrefixBox in GetAll(this, typeof(TextBox)))
            {
                if (hexPrefixBox.Name.Contains("hexPrefixBox"))
                    hexPrefixBox.Text = settings.HexPrefix;
            }
            foreach (TextBox hexNumberBox in GetAll(this, typeof(TextBox)))
            {
                if (hexNumberBox.Name.Contains("hexNumberBox"))
                    hexNumberBox.Validating += HexTextBox_Validating;
            }
            
            mapXPosLabel.Text = "X: " + settings.HexPrefix + "00";
            mapYPosLabel.Text = "Y: " + settings.HexPrefix + "00";
            eventXPosLabel.Text = "X: " + settings.HexPrefix + "00";
            eventYPosLabel.Text = "Y: " + settings.HexPrefix + "00";

            toolStripShowGrid.Checked = settings.ShowGrid;
            toolStripEventsShowGrid.Checked = settings.ShowGrid;
            showGridToolStripMenuItem.Checked = settings.ShowGrid;

            cbHeaderTabShowRawMapHeader.Checked = settings.ShowRawMapHeader;
            cbHeaderTabShowRawLayoutHeader.Checked = settings.ShowRawLayoutHeader;
            gbHeaderTabRawMapHeader.Visible = settings.ShowRawMapHeader;
            gbHeaderTabRawLayoutHeader.Visible = settings.ShowRawLayoutHeader;
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private MRUManager mruManager;

        private void MainWindow_Load(object sender, EventArgs e)
        {
            UndoManager.OnModified += (nl, ev) => {
                undoToolStripMenuItem.Enabled = UndoManager.HasUndo;
                redoToolStripMenuItem.Enabled = UndoManager.HasRedo;
                RefreshMapEditorControl();
            };
            mruManager = new MRUManager(
            //the menu item that will contain the recent files
            recentFilesToolStripMenuItem,

            //the funtion that will be called when a recent file gets clicked.
            recentFileGotClicked_handler,

            null,
            
            10);
        }

        public void recentFileGotClicked_handler(object sender, EventArgs e)
        {
            string fName = (sender as ToolStripItem).Text.Substring((sender as ToolStripItem).Text.IndexOf(' ') + 1); ;
            if (!File.Exists(fName))
            {
                if (MessageBox.Show(string.Format(PGMEBackend.Program.rmInternalStrings.GetString("RecentFileNotFound"), fName), PGMEBackend.Program.rmInternalStrings.GetString("FileNotFoundTitle"),
                         MessageBoxButtons.YesNo) == DialogResult.Yes)
                    mruManager.RemoveRecentFile(fName);
                return;
            }

            PGMEBackend.Program.LoadROM(fName);
        }

        public void QuitApplication(int code)
        {
            if (Application.MessageLoop)
            {
                Application.Exit();
            }
            else
            {
                Environment.Exit(code);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            PGMEBackend.Program.LoadROM();
        }

        private void toolStripMenuItemOpenROM_Click(object sender, EventArgs e)
        {
            PGMEBackend.Program.LoadROM();
        }

        public void AddRecentFile(string openedFile)
        {
            mruManager.AddRecentFile(openedFile);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void blockEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void mapAndBlocksSplitContainer_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        static Dictionary<string, MessageBoxButtons> BoxButtons = new Dictionary<string, MessageBoxButtons>
            {
                { "OK", MessageBoxButtons.OK },
                { "OKCancel", MessageBoxButtons.OKCancel },
                { "RetryCancel", MessageBoxButtons.RetryCancel },
                { "YesNo", MessageBoxButtons.YesNo },
                { "YesNoCancel", MessageBoxButtons.YesNoCancel }
            };

        static Dictionary<string, MessageBoxIcon> BoxIcons = new Dictionary<string, MessageBoxIcon>
            {
                { "Error", MessageBoxIcon.Error },
                { "Information", MessageBoxIcon.Information },
                { "Warning", MessageBoxIcon.Warning },
                { "None", MessageBoxIcon.None }
            };

        static Dictionary<DialogResult, string> DialogResults = new Dictionary<DialogResult, string>
            {
                { DialogResult.Cancel, "Cancel" },
                { DialogResult.No, "No" },
                { DialogResult.None, "None" },
                { DialogResult.OK, "OK" },
                { DialogResult.Yes, "Yes" },
            };

        public string ShowMessageBox(string body, string title)
        {
            return ShowMessageBox(body, title, "OK", "None");
        }

        public string ShowMessageBox(string body, string title, string buttons)
        {
            return ShowMessageBox(body, title, buttons, "None");
        }

        public string ShowMessageBox(string body, string title, string buttons, string icon)
        {
            return DialogResults[MessageBox.Show(body, title, BoxButtons[buttons], BoxIcons[icon])];
        }
        
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
        
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideEventEditors();
            PGMEBackend.Program.currentEntityType = cboEventTypes.SelectedIndex;

            switch (PGMEBackend.Program.currentEntityType)
            {
                default:
                    panelSpriteEvent.Visible = true;
                    nudEntityNum.Enabled = true;
                    if(PGMEBackend.Program.currentMap != null && PGMEBackend.Program.currentMap.NPCs != null)
                        SetEntityNumValues(NPC.currentNPC, PGMEBackend.Program.currentMap.NPCs.Length - 1);
                    LoadNPCView(PGMEBackend.Program.currentMap, (int)nudEntityNum.Value);
                    break;
                case 1:
                    panelWarpEvent.Visible = true;
                    nudEntityNum.Enabled = true;
                    if (PGMEBackend.Program.currentMap != null && PGMEBackend.Program.currentMap.Warps != null)
                        SetEntityNumValues(Warp.currentWarp, PGMEBackend.Program.currentMap.Warps.Length - 1);
                    LoadWarpView(PGMEBackend.Program.currentMap, (int)nudEntityNum.Value);
                    break;
                case 2:
                    panelScriptEvent.Visible = true;
                    nudEntityNum.Enabled = true;
                    if (PGMEBackend.Program.currentMap != null && PGMEBackend.Program.currentMap.Triggers != null)
                        SetEntityNumValues(Trigger.currentTrigger, PGMEBackend.Program.currentMap.Triggers.Length - 1);
                    LoadTriggerView(PGMEBackend.Program.currentMap, (int)nudEntityNum.Value);
                    break;
                case 3:
                    panelSignEvent.Visible = true;
                    nudEntityNum.Enabled = true;
                    if (PGMEBackend.Program.currentMap != null && PGMEBackend.Program.currentMap.Signs != null)
                        SetEntityNumValues(Sign.currentSign, PGMEBackend.Program.currentMap.Signs.Length - 1);
                    LoadSignView(PGMEBackend.Program.currentMap, (int)nudEntityNum.Value);
                    break;
            }
        }

        private void nudEntityNum_ValueChanged(object sender, EventArgs e)
        {
            switch (PGMEBackend.Program.currentEntityType)
            {
                default:
                    NPC.currentNPC = (int)nudEntityNum.Value;
                    LoadNPCView(PGMEBackend.Program.currentMap, NPC.currentNPC);
                    break;
                case 1:
                    Warp.currentWarp = (int)nudEntityNum.Value;
                    LoadWarpView(PGMEBackend.Program.currentMap, Warp.currentWarp);
                    break;
                case 2:
                    Trigger.currentTrigger = (int)nudEntityNum.Value;
                    LoadTriggerView(PGMEBackend.Program.currentMap, Trigger.currentTrigger);
                    break;
                case 3:
                    Sign.currentSign = (int)nudEntityNum.Value;
                    LoadSignView(PGMEBackend.Program.currentMap, Sign.currentSign);
                    break;
            }
        }

        void SetEntityNumValues(int value, int max)
        {
            if (PGMEBackend.Program.currentMap != null)
            {
                if (max >= 0)
                    nudEntityNum.Maximum = max;
                else
                    nudEntityNum.Maximum = 0;

                if (value <= nudEntityNum.Maximum)
                    nudEntityNum.Value = value;
                else
                    nudEntityNum.Value = nudEntityNum.Maximum;
            }
            else
            {
                nudEntityNum.Value = 0;
                nudEntityNum.Maximum = 0;
            }
        }

        public void EnableControlsOnROMLoad()
        {
            //Enable top menu items
            tsmiReloadROM.Enabled = true;
            toolStripMenuItemSaveROM.Enabled = true;
            toolStripMenuItemSaveROMAs.Enabled = true;
            toolStripMenuItemTilesetEditor.Enabled = true;
            toolStripMenuItemWorldMapEditor.Enabled = true;

            //Enable main toolstrip buttons
            toolStripSave.Enabled = true;
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;
            toolStripButton4.Enabled = true;
            toolStripBlockEditor.Enabled = true;
            toolStripConnectionEditor.Enabled = true;
            toolStripWorldMapEditor.Enabled = true;
            toolStripPluginManager.Enabled = true;
            toolStripButton9.Enabled = true;

            //Enable map list tree
            mapListTreeView.Enabled = true;
            tsMapListTree.Enabled = true;

            //Initialize comboboxes to default values
            cboEventTypes.SelectedIndex = 0;
            //cboTimeofDayMap.SelectedIndex = 2;
            //cboTimeofDayEvents.SelectedIndex = 2;
            cboEncounterTypes.SelectedIndex = 0;

            //disable already enabled stuff in case a ROM had already been loaded
            mainTabControl.Enabled = false;
            toolStripMenuItemConnectionEditor.Enabled = false;

            //set some defaults
            mainTabControl.SelectedIndex = 0;
            glControlBlocks.Size = new Size(128, 64);
            glControlMapEditor.Size = new Size(64, 64);
        }

        public void EnableControlsOnMapLoad()
        {
            if (PGMEBackend.Program.currentLayout == null)
            {
                mainTabControl.Enabled = true;
                toolStripMenuItemConnectionEditor.Enabled = true;
                tsmiSaveMap.Enabled = true;
                toolStripSaveMap.Enabled = true;
                tsbMapEditorMouse.Checked = true;
                showGridToolStripMenuItem.Enabled = true;
            }

            editorTabLoaded = false;
            entityTabLoaded = false;
            wildTabLoaded = false;
            headerTabLoaded = false;
        }

        private void mapNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMapSortOrder("Name");
            ClearMapNodes();
            LoadMapNodes();
        }

        private void mapBankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMapSortOrder("Bank");
            ClearMapNodes();
            LoadMapNodes();
        }

        private void mapLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMapSortOrder("Layout");
            ClearMapNodes();
            LoadMapNodes();
        }

        private void mapTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMapSortOrder("Tileset");
            ClearMapNodes();
            LoadMapNodes();
        }

        private void SetMapSortOrder(string order)
        {
            switch (order)
            {
                default:
                    tsddbMapSortOrder.Image = Properties.Resources.sort_alphabel_16x16;
                    mapNameToolStripMenuItem.Checked = true;
                    break;
                case "Bank":
                    tsddbMapSortOrder.Image = Properties.Resources.sort_number_16x16;
                    mapBankToolStripMenuItem.Checked = true;
                    break;
                case "Layout":
                    tsddbMapSortOrder.Image = Properties.Resources.sort_map_16x16;
                    mapLayoutToolStripMenuItem.Checked = true;
                    break;
                case "Tileset":
                    tsddbMapSortOrder.Image = Properties.Resources.sort_date_16x16;
                    mapTilesetToolStripMenuItem.Checked = true;
                    break;
            }
            settings.MapSortOrder = order;
            WriteConfig();
            SetCheckedMapSortOrder(order);
        }

        private void SetCheckedMapSortOrder(string order)
        {
            foreach (ToolStripMenuItem item in tsddbMapSortOrder.DropDownItems)
            {
                if (item.Tag.Equals(order))
                    item.Checked = true;
                else
                    item.Checked = false;
            }
        }

        private void label55_Click(object sender, EventArgs e)
        {

        }

        private void cboEncounterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideWildEditors();
            switch (cboEncounterTypes.SelectedIndex)
            {
                default:
                    grbGrassEncounters.Visible = true;
                    break;
                case 1:
                    grbWaterEncounters.Visible = true;
                    break;
                case 2:
                    grbFishingRodEncounters.Visible = true;
                    break;
                case 3:
                    grbOtherEncounters.Visible = true;
                    break;
            }
        }

        private void HideWildEditors()
        {
            grbGrassEncounters.Visible = false;
            grbWaterEncounters.Visible = false;
            grbOtherEncounters.Visible = false;
            grbFishingRodEncounters.Visible = false;
        }

        public void ClearMapNodes()
        {
            mapTreeNodes.Clear();
            mapListTreeView.Nodes.Clear();
            backupTree.Nodes.Clear();
        }

        TreeView backupTree = new TreeView();

        public void LoadMapNodes()
        {
            int i = 0;
            switch (settings.MapSortOrder)
            {
                default:
                    mapTreeNodes.Add(0xFF, new TreeNode(PGMEBackend.Program.rmInternalStrings.GetString("InvalidMapNameIndex")));
                    foreach (KeyValuePair<int, MapName> mapName in PGMEBackend.Program.mapNames)
                    {
                        var mapNameNode = new TreeNode("[" + mapName.Key.ToString("X2") + "] " + mapName.Value.Name);
                        mapNameNode.SelectedImageKey = "Folder Closed";
                        mapNameNode.ImageKey = "Folder Closed";
                        backupTree.Nodes.Add(mapNameNode);
                        mapTreeNodes.Add(mapName.Key, mapNameNode);

                    }
                    foreach (MapBank mapBank in PGMEBackend.Program.mapBanks.Values)
                    {
                        foreach (Map map in mapBank.GetBank().Values)
                        {
                            try
                            {
                                var node = mapTreeNodes[map.mapNameIndex].Nodes.Add("mapNode" + i++, map.name);
                                node.Tag = map;
                                mapTreeNodes[map.mapNameIndex].SelectedImageKey = "Map Folder Closed";
                                mapTreeNodes[map.mapNameIndex].ImageKey = "Map Folder Closed";
                            }
                            catch (KeyNotFoundException)
                            {
                                if (!backupTree.Nodes.Contains(mapTreeNodes[0xFF]))
                                {
                                    backupTree.Nodes.Add(mapTreeNodes[0xFF]);
                                }
                                var node = mapTreeNodes[0xFF].Nodes.Add("mapNode" + i++, map.name);
                                node.Tag = map;
                                mapTreeNodes[0xFF].SelectedImageKey = "Map Folder Closed";
                                mapTreeNodes[0xFF].ImageKey = "Map Folder Closed";
                            }
                        }
                    }
                    break;
                case "Bank":
                    foreach (KeyValuePair<int, MapBank> mapBank in PGMEBackend.Program.mapBanks)
                    {
                        var bankNode = new TreeNode("[" + mapBank.Key.ToString("X2") + "]");
                        bankNode.SelectedImageKey = "Folder Closed";
                        bankNode.ImageKey = "Folder Closed";
                        backupTree.Nodes.Add(bankNode);
                        mapTreeNodes.Add(mapBank.Key, bankNode);
                        foreach (Map map in mapBank.Value.GetBank().Values)
                        {
                            var node = bankNode.Nodes.Add("mapNode" + i++, map.name);
                            node.Tag = map;
                            bankNode.SelectedImageKey = "Map Folder Closed";
                            bankNode.ImageKey = "Map Folder Closed";
                        }
                    }
                    break;
                case "Layout":
                    foreach (KeyValuePair<int, MapLayout> mapLayout in PGMEBackend.Program.mapLayouts)
                    {
                        var mapLayoutNode = new TreeNode(mapLayout.Value.name);
                        mapLayoutNode.Tag = mapLayout.Value;
                        backupTree.Nodes.Add(mapLayoutNode);
                        mapTreeNodes.Add(mapLayout.Key, mapLayoutNode);
                    }
                    foreach (MapBank mapBank in PGMEBackend.Program.mapBanks.Values)
                    {
                        foreach (Map map in mapBank.GetBank().Values)
                        {
                            var node = mapTreeNodes[map.mapLayoutIndex].Nodes.Add("mapNode" + i++, map.name);
                            node.Tag = map;
                            mapTreeNodes[map.mapLayoutIndex].SelectedImageKey = "Map Folder Closed";
                            mapTreeNodes[map.mapLayoutIndex].ImageKey = "Map Folder Closed";
                        }

                    }
                    break;
                case "Tileset":
                    int j = 0;
                    foreach (KeyValuePair<int, MapTileset> mapTileset in PGMEBackend.Program.mapTilesets)
                    {
                        var mapTilesetNode = new TreeNode("[" + j++ + "] " + settings.HexPrefix + (mapTileset.Key + 0x8000000).ToString("X8"));
                        backupTree.Nodes.Add(mapTilesetNode);
                        mapTreeNodes.Add(mapTileset.Key, mapTilesetNode);
                    }
                    foreach (MapBank mapBank in PGMEBackend.Program.mapBanks.Values)
                    {
                        foreach (Map map in mapBank.GetBank().Values)
                        {
                            var node = mapTreeNodes[map.layout.globalTilesetPointer].Nodes.Add("mapNode" + i++, map.name);
                            node.Tag = map;
                            mapTreeNodes[map.layout.globalTilesetPointer].SelectedImageKey = "Map Folder Closed";
                            mapTreeNodes[map.layout.globalTilesetPointer].ImageKey = "Map Folder Closed";

                            node = mapTreeNodes[map.layout.localTilesetPointer].Nodes.Add("mapNode" + i++, map.name);
                            node.Tag = map;
                            mapTreeNodes[map.layout.localTilesetPointer].SelectedImageKey = "Map Folder Closed";
                            mapTreeNodes[map.layout.localTilesetPointer].ImageKey = "Map Folder Closed";
                        }

                    }
                    foreach (KeyValuePair<int, MapLayout> mapLayout in PGMEBackend.Program.mapLayouts)
                    {
                        TreeNode node;
                        if (mapTreeNodes.ContainsKey(mapLayout.Value.globalTilesetPointer) && GetNodeFromTag(mapLayout.Value, mapTreeNodes[mapLayout.Value.globalTilesetPointer]) == null)
                        {
                            node = mapTreeNodes[mapLayout.Value.globalTilesetPointer].Nodes.Add("mapNode" + i++, mapLayout.Value.name);
                            node.Tag = mapLayout.Value;
                            //mapTreeNodes.Add(mapLayout.Key, node);
                            mapTreeNodes[mapLayout.Value.globalTilesetPointer].SelectedImageKey = "Map Folder Closed";
                            mapTreeNodes[mapLayout.Value.globalTilesetPointer].ImageKey = "Map Folder Closed";
                        }
                        if (mapTreeNodes.ContainsKey(mapLayout.Value.localTilesetPointer) && GetNodeFromTag(mapLayout.Value, mapTreeNodes[mapLayout.Value.localTilesetPointer]) == null)
                        {
                            node = mapTreeNodes[mapLayout.Value.localTilesetPointer].Nodes.Add("mapNode" + i++, mapLayout.Value.name);
                            node.Tag = mapLayout.Value;
                            //mapTreeNodes.Add(mapLayout.Key, node);
                            mapTreeNodes[mapLayout.Value.localTilesetPointer].SelectedImageKey = "Map Folder Closed";
                            mapTreeNodes[mapLayout.Value.localTilesetPointer].ImageKey = "Map Folder Closed";
                        }
                    }
                    break;
            }
            CopyTreeNodes(backupTree, mapListTreeView);
            if (PGMEBackend.Program.currentLayout != null)
            {
                TreeNode itemNode = null;
                object tag = null;
                if (PGMEBackend.Program.currentMap != null)
                    tag = PGMEBackend.Program.currentMap;
                else
                    tag = PGMEBackend.Program.currentLayout;
                foreach (TreeNode node in mapListTreeView.Nodes)
                {
                    itemNode = GetNodeFromTag(tag, node);
                    if (itemNode != null)
                    {
                        itemNode.EnsureVisible();
                        itemNode.ImageKey = "Map Selected";
                        currentTreeNode = itemNode;
                        break;
                    }
                }
            }
        }

        public TreeNode GetNodeFromTag(object tag, TreeNode rootNode)
        {
            if (rootNode.Tag != null && rootNode.Tag.Equals(tag))
                return rootNode;
            foreach (TreeNode node in rootNode.Nodes)
            {
                TreeNode next = GetNodeFromTag(tag, node);
                if (next != null)
                    return next;
            }
            return null;
        }

        public void CopyTreeNodes(TreeView treeview1, TreeView treeview2)
        {
            TreeNode newTn;
            foreach (TreeNode tn in treeview1.Nodes)
            {
                newTn = (TreeNode)tn.Clone();
                newTn.Nodes.Clear();
                bool matchesFilter = newTn.Text.ToLower().Contains(tsMapFilter.Text.ToLower());
                if (CopyChildren(newTn, tn, matchesFilter) || matchesFilter)
                    treeview2.Nodes.Add(newTn);
            }
        }
        public bool CopyChildren(TreeNode parent, TreeNode original, bool forceCreate)
        {
            TreeNode newTn;
            bool shouldCreateParent = false;
            foreach (TreeNode tn in original.Nodes)
            {
                newTn = (TreeNode)tn.Clone();
                newTn.Nodes.Clear();
                bool matchesFilter = newTn.Text.ToLower().Contains(tsMapFilter.Text.ToLower()) || forceCreate;
                bool childrenWantParent = false;
                if (tn.Nodes.Count != 0)
                    childrenWantParent = CopyChildren(newTn, tn, matchesFilter);
                if (childrenWantParent || matchesFilter)
                    shouldCreateParent = true;
                if (childrenWantParent || matchesFilter)
                    parent.Nodes.Add(newTn);
            }
            return shouldCreateParent;
        }

        public void LoadMapDropdowns()
        {
            cbHeaderTabMapNames.Items.Clear();
            foreach (KeyValuePair<int, MapName> mapName in PGMEBackend.Program.mapNames)
            {
                cbHeaderTabMapNames.Items.Add(mapName.Value.Name);
            }
        }

        public void LoadMusicDropdowns()
        {
            cbHeaderTabMusic.Items.Clear();

            for (int i = 0; i <= PGMEBackend.Program.currentGame.Songs.Keys.Last(); i++)
            {
                cbHeaderTabMusic.Items.Add((PGMEBackend.Program.currentGame.Songs.ContainsKey(i) ? PGMEBackend.Program.currentGame.Songs[i] : PGMEBackend.Program.rmInternalStrings.GetString("UnknownSong") + " " + i.ToString("X4")));
            }
        }

        public string ShowFileOpenDialog(string title, string filter, bool multiselect)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = title;
            open.Filter = filter;
            open.Multiselect = multiselect;
            open.CheckFileExists = true;
            open.CheckPathExists = true;
            open.ShowDialog();
            return open.FileName;
        }

        public void SetTitleText(string title)
        {
            Text = title;
        }

        public void SetLoadingStatus(string status)
        {
            tsslLoadingStatus.Text = status;
        }

        private void tsddbMapSortOrder_Click(object sender, EventArgs e)
        {

        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void mapListTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.ImageKey = "Map Folder Open";
            e.Node.SelectedImageKey = "Map Folder Open";
        }

        private void mapListTreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.ImageKey = "Map Folder Closed";
            e.Node.SelectedImageKey = "Map Folder Closed";
        }

        private void mapListTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //TreeNode node = ((TreeView)sender).SelectedNode;
            TreeNode node = e.Node;
            if(((TreeView)sender).SelectedNode == node)
                LoadMapFromNode(node);
        }

        private void mapListTreeView_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void mapListTreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (mapListTreeView.SelectedNode != null && e.KeyCode == Keys.Enter)
            {
                TreeNode node = ((TreeView)sender).SelectedNode;
                if (node.Nodes.Count == 0)
                {
                    LoadMapFromNode(node);
                }
                else if (!node.IsExpanded)
                    node.Expand();
                else
                    node.Collapse();
                e.SuppressKeyPress = true;      //needed to prevent sound
            }
        }

        void LoadMapFromNode(TreeNode node)
        {
            if (node != null && node.Nodes.Count == 0 && node.Tag != null)
            {
                if (PGMEBackend.Program.LoadMap(node.Tag) != 0)
                {
                    mapListTreeView.SelectedNode = currentTreeNode;
                    return;
                }
                if (currentTreeNode != null)
                {
                    currentTreeNode.ImageKey = "Map";
                    currentTreeNode.SelectedImageKey = "Map";
                }
                node.ImageKey = "Map Selected";
                node.SelectedImageKey = "Map Selected";
                currentTreeNode = node;
                node.EnsureVisible();
            }
        }
        
        public void LoadMap(object map)
        {
            switch (mainTabControl.SelectedIndex)
            {
                default:
                    LoadEditorTab(map);
                    break;
                case 1:
                    LoadEntityTab(map);
                    break;
                case 3:
                    LoadHeaderTab(map);
                    break;
            }
        }

        public void LoadEditorTab(object maybeaMap)
        {
            MapLayout mapLayout;
            if (maybeaMap is Map)
                mapLayout = (maybeaMap as Map).layout;
            else
                mapLayout = maybeaMap as MapLayout;

            if (mapLayout == null)
                return;

            if (mapLayout.globalTileset != null)
                mapLayout.globalTileset.Initialize(mapLayout);
            if (mapLayout.localTileset != null)
                mapLayout.localTileset.Initialize(mapLayout);

            PGMEBackend.Program.glMapEditor.width = mapLayout.layoutWidth * 16;
            PGMEBackend.Program.glMapEditor.height = mapLayout.layoutHeight * 16;
            SetGLMapEditorSize(PGMEBackend.Program.glMapEditor.width, PGMEBackend.Program.glMapEditor.height);

            int blockChooserHeight = (int)Math.Ceiling(PGMEBackend.Program.currentGame.MainTSBlocks / 8.0d) * 16;
            if (mapLayout.localTileset != null && mapLayout.localTileset.blockSet != null)
                blockChooserHeight += (int)Math.Ceiling(mapLayout.localTileset.blockSet.blocks.Length / 8.0d) * 16;
            PGMEBackend.Program.glBlockChooser.height = blockChooserHeight;
            SetGLBlockChooserSize(PGMEBackend.Program.glBlockChooser.width, PGMEBackend.Program.glBlockChooser.height);

            SetGLBorderBlocksSize(mapLayout.borderWidth * 16, mapLayout.borderHeight * 16);
            
            RefreshMapEditorControl();
            RefreshBlockEditorControl();
            RefreshBorderBlocksControl();

            editorTabLoaded = true;
        }
        
        public void LoadEntityTab(object Map)
        {
            if (Map == null || !(Map is Map))
            {
                DisableEntityPage();
                if(Map is MapLayout)
                {
                    MapLayout mapLayout = Map as MapLayout;

                    if (mapLayout.globalTileset != null)
                        mapLayout.globalTileset.Initialize(mapLayout);
                    if (mapLayout.localTileset != null)
                        mapLayout.localTileset.Initialize(mapLayout);

                    RefreshEntityEditorControl();
                    entityTabLoaded = true;
                }
                return;
            }

            panelEntityData.Enabled = true;

            Map map = Map as Map;

            if (map.layout.globalTileset != null)
                map.layout.globalTileset.Initialize(map.layout);
            if (map.layout.localTileset != null)
                map.layout.localTileset.Initialize(map.layout);
            
            cboEventTypes.SelectedIndex = PGMEBackend.Program.currentEntityType;
            switch(PGMEBackend.Program.currentEntityType)
            {
                default:
                    SetEntityNumValues(NPC.currentNPC, map.NPCs.Length - 1);
                    LoadNPCView(map, NPC.currentNPC);
                    PGMEBackend.Program.glEntityEditor.currentEntity = new List<Entity> { map.NPCs[NPC.currentNPC] };
                    break;
                case 1:
                    SetEntityNumValues(Warp.currentWarp, map.Warps.Length - 1);
                    LoadWarpView(map, Warp.currentWarp);
                    PGMEBackend.Program.glEntityEditor.currentEntity = new List<Entity> { map.Warps[Warp.currentWarp] };
                    break;
                case 2:
                    SetEntityNumValues(Trigger.currentTrigger, map.Triggers.Length - 1);
                    LoadTriggerView(map, Trigger.currentTrigger);
                    PGMEBackend.Program.glEntityEditor.currentEntity = new List<Entity> { map.Triggers[Trigger.currentTrigger] };
                    break;
                case 3:
                    SetEntityNumValues(Sign.currentSign, map.Signs.Length - 1);
                    LoadSignView(map, Sign.currentSign);
                    PGMEBackend.Program.glEntityEditor.currentEntity = new List<Entity> { map.Signs[Sign.currentSign] };
                    break;
            }

            RefreshEntityEditorControl();

            entityTabLoaded = true;
        }

        public void DisableEntityPage()
        {
            panelEntityData.Enabled = false;
            cboEventTypes.SelectedIndex = 0;
            SetEntityNumValues(0, 0);
            foreach (Control ctrl in panelSpriteEvent.Controls)
            {
                DisableEntityViewControl(ctrl);
            }
        }

        public void LoadNPCView(Map map, int npcNum)
        {
            if (map != null && map.NPCs != null && map.NPCs.Length > npcNum)
                LoadNPCView(map.NPCs[npcNum]);
            else
                NoEntitiesOfType();
        }

        public void LoadNPCView(NPC npc)
        {
            panelSpriteEvent.Visible = true;
            nudEntityNum.Enabled = true;
            nudNPCNum.Value = npc.npcNumber;
            nudNPCSpriteNum.Value = npc.spriteNumber;
            hexNumberBoxNPCReplacement.Text = npc.replacement.ToString("X2");
            hexNumberBoxNPCFiller1.Text = npc.filler1.ToString("X2");
            hexNumberBoxNPCXPos.Text = npc.xPos.ToString("X4");
            hexNumberBoxNPCYPos.Text = npc.yPos.ToString("X4");
            cbNPCHeight.SelectedIndex = npc.height;
            cbNPCIdleAnim.SelectedIndex = npc.idleAnimation;
            hexNumberBoxNPCXBound.Text = npc.xBounds.ToString("X1");
            hexNumberBoxNPCYBound.Text = npc.yBounds.ToString("X1");
            hexNumberBoxNPCFiller2.Text = npc.filler2.ToString("X2");
            hexNumberBoxNPCTrainer.Text = npc.trainer.ToString("X2");
            hexNumberBoxNPCFiller3.Text = npc.filler3.ToString("X2");
            hexNumberBoxNPCViewRadius.Text = npc.viewRadius.ToString("X2");
            hexNumberBoxNPCFiller4.Text = npc.filler4.ToString("X2");
            hexNumberBoxNPCScriptOffset.Text = (npc.scriptOffset + 0x8000000).ToString("X8");
            hexNumberBoxNPCVisibilityFlag.Text = npc.visibilityFlag.ToString("X4");
            hexNumberBoxNPCFiller5.Text = npc.filler5.ToString("X2");
            hexNumberBoxNPCFiller6.Text = npc.filler6.ToString("X2");
            PGMEBackend.Program.glEntityEditor.currentEntity = new List<Entity> { npc };
            RefreshEntityEditorControl();
        }
        
        public void LoadWarpView(Map map, int warpNum)
        {
            if (map.Warps.Length > warpNum)
                LoadWarpView(map.Warps[warpNum]);
            else
                NoEntitiesOfType();
        }

        public void LoadWarpView(Warp warp)
        {
            panelWarpEvent.Visible = true;
            nudEntityNum.Enabled = true;
            hexNumberBoxWarpXPos.Text = warp.xPos.ToString("X4");
            hexNumberBoxWarpYPos.Text = warp.yPos.ToString("X4");
            cbWarpHeight.SelectedIndex = warp.height;
            hexNumberBoxWarpNum.Text = warp.destWarpNum.ToString("X2");
            hexNumberBoxWarpBank.Text = warp.destMapBank.ToString("X2");
            hexNumberBoxWarpMap.Text = warp.destMapNum.ToString("X2");
            PGMEBackend.Program.glEntityEditor.currentEntity = new List<Entity> { warp };
            RefreshEntityEditorControl();
        }
        
        public void LoadSignView(Map map, int signNum)
        {
            if (map != null && map.Signs != null && map.Signs.Length > signNum)
                LoadSignView(map.Signs[signNum]);
            else
                NoEntitiesOfType();
        }

        public void LoadSignView(Sign sign)
        {
            panelSignEvent.Visible = true;
            nudEntityNum.Enabled = true;
            hexNumberBoxSignXPos.Text = sign.xPos.ToString("X4");
            hexNumberBoxSignYPos.Text = sign.yPos.ToString("X4");
            cbSignHeight.SelectedIndex = sign.height;
            cbSignType.SelectedIndex = sign.type;
            hexNumberBoxSignFiller1.Text = sign.filler1.ToString("X2");
            hexNumberBoxSignFiller2.Text = sign.filler2.ToString("X2");
            hexNumberBoxSignScriptOffset.Text = (sign.scriptOffset + 0x8000000).ToString("X8");
            PGMEBackend.Program.glEntityEditor.currentEntity = new List<Entity> { sign };
            RefreshEntityEditorControl();
        }
        
        public void LoadTriggerView(Map map, int triggerNum)
        {
            if (map != null && map.Triggers != null && map.Triggers.Length > triggerNum)
                LoadTriggerView(map.Triggers[triggerNum]);
            else
                NoEntitiesOfType();
        }

        public void LoadTriggerView(Trigger trigger)
        {
            panelScriptEvent.Visible = true;
            nudEntityNum.Enabled = true;
            hexNumberBoxTriggerXPos.Text = trigger.xPos.ToString("X4");
            hexNumberBoxTriggerXPos.Text = trigger.yPos.ToString("X4");
            cbTriggerHeight.SelectedIndex = trigger.height;
            hexNumberBoxTriggerFiller1.Text = trigger.filler1.ToString("X2");
            hexNumberBoxTriggerVariable.Text = trigger.variable.ToString("X4");
            hexNumberBoxTriggerValue.Text = trigger.value.ToString("X4");
            hexNumberBoxTriggerFiller2.Text = trigger.filler2.ToString("X2");
            hexNumberBoxTriggerFiller3.Text = trigger.filler3.ToString("X2");
            hexNumberBoxTriggerScriptOffset.Text = (trigger.scriptOffset + 0x8000000).ToString("X8");
            PGMEBackend.Program.glEntityEditor.currentEntity = new List<Entity> { trigger };
            RefreshEntityEditorControl();
        }
        
        public void DisableEntityViewControl(Control ctrl)
        {
            ctrl.Enabled = false;
            if (ctrl is NumericUpDown)
                (ctrl as NumericUpDown).Value = 0;
            else if (ctrl is ComboBox)
                (ctrl as ComboBox).SelectedIndex = 0;
            else if (ctrl is TextBox && !ctrl.Name.Contains("hexPrefixBox"))
                (ctrl as TextBox).Text = 0.ToString("X" + (ctrl as TextBox).MaxLength);
        }

        public void LoadHeaderTab(object maybeaMap)
        {
            if (maybeaMap.GetType() == typeof(Map))
            {
                Map map = (Map)maybeaMap;
                gbHeaderTabMapHeader.Visible = true;
                LoadHeaderTabMapHeader(map);
            }
            else
            {
                /*hexViewerRawMapHeader.ByteProvider = null;
                hexNumberBoxHeaderTabLayoutHeaderPointer.Text = string.Empty;
                hexNumberBoxHeaderTabEventDataPointer.Text = string.Empty;
                hexNumberBoxHeaderTabLevelScriptPointer.Text = string.Empty;
                hexNumberBoxHeaderTabConnectionDataPointer.Text = string.Empty;

                hexNumberBoxHeaderTabMapNames.Text = string.Empty;
                cbHeaderTabMapNames.Text = string.Empty;
                cbHeaderTabShowsName.Checked = false;
                cbHeaderTabCanRun.Checked = false;
                cbHeaderTabCanRideBike.Checked = false;
                cbHeaderTabCanEscape.Checked = false;

                hexNumberBoxHeaderTabLayoutIndex.Text = string.Empty;

                hexNumberBoxHeaderTabMusic.Text = string.Empty;
                cbHeaderTabMusic.Text = string.Empty;

                cbHeaderTabVisibility.SelectedIndex = 0;
                cbHeaderTabWeather.SelectedIndex = 0;
                cbHeaderTabMapType.SelectedIndex = 0;
                cbHeaderTabBattleTransition.SelectedIndex = 0;
                gbHeaderTabMapHeader.Enabled = false;*/
                gbHeaderTabMapHeader.Visible = false;
            }

            if (maybeaMap.GetType() == typeof(MapLayout) || ((Map)maybeaMap).layout != null)
            {
                MapLayout mapLayout;
                if (maybeaMap.GetType() == typeof(MapLayout))
                    mapLayout = (MapLayout)maybeaMap;
                else
                    mapLayout = ((Map)maybeaMap).layout;
                gbHeaderTabLayoutHeader.Visible = true;
                LoadHeaderTabLayoutHeader(mapLayout);
            }
            else
            {
                /*hexViewerRawLayoutHeader.ByteProvider = null;
                hexNumberBoxHeaderTabBorderPointer.Text = string.Empty;
                hexNumberBoxHeaderTabMapPointer.Text = string.Empty;
                hexNumberBoxHeaderTabGlobalTilesetPointer.Text = string.Empty;
                hexNumberBoxHeaderTabLocalTilesetPointer.Text = string.Empty;

                tbHeaderTabMapWidth.Text = string.Empty;
                tbHeaderTabMapHeight.Text = string.Empty;

                tbHeaderTabBorderWidth.Text = string.Empty;
                tbHeaderTabBorderHeight.Text = string.Empty;
                gbHeaderTabLayoutHeader.Enabled = false;*/
                gbHeaderTabRawLayoutHeader.Visible = false;
                gbHeaderTabLayoutHeader.Visible = false;
            }

            headerTabLoaded = true;
        }

        private void LoadHeaderTabMapHeader(Map map)
        {
            hexViewerRawMapHeader.ByteProvider = new DynamicByteProvider(map.rawData, true, false, false);
            hexBox1.ByteProvider = new DynamicByteProvider(map.rawData, true, false, false);
            hexNumberBoxHeaderTabLayoutHeaderPointer.Text = (map.mapDataPointer + 0x8000000).ToString("X8");
            hexNumberBoxHeaderTabEventDataPointer.Text = (map.eventDataPointer + 0x8000000).ToString("X8");
            hexNumberBoxHeaderTabLevelScriptPointer.Text = (map.mapScriptDataPointer + 0x8000000).ToString("X8");
            hexNumberBoxHeaderTabConnectionDataPointer.Text = (map.connectionsDataPointer + 0x8000000).ToString("X8");

            hexNumberBoxHeaderTabMapNames.Text = ((byte)map.mapNameIndex).ToString("X2");
            cbHeaderTabShowsName.Checked = map.showsName;
            cbHeaderTabCanRun.Checked = map.canRun;
            cbHeaderTabCanRideBike.Checked = map.canRideBike;
            cbHeaderTabCanEscape.Checked = map.canEscape;

            hexNumberBoxHeaderTabLayoutIndex.Text = ((short)map.mapLayoutIndex).ToString("X4");

            hexNumberBoxHeaderTabMusic.Text = ((short)map.musicNumber).ToString("X4");

            cbHeaderTabVisibility.SelectedIndex = ((cbHeaderTabVisibility.Items.Count > map.visibility) ? map.visibility : 0);
            cbHeaderTabWeather.SelectedIndex = ((cbHeaderTabWeather.Items.Count > map.weather) ? map.weather : 0);
            cbHeaderTabMapType.SelectedIndex = ((cbHeaderTabMapType.Items.Count > map.mapType) ? map.mapType : 0);
            cbHeaderTabBattleTransition.SelectedIndex = ((cbHeaderTabBattleTransition.Items.Count > map.battleTransition) ? map.battleTransition : 0);
        }

        private void LoadHeaderTabLayoutHeader(MapLayout mapLayout)
        {
            hexViewerRawLayoutHeader.ByteProvider = new DynamicByteProvider(mapLayout.rawData, true, false, false);
            hexNumberBoxHeaderTabBorderPointer.Text = (mapLayout.borderBlocksPointer + 0x8000000).ToString("X8");
            hexNumberBoxHeaderTabMapPointer.Text = (mapLayout.mapDataPointer + 0x8000000).ToString("X8");
            hexNumberBoxHeaderTabGlobalTilesetPointer.Text = (mapLayout.globalTilesetPointer + 0x8000000).ToString("X8");
            hexNumberBoxHeaderTabLocalTilesetPointer.Text = (mapLayout.localTilesetPointer + 0x8000000).ToString("X8");

            tbHeaderTabMapWidth.Text = mapLayout.layoutWidth.ToString();
            tbHeaderTabMapHeight.Text = mapLayout.layoutHeight.ToString();

            if (PGMEBackend.Program.currentGame.RomType == "FRLG")
            {
                tbHeaderTabBorderWidth.Text = mapLayout.borderWidth.ToString();
                tbHeaderTabBorderHeight.Text = mapLayout.borderHeight.ToString();
                tbHeaderTabBorderWidth.Enabled = true;
                tbHeaderTabBorderHeight.Enabled = true;
            }
            else
            {
                tbHeaderTabBorderWidth.Text = string.Empty;
                tbHeaderTabBorderHeight.Text = string.Empty;
                tbHeaderTabBorderWidth.Enabled = false;
                tbHeaderTabBorderHeight.Enabled = false;
            }
        }

        private void tbHeaderTabMapName_TextChanged(object sender, EventArgs e)
        {
            int mapNameIndex;
            if (int.TryParse(hexNumberBoxHeaderTabMapNames.Text, NumberStyles.HexNumber, null, out mapNameIndex))
            {
                if (mapNameIndex >= PGMEBackend.Program.currentGame.MapNameStart && mapNameIndex <= PGMEBackend.Program.currentGame.MapNameTotal)
                    cbHeaderTabMapNames.SelectedIndex = mapNameIndex - PGMEBackend.Program.currentGame.MapNameStart;
                else
                    cbHeaderTabMapNames.Text = PGMEBackend.Program.rmInternalStrings.GetString("UnknownIndex");
            }
        }

        private void tbHeaderTabMapName_Validating(object sender, CancelEventArgs e)
        {
            SanitizeHexTextbox(sender as TextBox);
        }

        private void tbHeaderTabMapName_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void cbHeaderTabBattleTransition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void HexTextBox_Validating(object sender, EventArgs e)
        {
            SanitizeHexTextbox(sender as TextBox);
        }

        private void SanitizeHexTextbox(TextBox box)
        {
            box.Text = Convert.ToInt32(SanitizeHex(box.Text), 0x10).ToString("X" + box.MaxLength);
        }

        private string SanitizeHex(string textString)
        {
            // Replace non-hex characters with empty strings.
            return Regex.Replace(textString, @"[^0-9A-F]", "", RegexOptions.None);
        }

        private void hexNumberBoxHeaderTabMusic_TextChanged(object sender, EventArgs e)
        {
            int musicIndex;
            if (int.TryParse(hexNumberBoxHeaderTabMusic.Text, NumberStyles.HexNumber, null, out musicIndex))
            {
                if (musicIndex >= 0 && musicIndex <= PGMEBackend.Program.currentGame.Songs.Keys.Last())
                    cbHeaderTabMusic.SelectedIndex = musicIndex;
            }
        }

        private void cbHeaderTabMapNames_SelectionChangeCommitted(object sender, EventArgs e)
        {
            hexNumberBoxHeaderTabMapNames.Text = (((ComboBox)sender).SelectedIndex + PGMEBackend.Program.currentGame.MapNameStart).ToString("X2");
        }

        private void cbHeaderTabMusic_SelectionChangeCommitted(object sender, EventArgs e)
        {
            hexNumberBoxHeaderTabMusic.Text = ((ComboBox)sender).SelectedIndex.ToString("X4");
        }

        private void tsmiReloadROM_Click(object sender, EventArgs e)
        {
            PGMEBackend.Program.ReloadROM();
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            QuitApplication(0);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(PGMEBackend.Program.ROM.Edited || PGMEBackend.Program.isEdited)
                e.Cancel = PGMEBackend.Program.UnsavedChangesQuitDialog() == "Cancel";
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.ShowDialog();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            gbHeaderTabRawMapHeader.Visible = (sender as CheckBox).Checked;
        }

        private void cbHeaderTabShowRawLayoutHeader_CheckedChanged(object sender, EventArgs e)
        {
            gbHeaderTabRawLayoutHeader.Visible = (sender as CheckBox).Checked;
        }

        private void hexViewerRawMapHeader_Validating(object sender, CancelEventArgs e)
        {
            Map currentMap = PGMEBackend.Program.currentMap;
            currentMap.rawData = (hexViewerRawMapHeader.ByteProvider as DynamicByteProvider).Bytes.ToArray();
            currentMap.LoadMapHeaderFromRaw(PGMEBackend.Program.ROM);
            LoadHeaderTabMapHeader(currentMap);
        }

        private void hexNumberBoxHeaderTabLayoutHeaderPointer_Validated(object sender, EventArgs e)
        {
            Map currentMap = PGMEBackend.Program.currentMap;
            currentMap.mapDataPointer = int.Parse(((TextBox)sender).Text, NumberStyles.HexNumber);
            currentMap.LoadRawFromMapHeader();
            hexViewerRawMapHeader.ByteProvider = new DynamicByteProvider(currentMap.rawData, true, false, false);
        }

        private void hexNumberBoxHeaderTabEventDataPointer_Validated(object sender, EventArgs e)
        {
            Map currentMap = PGMEBackend.Program.currentMap;
            currentMap.eventDataPointer = int.Parse(((TextBox)sender).Text, NumberStyles.HexNumber);
            currentMap.LoadRawFromMapHeader();
            hexViewerRawMapHeader.ByteProvider = new DynamicByteProvider(currentMap.rawData, true, false, false);
        }

        private void hexNumberBoxHeaderTabLevelScriptPointer_Validated(object sender, EventArgs e)
        {
            Map currentMap = PGMEBackend.Program.currentMap;
            currentMap.mapScriptDataPointer = int.Parse(((TextBox)sender).Text, NumberStyles.HexNumber);
            currentMap.LoadRawFromMapHeader();
            hexViewerRawMapHeader.ByteProvider = new DynamicByteProvider(currentMap.rawData, true, false, false);
        }

        private void hexNumberBoxHeaderTabConnectionDataPointer_Validated(object sender, EventArgs e)
        {
            Map currentMap = PGMEBackend.Program.currentMap;
            currentMap.connectionsDataPointer = int.Parse(((TextBox)sender).Text, NumberStyles.HexNumber);
            currentMap.LoadRawFromMapHeader();
            hexViewerRawMapHeader.ByteProvider = new DynamicByteProvider(currentMap.rawData, true, false, false);
        }

        private void hexNumberBoxHeaderTabMapNames_Validated(object sender, EventArgs e)
        {
            Map currentMap = PGMEBackend.Program.currentMap;
            currentMap.mapNameIndex = int.Parse(((TextBox)sender).Text, NumberStyles.HexNumber);
            currentMap.LoadRawFromMapHeader();
            hexViewerRawMapHeader.ByteProvider = new DynamicByteProvider(currentMap.rawData, true, false, false);
        }

        private void cbHeaderTabMapNames_Validated(object sender, EventArgs e)
        {
            Map currentMap = PGMEBackend.Program.currentMap;
            currentMap.mapNameIndex = int.Parse(hexNumberBoxHeaderTabMapNames.Text, NumberStyles.HexNumber);
            currentMap.LoadRawFromMapHeader();
            hexViewerRawMapHeader.ByteProvider = new DynamicByteProvider(currentMap.rawData, true, false, false);
        }

        private void cbHeaderTabShowsName_CheckedChanged(object sender, EventArgs e)
        {
            Map currentMap = PGMEBackend.Program.currentMap;
            currentMap.showsName = ((CheckBox)sender).Checked;
            currentMap.LoadRawFromMapHeader();
            hexViewerRawMapHeader.ByteProvider = new DynamicByteProvider(currentMap.rawData, true, false, false);
        }

        private void cbHeaderTabCanRun_CheckedChanged(object sender, EventArgs e)
        {
            Map currentMap = PGMEBackend.Program.currentMap;
            currentMap.canRun = ((CheckBox)sender).Checked;
            currentMap.LoadRawFromMapHeader();
            hexViewerRawMapHeader.ByteProvider = new DynamicByteProvider(currentMap.rawData, true, false, false);
        }

        private void cbHeaderTabCanRideBike_CheckedChanged(object sender, EventArgs e)
        {
            Map currentMap = PGMEBackend.Program.currentMap;
            currentMap.canRideBike = ((CheckBox)sender).Checked;
            currentMap.LoadRawFromMapHeader();
            hexViewerRawMapHeader.ByteProvider = new DynamicByteProvider(currentMap.rawData, true, false, false);
        }

        private void cbHeaderTabCanEscape_CheckedChanged(object sender, EventArgs e)
        {
            Map currentMap = PGMEBackend.Program.currentMap;
            currentMap.canEscape = ((CheckBox)sender).Checked;
            currentMap.LoadRawFromMapHeader();
            hexViewerRawMapHeader.ByteProvider = new DynamicByteProvider(currentMap.rawData, true, false, false);
        }

        private void hexNumberBoxHeaderTabLayoutIndex_Validated(object sender, EventArgs e)
        {
            Map currentMap = PGMEBackend.Program.currentMap;
            currentMap.mapLayoutIndex = int.Parse(hexNumberBoxHeaderTabLayoutIndex.Text, NumberStyles.HexNumber);
            currentMap.LoadRawFromMapHeader();
            hexViewerRawMapHeader.ByteProvider = new DynamicByteProvider(currentMap.rawData, true, false, false);
        }

        private void hexViewerRawLayoutHeader_Validating(object sender, CancelEventArgs e)
        {
            MapLayout currentLayout = PGMEBackend.Program.currentLayout;
            currentLayout.rawData = (hexViewerRawLayoutHeader.ByteProvider as DynamicByteProvider).Bytes.ToArray();
            currentLayout.LoadLayoutHeaderFromRaw(PGMEBackend.Program.ROM);
            LoadHeaderTabLayoutHeader(currentLayout);
        }

        private void hexNumberBoxHeaderTabBorderPointer_Validated(object sender, EventArgs e)
        {
            MapLayout currentLayout = PGMEBackend.Program.currentLayout;
            currentLayout.borderBlocksPointer = int.Parse(hexNumberBoxHeaderTabBorderPointer.Text, NumberStyles.HexNumber);
            currentLayout.LoadRawFromLayoutHeader();
            hexViewerRawLayoutHeader.ByteProvider = new DynamicByteProvider(currentLayout.rawData, true, false, false);
        }

        private void hexNumberBoxHeaderTabMapPointer_Validated(object sender, EventArgs e)
        {
            MapLayout currentLayout = PGMEBackend.Program.currentLayout;
            currentLayout.mapDataPointer = int.Parse(hexNumberBoxHeaderTabMapPointer.Text, NumberStyles.HexNumber);
            currentLayout.LoadRawFromLayoutHeader();
            hexViewerRawLayoutHeader.ByteProvider = new DynamicByteProvider(currentLayout.rawData, true, false, false);
        }

        private void hexNumberBoxHeaderTabGlobalTilesetPointer_Validated(object sender, EventArgs e)
        {
            MapLayout currentLayout = PGMEBackend.Program.currentLayout;
            currentLayout.globalTilesetPointer = int.Parse(hexNumberBoxHeaderTabGlobalTilesetPointer.Text, NumberStyles.HexNumber);
            currentLayout.LoadRawFromLayoutHeader();
            hexViewerRawLayoutHeader.ByteProvider = new DynamicByteProvider(currentLayout.rawData, true, false, false);
        }

        private void hexNumberBoxHeaderTabLocalTilesetPointer_Validated(object sender, EventArgs e)
        {
            MapLayout currentLayout = PGMEBackend.Program.currentLayout;
            currentLayout.localTilesetPointer = int.Parse(hexNumberBoxHeaderTabLocalTilesetPointer.Text, NumberStyles.HexNumber);
            currentLayout.LoadRawFromLayoutHeader();
            hexViewerRawLayoutHeader.ByteProvider = new DynamicByteProvider(currentLayout.rawData, true, false, false);
        }

        private void tbHeaderTabMapWidth_Validated(object sender, EventArgs e)
        {
            MapLayout currentLayout = PGMEBackend.Program.currentLayout;
            currentLayout.layoutWidth = int.Parse(tbHeaderTabMapWidth.Text);
            currentLayout.LoadRawFromLayoutHeader();
            hexViewerRawLayoutHeader.ByteProvider = new DynamicByteProvider(currentLayout.rawData, true, false, false);
        }

        private void tbHeaderTabMapHeight_Validated(object sender, EventArgs e)
        {
            MapLayout currentLayout = PGMEBackend.Program.currentLayout;
            currentLayout.layoutHeight = int.Parse(tbHeaderTabMapHeight.Text);
            currentLayout.LoadRawFromLayoutHeader();
            hexViewerRawLayoutHeader.ByteProvider = new DynamicByteProvider(currentLayout.rawData, true, false, false);
        }

        private void tbHeaderTabBorderWidth_Validated(object sender, EventArgs e)
        {
            MapLayout currentLayout = PGMEBackend.Program.currentLayout;
            currentLayout.borderWidth = int.Parse(tbHeaderTabBorderWidth.Text);
            currentLayout.LoadRawFromLayoutHeader();
            hexViewerRawLayoutHeader.ByteProvider = new DynamicByteProvider(currentLayout.rawData, true, false, false);
        }

        private void tbHeaderTabBorderHeight_Validated(object sender, EventArgs e)
        {
            MapLayout currentLayout = PGMEBackend.Program.currentLayout;
            currentLayout.borderHeight = int.Parse(tbHeaderTabBorderHeight.Text);
            currentLayout.LoadRawFromLayoutHeader();
            hexViewerRawLayoutHeader.ByteProvider = new DynamicByteProvider(currentLayout.rawData, true, false, false);
        }

        private void glControlMapEditor_Load(object sender, EventArgs e)
        {
            glControlMapEditor.MakeCurrent();
            PGMEBackend.Program.glMapEditor = new PGMEBackend.GLControls.GLMapEditor(glControlMapEditor.Width, glControlMapEditor.Height);
        }
        
        private void glControlBlocks_Load(object sender, EventArgs e)
        {
            glControlBlocks.MakeCurrent();
            PGMEBackend.Program.glBlockChooser = new PGMEBackend.GLControls.GLBlockChooser(glControlBlocks.Width, glControlBlocks.Height);
        }

        private void glControlMapEditor_Paint(object sender, PaintEventArgs e)
        {
            if (!PGMEBackend.Program.glMapEditor) // Play nice
                return;
            
            glControlMapEditor.MakeCurrent();
            PGMEBackend.Program.glMapEditor.Paint(glControlMapEditor.Width, glControlMapEditor.Height);
            glControlMapEditor.SwapBuffers();
        }
        
        private void glControlBlocks_Paint(object sender, PaintEventArgs e)
        {
            if (!PGMEBackend.Program.glBlockChooser) // Play nice
                return;
            
            glControlBlocks.MakeCurrent();
            PGMEBackend.Program.glBlockChooser.Paint(glControlBlocks.Width, glControlBlocks.Height);
            glControlBlocks.SwapBuffers();
        }
        
        public void SetGLMapEditorSize(int w, int h)
        {
            glControlMapEditor.Width = w;
            glControlMapEditor.Height = h;
        }

        public void SetGLBlockChooserSize(int w, int h)
        {
            glControlBlocks.Width = w;
            glControlBlocks.Height = h;
        }

        public void SetGLBorderBlocksSize(int w, int h)
        {
            int totalHeight = borderBlocksBox.Height + paintTabControl.Height;
            glControlBorderBlocks.Width = w;
            glControlBorderBlocks.Height = h;
            glControlBorderBlocks.Location = new Point(78 - (w /2), glControlBorderBlocks.Location.Y);
            borderBlocksBox.Size = new Size(borderBlocksBox.Size.Width, 24 + h);
            paintTabControl.Location = new Point(paintTabControl.Location.X, 30 + h);
            paintTabControl.Size = new Size(paintTabControl.Size.Width, totalHeight - borderBlocksBox.Height);
        }

        public void SetGLEntityEditorSize(int w, int h)
        {
            glControlEntityEditor.Width = w;
            glControlEntityEditor.Height = h;
        }

        private void glControlMapEditor_MouseMove(object sender, MouseEventArgs e)
        {
            int oldX = PGMEBackend.Program.glMapEditor.mouseX;
            int oldY = PGMEBackend.Program.glMapEditor.mouseY;

            PGMEBackend.Program.glMapEditor.MouseMove(e.X, e.Y);

            if ((oldX != PGMEBackend.Program.glMapEditor.mouseX) || (oldY != PGMEBackend.Program.glMapEditor.mouseY))
            {
                mapXPosLabel.Text = "X: " + settings.HexPrefix + PGMEBackend.Program.glMapEditor.mouseX.ToString("X2");
                mapYPosLabel.Text = "Y: " + settings.HexPrefix + PGMEBackend.Program.glMapEditor.mouseY.ToString("X2");
                RefreshMapEditorControl();
            }
        }

        private void glControlBlocks_MouseMove(object sender, MouseEventArgs e)
        {
            int oldX = PGMEBackend.Program.glBlockChooser.mouseX;
            int oldY = PGMEBackend.Program.glBlockChooser.mouseY;

            PGMEBackend.Program.glBlockChooser.MouseMove(e.X, e.Y);

            if ((oldX != PGMEBackend.Program.glBlockChooser.mouseX) || (oldY != PGMEBackend.Program.glBlockChooser.mouseY))
                RefreshBlockEditorControl();
        }

        private void glControlEntityEditor_MouseMove(object sender, MouseEventArgs e)
        {
            int oldX = PGMEBackend.Program.glEntityEditor.mouseX;
            int oldY = PGMEBackend.Program.glEntityEditor.mouseY;

            PGMEBackend.Program.glEntityEditor.MouseMove(e.X, e.Y);

            if ((oldX != PGMEBackend.Program.glEntityEditor.mouseX) || (oldY != PGMEBackend.Program.glEntityEditor.mouseY))
            {
                if (PGMEBackend.Program.glEntityEditor.mouseX >= 0)
                {
                    eventXPosLabel.Width = 43;
                    eventXPosLabel.Text = "X: " + settings.HexPrefix + PGMEBackend.Program.glEntityEditor.mouseX.ToString("X2");
                }
                else
                {
                    eventXPosLabel.Width = 48;
                    eventXPosLabel.Text = "X: -" + settings.HexPrefix + Math.Abs(PGMEBackend.Program.glEntityEditor.mouseX).ToString("X2");
                }
                if (PGMEBackend.Program.glEntityEditor.mouseY >= 0)
                {
                    eventYPosLabel.Width = 43;
                    eventYPosLabel.Text = "Y: " + settings.HexPrefix + PGMEBackend.Program.glEntityEditor.mouseY.ToString("X2");
                }
                else
                {
                    eventYPosLabel.Width = 48;
                    eventYPosLabel.Text = "Y: -" + settings.HexPrefix + Math.Abs(PGMEBackend.Program.glEntityEditor.mouseY).ToString("X2");
                }
                RefreshEntityEditorControl();
            }
        }

        private void glControlMapEditor_MouseLeave(object sender, EventArgs e)
        {
            PGMEBackend.Program.glMapEditor.MouseLeave();
            RefreshMapEditorControl();
        }
        
        private void glControlBlocks_MouseLeave(object sender, EventArgs e)
        {
            PGMEBackend.Program.glBlockChooser.MouseLeave();
            RefreshBlockEditorControl();
        }

        private void glControlEntityEditor_MouseLeave(object sender, EventArgs e)
        {
            PGMEBackend.Program.glEntityEditor.MouseLeave();
            RefreshEntityEditorControl();
        }

        private void glControlMapEditor_MouseDown(object sender, MouseEventArgs e)
        {
            MapEditorTools tool = GetMapEditorTool(e.Button);
            if (tool == PGMEBackend.Program.glMapEditor.tool)
                return;
            PGMEBackend.Program.glMapEditor.MouseDown(tool);
            RefreshMapEditorControl();
        }

        private void glControlBlocks_MouseDown(object sender, MouseEventArgs e)
        {
            MapEditorTools tool = GetMapEditorTool(e.Button);
            if (tool == PGMEBackend.Program.glMapEditor.tool)
                return;
            PGMEBackend.Program.glBlockChooser.MouseDown(tool);
            RefreshBlockEditorControl();
        }

        private void glControlEntityEditor_MouseDown(object sender, MouseEventArgs e)
        {
            EntityEditorTools tool = GetEntityEditorTool(e.Button);
            if (tool == PGMEBackend.Program.glEntityEditor.tool)
                return;
            PGMEBackend.Program.glEntityEditor.MouseDown(tool);
            RefreshEntityEditorControl();
        }
        
        private void glControlEntityEditor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PGMEBackend.Program.glEntityEditor.MouseDoubleClick();
        }

        private void glControlMapEditor_MouseUp(object sender, MouseEventArgs e)
        {
            PGMEBackend.Program.glMapEditor.MouseUp(GetMapEditorTool(e.Button));
            RefreshMapEditorControl();
        }

        private void glControlBlocks_MouseUp(object sender, MouseEventArgs e)
        {
            PGMEBackend.Program.glBlockChooser.MouseUp(GetMapEditorTool(e.Button));
            RefreshBlockEditorControl();
        }

        private void glControlEntityEditor_MouseUp(object sender, MouseEventArgs e)
        {
            PGMEBackend.Program.glEntityEditor.MouseUp(GetEntityEditorTool(e.Button));
            RefreshEntityEditorControl();
        }

        private MapEditorTools GetMapEditorTool(MouseButtons b)
        {
            if ((tsbMapEditorMouse.Checked && b == MouseButtons.Left) || tsbMapEditorPencil.Checked)
                return MapEditorTools.Pencil;
            else if((tsbMapEditorMouse.Checked && b == MouseButtons.Right) || tsbMapEditorEyedropper.Checked)
                return MapEditorTools.Eyedropper;
            else if ((tsbMapEditorMouse.Checked && b == MouseButtons.Middle && isControlPressed) || tsbMapEditorFillAll.Checked)
                return MapEditorTools.FillAll;
            else if((tsbMapEditorMouse.Checked && b == MouseButtons.Middle) || tsbMapEditorFill.Checked)
                return MapEditorTools.Fill;
            else
                return MapEditorTools.None;
        }

        private EntityEditorTools GetEntityEditorTool(MouseButtons b)
        {
            if ((tsbMapEditorMouse.Checked && b == MouseButtons.Left && isControlPressed) || tsbMapEditorFillAll.Checked)
                return EntityEditorTools.MultiSelect;
            else if ((tsbMapEditorMouse.Checked && b == MouseButtons.Left) || tsbMapEditorPencil.Checked)
                return EntityEditorTools.Move;
            else if ((tsbMapEditorMouse.Checked && b == MouseButtons.Right) || tsbMapEditorEyedropper.Checked)
                return EntityEditorTools.RectSelect;
            else
                return EntityEditorTools.None;
        }

        private void glControlMapEditor_MouseEnter(object sender, EventArgs e)
        {

        }
        
        private void glControlBlocks_MouseEnter(object sender, EventArgs e)
        {

        }

        private void glControlEntityEditor_MouseEnter(object sender, EventArgs e)
        {

        }

        public void RefreshMapEditorControl()
        {
            glControlMapEditor.Invalidate();
        }

        public void RefreshBlockEditorControl()
        {
            glControlBlocks.Invalidate();
        }

        public void RefreshPermsChooserControl()
        {
            glControlPermsChooser.Invalidate();
        }

        public void RefreshBorderBlocksControl()
        {
            glControlBorderBlocks.Invalidate();
        }

        public void RefreshEntityEditorControl()
        {
            glControlEntityEditor.Invalidate();
        }

        private void panel8_Scroll(object sender, ScrollEventArgs e)
        {
            RefreshMapEditorControl();
        }

        public void ScrollBlockChooserToBlock(int blockNum)
        {
            using (Control c = new Control() { Parent = blockPaintPanel, Height = 16, Top = (blockNum / 8) * 16 + blockPaintPanel.AutoScrollPosition.Y })
            {
                blockPaintPanel.ScrollControlIntoView(c);
            }
        }

        public void ScrollPermChooserToPerm(int permNum)
        {
            using (Control c = new Control() { Parent = movementPaintPanel, Height = 16, Top = (permNum / 4) * 16 + movementPaintPanel.AutoScrollPosition.Y })
            {
                movementPaintPanel.ScrollControlIntoView(c);
            }
        }

        private void blockPaintPanel_Scroll(object sender, ScrollEventArgs e)
        {
            RefreshBlockEditorControl();
        }

        private void toolStripShowGrid_Click(object sender, EventArgs e)
        {
            ChangeGridState();
        }

        private void showGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeGridState();
        }

        private void toolStripSaveMap_Click(object sender, EventArgs e)
        {
            PGMEBackend.Program.SaveMap();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PGMEBackend.Program.SaveMap();
        }

        private void cboTimeofDayMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            PGMEBackend.Program.timeOfDay = (sender as ToolStripComboBox).SelectedIndex;
        }

        private void mainTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool oldValue = PGMEBackend.Program.showingPerms;
            if (mainTabControl.SelectedIndex == 0)
            {
                if (paintTabControl.SelectedIndex == 1)
                    PGMEBackend.Program.showingPerms = true;
                else
                    PGMEBackend.Program.showingPerms = false;
                if (!editorTabLoaded)
                    LoadEditorTab((PGMEBackend.Program.currentMap != null) ? PGMEBackend.Program.currentMap : (object)PGMEBackend.Program.currentLayout);
            }
            else
            {
                PGMEBackend.Program.showingPerms = false;
                switch(mainTabControl.SelectedIndex)
                {
                    case 1:
                        if (!entityTabLoaded)
                            LoadEntityTab((PGMEBackend.Program.currentMap != null) ? PGMEBackend.Program.currentMap : (object)PGMEBackend.Program.currentLayout);
                        break;
                    case 3:
                        if(!headerTabLoaded)
                            LoadHeaderTab((PGMEBackend.Program.currentMap != null) ? PGMEBackend.Program.currentMap : (object)PGMEBackend.Program.currentLayout);
                        break;
                }
            }
            if(oldValue != PGMEBackend.Program.showingPerms)
                PGMEBackend.Program.glMapEditor.RedrawAllChunks();
        }

        private void toolStripEventsShowGrid_Click(object sender, EventArgs e)
        {
            ChangeGridState();
        }

        private void ChangeGridState()
        {
            toolStripShowGrid.Checked = !toolStripShowGrid.Checked;
            toolStripEventsShowGrid.Checked = toolStripShowGrid.Checked;
            showGridToolStripMenuItem.Checked = toolStripShowGrid.Checked;
            settings.ShowGrid = toolStripShowGrid.Checked;
            WriteConfig();
            if (mainTabControl.SelectedIndex == 1)
                RefreshEntityEditorControl();
            else
            {
                RefreshMapEditorControl();
                RefreshBlockEditorControl();
                RefreshBorderBlocksControl();
            }
        }
        
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoManager.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoManager.Redo();
        }

        private void tsbMapEditorMouse_Click(object sender, EventArgs e)
        {
            SetCheckedMapTool("None");
        }

        private void tsbMapEditorPencil_Click(object sender, EventArgs e)
        {
            SetCheckedMapTool("Pencil");
        }

        private void tsbMapEditorEyedropper_Click(object sender, EventArgs e)
        {
            SetCheckedMapTool("Eyedropper");
        }

        private void tsbMapEditorFill_Click(object sender, EventArgs e)
        {
            SetCheckedMapTool("Fill");
        }

        private void tsbMapEditorFillAll_Click(object sender, EventArgs e)
        {
            SetCheckedMapTool("FillAll");
        }
        
        private void SetCheckedMapTool(string tool)
        {
            foreach (ToolStripItem item in tsMapEditorTab.Items)
            {
                if (item.Tag != null && item is ToolStripButton)
                {
                    if (item.Tag.Equals(tool))
                        (item as ToolStripButton).Checked = true;
                    else
                        (item as ToolStripButton).Checked = false;
                }
            }
        }
        
        private void glControlPermsChooser_Load(object sender, EventArgs e)
        {
            glControlPermsChooser.MakeCurrent();
            PGMEBackend.Program.glPermsChooser = new PGMEBackend.GLControls.GLPermsChooser(glControlPermsChooser.Width, glControlPermsChooser.Height);
        }

        private void glControlPermsChooser_Paint(object sender, PaintEventArgs e)
        {
            if (!PGMEBackend.Program.glPermsChooser) // Play nice
                return;

            glControlPermsChooser.MakeCurrent();
            PGMEBackend.Program.glPermsChooser.Paint(glControlPermsChooser.Width, glControlPermsChooser.Height);
            glControlPermsChooser.SwapBuffers();
        }

        private void glControlBorderBlocks_Load(object sender, EventArgs e)
        {
            glControlBorderBlocks.MakeCurrent();
            PGMEBackend.Program.glBorderBlocks = new PGMEBackend.GLControls.GLBorderBlocks(glControlBorderBlocks.Width, glControlBorderBlocks.Height);
        }

        private void glControlBorderBlocks_Paint(object sender, PaintEventArgs e)
        {
            if (!PGMEBackend.Program.glBorderBlocks) // Play nice
                return;

            glControlBorderBlocks.MakeCurrent();
            PGMEBackend.Program.glBorderBlocks.Paint(glControlBorderBlocks.Width, glControlBorderBlocks.Height);
            glControlBorderBlocks.SwapBuffers();
        }

        private void glControlEntityEditor_Load(object sender, EventArgs e)
        {
            glControlEntityEditor.MakeCurrent();
            PGMEBackend.Program.glEntityEditor = new PGMEBackend.GLControls.GLEntityEditor(glControlEntityEditor.Width, glControlEntityEditor.Height);
        }

        private void glControlEntityEditor_Paint(object sender, PaintEventArgs e)
        {
            if (!PGMEBackend.Program.glEntityEditor) // Play nice
                return;

            glControlEntityEditor.MakeCurrent();
            PGMEBackend.Program.glEntityEditor.Paint(glControlEntityEditor.Width, glControlEntityEditor.Height);
            glControlEntityEditor.SwapBuffers();
        }

        private void glControlPermsChooser_MouseDown(object sender, MouseEventArgs e)
        {
            MapEditorTools tool = MapEditorTools.None;
            if (e.Button != MouseButtons.None)
                tool = MapEditorTools.Pencil;
            if (tool == PGMEBackend.Program.glMapEditor.tool)
                return;
            PGMEBackend.Program.glPermsChooser.MouseDown(tool);
            RefreshPermsChooserControl();
        }

        private void glControlPermsChooser_MouseEnter(object sender, EventArgs e)
        {

        }

        private void glControlPermsChooser_MouseLeave(object sender, EventArgs e)
        {
            PGMEBackend.Program.glPermsChooser.MouseLeave();
            RefreshPermsChooserControl();
        }

        private void glControlPermsChooser_MouseMove(object sender, MouseEventArgs e)
        {
            int oldX = PGMEBackend.Program.glPermsChooser.mouseX;
            int oldY = PGMEBackend.Program.glPermsChooser.mouseY;

            PGMEBackend.Program.glPermsChooser.MouseMove(e.X, e.Y);

            if ((oldX != PGMEBackend.Program.glPermsChooser.mouseX) || (oldY != PGMEBackend.Program.glPermsChooser.mouseY))
                RefreshPermsChooserControl();
        }

        private void glControlPermsChooser_MouseUp(object sender, MouseEventArgs e)
        {
            MapEditorTools tool = MapEditorTools.None;
            if (e.Button != MouseButtons.None)
                tool = MapEditorTools.Pencil;
            PGMEBackend.Program.glPermsChooser.MouseUp(tool);
            RefreshPermsChooserControl();
        }

        private void glControlBorderBlocks_KeyDown(object sender, KeyEventArgs e)
        {
            isControlPressed = e.Control;
        }

        private void glControlBorderBlocks_KeyUp(object sender, KeyEventArgs e)
        {
            isControlPressed = e.Control;
        }

        private void glControlBorderBlocks_MouseDown(object sender, MouseEventArgs e)
        {
            MapEditorTools tool = GetMapEditorTool(e.Button);
            if (tool == PGMEBackend.Program.glBorderBlocks.tool)
                return;
            PGMEBackend.Program.glBorderBlocks.MouseDown(tool);
            RefreshBorderBlocksControl();
        }

        private void glControlBorderBlocks_MouseEnter(object sender, EventArgs e)
        {

        }

        private void glControlBorderBlocks_MouseLeave(object sender, EventArgs e)
        {
            PGMEBackend.Program.glBorderBlocks.MouseLeave();
            RefreshBorderBlocksControl();
        }

        private void glControlBorderBlocks_MouseMove(object sender, MouseEventArgs e)
        {
            int oldX = PGMEBackend.Program.glBorderBlocks.mouseX;
            int oldY = PGMEBackend.Program.glBorderBlocks.mouseY;

            PGMEBackend.Program.glBorderBlocks.MouseMove(e.X, e.Y);

            if ((oldX != PGMEBackend.Program.glBorderBlocks.mouseX) || (oldY != PGMEBackend.Program.glBorderBlocks.mouseY))
                RefreshBorderBlocksControl();
        }

        private void glControlBorderBlocks_MouseUp(object sender, MouseEventArgs e)
        {
            PGMEBackend.Program.glBorderBlocks.MouseUp(GetMapEditorTool(e.Button));
            RefreshBorderBlocksControl();
        }

        private void paintTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            PGMEBackend.Program.ChangePermsVisibility((sender as TabControl).SelectedIndex == 1);
            RefreshMapEditorControl();
        }

        bool isControlPressed = false;

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            isControlPressed = e.Control;
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            isControlPressed = e.Control;
        }

        public int permTransPreview = -1;

        public int PermTransPreviewValue()
        {
            return permTransPreview;
        }

        private void tsmiSettings_Click(object sender, EventArgs e)
        {
            OpenSettingsWindow();
        }

        public void OpenSettingsWindow()
        {
            SettingsDialog permTransDialog = new SettingsDialog(this);
            DialogResult result = permTransDialog.ShowDialog();
            if (result != DialogResult.OK && PGMEBackend.Program.currentLayout != null)
            {
                PGMEBackend.Program.glMapEditor.RedrawAllChunks();
                RefreshMapEditorControl();
            }
            permTransPreview = -1;
        }

        public void PreviewPermTranslucency(int value)
        {
            permTransPreview = value;
            if (PGMEBackend.Program.currentLayout != null)
            {
                PGMEBackend.Program.glMapEditor.RedrawAllChunks();
                RefreshMapEditorControl();
            }
        }

        private void tsMapFilter_TextChanged(object sender, EventArgs e)
        {
            ClearMapNodes();
            LoadMapNodes();
        }

        private void mainTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {

        }
        
        private void btnCreateNewEntity_Click(object sender, EventArgs e)
        {
            switch(cboEventTypes.SelectedIndex)
            {
                default:
                    panelSpriteEvent.Visible = true;
                    nudEntityNum.Enabled = true;
                    Array.Resize(ref PGMEBackend.Program.currentMap.NPCs, PGMEBackend.Program.currentMap.NPCs.Length + 1);
                    PGMEBackend.Program.currentMap.NPCs[PGMEBackend.Program.currentMap.NPCs.Length - 1] = new NPC();
                    SetEntityNumValues(PGMEBackend.Program.currentMap.NPCs.Length - 1, PGMEBackend.Program.currentMap.NPCs.Length - 1);
                    break;
                case 1:
                    panelWarpEvent.Visible = true;
                    nudEntityNum.Enabled = true;
                    Array.Resize(ref PGMEBackend.Program.currentMap.Warps, PGMEBackend.Program.currentMap.Warps.Length + 1);
                    PGMEBackend.Program.currentMap.Warps[PGMEBackend.Program.currentMap.Warps.Length - 1] = new Warp();
                    SetEntityNumValues(PGMEBackend.Program.currentMap.Warps.Length - 1, PGMEBackend.Program.currentMap.Warps.Length - 1);
                    break;
                case 2:
                    panelScriptEvent.Visible = true;
                    nudEntityNum.Enabled = true;
                    Array.Resize(ref PGMEBackend.Program.currentMap.Triggers, PGMEBackend.Program.currentMap.Triggers.Length + 1);
                    PGMEBackend.Program.currentMap.Triggers[PGMEBackend.Program.currentMap.Triggers.Length - 1] = new Trigger();
                    SetEntityNumValues(PGMEBackend.Program.currentMap.Triggers.Length - 1, PGMEBackend.Program.currentMap.Triggers.Length - 1);
                    break;
                case 3:
                    panelSignEvent.Visible = true;
                    nudEntityNum.Enabled = true;
                    Array.Resize(ref PGMEBackend.Program.currentMap.Signs, PGMEBackend.Program.currentMap.Signs.Length + 1);
                    PGMEBackend.Program.currentMap.Signs[PGMEBackend.Program.currentMap.Signs.Length - 1] = new Sign();
                    SetEntityNumValues(PGMEBackend.Program.currentMap.Signs.Length - 1, PGMEBackend.Program.currentMap.Signs.Length - 1);
                    break;
            }
        }

        public void LoadEntityView(int entityType, int entityNum)
        {
            cboEventTypes.SelectedIndex = entityType;
            nudEntityNum.Value = entityNum;
            nudEntityNum.Enabled = true;
            switch (entityType)
            {
                default:
                    panelSpriteEvent.Visible = true;
                    break;
                case 1:
                    panelWarpEvent.Visible = true;
                    break;
                case 2:
                    panelScriptEvent.Visible = true;
                    break;
                case 3:
                    panelSignEvent.Visible = true;
                    break;
            }
        }

        public void LoadEntityView(Entity entity)
        {
            if (entity is NPC)
                LoadNPCView(entity as NPC);
            else if (entity is Warp)
                LoadWarpView(entity as Warp);
            else if (entity is Trigger)
                LoadTriggerView(entity as Trigger);
            else if (entity is Sign)
                LoadSignView(entity as Sign);
        }

        private void btnDeleteNPC_Click(object sender, EventArgs e)
        {
            if(ShowMessageBox(PGMEBackend.Program.rmInternalStrings.GetString("DeleteEntity"), PGMEBackend.Program.rmInternalStrings.GetString("DeleteEntityTitle"), "YesNo", "Warning") == "Yes")
            {
                NPC.DeleteNPC(NPC.currentNPC);
                SetEntityNumValues(NPC.currentNPC, PGMEBackend.Program.currentMap.NPCs.Length - 1);
                LoadNPCView(PGMEBackend.Program.currentMap, NPC.currentNPC);
            }
        }

        private void btnDeleteWarp_Click(object sender, EventArgs e)
        {
            if (ShowMessageBox(PGMEBackend.Program.rmInternalStrings.GetString("DeleteEntity"), PGMEBackend.Program.rmInternalStrings.GetString("DeleteEntityTitle"), "YesNo", "Warning") == "Yes")
            {
                Warp.DeleteWarp(Warp.currentWarp);
                SetEntityNumValues(Warp.currentWarp, PGMEBackend.Program.currentMap.Warps.Length - 1);
                LoadWarpView(PGMEBackend.Program.currentMap, Warp.currentWarp);
            }
        }

        private void btnDeleteTrigger_Click(object sender, EventArgs e)
        {
            if (ShowMessageBox(PGMEBackend.Program.rmInternalStrings.GetString("DeleteEntity"), PGMEBackend.Program.rmInternalStrings.GetString("DeleteEntityTitle"), "YesNo", "Warning") == "Yes")
            {
                Trigger.DeleteTrigger(Trigger.currentTrigger);
                SetEntityNumValues(Trigger.currentTrigger, PGMEBackend.Program.currentMap.Triggers.Length - 1);
                LoadTriggerView(PGMEBackend.Program.currentMap, Trigger.currentTrigger);
            }
        }

        private void btnDeleteSign_Click(object sender, EventArgs e)
        {
            if (ShowMessageBox(PGMEBackend.Program.rmInternalStrings.GetString("DeleteEntity"), PGMEBackend.Program.rmInternalStrings.GetString("DeleteEntityTitle"), "YesNo", "Warning") == "Yes")
            {
                Sign.DeleteSign(Sign.currentSign);
                SetEntityNumValues(Sign.currentSign, PGMEBackend.Program.currentMap.Signs.Length - 1);
                LoadSignView(PGMEBackend.Program.currentMap, Sign.currentSign);
            }
        }
        
        public void MultipleEntitiesSelected()
        {
            labelEntityDataPanel.Text = PGMEBackend.Program.rmInternalStrings.GetString("MultipleEntitiesSelected");
            HideEventEditors();
        }

        public void NoEntitiesOfType()
        {
            labelEntityDataPanel.Text = PGMEBackend.Program.rmInternalStrings.GetString("NoEntitiesOfThisType");
            HideEventEditors();
        }

        private void HideEventEditors()
        {
            nudEntityNum.Enabled = false;
            panelSpriteEvent.Visible = false;
            panelWarpEvent.Visible = false;
            panelScriptEvent.Visible = false;
            panelSignEvent.Visible = false;
        }

        public void FollowWarp(Warp warp)
        {
            FollowWarp(warp.destMapBank, warp.destMapNum, warp.destWarpNum);
        }

        public void FollowWarp(int mapBank, int mapNum, int warpNum)
        {
            Map destMap = PGMEBackend.Program.mapBanks[mapBank].Bank[mapNum];
            if (destMap != null)
            {
                TreeNode itemNode = null;
                foreach (TreeNode node in mapListTreeView.Nodes)
                {
                    itemNode = GetNodeFromTag(destMap, node);
                    if (itemNode != null)
                    {
                        LoadMapFromNode(itemNode);
                        break;
                    }
                }
            }
            cboEventTypes.SelectedIndex = 1;
            nudEntityNum.Value = warpNum;
            //LoadMap((entity as Warp).destMapBank, (entity as Warp).destMapNum);
        }

        private void btnTravelToWarpDest_Click(object sender, EventArgs e)
        {
            FollowWarp(PGMEBackend.Program.glEntityEditor.currentEntity[0] as Warp);
        }

        public int LaunchScriptEditor(int scriptOffset)
        {
            if(string.IsNullOrEmpty(settings.ScriptEditor))
            {
                string result = ShowMessageBox(PGMEBackend.Program.rmInternalStrings.GetString("NoScriptEditorSpecified"), PGMEBackend.Program.rmInternalStrings.GetString("NoScriptEditorSpecifiedTitle"), "YesNo");
                if (result == "No")
                    return -1;
                OpenSettingsWindow();
                return 1;
            }
            if (scriptOffset < 0 || scriptOffset >= 0x2000000)
                scriptOffset = 0;
            if (System.Diagnostics.Process.Start(settings.ScriptEditor, PGMEBackend.Program.ROM.ROMPath + scriptOffset.ToString("X8")) != null)
                return 0;
            else
                return -1;
        }

        private void btnNPCOpenScript_Click(object sender, EventArgs e)
        {
            LaunchScriptEditor(PGMEBackend.Program.glEntityEditor.currentEntity[0].scriptOffset);
        }

        private void btnSignOpenScript_Click(object sender, EventArgs e)
        {
            LaunchScriptEditor(PGMEBackend.Program.glEntityEditor.currentEntity[0].scriptOffset);
        }

        private void btnTriggerOpenScript_Click(object sender, EventArgs e)
        {
            LaunchScriptEditor(PGMEBackend.Program.glEntityEditor.currentEntity[0].scriptOffset);
        }

        /*
        // Undo example usage
        // Note that the redo gets called automatically
        private void thisIsATestOfTheUndoSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UndoManager.PushAction(new PGMEBackend.Undo.UndoSetProperty(this, "Text", DateTime.Now.ToString()));
            
            var oldTitle = Text;
            UndoManager.PushAction(() =>
            {
                Text = "fuck this shit";
            }, () =>
            {
                Text = oldTitle;
            });

        }
        */

    }

    class GLPanel : Panel
    {
        protected override Point ScrollToControl(Control activeControl)
        {
            if (activeControl is OpenTK.GLControl)
                return this.AutoScrollPosition;
            else
                return base.ScrollToControl(activeControl);
        }
    }
}
