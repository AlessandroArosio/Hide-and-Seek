using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hide_and_Seek
{
    class RoomWithDoor : RoomWithHidingPlaces, IHasExteriordoor
    {
        public string DoorDescription { get; private set; }
        public Location DoorLocation { get; set; }

        public RoomWithDoor(string name, string decoration, string hidingNamePlace, string doorDescription) : base(name, decoration, hidingNamePlace)
        {
            DoorDescription = doorDescription;

        }
        public override string Description
        {
            get
            {
                return base.Description + " You see also " + DoorDescription + ".";
            }
        }
    }
}
