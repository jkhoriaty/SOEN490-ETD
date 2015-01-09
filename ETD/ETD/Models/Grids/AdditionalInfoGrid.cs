using ETD.ViewsPresenters.MapSection;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using ETD.Models.Objects;
using ETD.Models.Services;

namespace ETD.Models.Grids
{
    class AdditionalInfoGrid : Grid
    {
        public AdditionalInfoGrid(String AIPName, AdditionalInfoPage AIPmap, int size) : base()
        {
            this.Name = AIPName;
            this.Tag = "additionalinfo";
            this.Width = size;
            this.Height = size;
            this.MouseLeftButtonDown += new MouseButtonEventHandler(AIPmap.DragStart);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(AIPmap.DragStop);
            this.MouseMove += new MouseEventHandler(AIPmap.DragMove);
            this.ContextMenu = AIPmap.Resources["AIcontext"] as ContextMenu;
            (this.ContextMenu.Items[0] as MenuItem).IsChecked = true;

            Rectangle imageRectangle = new Rectangle();
            imageRectangle.Width = size;
            imageRectangle.Height = size;
            AdditionalInfos AIP = (AdditionalInfos)Enum.Parse(typeof(AdditionalInfos), AIPName);
            ImageBrush img = new ImageBrush();
            img.ImageSource = TechnicalServices.getImage(AIP);
            imageRectangle.Fill = img;
            this.Children.Add(imageRectangle);
        }

    }
}
