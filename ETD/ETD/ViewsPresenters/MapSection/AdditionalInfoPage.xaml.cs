﻿using ETD.Models.Objects;
using ETD.Models.ArchitecturalObjects;
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
using ETD.CustomObjects.CustomUIObjects;
using ETD.Services;
using System.Windows.Shapes;
using Microsoft.VisualBasic;

namespace ETD.ViewsPresenters.MapSection
{
	/// <summary>
	/// Interaction logic for AdditionnalInfoPage.xaml
	/// </summary>
	public partial class AdditionalInfoPage : Page
	{
		MainWindow mainWindow;
		ImageBrush imgbrush;//Used for loading the map
		private bool isMapLoaded = false;//Used to check if a map was loaded

		// Drawing lines variables
		private bool IsDrawing = false;
		private System.Windows.Point NewPt1, NewPt2;
		private List<Line> Lines = new List<Line>();
		private Line newline;
		private bool ContainsLine = false; 
		private static object slock = new object();

		//Drawing shapes variables
		private List<object> objectList = new List<object>();//Contains the list of added map modification items
		private System.Windows.Shapes.Shape mapModObject;
		private int _startX, _startY;
		private String mapModName;
		private String textInput = "Default";
		private System.Drawing.SizeF textSize;

		//Creates a new Additional map information page
		public AdditionalInfoPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
			AdditionalMap.Focus();
		}

		//Loading the map 
		public void setMap(BitmapImage coloredImage)
		{
			//Making the picture grayscale
			FormatConvertedBitmap grayBitmap = new FormatConvertedBitmap();
			grayBitmap.BeginInit();
			grayBitmap.Source = coloredImage;
			grayBitmap.DestinationFormat = PixelFormats.Gray8;
			grayBitmap.EndInit();

			//Displaying the map as the background
			imgbrush = new ImageBrush(grayBitmap);
			isMapLoaded = true;
			AdditionalMap.Background = imgbrush;
		}

		//Checks if a map has been loaded
		public bool MapLoaded()
		{
			return isMapLoaded;
		}

		//Get the current position of the mouse on the screen when the left mouse button is clicked
		internal void DrawingStart(object sender, MouseButtonEventArgs e)
		{
			IsDrawing = true;
			//get starting point
			NewPt1 = e.GetPosition(AdditionalMap);
			_startX = Convert.ToInt32(NewPt1.X);
			_startY = Convert.ToInt32(NewPt1.Y);

		}

		//Set the map modification object type
		internal void setMapModObjectType()
		{
			if (mapModName.Equals("rectangle"))
			{
				mapModObject = new System.Windows.Shapes.Rectangle();
				mapModObject.StrokeThickness = 3;
			}

			if (mapModName.Equals("circle"))
			{
				mapModObject = new System.Windows.Shapes.Ellipse();
				mapModObject.StrokeThickness = 3;
			}

			if (mapModName.Equals("ramp") || mapModName.Equals("camp") || mapModName.Equals("stairs"))
			{
				mapModObject = new System.Windows.Shapes.Rectangle();
				ImageBrush mapModImg = new ImageBrush();
				MapMods mapModi = (MapMods)Enum.Parse(typeof(MapMods), mapModName);
				mapModImg.ImageSource = TechnicalServices.getImage(mapModi);
				mapModObject.Fill = mapModImg;
			}

			if (mapModName.Equals("text"))
			{
				textInput = MainWindow.getAdditionalTextInput();

				if (textInput == null || textInput.Equals(""))
				{
					textInput = "Enter text";
				}
				else
				{
					textInput = MainWindow.getAdditionalTextInput();
				}
			
				System.Drawing.Font font = new System.Drawing.Font("Times New Roman",35.0f);
				System.Drawing.Bitmap bitmap = DrawText(textInput, font, System.Drawing.Color.Black, System.Drawing.Color.Transparent);

				ToBitmapImage(bitmap);
				BitmapImage bitmapimg = ToBitmapImage(bitmap);
				mapModObject = new System.Windows.Shapes.Rectangle();
				ImageBrush mapModImg = new ImageBrush();
				mapModImg.ImageSource = bitmapimg;
				mapModObject.Fill = mapModImg;
			}

			if (mapModName.Equals("line"))
			{
				return;
			}
		}

		//The selected map modification item is drawn on the map when the left mouse button is released
		internal void DrawingMove(object sender, MouseEventArgs e)
		{
			//Draws the selected map modification option
			if (mapModName.Equals("line"))
			{
				newline = new Line();
				newline.Stroke = System.Windows.Media.Brushes.Black;
				newline.StrokeThickness = 4;
				newline.X1 = NewPt1.X;
				newline.Y1 = NewPt1.Y;
				newline.X2 = NewPt2.X;
				newline.Y2 = NewPt2.Y;

				objectList.Add(newline);
				AdditionalMap.Children.Add(newline);
			}

			else if (!mapModName.Equals("line"))
			{
				//The width of the shape is the the maximum between the start x-position and current x-position minus the minimum of start x-position and current x-position
				int width = Convert.ToInt32(Math.Max(_startX, NewPt2.X) - Math.Min(_startX, NewPt2.X));

				//For the height value, it's basically the same thing as above, but with the y-values:
				int height = Convert.ToInt32(Math.Max(_startY, NewPt2.Y) - Math.Min(_startY, NewPt2.Y));

				//get the appropriate image of the item
				setMapModObjectType();

				//If the item is a rectangle , draw a border. Otherwise, set it as transparent
				if (mapModName.Equals("rectangle") || mapModName.Equals("circle"))
				{
					mapModObject.Stroke = System.Windows.Media.Brushes.Black;
				}
				else
				{
					mapModObject.Stroke = System.Windows.Media.Brushes.Transparent;
				}

				mapModObject.Width = width;
				mapModObject.Height = height;

				if (NewPt1.X > NewPt2.X && NewPt1.Y > NewPt2.Y) //draw from bottom right to  top left
				{
					mapModObject.Margin = new Thickness(NewPt2.X, NewPt2.Y, NewPt1.X, NewPt1.Y);
				}

				if (NewPt1.X < NewPt2.X && NewPt1.Y < NewPt2.Y) //draw from top left to bottom right 
				{
					mapModObject.Margin = new Thickness(NewPt1.X, NewPt1.Y, NewPt2.X, NewPt2.Y);
				}

				if (NewPt1.X < NewPt2.X && NewPt1.Y > NewPt2.Y) //draw from bottom left to top right
				{
					mapModObject.Margin = new Thickness(NewPt1.X, NewPt2.Y, NewPt2.X, NewPt1.Y);
				}

				if (NewPt1.X > NewPt2.X && NewPt1.Y < NewPt2.Y) //draw from top right to bottom left
				{
					mapModObject.Margin = new Thickness(NewPt2.X, NewPt1.Y, NewPt1.X, NewPt2.Y);
				}

				objectList.Add(mapModObject);
				AdditionalMap.Children.Add(mapModObject);
			}
		}

		//Fix asynchronous error when deleting shapes and lines
		void ThreadProc()
		{
			MessageBox.Show("Deleting..");
		}

		//When the mouse is moving, get its position to create the selected map modification item
		internal void Move(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				NewPt2 = e.GetPosition(AdditionalMap);
			}

			bool isEmpty = !objectList.Any();//Checks if the list of map modification object is empty


			//When the mouse is moving and the escape key is pressed, remove the most recently added map modification item
			if (Keyboard.IsKeyDown(Key.Escape))
			{
				if (!isEmpty)
				{
					try
					{
						int objectIndex = objectList.Count - 1;
						AdditionalMap.Children.RemoveAt(objectList.Count);

						//Fix asynchronous error when deleting shapes and lines
						System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
						t.Start();
						System.Threading.Thread.Sleep(100);
						if (t.IsAlive)
						{
							t.Abort();
						}

						//Deleting additional map info
						if (objectIndex == 0 && objectList[0] != null)
						{
							objectList.RemoveAt(0);
						}
						else
						{
							objectList.RemoveAt(objectIndex);
						}
					}
					catch (Exception ex)
					{

					}
				}
			}
		}

		//Stopped drawing 
		internal void DrawingStop(object sender, MouseButtonEventArgs e)
		{
			IsDrawing = false;
		}

		//Change the map modification color on mouse scroll
		internal void ChangeColor(object sender, MouseWheelEventArgs e)
		{
			//scroll up to new color
			if (e.Delta > 0)
			{
				if (objectList != null)
				{
					foreach (Shape ob in objectList)
					{
						ob.Stroke = System.Windows.Media.Brushes.Red;
					}
				}
			}
			//scroll down to original color
			else
			{
				if (objectList != null)
				{
					foreach (Shape ob in objectList)
					{
						ob.Stroke = System.Windows.Media.Brushes.Black;
					}
				}
			}
		}

		//Creates a new Map modification item
		public void CreateMapModificationPin(String AI)
		{
			mapModName = AI;
			MapMod mapMod = new MapMod("line");
			MapModPin lineMapModPin = new MapModPin(mapMod, this, this.ActualWidth, this.ActualHeight);
			this.Cursor = Cursors.Pen;
			//Only one map modification pin will be created. It will contain all shapes and lines drawn on the map. Thus it only needs to be created once.
			if (!ContainsLine)
			{
				AdditionalMap.Children.Add(lineMapModPin);
				ContainsLine = true;
			}
		}

		//Convert bitmap to bitmap image, used when drawing text
		public BitmapImage ToBitmapImage(System.Drawing.Bitmap bitmap)
		{
			using (var memory = new System.IO.MemoryStream())
			{
				bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
				memory.Position = 0;
				var bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = memory;
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.EndInit();

				return bitmapImage;
			}
		}

		//Draw text on the map
		private System.Drawing.Bitmap DrawText(String text, System.Drawing.Font font, System.Drawing.Color textColor, System.Drawing.Color backColor)
		{
			//Create a dummy bitmap to get a graphics object
			System.Drawing.Bitmap img = new System.Drawing.Bitmap(1, 1);
			System.Drawing.Graphics drawing = System.Drawing.Graphics.FromImage(img);

			//Measure the string to see how big the image needs to be
			 textSize = drawing.MeasureString(text, font);

			//free up the dummy image and old graphics object
			img.Dispose();
			drawing.Dispose();

			//create a new image of the right size
			img = new System.Drawing.Bitmap((int)textSize.Width, (int)textSize.Height);
			drawing = System.Drawing.Graphics.FromImage(img);

			//paint the background
			drawing.Clear(backColor);

			System.Drawing.SolidBrush textBrush = new System.Drawing.SolidBrush(textColor);
			drawing.DrawString(text, font, textBrush, 0, 0);
			drawing.Save();
			textBrush.Dispose();
			drawing.Dispose();

			return img;
		}
	}
}
