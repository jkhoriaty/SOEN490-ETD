using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Emergency_Team_Dispatcher
{
    public partial class Team
    {
        String name;
        Label label;
        TeamMember[] members;
        int memberCount = 0;
        System.Windows.Shapes.Rectangle rectangle;

        public Team()
        {
            name = "Alpha";
            members = new TeamMember[10];
            label = new Label();
            label.Content = this.name;
        }
        public Team(String name)
        {
            this.name = name;
            members = new TeamMember[400];
        }

        public void setRectangle(System.Windows.Shapes.Rectangle rect)
        {
            this.rectangle = rect;
            
        }

        public System.Windows.Shapes.Rectangle getRectangle()
        {
            return this.rectangle;

        }

        public void setLabel(System.Windows.Controls.Label lab)
        {
            this.label = lab;

        }

        public System.Windows.Controls.Label getLabel()
        {
            return this.label;

        }

        public bool addMember(TeamMember mem)
        {
            members[memberCount] = mem;
            memberCount++;
            return true;
        }

		public TeamMember getMember(int pos)
		{
			if (pos >= members.Length) return null;
			else return members[pos];
		}

        public void setName(String name)
        {
            this.name = name;
        }
        public String getName()
        {
            return this.name;
        }
        //Generate shape to represent team on screen
        public void draw()
        {

        }
    }
}
