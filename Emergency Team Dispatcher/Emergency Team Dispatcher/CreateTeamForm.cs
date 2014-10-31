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
            Regex nameRgx = new Regex(@"^[a-zA-Z]{1,6}$");
            if(!nameRgx.IsMatch(teamName.Text))
            {
                MessageBox.Show("Team name is invalid.");
                return;
            }

            Regex memberNameRgx = new Regex(@"^[a-zA-Z0-9]{2,30}$");
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

            if(!radioLevelOfTraining.Items.Contains(radioLevelOfTraining.Text))
            {
                MessageBox.Show("Radio level of training invalid.");
                return;
            }

            if (!firstAidLevelOfTraining.Items.Contains(firstAidLevelOfTraining.Text))
            {
                MessageBox.Show("First aid level of training invalid.");
                return;
            }

            Regex timeRgx = new Regex(@"^"); //needs to be done
            if(!timeRgx.IsMatch(radioDeparturehh.Text))
            {
                MessageBox.Show("Radio time of departure is invalid.");
                return;
            }

            if (!timeRgx.IsMatch(fAidhh.Text))
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


            TeamMember radioMember = new TeamMember(radioName.Text, rTraining, radioDeparturehh.Text);
            TeamMember firstAidMember = new TeamMember(firstAidName.Text, fTraining, fAidhh.Text);

            team.addMember(radioMember);
            team.addMember(firstAidMember);
            Globals.listOfTeams.Add(Globals.currentTeam, team);
            Globals.currentTeam++;

            MessageBox.Show(Globals.listOfTeams[0].getName());
            //team.addToDB;
            

            MessageBox.Show("Success");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
