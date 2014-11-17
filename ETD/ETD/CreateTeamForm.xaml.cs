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

namespace ETD
{
    /// <summary>
    /// Interaction logic for CreateTeamForm.xaml
    /// </summary>
    public partial class CreateTeamForm : Window, IObserver
    {
        CreateTeamFormUpdate CTFU;
        public CreateTeamForm()
        {
            InitializeComponent();
            CTFU = new CreateTeamFormUpdate();
            LanguageSelector.attach(this);
        }

        private void addMember_Click(object sender, RoutedEventArgs e)
        {
            CTFU.addMember(this);
        }

        public void update()
        { }
    }
}
