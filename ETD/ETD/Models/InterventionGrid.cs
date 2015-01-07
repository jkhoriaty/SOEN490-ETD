using ETD.ViewsPresenters.MapSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media.Effects;

namespace ETD.Models
{
	class InterventionGrid : Grid
	{
        public InterventionGrid(int interventionNumber, MapSectionPage mapSection, int size) : base()
        {
			this.Name = "intervention_" + interventionNumber;
			this.Tag = "intervention";
			this.Width = size;
			this.Height = size;
			this.MouseLeftButtonDown += new MouseButtonEventHandler(mapSection.DragStart);
			this.MouseLeftButtonUp += new MouseButtonEventHandler(mapSection.DragStop);
			this.MouseMove += new MouseEventHandler(mapSection.DragMove);

			Border interventionBorder = new Border();
			Thickness thickness = new Thickness();
			thickness.Top = 1;
			thickness.Bottom = 1;
			thickness.Left = 1;
			thickness.Right = 1;
			interventionBorder.BorderThickness = thickness;
			interventionBorder.CornerRadius = new CornerRadius(5);
			interventionBorder.BorderBrush = new SolidColorBrush(Colors.Red);
			this.Children.Add(interventionBorder);

			StackPanel verticalStack = new StackPanel();
			interventionBorder.Child = verticalStack;

			Grid grid = new Grid();
			verticalStack.Children.Add(grid);

			Rectangle imageRectangle = new Rectangle();
			imageRectangle.Width = size;
			imageRectangle.Height = size;
			ImageBrush img = new ImageBrush();
			img.ImageSource = Services.getImage("intervention");
			imageRectangle.Fill = img;
			grid.Children.Add(imageRectangle);

			Viewbox viewbox = new Viewbox();
			viewbox.Width = size;
			viewbox.Height = size;
			viewbox.HorizontalAlignment = HorizontalAlignment.Center;
			viewbox.VerticalAlignment = VerticalAlignment.Center;
			grid.Children.Add(viewbox);

			TextBlock nameLabel = new TextBlock();
			nameLabel.Text = interventionNumber.ToString();
			nameLabel.FontWeight = FontWeights.DemiBold;
			nameLabel.IsHitTestVisible = false;

			//Adding shadow effect so text appears clearly no matter what the background is
			DropShadowEffect shadow = new DropShadowEffect();
			shadow.ShadowDepth = 3;
			shadow.Direction = 315;
			shadow.Opacity = 1.0;
			shadow.BlurRadius = 3;
			shadow.Color = Colors.White;
			nameLabel.Effect = shadow;

			viewbox.Child = nameLabel;
        }
	}
}
