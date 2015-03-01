using ETD.Models.ArchitecturalObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETD.Models.Objects
{
    /// <summary>
    /// Map Model Object
    /// </summary>
    public enum MapMods { camp, circle, line, ramp, rectangle, square, stairs, text };

    public class MapMod : Observable
    {
        private static List<MapMod> mapModList = new List<MapMod>();

        private MapMods mapModType;

        public MapMod(String name)
       {
           mapModType = (MapMods)Enum.Parse(typeof(MapMods), name);
           mapModList.Add(this);
           ClassModifiedNotification(typeof(MapMod));
       }

        //getters

        public MapMods getMapModType()
        {
            return mapModType;
        }

        public static List<MapMod> getMapModList()
        {
            return mapModList;
        }

    }
}
