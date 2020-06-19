using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Technotheek.net_Core.Models.RoomSpace
{
    public class Room
    {
        public Room(SpaceBuilding buildingSpace)
        {
            this.buildingSpace = buildingSpace;
        }

        public enum SpaceBuilding
        {
            SmallBuilding = 5,
            MediumBuilding = 10,
            BigBuilding = 15,
        }
        public SpaceBuilding buildingSpace { get; set; }
    }
}
