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
            //view.file = model.Orig_Latex_Operation();
<<<<<<< Updated upstream
<<<<<<< Updated upstream

            //List<String> baza = new List<string> { "baza1", "baza2", "baza3", "baza4", "baza5", "baza6" };//wzory z bazy
            //List<String[]> testy = new List<string[]> { new string[] { "baza1", "path1" }, new string[] { "testy2", "path2" }, new string[] { "baza3", "path3" }, new string[] { "testy4", "path4" } };

            //List<String> sciezki = new List<string> { };
            //sciezki.Add(@"C:\Users\Arkad\Desktop\TEST_ORIG\document.tex");
            //sciezki.Add(@"C:\Users\Arkad\Desktop\TEST_ORIG\document.tex");
            //foreach (string x in model.baza(sciezki))
            //{
            //    Console.WriteLine(x);
            //}

            //List<String> sciezki = new List<string> { };
            //sciezki.Add(@"C:\Users\Arkad\Desktop\TEST_ORIG\document.tex");
            //sciezki.Add(@"C:\Users\Arkad\Desktop\TEST_ORIG\document.tex");
            //foreach (string[] x in model.testy(sciezki))
            //{
            //    Console.WriteLine(x[0] + " " + x[1]);
            //}
            //model.sciezki(@"C:\Users\Arkad\Desktop\TEST_ORIG\");
            //foreach (string a in model.sciezki(@"C:\Users\Arkad\Desktop\TEST_ORIG\"))
            //{
            //    Console.WriteLine(a);
            //}
            //model.Euclidan(baza, testy);
            model.Euclidan();

=======
=======
>>>>>>> Stashed changes
            //model.Euclidan();
            model.raport(new int[] { 1,2,3,4,5,6,7,8,9 });

>>>>>>> Stashed changes
        }
    }
}
