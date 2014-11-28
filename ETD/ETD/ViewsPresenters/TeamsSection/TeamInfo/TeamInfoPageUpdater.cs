using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;

using ETD.Models;

namespace ETD.ViewsPresenters.TeamsSection.TeamInfo
{
	class TeamInfoPageUpdater
	{
		TeamInfoPage caller;

		public TeamInfoPageUpdater(TeamInfoPage caller)
		{
			this.caller = caller;
		}

		public void setTeamInfo(String name, String PhoneticName, BitmapImage training)
		{
			caller.page.Name = name;

			caller.teamName.Content = PhoneticName;
			caller.teamName.Name = name;

			ImageBrush img = new ImageBrush();
			img.ImageSource = training;
			caller.teamTraining.Fill = img;
		}

		public void setMemberInfo(int position, String name, BitmapImage training)
		{
			Grid memberLine = (Grid)caller.informations.Children[position];
			memberLine.Visibility = System.Windows.Visibility.Visible;

			Label memberName = (Label)memberLine.Children[0];
			memberName.Content = name;

			Rectangle memberTraining = (Rectangle)memberLine.Children[1];
			ImageBrush img = new ImageBrush();
			img.ImageSource = training;
			memberTraining.Fill = img;
		}
	}
}
