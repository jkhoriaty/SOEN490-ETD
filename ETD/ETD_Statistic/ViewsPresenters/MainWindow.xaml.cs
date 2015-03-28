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
using ETD_Statistic.ViewsPresenters;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.IO;
using System.Windows.Markup;

namespace ETD_Statistic.ViewsPresenters
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DocumentViewer docViewer = new DocumentViewer();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoadStatistic(object sender, RoutedEventArgs e)
        {
            statisticView.Children.Clear();
            StatisticView sv = new StatisticView();
            Frame statsView = new Frame();
            statsView.Content = sv;
            statisticView.Children.Add(statsView);
        }

        public void LoadPreviousOperations(object sender, RoutedEventArgs e)
        {
            statisticView.Children.Clear();
            PreviousOperationView pov = new PreviousOperationView();
            Frame operationView = new Frame();
            operationView.Content = pov;
            statisticView.HorizontalAlignment = HorizontalAlignment.Center;
            statisticView.Children.Add(operationView);
        }

        public void ExportWPF()
        {
            StatisticView sv = new StatisticView();

            FixedDocument fixedDoc = new FixedDocument();
            PageContent pageCont = new PageContent();
            FixedPage fixedPage = new FixedPage();
            Frame frame = new Frame();
            frame.Content = sv;

            fixedPage.Children.Add(frame);


            //Size sz = new Size(96 * 8.5, 96 * 11);
            //fixedPage.Measure(sz);
            //fixedPage.Arrange(new Rect(new Point(), sz));
            //fixedPage.UpdateLayout();
  
            ((System.Windows.Markup.IAddChild)pageCont).AddChild(fixedPage);
            fixedDoc.Pages.Add(pageCont);

            docViewer.Document = fixedDoc;           
        }

        public void SaveXPS()
        {
            Microsoft.Win32.SaveFileDialog save = new Microsoft.Win32.SaveFileDialog();
            save.FileName = "StatisticReport";
            save.DefaultExt = ".xps";
            save.Filter = "XPS Documents (.xps)|*.xps";

            Nullable<bool> result = save.ShowDialog();
            if (result == true)
            {
                string fileName = save.FileName;
                FixedDocument doc = (FixedDocument)docViewer.Document;
                XpsDocument xpsDoc = new XpsDocument(fileName, FileAccess.ReadWrite);
                System.Windows.Xps.XpsDocumentWriter xpsWriter = XpsDocument.CreateXpsDocumentWriter(xpsDoc);
                xpsWriter.Write(doc);
                xpsDoc.Close();
            }
        }

        private FixedDocument PageSplitter(StatisticView page)
        {
            FixedDocument fd = new FixedDocument();
            double y = 0;
            Size size = page.DesiredSize;
            double width = 96 * 8.5;
            double height = 96 * 11; 
            while(y < size.Height)
            {
                VisualBrush vb = new VisualBrush(page);
                vb.Stretch = Stretch.None;
                vb.AlignmentX = AlignmentX.Left;
                vb.AlignmentY = AlignmentY.Top;
                vb.ViewboxUnits = BrushMappingMode.Absolute;
                vb.TileMode = TileMode.None;
                //vb.Viewbox = new Rect { 0, y, width, height};
                PageContent pc = new PageContent();
                FixedPage fp = new FixedPage();
                ((IAddChild)pc).AddChild(page);
                fd.Pages.Add(pc);
                //fp.Width = width;
                //fp.Height = height;
                //Canvas canvas = new Canvas();

                y += height;

            }
            return fd;
 
        }

        public void ExportToPDF(object sender, RoutedEventArgs e)
        {
            ExportWPF();
            SaveXPS();
        }
    }
}
