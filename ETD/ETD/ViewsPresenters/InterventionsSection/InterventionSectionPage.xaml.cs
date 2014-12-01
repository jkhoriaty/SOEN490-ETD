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

		public InterventionSectionPage(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
		}

		//Adjusting the intervention section width
		public void setInterventionsSectionWidth(Border InterventionsSection)
		{
			Scroller.MaxWidth = InterventionsSection.ActualWidth - InterventionsSectionLabel.ActualWidth - CreateInterventionButton.ActualWidth;
		}

		private void CreateInterventionForm(object sender, RoutedEventArgs e)
		{
			Frame frame = new Frame();
			frame.Content = new InterventionFormPage(this);
			InterventionList.Children.Add(frame);
		}
	}
}
