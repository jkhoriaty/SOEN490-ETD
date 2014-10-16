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
            Regex rgx = new Regex(@"^[a-zA-Z0-9]$");
            if(rgx.IsMatch(teamName.Text))
            {
                Team team = new Team(teamName.Text);
                MessageBox.Show(team.getName());
            }
            else
            {
                MessageBox.Show("Team name is invalid.");
            }
            
        }
    }
}
