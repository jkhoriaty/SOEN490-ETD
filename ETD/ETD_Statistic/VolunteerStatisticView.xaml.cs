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
using System.Data.SQLite;
using ETD_Statistic.Model;
using ETD.Services.Database;

namespace ETD_Statistic
{
    /// <summary>
    /// Interaction logic for VolunteerStatisticView.xaml
    /// </summary>
    public partial class VolunteerStatisticView : UserControl
    {
        public VolunteerStatisticView()
        {
            InitializeComponent();
            GenerateVolunteerHours();
        }

        private void GenerateVolunteerHours()
        {
            //SELECT Volunteers.Name as Name, Joined, Departure FROM Team_Members JOIN Teams ON Teams.Team_ID = Team_Members.Team_ID JOIN Volunteers ON Team_Members.Volunteer_ID = Volunteers.Volunteer_ID WHERE Operation_ID IN (4)
            SQLiteDataReader reader = StaticDBConnection.QueryDatabase("SELECT Volunteers.Name as Name, Joined, Departure FROM Team_Members JOIN Teams ON Teams.Team_ID = Team_Members.Team_ID JOIN Volunteers ON Team_Members.Volunteer_ID = Volunteers.Volunteer_ID WHERE Operation_ID IN " + Statistic.getOperationID());
            while (reader.Read())
            {
                TextBlock tb = new TextBlock();
                tb.FontWeight = FontWeights.Bold;
                tb.Height = 40;
                tb.FontSize = 13;
                String volunteer = reader["Name"].ToString();
                DateTime start = Convert.ToDateTime(reader["Joined"].ToString());
                DateTime end = Convert.ToDateTime(reader["Departure"].ToString());
                TimeSpan timeDiff = Statistic.getDateDifference(start, end);
                tb.Text = volunteer + timeDiff.ToString();
                volunteerStats.Children.Add(tb);

                
            }
            StaticDBConnection.CloseConnection();

        }

    }
}
