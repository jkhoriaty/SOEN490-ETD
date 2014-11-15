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

namespace ETD
{
    /// <summary>
    /// Interaction logic for TeamForm.xaml
    /// </summary>
    public partial class TeamForm : Page
    {
        TeamFormUpdate TF;
        public TeamForm()
        {
            InitializeComponent();
            TF = new TeamFormUpdate();
        }

        private void AddMember_Click(object sender, RoutedEventArgs e)
        {
            TF.addMember(this);
        }

        private void RemoveMember_Click(object sender, RoutedEventArgs e)
        {
            TF.removeMember(this);
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            TF.submit(this);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            TF.cancel(this);
        }
    }
}
