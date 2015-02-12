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

namespace ETD.ViewsPresenters
{
    /// <summary>
    /// Interaction logic for InitialSetup.xaml
    /// </summary>
    public partial class InitialSetup : Window
    {
        public InitialSetup()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if(isFormValid())
            {
                String shiftStartStr = shiftStartDate.SelectedDate.Value.ToShortDateString() +" " + shiftStartTimehh.Text + ":" + shiftStartTimemm.Text;
                DateTime shiftStart = Convert.ToDateTime(shiftStartStr);
                String shiftEndStr = shiftEndDate.SelectedDate.Value.ToShortDateString() + " " + shiftEndTimehh.Text + ":" + shiftEndTimemm.Text;
                DateTime shiftEnd= Convert.ToDateTime(shiftEndStr);

                ETD.Models.Objects.InitialInfo initInfo = new ETD.Models.Objects.InitialInfo(operationName.Text, acronym.Text, shiftStart, shiftEnd, dispatcherName.Text);
                
                MainWindow mw = new MainWindow();
                mw.Show();
                this.Close();
            }
        }

        internal void text_enter(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (((box.Text) == "hh") || ((box.Text) == "mm"))
            box.Clear();
        }

        internal void text_fillhh(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (string.IsNullOrWhiteSpace(box.Text))
                box.Text = "hh";
        }

        internal void text_fillmm(object sender, RoutedEventArgs e)
        {
            TextBox box = sender as TextBox;
            if (string.IsNullOrWhiteSpace(box.Text))
            box.Text = "mm";
        }

        private Boolean isFormValid()
        {
            //operation name 
            
            if (operationName.Text == "")
            {
                MessageBox.Show("Invalid operation name.");
                return false;
            }

            //acronym regex
            Regex acronymRgx = new Regex(@"^[a-zA-Z]{1,3}$");
            if (!acronymRgx.Match(acronym.Text).Success)
            {
                MessageBox.Show("Invalid Acronym.");
                return false;
            }


            //start time regex
            Regex timeRgx = new Regex(@"^[0-9]{2}$");
            if (!timeRgx.Match(shiftStartTimehh.Text).Success || !timeRgx.Match(shiftStartTimemm.Text).Success || (Convert.ToInt32(shiftStartTimehh.Text) < 0 || Convert.ToInt32(shiftStartTimehh.Text) > 24) || (Convert.ToInt32(shiftStartTimemm.Text) < 0 || Convert.ToInt32(shiftStartTimehh.Text) > 59))
            {
                MessageBox.Show("Invalid start time.");
                return false;
            }

            //end time regex
            if (!timeRgx.Match(shiftEndTimehh.Text).Success || !timeRgx.Match(shiftEndTimemm.Text).Success || (Convert.ToInt32(shiftEndTimehh.Text) < 0 || Convert.ToInt32(shiftEndTimehh.Text) > 24) || (Convert.ToInt32(shiftEndTimemm.Text) < 0 || Convert.ToInt32(shiftEndTimehh.Text) > 59))
            {
                MessageBox.Show("Invalid end time.");
                return false;
            }

            //dispatcher
            Regex nameRgx = new Regex(@"^[a-zA-Z '-]+$");
            if (!nameRgx.Match(dispatcherName.Text).Success)
            {
                MessageBox.Show("Invalid dispatcher name.");
                return false;
            }

            //date
            if (shiftStartDate.Text == "")
            {
                MessageBox.Show("Invalid shift start date.");
                return false;
            }
            if (shiftEndDate.Text == "")
            {
                MessageBox.Show("Invalid shift end date.");
                return false;
            }
            return true;
        }

    }
}
