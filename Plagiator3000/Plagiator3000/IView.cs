using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiator3000
{
    interface IView
    {
        string fil { set; }
        string direct { set; }

        event Action Add_Fil;
        event Action Add_Direc;
        event Action Start_Prog;
    }
}
