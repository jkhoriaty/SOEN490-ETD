using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace Emergency_Team_Dispatcher
{
    public partial class CreateTeamForm : Form
    {
        public CreateTeamForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex nameRgx = new Regex(@"^[a-zA-Z0-9]$");
            if(!nameRgx.IsMatch(teamName.Text))
            {
                MessageBox.Show("Team name is invalid.");
                return;
            }

            Regex memberNameRgx = new Regex(@"^[a-zA-Z0-9]{2,10}$");
            if (!memberNameRgx.IsMatch(radioName.Text))
            {
                MessageBox.Show("Radio member name is invalid.");
                return;
            }
            if (!memberNameRgx.IsMatch(firstAidName.Text))
            {
                MessageBox.Show("First aid member name is invalid.");
                return;
            }

            if (!(radioLevelOfTraining.Text == "General First Aid" || radioLevelOfTraining.Text == "First Responder" || radioLevelOfTraining.Text == "Medicine"))
            {
                MessageBox.Show("Radio level of training invalid.");
                return;
            }

            if (!(firstAidLevelOfTraining.Text == "General First Aid" || radioLevelOfTraining.Text == "First Responder" || radioLevelOfTraining.Text == "Medicine"))
            {
                MessageBox.Show("First aid level of training invalid.");
                return;
            }

            Regex timeRgx = new Regex(@"^[0-2][0-4]:[0-5][0-9]$");
            if(!timeRgx.IsMatch(radioDeparture.Text))
            {
                MessageBox.Show("Radio time of departure is invalid.");
                return;
            }

            if (!timeRgx.IsMatch(fAidDeparture.Text))
            {
                MessageBox.Show("First aid time of departure is invalid.");
                return;
            }

            Team team = new Team(teamName.Text);
            MessageBox.Show(team.getName());
            
        }
    }
}
