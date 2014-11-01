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
            Regex memberNameRgx = new Regex(@"^[a-zA-Z0-9]{2,30}$");
			Regex timeRgx = new Regex(@"^[0-9][0-9]$");
			Regex timeMinutesRgx = new Regex(@"^[0-5][0-9]$");

			String warning = "";

            if(!nameRgx.IsMatch(teamName.Text))
            {
               warning += "Team name is invalid.\n";
            }

            if (!memberNameRgx.IsMatch(radioName.Text))
            {
                warning += "Radio member name is invalid.\n";
            }
            if (!memberNameRgx.IsMatch(firstAidName.Text))
            {
                warning += "First aid member name is invalid.\n";
            }

            if(!radioLevelOfTraining.Items.Contains(radioLevelOfTraining.Text))
            {
                warning += "Radio level of training invalid.\n";
            }

            if (!firstAidLevelOfTraining.Items.Contains(firstAidLevelOfTraining.Text))
            {
                warning += "First aid level of training invalid.\n";
            }

            if(!timeRgx.IsMatch(radioDeparturehh.Text))
            {
                warning += "Radio time of departure is invalid.\n";
            }


            //Check if the hours are between 0 to 24
			//int radioDepInt = Convert.ToInt32(radioDeparturehh.Text);
			int radioDepInt = int.Parse(radioDeparturehh.Text);
            if(radioDepInt > 23 || radioDepInt < 0)
            {
                warning += "Radio time of departure is invalid.\n";
            }


            if (!timeRgx.IsMatch(fAidhh.Text))
            {
                warning += "First aid time of departure is invalid.\n";
            }

			//int fAidInt = Convert.ToInt32(fAidhh.Text);
			int fAidInt = int.Parse(fAidhh.Text);
            if (fAidInt > 23 || fAidInt < 0)
            {
                warning += "First aid time of departure is invalid.\n";
            }

            if (!timeMinutesRgx.IsMatch(radioDeparturehh.Text))
            {
                warning += "Radio time of departure is invalid.\n";
            }

            if (!timeMinutesRgx.IsMatch(fAidhh.Text))
            {
                warning += "First aid time of departure is invalid.\n";
            }

			if(!warning.Equals(""))
			{
				MessageBox.Show(warning);
				return;
			}

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
           
			//Creation of team
            TeamMember radioMember = new TeamMember(radioName.Text, rTraining, radiotime);
            TeamMember firstAidMember = new TeamMember(firstAidName.Text, fTraining, fAidtime);

            Team team = new Team(teamName.Text);
            team.addMember(radioMember);
            team.addMember(firstAidMember);

            //Add to global teams list and increment currentTeam count
            Globals.listOfTeams.Add(Globals.currentTeam, team);
            Globals.currentTeam++;

            //team.addToDB;
            

            MessageBox.Show("Success");
            parent.TeamDisplay();
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
			//Resizing form to fit last section
			this.Size = new System.Drawing.Size(438, 540);

			//Moving buttons down to the bottom of the form, and erasing the add member button
			button1.Location = new System.Drawing.Point(120, 466);
			button1.TabIndex = 13;
			button2.Location = new System.Drawing.Point(230, 466);
			button2.TabIndex = 14;
			button3.Visible = false;
			button3.TabIndex = 15;
			button3.TabStop = false;
			
			//Adding new section to the form to fill in 3rd member information
			Label label12 = new System.Windows.Forms.Label();
			label12.AutoSize = true;
			label12.Enabled = false;
			label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label12.Location = new System.Drawing.Point(25, 326);
			label12.Name = "label12";
			label12.Size = new System.Drawing.Size(205, 20);
			label12.TabIndex = 12;
			label12.Text = "Team Member (Second First Aid Kit)";

			//Name
			Label label13 = new System.Windows.Forms.Label();
			label13.AutoSize = true;
			label13.Enabled = false;
			label13.Location = new System.Drawing.Point(55, 361);
			label13.Name = "label13";
			label13.Size = new System.Drawing.Size(35, 13);
			label13.TabIndex = 18;
			label13.Text = "Name";

			TextBox firstAidName2 = new System.Windows.Forms.TextBox();
			firstAidName2.Location = new System.Drawing.Point(253, 361);
			firstAidName2.Name = "firstAidName2";
			firstAidName2.Size = new System.Drawing.Size(125, 20);
			firstAidName2.TabIndex = 9;

			//Time of departure
			Label label14 = new System.Windows.Forms.Label();
			label14.AutoSize = true;
			label14.Enabled = false;
			label14.Location = new System.Drawing.Point(55, 395);
			label14.Name = "label14";
			label14.Size = new System.Drawing.Size(205, 20);
			label14.TabIndex = 12;
			label14.Text = "Time of Departure (24h format)";

			TextBox fAid2hh = new System.Windows.Forms.TextBox();
			fAid2hh.Location = new System.Drawing.Point(253, 395);
			fAid2hh.MaxLength = 2;
			fAid2hh.Name = "fAid2hh";
			fAid2hh.Size = new System.Drawing.Size(25, 20);
			fAid2hh.TabIndex = 10;
			fAid2hh.Text = "hh";
			fAid2hh.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			fAid2hh.MouseClick += new System.Windows.Forms.MouseEventHandler(this.time_Click);
			fAid2hh.Leave += new System.EventHandler(this.restorehh_Leave);

			Label label15 = new System.Windows.Forms.Label();
			label15.AutoSize = true;
			label15.Enabled = false;
			label15.Location = new System.Drawing.Point(280, 395);
			label15.Name = "label15";
			label15.Size = new System.Drawing.Size(10, 13);
			label15.TabIndex = 20;
			label15.Text = ":";

			TextBox fAid2mm = new System.Windows.Forms.TextBox();
			fAid2mm.Location = new System.Drawing.Point(292, 395);
			fAid2mm.MaxLength = 2;
			fAid2mm.Name = "fAid2mm";
			fAid2mm.Size = new System.Drawing.Size(25, 20);
			fAid2mm.TabIndex = 11;
			fAid2mm.Text = "mm";
			fAid2mm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			fAid2mm.MouseClick += new System.Windows.Forms.MouseEventHandler(this.time_Click);
			fAid2mm.Leave += new System.EventHandler(this.restoremm_Leave);

			//Level of training
			Label label16 = new System.Windows.Forms.Label();
			label16.AutoSize = true;
			label16.Enabled = false;
			label16.Location = new System.Drawing.Point(55, 427);
			label16.Name = "label16";
			label16.Size = new System.Drawing.Size(88, 13);
			label16.TabIndex = 11;
			label16.Text = "Level Of Training";

			ComboBox firstAid2LevelOfTraining = new System.Windows.Forms.ComboBox();
			firstAid2LevelOfTraining.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			firstAid2LevelOfTraining.FormattingEnabled = true;
			firstAid2LevelOfTraining.Items.AddRange(new object[] {
            "General First Aid",
            "First Responder",
            "Medicine"});
			firstAid2LevelOfTraining.Location = new System.Drawing.Point(253, 427);
			firstAid2LevelOfTraining.Name = "firstAid2LevelOfTraining";
			firstAid2LevelOfTraining.Size = new System.Drawing.Size(121, 21);
			firstAid2LevelOfTraining.TabIndex = 12;

			this.Controls.AddRange(new Control[] { label12, label13, firstAidName2, label14, fAid2hh, label15, fAid2mm, label16, firstAid2LevelOfTraining});
			firstAidName2.Focus();
		}
    }
}
