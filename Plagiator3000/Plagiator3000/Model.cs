using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SautinSoft.Document;
using System.Diagnostics;

namespace Plagiator3000
{
    class Model
    {
        string path;
        string path_dir;
        public string Load_Orig_Latex() //wczytywanie pliku z oryginalnym latexem
        {
            using(OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.RestoreDirectory = true;

                if(openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    path = openFileDialog.FileName;
                }
            }
            return path;
        }

        public string Load_Plagiat_Direc() //Wczytanie folderu z plikami i zwrot sciezki do folderu
        {
            using (FolderBrowserDialog win = new FolderBrowserDialog())
            {
                if(win.ShowDialog() == DialogResult.OK)
                {
                    path_dir = win.SelectedPath;
                }
            }
            return path_dir;
        }

        public void SameOrNot(string alg)
        {
            var files = Directory.EnumerateFiles(path_dir, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".tex"));
            string text = File.ReadAllText(path);
            Console.WriteLine(text);
            Console.WriteLine("-------------------------------------WZORY Z ORYGINALU:---------------------------------");
            string[] tab_oryg = WZORY.Orig_Latex_Operation_Wzory(path); //tablica z wzorami z oryginalu
            
            string wzor_oryg, wzor_plag; //zmienne gdzie sa przechowywane kolejno wzor oryginalu i wzor z plagiatu
            double sameornot; //zmienna ktora przechowuje podobienstwo
            Console.WriteLine("-------------------------------------BAZA PLIKOW:---------------------------------");
            foreach (string fileName in files)
            {
                Console.WriteLine("SCIEZKA DO PLIKU: ");
                Console.WriteLine(fileName);
                string File_Latex = fileName;
                
                Console.WriteLine("\nWZORY Z PLIKU: ");

                string[] tab_plag = WZORY.Orig_Latex_Operation_Wzory(File_Latex); //tablica ktora przechowuje wzory z plagiatu. Po jednym przejsciu petli foreach wczutuje wzory z nastepnego pliku           

                for (int i = 0; i < tab_oryg.Length; i++) //GLOWNA PETLA PROGRAMU. POROWNUJE WSZYSTKIE WZORY WYBRANYM ALGORYTMEM
                {
                    wzor_oryg = tab_oryg[i];

                    for (int j = 0; j < tab_plag.Length; j++)
                    {
                        wzor_plag = tab_plag[j];
                        Console.WriteLine("\nWzor oryginalny: " + wzor_oryg);
                        Console.WriteLine("Wzor plagiatu: " + wzor_plag);
                        if (alg == "CosineDistance")
                        { 
                            sameornot = Algorytm.CosineDistance(wzor_oryg, wzor_plag);
                        }
                        else 
                        {
                            sameornot = Algorytm.EuclideanDistance(wzor_oryg, wzor_plag);
                        }
                        
                        Console.WriteLine("SAME OR NOT------------------------------------------ ???");
                        Console.WriteLine(sameornot);
                    }
                }
                Console.WriteLine("-------------------------------------KONIEC PLIKU------------------------------------");
            }
        }

        public List<String> baza(List<String> sciezki) 
        {
            List<String> wzory_baza = new List<string> { };//główna lista z bazą wzorów

            foreach (string sciezka in sciezki)
            {
                string File_Latex = sciezka;
                string text = File.ReadAllText(File_Latex);

                text = File.ReadAllText(File_Latex).Replace(" ", "");

                string[] text_split = text.Split(new char[] { });

                string[] new_text = new string[text_split.Length];

                int j = 0;
                for (int i = 0; i < text_split.Length; i++)
                {
                    if (text_split[i] != "")
                    {
                        new_text[j] = text_split[i];
                        j++;
                    }
                }

                string[] mat = new string[new_text.Length];//tablica z wyodrębnionymi wzorami

                int l = 0;

                for (int i = 0; i < new_text.Length; i++)
                {
                    if (new_text[i] == @"\begin{math}")
                    {
                        mat[l] = new_text[i + 1];
                        wzory_baza.Add(new_text[i + 1]);
                        l++;
                    }
                    else if (new_text[i] == @"\begin{displaymath}")
                    {
                        mat[l] = new_text[i + 1];
                        wzory_baza.Add(new_text[i + 1]);
                        l++;
                    }
                    else if (new_text[i] == @"\begin{equation}")
                    {
                        mat[l] = new_text[i + 1];
                        wzory_baza.Add(new_text[i + 1]);
                        l++;
                    }
                }
            }

            return wzory_baza;//zwraca bazę
        }

        public List<String> sciezki(string Path)
        {
            List<String> sciezki = new List<string> { };

            DirectoryInfo di = new DirectoryInfo(Path);
            foreach (var fi in di.GetFiles("*.tex"))
            {
                sciezki.Add(fi.FullName);
                //Console.WriteLine(fi.FullName);
            }

            return sciezki;
        }

        private string konwersjaSlowa(string slowo)
        {
            string koncowe = "";
            for (int j = 0; j < slowo.Length; j++)
            {
                char sc = slowo[j];
                if (sc == '\\')
                {
                    koncowe += "$\\backslash$"; 
                }
                else if (sc == '_')
                {
                    koncowe += "\\_"; 
                }
                else
                {
                    koncowe += slowo[j];
                }
            }
            return koncowe;
        }

        public void raport(int[] tablica_wynikow, string[,] tablica_wynikow_wzory)
        {
            List<string> wzory_oryg = baza(new List<string> { path });
            List<string> wzory_test = baza(sciezki(path_dir));
            List<string> sciezki_test = sciezki(path_dir);
            int dl = tablica_wynikow.Length;
            string body_tex = "";
            string prebody_tex = "";

            prebody_tex += "\\begin{flushleft}\n" + "Plik bazowy : " + konwersjaSlowa(path) + "\n\\end{flushleft}\n\\hrule\n";
            for (int i = 0; i < sciezki_test.Count; i++) 
            {
                string lista_wzorow = "";
                Console.WriteLine(tablica_wynikow_wzory.GetLength(0));
                for (int j = 0; j < tablica_wynikow_wzory.GetLength(0); j++)
                {
                    //if (tablica_wynikow_wzory[j, 0] == sciezki_test[i])
                    if(true)
                    {
                        lista_wzorow += "{\\tiny " + tablica_wynikow_wzory[j, 1] + " jest podobny do " + tablica_wynikow_wzory[j, 2] + " w " + tablica_wynikow_wzory[j, 3] + "\\% } \\\\ \n";
                    }
                }
                body_tex += "\\begin{flushleft}\n" + "Plik : " + konwersjaSlowa(sciezki_test[i]) + "\\\\\n{\\huge Stopień podobieństwa: " + tablica_wynikow[i] + "\\%} \\\\ \n" + "Lista podobnych wzorów: \\\\ \n" + lista_wzorow + "\n\\end{flushleft}\n\\hrule\n";
            }
            var raportTEX = "\\documentclass{article}\n\\usepackage{polski}\n\\usepackage[utf8]{inputenc}\n\\usepackage{ragged2e}\n\\begin{document}\n\\title{\\huge\\bfseries Raport porównania plików }\n\\date{\\today}\n\\maketitle\n" + prebody_tex + body_tex + "\\end{document}";

            string PATH = path_dir + "\\raport";
            string PATHtex = PATH + "\\raport.tex";
            string PATHbat = PATH + "\\batch.bat";
            bool exists = System.IO.Directory.Exists(PATH);
            if (exists)
            {
                Console.WriteLine("Stworzono raport do istniejącej ścieżki raport");
                System.IO.File.WriteAllText(PATHtex, raportTEX);
            }
            else if (!exists)
            {
                Console.WriteLine("Stworzono raport do nie istniejącej ścieżki raport");
                System.IO.Directory.CreateDirectory(PATH);
                System.IO.File.WriteAllText(PATHtex, raportTEX);
                //Process.Start("chrome.exe", PATHtex);
            }

            //Process p1 = new Process();
            //string batbatch = "rem %1 represents the file name with no extension.\npdflatex - shell - escape % 1";
            //System.IO.File.WriteAllText(PATHbat, batbatch);
            //p1.StartInfo.FileName = PATHbat;
            //p1.StartInfo.Arguments = PATHtex;
            //p1.StartInfo.UseShellExecute = false;
            //p1.Start();
            //p1.WaitForExit();


            MessageBox.Show("Raport został stworzony");
        }
    }
}
