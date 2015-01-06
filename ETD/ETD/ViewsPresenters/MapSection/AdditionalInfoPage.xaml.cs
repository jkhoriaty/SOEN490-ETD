using ETD.Models;
using ETD.ViewsPresenters.MapSection.PinManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ETD.ViewsPresenters.MapSection
{

	/// <summary>
    /// Interaction logic for AdditionnalInfoPage.xaml
	/// </summary>
	public partial class AdditionalInfoPage : Page
	{
		MainWindow mainWindow;
		PinEditor pinEditor;
        AIPinHandler pinHandler;

        public AdditionalInfoPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
			pinEditor = new PinEditor(this);
            pinHandler = new AIPinHandler(this);
		}

		//Loading of map as a result to the user clicking the "Load Map" button
        //Loading the map should only be done on the AdditionalInfoPAge.xaml rather than MapSectionPage.xaml
		public void SetMap(BitmapImage coloredImage)
		{
			//Making the picture grayscale
			FormatConvertedBitmap grayBitmap = new FormatConvertedBitmap();
			grayBitmap.BeginInit();
			grayBitmap.Source = coloredImage;
			grayBitmap.DestinationFormat = PixelFormats.Gray8;
			grayBitmap.EndInit();

			//Displaying the map as the background
            AdditionalMap.Background = new ImageBrush(grayBitmap);
		}
		
	

		public void SetPinPosition(Grid g, double X, double Y)
		{
			pinHandler.SetPinPosition(g, X, Y);
		}
		
		internal void DragStart(object sender, MouseButtonEventArgs e)
		{
			pinHandler.DragStart(sender, e);
		}

		internal void DragStop(object sender, MouseButtonEventArgs e)
		{
			pinHandler.DragStop(sender, e);
		}

		internal void DragMove(object sender, MouseEventArgs e)
		{
			pinHandler.DragMove(sender, e);
		}

		//public void DetectCollision(Grid movedItem, double movedItem_X, double movedItem_Y)
		//{
		//	pinHandler.DetectCollision(movedItem, movedItem_X, movedItem_Y);
		//}

        internal void CheckRight(object sender, RoutedEventArgs e)
        {
            pinEditor.CheckRight(sender, e);
        }

		//When the window is resized, the pins need to move to stay in the window
		public void movePins(double widthRatio, double heightRatio)
		{
			pinHandler.movePins(widthRatio, heightRatio);
		}


        /// <summary>
        /// //////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="AI"></param>
        public void CreateAdditionnalInfoPin(String AI)
        {
            pinEditor.CreateAdditionnalInfoPin(AI);
        }

        		internal void AIDeletePin(object sender, RoutedEventArgs e)
		{
            pinEditor.AIDeletePin(sender,e);
		}

                internal void ScalePin(object sender, RoutedEventArgs e)
        {
            pinEditor.ScalePin(sender,e);
        }

	}
}
