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
        String dispatcherName;
        int teamCount;
        int volunteerCount;

        public StatisticView()
        {
            InitializeComponent();
            GenerateStatisticSummary();
        }

        private void GenerateStatisticSummary()
        {
            getOperationInformationFromDatabase();
            getTeamCount();
            getVolunteerCount();
       
            VolunteerFollowUp.TextWrapping = TextWrapping.Wrap;
            Finance.TextWrapping = TextWrapping.Wrap;
            Vehicle.TextWrapping = TextWrapping.Wrap;
            ParticularSituation.TextWrapping = TextWrapping.Wrap;
            OrganizationFollowUp.TextWrapping = TextWrapping.Wrap;
            SupervisorFollowUp.TextWrapping = TextWrapping.Wrap;

            if (Statistic.getListSize() == 1)
            {
                OperationID.Text = "Operation " + Statistic.getOperationID() + " : " + eventName;
                BeginDate.Text = "Start Date: " + startDate.ToString("g");
                EndingDate.Text = "End Date: " + endDate.ToString("g");
                DispatcherName.Text = "Dispatcher: " + dispatcherName;
                TeamCount.Text = "Number of teams: " + teamCount.ToString();
                VolunteerCount.Text = "Number of volunteers: " + volunteerCount.ToString();

                gridVisibility.Visibility = Visibility.Visible;
                VolunteerFollowUp.Text = volunteerFollowUpText;
                Finance.Text = financeText;
                Vehicle.Text = vehicleText;
                ParticularSituation.Text = particularSituationText;
                OrganizationFollowUp.Text = organizationFollowUpText;
                SupervisorFollowUp.Text = supervisorFollowUpText;
            }
            else
            {
                GenerateViewForMultiDayStatistic();
            }
        }

        private void getOperationInformationFromDatabase()
        {
            SQLiteDataReader reader = StaticDBConnection.QueryDatabase("SELECT * FROM Operations WHERE Operation_ID IN "+Statistic.getOperationID());
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
                dispatcherName = reader["Dispatcher"].ToString();
            }
            StaticDBConnection.CloseConnection();
        }

        private void getTeamCount()
        {
            SQLiteDataReader reader = StaticDBConnection.QueryDatabase("SELECT count(*) as Count FROM Teams WHERE Operation_ID IN " + Statistic.getOperationID());
            while (reader.Read())
            {
                teamCount = int.Parse(reader["Count"].ToString());
            }
            StaticDBConnection.CloseConnection();
        }

        private void getVolunteerCount()
        {
            SQLiteDataReader reader = StaticDBConnection.QueryDatabase("SELECT count(*) as Num FROM (Volunteers JOIN Team_Members ON Volunteers.Volunteer_ID = Team_Members.Volunteer_ID) JOIN Teams ON Team_Members.Team_ID = Teams.Team_ID WHERE Operation_ID IN " + Statistic.getOperationID());
            while (reader.Read())
            {
                volunteerCount = int.Parse(reader["Num"].ToString());
            }
            StaticDBConnection.CloseConnection();
        }

        private void GenerateViewForMultiDayStatistic()
        {
            MultiDayOperationID.Text = "Operations: " + Statistic.getOperationID();
            foreach (String i in Statistic.getOperationList())
            {
                SQLiteDataReader reader = StaticDBConnection.QueryDatabase("SELECT * FROM Operations WHERE Operation_ID IN " + Statistic.getOperationID());
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
                    dispatcherName = reader["Dispatcher"].ToString();
                }
            }
            StaticDBConnection.CloseConnection();

        }

    }
}
