using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiator3000
{
    interface IView
    {
        string file { set; }
        string direct { set; }
        string alg { get; }
        string err { get; }

        void Message(string s);

        event Action Add_File;
        event Action Add_Direc;
        event Action Start_Prog;
    }
}
