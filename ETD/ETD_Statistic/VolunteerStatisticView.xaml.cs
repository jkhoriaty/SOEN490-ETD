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

namespace ETD_Statistic
{
    /// <summary>
    /// Interaction logic for VolunteerStatisticView.xaml
    /// </summary>
    public partial class VolunteerStatisticView : UserControl
    {
        static int rowIndexer;
        VolunteerStatisticMapper vsm = new VolunteerStatisticMapper();
        public VolunteerStatisticView()
        {
            rowIndexer = 1;
            InitializeComponent();
            GenerateVolunteerHours();
        }

        private void GenerateVolunteerHours()
        {
            //SELECT Volunteers.Name as Name, Joined, Departure FROM Team_Members JOIN Teams ON Teams.Team_ID = Team_Members.Team_ID JOIN Volunteers ON Team_Members.Volunteer_ID = Volunteers.Volunteer_ID WHERE Operation_ID IN (4)
            foreach(VolunteerStatistic vs in vsm.getList())
            {
                //Creation of row for each new volunteer
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(50);
                VolunteerStats.RowDefinitions.Add(rd);
                Border border = new Border();
                border.BorderThickness = new Thickness(1, 0, 1, 1);
                border.BorderBrush = new SolidColorBrush(Colors.Black);

                //Creation of new column for start time
                Border startBorder = new Border();
                startBorder.BorderThickness = new Thickness(1, 0, 1, 1);
                startBorder.BorderBrush = new SolidColorBrush(Colors.Black);

                //Creation of new column for end time
                Border endBorder = new Border();
                endBorder.BorderThickness = new Thickness(0, 0, 1, 1);
                endBorder.BorderBrush = new SolidColorBrush(Colors.Black);

                //Creation of new column for total hours
                Border totalBorder = new Border();
                totalBorder.BorderThickness = new Thickness(0, 0, 1, 1);
                totalBorder.BorderBrush = new SolidColorBrush(Colors.Black);

                //Textblock creation for volunteer name
                TextBlock tb = new TextBlock();
                tb.FontWeight = FontWeights.Bold;
                tb.Height = 40;
                tb.FontSize = 13;
                tb.Margin = new Thickness(15, 0, 0, 0);
                tb.VerticalAlignment = VerticalAlignment.Center;
                tb.HorizontalAlignment = HorizontalAlignment.Center;

                //Textblock creation for start time 
                TextBlock tbStart = new TextBlock();
                tbStart.FontWeight = FontWeights.Bold;
                tbStart.Height = 40;
                tbStart.FontSize = 13;
                tbStart.Margin = new Thickness(15, 0, 0, 0);
                tbStart.VerticalAlignment = VerticalAlignment.Center;
                tbStart.HorizontalAlignment = HorizontalAlignment.Center;

                //Textblock creation for end time
                TextBlock tbEnd = new TextBlock();
                tbEnd.FontWeight = FontWeights.Bold;
                tbEnd.Height = 40;
                tbEnd.FontSize = 13;
                tbEnd.Margin = new Thickness(15, 0, 0, 0);
                tbEnd.VerticalAlignment = VerticalAlignment.Center;
                tbEnd.HorizontalAlignment = HorizontalAlignment.Center;

                //Textblock creation for total hours
                TextBlock tbTotal = new TextBlock();
                tbTotal.FontWeight = FontWeights.Bold;
                tbTotal.Height = 40;
                tbTotal.FontSize = 13;
                tbTotal.Margin = new Thickness(15, 0, 0, 0);
                tbTotal.VerticalAlignment = VerticalAlignment.Center;
                tbTotal.HorizontalAlignment = HorizontalAlignment.Center;

                //adding textblock to borders
                border.Child = tb;
                startBorder.Child = tbStart;
                endBorder.Child = tbEnd;
                totalBorder.Child = tbTotal;

                //adding borders to main grid
                VolunteerStats.Children.Add(border);
                VolunteerStats.Children.Add(startBorder);
                VolunteerStats.Children.Add(endBorder);
                VolunteerStats.Children.Add(totalBorder);

                //Getting values from DB
                

                //Setting strings from db values
                tb.Text = vs.getName();
                tbStart.Text = vs.getStart().ToString();
                tbEnd.Text = vs.getEnd().ToString(); 
                tbTotal.Text = vs.getTimeDiff().ToString();

                //When more than one operation
                if (Statistic.getListSize() > 1)
                {
                    CreateOperationIDColumn();
                    //Creation of new column for operationID
                    Border operationBorder = new Border();
                    operationBorder.BorderThickness = new Thickness(0, 0, 1, 1);
                    operationBorder.BorderBrush = new SolidColorBrush(Colors.Black);

                    //Textblock creation for opeartion ID
                    TextBlock tbOperation = new TextBlock();
                    tbOperation.FontWeight = FontWeights.Bold;
                    tbOperation.Height = 40;
                    tbOperation.FontSize = 13;
                    tbOperation.Margin = new Thickness(15, 0, 0, 0);
                    tbOperation.VerticalAlignment = VerticalAlignment.Center;
                    tbOperation.HorizontalAlignment = HorizontalAlignment.Center;


                    tbOperation.Text = vs.getOperationID();
                    operationBorder.Child = tbOperation;
                    VolunteerStats.Children.Add(operationBorder);

                    Grid.SetColumn(operationBorder, 4);
                    Grid.SetRow(operationBorder, rowIndexer);
                    
                }


                Grid.SetColumn(border, 0);
                Grid.SetRow(border, rowIndexer);
                Grid.SetColumn(startBorder, 1);
                Grid.SetRow(startBorder, rowIndexer);
                Grid.SetColumn(endBorder, 2);
                Grid.SetRow(endBorder, rowIndexer);
                Grid.SetColumn(totalBorder, 3);
                Grid.SetRow(totalBorder, rowIndexer);

                rowIndexer++;
             
            }
            vsm.ClearList();
        }

        private void CreateOperationIDColumn()
        {
            ColumnDefinition cd = new ColumnDefinition();
            cd.Width = new GridLength(150);
            VolunteerStats.ColumnDefinitions.Add(cd);
            Border border = new Border();
            border.BorderThickness = new Thickness(1, 1, 1, 5);
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            TextBlock tb = new TextBlock();
            tb.Height = 50;
            tb.Margin = new Thickness(5, 0, 0, 0);
            tb.FontWeight = FontWeights.ExtraBold;
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Center;
            tb.FontSize = 15;
            tb.Text = "Operation ID";
            border.Child = tb;
            VolunteerStats.Children.Add(border);
            Grid.SetColumn(border, 4);
            Grid.SetRow(border, 0);
        }

    }
}
