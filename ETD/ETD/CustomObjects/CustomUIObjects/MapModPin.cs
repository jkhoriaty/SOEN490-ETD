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
        private static int size = 30;
        private AdditionalInfoPage AIPmap;
  
        private double height;

        //Normal Map modification items(  camp, circle, line, ramp, rectangle, square, stairs )
        public MapModPin(MapMod mapMod, AdditionalInfoPage aiSection) : base(mapMod, aiSection, size)
        {
            base.setImage(TechnicalServices.getImage(mapMod.getMapModType()));
            mapModification = mapMod;
        }
        
        //Line items
        public MapModPin(MapMod mapMod, AdditionalInfoPage aiSection,  double width, double height)
            : base(mapMod, aiSection, aiSection.AdditionalMap.ActualWidth, aiSection.AdditionalMap.ActualHeight )
        {
            base.setImage(TechnicalServices.getImage(mapMod.getMapModType()));
            mapModification = mapMod;
        }
         
    }
}
