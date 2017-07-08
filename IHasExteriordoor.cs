using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hide_and_Seek
{
    interface IHasExteriordoor
    {
        string DoorDescription { get; }
        Location DoorLocation { set; get; }
    }
}
