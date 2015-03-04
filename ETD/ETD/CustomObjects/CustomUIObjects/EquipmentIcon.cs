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
        Equipment equip; //Reference to the equipment represented by the icon
        Team team; //Reference to the team that holds the equipment

        //Default constructor, takes in the specific information that is necessary to the existence of this object.
        public EquipmentIcon(Team relatedTeam,TeamInfoPage teamInfoPage,int size, Equipment relatedEquipment) : base()
        {
            this.Width = size;
            this.Height = size;
            this.FlowDirection = FlowDirection.LeftToRight;
            this.MouseRightButtonDown += new MouseButtonEventHandler(teamInfoPage.RemoveTeamEquipment);
            this.equip = relatedEquipment;
            this.team = relatedTeam;
        }

        //Sets the image that is show in this icon
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

        //Basic accessor, returns the team the equipment is associated with
        public Team GetTeam()
        {
            return this.team;
        }
        //Basic accessor, returns the equipment the team is associated with
        public Equipment GetEquip()
        {
            return this.equip;
        }
    }
}
