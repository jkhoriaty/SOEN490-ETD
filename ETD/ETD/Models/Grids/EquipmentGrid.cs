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
    class EquipmentGrid : Grid
    {
        public EquipmentGrid(String equipmentName, MapSectionPage mapSection, int size) : base()
        {
            this.Name = equipmentName;
            this.Tag = "equipment";
            this.Width = size;
            this.Height = size;
            this.MouseLeftButtonDown += new MouseButtonEventHandler(mapSection.DragStart);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(mapSection.DragStop);
            this.MouseMove += new MouseEventHandler(mapSection.DragMove);
            this.ContextMenu = mapSection.Resources["ContextMenu"] as ContextMenu;

            Rectangle imageRectangle = new Rectangle();
            imageRectangle.Width = size;
            imageRectangle.Height = size;
            Equipments equipment = (Equipments)Enum.Parse(typeof(Equipments), equipmentName);
            ImageBrush img = new ImageBrush();
            img.ImageSource = TechnicalServices.getImage(equipment);
            imageRectangle.Fill = img;
            this.Children.Add(imageRectangle);
        }
    }
}
