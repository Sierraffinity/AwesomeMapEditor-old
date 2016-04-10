using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PGMEBackend;
using static PGMEBackend.Config;
using PGMEBackend.Entities;
using Microsoft.Win32;
using Xceed.Wpf.Toolkit;
using System.Globalization;

namespace PGMEWPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, UIInteractionLayer
    {
        public MainWindow()
        {
            if (ReadConfig() != 0)
            {
                QuitApplication(0);
            }
            InitializeComponent();
            Program.Initialize(this);
        }

        private void canvasMapEditor_Initialized(object sender, EventArgs e)
        {

        }

        private void canvasEntityEditor_Initialized(object sender, EventArgs e)
        {

        }

        private void OpenROMButton_Click(object sender, RoutedEventArgs e)
        {
            Program.LoadROM();
            LoadMap
        }

        public void SetTitleText(string title)
        {

        }

        public void SetLoadingStatus(string status)
        {

        }

        public string ShowMessageBox(string body, string title)
        {
            return string.Empty;
        }

        public string ShowMessageBox(string body, string title, string buttons)
        {
            return string.Empty;
        }

        public string ShowMessageBox(string body, string title, string buttons, string icon)
        {
            return string.Empty;
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

        public void EnableControlsOnROMLoad()
        {

        }

        public void EnableControlsOnMapLoad()
        {

        }

        public Dictionary<int, TreeViewItem> mapTreeViewItems;
        ImageTreeViewItem currentTreeViewItem;
        TreeView backupTree = new TreeView();
        System.Drawing.Bitmap mapImage = Properties.Resources.map_16x16;
        System.Drawing.Bitmap mapSelectedImage = Properties.Resources.image_16x16;
        System.Drawing.Bitmap mapFolderClosedImage = Properties.Resources.folder_closed_map_16x16;
        System.Drawing.Bitmap mapFolderOpenImage = Properties.Resources.folder_map_16x16;
        System.Drawing.Bitmap folderClosedImage = Properties.Resources.folder_closed_16x16;

        public void LoadMapNodes()
        {/*
            int i = 0;
            switch (settings.MapSortOrder)
            {
                default:
                    mapTreeNodes.Add(0xFF, new TreeNode(PGMEBackend.Program.rmInternalStrings.GetString("InvalidMapNameIndex")));
                    foreach (KeyValuePair<int, MapName> mapName in PGMEBackend.Program.mapNames)
                    {
                        var mapNameNode = new TreeNode("[" + mapName.Key.ToString("X2") + "] " + mapName.Value.name);
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
                                var node = mapTreeViewItems[map.mapNameIndex].Nodes.Add("mapNode" + i++, map.name);
                                node.Tag = map;
                                mapTreeViewItems[map.mapNameIndex].SelectedImageKey = "Map Folder Closed";
                                mapTreeViewItems[map.mapNameIndex].ImageKey = "Map Folder Closed";
                            }
                            catch (KeyNotFoundException)
                            {
                                if (!backupTree.Nodes.Contains(mapTreeViewItems[0xFF]))
                                {
                                    backupTree.Nodes.Add(mapTreeViewItems[0xFF]);
                                }
                                var node = mapTreeViewItems[0xFF].Nodes.Add("mapNode" + i++, map.name);
                                node.Tag = map;
                                mapTreeViewItems[0xFF].SelectedImageKey = "Map Folder Closed";
                                mapTreeViewItems[0xFF].ImageKey = "Map Folder Closed";
                            }
                        }
                    }
                    break;
                case "Bank":
                    foreach (KeyValuePair<int, MapBank> mapBank in PGMEBackend.Program.mapBanks)
                    {
                        var bankNode = new TreeViewItem("[" + mapBank.Key.ToString("X2") + "]");
                        bankNode.SelectedImageKey = "Folder Closed";
                        bankNode.ImageKey = "Folder Closed";
                        backupTree.Nodes.Add(bankNode);
                        mapTreeViewItems.Add(mapBank.Key, bankNode);
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
                        var mapLayoutNode = new TreeViewItem(mapLayout.Value.name);
                        mapLayoutNode.Tag = mapLayout.Value;
                        backupTree.Nodes.Add(mapLayoutNode);
                        mapTreeViewItems.Add(mapLayout.Key, mapLayoutNode);
                    }
                    foreach (MapBank mapBank in PGMEBackend.Program.mapBanks.Values)
                    {
                        foreach (Map map in mapBank.GetBank().Values)
                        {
                            var node = mapTreeViewItems[map.mapLayoutIndex].Nodes.Add("mapNode" + i++, map.name);
                            node.Tag = map;
                            mapTreeViewItems[map.mapLayoutIndex].SelectedImageKey = "Map Folder Closed";
                            mapTreeViewItems[map.mapLayoutIndex].ImageKey = "Map Folder Closed";
                        }

                    }
                    break;
                case "Tileset":
                    int j = 0;
                    foreach (KeyValuePair<int, MapTileset> mapTileset in PGMEBackend.Program.mapTilesets)
                    {
                        var mapTilesetNode = new TreeViewItem("[" + j++ + "] " + settings.HexPrefix + (mapTileset.Key + 0x8000000).ToString("X8"));
                        backupTree.Nodes.Add(mapTilesetNode);
                        mapTreeViewItems.Add(mapTileset.Key, mapTilesetNode);
                    }
                    foreach (MapBank mapBank in PGMEBackend.Program.mapBanks.Values)
                    {
                        foreach (Map map in mapBank.GetBank().Values)
                        {
                            var node = mapTreeViewItems[map.layout.globalTilesetPointer].Nodes.Add("mapNode" + i++, map.name);
                            node.Tag = map;
                            mapTreeViewItems[map.layout.globalTilesetPointer].SelectedImageKey = "Map Folder Closed";
                            mapTreeViewItems[map.layout.globalTilesetPointer].ImageKey = "Map Folder Closed";

                            node = mapTreeViewItems[map.layout.localTilesetPointer].Nodes.Add("mapNode" + i++, map.name);
                            node.Tag = map;
                            mapTreeViewItems[map.layout.localTilesetPointer].SelectedImageKey = "Map Folder Closed";
                            mapTreeViewItems[map.layout.localTilesetPointer].ImageKey = "Map Folder Closed";
                        }

                    }
                    foreach (KeyValuePair<int, MapLayout> mapLayout in PGMEBackend.Program.mapLayouts)
                    {
                        TreeViewItem node;
                        if (mapTreeViewItems.ContainsKey(mapLayout.Value.globalTilesetPointer) && GetNodeFromTag(mapLayout.Value, mapTreeViewItems[mapLayout.Value.globalTilesetPointer]) == null)
                        {
                            node = mapTreeViewItems[mapLayout.Value.globalTilesetPointer].Nodes.Add("mapNode" + i++, mapLayout.Value.name);
                            node.Tag = mapLayout.Value;
                            //mapTreeViewItems.Add(mapLayout.Key, node);
                            mapTreeViewItems[mapLayout.Value.globalTilesetPointer].SelectedImageKey = "Map Folder Closed";
                            mapTreeViewItems[mapLayout.Value.globalTilesetPointer].ImageKey = "Map Folder Closed";
                        }
                        if (mapTreeViewItems.ContainsKey(mapLayout.Value.localTilesetPointer) && GetNodeFromTag(mapLayout.Value, mapTreeViewItems[mapLayout.Value.localTilesetPointer]) == null)
                        {
                            node = mapTreeViewItems[mapLayout.Value.localTilesetPointer].Nodes.Add("mapNode" + i++, mapLayout.Value.name);
                            node.Tag = mapLayout.Value;
                            //mapTreeViewItems.Add(mapLayout.Key, node);
                            mapTreeViewItems[mapLayout.Value.localTilesetPointer].SelectedImageKey = "Map Folder Closed";
                            mapTreeViewItems[mapLayout.Value.localTilesetPointer].ImageKey = "Map Folder Closed";
                        }
                    }
                    break;
            }
            CopyTreeViewItems(backupTree, mapListTreeView);
            if (PGMEBackend.Program.currentLayout != null)
            {
                TreeViewItem itemNode = null;
                object tag = null;
                if (PGMEBackend.Program.currentMap != null)
                    tag = PGMEBackend.Program.currentMap;
                else
                    tag = PGMEBackend.Program.currentLayout;
                foreach (TreeViewItem node in mapListTreeView.Nodes)
                {
                    itemNode = GetNodeFromTag(tag, node);
                    if (itemNode != null)
                    {
                        itemNode.EnsureVisible();
                        itemNode.ImageKey = "Map Selected";
                        currentTreeViewItem = itemNode;
                        break;
                    }
                }
            }*/
        }

        public void ClearMapNodes()
        {

        }

        public void LoadHeaderTabDropdowns()
        {

        }

        public void QuitApplication(int code)
        {

        }

        public void LoadMap(object map)
        {

        }

        public void SetGLMapEditorSize(int w, int h)
        {

        }

        public void SetGLBlockChooserSize(int w, int h)
        {

        }

        public void SetGLBorderBlocksSize(int w, int h)
        {

        }

        public void SetGLEntityEditorSize(int w, int h)
        {

        }

        public void RefreshMapEditorControl()
        {

        }

        public void RefreshBlockEditorControl()
        {

        }

        public void RefreshPermsChooserControl()
        {

        }

        public void RefreshBorderBlocksControl()
        {

        }

        public void RefreshEntityEditorControl()
        {

        }

        public void ScrollBlockChooserToBlock(int blockNum)
        {

        }

        public void ScrollPermChooserToPerm(int permNum)
        {

        }

        public int PermTransPreviewValue()
        {
            return 0;
        }

        public void AddRecentFile(string fname)
        {

        }

        public void LoadEntityView(Entity entity)
        {
            
        }

        public void LoadEntityView(Entity.EntityType entityType, int entityNum)
        {

        }

        public void MultipleEntitiesSelected()
        {

        }

        public void FollowWarp(int mapBank, int mapNum, int warpNum)
        {

        }

        public void FollowWarp(Warp warp)
        {

        }

        public int LaunchScriptEditor(int scriptOffset)
        {
            return 0;
        }

        public Entity CreateNewEntity(Entity.EntityType entityType, int x = 0, int y = 0)
        {
            return null;
        }

        public Entity CreateNewEntity(int x = 0, int y = 0)
        {
            return null;
        }

        public void CreateNewEntity(Entity entity)
        {

        }

        public bool DeleteEntity(Entity entity)
        {
            return false;
        }

        private void HexBoxOnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int hexNumber;
            e.Handled = !int.TryParse(e.Text, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out hexNumber);
        }
    }
    
    public class WidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Math.Max(System.Convert.ToDouble(value) - System.Convert.ToDouble(parameter), 0.0);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Class used to have an image that is able to be gray when the control is not enabled.
    /// Author: Thomas LEBRUN (http://blogs.developpeur.org/tom)
    /// </summary>
    public class AutoGreyableImage : Image
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoGreyableImage"/> class.
        /// </summary>
        static AutoGreyableImage()
        {
            // Override the metadata of the IsEnabled property.
            IsEnabledProperty.OverrideMetadata(typeof(AutoGreyableImage), new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnAutoGreyScaleImageIsEnabledPropertyChanged)));
        }
        /// <summary>
        /// Called when [auto grey scale image is enabled property changed].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="args">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void OnAutoGreyScaleImageIsEnabledPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            var autoGreyScaleImg = source as AutoGreyableImage;
            var isEnable = Convert.ToBoolean(args.NewValue);
            if (autoGreyScaleImg != null)
            {
                if (!isEnable)
                {
                    // Get the source bitmap
                    var bitmapImage = new BitmapImage(new Uri(autoGreyScaleImg.Source.ToString()));
                    // Convert it to Gray
                    autoGreyScaleImg.Source = new FormatConvertedBitmap(bitmapImage, PixelFormats.Gray32Float, null, 0);
                    // Create Opacity Mask for greyscale image as FormatConvertedBitmap does not keep transparency info
                    autoGreyScaleImg.OpacityMask = new ImageBrush(bitmapImage);
                }
                else
                {
                    // Set the Source property to the original value.
                    autoGreyScaleImg.Source = ((FormatConvertedBitmap)autoGreyScaleImg.Source).Source;
                    // Reset the Opcity Mask
                    autoGreyScaleImg.OpacityMask = null;
                }
            }
        }
    }

    public class ImageTreeViewItem : TreeViewItem
    {
        #region Data Member

        Uri _imageUrl = null;
        Image _image = null;
        TextBlock _textBlock = null;

        #endregion

        #region Properties

        public Uri ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                _image.Source = new BitmapImage(value);
            }
        }

        public string Text
        {
            get { return _textBlock.Text; }
            set { _textBlock.Text = value; }
        }

        #endregion

        #region Constructor

        public ImageTreeViewItem()
        {
            CreateTreeViewItemTemplate();
        }

        public ImageTreeViewItem(string name, ImageSource image)
        {
            CreateTreeViewItemTemplate();
            _image.Source = image;
            _textBlock.Text = name;
        }

        #endregion

        #region Private Methods

        private void CreateTreeViewItemTemplate()
        {
            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;

            _image = new Image();
            _image.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            _image.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            _image.Width = 16;
            _image.Height = 16;
            _image.Margin = new Thickness(2);

            stack.Children.Add(_image);

            _textBlock = new TextBlock();
            _textBlock.Margin = new Thickness(2);
            _textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            stack.Children.Add(_textBlock);

            Header = stack;
        }

        #endregion
    }
}
