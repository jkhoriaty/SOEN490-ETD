using ETD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace ETD.ViewsPresenters.MapSection
{
	class PinEditor
	{
		private MapSectionPage caller;
		private double teamSize = 40;
		private double equipmentSize = 30;

		public PinEditor(MapSectionPage caller)
		{
			this.caller = caller;
		}

		//Creating a new team pin as a result to the successfull submission of the team form
		public void CreateTeamPin(Team team)
		{
			Grid mainContainer = new Grid();
			mainContainer.Name = team.getName();
			mainContainer.Tag = "team";
			mainContainer.Width = teamSize;
			mainContainer.Height = teamSize;
			mainContainer.MouseLeftButtonDown += new MouseButtonEventHandler(caller.DragStart);
			mainContainer.MouseLeftButtonUp += new MouseButtonEventHandler(caller.DragStop);
			mainContainer.MouseMove += new MouseEventHandler(caller.DragMove);
			caller.Map.Children.Add(mainContainer);

			Rectangle imageRectangle = new Rectangle();
			imageRectangle.Width = teamSize;
			imageRectangle.Height = teamSize;
			ImageBrush img = new ImageBrush();
			img.ImageSource = Services.getImage(team, statuses.available);
			imageRectangle.Fill = img;
			mainContainer.Children.Add(imageRectangle);

			Label nameLabel = new Label();
			nameLabel.Width = teamSize;
			nameLabel.Height = teamSize;
			nameLabel.FontWeight = FontWeights.DemiBold;
			nameLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
			nameLabel.VerticalContentAlignment = VerticalAlignment.Center;
			nameLabel.IsHitTestVisible = false;

			//Adding shadow effect so text appears clearly no matter what the background is
			DropShadowEffect shadow = new DropShadowEffect();
			shadow.ShadowDepth = 3;
			shadow.Direction = 315;
			shadow.Opacity = 1.0;
			shadow.BlurRadius = 3;
			shadow.Color = Colors.White;
			nameLabel.Effect = shadow;

			//Determining size of text and margin to make the text appear perfectly centered
			Thickness labelMargin = nameLabel.Margin;
			nameLabel.Content = team.getName();
			switch (nameLabel.Content.ToString().Length)
			{
				case 1:
					nameLabel.FontSize = 28;
					labelMargin.Top = -10;
					break;
				case 2:
					nameLabel.FontSize = 24;
					labelMargin.Top = -6;
					break;
				case 3:
					nameLabel.FontSize = 20;
					labelMargin.Top = -3;
					break;
				case 4:
				default:
					nameLabel.FontSize = 16;
					labelMargin.Top = -2;
					break;
				case 5:
				case 6:
					nameLabel.FontSize = 12;
					labelMargin.Top = -1;
					break;
			}
			nameLabel.Margin = labelMargin;

			mainContainer.Children.Add(nameLabel);

			//Setting pin in the top-left corner and making sure it does not cover any other item
			caller.SetPinPosition(mainContainer, (teamSize / 2), (teamSize / 2)); //Setting it top corner
			caller.DetectCollision(mainContainer, (teamSize / 2), (teamSize / 2));
		}

		public void CreateEquipmentPin(String equipmentName)
		{
			Grid mainContainer = new Grid();
			mainContainer.Name = equipmentName;
			mainContainer.Tag = "equipment";
			mainContainer.Width = equipmentSize;
			mainContainer.Height = equipmentSize;
			mainContainer.MouseLeftButtonDown += new MouseButtonEventHandler(caller.DragStart);
			mainContainer.MouseLeftButtonUp += new MouseButtonEventHandler(caller.DragStop);
			mainContainer.MouseMove += new MouseEventHandler(caller.DragMove);
			caller.Map.Children.Add(mainContainer);

			Rectangle imageRectangle = new Rectangle();
			imageRectangle.Width = equipmentSize;
			imageRectangle.Height = equipmentSize;
			equipments equipment = (equipments)Enum.Parse(typeof(equipments), equipmentName);
			ImageBrush img = new ImageBrush();
			img.ImageSource = Services.getImage(equipment);
			imageRectangle.Fill = img;
			mainContainer.Children.Add(imageRectangle);

			//Setting pin in the top-left corner and making sure it does not cover any other item
			caller.SetPinPosition(mainContainer, (equipmentSize / 2), (equipmentSize / 2));
			caller.DetectCollision(mainContainer, (equipmentSize / 2), (equipmentSize / 2));
		}

		//Deleting pin using its name
		public void DeletePin(String pinName)
		{
			foreach (Grid grid in caller.Map.Children)
			{
				if (grid.Name.Equals(pinName))
				{
					caller.Map.Children.Remove(grid);
					return;
				}
			}
		}
	}
}
