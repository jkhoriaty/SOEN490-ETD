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
using ETD_Statistic.Model;
using ETD.Services.Database;
using System.Data.SQLite;

namespace ETD_Statistic.ViewsPresenters
{
    /// <summary>
    /// Interaction logic for StatisticView.xaml
    /// </summary>
    public partial class StatisticView : Page
    {
        DateTime startDate;
        DateTime endDate;
        String volunteerFollowUpText;
        String financeText;
        String vehicleText;
        String particularSituationText;
        String organizationFollowUpText;
        String supervisorFollowUpText;
        String eventName;
        int teamCount;

        public StatisticView()
        {
            InitializeComponent();
            GenerateStatisticSummary();
        }

        private void GenerateStatisticSummary()
        {
            getOperationInformationFromDatabase();
            getTeamCountFromDatabase();
       
            VolunteerFollowUp.TextWrapping = TextWrapping.Wrap;
            Finance.TextWrapping = TextWrapping.Wrap;
            Vehicle.TextWrapping = TextWrapping.Wrap;
            ParticularSituation.TextWrapping = TextWrapping.Wrap;
            OrganizationFollowUp.TextWrapping = TextWrapping.Wrap;
            SupervisorFollowUp.TextWrapping = TextWrapping.Wrap;

            OperationID.Text = "Operation " + Statistic.getOperationID();
            BeginDate.Text = "Start Date: " + startDate.ToString("g");
            EndingDate.Text = "End Date: " + endDate.ToString("g");
            EventName.Text = "Name of Event: " + eventName;
            TeamCount.Text = "Number of teams: " + teamCount.ToString();
            VolunteerFollowUp.Text = volunteerFollowUpText;
            Finance.Text = financeText;
            Vehicle.Text = vehicleText;
            ParticularSituation.Text = particularSituationText;
            OrganizationFollowUp.Text = organizationFollowUpText;
            SupervisorFollowUp.Text = supervisorFollowUpText;           
        }

        private void getOperationInformationFromDatabase()
        {
            SQLiteDataReader reader = StaticDBConnection.QueryDatabase("SELECT * FROM Operations WHERE Operation_ID="+Statistic.getOperationID());
            while (reader.Read())
            {
                              
                startDate = Convert.ToDateTime(reader["Shift_Start"].ToString());
                endDate = Convert.ToDateTime(reader["Shift_End"].ToString());
                volunteerFollowUpText = reader["VolunteerFollowUp"].ToString();
                financeText = reader["Finance"].ToString();
                vehicleText = reader["Vehicle"].ToString();
                particularSituationText = reader["ParticularSituation"].ToString();
                organizationFollowUpText = reader["OrganizationFollowUp"].ToString();
                supervisorFollowUpText = reader["SupervisorFollowUp"].ToString();
                eventName = reader["Name"].ToString();

            }
            StaticDBConnection.CloseConnection();
        }

        private void getTeamCountFromDatabase()
        {
            SQLiteDataReader reader = StaticDBConnection.QueryDatabase("SELECT count(*) as Count from Teams WHERE Operation_ID=" + Statistic.getOperationID());
            while (reader.Read())
            {
                teamCount = int.Parse(reader["Count"].ToString());
            }
            StaticDBConnection.CloseConnection();

        }
    }
}
