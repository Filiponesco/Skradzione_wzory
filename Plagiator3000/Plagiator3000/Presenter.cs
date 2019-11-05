using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiator3000
{
    class Presenter
    {
        IView view;
        Model model;
        
        public Presenter(IView view, Model model)
        {
            this.view = view;
            this.model = model;
            this.view.Add_File += OriginalFile;
            this.view.Add_Direc += Plag_Direc;
            this.view.Start_Prog += Operation;
        }

        public void OriginalFile()
        {
            view.file = model.Load_Orig_Latex();
        }

        public void Plag_Direc()
        {
            view.direct = model.Load_Plagiat_Direc();
        }

        public void Operation()
        {
            view.file = model.Orig_Latex_Operation();
        }
    }
}
