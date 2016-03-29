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

namespace PGMEWPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, UIInteractionLayer
    {
        public MainWindow()
        {
            InitializeComponent();
            Program.Initialize(this);
            if (ReadConfig() != 0)
            {
                QuitApplication(0);
            }
        }

        private void canvasMapEditor_Initialized(object sender, EventArgs e)
        {

        }

        private void OpenROMButton_Click(object sender, RoutedEventArgs e)
        {
            Program.LoadROM();
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

        public void LoadMapNodes()
        {

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
    }

    public class WidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Math.Max(System.Convert.ToDouble(value) - System.Convert.ToDouble(parameter), 0.0);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
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
}
