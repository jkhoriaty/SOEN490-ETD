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


        //load statistic button
        public void LoadStatistic(object sender, RoutedEventArgs e)
        {
            statisticView.Children.Clear();
            StatisticView sv = new StatisticView();
            Frame statsView = new Frame();
            statsView.Content = sv;
            statisticView.Children.Add(statsView);
        }

        //load previous statistic button
        public void LoadPreviousOperations(object sender, RoutedEventArgs e)
        {
            statisticView.Children.Clear();
            PreviousOperationView pov = new PreviousOperationView();
            Frame operationView = new Frame();
            operationView.Content = pov;
            statisticView.HorizontalAlignment = HorizontalAlignment.Center;
            statisticView.Children.Add(operationView);
        }


        //function to export all WPF documents as a fixed document
        public void ExportWPF()
        {
            StatisticView sv = new StatisticView();
            InterventionView iv = new InterventionView();
            VolunteerStatisticView vs = new VolunteerStatisticView();

            FixedDocument fixedDoc = new FixedDocument();
            PageContent firstPageCont = new PageContent();
            PageContent secondPageCont = new PageContent();
            PageContent thirdPageCont = new PageContent();


            FixedPage firstFixedPage = new FixedPage();
            FixedPage secondFixedPage = new FixedPage();
            FixedPage thirdFixedPage = new FixedPage();


            Frame svFrame = new Frame();
            Frame ivFrame = new Frame();
            Frame vsFrame = new Frame();

            svFrame.Content = sv;
            ivFrame.Content = iv;
            vsFrame.Content = vs;

            firstFixedPage.Children.Add(svFrame);
            secondFixedPage.Children.Add(ivFrame);
            thirdFixedPage.Children.Add(vsFrame);
  
            ((System.Windows.Markup.IAddChild)firstPageCont).AddChild(firstFixedPage);
            ((System.Windows.Markup.IAddChild)secondPageCont).AddChild(secondFixedPage);
            ((System.Windows.Markup.IAddChild)thirdPageCont).AddChild(thirdFixedPage);


            fixedDoc.Pages.Add(firstPageCont);
            fixedDoc.Pages.Add(secondPageCont);
            fixedDoc.Pages.Add(thirdPageCont);


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
                try
                {
                    if (System.IO.File.Exists(fileName))
                    {
                        System.IO.File.Delete(fileName);
                    }
                }

                catch (IOException e)
                {
                }
                MemoryStream ms = new MemoryStream();
                Package pkg = Package.Open(ms, FileMode.Create);
                FixedDocument doc = (FixedDocument)docViewer.Document;
                XpsDocument xpsDoc = new XpsDocument(pkg);
                System.Windows.Xps.XpsDocumentWriter xpsWriter = XpsDocument.CreateXpsDocumentWriter(xpsDoc);
                xpsWriter.Write(doc);
                xpsDoc.Close();
                pkg.Close();
                var xpsToPdf = PdfSharp.Xps.XpsModel.XpsDocument.Open(ms);
                PdfSharp.Xps.XpsConverter.Convert(xpsToPdf, fileName, 0);
            }
        }


        public void ExportToPDF(object sender, RoutedEventArgs e)
        {
            ExportWPF();
            SavePDF();
        }
    }
}
