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
            model.Orig_Latex_Operation_Mat();
            model.Orig_Latex_Operation_Wzory();

            model.raport(new int[] { 1, 2, 3, 3, 4, 5, 6, 7, 9 }, new string[,] { { "sciezka1", "w1","w1","p1" },{"sciezka2","w2","w2","p2"},{"sciezka3","w3","w3","p3" },{"sciezka4","w4","w4","p4" },{ "sciezka5","w5","w5","p5"} });

        }
    }
}
