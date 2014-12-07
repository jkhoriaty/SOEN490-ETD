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
            

            Rectangle imageRectangle = new Rectangle();
            imageRectangle.Width = size;
            imageRectangle.Height = size;
            equipments equipment = (equipments)Enum.Parse(typeof(equipments), equipmentName);
            ImageBrush img = new ImageBrush();
            img.ImageSource = Services.getImage(equipment);
            imageRectangle.Fill = img;
            this.Children.Add(imageRectangle);
        }
    }
}
