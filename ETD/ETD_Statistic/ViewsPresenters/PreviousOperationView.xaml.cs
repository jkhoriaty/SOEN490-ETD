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
using ETD.Services.Database;
using System.IO;
using System.Data.SQLite;
using ETD_Statistic.Model;
using System.Collections;

namespace ETD_Statistic.ViewsPresenters
{
    /// <summary>
    /// Interaction logic for PreviousOperationView.xaml
    /// </summary>
    public partial class PreviousOperationView : Page
    {
        private TextBlock tb;
        private Boolean operationCheck = false;

        public PreviousOperationView()
        {
            InitializeComponent();
            PopulateOperations();
        }

        private void PopulateOperations()
        {
            Statistic.clearOperationsList();
            SQLiteDataReader reader = StaticDBConnection.QueryDatabase("SELECT * FROM Operations ORDER BY Shift_Start");
            while(reader.Read())
            {        
                operationCheck = true;
                CheckBox cb = new CheckBox();
                cb.FontSize = 20;
                cb.FontWeight = FontWeights.Bold;
                cb.Foreground = Brushes.DarkSlateBlue;
                cb.Name = "operation" + reader["Operation_ID"];
                cb.Checked += OperationChecked;
                cb.Unchecked += OperationUnchecked;
                DateTime startDate = Convert.ToDateTime(reader["Shift_Start"].ToString());
                DateTime endDate = Convert.ToDateTime(reader["Shift_End"].ToString());
                cb.Content = "Operation ID: " + reader["Operation_ID"] + " Operation Name: " + reader["Name"] + " Start: " + startDate.ToString("g") + " End: " + endDate.ToString("g");
                //cb.MouseLeftButtonDown += new MouseButtonEventHandler(OperationClicked);
                previousOperation.Children.Add(cb);
            }
            StaticDBConnection.CloseConnection();

            if (operationCheck == true)
            {
                Button submitButton = new Button();
                submitButton.Content = "Submit";
                submitButton.Width = 100;
                submitButton.Margin = new Thickness(0, 50, 0, 0);
                submitButton.Click += OperationSubmit;
                previousOperation.Children.Add(submitButton);
            }
        }

        //function to display statistic for selected operations upon submit button click
        private void OperationSubmit(object sender, RoutedEventArgs e)
        {
            LoadStatistic(sender, e);
        }

        //function to add to operation list when checkbox is checked
        private void OperationChecked(object sender, RoutedEventArgs e)
        {
            CheckBox check = sender as CheckBox;
            Statistic.setOperationID(check.Name.ToString());
        }

        //function to remove from operation list when checkbox is unchecked
        private void OperationUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox uncheck = sender as CheckBox;
            Statistic.removeOperationID(uncheck.Name.ToString());
        }

        private void LoadStatistic(object sender, RoutedEventArgs e)
        {
            previousOperation.Children.Clear();
            StatisticView sv = new StatisticView();
            InterventionView iv = new InterventionView();
            Frame interView = new Frame();
            Frame statsView = new Frame();
            statsView.Content = sv;
            interView.Content = iv;
            previousOperation.Children.Add(statsView);
            previousOperation.Children.Add(interView);              
        }
    }
}
