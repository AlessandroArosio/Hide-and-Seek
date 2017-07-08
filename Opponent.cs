using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Hide_and_Seek
{
    class Opponent
    {
        private Random random;
        private Location myLocation;

        public Opponent(Location startingLocation)
        {
            myLocation = startingLocation;
            random = new Random();
        }

        public void Move()
        {
            bool hidden = false;
            while (!hidden)
            {
                if (myLocation is IHasExteriordoor)
                {
                    IHasExteriordoor locationWithDoor = myLocation as IHasExteriordoor;
                    if (random.Next(2) == 1)
                        myLocation = locationWithDoor.DoorLocation;
                }
                int rand = random.Next(myLocation.Exits.Length);
                myLocation = myLocation.Exits[rand];
                if (myLocation is IHidingPlaces)
                    hidden = true;
            }
                
        }

        public bool Check(Location locationToCheck)
        {
            if (locationToCheck != myLocation)
                return false;
            else
                return true;
        }
    }
}
