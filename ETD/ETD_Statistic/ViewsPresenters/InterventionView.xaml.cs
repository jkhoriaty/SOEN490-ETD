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
using ETD.Services.Database;
using System.Data.SQLite;
using ETD_Statistic.Model;


namespace ETD_Statistic.ViewsPresenters
{
    /// <summary>
    /// Interaction logic for InterventionView.xaml
    /// </summary>
    public partial class InterventionView : Page
    {
        String complaint;
        int count = 2;
        static int countWithoutAmbulance = 0;
        static int countWithAmbulance = 0;
        static int totalChildrenCount = 0;
        static int totalAdultCount = 0;
        public InterventionView()
        {
            InitializeComponent();
            GenerateComplainName();
        }

        private void GenerateComplainName()
        {
            SQLiteDataReader reader = StaticDBConnection.QueryDatabase("SELECT DISTINCT Chief_Complaint FROM Interventions WHERE Operation_ID =" + Statistic.getOperationID());
            while (reader.Read())
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
                complaint = reader["Chief_Complaint"].ToString();
                tb.Name = reader["Chief_Complaint"].ToString();
                tb.Text = reader["Chief_Complaint"].ToString();
                border.Child = tb;
                ComplaintGrid.Children.Add(border);
                Grid.SetColumn(border, 0);
                Grid.SetRow(border, count);
                GenerateInterventionWithoutAmbulance(count, complaint);
                GenerateInterventionWithAmbulance(count, complaint);
                GenerateTotalTable(count);

                //resetting the total counters
                countWithoutAmbulance = 0;
                countWithAmbulance = 0;
                totalChildrenCount = 0;
                totalAdultCount = 0;

                count++;
                
            }
            StaticDBConnection.CloseConnection();
           
        }

        private void GenerateInterventionWithoutAmbulance(int row, String complaint)
        {
            //generating table for Children without ambulance services

            SQLiteDataReader childrenReader = StaticDBConnection.QueryDatabase("SELECT count(*) as Count from Interventions WHERE Conclusion NOT LIKE '911' AND Operation_ID=" + Statistic.getOperationID() +" AND Chief_Complaint='"+complaint+"' AND Age < 18");
            while (childrenReader.Read())
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(40);
                ComplaintGrid.RowDefinitions.Add(rd);
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
                tb.Text = childrenReader["Count"].ToString();
                countWithoutAmbulance += int.Parse(tb.Text.ToString());
                totalChildrenCount += int.Parse(tb.Text.ToString());
                border.Child = tb;
                ComplaintGrid.Children.Add(border);
                Grid.SetColumn(border, 1);
                Grid.SetRow(border, row);
            }

            //generating table for Adult without ambulance services
            SQLiteDataReader adultReader = StaticDBConnection.QueryDatabase("SELECT count(*) as Count from Interventions WHERE Conclusion NOT LIKE '911' AND Operation_ID=" + Statistic.getOperationID() + " AND Chief_Complaint='" + complaint + "' AND Age >= 18");
            while (adultReader.Read())
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(40);
                ComplaintGrid.RowDefinitions.Add(rd);
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
                tb.Text = adultReader["Count"].ToString();
                countWithoutAmbulance += int.Parse(tb.Text.ToString());
                totalAdultCount += int.Parse(tb.Text.ToString());
                border.Child = tb;
                ComplaintGrid.Children.Add(border);
                Grid.SetColumn(border, 2);
                Grid.SetRow(border, row);
            }

            //generating total table for without amublance services
            RowDefinition rowDef = new RowDefinition();
            rowDef.Height = new GridLength(40);
            ComplaintGrid.RowDefinitions.Add(rowDef);
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
            SQLiteDataReader childrenReader = StaticDBConnection.QueryDatabase("SELECT count(*) as Count from Interventions WHERE Conclusion LIKE '911' AND Operation_ID=" + Statistic.getOperationID() + " AND Chief_Complaint='" + complaint + "' AND Age < 18");
            while (childrenReader.Read())
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(40);
                ComplaintGrid.RowDefinitions.Add(rd);
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
                tb.Text = childrenReader["Count"].ToString();
                countWithAmbulance += int.Parse(tb.Text.ToString());
                totalChildrenCount += int.Parse(tb.Text.ToString());
                border.Child = tb;
                ComplaintGrid.Children.Add(border);
                Grid.SetColumn(border, 4);
                Grid.SetRow(border, row);
            }

            //generating table for Adult with ambulance services
            SQLiteDataReader adultReader = StaticDBConnection.QueryDatabase("SELECT count(*) as Count from Interventions WHERE Conclusion LIKE '911' AND Operation_ID=" + Statistic.getOperationID() + " AND Chief_Complaint='" + complaint + "' AND Age >= 18");
            while (adultReader.Read())
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(40);
                ComplaintGrid.RowDefinitions.Add(rd);
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
                tb.Text = adultReader["Count"].ToString();
                countWithAmbulance += int.Parse(tb.Text.ToString());
                totalAdultCount += int.Parse(tb.Text.ToString());
                border.Child = tb;
                ComplaintGrid.Children.Add(border);
                Grid.SetColumn(border, 5);
                Grid.SetRow(border, row);
            }

            //generating total table for with amublance services
            RowDefinition rowDef = new RowDefinition();
            rowDef.Height = new GridLength(40);
            ComplaintGrid.RowDefinitions.Add(rowDef);
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

        private void GenerateTotalTable(int row)
        {

            //generating row for children total
            RowDefinition childRd = new RowDefinition();
            childRd.Height = new GridLength(40);
            ComplaintGrid.RowDefinitions.Add(childRd);
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
            childTb.Text = totalChildrenCount.ToString();
            childBorder.Child = childTb;
            ComplaintGrid.Children.Add(childBorder);
            Grid.SetColumn(childBorder, 7);
            Grid.SetRow(childBorder, row);

            //generating row for adult total
            RowDefinition adultRd = new RowDefinition();
            adultRd.Height = new GridLength(40);
            ComplaintGrid.RowDefinitions.Add(adultRd);
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
            adultTb.Text = totalAdultCount.ToString();
            adultBorder.Child = adultTb;
            ComplaintGrid.Children.Add(adultBorder);
            Grid.SetColumn(adultBorder, 8);
            Grid.SetRow(adultBorder, row);

            //generating row for total of everything
            RowDefinition totalRd = new RowDefinition();
            totalRd.Height = new GridLength(40);
            ComplaintGrid.RowDefinitions.Add(totalRd);
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
            totalTb.Text = (totalAdultCount + totalChildrenCount).ToString();
            totalBorder.Child = totalTb;
            ComplaintGrid.Children.Add(totalBorder);
            Grid.SetColumn(totalBorder, 9);
            Grid.SetRow(totalBorder, row);
 
        }

    }
}
