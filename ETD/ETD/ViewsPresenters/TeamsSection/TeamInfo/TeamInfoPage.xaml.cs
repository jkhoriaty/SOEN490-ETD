﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ETD.Models.Objects;
using ETD.Services;
using ETD.Models.ArchitecturalObjects;

namespace ETD.ViewsPresenters.TeamsSection.TeamInfo
{
	/// <summary>
	/// Interaction logic for TeamInfoPage.xaml
	/// </summary>
	public partial class TeamInfoPage : Page, Observer
	{
		TeamsSectionPage teamsSection;
        Team team;

		public TeamInfoPage(TeamsSectionPage teamsSection, Team team)
		{
			InitializeComponent();
			this.teamsSection = teamsSection;
			this.team = team;
			populateInfo();
            teamName.ContextMenu = (ContextMenu)TeamContextName;

			team.RegisterInstanceObserver(this);
		}

		public void Update()
		{
			populateInfo();
		}

		//Filling up the page with the information on the team
		private void populateInfo()
		{
            equipmentStackPanel.Children.RemoveRange(1, equipmentStackPanel.Children.Count - 1);

			teamName.Name = team.getName();
			if (team.getName().Length == 1)
			{
				teamName.Text = TechnicalServices.getPhoneticLetter(team.getName());
			}
			else
			{
				teamName.Text = team.getName();
			}

			ImageBrush img = new ImageBrush();
			img.ImageSource = TechnicalServices.getImage(team.getHighestLevelOfTraining());
			teamTraining.Fill = img;

            List<Equipment> equipmentList = team.getEquipmentList();
            //MessageBox.Show(equipmentList.Count().ToString());

            foreach (Equipment eq in equipmentList)
            {
                Rectangle imageRectangle = new Rectangle();
                //imageRectangle.Name = eq.getEquipmentType().ToString();
                imageRectangle.Tag = teamName;
                imageRectangle.Width = 27;
                imageRectangle.Height = 27;
                //imageRectangle.MouseRightButtonDown += new MouseButtonEventHandler(RemoveTeamEquipment);
                imageRectangle.FlowDirection = FlowDirection.LeftToRight;

                Thickness equipmentMargin = imageRectangle.Margin;
                equipmentMargin.Right = 1;
                imageRectangle.Margin = equipmentMargin;

                //Getting the background image to the rectangle
                ImageBrush equipmentImage = new ImageBrush();
                equipmentImage.ImageSource = TechnicalServices.getImage(eq.getEquipmentType());
                imageRectangle.Fill = equipmentImage;

                //Getting the appropriate equipment StackPanel
                equipmentStackPanel.Children.Add(imageRectangle);
            }

			TeamMember member = null;
			int position = 0;
			while ((member = team.getMember(position++)) != null)
			{
				Grid memberLine = (Grid)informations.Children[position];
				memberLine.Visibility = System.Windows.Visibility.Visible;
				member.setNameGrid(memberLine);

				Label memberName = (Label)memberLine.Children[0];
				memberName.Content = member.getName();     
				memberName.ToolTip = DepartureTimeToString(member);
    
				Rectangle memberTraining = (Rectangle)memberLine.Children[1];
				ImageBrush img2 = new ImageBrush();
				img2.ImageSource = TechnicalServices.getImage(member.getTrainingLevel());
				memberTraining.Fill = img2;
			}
		}

        public void RemoveTeamEquipment(object sender, RoutedEventArgs e)
        {
            Rectangle equipment = (Rectangle)sender;
            String equipmentName = equipment.Name;
            Equipment tempEquipment = new Equipment(equipmentName);
            team.RemoveEquipment(tempEquipment);
            populateInfo();
        }

		private String DepartureTimeToString(TeamMember member)
		{
			String departurehh = member.getDeparture().Hour.ToString();
			String departuremm = member.getDeparture().Minute.ToString();
			if(departuremm.Length == 1)
			{
				departuremm = "0" + departuremm;
			}
			return departurehh + ":" + departuremm;
		}

		//Right click on the team to remove it from the team list
		internal void DeleteTeam(object sender, RoutedEventArgs e)
		{                      
			Team.DeleteTeam(team);
		}

        internal Team getTeam()
        {
            return team;
        }
	}
}
