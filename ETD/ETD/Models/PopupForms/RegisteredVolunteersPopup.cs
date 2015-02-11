using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ETD.Models.PopupForms
{
	class RegisteredVolunteersForm : Grid
	{
		public RegisteredVolunteersForm(Dictionary<String, String> registeredVolunteers)
		{
			ColumnDefinition ColumnDefinition1 = new ColumnDefinition();
			ColumnDefinition1.Width = new GridLength(100);
			this.ColumnDefinitions.Add(ColumnDefinition1);
			ColumnDefinition ColumnDefinition2 = new ColumnDefinition();
			ColumnDefinition2.Width = new GridLength(100);
			this.ColumnDefinitions.Add(ColumnDefinition2);

			if(registeredVolunteers.Count == 0)
			{
				MessageBox.Show("Warning");
			}

			int row = 0;
			foreach(KeyValuePair<String, String> volunteer in registeredVolunteers)
			{
				RowDefinition rowDefinition = new RowDefinition();
				rowDefinition.Height = new GridLength(25);
				this.RowDefinitions.Add(rowDefinition);

				TextBlock text = new TextBlock();
				text.Text = volunteer.Value;
				Grid.SetRow(text, row);
				Grid.SetColumn(text, 0);

				this.Children.Add(text);
			}
		}
	}
}
