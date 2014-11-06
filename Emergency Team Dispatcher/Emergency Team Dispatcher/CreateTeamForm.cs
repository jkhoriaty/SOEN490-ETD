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
		int nbOfMembers;
        public CreateTeamForm(MainWindow parent)
        {
            this.parent = parent;
            InitializeComponent();
			nbOfMembers = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
			Regex timeHoursRgx = new Regex(@"^[0-9][0-9]$");
			Regex timeMinutesRgx = new Regex(@"^[0-5][0-9]$");

			String warning = "";
			Team team = null;
			int teamID = -1;
			var dateNow = DateTime.Now;
			var radiotime = new DateTime();
			int rTraining = -1;
			var fAidtime = new DateTime();
			int fTraining = -1;
			var fAid2time = new DateTime();
			int f2Training = -1;
			
			//Checking team info
			if(teamName.Text == "" || teamName.Text.Length > 6)
            {
               warning += "Team name is invalid.\n";
            }
			else
			{
				team = new Team(teamName.Text);
				//teamID = dbAccess.insertTeam(teamName.Text);
				//MessageBox.Show("" + teamID);
			}

			//
			//Checking radio info
			//
			if (radioName.Text == null || radioName.Text.Length > 32)
            {
                warning += "Radio member name is invalid.\n";
            }

			if (!timeHoursRgx.IsMatch(radioDeparturehh.Text) || !timeMinutesRgx.IsMatch(radioDeparturemm.Text))
			{
				warning += "Radio member time of departure is invalid.\n";
			}
			else
			{
				//Check if the hours are between 0 to 24
				int radioDepInt = int.Parse(radioDeparturehh.Text);
				if (radioDepInt > 23 || radioDepInt < 0)
				{
					warning += "Radio member time of departure is invalid.\n";
				}
				else
				{
					radiotime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, radioDepInt, int.Parse(radioDeparturemm.Text), dateNow.Second);
				}
			}
			
			if(!radioLevelOfTraining.Items.Contains(radioLevelOfTraining.Text))
            {
                warning += "Radio member level of training invalid.\n";
            }
			else
			{
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
			}

			//
            //Checking first aid info
			//
			if (nbOfMembers > 1)
			{
				if (firstAidName.Text == "" || firstAidName.Text.Length > 32)
				{
					warning += "First aid member name is invalid.\n";
				}

				if (!timeHoursRgx.IsMatch(fAidhh.Text) || !timeMinutesRgx.IsMatch(fAidmm.Text))
				{
					warning += "First aid member time of departure is invalid.\n";
				}
				else
				{
					int fAidInt = int.Parse(fAidhh.Text);
					if (fAidInt > 23 || fAidInt < 0)
					{
						warning += "First aid member time of departure is invalid.\n";
					}
					else
					{
						fAidtime = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, fAidInt, int.Parse(fAidmm.Text), dateNow.Second);
					}
				}

				if (!firstAidLevelOfTraining.Items.Contains(firstAidLevelOfTraining.Text))
				{
					warning += "First aid member level of training invalid.\n";
				}
				else
				{
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
				}
			}

			//
			//Checking first aid 2 info
			//
			if (nbOfMembers > 2)
			{
				if (firstAid2Name.Text == "" || firstAid2Name.Text.Length > 32)
				{
					warning += "Second First aid member name is invalid.\n";
				}

				if (!timeHoursRgx.IsMatch(fAid2hh.Text) || !timeMinutesRgx.IsMatch(fAid2mm.Text))
				{
					warning += "Second First aid member time of departure is invalid.\n";
				}
				else
				{
					int fAid2Int = int.Parse(fAid2hh.Text);
					if (fAid2Int > 23 || fAid2Int < 0)
					{
						warning += "Second First aid member time of departure is invalid.\n";
					}
					else
					{
						fAid2time = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, fAid2Int, int.Parse(fAid2mm.Text), dateNow.Second);
					}
				}

				if (!firstAid2LevelOfTraining.Items.Contains(firstAid2LevelOfTraining.Text))
				{
					warning += "Second First aid member level of training invalid.\n";
				}
				else
				{
					if (firstAid2LevelOfTraining.Text == "General First Aid")
					{
						f2Training = 0;
					}
					else if (firstAid2LevelOfTraining.Text == "First Responder")
					{
						f2Training = 1;
					}
					else if (firstAid2LevelOfTraining.Text == "Medicine")
					{
						f2Training = 2;
					}
				}
			}


			if (!warning.Equals(""))
			{
				MessageBox.Show(warning);
				return;
			}

			team.addMember(new TeamMember(radioName.Text, rTraining, radiotime));
			if(nbOfMembers > 1) team.addMember(new TeamMember(firstAidName.Text, fTraining, fAidtime));
			if(nbOfMembers > 2) team.addMember(new TeamMember(firstAid2Name.Text, f2Training, fAid2time));

            //Add to global teams list and increment currentTeam count
            Globals.listOfTeams.Add(Globals.currentTeam, team);
            Globals.currentTeam++;

            //team.addToDB;
            
            MessageBox.Show("Success");
            this.Close();
        }

        private void time_Enter(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void time_Click(object sender, MouseEventArgs e)
		{
			TextBox tb = (TextBox)sender;
			tb.Text = "";
		}

		private void restorehh_Leave(object sender, EventArgs e)
		{
			TextBox tb = (TextBox)sender;
			if(tb.Text.Equals(""))
			{
				tb.Text = "hh";
			}
		}

		private void restoremm_Leave(object sender, EventArgs e)
		{
			TextBox tb = (TextBox)sender;
			if(tb.Text.Equals(""))
			{
				tb.Text = "mm";
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			switch(nbOfMembers)
			{
				case 1:
					this.Size = new System.Drawing.Size(438, 400);
					button1.Location = new System.Drawing.Point(240, 330);
					button2.Location = new System.Drawing.Point(320, 330);
					button3.Location = new System.Drawing.Point(30, 330);
					button4.Visible = true;
					label7.Visible = true;
					label9.Visible = true;
					firstAidName.Visible = true;
					label8.Visible = true;
					fAidhh.Visible = true;
					label11.Visible = true;
					fAidmm.Visible = true;
					label6.Visible = true;
					firstAidLevelOfTraining.Visible = true;
					nbOfMembers = 2;
					firstAidName.Focus();
					break;
				case 2:
					this.Size = new System.Drawing.Size(438, 540);
					button1.Location = new System.Drawing.Point(240, 470);
					button2.Location = new System.Drawing.Point(320, 470);
					button3.Visible = false;
					button4.Location = new System.Drawing.Point(110, 470);
					label12.Visible = true;
					label13.Visible = true;
					firstAid2Name.Visible = true;
					label14.Visible = true;
					fAid2hh.Visible = true;
					label15.Visible = true;
					fAid2mm.Visible = true;
					label16.Visible = true;
					firstAid2LevelOfTraining.Visible = true;
					nbOfMembers = 3;
					firstAid2Name.Focus();
					break;
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			switch (nbOfMembers)
			{
				case 2:
					this.Size = new System.Drawing.Size(438, 260);
					button1.Location = new System.Drawing.Point(240, 190);
					button2.Location = new System.Drawing.Point(320, 190);
					button3.Location = new System.Drawing.Point(30, 190);
					button4.Visible = false;
					label7.Visible = false;
					label9.Visible = false;
					firstAidName.Visible = false;
					label8.Visible = false;
					fAidhh.Visible = false;
					label11.Visible = false;
					fAidmm.Visible = false;
					label6.Visible = false;
					firstAidLevelOfTraining.Visible = false;
					nbOfMembers = 1;
					radioName.Focus();
					break;
				case 3:
					this.Size = new System.Drawing.Size(438, 400);
					button1.Location = new System.Drawing.Point(240, 330);
					button2.Location = new System.Drawing.Point(320, 330);
					button3.Visible = true;
					button4.Location = new System.Drawing.Point(110, 330);
					label12.Visible = false;
					label13.Visible = false;
					firstAid2Name.Visible = false;
					label14.Visible = false;
					fAid2hh.Visible = false;
					label15.Visible = false;
					fAid2mm.Visible = false;
					label16.Visible = false;
					firstAid2LevelOfTraining.Visible = false;
					nbOfMembers = 2;
					firstAidName.Focus();
					break;
			}
		}
    }
}
