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
        private void rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isRectDragInProg = true;
            rect.CaptureMouse();
        }

        private void rect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isRectDragInProg = false;
			rect.ReleaseMouseCapture();
        }

        private void rect_MouseMove(object sender, MouseEventArgs e)
        {
			if (!_isRectDragInProg) return;

            // get the position of the mouse relative to the Canvas
            var mousePos = e.GetPosition(canvas);

            // center the rect on the mouse
            double left = mousePos.X - (rect.ActualWidth / 2);
            double top = mousePos.Y - (rect.ActualHeight / 2);

			if(left > 150 || top > 150 || left < 0 || top < 0)
			{
				return;
			}
            Canvas.SetLeft(rect, left);
            Canvas.SetTop(rect, top);
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			string server = "localhost";
			string database = "test";
			string uid = "user";
			string password = "password";

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
    }
}
