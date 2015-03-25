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

    }
}
