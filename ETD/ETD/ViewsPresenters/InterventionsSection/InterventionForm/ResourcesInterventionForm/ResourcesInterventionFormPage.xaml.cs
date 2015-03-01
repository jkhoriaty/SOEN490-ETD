using System;
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

namespace ETD.ViewsPresenters.InterventionsSection.InterventionForm.ResourcesInterventionForm
{
	/// <summary>
	/// Interaction logic for ResourcesInterventionForm.xaml
	/// </summary>
	public partial class ResourcesInterventionFormPage : Page
	{
		private InterventionFormPage interventionForm;
		private Intervention intervention;
		private Dictionary<String, TextBox[]> movingMap = new Dictionary<String, TextBox[]>();
		private Dictionary<String, TextBox[]> arrivalMap = new Dictionary<String, TextBox[]>();
		private Dictionary<String, TextBox[]> informationMap = new Dictionary<String, TextBox[]>();
		private List<Button> movingButtons = new List<Button>();
		private List<Button> arrivalButtons = new List<Button>();
        private Team team;
		private int addResourcesOffset = 0;
        private bool changed;

		public ResourcesInterventionFormPage(InterventionFormPage interventionForm, Intervention intervention)
		{
			InitializeComponent();
			Resources1.TextChanged += ResourcesChanged;
			this.interventionForm = interventionForm;
			this.intervention = intervention;

			setupMovingMap();
			setupArrivalMap();
			setupInformationMap();
			setupMovingButtons();
			setupArrivalButtons();
            FillForm();
		}

		private void setupMovingMap()
		{
			movingMap.Add("Moving1", TextBoxHandler.textboxArray(Movinghh1, Movingmm1));
			movingMap.Add("Moving2", TextBoxHandler.textboxArray(Movinghh2, Movingmm2));
			movingMap.Add("Moving3", TextBoxHandler.textboxArray(Movinghh3, Movingmm3));
			movingMap.Add("Moving4", TextBoxHandler.textboxArray(Movinghh4, Movingmm4));
			movingMap.Add("Moving5", TextBoxHandler.textboxArray(Movinghh5, Movingmm5));
			movingMap.Add("Moving6", TextBoxHandler.textboxArray(Movinghh6, Movingmm6));
			movingMap.Add("Moving7", TextBoxHandler.textboxArray(Movinghh7, Movingmm7));
			movingMap.Add("Moving8", TextBoxHandler.textboxArray(Movinghh8, Movingmm8));
			movingMap.Add("Moving9", TextBoxHandler.textboxArray(Movinghh9, Movingmm9));
			movingMap.Add("Moving10", TextBoxHandler.textboxArray(Movinghh10, Movingmm10));
		}

		private void setupArrivalMap()
		{
			arrivalMap.Add("Arrival1", TextBoxHandler.textboxArray(Arrivalhh1, Arrivalmm1));
			arrivalMap.Add("Arrival2", TextBoxHandler.textboxArray(Arrivalhh2, Arrivalmm2));
			arrivalMap.Add("Arrival3", TextBoxHandler.textboxArray(Arrivalhh3, Arrivalmm3));
			arrivalMap.Add("Arrival4", TextBoxHandler.textboxArray(Arrivalhh4, Arrivalmm4));
			arrivalMap.Add("Arrival5", TextBoxHandler.textboxArray(Arrivalhh5, Arrivalmm5));
			arrivalMap.Add("Arrival6", TextBoxHandler.textboxArray(Arrivalhh6, Arrivalmm6));
			arrivalMap.Add("Arrival7", TextBoxHandler.textboxArray(Arrivalhh7, Arrivalmm7));
			arrivalMap.Add("Arrival8", TextBoxHandler.textboxArray(Arrivalhh8, Arrivalmm8));
			arrivalMap.Add("Arrival9", TextBoxHandler.textboxArray(Arrivalhh9, Arrivalmm9));
			arrivalMap.Add("Arrival10", TextBoxHandler.textboxArray(Arrivalhh10, Arrivalmm10));
		}

		private void setupInformationMap()
		{
			informationMap.Add("Moving1", TextBoxHandler.textboxArray(Team1, Resources1));
			informationMap.Add("Moving2", TextBoxHandler.textboxArray(Team2, Resources2));
			informationMap.Add("Moving3", TextBoxHandler.textboxArray(Team3, Resources3));
			informationMap.Add("Moving4", TextBoxHandler.textboxArray(Team4, Resources4));
			informationMap.Add("Moving5", TextBoxHandler.textboxArray(Team5, Resources5));
			informationMap.Add("Moving6", TextBoxHandler.textboxArray(Team6, Resources6));
			informationMap.Add("Moving7", TextBoxHandler.textboxArray(Team7, Resources7));
			informationMap.Add("Moving8", TextBoxHandler.textboxArray(Team8, Resources8));
			informationMap.Add("Moving9", TextBoxHandler.textboxArray(Team9, Resources9));
			informationMap.Add("Moving10", TextBoxHandler.textboxArray(Team10, Resources10));
		}

		private void setupMovingButtons()
		{
			movingButtons.Add(Moving1);
			movingButtons.Add(Moving2);
			movingButtons.Add(Moving3);
			movingButtons.Add(Moving4);
			movingButtons.Add(Moving5);
			movingButtons.Add(Moving6);
			movingButtons.Add(Moving7);
			movingButtons.Add(Moving8);
			movingButtons.Add(Moving9);
			movingButtons.Add(Moving10);
		}

		private void setupArrivalButtons()
		{
			arrivalButtons.Add(Arrival1);
			arrivalButtons.Add(Arrival2);
			arrivalButtons.Add(Arrival3);
			arrivalButtons.Add(Arrival4);
			arrivalButtons.Add(Arrival5);
			arrivalButtons.Add(Arrival6);
			arrivalButtons.Add(Arrival7);
			arrivalButtons.Add(Arrival8);
			arrivalButtons.Add(Arrival9);
			arrivalButtons.Add(Arrival10);
		}

		private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.GotFocus(sender, e);
		}

		private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.LostFocus(sender, e);
            if (changed)
            {
                TextBox tb = (TextBox)sender;
                int index = int.Parse(tb.Name.ToCharArray()[tb.Name.Length - 1].ToString());
                UpdateResource(index - 1, movingMap["Moving" + index][0], movingMap["Moving" + index][1], arrivalMap["Arrival" + index][0], arrivalMap["Arrival" + index][1], informationMap["Moving" + index][1], informationMap["Moving" + index][0]);
                changed = false;
            }
		}

		//Display an extra resource line when filling out the last available line
		private void TeamChanged(object sender, RoutedEventArgs e)
		{
            changed = true;
			TextBox tb = (TextBox)sender;
			if(tb.Name.Equals("Team1"))
			{
				interventionForm.setTeam(tb.Text);
			}
			else if (tb.Name.Equals("Team4"))
			{
				MainGrid.RowDefinitions[5].Height = new GridLength(23);
			}
			else if(tb.Name.Equals("Team5"))
			{
				MainGrid.RowDefinitions[6].Height = new GridLength(23);
			}
			else if(tb.Name.Equals("Team6"))
			{
				MainGrid.RowDefinitions[7].Height = new GridLength(23);
			}
			else if(tb.Name.Equals("Team7"))
			{
				MainGrid.RowDefinitions[8].Height = new GridLength(23);
			}
			else if(tb.Name.Equals("Team8"))
			{
				MainGrid.RowDefinitions[9].Height = new GridLength(23);
			}
			else if(tb.Name.Equals("Team9"))
			{
				MainGrid.RowDefinitions[10].Height = new GridLength(23);
			}
		}

		public void PersistencyUpdate()
		{
			if(!Team1.Text.Equals(""))
			{
				UpdateResource(0, Movinghh1, Movingmm1, Arrivalhh1, Arrivalmm1, Resources1, Team1);
			}

			if (!Team2.Text.Equals(""))
			{
				UpdateResource(1, Movinghh2, Movingmm2, Arrivalhh2, Arrivalmm2, Resources2, Team2);
			}

			if (!Team3.Text.Equals(""))
			{
				UpdateResource(2, Movinghh3, Movingmm3, Arrivalhh3, Arrivalmm3, Resources3, Team3);
			}

			if (!Team4.Text.Equals(""))
			{
				UpdateResource(3, Movinghh4, Movingmm4, Arrivalhh4, Arrivalmm4, Resources4, Team4);
			}

			if (!Team5.Text.Equals(""))
			{
				UpdateResource(4, Movinghh5, Movingmm5, Arrivalhh5, Arrivalmm5, Resources5, Team5);
			}

			if (!Team6.Text.Equals(""))
			{
				UpdateResource(5, Movinghh6, Movingmm6, Arrivalhh6, Arrivalmm6, Resources6, Team6);
			}

			if (!Team7.Text.Equals(""))
			{
				UpdateResource(6, Movinghh7, Movingmm7, Arrivalhh7, Arrivalmm7, Resources7, Team7);
			}

			if (!Team8.Text.Equals(""))
			{
				UpdateResource(7, Movinghh8, Movingmm8, Arrivalhh8, Arrivalmm8, Resources8, Team8);
			}

			if (!Team9.Text.Equals(""))
			{
				UpdateResource(8, Movinghh9, Movingmm9, Arrivalhh9, Arrivalmm9, Resources9, Team9);
			}

			if (!Team10.Text.Equals(""))
			{
				UpdateResource(9, Movinghh10, Movingmm10, Arrivalhh10, Arrivalmm10, Resources10, Team10);
			}
		}

		private void ResourcesChanged(object sender, TextChangedEventArgs e)
		{
			TextBox resource = (TextBox)sender;
			movingButtons.ElementAt(Grid.GetRow(resource)-1).RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            changed = true;
		}

		private void UpdateResource(int position, TextBox MovinghhBox, TextBox MovingmmBox, TextBox ArrivalhhBox, TextBox ArrivalmmBox, TextBox Resource, TextBox Team)
		{
			int movinghh = 0;
			int movingmm = 0;
			if (!MovinghhBox.Text.Equals("hh") && !MovingmmBox.Text.Equals("mm"))
			{
				try
				{
					movinghh = int.Parse(MovinghhBox.Text);
					movingmm = int.Parse(MovingmmBox.Text);
				}
				catch(Exception e)
				{ return; }
			}
			DateTime moving = DateTime.Now;
			moving = moving.Date + new TimeSpan(movinghh, movingmm, 0);

			int arrivalhh = 0;
			int arrivalmm = 0;
			if (!ArrivalhhBox.Text.Equals("hh") && !ArrivalmmBox.Text.Equals("mm"))
			{
				arrivalhh = int.Parse(ArrivalhhBox.Text);
				arrivalmm = int.Parse(ArrivalmmBox.Text);
			}
			DateTime arrival = DateTime.Now;
			arrival = arrival.Date + new TimeSpan(arrivalhh, arrivalmm, 0);
            Team foundTeam = team.getTeamObject(Team.Text.ToString());
			intervention.setResources(position, new Resource(Resource.Text, foundTeam, moving, arrival));
		}

		private void Moving_Click(object sender, RoutedEventArgs e)
		{
			Button bt = (Button)sender;
			if (movingMap[bt.Name][0].Text.Equals("hh") || movingMap[bt.Name][1].Text.Equals("mm"))
			{
				TextBoxHandler.setNow(movingMap[bt.Name][0], movingMap[bt.Name][1]);
			}

            if (informationMap[bt.Name][0].Text.Equals(""))
			{
				MessageBox.Show("Please set the team and resource name before submitting time.");
			}
			else
			{
				StackPanel sp = (StackPanel)bt.Parent;
				Border bd = (Border)sp.Parent;
				try
				{
					int hh = int.Parse(movingMap[bt.Name][0].Text);
					int mm = int.Parse(movingMap[bt.Name][1].Text);
					DateTime startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, DateTime.Now.Second);
					int offset = (int)DateTime.Now.Subtract(startTime).TotalSeconds;
					if(offset < 0)
					{
						MessageBox.Show("The time inserted is in the future!");
					}
					else
					{
						if(DateTime.Now.Hour == hh && DateTime.Now.Minute == mm)
						{
							offset = 0;
						}
						interventionForm.CreateTimer(Grid.GetRow(bd), informationMap[bt.Name][0].Text, informationMap[bt.Name][1].Text, offset);
					}
					
				}
				catch(Exception ex)
				{
					MessageBox.Show("The text inserted in the time boxes is not valid");
				}
			}
		}

		private void Arrival_Click(object sender, RoutedEventArgs e)
		{
			Button bt = (Button)sender;
			if (arrivalMap[bt.Name][0].Text.Equals("hh") || arrivalMap[bt.Name][1].Text.Equals("mm"))
			{
				TextBoxHandler.setNow(arrivalMap[bt.Name][0], arrivalMap[bt.Name][1]);
			}

			StackPanel sp = (StackPanel)bt.Parent;
			Border bd = (Border)sp.Parent;
			try
			{
				int hh = int.Parse(arrivalMap[bt.Name][0].Text);
				int mm = int.Parse(arrivalMap[bt.Name][1].Text);
				DateTime endTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, DateTime.Now.Second);
				int offset = (int)DateTime.Now.Subtract(endTime).TotalMinutes;
				if (offset < 0)
				{
					MessageBox.Show("The time inserted is in the future!");
				}
				else
				{
					interventionForm.StopTimer(Grid.GetRow(bd), offset);
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show("The text inserted in the time boxes is not valid");
			}

            interventionForm.ReportArrived(Grid.GetRow(bd) - 1);
		}

		internal void AddResources(string teamName)
		{
			informationMap.ElementAt(addResourcesOffset).Value[0].Text = teamName;
			movingButtons.ElementAt(addResourcesOffset).RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
			if (addResourcesOffset != 0)
			{
				informationMap.ElementAt(addResourcesOffset).Value[1].Focus();
			}
			addResourcesOffset++;
		}

		internal void ReportArrival(String teamName)
		{
			for(int i = 0; i < informationMap.Count; i++)
			{
				if(informationMap.ElementAt(i).Value[0].Text.Equals(teamName))
				{
					arrivalButtons.ElementAt(i).RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
					return;
				}
			}
		}

        private void FillForm()
        {
			List<Team> interveningTeamList = intervention.getInterveningTeamList();
			MessageBox.Show(interveningTeamList[Team1].ToString());
            Resource[] resources = intervention.getResources();

            for (int i = 0; i < resources.Length; i++)
            {
                int j = i + 1;
                if (resources[i] != null)
                {
                    //UpdateResource(index - 1, movingMap["Moving" + index][0], movingMap["Moving" + index][1], arrivalMap["Arrival" + index][0], arrivalMap["Arrival" + index][1], informationMap["Moving" + index][1], informationMap["Moving" + index][0]);
                    informationMap["Moving" + j][1].Text = resources[i].getResourceName();
                    informationMap["Moving" + j][0].Text = resources[i].getTeamObject().ToString();
                    DateTime moving = resources[i].getMovingTime();
                    movingMap["Moving" + j][0].Text = (moving.Hour != 0) ? moving.Hour.ToString() : "hh";
                    movingMap["Moving" + j][1].Text = (moving.Minute != 0) ? moving.Minute.ToString() : "00";
                    DateTime arrival = resources[i].getArrivalTime();
                    arrivalMap["Arrival" + j][0].Text = (arrival.Hour != 0) ? arrival.Hour.ToString() : "hh";
                    arrivalMap["Arrival" + j][1].Text = (arrival.Minute != 0) ? arrival.Minute.ToString() : "00";
                }
            }
        }
	}
}
