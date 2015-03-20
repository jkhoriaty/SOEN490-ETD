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
using System.Windows.Shapes;

namespace ETD.ViewsPresenters
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AdditionalStatisticInfo : Window
    {
        public AdditionalStatisticInfo()
        {
            InitializeComponent();
        }

        protected void SavingInformation(Object sender, System.EventArgs e)
        {
            MessageBox.Show("The information have been saved!");
        }
    }
}
