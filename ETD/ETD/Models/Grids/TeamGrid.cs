using ETD.ViewsPresenters.MapSection;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using ETD.Models.Objects;
using ETD.Services;

namespace ETD.Models.Grids
{
    public class TeamGrid : Grid
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
            this.ContextMenu = mapSection.Resources["ContextMenu"] as ContextMenu;
			this.ContextMenuOpening += new ContextMenuEventHandler(mapSection.CheckRight);
            (this.ContextMenu.Items[0] as MenuItem).IsChecked = true;

            imageRectangle = new Rectangle();
            imageRectangle.Width = size;
            imageRectangle.Height = size;
            ImageBrush img = new ImageBrush();
            img.ImageSource = TechnicalServices.getImage(team, team.getStatus());
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

			viewbox.Child = nameLabel;
        }

        public void ChangeStatus(String status)
        {
            team.setStatus((Statuses)Enum.Parse(typeof(Statuses), status));
            ImageBrush img = new ImageBrush();
            img.ImageSource = TechnicalServices.getImage(team, team.getStatus());
            imageRectangle.Fill = img;
        }

        public string GetStatus()
        {
            return team.getStatus().ToString();
        }
    }
}
