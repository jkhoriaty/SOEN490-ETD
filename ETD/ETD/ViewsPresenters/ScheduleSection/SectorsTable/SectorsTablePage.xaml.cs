using ETD.Models.Objects;
using ETD.Services;
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

namespace ETD.ViewsPresenters.ScheduleSection.SectorsTable
{
    /// <summary>
    /// Interaction logic for SectorsTablePage.xaml
    /// </summary>
    public partial class SectorsTablePage : Page
    {
        ScheduleSectionPage parent;
        Scheduler scheduler;
        bool activated;

        public SectorsTablePage(ScheduleSectionPage page)
        {
            InitializeComponent();

            parent = page;
            scheduler = new Scheduler();

            int index = SectorGrid.RowDefinitions.IndexOf(Sector1Row);
            foreach (UIElement uie in SectorGrid.Children)
            {
                if (Grid.GetRow(uie) >= index)
                {
                    uie.Visibility = System.Windows.Visibility.Hidden;
                }
                if(uie.GetType().Equals(typeof(ComboBox)))
                {
                    foreach(String n in Team.teamsList.Keys)
                    {
                        (uie as ComboBox).Items.Add(n);
                    }
                }
            }
            activated = false;
        }

        public void Close()
        {
            SectorGrid.IsEnabled = false;
        }

        public void Confirm(int column)
        { 
            if(column > 0 && column <= SectorGrid.ColumnDefinitions.Count)
            {
                string hour = "", minutes = "", sector = "";
                foreach (UIElement uie in SectorGrid.Children)
                {
                    if (Grid.GetRow(uie)  == 0)
                    {
                        if(column == 1)
                        {
                            hour = Endhh1.Text;
                            minutes = Endmm1.Text;
                        }
                        else if (column == 2)
                        {
                            hour = Endhh2.Text;
                            minutes = Endmm2.Text;
                        }
                        else if (column == 3)
                        {
                            hour = Endhh3.Text;
                            minutes = Endmm3.Text;
                        }
                    }
                    else if (Grid.GetColumn(uie) == 0)
                    {
                        switch (Grid.GetRow(uie))
                        {
                            case 1: sector = Sector1.Text;
                                break;
                            case 2: sector = Sector2.Text;
                                break;
                            case 3: sector = Sector3.Text;
                                break;
                            case 4: sector = Sector4.Text;
                                break;
                            case 5: sector = Sector5.Text;
                                break;
                            case 6: sector = Sector6.Text;
                                break;
                            case 7: sector = Sector7.Text;
                                break;
                            case 8: sector = Sector8.Text;
                                break;
                            case 9: sector = Sector9.Text;
                                break;
                            case 10: sector = Sector10.Text;
                                break;
                            default: break;
                        }
                    }
                    else if (Grid.GetColumn(uie) == column)
                    {
                        scheduler.AddShift(sector, Team.teamsList[(uie as ComboBox).SelectedItem as string], Convert.ToInt32(hour), Convert.ToInt32(minutes));
                    }
                }
            }
        }

        private void OnFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.GotFocus(sender, e);
        }

        private void LoseFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.LostFocus(sender, e);
            TextBox box = sender as TextBox;
            if (!box.Text.Equals("..."))
            {
                int index = Grid.GetRow(box);
                foreach (UIElement uie in SectorGrid.Children)
                {
                    if ((Grid.GetRow(uie) == (index + 1)) && (uie.Visibility == System.Windows.Visibility.Hidden))
                    {
                        uie.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
        }

        public void Suggest(int column)
        { 
            if(column > 0 && column < SectorGrid.ColumnDefinitions.Count)
            { }
        }

        private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.GotFocus(sender, e);
        }

        private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.LostFocus(sender, e);
        }

        private void End_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int index = Grid.GetColumn(button.Parent as StackPanel);
            if (index == 1)
            {
                TextBoxHandler.setNow(Endhh1, Endmm1);
            }
            else if (index == 2)
            {
                TextBoxHandler.setNow(Endhh2, Endmm2);
            }
            else if (index == 3)
            {
                TextBoxHandler.setNow(Endhh3, Endmm3);
            }
            Confirm(index);
            Suggest(index + 1);
        }

        public void Activate()
        {
            foreach (UIElement uie in SectorGrid.Children)
            {
                if (Grid.GetRow(uie) == 1)
                {
                    uie.Visibility = System.Windows.Visibility.Hidden;
                }
                if (uie.GetType().Equals(typeof(ComboBox)))
                {
                    foreach (String n in Team.teamsList.Keys)
                    {
                        (uie as ComboBox).Items.Add(n);
                    }
                }
            }
        }

        internal void UpdateSectors()
        {
            if(!activated)
            {
                foreach (UIElement uie in SectorGrid.Children)
                {
                    if (Grid.GetRow(uie) == 1)
                    {
                        uie.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
            foreach (UIElement uie in SectorGrid.Children)
            {
                if (uie.GetType().Equals(typeof(ComboBox)))
                {
                    foreach (String n in Team.teamsList.Keys)
                    {
                        if (!(uie as ComboBox).Items.Contains(n))
                        (uie as ComboBox).Items.Add(n);
                    }
                }
            }
        }
    }
}
