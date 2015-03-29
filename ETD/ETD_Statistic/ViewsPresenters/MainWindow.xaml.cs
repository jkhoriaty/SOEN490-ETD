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
using System.IO.Packaging;
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
            InterventionView iv = new InterventionView();

            FixedDocument fixedDoc = new FixedDocument();
            PageContent firstPageCont = new PageContent();
            PageContent secondPageCont = new PageContent();

            FixedPage firstFixedPage = new FixedPage();
            FixedPage secondFixedPage = new FixedPage();

            Frame svFrame = new Frame();
            Frame ivFrame = new Frame();

            svFrame.Content = sv;
            ivFrame.Content = iv;

            firstFixedPage.Children.Add(svFrame);
            secondFixedPage.Children.Add(ivFrame);


            //Size sz = new Size(96 * 8.5, 96 * 11);
            //fixedPage.Measure(sz);
            //fixedPage.Arrange(new Rect(new Point(), sz));
            //fixedPage.UpdateLayout();
  
            ((System.Windows.Markup.IAddChild)firstPageCont).AddChild(firstFixedPage);
            ((System.Windows.Markup.IAddChild)secondPageCont).AddChild(secondFixedPage);

            fixedDoc.Pages.Add(firstPageCont);
            fixedDoc.Pages.Add(secondPageCont);

            docViewer.Document = fixedDoc;           
        }

        //function to save WPF document as XPS then transform into PDF with PdfSharp dll
        public void SavePDF()
        {
            Microsoft.Win32.SaveFileDialog save = new Microsoft.Win32.SaveFileDialog();
            save.FileName = "StatisticReport";
            save.DefaultExt = ".pdf";
            save.Filter = "PDF Documents (.pdf)|*.pdf";
            save.OverwritePrompt = true;

            Nullable<bool> result = save.ShowDialog();
            if (result == true)
            {
                string fileName = save.FileName;
                if (System.IO.File.Exists(fileName))
                {
                    System.IO.File.Delete(fileName);
                }

                MemoryStream ms = new MemoryStream();
                Package pkg = Package.Open(ms, FileMode.Create);
                FixedDocument doc = (FixedDocument)docViewer.Document;
                //XpsDocument xpsDoc = new XpsDocument(fileName, FileAccess.ReadWrite);
                XpsDocument xpsDoc = new XpsDocument(pkg);
                System.Windows.Xps.XpsDocumentWriter xpsWriter = XpsDocument.CreateXpsDocumentWriter(xpsDoc);
                xpsWriter.Write(doc);
                xpsDoc.Close();
                pkg.Close();
                var xpsToPdf = PdfSharp.Xps.XpsModel.XpsDocument.Open(ms);
                PdfSharp.Xps.XpsConverter.Convert(xpsToPdf, fileName, 0);
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
            SavePDF();
        }
    }
}
