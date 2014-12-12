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
		private static Dictionary<String, String> defaultTextboxContent = new Dictionary<String, String>();
		private static Dictionary<String, String> alternativeTextboxContent = new Dictionary<String, String>();

		//Focus: Textboxes - Clearing the fields upon focus if populated by the default text
		public static void GotFocus(object sender, RoutedEventArgs e)
		{
			TextBox tb = (TextBox)sender;

			//If it's the first time that is it clicked, add it to the dicitonnary
			if (!defaultTextboxContent.ContainsKey(tb.Name))
			{
				defaultTextboxContent.Add(tb.Name, tb.Text);
			}

			//If the text is the default text clear it, else save the text in the alternative dictionnary
			if (tb.Text.Equals(defaultTextboxContent[tb.Name]))
			{
				tb.Text = "";
			}
			else
			{
				if(alternativeTextboxContent.ContainsKey(tb.Name))
				{
					alternativeTextboxContent[tb.Name] = tb.Text;
				}
				else
				{
					alternativeTextboxContent.Add(tb.Name, tb.Text);
				}
			}
		}

		//LostFocus: Textboxes - Recovering the fields default text if left empty
		public static void LostFocus(object sender, RoutedEventArgs e)
		{
			TextBox tb = (TextBox)sender;

			//If the textbox is empty, recover the alternative text if existent, if not, recover default text
			if (tb.Text.Equals(""))
			{
				if(alternativeTextboxContent.ContainsKey(tb.Name))
				{
					tb.Text = alternativeTextboxContent[tb.Name];
				}
				else
				{
					tb.Text = defaultTextboxContent[tb.Name];
				}
			}
		}

		//Return true if the text is the default text, if not false
		public static bool isDefaultText(TextBox tb)
		{
			if (defaultTextboxContent.ContainsKey(tb.Name))
			{
				if (tb.Text.Equals(defaultTextboxContent[tb.Name]))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return true;
			}
		}

		public static void setNow(TextBox hh, TextBox mm)
		{
			hh.Text = "" + DateTime.Now.Hour;
			if (DateTime.Now.Minute < 10)
			{
				mm.Text = "0" + DateTime.Now.Minute;
			}
			else
			{
				mm.Text = "" + DateTime.Now.Minute;
			}
		}
	}
}
