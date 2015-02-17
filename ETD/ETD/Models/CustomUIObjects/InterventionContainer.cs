using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace ETD.Models.CustomUIObjects
{
	class InterventionContainer : Grid
	{
		public InterventionContainer(String name, double width, double height)
		{
			this.Width = width;
			this.Height = height;

			Border border = new Border();
			border.BorderThickness =  new Thickness(1);
			border.CornerRadius = new CornerRadius(5);
			border.BorderBrush = new SolidColorBrush(Colors.Red);
			this.Children.Add(border);
		}

		public void setWidth (double width)
		{
			this.Width = width;
		}

		public void setHeight (double height)
		{
			this.Height = height;
		}
	}
}
