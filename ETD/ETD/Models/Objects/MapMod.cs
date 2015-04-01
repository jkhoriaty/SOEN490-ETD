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
   
    //Mapmofidication items
    public enum MapMods { camp, circle, line, ramp, rectangle, square, stairs, text };

    [Serializable()]
    public class MapMod : Observable
    {
        private static List<MapMod> mapModList = new List<MapMod>();//Contains a list of map modification objects

        private MapMods mapModType;
        private int mapModID;

        //Creates a map modification object
        public MapMod(String name)
       {
           mapModType = (MapMods)Enum.Parse(typeof(MapMods), name);
           mapModList.Add(this);
           mapModID = mapModList.Count;
           ClassModifiedNotification(typeof(MapMod));
       }

        //Accessors

        //Returns the type of the map modification
        public MapMods getMapModType()
        {
            return mapModType;
        }

        //Returns the list of map modification objects
        public static List<MapMod> getMapModList()
        {
            return mapModList;
        }

        public int getID()
        {
            return mapModID;
        }
    }
}
