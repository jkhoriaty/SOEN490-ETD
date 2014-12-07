using ETD.ViewsPresenters.MapSection;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace ETD.Models
{
    class TeamGrid : Grid
    {
        private Rectangle imageRectangle;
        public Team team;
        public TeamGrid(Team team, MapSectionPage mapSection, int size) : base()
        {
            this.team = team;

            this.Name = team.getName();
            this.Tag = "team";
            this.Width = size;
            this.Height = size;
            this.MouseLeftButtonDown += new MouseButtonEventHandler(mapSection.DragStart);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(mapSection.DragStop);
            this.MouseMove += new MouseEventHandler(mapSection.DragMove);
            this.ContextMenu = mapSection.Resources["TeamContext"] as ContextMenu;
            (this.ContextMenu.Items[0] as MenuItem).IsChecked = true;

            imageRectangle = new Rectangle();
            imageRectangle.Width = size;
            imageRectangle.Height = size;
            ImageBrush img = new ImageBrush();
            img.ImageSource = Services.getImage(team, team.getStatus());
            imageRectangle.Fill = img;
            this.Children.Add(imageRectangle);

			Viewbox viewbox = new Viewbox();
			viewbox.Width = size;
			viewbox.Height = size;
			viewbox.HorizontalAlignment = HorizontalAlignment.Center;
			viewbox.VerticalAlignment = VerticalAlignment.Center;
            this.Children.Add(viewbox);

			TextBlock nameLabel = new TextBlock();
			nameLabel.Text = team.getName();
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

        public void ChangeStatus(String status)
        {
            team.setStatus((statuses)Enum.Parse(typeof(statuses), status));
            ImageBrush img = new ImageBrush();
            img.ImageSource = Services.getImage(team, team.getStatus());
            imageRectangle.Fill = img;
        }
    }
}
