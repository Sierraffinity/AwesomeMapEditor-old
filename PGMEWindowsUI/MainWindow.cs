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
            cbHeaderTabShowRawMapHeader.Checked = settings.ShowRawMapHeader;
            cbHeaderTabShowRawLayoutHeader.Checked = settings.ShowRawLayoutHeader;
            gbHeaderTabRawMapHeader.Visible = cbHeaderTabShowRawMapHeader.Checked;
            gbHeaderTabRawLayoutHeader.Visible = cbHeaderTabShowRawLayoutHeader.Checked;

            mapListTreeView.ImageList = imageListMapTree;
            mainTabControl.ImageList = imageListTabControl;
            for (int i = 0; i < mainTabControl.TabPages.Count; i++)
                mainTabControl.TabPages[i].ImageIndex = i;
            PGMEBackend.Program.SetMainGUITitle(this.Text);
            SetInitialCheckedStuff();
            SetMapSortOrder(settings.MapSortOrder);
            mapTreeNodes = new Dictionary<int, TreeNode>();
        }

        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

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

        private void SetInitialCheckedStuff()
        {
            SetCheckedLanguage(settings.Language);
            createOnOpenToolStripMenuItem.Checked = settings.CreateBackups;
        }

        private void SetCheckedLanguage(string lang)
        {
            foreach (ToolStripMenuItem item in toolStripMenuItemLanguage.DropDownItems)
            {
                if (item.Tag.Equals(lang))
                {
                    item.Checked = true;
                    break;
                }
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

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLanguage("en");
            YouOnlyCheckOne(sender);
        }

        private void espanolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLanguage("es");
            YouOnlyCheckOne(sender);
        }

        private void françaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLanguage("fr");
            YouOnlyCheckOne(sender);
        }

        private void deutschToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLanguage("de");
            YouOnlyCheckOne(sender);
        }

        private void YouOnlyCheckOne(object sender)
        {
            foreach (ToolStripMenuItem item in ((ToolStripMenuItem)sender).GetCurrentParent().Items)
            {
                if ((item != null) && (item != sender))
                {
                    item.Checked = false;
                }
            }
        }

        private void SetLanguage(string lang)
        {
            settings.Language = lang;
            WriteConfig();

            MessageBox.Show(PGMEBackend.Program.rmInternalStrings.GetString("RestartToSaveLanguage", new CultureInfo(lang)), PGMEBackend.Program.rmInternalStrings.GetString("RestartToSaveLanguageTitle", new CultureInfo(lang)), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        static Dictionary<string, MessageBoxButtons> BoxButtons = new Dictionary<string, System.Windows.Forms.MessageBoxButtons>
            {
                { "OK", MessageBoxButtons.OK },
                { "OKCancel", MessageBoxButtons.OKCancel },
                { "RetryCancel", MessageBoxButtons.RetryCancel },
                { "YesNo", MessageBoxButtons.YesNo },
                { "YesNoCancel", MessageBoxButtons.YesNoCancel }
            };

        static Dictionary<string, MessageBoxIcon> BoxIcons = new Dictionary<string, System.Windows.Forms.MessageBoxIcon>
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
            cboTimeofDayMap.SelectedIndex = 2;
            cboTimeofDayEvents.SelectedIndex = 2;
            cboEncounterTypes.SelectedIndex = 0;

            //disable already enabled stuff in case a ROM had already been loaded
            mainTabControl.Enabled = false;
            toolStripMenuItemConnectionEditor.Enabled = false;

            //set some defaults
            mainTabControl.SelectedIndex = 0;
        }

        public void EnableControlsOnMapLoad()
        {
            mainTabControl.Enabled = true;
            toolStripMenuItemConnectionEditor.Enabled = true;
        }

        private void mapNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMapSortOrder("Name");
            YouOnlyCheckOne(sender);
            ClearMapNodes();
            LoadMapNodes();
        }

        private void mapBankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMapSortOrder("Bank");
            YouOnlyCheckOne(sender);
            ClearMapNodes();
            LoadMapNodes();
        }

        private void mapLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMapSortOrder("Layout");
            YouOnlyCheckOne(sender);
            ClearMapNodes();
            LoadMapNodes();
        }

        private void mapTilesetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetMapSortOrder("Tileset");
            YouOnlyCheckOne(sender);
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
        }

        public void LoadMapNodes()
        {
            mapListTreeView.BeginUpdate();

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
                        mapListTreeView.Nodes.Add(mapNameNode);
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
                                if (!mapListTreeView.Nodes.Contains(mapTreeNodes[0xFF]))
                                {
                                    mapListTreeView.Nodes.Add(mapTreeNodes[0xFF]);
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
                        mapListTreeView.Nodes.Add(bankNode);
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
                        mapListTreeView.Nodes.Add(mapLayoutNode);
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
                    foreach (KeyValuePair<int, MapTileset> mapTileset in PGMEBackend.Program.mapTilesets)
                    {
                        var mapTilesetNode = new TreeNode("[" + mapTileset.Key.ToString("X8") + "]");
                        mapListTreeView.Nodes.Add(mapTilesetNode);
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
            mapListTreeView.EndUpdate();
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
            TreeNode node = ((TreeView)sender).SelectedNode;
            if (node == null)
                return;
            if (node.Nodes.Count == 0 && node.Tag != null)
            {
                if (currentTreeNode != null)
                {
                    currentTreeNode.ImageKey = "Map";
                    currentTreeNode.SelectedImageKey = "Map";
                }
                node.ImageKey = "Map Selected";
                node.SelectedImageKey = "Map Selected";
                if (node.Tag.GetType() == typeof(Map))
                    PGMEBackend.Program.LoadMap((Map)node.Tag);
                else if (node.Tag.GetType() == typeof(MapLayout))
                    PGMEBackend.Program.LoadMap((MapLayout)node.Tag);
                currentTreeNode = node;
            }
        }

        bool mapLoaded = false;

        public void LoadMap(object map)
        {
            LoadEditorTab(map);
            LoadHeaderTab(map);
            mapLoaded = true;
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

            glControlMapEditor.Invalidate();
            glControlBlocks.Invalidate();
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
            if (PGMEBackend.Program.TryToQuitApplication())
                QuitApplication(0);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !PGMEBackend.Program.TryToQuitApplication();
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

        bool mapWindowLoaded = false;

        private void glControlMapEditor_Load(object sender, EventArgs e)
        {
            GL.ClearColor(Color.Transparent);
            SetupViewport(glControlMapEditor.Width, glControlMapEditor.Height);
            mapEditorRectColor = rectDefaultColor;
            mapWindowLoaded = true;
        }

        private void SetupViewport(int width, int height)
        {
            int w = width;
            int h = height;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, w, h, 0, -1, 1); // Top left corner pixel has coordinate (0, 0)
            GL.Viewport(0, 0, w, h); // Use all of the glControl painting area
        }

        private void glControlMapEditor_Paint(object sender, PaintEventArgs e)
        {
            if (!mapWindowLoaded) // Play nice
                return;

            glControlMapEditor.MakeCurrent();
            SetupViewport(glControlMapEditor.Width, glControlMapEditor.Height);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            int w = glControlMapEditor.Width;
            int h = glControlMapEditor.Height;
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            RenderMap();

            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Blend);
            glControlMapEditor.SwapBuffers();
        }

        private void RenderMap()
        {
            if (!mapLoaded)
                return;
            MapLayout layout = PGMEBackend.Program.currentLayout;
            if (layout != null)
            {
                layout.Draw((PGMEBackend.Program.currentLayout.globalTileset != null) ? PGMEBackend.Program.currentLayout.globalTileset.tileSheets : null,
                            (PGMEBackend.Program.currentLayout.localTileset != null) ? PGMEBackend.Program.currentLayout.localTileset.tileSheets : null, 0, 0, 1);
                glControlMapEditor.Width = layout.layoutWidth * 16;
                glControlMapEditor.Height = layout.layoutHeight * 16;
                if (mapEditorMouseX != -1 && mapEditorMouseY != -1)
                {
                    int x = mapEditorMouseX * 16;
                    int y = mapEditorMouseY * 16;
                    int endX = mapEditorEndMouseX * 16;
                    int endY = mapEditorEndMouseY * 16;

                    if (mapEditorEndMouseX >= glControlMapEditor.Width / 16)
                        endX = ((glControlMapEditor.Width - 1) / 16) * 16;
                    if (mapEditorEndMouseY >= glControlMapEditor.Height / 16)
                        endY = ((glControlMapEditor.Height - 1) / 16) * 16;

                    int width = x - endX;
                    int height = y - endY;

                    Surface.DrawOutlineRect(endX + (width < 0 ? 16 : 0), endY + (height < 0 ? 16 : 0), width + (width >= 0 ? 16 : -16), height + (height >= 0 ? 16 : -16), mapEditorRectColor);
                }
            }
        }

        bool blockChooserLoaded = false;

        private void glControlBlocks_Load(object sender, EventArgs e)
        {
            blockChooserLoaded = true;
            GL.ClearColor(Color.Transparent);
            SetupViewport(glControlBlocks.Width, glControlBlocks.Height);
        }

        private void glControlBlocks_Paint(object sender, PaintEventArgs e)
        {
            if (!blockChooserLoaded) // Play nice
                return;

            glControlBlocks.MakeCurrent();
            SetupViewport(glControlBlocks.Width, glControlBlocks.Height);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            int w = glControlBlocks.Width;
            int h = glControlBlocks.Height;
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            RenderBlockChooser();

            GL.Disable(EnableCap.Texture2D);
            GL.Disable(EnableCap.Blend);
            glControlBlocks.SwapBuffers();
        }

        private void RenderBlockChooser()
        {
            if (!mapLoaded)
                return;
            MapTileset globalTileset = PGMEBackend.Program.currentLayout.globalTileset;
            MapTileset localTileset = PGMEBackend.Program.currentLayout.localTileset;
            int height = 0;
            if (globalTileset != null && globalTileset.blockSet != null)
            {
                globalTileset.blockSet.Draw((globalTileset != null) ? globalTileset.tileSheets : null, (localTileset != null) ? localTileset.tileSheets : null, 0, 0, 1);
                height += globalTileset.blockSet.blocks.Length * 2;
            }
            if (localTileset != null && localTileset.blockSet != null)
            {
                localTileset.blockSet.Draw((globalTileset != null) ? globalTileset.tileSheets : null, (localTileset != null) ? localTileset.tileSheets : null, 0, PGMEBackend.Program.currentGame.MainTSBlocks * 2, 1);
                height += localTileset.blockSet.blocks.Length * 2;
            }
            glControlBlocks.Height = height;
        }

        private int mapEditorMouseX = -1;
        private int mapEditorMouseY = -1;
        private int mapEditorEndMouseX = -1;
        private int mapEditorEndMouseY = -1;
        private int mapEditorSelectWidth = 0;
        private int mapEditorSelectHeight = 0;

        private void glControlMapEditor_MouseMove(object sender, MouseEventArgs e)
        {
            int oldX = mapEditorMouseX;
            int oldY = mapEditorMouseY;

            mapEditorMouseX = e.X / 16;
            mapEditorMouseY = e.Y / 16;

            if (mapEditorMouseX >= glControlMapEditor.Width / 16)
                mapEditorMouseX = (glControlMapEditor.Width - 1) / 16;
            if (mapEditorMouseY >= glControlMapEditor.Height / 16)
                mapEditorMouseY = (glControlMapEditor.Height - 1) / 16;

            if (mapEditorMouseX < 0)
                mapEditorMouseX = 0;
            if (mapEditorMouseY < 0)
                mapEditorMouseY = 0;

            if (mapEditorButtons != MouseButtons.Right)
            {
                mapEditorSelectWidth = Math.Abs(mapEditorSelectWidth);
                mapEditorSelectHeight = Math.Abs(mapEditorSelectHeight);
                mapEditorEndMouseX = mapEditorMouseX + mapEditorSelectWidth;
                mapEditorEndMouseY = mapEditorMouseY + mapEditorSelectHeight;
            }

            if((oldX != mapEditorMouseX) || (oldY != mapEditorMouseY))
                glControlMapEditor.Invalidate();
        }

        private void glControlMapEditor_MouseLeave(object sender, EventArgs e)
        {
            mapEditorMouseX = -1;
            mapEditorMouseY = -1;
            mapEditorEndMouseX = -1;
            mapEditorEndMouseY = -1;
            glControlMapEditor.Invalidate();
        }

        private MouseButtons mapEditorButtons;

        private Color mapEditorRectColor;
        private Color rectDefaultColor = Color.FromArgb(0, 255, 0);
        private Color rectPaintColor = Color.FromArgb(255, 0, 0);
        private Color rectSelectColor = Color.FromArgb(255, 255, 0);

        private void glControlMapEditor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == mapEditorButtons)
                return;
            mapEditorButtons = e.Button;
            if (mapEditorButtons == MouseButtons.Left)
                mapEditorRectColor = rectPaintColor;
            else if (mapEditorButtons == MouseButtons.Right)
            {
                mapEditorSelectWidth = 0;
                mapEditorSelectHeight = 0;
                mapEditorEndMouseX = mapEditorMouseX;
                mapEditorEndMouseY = mapEditorMouseY;
                mapEditorRectColor = rectSelectColor;
            }
            else
                mapEditorRectColor = rectDefaultColor;
            glControlMapEditor.Invalidate();
        }

        private void glControlMapEditor_MouseUp(object sender, MouseEventArgs e)
        {
            mapEditorSelectWidth = mapEditorMouseX - mapEditorEndMouseX;
            mapEditorSelectHeight = mapEditorMouseY - mapEditorEndMouseY;

            mapEditorButtons = MouseButtons.None;
            mapEditorRectColor = rectDefaultColor;
            glControlMapEditor.Invalidate();
        }

        private void glControlMapEditor_MouseEnter(object sender, EventArgs e)
        {

        }
    }
}
