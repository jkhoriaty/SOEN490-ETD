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
using ETD.Models;

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

		public ResourcesInterventionFormPage(InterventionFormPage interventionForm, Intervention intervention)
		{
			InitializeComponent();
			this.interventionForm = interventionForm;
			this.intervention = intervention;

			setupMovingMap();
			setupArrivalMap();
			setupInformationMap();
		}

		private void setupMovingMap()
		{
			movingMap.Add("Moving1", textboxArray(Movinghh1, Movingmm1));
			movingMap.Add("Moving2", textboxArray(Movinghh2, Movingmm2));
			movingMap.Add("Moving3", textboxArray(Movinghh3, Movingmm3));
			movingMap.Add("Moving4", textboxArray(Movinghh4, Movingmm4));
			movingMap.Add("Moving5", textboxArray(Movinghh5, Movingmm5));
			movingMap.Add("Moving6", textboxArray(Movinghh6, Movingmm6));
			movingMap.Add("Moving7", textboxArray(Movinghh7, Movingmm7));
			movingMap.Add("Moving8", textboxArray(Movinghh8, Movingmm8));
			movingMap.Add("Moving9", textboxArray(Movinghh9, Movingmm9));
			movingMap.Add("Moving10", textboxArray(Movinghh10, Movingmm10));
		}

		private void setupArrivalMap()
		{
			arrivalMap.Add("Arrival1", textboxArray(Arrivalhh1, Arrivalmm1));
			arrivalMap.Add("Arrival2", textboxArray(Arrivalhh2, Arrivalmm2));
			arrivalMap.Add("Arrival3", textboxArray(Arrivalhh3, Arrivalmm3));
			arrivalMap.Add("Arrival4", textboxArray(Arrivalhh4, Arrivalmm4));
			arrivalMap.Add("Arrival5", textboxArray(Arrivalhh5, Arrivalmm5));
			arrivalMap.Add("Arrival6", textboxArray(Arrivalhh6, Arrivalmm6));
			arrivalMap.Add("Arrival7", textboxArray(Arrivalhh7, Arrivalmm7));
			arrivalMap.Add("Arrival8", textboxArray(Arrivalhh8, Arrivalmm8));
			arrivalMap.Add("Arrival9", textboxArray(Arrivalhh9, Arrivalmm9));
			arrivalMap.Add("Arrival10", textboxArray(Arrivalhh10, Arrivalmm10));
		}

		private void setupInformationMap()
		{
			informationMap.Add("Moving1", textboxArray(Team1, Resources1));
			informationMap.Add("Moving2", textboxArray(Team2, Resources2));
			informationMap.Add("Moving3", textboxArray(Team3, Resources3));
			informationMap.Add("Moving4", textboxArray(Team4, Resources4));
			informationMap.Add("Moving5", textboxArray(Team5, Resources5));
			informationMap.Add("Moving6", textboxArray(Team6, Resources6));
			informationMap.Add("Moving7", textboxArray(Team7, Resources7));
			informationMap.Add("Moving8", textboxArray(Team8, Resources8));
			informationMap.Add("Moving9", textboxArray(Team9, Resources9));
			informationMap.Add("Moving10", textboxArray(Team10, Resources10));
		}

		private TextBox[] textboxArray(TextBox box1, TextBox box2)
		{
			TextBox[] array = new TextBox[2];
			array[0] = box1;
			array[1] = box2;
			return array;
		}

		private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.GotFocus(sender, e);
		}

		private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.LostFocus(sender, e);
		}

		//Display an extra resource line when filling out the last available line
		private void TextBoxes_TextChanged(object sender, RoutedEventArgs e)
		{
			TextBox tb = (TextBox)sender;
			if (tb.Name.Equals("Team4"))
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

		private void UpdateResource(int position, TextBox MovinghhBox, TextBox MovingmmBox, TextBox ArrivalhhBox, TextBox ArrivalmmBox, TextBox Resource, TextBox Team)
		{
			int movinghh = int.Parse(MovinghhBox.Text);
			int movingmm = int.Parse(MovingmmBox.Text);
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

			intervention.resources[position] = new Intervention.Resource(Resource.Text, Team.Text, moving, arrival);
		}

		private void Moving_Click(object sender, RoutedEventArgs e)
		{
			Button bt = (Button)sender;
			if (movingMap[bt.Name][0].Text.Equals("hh") || movingMap[bt.Name][1].Text.Equals("mm"))
			{
				TextBoxHandler.setNow(movingMap[bt.Name][0], movingMap[bt.Name][1]);
			}

			if(Team1.Text.Equals("") || Resources1.Text.Equals(""))
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
					int offset = (int)DateTime.Now.Subtract(startTime).TotalMinutes;
					if(offset < 0)
					{
						MessageBox.Show("The time inserted is in the future!");
					}
					else
					{
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
		}
	}
}
