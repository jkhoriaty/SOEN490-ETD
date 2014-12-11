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

		public ResourcesInterventionFormPage(InterventionFormPage interventionForm, Intervention intervention)
		{
			InitializeComponent();
			this.interventionForm = interventionForm;
			this.intervention = intervention;
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
			if(tb.Name.Equals("Team1"))
			{
				//TextBoxHandler.setNow(Movinghh1, Movingmm1);
			}
			else if (tb.Name.Equals("Team2"))
			{
				//TextBoxHandler.setNow(Movinghh2, Movingmm2);
			}
			else if (tb.Name.Equals("Team3"))
			{
				//TextBoxHandler.setNow(Movinghh3, Movingmm3);
			}
			else if (tb.Name.Equals("Team4"))
			{
				//TextBoxHandler.setNow(Movinghh4, Movingmm4);
				MainGrid.RowDefinitions[5].Height = new GridLength(23);
			}
			else if(tb.Name.Equals("Team5"))
			{
				//TextBoxHandler.setNow(Movinghh5, Movingmm5);
				MainGrid.RowDefinitions[6].Height = new GridLength(23);
			}
			else if(tb.Name.Equals("Team6"))
			{
				//TextBoxHandler.setNow(Movinghh6, Movingmm6);
				MainGrid.RowDefinitions[7].Height = new GridLength(23);
			}
			else if(tb.Name.Equals("Team7"))
			{
				//TextBoxHandler.setNow(Movinghh7, Movingmm7);
				MainGrid.RowDefinitions[8].Height = new GridLength(23);
			}
			else if(tb.Name.Equals("Team8"))
			{
				//TextBoxHandler.setNow(Movinghh8, Movingmm8);
				MainGrid.RowDefinitions[9].Height = new GridLength(23);
			}
			else if(tb.Name.Equals("Team9"))
			{
				//TextBoxHandler.setNow(Movinghh9, Movingmm9);
				MainGrid.RowDefinitions[10].Height = new GridLength(23);
			}
			else if (tb.Name.Equals("Team10"))
			{
				//TextBoxHandler.setNow(Movinghh10, Movingmm10);
			}
		}

		public void PersistencyUpdate()
		{
			if(!Team1.Text.Equals(""))
			{
				int movinghh = int.Parse(Movinghh1.Text);
				int movingmm = int.Parse(Movingmm1.Text);
				DateTime moving = DateTime.Now;
				moving = moving.Date + new TimeSpan(movinghh, movingmm, 0);

				int arrivalhh = 0;
				int arrivalmm = 0;
				if(!Arrivalhh1.Text.Equals("hh") && !Arrivalmm1.Text.Equals("mm"))
				{
					arrivalhh = int.Parse(Movinghh1.Text);
					arrivalmm = int.Parse(Movingmm1.Text);
				}
				DateTime arrival = DateTime.Now;
				arrival = arrival.Date + new TimeSpan(arrivalhh, arrivalmm, 0);

				intervention.resources[0] = new Intervention.Resource(Resources1.Text, Team1.Text, moving, arrival);
			}

			if (!Team2.Text.Equals(""))
			{
				int movinghh = int.Parse(Movinghh2.Text);
				int movingmm = int.Parse(Movingmm2.Text);
				DateTime moving = DateTime.Now;
				moving = moving.Date + new TimeSpan(movinghh, movingmm, 0);

				int arrivalhh = 0;
				int arrivalmm = 0;
				if (!Arrivalhh2.Text.Equals("hh") && !Arrivalmm2.Text.Equals("mm"))
				{
					arrivalhh = int.Parse(Movinghh2.Text);
					arrivalmm = int.Parse(Movingmm2.Text);
				}
				DateTime arrival = DateTime.Now;
				arrival = arrival.Date + new TimeSpan(arrivalhh, arrivalmm, 0);

				intervention.resources[1] = new Intervention.Resource(Resources2.Text, Team2.Text, moving, arrival);
			}

			if (!Team3.Text.Equals(""))
			{
				int movinghh = int.Parse(Movinghh3.Text);
				int movingmm = int.Parse(Movingmm3.Text);
				DateTime moving = DateTime.Now;
				moving = moving.Date + new TimeSpan(movinghh, movingmm, 0);

				int arrivalhh = 0;
				int arrivalmm = 0;
				if (!Arrivalhh3.Text.Equals("hh") && !Arrivalmm3.Text.Equals("mm"))
				{
					arrivalhh = int.Parse(Movinghh3.Text);
					arrivalmm = int.Parse(Movingmm3.Text);
				}
				DateTime arrival = DateTime.Now;
				arrival = arrival.Date + new TimeSpan(arrivalhh, arrivalmm, 0);

				intervention.resources[2] = new Intervention.Resource(Resources3.Text, Team3.Text, moving, arrival);
			}

			if (!Team4.Text.Equals(""))
			{
				int movinghh = int.Parse(Movinghh4.Text);
				int movingmm = int.Parse(Movingmm4.Text);
				DateTime moving = DateTime.Now;
				moving = moving.Date + new TimeSpan(movinghh, movingmm, 0);

				int arrivalhh = 0;
				int arrivalmm = 0;
				if (!Arrivalhh4.Text.Equals("hh") && !Arrivalmm4.Text.Equals("mm"))
				{
					arrivalhh = int.Parse(Movinghh4.Text);
					arrivalmm = int.Parse(Movingmm4.Text);
				}
				DateTime arrival = DateTime.Now;
				arrival = arrival.Date + new TimeSpan(arrivalhh, arrivalmm, 0);

				intervention.resources[3] = new Intervention.Resource(Resources4.Text, Team4.Text, moving, arrival);
			}

			if (!Team5.Text.Equals(""))
			{
				int movinghh = int.Parse(Movinghh5.Text);
				int movingmm = int.Parse(Movingmm5.Text);
				DateTime moving = DateTime.Now;
				moving = moving.Date + new TimeSpan(movinghh, movingmm, 0);

				int arrivalhh = 0;
				int arrivalmm = 0;
				if (!Arrivalhh5.Text.Equals("hh") && !Arrivalmm5.Text.Equals("mm"))
				{
					arrivalhh = int.Parse(Movinghh5.Text);
					arrivalmm = int.Parse(Movingmm5.Text);
				}
				DateTime arrival = DateTime.Now;
				arrival = arrival.Date + new TimeSpan(arrivalhh, arrivalmm, 0);

				intervention.resources[4] = new Intervention.Resource(Resources5.Text, Team5.Text, moving, arrival);
			}

			if (!Team6.Text.Equals(""))
			{
				int movinghh = int.Parse(Movinghh6.Text);
				int movingmm = int.Parse(Movingmm6.Text);
				DateTime moving = DateTime.Now;
				moving = moving.Date + new TimeSpan(movinghh, movingmm, 0);

				int arrivalhh = 0;
				int arrivalmm = 0;
				if (!Arrivalhh6.Text.Equals("hh") && !Arrivalmm6.Text.Equals("mm"))
				{
					arrivalhh = int.Parse(Movinghh6.Text);
					arrivalmm = int.Parse(Movingmm6.Text);
				}
				DateTime arrival = DateTime.Now;
				arrival = arrival.Date + new TimeSpan(arrivalhh, arrivalmm, 0);

				intervention.resources[5] = new Intervention.Resource(Resources6.Text, Team6.Text, moving, arrival);
			}

			if (!Team7.Text.Equals(""))
			{
				int movinghh = int.Parse(Movinghh7.Text);
				int movingmm = int.Parse(Movingmm7.Text);
				DateTime moving = DateTime.Now;
				moving = moving.Date + new TimeSpan(movinghh, movingmm, 0);

				int arrivalhh = 0;
				int arrivalmm = 0;
				if (!Arrivalhh7.Text.Equals("hh") && !Arrivalmm7.Text.Equals("mm"))
				{
					arrivalhh = int.Parse(Movinghh7.Text);
					arrivalmm = int.Parse(Movingmm7.Text);
				}
				DateTime arrival = DateTime.Now;
				arrival = arrival.Date + new TimeSpan(arrivalhh, arrivalmm, 0);

				intervention.resources[6] = new Intervention.Resource(Resources7.Text, Team7.Text, moving, arrival);
			}

			if (!Team8.Text.Equals(""))
			{
				int movinghh = int.Parse(Movinghh8.Text);
				int movingmm = int.Parse(Movingmm8.Text);
				DateTime moving = DateTime.Now;
				moving = moving.Date + new TimeSpan(movinghh, movingmm, 0);

				int arrivalhh = 0;
				int arrivalmm = 0;
				if (!Arrivalhh8.Text.Equals("hh") && !Arrivalmm8.Text.Equals("mm"))
				{
					arrivalhh = int.Parse(Movinghh8.Text);
					arrivalmm = int.Parse(Movingmm8.Text);
				}
				DateTime arrival = DateTime.Now;
				arrival = arrival.Date + new TimeSpan(arrivalhh, arrivalmm, 0);

				intervention.resources[7] = new Intervention.Resource(Resources8.Text, Team8.Text, moving, arrival);
			}

			if (!Team9.Text.Equals(""))
			{
				int movinghh = int.Parse(Movinghh9.Text);
				int movingmm = int.Parse(Movingmm9.Text);
				DateTime moving = DateTime.Now;
				moving = moving.Date + new TimeSpan(movinghh, movingmm, 0);

				int arrivalhh = 0;
				int arrivalmm = 0;
				if (!Arrivalhh9.Text.Equals("hh") && !Arrivalmm9.Text.Equals("mm"))
				{
					arrivalhh = int.Parse(Movinghh9.Text);
					arrivalmm = int.Parse(Movingmm9.Text);
				}
				DateTime arrival = DateTime.Now;
				arrival = arrival.Date + new TimeSpan(arrivalhh, arrivalmm, 0);

				intervention.resources[8] = new Intervention.Resource(Resources9.Text, Team9.Text, moving, arrival);
			}

			if (!Team10.Text.Equals(""))
			{
				int movinghh = int.Parse(Movinghh10.Text);
				int movingmm = int.Parse(Movingmm10.Text);
				DateTime moving = DateTime.Now;
				moving = moving.Date + new TimeSpan(movinghh, movingmm, 0);

				int arrivalhh = 0;
				int arrivalmm = 0;
				if (!Arrivalhh10.Text.Equals("hh") && !Arrivalmm10.Text.Equals("mm"))
				{
					arrivalhh = int.Parse(Movinghh10.Text);
					arrivalmm = int.Parse(Movingmm10.Text);
				}
				DateTime arrival = DateTime.Now;
				arrival = arrival.Date + new TimeSpan(arrivalhh, arrivalmm, 0);

				intervention.resources[9] = new Intervention.Resource(Resources10.Text, Team10.Text, moving, arrival);
			}
		}

		private void Moving1_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Movinghh1, Movingmm1);
			if(Team1.Text.Equals("") || Resources1.Text.Equals(""))
			{
				MessageBox.Show("Please set the team and resource name before submitting time.");
			}
			else
			{
				interventionForm.CreateTimer(Team1.Text, Resources1.Text);
			}
		}

		private void Arrival1_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Arrivalhh1, Arrivalmm1);
		}

		private void Moving2_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Movinghh2, Movingmm2);
		}

		private void Arrival2_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Arrivalhh2, Arrivalmm2);
		}

		private void Moving3_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Movinghh3, Movingmm3);
		}

		private void Arrival3_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Arrivalhh3, Arrivalmm3);
		}

		private void Moving4_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Movinghh4, Movingmm4);
		}

		private void Arrival4_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Arrivalhh4, Arrivalmm4);
		}

		private void Moving5_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Movinghh5, Movingmm5);
		}

		private void Arrival5_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Arrivalhh5, Arrivalmm5);
		}

		private void Moving6_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Movinghh6, Movingmm6);
		}

		private void Arrival6_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Arrivalhh6, Arrivalmm6);
		}

		private void Moving7_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Movinghh7, Movingmm7);
		}

		private void Arrival7_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Arrivalhh7, Arrivalmm7);
		}

		private void Moving8_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Movinghh8, Movingmm8);
		}

		private void Arrival8_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Arrivalhh8, Arrivalmm8);
		}

		private void Moving9_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Movinghh9, Movingmm9);
		}

		private void Arrival9_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Arrivalhh9, Arrivalmm9);
		}

		private void Moving10_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Movinghh10, Movingmm10);
		}

		private void Arrival10_Click(object sender, RoutedEventArgs e)
		{
			TextBoxHandler.setNow(Arrivalhh10, Arrivalmm10);
		}
	}
}
