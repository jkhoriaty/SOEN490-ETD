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
using ETD.Models.Objects;
using ETD.Services;
using ETD.CustomObjects.CustomUIObjects;

namespace ETD.CustomObjects.PopupForms
{
    /// <summary>
    /// Interaction logic for FollowUpSectionForm.xaml
    /// </summary>
    public partial class FollowUpSectionForm : Page
    {

        //Create a follow up form containing a list of special requests made during the operation
        public FollowUpSectionForm()
        {
            InitializeComponent();
            RequestLine requestLine = new RequestLine();
            requestLine.doRequestLine(this);
        }

		//Return the follow up section grid
        public Grid getRequestGrid()
        {
            return Request_Grid;
        }


    }

}
