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
            //StaticDBConnection.NonQueryDatabase("UPDATE [Team_Members] SET Disbanded='" + StaticDBConnection.DateTimeSQLite(DateTime.Now) + "' WHERE Volunteer_ID=" + team.getMember(0).getID() + " AND Team_ID=" + team.getID() + ";");
            MessageBox.Show("The information have been saved!");
        }
    }
}
