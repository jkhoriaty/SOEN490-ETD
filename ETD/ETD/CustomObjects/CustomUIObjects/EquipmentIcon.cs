using ETD.Services;
using ETD.ViewsPresenters.MapSection;
using ETD.ViewsPresenters.TeamsSection.TeamInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows;
using ETD.Models.Objects;

namespace ETD.CustomObjects.CustomUIObjects
{
    class EquipmentIcon : Grid
    {
        Equipment equip;
        Team team;
        Rectangle rekt;

        public EquipmentIcon(Team relatedTeam,TeamInfoPage teamInfoPage,int size, Equipment relatedEquipment) : base()
        {
            this.Width = size;
            this.Height = size;
            this.FlowDirection = FlowDirection.LeftToRight;

            this.MouseRightButtonDown += new MouseButtonEventHandler(teamInfoPage.RemoveTeamEquipment);

            this.equip = relatedEquipment;
            this.team = relatedTeam;
        }

        public void SetImage(BitmapImage image)
        {
            Rectangle imageRectangle = new Rectangle();
            imageRectangle.Width = this.Width;
            imageRectangle.Height = this.Height;
            ImageBrush img = new ImageBrush();
            img.ImageSource = image;
            imageRectangle.Fill = img;
            this.Children.Add(imageRectangle);
        }

        public Team GetTeam()
        {
            return this.team;
        }

        public Equipment GetEquip()
        {
            return this.equip;
        }
    }
}
