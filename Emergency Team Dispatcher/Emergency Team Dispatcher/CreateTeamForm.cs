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
        MainWindow parent;
        public CreateTeamForm(MainWindow parent)
        {
            this.parent = parent;
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

            Regex timeRgx = new Regex(@"^[0-9][0-9]$"); 
            if(!timeRgx.IsMatch(radioDeparturehh.Text))
            {
                MessageBox.Show("Radio time of departure is invalid.");
                return;
            }


            //Check if the hours are between 0 to 24
            int radioDepInt = Convert.ToInt32(radioDeparturehh.Text);
            radioDepInt = int.Parse(radioDeparturehh.Text);
            if(radioDepInt > 23 || radioDepInt < 0)
            {
                MessageBox.Show("Radio time of departure is invalid.");
                return;
            }


            if (!timeRgx.IsMatch(fAidhh.Text))
            {
                MessageBox.Show("First aid time of departure is invalid.");
                return;
            }

            int fAidInt = Convert.ToInt32(fAidhh.Text);
            fAidInt = int.Parse(fAidhh.Text);
            if (fAidInt > 23 || fAidInt < 0)
            {
                MessageBox.Show("First aid time of departure is invalid.");
                return;
            }

            Regex timeMinutesRgx = new Regex(@"^[0-5][0-9]$");
            if (!timeMinutesRgx.IsMatch(radioDeparturehh.Text))
            {
                MessageBox.Show("Radio time of departure is invalid.");
                return;
            }

            if (!timeMinutesRgx.IsMatch(fAidhh.Text))
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

            var dateNow = DateTime.Now;
            
            var radiotime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, radioDepInt, int.Parse(radioDeparturemm.Text), dateNow.Second);
            var fAidtime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, fAidInt, int.Parse(fAidmm.Text), dateNow.Second);
           
            TeamMember radioMember = new TeamMember(radioName.Text, rTraining, radiotime);
            TeamMember firstAidMember = new TeamMember(firstAidName.Text, fTraining, fAidtime);

            team.addMember(radioMember);
            team.addMember(firstAidMember);

            //Add to global teams list and increment currentTeam count
            Globals.listOfTeams.Add(Globals.currentTeam, team);
            Globals.currentTeam++;

            //team.addToDB;
            

            MessageBox.Show("Success");
            parent.Teamformation();
            this.Close();
        }

        private void time_Click(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
