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

namespace PGMEWindowsUI
{
    public partial class MainWindow : Form, UIInteractionLayer
    {
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
            switch (cboEventTypes.SelectedIndex)
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

        private void HideEventEditors()
        {
            panelSpriteEvent.Visible = false;
            panelWarpEvent.Visible = false;
            panelScriptEvent.Visible = false;
            panelSignEvent.Visible = false;
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
                        if (mapTreeNodes.ContainsKey(mapLayout.Value.globalTilesetPointer))
                        {
                            if (mapTreeNodes[mapLayout.Value.globalTilesetPointer].Nodes.Count == 0)
                            {
                                node = mapTreeNodes[mapLayout.Value.globalTilesetPointer].Nodes.Add("mapNode" + i++, mapLayout.Value.name);
                                node.Tag = mapLayout.Value;
                                mapTreeNodes.Add(mapLayout.Key, node);
                                mapTreeNodes[mapLayout.Value.globalTilesetPointer].SelectedImageKey = "Map Folder Closed";
                                mapTreeNodes[mapLayout.Value.globalTilesetPointer].ImageKey = "Map Folder Closed";
                            }
                        }
                        if (mapTreeNodes.ContainsKey(mapLayout.Value.localTilesetPointer))
                        {
                            if (mapTreeNodes[mapLayout.Value.localTilesetPointer].Nodes.Count == 0)
                            {
                                node = mapTreeNodes[mapLayout.Value.localTilesetPointer].Nodes.Add("mapNode" + i++, mapLayout.Value.name);
                                node.Tag = mapLayout.Value;
                                mapTreeNodes.Add(mapLayout.Key, node);
                                mapTreeNodes[mapLayout.Value.localTilesetPointer].SelectedImageKey = "Map Folder Closed";
                                mapTreeNodes[mapLayout.Value.localTilesetPointer].ImageKey = "Map Folder Closed";
                            }
                        }
                    }
                    break;
            }
            CopyTreeNodes(backupTree, mapListTreeView);
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
            }
        }
        
        public void LoadMap(object map)
        {
            LoadEditorTab(map);
            LoadHeaderTab(map);
        }

        public void LoadEditorTab(object maybeaMap)
        {
            MapLayout mapLayout = PGMEBackend.Program.currentLayout;
            if (mapLayout == null)
                return;

            if(mapLayout.globalTileset != null)
                mapLayout.globalTileset.Initialize();
            if (mapLayout.localTileset != null)
                mapLayout.localTileset.Initialize();

            RefreshMapEditorControl();
            RefreshBlockEditorControl();
            RefreshBorderBlocksControl();
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
        }

        private void LoadHeaderTabMapHeader(Map map)
        {
            hexViewerRawMapHeader.ByteProvider = new DynamicByteProvider(map.rawData, true, false, false);
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
            currentMap.LoadMapHeaderFromRaw();
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
            glControlBorderBlocks.Width = w;
            glControlBorderBlocks.Height = h;
            glControlBorderBlocks.Location = new Point(78 - (w /2), glControlBorderBlocks.Location.Y);
            borderBlocksBox.Size = new Size(borderBlocksBox.Size.Width, 24 + h);
            paintTabControl.Location = new Point(paintTabControl.Location.X, 30 + h);
            paintTabControl.Size = new Size(paintTabControl.Size.Width, 550 - h);
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
                eventXPosLabel.Text = "X: " + settings.HexPrefix + PGMEBackend.Program.glMapEditor.mouseX.ToString("X2");
                eventYPosLabel.Text = "Y: " + settings.HexPrefix + PGMEBackend.Program.glMapEditor.mouseY.ToString("X2");
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
        
        private void glControlMapEditor_MouseDown(object sender, MouseEventArgs e)
        {
            MapEditorTools tool = GetTool(e.Button);
            if (tool == PGMEBackend.Program.glMapEditor.tool)
                return;
            PGMEBackend.Program.glMapEditor.MouseDown(tool);
            RefreshMapEditorControl();
        }

        private void glControlBlocks_MouseDown(object sender, MouseEventArgs e)
        {
            MapEditorTools tool = GetTool(e.Button);
            if (tool == PGMEBackend.Program.glMapEditor.tool)
                return;
            PGMEBackend.Program.glBlockChooser.MouseDown(tool);
            RefreshBlockEditorControl();
        }

        private void glControlMapEditor_MouseUp(object sender, MouseEventArgs e)
        {
            PGMEBackend.Program.glMapEditor.MouseUp(GetTool(e.Button));
            RefreshMapEditorControl();
        }

        private void glControlBlocks_MouseUp(object sender, MouseEventArgs e)
        {
            PGMEBackend.Program.glBlockChooser.MouseUp(GetTool(e.Button));
            RefreshBlockEditorControl();
        }

        private MapEditorTools GetTool(MouseButtons b)
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

        private void glControlMapEditor_MouseEnter(object sender, EventArgs e)
        {

        }
        
        private void glControlBlocks_MouseEnter(object sender, EventArgs e)
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
            }
            else
                PGMEBackend.Program.showingPerms = false;
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
            RefreshMapEditorControl();
            RefreshBlockEditorControl();
            RefreshBorderBlocksControl();
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
            MapEditorTools tool = GetTool(e.Button);
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
            PGMEBackend.Program.glBorderBlocks.MouseUp(GetTool(e.Button));
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
            SettingsDialog permTransDialog = new SettingsDialog(this);
            DialogResult result = permTransDialog.ShowDialog();
            if(result != DialogResult.OK && PGMEBackend.Program.currentLayout != null)
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
