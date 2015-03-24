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

namespace ETD_Statistic.ViewsPresenters
{
    /// <summary>
    /// Interaction logic for StatisticView.xaml
    /// </summary>
    public partial class StatisticView : Page
    {
        public StatisticView()
        {
            InitializeComponent();
            GenerateStatisticSummary();
        }

        private void GenerateStatisticSummary()
        {                    
            VolunteerFollowUp.TextWrapping = TextWrapping.Wrap;
            Finance.TextWrapping = TextWrapping.Wrap;
            Vehicle.TextWrapping = TextWrapping.Wrap;
            ParticularSituation.TextWrapping = TextWrapping.Wrap;
            OrganizationFollowUp.TextWrapping = TextWrapping.Wrap;
            SupervisorFollowUp.TextWrapping = TextWrapping.Wrap;

            VolunteerFollowUp.Text = "Volunteer TO BE COMPLETEDVolunteer TO BE COMPLETEDVolunteer TO BE COMPLETEDVolunteer TO BE COMPLETEDVolunteer TO BE COMPLETEDVolunteer TO BE COMPLETEDVolunteer TO BE COMPLETEDVolunteer TO BE COMPLETED";
            Finance.Text = "TO BE COMPLETED TO BE COMPLETED D";
            Vehicle.Text = "TO BE COMPLETED TO BE COMPLETED  TO BE COMPLETED TO BE COMPLETED TO BE COMPLETED";
            ParticularSituation.Text = "TO BE COMPLETED TO BE COMPLETED  TO BE COMPLETED TO BE COMPLETED TO BE COMPLETED";
            OrganizationFollowUp.Text = "TO BE COMPLETED TO BE COMPLETED  TO BE COMPLETED TO BE COMPLETED TO BE COMPLETED";
            SupervisorFollowUp.Text = "TO BE COMPLETED TO BE COMPLETED  TO BE COMPLETED TO BE COMPLETED TO BE COMPLETED";           
        }
    }
}
