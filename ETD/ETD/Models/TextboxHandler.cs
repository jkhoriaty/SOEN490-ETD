using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ETD.Models
{
	static class TextBoxHandler
	{
		private static Dictionary<String, String> textboxContent = new Dictionary<string, string>();

		//Focus: Textboxes - Clearing the fields upon focus if populated by the default text
		public static void GotFocus(object sender, RoutedEventArgs e)
		{
			TextBox tb = (TextBox)sender;
			if (!textboxContent.ContainsKey(tb.Name))
			{
				textboxContent.Add(tb.Name, tb.Text);
			}
			if (tb.Text.Equals(textboxContent[tb.Name]))
			{
				tb.Text = "";
			}
		}

		//LostFocus: Textboxes - Recovering the fields default text if left empty
		public static void LostFocus(object sender, RoutedEventArgs e)
		{
			TextBox tb = (TextBox)sender;
			if (tb.Text.Equals(""))
			{
				tb.Text = textboxContent[tb.Name];
			}
		}
	}
}
