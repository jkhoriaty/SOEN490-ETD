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
        private int size = 40;
        private Rectangle imageRectangle;
        private Team team;
        public TeamGrid(Team team, MapSectionPage caller) : base()
        {
            this.team = team;

            this.Name = team.getName();
            this.Tag = "team";
            this.Width = size;
            this.Height = size;
            this.MouseLeftButtonDown += new MouseButtonEventHandler(caller.DragStart);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(caller.DragStop);
            this.MouseMove += new MouseEventHandler(caller.DragMove);
            this.ContextMenu = caller.Resources["TeamContext"] as ContextMenu;
            (this.ContextMenu.Items[0] as MenuItem).IsChecked = true;

            imageRectangle = new Rectangle();
            imageRectangle.Width = size;
            imageRectangle.Height = size;
            ImageBrush img = new ImageBrush();
            img.ImageSource = Services.getImage(team, team.getStatus());
            imageRectangle.Fill = img;
            this.Children.Add(imageRectangle);

            Label nameLabel = new Label();
            nameLabel.Width = size;
            nameLabel.Height = size;
            nameLabel.FontWeight = FontWeights.DemiBold;
            nameLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            nameLabel.VerticalContentAlignment = VerticalAlignment.Center;
            nameLabel.IsHitTestVisible = false;

            //Adding shadow effect so text appears clearly no matter what the background is
            DropShadowEffect shadow = new DropShadowEffect();
            shadow.ShadowDepth = 3;
            shadow.Direction = 315;
            shadow.Opacity = 1.0;
            shadow.BlurRadius = 3;
            shadow.Color = Colors.White;
            nameLabel.Effect = shadow;

            //Determining size of text and margin to make the text appear perfectly centered
            Thickness labelMargin = nameLabel.Margin;
            nameLabel.Content = team.getName();
            switch (nameLabel.Content.ToString().Length)
            {
                case 1:
                    nameLabel.FontSize = 28;
                    labelMargin.Top = -10;
                    break;
                case 2:
                    nameLabel.FontSize = 24;
                    labelMargin.Top = -6;
                    break;
                case 3:
                    nameLabel.FontSize = 20;
                    labelMargin.Top = -3;
                    break;
                case 4:
                default:
                    nameLabel.FontSize = 16;
                    labelMargin.Top = -2;
                    break;
                case 5:
                case 6:
                    nameLabel.FontSize = 12;
                    labelMargin.Top = -1;
                    break;
            }
            nameLabel.Margin = labelMargin;

            this.Children.Add(nameLabel);
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
