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

            Regex timeRgx = new Regex(@"^[a-zA-Z0-9]{2,32}$");
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

            //Convert level of training to int
            int rTraining = -1;
            
            if(radioLevelOfTraining.Text == "General First Aid")
            {
                rTraining = 0;
            }
            else if(radioLevelOfTraining.Text == "First Responder")
            {
                rTraining = 1;
            }
            else if(radioLevelOfTraining.Text == "Medicine")
            {
                rTraining = 2;
            }

            int fTraining = -1;
            if (firstAidLevelOfTraining.Text == "General First Aid")
            {
                fTraining = 0;
            }
            else if (firstAidLevelOfTraining.Text == "First Responder")
            {
                fTraining = 1;
            }
            else if (firstAidLevelOfTraining.Text == "Medicine")
            {
                fTraining = 2;
            }


            TeamMember radioMember = new TeamMember(radioName.Text, rTraining, radioDeparture.Text);
            TeamMember firstAidMember = new TeamMember(firstAidName.Text, fTraining, fAidDeparture.Text);

            team.addMember(radioMember);
            team.addMember(firstAidMember);

            //team.addToDB;
            

            MessageBox.Show(team.getName());
            
        }
    }
}
