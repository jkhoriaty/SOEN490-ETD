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
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using ETD.Services;
using ETD.Services.Database;
using System.Data.SQLite;

namespace ETD.ViewsPresenters
{
    /// <summary>
    /// Interaction logic for InitialSetup.xaml
    /// </summary>
    public partial class InitialSetup : Window
    {
        String shiftStartStr;
        DateTime shiftStart;
        String shiftEndStr;
        DateTime shiftEnd;

        public InitialSetup()
        {
            InitializeComponent();
            Serializer serializer = Serializer.Instance;
            if(serializer.Recoverable())
            {
                MainWindow mw = new MainWindow();
                mw.Show();
                serializer.PerformRecovery();
                serializer.StartBackUp();
                this.Close();
            }

			ComboBoxItem createUserItem = new ComboBoxItem();
			createUserItem.Content = "NEW USER";
			createUserItem.FontStyle = FontStyles.Italic;
			createUserItem.FontWeight = FontWeights.Bold;

			ComboBoxItem createUserItemSupervisor = new ComboBoxItem();
			createUserItemSupervisor.Content = "NEW USER";
			createUserItemSupervisor.FontStyle = FontStyles.Italic;
			createUserItemSupervisor.FontWeight = FontWeights.Bold;

			ComboBoxItem createUserItemOpManager = new ComboBoxItem();
			createUserItemOpManager.Content = "NEW USER";
			createUserItemOpManager.FontStyle = FontStyles.Italic;
			createUserItemOpManager.FontWeight = FontWeights.Bold;

			dispatcherName.Items.Add(createUserItem);
			supervisorName.Items.Add(createUserItemSupervisor);
			opManagerName.Items.Add(createUserItemOpManager);
			

            using (SQLiteDataReader reader = StaticDBConnection.QueryDatabase("SELECT Name FROM Volunteers"))
            {
                while (reader.Read())
                {
                    ComboBoxItem cbItem = new ComboBoxItem();
                    cbItem.Content = reader["Name"].ToString();
                    ComboBoxItem cbItemSupervisor = new ComboBoxItem();
                    cbItemSupervisor.Content = reader["Name"].ToString();
                    ComboBoxItem cbItemOpManager = new ComboBoxItem();
                    cbItemOpManager.Content = reader["Name"].ToString();
                    dispatcherName.Items.Add(cbItem);
                    supervisorName.Items.Add(cbItemSupervisor);
                    opManagerName.Items.Add(cbItemOpManager);
                }
            }
            StaticDBConnection.CloseConnection();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {


            if(IsFormValid()) //if the form is valid, create an instance of mainwindow
            {                
                ETD.Models.Objects.Operation initInfo = new ETD.Models.Objects.Operation(operationName.Text, acronym.Text, shiftStart, shiftEnd, dispatcherName.Text);
                Serializer.Instance.SetOperation(initInfo);
                MainWindow mw = new MainWindow();
                mw.Show();
                Serializer.Instance.StartBackUp();
                this.Close();
            }
 
        }
        internal void Text_Enter(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (((box.Text) == "hh") || ((box.Text) == "mm"))
			{ 
				box.Clear();
			}
        }

        internal void Text_Fillhh(object sender, RoutedEventArgs e) //re-fill box if user hasnt entered hours
        {
            TextBox box = sender as TextBox;
            if (string.IsNullOrWhiteSpace(box.Text))
			{ 
                box.Text = "hh";
			}
        }

        internal void Text_Fillmm(object sender, RoutedEventArgs e) //re-fill minute box if user hasn't entered minutes
        {
            TextBox box = sender as TextBox;
            if (string.IsNullOrWhiteSpace(box.Text))
			{ 
				box.Text = "mm";
			}
        }

        private Boolean IsFormValid() //checks if the user submitted valid values in the form
        {
            //operation name 
            
            if (operationName.Text == "")
            {
                MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_InvalidOperationName);
                return false;
            }

            //acronym regex
            Regex acronymRgx = new Regex(@"^[a-zA-Z]{1,3}$");
            if (!acronymRgx.Match(acronym.Text).Success)
            {
                MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_InvalidAcronym);
                return false;
            }


            //start time regex
            Regex timeRgx = new Regex(@"^[0-9]{2}$");
            if (!timeRgx.Match(shiftStartTimehh.Text).Success || !timeRgx.Match(shiftStartTimemm.Text).Success || (Convert.ToInt32(shiftStartTimehh.Text) < 0 || Convert.ToInt32(shiftStartTimehh.Text) > 24) || (Convert.ToInt32(shiftStartTimemm.Text) < 0 || Convert.ToInt32(shiftStartTimehh.Text) > 59))
            {
                MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_InvalidStartTime);
                return false;
            }

            //end time regex
            if (!timeRgx.Match(shiftEndTimehh.Text).Success || !timeRgx.Match(shiftEndTimemm.Text).Success || (Convert.ToInt32(shiftEndTimehh.Text) < 0 || Convert.ToInt32(shiftEndTimehh.Text) > 24) || (Convert.ToInt32(shiftEndTimemm.Text) < 0 || Convert.ToInt32(shiftEndTimehh.Text) > 59))
            {
                MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_InvalidEndTime);
                return false;
            }

            //dispatcher
            Regex nameRgx = new Regex(@"^[a-zA-Z '-]+$");
            if (!nameRgx.Match(dispatcherName.Text).Success)
            {
                MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_InvalidDispatcherName);
                return false;
            }

            //check startTime < endTime
            shiftStartStr = shiftStartDate.SelectedDate.Value.ToShortDateString() + " " + shiftStartTimehh.Text + ":" + shiftStartTimemm.Text;
            shiftEndStr = shiftEndDate.SelectedDate.Value.ToShortDateString() + " " + shiftEndTimehh.Text + ":" + shiftEndTimemm.Text;
            shiftStart = Convert.ToDateTime(shiftStartStr);
            shiftEnd = Convert.ToDateTime(shiftEndStr);
            if (shiftStart >= shiftEnd)
            {
                MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_StartTimeAfterEndTime);
                return false;
            }


            //date
            if (shiftStartDate.Text == "")
            {
                MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_InvalidStartDate);
                return false;
            }
            if (shiftEndDate.Text == "")
            {
                MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_InvalidEndDate);
                return false;
            }
            return true;
        }

		private void dispatcherName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(dispatcherName.SelectedIndex == 0)
			{
				dispatcherName.Visibility = Visibility.Hidden;
				Textbox_DispatcherName.Visibility = Visibility.Visible;
				Button_OKDispatcherName.Visibility = Visibility.Visible;
				Button_CancelDispatcherName.Visibility = Visibility.Visible;
			}
		}

		private void supervisorName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (supervisorName.SelectedIndex == 0)
			{
				supervisorName.Visibility = Visibility.Hidden;
				Textbox_SupervisorName.Visibility = Visibility.Visible;
				Button_OKSupervisorName.Visibility = Visibility.Visible;
				Button_CancelSupervisorName.Visibility = Visibility.Visible;
			}
		}

		private void opManagerName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (opManagerName.SelectedIndex == 0)
			{
				opManagerName.Visibility = Visibility.Hidden;
				Textbox_OperationManagerName.Visibility = Visibility.Visible;
				Button_OKOperationManagerName.Visibility = Visibility.Visible;
				Button_CancelOperationManagerName.Visibility = Visibility.Visible;
			}
		}

		private void Button_CancelDispatcherName_Click(object sender, RoutedEventArgs e)
		{
			Textbox_DispatcherName.Visibility = Visibility.Hidden;
			Button_OKDispatcherName.Visibility = Visibility.Hidden;
			Button_CancelDispatcherName.Visibility = Visibility.Hidden;
			dispatcherName.Visibility = Visibility.Visible;
			dispatcherName.SelectedIndex = -1;
		}

		private void Button_OKDispatcherName_Click(object sender, RoutedEventArgs e)
		{
			if(Textbox_DispatcherName.Text == "")
			{
				MessageBox.Show("Please enter a user name.");
			}
			else
			{
				StaticDBConnection.NonQueryDatabase("INSERT INTO [Volunteers] (Name, Training_Level) VALUES ('" + Textbox_DispatcherName.Text + "', 0);");
				Textbox_DispatcherName.Visibility = Visibility.Hidden;
				Button_OKDispatcherName.Visibility = Visibility.Hidden;
				Button_CancelDispatcherName.Visibility = Visibility.Hidden;
				dispatcherName.Visibility = Visibility.Visible;

				ComboBoxItem newUser = new ComboBoxItem();
				newUser.Content = Textbox_DispatcherName.Text;
				dispatcherName.Items.Add(newUser);
				dispatcherName.SelectedItem = newUser;
			}
		}

		private void Button_CancelSupervisorName_Click(object sender, RoutedEventArgs e)
		{
			Textbox_SupervisorName.Visibility = Visibility.Hidden;
			Button_OKSupervisorName.Visibility = Visibility.Hidden;
			Button_CancelSupervisorName.Visibility = Visibility.Hidden;
			supervisorName.Visibility = Visibility.Visible;
		}
		private void Button_OKSupervisorName_Click(object sender, RoutedEventArgs e)
		{
			if (Textbox_SupervisorName.Text == "")
			{
				MessageBox.Show("Please enter a user name.");
			}
			else
			{
				StaticDBConnection.NonQueryDatabase("INSERT INTO [Volunteers] (Name, Training_Level) VALUES ('" + Textbox_SupervisorName.Text + "', 0);");
				Textbox_SupervisorName.Visibility = Visibility.Hidden;
				Button_OKSupervisorName.Visibility = Visibility.Hidden;
				Button_CancelSupervisorName.Visibility = Visibility.Hidden;
				supervisorName.Visibility = Visibility.Visible;

				ComboBoxItem newUser = new ComboBoxItem();
				newUser.Content = Textbox_SupervisorName.Text;
				supervisorName.Items.Add(newUser);
				supervisorName.SelectedItem = newUser;
			}
		}
		private void Button_CancelOperationManagerName_Click(object sender, RoutedEventArgs e)
		{
			Textbox_OperationManagerName.Visibility = Visibility.Hidden;
			Button_OKOperationManagerName.Visibility = Visibility.Hidden;
			Button_CancelOperationManagerName.Visibility = Visibility.Hidden;
			opManagerName.Visibility = Visibility.Visible;
		}
		private void Button_OKOperationManagerName_Click(object sender, RoutedEventArgs e)
		{
			if (Textbox_OperationManagerName.Text == "")
			{
				MessageBox.Show("Please enter a user name.");
			}
			else
			{
				StaticDBConnection.NonQueryDatabase("INSERT INTO [Volunteers] (Name, Training_Level) VALUES ('" + Textbox_OperationManagerName.Text + "', 0);");
				Textbox_OperationManagerName.Visibility = Visibility.Hidden;
				Button_OKOperationManagerName.Visibility = Visibility.Hidden;
				Button_CancelOperationManagerName.Visibility = Visibility.Hidden;
				opManagerName.Visibility = Visibility.Visible;

				ComboBoxItem newUser = new ComboBoxItem();
				newUser.Content = Textbox_OperationManagerName.Text;
				opManagerName.Items.Add(newUser);
				opManagerName.SelectedItem = newUser;
			}
		}
    }
}
