using ETD.Models.Objects;
using ETD.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ETD.CustomObjects.CustomUIObjects
{
	class ResourceLine
	{
		private Intervention intervention;
		private Resource resource;

		private TextBox resourceNameTextBox;

		private Label teamNameLabel;

		private Border movingBorder;
		private StackPanel movingStackPanel;
		private TextBox movingHHTextBox;
		private Button movingButton;
		private TextBox movingMMTextBox;

		private Border arrivalBorder;
		private StackPanel arrivalStackPanel;
		private TextBox arrivalHHTextBox;
		private Button arrivalButton;
		private TextBox arrivalMMTextBox;

		//Constructor
		public ResourceLine(Resource resource, Intervention intervention)
		{
			this.intervention = intervention;
			this.resource = resource;
			BuildLine();
			PopulateLine();
		}

		//Create all controls for the resource line and set their attributes
		private void BuildLine()
		{
			resourceNameTextBox = new TextBox();
			resourceNameTextBox.GotFocus += TextBoxes_GotFocus;
			resourceNameTextBox.LostFocus += TextBoxes_LostFocus;
			resourceNameTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
			resourceNameTextBox.VerticalContentAlignment = VerticalAlignment.Center;
			resourceNameTextBox.BorderBrush = new SolidColorBrush(Colors.Black);
			resourceNameTextBox.BorderThickness = new Thickness(0, 1, 1, 0);


			teamNameLabel = new Label();
			teamNameLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
			teamNameLabel.VerticalContentAlignment = VerticalAlignment.Center;
			teamNameLabel.BorderBrush = new SolidColorBrush(Colors.Black);
			teamNameLabel.BorderThickness = new Thickness(1, 1, 1, 0);


			movingBorder = new Border();
			movingBorder.BorderBrush = new SolidColorBrush(Colors.Black);
			movingBorder.BorderThickness = new Thickness(1, 1, 1, 0);

			movingStackPanel = new StackPanel();
			movingStackPanel.Orientation = Orientation.Horizontal;
			movingStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
			movingStackPanel.VerticalAlignment = VerticalAlignment.Center;
			movingBorder.Child = movingStackPanel;

			movingHHTextBox = new TextBox();
			movingHHTextBox.Width = 29;
			movingHHTextBox.GotFocus += TextBoxes_GotFocus;
			movingHHTextBox.LostFocus += TextBoxes_LostFocus;
			movingHHTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
			movingHHTextBox.VerticalContentAlignment = VerticalAlignment.Center;
			movingStackPanel.Children.Add(movingHHTextBox);

			movingButton = new Button();
			movingButton.Width = 10;
			movingButton.Content = ":";
			movingButton.Click += Moving_Click;
			movingButton.HorizontalContentAlignment = HorizontalAlignment.Center;
			movingButton.VerticalContentAlignment = VerticalAlignment.Center;
			movingStackPanel.Children.Add(movingButton);

			movingMMTextBox = new TextBox();
			movingMMTextBox.Width = 29;
			movingMMTextBox.GotFocus += TextBoxes_GotFocus;
			movingMMTextBox.LostFocus += TextBoxes_LostFocus;
			movingMMTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
			movingMMTextBox.VerticalContentAlignment = VerticalAlignment.Center;
			movingStackPanel.Children.Add(movingMMTextBox);

			arrivalBorder = new Border();
			arrivalBorder.BorderBrush = new SolidColorBrush(Colors.Black);
			arrivalBorder.BorderThickness = new Thickness(1, 1, 0, 0);

			arrivalStackPanel = new StackPanel();
			arrivalStackPanel.Orientation = Orientation.Horizontal;
			arrivalStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
			arrivalStackPanel.VerticalAlignment = VerticalAlignment.Center;
			arrivalBorder.Child = arrivalStackPanel;

			arrivalHHTextBox = new TextBox();
			arrivalHHTextBox.Width = 29;
			arrivalHHTextBox.GotFocus += TextBoxes_GotFocus;
			arrivalHHTextBox.LostFocus += TextBoxes_LostFocus;
			arrivalHHTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
			arrivalHHTextBox.VerticalContentAlignment = VerticalAlignment.Center;
			arrivalStackPanel.Children.Add(arrivalHHTextBox);

			arrivalButton = new Button();
			arrivalButton.Width = 10;
			arrivalButton.Content = ":";
			arrivalButton.Click += Arrival_Click;
			arrivalButton.HorizontalContentAlignment = HorizontalAlignment.Center;
			arrivalButton.VerticalContentAlignment = VerticalAlignment.Center;
			arrivalStackPanel.Children.Add(arrivalButton);

			arrivalMMTextBox = new TextBox();
			arrivalMMTextBox.Width = 29;
			arrivalMMTextBox.GotFocus += TextBoxes_GotFocus;
			arrivalMMTextBox.LostFocus += TextBoxes_LostFocus;
			arrivalMMTextBox.HorizontalContentAlignment = HorizontalAlignment.Center;
			arrivalMMTextBox.VerticalContentAlignment = VerticalAlignment.Center;
			arrivalStackPanel.Children.Add(arrivalMMTextBox);
		}

		//Fill the controls with the resource information
		private void PopulateLine()
		{
			resourceNameTextBox.Text = resource.getResourceName();
			teamNameLabel.Content = resource.getTeam().getName();
			
			DateTime moving = resource.getMovingTime();
			movingHHTextBox.Text = moving.Hour.ToString();
			movingMMTextBox.Text = (moving.Minute < 10) ? "0" + moving.Minute.ToString() : moving.Minute.ToString();

			if(resource.hasArrived())
			{
				DateTime arrival = resource.getArrivalTime();
				arrivalHHTextBox.Text = arrival.Hour.ToString();
				arrivalMMTextBox.Text = (arrival.Minute < 10) ? "0" + arrival.Minute.ToString() : arrival.Minute.ToString();
			}
			else
			{
				arrivalHHTextBox.Text = "hh";
				arrivalMMTextBox.Text = "00";
			}
		}

		//Called when the textboxes gain focus
		private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.GotFocus(sender, e);
		}

		//Called when the textboxes lose focus
		private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.LostFocus(sender, e);

			//If the textbox is for the resource name, update the resource
			if((TextBox)sender == resourceNameTextBox)
			{
				UpdateResource();
			}
		}

		//Update the resource with all the information entered
		private void UpdateResource()
		{
			resource.setResourceName(resourceNameTextBox.Text);

			int movinghh = 0;
			int movingmm = 0;
			if (!movingHHTextBox.Text.Equals("hh"))
			{
				try
				{
					movinghh = int.Parse(movingHHTextBox.Text);
					movingmm = int.Parse(movingMMTextBox.Text);

					DateTime moving = DateTime.Now;
					moving = moving.Date + new TimeSpan(movinghh, movingmm, 0);

					//Don't change the time if it wasn't changed, to preserve the correct time span
					if(!(resource.getMovingTime().Hour == moving.Hour && resource.getMovingTime().Minute == moving.Minute))
					{
						resource.setMoving(moving);
					}
				}
				catch (Exception e)
				{ 
					return;
				}
			}

			int arrivalhh = 0;
			int arrivalmm = 0;
			if (!arrivalHHTextBox.Text.Equals("hh"))
			{
				try
				{
					arrivalhh = int.Parse(arrivalHHTextBox.Text);
					arrivalmm = int.Parse(arrivalMMTextBox.Text);
					
					DateTime arrival = DateTime.Now;
					arrival = arrival.Date + new TimeSpan(arrivalhh, arrivalmm, 0);

					//Don't change the time if it wasn't changed, to preserve the correct time span
					if (!(resource.getArrivalTime().Hour == arrival.Hour && resource.getArrivalTime().Minute == arrival.Minute))
					{
						resource.setArrival(arrival);
					}
				}
				catch(Exception e)
				{
					return;
				}
			}

			intervention.ResourceModified();
		}

		//Called when the colon in the moving column is clicked
		private void Moving_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				int hh = int.Parse(movingHHTextBox.Text);
				int mm = int.Parse(movingMMTextBox.Text);
				if (hh != resource.getMovingTime().Hour && mm != resource.getMovingTime().Minute)
				{
					DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, DateTime.Now.Second);
					int offset = (int)DateTime.Now.Subtract(startTime).TotalSeconds;
					if (offset < 0)
					{
						MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_FutureTime);
					}
					else
					{
						resource.setMoving(startTime);
					}
				}
			}
			catch (Exception ex)
			{
                MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_InvalidTime);
			}
		}

		//Called when the colon in the arrival column is clicked
		private void Arrival_Click(object sender, RoutedEventArgs e)
		{
			if (arrivalHHTextBox.Text.Equals("hh"))
			{
				TextBoxHandler.setNow(arrivalHHTextBox, arrivalMMTextBox);
			}

			try
			{
				int hh = int.Parse(arrivalHHTextBox.Text);
				int mm = int.Parse(arrivalMMTextBox.Text);
				DateTime endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, DateTime.Now.Second);
				int offset = (int)DateTime.Now.Subtract(endTime).TotalMinutes;
				if (offset < 0)
				{
                    MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_FutureTime);
				}
				else
				{
					resource.setArrival(endTime);
				}
			}
			catch (Exception ex)
			{
                MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_InvalidTime);
			}

			resource.getTeam().setStatus("intervening");
		}

		//Getters

		public TextBox getResourceNameTextBox()
		{
			return resourceNameTextBox;
		}
		
		public Label getTeamNameLabel()
		{
			return teamNameLabel;
		}

		public Border getMovingBorder()
		{
			return movingBorder;
		}

		public Border getArrivalBorder()
		{
			return arrivalBorder;
		}
	}
}
