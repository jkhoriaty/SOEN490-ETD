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
			/*ImageBrush imageBrush = new ImageBrush();
			Uri uri = new Uri(@"/DragAndDropRectangle/resources/back.jpg", UriKind.Relative);
			MessageBox.Show(uri.ToString());
			imageBrush.ImageSource = new BitmapImage(uri);
			canvas.Background = imageBrush;*/
        }

        private bool _isRectDragInProg;
        private double oldLeft;
        private double oldTop;
        private void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle r = sender as Rectangle;
            _isRectDragInProg = r.CaptureMouse();
            oldLeft = Canvas.GetLeft(r);
            oldTop = Canvas.GetTop(r);
        }

        private void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle r = sender as Rectangle;
            r.ReleaseMouseCapture();
            _isRectDragInProg = false;

            /////////////////////CODE FOR COLLISION DETECTION NOT WORKING PROPERLY AT THE MOMENT////////////////////////
            ///Comment out the section if you want to use it without the collision detection///////////////////////////
            Rect r1 = new Rect(Canvas.GetLeft(r), Canvas.GetTop(r), r.Width, r.Height);


            var rectangles = canvas.Children.OfType<Rectangle>().ToList();

            foreach (var rectangle in rectangles)
            {
                Rect r2 = new Rect(Canvas.GetLeft(rectangle), Canvas.GetTop(rectangle), rect2.Width, rect2.Height);
                if (r2 != r1)
                {
                    Rect inter = Rect.Intersect(r1, r2);
                    if (!inter.IsEmpty)
                    {
                        Canvas.SetLeft(r, oldLeft);
                        Canvas.SetTop(r, oldTop);
                        //Canvas.SetLeft(r, oldLeft);
                        //Canvas.SetTop(r, oldTop);
                        return;
                    }
                }
            }
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////

           
        }

        private void rect_MouseMove(object sender, MouseEventArgs e)
        {
			if (!_isRectDragInProg) return;
            

            // get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(canvas);
            Rectangle r = sender as Rectangle;

            // center the rect on the mouse
            double left = mousePos.X - (r.ActualWidth / 2);
            double top = mousePos.Y - (r.ActualHeight / 2);

            if (left > canvas.ActualWidth - 50 || top > canvas.ActualHeight - 50 || left < 0 || top < 0)
			{
				return;
			}


            Canvas.SetLeft(r, left);
            Canvas.SetTop(r, top);

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
            r.Width = 50;
            r.Height = 50;
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
