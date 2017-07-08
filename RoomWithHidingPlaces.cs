using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hide_and_Seek
{
    class RoomWithHidingPlaces : Room, IHidingPlaces
    {
        public string HidingPlaceName { get; }

        public RoomWithHidingPlaces(string name, string decoration, string hidingPlace) : base(name, decoration)
        {
            HidingPlaceName = hidingPlace;
        }

        public override string Description
        {
            get
            {
                return base.Description + " Someone could hide " + HidingPlaceName + ".";
            }
        }

    }
}
