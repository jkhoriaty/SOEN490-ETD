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
using ETD.ViewsPresenters.InterventionsSection.InterventionForm;

namespace ETD.ViewsPresenters.InterventionsSection
{
	/// <summary>
	/// Interaction logic for InterventionSectionPage.xaml
	/// </summary>
	public partial class InterventionSectionPage : Page
	{
		private MainWindow mainWindow;
		private double interventionSizeDifference = 0;

		public InterventionSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
		}

		//Adjusting the intervention section width
		public void setInterventionsSectionWidth(Border InterventionsSection)
		{
			if (interventionSizeDifference == 0)
			{
				//By getting the position of the button, we're able to now how much the label, and button are occupying space
				var position = CreateInterventionButton.PointToScreen(new Point(0, 0));
				interventionSizeDifference = position.X;
			}
			Scroller.MaxWidth = InterventionsSection.ActualWidth - interventionSizeDifference;
		}

		private void CreateInterventionForm(object sender, RoutedEventArgs e)
		{
			Frame frame = new Frame();
			frame.Content = new InterventionFormPage(this);
			InterventionsList.Children.Add(frame);
		}
	}
}
