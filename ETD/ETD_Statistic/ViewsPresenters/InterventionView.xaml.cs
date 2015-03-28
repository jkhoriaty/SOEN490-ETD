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
using System.Data.SQLite;
using ETD_Statistic.Model;


namespace ETD_Statistic.ViewsPresenters
{
    /// <summary>
    /// Interaction logic for InterventionView.xaml
    /// </summary>
    public partial class InterventionView : Page
    {
        String complaint;
        int count = 0;
        public InterventionView()
        {
            InitializeComponent();
        }

        private void GenerateComplainName()
        {
            SQLiteDataReader reader = StaticDBConnection.QueryDatabase("SELECT DISTINCT Chief_Complaint FROM Interventions WHERE Operation_ID =" + Statistic.getOperationID());
            while (reader.Read())
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(40);
                ComplaintGrid.RowDefinitions.Add(rd);
                Border border = new Border();
                border.BorderThickness = new Thickness(0,1,1,1);
                border.BorderBrush = new SolidColorBrush(Colors.Black);


                count++;
                
            }
            StaticDBConnection.CloseConnection();
 
        }
    }
}
