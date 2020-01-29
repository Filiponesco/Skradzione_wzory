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
            try
            {
                view.file = model.Load_Orig_Latex();
            }
            catch(Exception e)
            {
                view.Message("Błąd ładowania oryginalnych plików: " + e.Message);
            }
        }

        public void Plag_Direc()
        {
            try
            {
                view.direct = model.Load_Plagiat_Direc();
            }
            catch (Exception e)
            {
                view.Message("Błąd ładowania plagiatów: " + e.Message);
            }
        }

        public void Operation()
        {
            try
            {
                model.SameOrNot(view.alg, view.err);
            }
            catch (Exception e)
            {
                view.Message("Błąd algorytmu: " + e.Message);
            }
        }

    }
}
