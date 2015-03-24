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

        private void LoadStatistic(object sender, RoutedEventArgs e)
        {
            statisticView.Children.Clear();
            StatisticView sv = new StatisticView();
            Frame statsView = new Frame();
            statsView.Content = sv;
            statisticView.Children.Add(statsView);
        }

        private void LoadPreviousOperations(object sender, RoutedEventArgs e)
        {
            statisticView.Children.Clear();
            PreviousOperationView pov = new PreviousOperationView();
            Frame operationView = new Frame();
            operationView.Content = pov;
            statisticView.HorizontalAlignment = HorizontalAlignment.Center;
            statisticView.Children.Add(operationView);
        }

        public void ExportWPF(StackPanel element)
        {
            StackPanel sp = new StackPanel();
            sp.DataContext = element;
            FixedDocument fixedDoc = new FixedDocument();
            PageContent pageCont = new PageContent();
            FixedPage fixedPage = new FixedPage();

            fixedPage.Children.Add(sp);
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

        public void ExportToPDF(object sender, RoutedEventArgs e)
        {
            ExportWPF(buttonView);
            SaveXPS();
        }


    }
}
