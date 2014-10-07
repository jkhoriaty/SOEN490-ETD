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
using MySql.Data.MySqlClient;
using System.Drawing;

namespace DragAndDropRectangle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool _isRectDragInProg;
		String movingRectangle;
		int shapeRadius = 25;

        private void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle r = sender as Rectangle;
            _isRectDragInProg = r.CaptureMouse();
			movingRectangle = r.Name;
        }

        private void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle r = sender as Rectangle;

			if(!r.Name.Equals(movingRectangle))
			{
				return;
			}

            r.ReleaseMouseCapture();
            _isRectDragInProg = false;

			var mousePos = e.GetPosition(canvas);
			double horizontalDropped = Math.Round(mousePos.X, 3);
			double verticalDropped = Math.Round(mousePos.Y, 3);

			bool collisionDetected = true;
			int verifications = 0;

			while(collisionDetected == true)
			{
				collisionDetected = false;
				verifications++;

				//Gathering all rectangles to search for collision
				var rectangles = canvas.Children.OfType<Rectangle>().ToList();

				//Iterating throught them
				foreach (var rectangle in rectangles)
				{
					//Avoiding detecting a collision with itself
					if (rectangle != r)
					{
						//Getting the position of where the rectangle has been dropped
						double horizontalFixed = Math.Round((((double)Canvas.GetLeft(rectangle)) + shapeRadius), 3);
						double verticalFixed = Math.Round((((double)Canvas.GetTop(rectangle)) + shapeRadius), 3);

						//Variables for possible collision resolution
						double horizontalDifference;
						double verticalDifference;
						double differenceRatio = 0.1;

						//Checking if the dropped rectangle is within the bounds of any other rectangle
						while (horizontalDropped > (horizontalFixed - (shapeRadius * 2)) && horizontalDropped < (horizontalFixed + (shapeRadius * 2)) && verticalDropped > (verticalFixed - (shapeRadius * 2)) && verticalDropped < (verticalFixed + (shapeRadius * 2)))
						{
							collisionDetected = true;

							//Collision detected, resolution by shifting the rectangle in the same direction that it has been dropped
							box.Text = "horDrop: " + horizontalDropped + "; verDrop: " + verticalDropped;
							box2.Text = "horPos: " + horizontalFixed + "; verPos: " + verticalFixed;

							//Finding out how much is the dropped rectangle covering the fixed one
							horizontalDifference = Math.Round((horizontalDropped - horizontalFixed), 3);
							verticalDifference = Math.Round((verticalDropped - verticalFixed), 3);

							box3.Text = "horDifference: " + horizontalDifference;
							box4.Text = "verDifference: " + verticalDifference;

							bool moved = false;

							//Don't move horizontally if there are no difference and avoiding division by 0
							if (horizontalDifference != 0)
							{
								//Finding the ratio at which we should increase the values to put them side by side but in the same direction as it was dropped
								differenceRatio = Math.Round(((Math.Abs(verticalDifference) / Math.Abs(horizontalDifference)) / 10), 3);

								//Shifting horizontally in the correct direction, if it is not colliding with the map border
								if (horizontalDropped > shapeRadius && horizontalDropped < (canvas.ActualWidth - shapeRadius))
								{
									if (horizontalDifference < 0)
									{
										horizontalDropped -= 0.1;
										moved = true;
									}
									else
									{
										horizontalDropped += 0.1;
										moved = true;
									}
								}
							}

							//Don't move vertically if there are no difference
							if (verticalDifference != 0)
							{
								//Shifting vertically in the correct direction, if it is not colliding with the map border
								if (verticalDropped > shapeRadius && verticalDropped < (canvas.ActualHeight - shapeRadius))
								{
									if (verticalDifference < 0)
									{
										verticalDropped -= differenceRatio;
										moved = true;
									}
									else
									{
										verticalDropped += differenceRatio;
										moved = true;
									}
								}
							}

							//Rounding values
							horizontalDropped = Math.Round(horizontalDropped, 3);
							verticalDropped = Math.Round(verticalDropped, 3);

							//Handling situation where object is dropped between two others and is just bouncing around.
							if(verifications > 100)
							{
								MessageBox.Show("The dropepd object is dropped between two objects and is bouncing around with no progress. Resetting it.");
								horizontalDropped = (canvas.ActualWidth / 2) - shapeRadius;
								verticalDropped = (canvas.ActualHeight / 2) - shapeRadius;
								Canvas.SetLeft(r, horizontalDropped);
								Canvas.SetTop(r, verticalDropped);
							}

							//Handling corner situation
							if(moved == false)
							{
								MessageBox.Show("There's not enough space in the corner for this item. Replacing it in the center for you to replace it elsewhere.");
								horizontalDropped = (canvas.ActualWidth / 2) - shapeRadius;
								verticalDropped = (canvas.ActualHeight / 2) - shapeRadius;
								Canvas.SetLeft(r, horizontalDropped);
								Canvas.SetTop(r, verticalDropped);

								//Uncomment following line if we decide to place the item in the perfect middle without collision detection for the user to move it
								//return;
							}
						}
					}
				}
			}

			//Drop the rectangle if there are not collision or after resolution of collision
			Canvas.SetLeft(r, (horizontalDropped - shapeRadius));
			Canvas.SetTop(r, (verticalDropped - shapeRadius));
        }

		//Method to visually drag the item selected and insuring it doesn't go outside of the map
        private void rect_MouseMove(object sender, MouseEventArgs e)
        {
			//If no rectangle are clicked, exit method
			if (!_isRectDragInProg) return;
			
            // get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(canvas);
            Rectangle r = sender as Rectangle;

			//Handling exception where fixed rectangle gets moved when another rectangle is dropped on it
			if (!r.Name.Equals(movingRectangle))
			{
				return;
			}

			//Making sure it is not dragged out of bounds
			if (mousePos.X > (canvas.ActualWidth - shapeRadius) || mousePos.Y > (canvas.ActualHeight - shapeRadius) || mousePos.X < shapeRadius || mousePos.Y < shapeRadius)
			{
				return;
			}

            Canvas.SetLeft(r, (mousePos.X - shapeRadius));
            Canvas.SetTop(r, (mousePos.Y - shapeRadius));
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string server = "localhost";
			string database = "test";
			string uid = "csharp";
			string password = "csharp";

			string connectionString = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";

			MySqlConnection connection = new MySqlConnection(connectionString);

			try
			{
				//Opening connection
				connection.Open();

				//Inserting into the database
				Random rnd = new Random();
				string title = "Test " + rnd.Next(1, 100);
				string text = "This is sample text for a proof of concept";

				string query = "INSERT INTO testTable (title, text) VALUES('" + title + "', '" + text + "')";

				MySqlCommand cmd = new MySqlCommand(query, connection);
				cmd.ExecuteNonQuery();

				//Pulling from the database
				query = "SELECT * FROM testTable";

				List< string >[] list = new List< string >[3];
				list[0] = new List<string>();
				list[1] = new List<string>();
				list[2] = new List<string>();

				cmd = new MySqlCommand(query, connection);
				MySqlDataReader dataReader = cmd.ExecuteReader();

				while (dataReader.Read())
				{
					list[0].Add(dataReader["id"] + "");
					list[1].Add(dataReader["title"] + "");
					list[2].Add(dataReader["text"] + "");
				}

				dataReader.Close();

				int lastMessage = list[0].Count;
				box.Text = "Id: " + list[0][lastMessage-1] + "; " + list[1][lastMessage-1] + ": " + list[2][lastMessage-1];

				//Closing connection
				connection.Close();
			}
			catch(Exception ex)
			{
				MessageBox.Show("Something has failed! " + ex.Message);
			}
		}

        private void Generate_rectangle(object sender, RoutedEventArgs e)
        {
            Rectangle r = new Rectangle();
            r.Width = shapeRadius * 2;
            r.Height = shapeRadius * 2;
            r.Stroke = new SolidColorBrush(Colors.Black);
            r.Fill = new SolidColorBrush(Colors.GreenYellow);
            r.MouseLeftButtonDown += new MouseButtonEventHandler(rect_MouseLeftButtonDown);
            r.MouseLeftButtonUp += new MouseButtonEventHandler(rect_MouseLeftButtonUp);
            r.MouseMove += new MouseEventHandler(rect_MouseMove);
            Canvas.SetTop(r, 0);
            Canvas.SetLeft(r, 0);
            canvas.Children.Add(r);

            box.Text = "succ";

        }
    }
}
