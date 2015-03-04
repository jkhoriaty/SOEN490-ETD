using ETD.Models.Objects;
using ETD.Services;
using ETD.ViewsPresenters.MapSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ETD.CustomObjects.CustomUIObjects
{
    class MapModPin : Pin
    {
        private MapMod mapModification;

        public MapModPin(MapMod mapMod, AdditionalInfoPage aiSection, double width, double height)
            : base(mapMod, aiSection, aiSection.ActualWidth, aiSection.ActualHeight)
        {
            base.setImage(TechnicalServices.getImage(mapMod.getMapModType())); //Setting the image of the map modification pin
            mapModification = mapMod;//Providing a link to the map that this pin represents
        }
         
    }
}
