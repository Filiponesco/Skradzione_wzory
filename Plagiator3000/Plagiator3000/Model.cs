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
        string[] table = new string[4];
        List<double> listwithalgo = new List<double> { };
        List<double> listmaintex = new List<double> { };
        List<string[]> main_list = new List<string[]> { };
        double sum = 0;
        double main_proc = 0;
        int iter = 0;
        int asd=0;
        public string Load_Orig_Latex() //wczytywanie pliku z oryginalnym latexem
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
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
                if (win.ShowDialog() == DialogResult.OK)
                {
                    path_dir = win.SelectedPath;
                }
            }
            return path_dir;
        }

        public void SameOrNot(string alg, string err)
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
                if (alg == "Third")
                    for (int i = 0; i < tab_plag.Length; i++)
                    {

                        double third = Algorytm.Trzeci(tab_oryg, tab_plag);
                        sameornot = third;
                        if (third < 0) sameornot = sameornot * -1;
                        Console.WriteLine(fileName, sameornot);
                        listwithalgo.Add(sameornot);
                        sum += sameornot;
                        iter++;
                        asd = 1;
                    }
                else
                {

                    for (int i = 0;
                        i < tab_oryg.Length;
                        i++) //GLOWNA PETLA PROGRAMU. POROWNUJE WSZYSTKIE WZORY WYBRANYM ALGORYTMEM
                    {
                        wzor_oryg = tab_oryg[i];

                        for (int j = 0; j < tab_plag.Length; j++)
                        {
                            wzor_plag = tab_plag[j];

                            Console.WriteLine("\nWzor oryginalny: " + wzor_oryg);
                            Console.WriteLine("Wzor plagiatu: " + wzor_plag);
                            if (alg == "CosineDistance")
                            {
                                double cosDist = Algorytm.CosineDistance(wzor_oryg, wzor_plag);
                                sameornot = Algorytm.ToPercent(alg, cosDist);
                            }
                            else
                            {
                                double euclDist = Algorytm.EuclideanDistance(wzor_oryg, wzor_plag);
                                sameornot = Algorytm.ToPercent(alg, euclDist);
                            }

                            Console.WriteLine("SAME OR NOT------------------------------------------ ???");
                            Console.WriteLine(sameornot);

                            listwithalgo.Add(sameornot);
                            main_list.Add(new string[] {fileName, wzor_plag, wzor_oryg, sameornot.ToString()});
                            sum += sameornot;
                            iter++;
                        }
                    }
                }

                Console.WriteLine("-------------------------------------KONIEC PLIKU------------------------------------");

                main_proc = sum / iter;
                listmaintex.Add(main_proc);
                main_proc = 0;
                sum = 0;
                iter = 0;
            }

            raport(listmaintex, main_list, err);                     
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

        private string konversjaNajlepszegoSlowaNaSwiecie(string slowo)
        {
            string koncowe = "";
            for (int j = 0; j < slowo.Length; j++)
            {
                char sc = slowo[j];
                if (sc == '\\' && slowo[j + 1] == 'i' && slowo[j + 2] == 'n' && slowo[j + 3] == 't')
                {
                    koncowe += "\\int ";
                    j += 3;
                }
                else if (sc == '\\' && slowo[j + 1] == 'c' && slowo[j + 2] == 'd' && slowo[j + 3] == 'o' && slowo[j + 4] == 't')
                {
                    koncowe += "\\cdot ";
                    j += 4;
                }
                else if (sc == '\\' && slowo[j + 1] == 'P' && slowo[j + 2] == 'i')
                {
                    koncowe += "\\Pi ";
                    j += 2;
                }
                else if (sc == '\\' && slowo[j + 1] == 'i' && slowo[j + 2] == 'n')
                {
                    koncowe += "\\in ";
                    j += 2;
                }
                else if (sc == '\\' && slowo[j + 1] == 'l' && slowo[j + 2] == 'n')
                {
                    koncowe += "\\ln ";
                    j += 2;
                }
                else if (sc == '\\' && slowo[j + 1] == 't' && slowo[j + 2] == 'i' && slowo[j + 3] == 'm' && slowo[j + 4] == 'e' && slowo[j + 5] == 's')
                {
                    koncowe += "\\times ";
                    j += 5;
                }
                else if (sc == '\\' && slowo[j + 1] == 'd' && slowo[j + 2] == 'e' && slowo[j + 3] == 'l' && slowo[j + 4] == 't' && slowo[j + 5] == 'a')
                {
                    koncowe += "\\delta ";
                    j += 5;
                }
                else if (sc == '\\' && slowo[j + 1] == 'D' && slowo[j + 2] == 'e' && slowo[j + 3] == 'l' && slowo[j + 4] == 't' && slowo[j + 5] == 'a')
                {
                    koncowe += "\\Delta ";
                    j += 5;
                }
                else if (sc == '\\' && slowo[j + 1] == 'c' && slowo[j + 2] == 'h' && slowo[j + 3] == 'o' && slowo[j + 4] == 'o' && slowo[j + 5] == 's' && slowo[j + 6] == 'e')
                {
                    koncowe += "\\choose ";
                    j += 6;
                }
                else
                {
                    koncowe += slowo[j];
                }
            }
            return koncowe;
        }
        public void raport(List<double> tablica_wynikow, List<string[]> tablica_wynikow_wzory, string err)
        {
            List<string> sciezki_test = sciezki(path_dir);
            string body_tex = "";
            string prebody_tex = "";
            string body_tex2 = "";
            string prebody_tex2 = "";
            string body_html3 = "";
            string body_html4 = "";

            //Tworzenie stringa z raportem ogólnym
            prebody_tex += "\\begin{flushleft}\n" + "Plik bazowy : " + konwersjaSlowa(path) + "\n\\end{flushleft}\n\\hrule\n";
            for (int i = 0; i < sciezki_test.Count; i++)
            {
                if (tablica_wynikow[i] < int.Parse(err))
                {
                    if (asd == 0)
                    {
                        body_tex += "\\begin{flushleft}\n" + "Plik : " + konwersjaSlowa(sciezki_test[i]) + "\\\\\n{\\huge Stopień podobieństwa: " + tablica_wynikow[i] + "\\%} \\\\ \n" + "\n\\end{flushleft}\n\\hrule\n";
                    }
                    else
                    {
                        body_tex += "\\begin{flushleft}\n" + "Plik : " + konwersjaSlowa(sciezki_test[i]) + "\\\\\n{\\Różnice ilości znaków między plikami: " + tablica_wynikow[i] + "\\%} \\\\ \n" + "\n\\end{flushleft}\n\\hrule\n";
                    }
                }
                else
                {
                    body_tex += "\\begin{flushleft}\n" + "Plik : \\textcolor{Red}{";
                    body_tex += konwersjaSlowa(sciezki_test[i]);
                    body_tex += "}\\\\\n{\\huge Stopień podobieństwa: \\textcolor{Red}{";
                    body_tex += tablica_wynikow[i] + "}\\%} \\\\ \n" + "\n\\end{flushleft}\n\\hrule\n";
                }
            }
            var raportTEX = "\\documentclass{article}\n\\usepackage{polski}\n\\usepackage[utf8]{inputenc}\n\\usepackage{ragged2e}\n\\usepackage[dvipsnames]{xcolor}\n\\begin{document}\n\\title{\\huge\\bfseries Raport ogólny porównania plików }\n\\date{\\today}\n\\maketitle\n" + prebody_tex + body_tex + "\\end{document}";

            //Tworzenie stringa z raportem szczegółowym
            prebody_tex2 += "\\begin{flushleft}\n" + "Plik bazowy : " + konwersjaSlowa(path) + "\n\\end{flushleft}\n\\hrule\n";
            for (int i = 0; i < sciezki_test.Count; i++)
            {
                string lista_wzorow2 = "\\begin{longtable}{|c|c|c|} \n \\hline \n Wzór & Jest podobny do & Procent podobieństwa \\\\ \\hline  \n";
                for (int j = 0; j < tablica_wynikow_wzory.Count; j++)
                {
                    lista_wzorow2 += "$" + konversjaNajlepszegoSlowaNaSwiecie(tablica_wynikow_wzory[j][1]) + "$ & $" + konversjaNajlepszegoSlowaNaSwiecie(tablica_wynikow_wzory[j][2]) + "$ & $" + tablica_wynikow_wzory[j][3] + "$ \\\\ \\hline \n";
                }
                lista_wzorow2 += "\\end{longtable} \n";
                body_tex2 += "\\begin{flushleft}\n" + "Plik : " + konwersjaSlowa(sciezki_test[i]) + "\\\\ \nLista podobnych wzorów: \\\\ \n" + lista_wzorow2 + "\n\\end{flushleft}\n\\hrule\n";
            }
            var raportTEX2 = "\\documentclass{article}\n\\usepackage{polski}\n\\usepackage[utf8]{inputenc}\n\\usepackage{ragged2e}\n\\usepackage{longtable}\n\\begin{document}\n\\title{\\huge\\bfseries Raport szczegółowy porównania plików }\n\\date{\\today}\n\\maketitle\n" + prebody_tex2 + body_tex2 + "\\end{document}";

            //HTML
            //Tworzenie stringa z raportem ogólnym
            for (int i = 0; i < sciezki_test.Count; i++)
            {
                if (tablica_wynikow[i] < int.Parse(err))//nie zaznacza na czerwono 
                {
                    if (asd == 0)
                    {
                        body_html3 += "<h3>Plik: " + sciezki_test[i] +
                                      " </h3> \n <h2>Stopień podobieństwa: </h2> \n <h3> " + tablica_wynikow[i] +
                                      " </h3> \n <hr> \n";
                    }
                    else
                    {
                        body_html3 += "<h3>Plik: " + sciezki_test[i] +
                                      " </h3> \n <h2>Różnica ilości znaków między plikami: </h2> \n <h3> " + tablica_wynikow[i] +
                                      " </h3> \n <hr> \n";
                    }
                }
                else//zaznacza na czerwono
                {
                    if (asd == 0)
                    {
                        body_html3 += "<h3><font color=\"red\">Plik: " + sciezki_test[i] + " </font></h3> \n <h2>Stopień podobieństwa: </h2> \n <h3><font color=\"red\"> " + tablica_wynikow[i] + " </font></h3> \n <hr> \n";
                    }
                    else
                    {
                        body_html3 += "<h3>Plik: " + sciezki_test[i] + " </h3> \n <h2>Rożnica ilości znaków między plikami: </h2> \n " + tablica_wynikow[i] + " </h3> \n <hr> \n";
                    }
                }
            }
            var raportTEX3 = "<!DOCTYPE html> \n <html lang=\"pl\"> \n <head> \n <meta charset=\"utf-8\"> \n <script src=\"https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.1/MathJax.js?config=TeX-AMS-MML_HTMLorMML\"></script> \n <title>Raport</title> \n </head> \n <body> \n <header> \n <h1>Raport ogólny porównania plików</h1> \n </header> \n <h2>Plik bazowy: " + path + "</h2> \n <hr> \n " + body_html3 + " </body> \n </html>";

            //Tworzenie stringa z raportem szczegółowym
            for (int i = 0; i < sciezki_test.Count; i++)
            {
                body_html4 += "<h3>Plik: " + sciezki_test[i] + " </h3> \n <hr> \n <table style=\"width:100% \"> \n <tr> \n <th>Wzór</th> \n <th>Jest podobny do </th> \n <th>Procent podobieństwa</th> \n </tr> \n";
                for (int j = 0; j < tablica_wynikow_wzory.Count; j++)
                {
                    body_html4 += "<tr> \n <td><script type=\"math/tex; mode=display\"> " + konversjaNajlepszegoSlowaNaSwiecie(tablica_wynikow_wzory[j][1]) + " </script></td> \n <td><script type=\"math/tex; mode=display\"> " + konversjaNajlepszegoSlowaNaSwiecie(tablica_wynikow_wzory[j][2]) + " </script></td> \n <td><script type=\"math/tex; mode=display\"> " + konversjaNajlepszegoSlowaNaSwiecie(tablica_wynikow_wzory[j][3]) + " </script></td> \n </tr> \n";
                }
                body_html4 += "</table> \n";
            }
            var raportTEX4 = "<!DOCTYPE html> \n <html lang=\"pl\"> \n <head> \n <meta charset=\"utf-8\"> \n <script src=\"https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.1/MathJax.js?config=TeX-AMS-MML_HTMLorMML\"></script> \n <title>Raport</title> \n <style> \n table, th, td { \n border: 1px solid black; \n } \n </style> \n </head> \n <body> \n <header> \n <h1>Raport szczegółowy porównania plików</h1> \n </header> \n <h2>Plik bazowy: " + path + "</h2> \n" + body_html4 + " </body> \n </html>";

            //Zapis
            DateTime dt = DateTime.Now;
            string PATH = Path.GetDirectoryName(path) + "\\raport\\" + dt.ToString("MM_dd_yyyy_hh_mm_ss_ffff");
            string PATHtex = PATH + "\\raportOGL.tex";
            string PATHtex2 = PATH + "\\raportSZCZ.tex";
            string PATHtex3 = PATH + "\\raportOGL.html";
            string PATHtex4 = PATH + "\\raportSZCZ.html";
            bool exists = System.IO.Directory.Exists(PATH);
            if (exists)
            {
                System.IO.File.WriteAllText(PATHtex, raportTEX);
                System.IO.File.WriteAllText(PATHtex2, raportTEX2);
                System.IO.File.WriteAllText(PATHtex3, raportTEX3);
                System.IO.File.WriteAllText(PATHtex4, raportTEX4);

                Process.Start("chrome.exe", PATHtex3);
                if (asd == 0)
                {
                    Process.Start("chrome.exe", PATHtex4);
                }

            }
            else if (!exists)
            {
                System.IO.Directory.CreateDirectory(PATH);
                System.IO.File.WriteAllText(PATHtex, raportTEX);
                System.IO.File.WriteAllText(PATHtex2, raportTEX2);
                System.IO.File.WriteAllText(PATHtex3, raportTEX3);
                System.IO.File.WriteAllText(PATHtex4, raportTEX4);

                Process.Start("chrome.exe", PATHtex3);
                if (asd == 0)
                {
                    Process.Start("chrome.exe", PATHtex4);
                }
            }
            MessageBox.Show("Raport został stworzony");
        }
    }
}
