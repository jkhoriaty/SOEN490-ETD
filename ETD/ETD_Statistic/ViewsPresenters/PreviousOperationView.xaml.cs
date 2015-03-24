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


namespace ETD_Statistic.ViewsPresenters
{
    /// <summary>
    /// Interaction logic for PreviousOperationView.xaml
    /// </summary>
    public partial class PreviousOperationView : Page
    {
        private TextBlock tb;

        public PreviousOperationView()
        {
            InitializeComponent();
            PopulateOperations();
        }

        private void PopulateOperations()
        {
            SQLiteDataReader reader = StaticDBConnection.QueryDatabase("SELECT * FROM Operations ORDER BY Shift_Start");
            while(reader.Read())
            {
                tb = new TextBlock();
                tb.FontSize = 20;
                tb.FontWeight = FontWeights.Bold;
                tb.Foreground = Brushes.CadetBlue;
                DateTime startDate = Convert.ToDateTime(reader["Shift_Start"].ToString());
                DateTime endDate = Convert.ToDateTime(reader["Shift_End"].ToString());
                tb.Text = "Operation ID: " + reader["Operation_ID"] + " Operation Name: " + reader["Name"] + " Start: " + startDate.ToString("g")+ " End: " + endDate.ToString("g");
                previousOperation.Children.Add(tb);
            }
            StaticDBConnection.CloseConnection();
        }
    }
}
