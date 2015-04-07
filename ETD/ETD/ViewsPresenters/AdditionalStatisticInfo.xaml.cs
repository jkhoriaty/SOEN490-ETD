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
using ETD.Models.Objects;
using ETD.Services.Database;

namespace ETD.ViewsPresenters
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AdditionalStatisticInfo : Window
    {
        public AdditionalStatisticInfo()
        {
            InitializeComponent();
        }

        protected void SavingInformation(Object sender, System.EventArgs e)
        {
            int id = Operation.currentOperation.getID();
            string VolunteerFollowUpText = VolunteerFollowUp.Text;
            string FinanceText = Finance.Text;
            string VehicleText = Vehicle.Text;
            string ParticularSituationText = ParticularSituation.Text;
            string OrganizationFollowUpText = OrganizationFollowUp.Text;
            string SupervisorFollowUpText = SupervisorFollowUp.Text;

            //pushing changes to the db file for extra information at the end of the operation
            StaticDBConnection.NonQueryDatabase("UPDATE [Operations] SET VolunteerFollowUp= '" + VolunteerFollowUpText + "', Finance= '" + FinanceText + "', Vehicle= '" + VehicleText + "', ParticularSituation= '" + ParticularSituationText + "', OrganizationFollowUp= '" + OrganizationFollowUpText + "', SupervisorFollowUp= '" + SupervisorFollowUpText + "' WHERE Operation_ID =" + id + ";");
            MessageBox.Show(Properties.Resources.MessageBox_InformationSaved);
        }
    }
}
