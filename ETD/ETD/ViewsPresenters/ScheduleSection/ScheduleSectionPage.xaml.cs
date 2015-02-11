using ETD.ViewsPresenters.ScheduleSection.SectorsTable;
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

namespace ETD.ViewsPresenters.ScheduleSection
{
    /// <summary>
    /// Interaction logic for ScheduleSectionPage.xaml
    /// </summary>
    public partial class ScheduleSectionPage : Page
    {
        private MainWindow parent;
        private int tabs = 1;
        private List<SectorsTablePage> tables = new List<SectorsTablePage>();
        private List<Frame> tableFrames = new List<Frame>();
        public ScheduleSectionPage(MainWindow mainWindow)
        {
            InitializeComponent();
            parent = mainWindow;
            addButton.Margin = new Thickness(this.Width - addButton.Width, this.Height - addButton.Height, 0.0, 0.0);

            tables.Add(new SectorsTablePage(this));
            tableFrames.Add(new Frame());
            tableFrames[0].Content = tables[0];
            Schedule1.Content = tableFrames[0];

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (tables[tabs-1] as SectorsTablePage).Close();
            tabs++;
            TabItem newTab = new TabItem();
            newTab.Header = "" + tabs;
            SchedulesTab.Items.Add(newTab);
            SchedulesTab.SelectedIndex = tabs-1;

            tables.Add(new SectorsTablePage(this));
            tableFrames.Add(new Frame());
            tableFrames[tabs - 1].Content = tables[tabs - 1];
            (SchedulesTab.Items[tabs - 1] as TabItem).Content = tableFrames[tabs - 1];

        }

        internal void UpdateSectors()
        {
            foreach(SectorsTablePage stp in tables)
            {
                stp.UpdateSectors();
            }
        }
    }
}
