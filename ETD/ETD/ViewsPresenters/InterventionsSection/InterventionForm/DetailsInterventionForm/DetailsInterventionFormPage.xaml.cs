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

namespace ETD.ViewsPresenters.InterventionsSection.InterventionForm.DetailsInterventionForm
{
    /// <summary>
    /// Interaction logic for DetailsInterventionFormPage.xaml
    /// </summary>
    public partial class DetailsInterventionFormPage : Page
    {
        private InterventionFormPage interventionForm;
        private Intervention intervention;

        public DetailsInterventionFormPage(InterventionFormPage interventionForm, Intervention intervention)
        {
            InitializeComponent();
            this.interventionForm = interventionForm;
            this.intervention = intervention;
            //MessageBox.Show(intervention.getCallerName());
            FillForm(intervention);
        }
        private void FillForm(Intervention intervention)
        {
            if (intervention.getCallerName() != null)
            {
                CallerName.Text = intervention.getCallerName();
            }
            if (intervention.getLocation() != null)
            {
                Location.Text = intervention.getLocation();
            }
            if (intervention.getNatureOfCall() != null)
            {
                NatureOfCall.Text = intervention.getNatureOfCall();
            }
            if (intervention.getAge() != null)
            {
                Age.Text = intervention.getAge();
            }
            if (intervention.getCode() != 0)
            {
                Priority.SelectedIndex = intervention.getCode() - 1;
            }
            if (intervention.getGender() != null)
            {
                int index = -1;
                foreach (ComboBoxItem cb in Gender.Items)
                {
                    index++;
                    if (cb.Content.ToString() == intervention.getGender())
                    {
                        break;
                    }
                }
                Gender.SelectedIndex = index;
            }
            if (intervention.getChiefComplaint() != null)
            {
                int index = -1;
                foreach (ComboBoxItem cb in Complaint.Items)
                {
                    index++;
                    if (cb.Content.ToString() == intervention.getChiefComplaint())
                    {
                        break;
                    }
                }
                Complaint.SelectedIndex = index;
            }
            if (intervention.getTimeOfCall() != null)
            {
                TextBoxHandler.setTime(Callhh, Callmm, intervention.getTimeOfCall().Hour, intervention.getTimeOfCall().Minute);
            }
        }
        private void TextBoxes_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.GotFocus(sender, e);
        }

        private void TextBoxes_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxHandler.LostFocus(sender, e);
            if (((TextBox)sender == CallerName) && !this.CallerName.Text.Equals(ETD.Properties.Resources.TextBox_CallerName))
            {
                intervention.setCallerName(this.CallerName.Text);
            }
            if (((TextBox)sender == Location) && !this.Location.Text.Equals(ETD.Properties.Resources.TextBox_Location))
            {
                intervention.setLocation(this.Location.Text);
            }
            if (((TextBox)sender == NatureOfCall) && !this.NatureOfCall.Text.Equals(ETD.Properties.Resources.TextBox_Nature))
            {
                intervention.setNatureOfCall(this.NatureOfCall.Text);
            }
            if (((TextBox)sender == Age) && !this.Age.Text.Equals(""))
            {
                intervention.setAge(this.Age.Text);
            }

            if (((TextBox)sender == OtherChiefComplaint) && !this.OtherChiefComplaint.Text.Equals(""))
			{
				intervention.setOtherChiefComplaint(OtherChiefComplaint.Text);
			}
            if (((TextBox)sender == Callhh) || ((TextBox)sender == Callmm))
            {
                int hh = int.Parse(Callhh.Text);
                int mm = int.Parse(Callmm.Text);
                DateTime timeOfCall = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, 0);
                intervention.setTimeOfCall(timeOfCall);
            }
        }

        private void Priority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)comboBox.SelectedItem;
            intervention.setCode(Convert.ToInt32(item.Content));
			if (intervention.getCode() == 1)
			{
				interventionForm.interventionType.Foreground = new SolidColorBrush(Colors.Red);
			}
			else
			{
				interventionForm.interventionType.Foreground = new SolidColorBrush(Colors.Black);
			}
        }

        private void Gender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)comboBox.SelectedItem;
            intervention.setGender(item.Content.ToString());
        }
        private void Complaint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)comboBox.SelectedItem;
            if (item.Content.Equals("Other"))
            {
                Grid.SetColumnSpan(Complaint, 1);
                OtherChiefComplaint.Visibility = Visibility.Visible;
            }
            else
            {
                OtherChiefComplaint.Visibility = Visibility.Collapsed;
                Grid.SetColumnSpan(Complaint, 2);
            }
            intervention.setChiefComplaint("" + item.Content);
			interventionForm.interventionType.Text = intervention.getChiefComplaint();
        }

/*        public void PersistencyUpdate()
        {
            try
            {
                DateTime callTime = intervention.getTimeOfCall();
                int hours = int.Parse(Callhh.Text);
                int minutes = int.Parse(Callmm.Text);
                callTime = callTime.Date + (new TimeSpan(hours, minutes, callTime.Second));
                intervention.setTimeOfCall(DateTime.Now);
            }
            catch (Exception e) { }

            if (!TextBoxHandler.isDefaultText(CallerName))
            {
                intervention.setCallerName(CallerName.Text);
            }
            if (!TextBoxHandler.isDefaultText(Location))
            {
                intervention.setLocation(Location.Text);
            }
            if (!TextBoxHandler.isDefaultText(NatureOfCall))
            {
                intervention.setNatureOfCall(NatureOfCall.Text);
            }

            try
            {
                ComboBoxItem priorityItem = (ComboBoxItem)Priority.SelectedItem;
                int code = int.Parse("" + priorityItem.Content);
                intervention.setCode(code);
            }
            catch (Exception e) { }

            try
            {
                ComboBoxItem genderItem = (ComboBoxItem)Gender.SelectedItem;
                intervention.setGender("" + genderItem.Content);
            }
            catch (Exception e) { }

            intervention.setAge("" + Age.Text);

            try
            {
                ComboBoxItem chiefComplaint = (ComboBoxItem)Complaint.SelectedItem;
                String complaint = "" + chiefComplaint.Content;
                intervention.setChiefComplaint(complaint);
                if (complaint.Equals("Other"))
                {
                    intervention.setOtherChiefComplaint(OtherChiefComplaint.Text);
                }
            }
            catch (Exception e) { }
        }
*/
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int hh = int.Parse(Callhh.Text);
                int mm = int.Parse(Callmm.Text);
                DateTime callTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hh, mm, DateTime.Now.Second);
                int offset = (int)DateTime.Now.Subtract(callTime).TotalMinutes;
                if (offset < 0)
                {
                    MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_FutureTime);
                }
                else
                {
					intervention.setTimeOfCall(callTime);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ETD.Properties.Resources.MessageBox_Notification_InvalidTime);
            }
        }
    }
}
