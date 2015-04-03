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
using ETD_Statistic.Model;
using ETD.Services.Database;
using ETD.Services.Database;
using System.Data.SQLite;

namespace ETD_Statistic.ViewsPresenters
{
    /// <summary>
    /// Interaction logic for InterventionView.xaml
    /// </summary>
    public partial class InterventionView : Page
    {
        InterventionStatisticMapper ism;
        String displayComplaint;
        int count = 2;
        static int countWithoutAmbulance = 0;
        static int countWithAmbulance = 0;
        //keeping track of total in the horizontal rows
        static int totalChildrenHoriCount = 0;
        static int totalAdultHoriCount = 0;
        //keeping track of vertical total amount
        static int totalChildrenVertical = 0;
        static int totalAdultVertical = 0;
        public InterventionView()
        {
            InitializeComponent();
            GenerateComplainName();
        }

        private void GenerateComplainName()
        {
            ism = new InterventionStatisticMapper();

            foreach(String complaint in ism.getListComplaint())
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(40);
                ComplaintGrid.RowDefinitions.Add(rd);
                Border border = new Border();
                border.BorderThickness = new Thickness(1, 0, 5, 1);
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                TextBlock tb = new TextBlock();
                tb.FontWeight = FontWeights.Bold;
                tb.Height = 40;
                tb.FontSize = 13;
                tb.Margin = new Thickness(5, 0, 0, 0);
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.TextWrapping = TextWrapping.Wrap;
                displayComplaint = StaticDBConnection.GetResource(complaint);
                tb.Name = complaint;
                tb.Text = displayComplaint;
                border.Child = tb;
                ComplaintGrid.Children.Add(border);
                Grid.SetColumn(border, 0);
                Grid.SetRow(border, count);
                GenerateInterventionWithoutAmbulance(count, complaint);
                GenerateInterventionWithAmbulance(count, complaint);
                GenerateTotalTableHorizontal(count);

                //resetting the total counters
                countWithoutAmbulance = 0;
                countWithAmbulance = 0;
                totalChildrenHoriCount = 0;
                totalAdultHoriCount = 0;
                
                count++;
                
            }
            StaticDBConnection.CloseConnection();
            GenerateTotalTableVertical(count);
            totalChildrenVertical = 0;
            totalAdultVertical = 0;
           
        }

        private void GenerateInterventionWithoutAmbulance(int row, String complaint)
        {
            //generating table for Children without ambulance services
            ism.getInterventionChildrenWithoutAmublanceCount(complaint);

            foreach (int count in ism.getInterventionChildrenWithoutAmublanceList())
            {
                Border border = new Border();
                border.BorderThickness = new Thickness(0, 0, 1, 1);
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                TextBlock tb = new TextBlock();
                tb.FontWeight = FontWeights.Black;
                tb.Height = 40;
                tb.FontSize = 12;
                tb.Margin = new Thickness(3, 0, 0, 0);
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.Text = count.ToString();
                countWithoutAmbulance += int.Parse(tb.Text.ToString());
                totalChildrenHoriCount += int.Parse(tb.Text.ToString());
                border.Child = tb;
                ComplaintGrid.Children.Add(border);
                Grid.SetColumn(border, 1);
                Grid.SetRow(border, row);
            }
            ism.clearInterventionChildrenWACountList();

            //generating table for Adult without ambulance services
            ism.getInterventionAdultWithoutAmublanceCount(complaint);
            
            foreach(int count in ism.getInterventionAdultWithoutAmublanceList())
            {               
                Border border = new Border();
                border.BorderThickness = new Thickness(0, 0, 1, 1);
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                TextBlock tb = new TextBlock();
                tb.FontWeight = FontWeights.Black;
                tb.Height = 40;
                tb.FontSize = 12;
                tb.Margin = new Thickness(3, 0, 0, 0);
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.Text = count.ToString();
                countWithoutAmbulance += int.Parse(tb.Text.ToString());
                totalAdultHoriCount += int.Parse(tb.Text.ToString());
                border.Child = tb;
                ComplaintGrid.Children.Add(border);
                Grid.SetColumn(border, 2);
                Grid.SetRow(border, row);
            }
            ism.clearInterventionAdultWACountList();


            //generating total table for without amublance services          
            Border totalBorder = new Border();
            totalBorder.BorderThickness = new Thickness(0, 0, 1, 1);
            totalBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            TextBlock totalTb = new TextBlock();
            totalTb.FontWeight = FontWeights.Black;
            totalTb.Height = 40;
            totalTb.FontSize = 12;
            totalTb.Margin = new Thickness(3, 0, 0, 0);
            totalTb.VerticalAlignment = VerticalAlignment.Center;
            totalTb.HorizontalAlignment = HorizontalAlignment.Center;
            totalTb.Text = countWithoutAmbulance.ToString();
            totalBorder.Child = totalTb;
            ComplaintGrid.Children.Add(totalBorder);
            Grid.SetColumn(totalBorder, 3);
            Grid.SetRow(totalBorder, row);

           
        }

        private void GenerateInterventionWithAmbulance(int row, String complaint)
        {
            //generating table for Children with ambulance services
            ism.getInterventionChildrenWithAmublanceCount(complaint);

            foreach(int count in ism.getInterventionChildrenWithAmublanceList())
            {
                Border border = new Border();
                border.BorderThickness = new Thickness(0, 0, 1, 1);
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                TextBlock tb = new TextBlock();
                tb.FontWeight = FontWeights.Black;
                tb.Height = 40;
                tb.FontSize = 12;
                tb.Margin = new Thickness(3, 0, 0, 0);
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.Text = count.ToString();
                countWithAmbulance += int.Parse(tb.Text.ToString());
                totalChildrenHoriCount += int.Parse(tb.Text.ToString());
                border.Child = tb;
                ComplaintGrid.Children.Add(border);
                Grid.SetColumn(border, 4);
                Grid.SetRow(border, row);
            }
            ism.clearInterventionChildrenACountList();



            //generating table for Adult with ambulance services
            ism.getInterventionAdultWithAmublanceCount(complaint);
            foreach(int count in ism.getInterventionAdultWithAmublanceList())
            {               
                Border border = new Border();
                border.BorderThickness = new Thickness(0, 0, 1, 1);
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                TextBlock tb = new TextBlock();
                tb.FontWeight = FontWeights.Black;
                tb.Height = 40;
                tb.FontSize = 12;
                tb.Margin = new Thickness(3, 0, 0, 0);
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.Text = count.ToString();
                countWithAmbulance += int.Parse(tb.Text.ToString());
                totalAdultHoriCount += int.Parse(tb.Text.ToString());
                border.Child = tb;
                ComplaintGrid.Children.Add(border);
                Grid.SetColumn(border, 5);
                Grid.SetRow(border, row);
            }
            ism.clearInterventionAdultACountList();

            //generating total table for with amublance services          
            Border totalBorder = new Border();
            totalBorder.BorderThickness = new Thickness(0, 0, 1, 1);
            totalBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            TextBlock totalTb = new TextBlock();
            totalTb.FontWeight = FontWeights.Black;
            totalTb.Height = 40;
            totalTb.FontSize = 12;
            totalTb.Margin = new Thickness(3, 0, 0, 0);
            totalTb.VerticalAlignment = VerticalAlignment.Center;
            totalTb.HorizontalAlignment = HorizontalAlignment.Center;
            totalTb.Text = countWithAmbulance.ToString();
            totalBorder.Child = totalTb;
            ComplaintGrid.Children.Add(totalBorder);
            Grid.SetColumn(totalBorder, 6);
            Grid.SetRow(totalBorder, row);

 
        }

        private void GenerateTotalTableHorizontal(int row)
        {

            //generating row for children total          
            Border childBorder = new Border();
            childBorder.BorderThickness = new Thickness(0, 0, 1, 1);
            childBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            TextBlock childTb = new TextBlock();
            childTb.FontWeight = FontWeights.Black;
            childTb.Height = 40;
            childTb.FontSize = 12;
            childTb.Margin = new Thickness(3, 0, 0, 0);
            childTb.VerticalAlignment = VerticalAlignment.Center;
            childTb.HorizontalAlignment = HorizontalAlignment.Center;
            childTb.Text = totalChildrenHoriCount.ToString();
            childBorder.Child = childTb;
            ComplaintGrid.Children.Add(childBorder);
            Grid.SetColumn(childBorder, 7);
            Grid.SetRow(childBorder, row);

            //generating row for adult total           
            Border adultBorder = new Border();
            adultBorder.BorderThickness = new Thickness(0, 0, 1, 1);
            adultBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            TextBlock adultTb = new TextBlock();
            adultTb.FontWeight = FontWeights.Black;
            adultTb.Height = 40;
            adultTb.FontSize = 12;
            adultTb.Margin = new Thickness(3, 0, 0, 0);
            adultTb.VerticalAlignment = VerticalAlignment.Center;
            adultTb.HorizontalAlignment = HorizontalAlignment.Center;
            adultTb.Text = totalAdultHoriCount.ToString();
            adultBorder.Child = adultTb;
            ComplaintGrid.Children.Add(adultBorder);
            Grid.SetColumn(adultBorder, 8);
            Grid.SetRow(adultBorder, row);

            //generating row for total of everything          
            Border totalBorder = new Border();
            totalBorder.BorderThickness = new Thickness(0, 0, 1, 1);
            totalBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            TextBlock totalTb = new TextBlock();
            totalTb.FontWeight = FontWeights.Black;
            totalTb.Height = 40;
            totalTb.FontSize = 12;
            totalTb.Margin = new Thickness(3, 0, 0, 0);
            totalTb.VerticalAlignment = VerticalAlignment.Center;
            totalTb.HorizontalAlignment = HorizontalAlignment.Center;
            totalTb.Text = (totalAdultHoriCount + totalChildrenHoriCount).ToString();
            totalChildrenVertical += totalChildrenHoriCount;
            totalAdultVertical += totalAdultHoriCount;
            totalBorder.Child = totalTb;
            ComplaintGrid.Children.Add(totalBorder);
            Grid.SetColumn(totalBorder, 9);
            Grid.SetRow(totalBorder, row);
 
        }

        private void GenerateTotalTableVertical(int row)
        {

            RowDefinition totalRow = new RowDefinition();
            totalRow.Height = new GridLength(40);
            ComplaintGrid.RowDefinitions.Add(totalRow);

            //setting total for children
            Border border = new Border();
            border.BorderThickness = new Thickness(5, 1, 5, 5);
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            TextBlock tb = new TextBlock();
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.FontWeight = FontWeights.Bold;
            tb.Height = 40;
            tb.FontSize = 13;
            tb.Margin = new Thickness(5, 0, 0, 0);
            tb.Text = totalChildrenVertical.ToString();
            border.Child = tb;
            ComplaintGrid.Children.Add(border);
            Grid.SetColumn(border, 7);
            Grid.SetRow(border, row + 1);
          

            //setting total for adult
            Border adultBorder = new Border();
            adultBorder.BorderThickness = new Thickness(1, 1, 1, 5);
            adultBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            TextBlock adultTb = new TextBlock();
            adultTb.VerticalAlignment = VerticalAlignment.Center;
            adultTb.HorizontalAlignment = HorizontalAlignment.Center;
            adultTb.FontWeight = FontWeights.Bold;
            adultTb.Height = 40;
            adultTb.FontSize = 13;
            adultTb.Margin = new Thickness(5, 0, 0, 0);
            adultTb.Text = totalAdultVertical.ToString();
            adultBorder.Child = adultTb;
            ComplaintGrid.Children.Add(adultBorder);
            Grid.SetColumn(adultBorder, 8);
            Grid.SetRow(adultBorder, row + 1);

            //setting total for total
            Border totalBorder = new Border();
            totalBorder.BorderThickness = new Thickness(5, 1, 5, 5);
            totalBorder.BorderBrush = new SolidColorBrush(Colors.Black);
            TextBlock totalTb = new TextBlock();
            totalTb.VerticalAlignment = VerticalAlignment.Center;
            totalTb.HorizontalAlignment = HorizontalAlignment.Center;
            totalTb.FontWeight = FontWeights.Bold;
            totalTb.Height = 40;
            totalTb.FontSize = 13;
            totalTb.Margin = new Thickness(5, 0, 0, 0);
            totalTb.Text = (totalChildrenVertical+totalAdultVertical).ToString();
            totalBorder.Child = totalTb;
            ComplaintGrid.Children.Add(totalBorder);
            Grid.SetColumn(totalBorder, 9);
            Grid.SetRow(totalBorder, row + 1);
        }

    }
}
