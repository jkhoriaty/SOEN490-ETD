using ETD.ViewsPresenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace ETD.CustomObjects.PopupForms
{
	class FormPopup : Popup
	{
		private static MainWindow mainWindow;

		public FormPopup(Page page)
		{
			this.AllowsTransparency = true;
			this.HorizontalOffset = 253;
			this.VerticalOffset = 52;
			this.StaysOpen = false;
			this.IsOpen = true;
			this.Focus();
			this.LostFocus += mainWindow.PopupLostFocus;

			Border border = new Border();
			border.BorderThickness = new Thickness(1);
			border.Padding = new Thickness(2);
			border.BorderBrush = new SolidColorBrush(Colors.Red);
			border.Background = new SolidColorBrush(Colors.White);
			border.CornerRadius = new CornerRadius(5);
			this.Child = border;

			Frame frame = new Frame();
			frame.Content = page;
			border.Child = frame;
		}

		public static void RegisterMainWindow(MainWindow root)
		{
			mainWindow = root;
		}
	}
}
