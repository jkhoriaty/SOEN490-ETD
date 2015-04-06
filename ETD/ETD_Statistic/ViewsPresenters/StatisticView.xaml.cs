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
        OperationStatisticMapper osm = new OperationStatisticMapper();
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
            foreach(OperationStatistic os in osm.getList())
            {
                startDate = os.getStartDate();
                endDate = os.getEndDate();
                volunteerFollowUpText = os.getVolunteerFollowup();
                financeText = os.getFinance();
                vehicleText = os.getVehicle();
                particularSituationText = os.getParticularSituation();
                organizationFollowUpText = os.getOrganizationFollowup();
                supervisorFollowUpText = os.getSupervisorFollowup();
                eventName = os.getEventName();
                dispatcherName = os.getDispatcherName();
            }
        }

        private void getTeamCount()
        {
            teamCount = osm.getTeamCountFromDB();          
        }

        private void getVolunteerCount()
        {
            volunteerCount = osm.getVolunteerCountFromDB();
        }

        private void GenerateViewForMultiDayStatistic()
        {
            MultiDayOperationID.Text = "Operations: " + Statistic.getOperationID();
            foreach (String i in Statistic.getOperationList())
            {
                foreach (OperationStatistic os in osm.getList())
                {
                    startDate = os.getStartDate();
                    endDate = os.getEndDate();
                    volunteerFollowUpText = os.getVolunteerFollowup();
                    financeText = os.getFinance();
                    vehicleText = os.getVehicle();
                    particularSituationText = os.getParticularSituation();
                    organizationFollowUpText = os.getOrganizationFollowup();
                    supervisorFollowUpText = os.getSupervisorFollowup();
                    eventName = os.getEventName();
                    dispatcherName = os.getDispatcherName();
                }
            }

        }

    }
}
