using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ETD.Services
{
	static class TextBoxHandler
	{
		private static Dictionary<TextBox, String> defaultTextboxContent = new Dictionary<TextBox, String>();
		private static Dictionary<TextBox, String> alternativeTextboxContent = new Dictionary<TextBox, String>();

		//Focus: Textboxes - Clearing the fields upon focus if populated by the default text
		public static void GotFocus(object sender, RoutedEventArgs e)
		{
			TextBox tb = (TextBox)sender;

			//If it's the first time that is it clicked, add it to the dicitonnary
			if (!defaultTextboxContent.ContainsKey(tb))
			{
				defaultTextboxContent.Add(tb, tb.Text);
			}

			//If the text is the default text clear it, else save the text in the alternative dictionnary
			if (tb.Text.Equals(defaultTextboxContent[tb]))
			{
				tb.Text = "";
			}
			else
			{
				if(alternativeTextboxContent.ContainsKey(tb))
				{
					alternativeTextboxContent[tb] = tb.Text;
				}
				else
				{
					alternativeTextboxContent.Add(tb, tb.Text);
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
				if(alternativeTextboxContent.ContainsKey(tb))
				{
					tb.Text = alternativeTextboxContent[tb];
				}
				else
				{
					tb.Text = defaultTextboxContent[tb];
				}
			}
		}

		//Reset the default when the same box was given different contextual default texts
		public static void ResetHandling(TextBox tb)
		{
			if(defaultTextboxContent.ContainsKey(tb))
			{
				defaultTextboxContent.Remove(tb);
			}
			if (alternativeTextboxContent.ContainsKey(tb))
			{
				alternativeTextboxContent.Remove(tb);
			}
		}

		//Return true if the text is the default text, if not false
		public static bool isDefaultText(TextBox tb)
		{
			if (defaultTextboxContent.ContainsKey(tb))
			{
				if (tb.Text.Equals(defaultTextboxContent[tb]))
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

		//Sets the current hours and minutes in the passed TextBoxes
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

		//Places the two items in a TextBox array and returns the array
		public static TextBox[] textboxArray(TextBox box1, TextBox box2)
		{
			TextBox[] array = new TextBox[2];
			array[0] = box1;
			array[1] = box2;
			return array;
		}
	}
}
