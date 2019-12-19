﻿using System;
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
        string path = "d:\\";
        string path_dir = "d:\\";
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

        public string Load_Plagiat_Direc() //Wczytanie folderu z plikami
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

        public void Load_Plagiat_Files() //Wczytuje pliki z folderu
        {
            var files = Directory.EnumerateFiles(path_dir, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".tex"));

            foreach (string fileName in files)
            {
                Console.WriteLine(fileName);
                string File_Latex = fileName;
                string text = File.ReadAllText(File_Latex);
                Console.WriteLine(text);
            }
        }

        public string Orig_Latex_Operation() //Operacje na oryginalnym latexie
        {
            string File_Latex = path;
            string text_load = File.ReadAllText(File_Latex);

            string[] text_opt = new string[text_load.Length + 20]; //do poprawy wczytywan wczytywan

            text_load = File.ReadAllText(File_Latex).Replace(" ", "");

            Console.WriteLine("Tekst: " + text_load);

            int k = 0, n = 0;
            for (int i = 0; i < text_load.Length; i++)
            {
                if ( (text_load[i].ToString() == @"\".ToString() ) && (text_load[i + 1].ToString() == @"e".ToString()) && (text_load[i + 5].ToString() == @"m".ToString()) && (text_load[i +9].ToString() == @"}".ToString()) )
                {
                    for(int m = 0; m < 11; m++)
                    {
                        if (n == 10)
                        {
                            text_opt[k] = "\n".ToString();
                            k++;
                            text_opt[k] = text_load[i].ToString();
                            i++;
                            k++;
                        }
                        else
                        {
                            text_opt[k] = text_load[i].ToString();
                            k++;
                            i++;
                            n++;
                        }
                    }
                    n = 0;
                }
                else if((text_load[i].ToString() == @"\".ToString()) && (text_load[i + 1].ToString() == @"e".ToString()) && (text_load[i + 5].ToString() == @"d".ToString()) && (text_load[i + 9].ToString() == @"l".ToString()) && (text_load[i + 16].ToString() == @"}".ToString()))
                {
                    for (int m = 0; m < 18; m++)
                    {
                        if (n == 17)
                        {
                            text_opt[k] = "\n".ToString();
                            k++;
                            text_opt[k] = text_load[i].ToString();
                            i++;
                            k++;
                        }
                        else
                        {
                            text_opt[k] = text_load[i].ToString();
                            k++;
                            i++;
                            n++;
                        }
                    }
                    n = 0;
                }
                else if ((text_load[i].ToString() == @"\".ToString()) && (text_load[i + 1].ToString() == @"e".ToString()) && (text_load[i + 5].ToString() == @"e".ToString()) && (text_load[i + 9].ToString() == @"t".ToString()) && (text_load[i + 13].ToString() == @"}".ToString()))
                {
                    for (int m = 0; m < 15; m++)
                    {
                        if (n == 14)
                        {
                            text_opt[k] = "\n".ToString();
                            k++;
                            text_opt[k] = text_load[i].ToString();
                            i++;
                            k++;
                        }
                        else
                        {
                            text_opt[k] = text_load[i].ToString();
                            k++;
                            i++;
                            n++;
                        }
                    }
                    n = 0;
                }
                else
                {
                    text_opt[k] = text_load[i].ToString();
                    k++;
                }
            }
            k = 0;

            string text = "";

            text = String.Join("", text_opt);

            string[] text_split = text.Split(new char[] { });
            

            string[] new_text = new string[text_split.Length];

            int j = 0;
            for (int i = 0; i < text_split.Length; i++) //zapisuje litery jako linie tekstu
            {
                if (text_split[i] != "")
                {
                    new_text[j] = text_split[i];
                    j++;
                }
            }

            string[] mat = new string[new_text.Length]; // tablica ktorej kazdy element to jedna linia wzoru
            string[] wzory = new string[new_text.Length]; //tablica w ktorej jeden element to caly wzor
            string[] pom = new string[new_text.Length]; //tablica pomocnicza do zapisu calego wzoru
            string[] znaki = new string[text.Length]; //tablica pomocnicza do zapisu znakow (chary przeciazone na stringi metoda toString)
            string join_char = ""; //pomocniczy string do laczenia tablicy charow w stringi

            int l = 0; // licznik lini wzorow
            int lpom = 0; //licznik pomocniczy
            int lwzor = 0; //licznik pomocniczy
            int count = 0; //licznik


            for (int i = 0; i < new_text.Length; i++) // for znajdujacy wzory 
            {
                try
                {
                    if (new_text[i] == @"\begin{math}")
                    {
                        while (new_text[i + 1] != @"\end{math}")
                        {
                            mat[l] = new_text[i + 1];
                            pom[lpom] = new_text[i + 1];
                            l++;
                            i++;
                            count++;
                            lpom++;
                        }
                        i++;
                        if(count == 1)
                        {
                            wzory[lwzor] = mat[l - 1];
                            lwzor++;  
                        }
                        else if(count > 1)
                        {
                            join_char = String.Join("", pom);
                            wzory[lwzor] = join_char;
                            lwzor++;
                        }
                        count = 0;
                        lpom = 0;
                    }
                    else if (new_text[i] == @"\begin{displaymath}")
                    {
                        while (new_text[i + 1] != @"\end{displaymath}")
                        {
                            mat[l] = new_text[i + 1];
                            pom[lpom] = new_text[i + 1];
                            l++;
                            i++;
                            count++;
                            lpom++;
                        }
                        i++;
                        if (count == 1)
                        {
                            wzory[lwzor] = mat[l - 1];
                            lwzor++;
                        }
                        else if (count > 1)
                        {
                            join_char = String.Join("", pom);
                            wzory[lwzor] = join_char;
                            lwzor++;
                        }
                        count = 0;
                        lpom = 0;
                    }
                    else if (new_text[i] == @"\begin{equation}")
                    {
                        while (new_text[i + 1] != @"\end{equation}")
                        {
                            mat[l] = new_text[i + 1];
                            pom[lpom] = new_text[i + 1];
                            l++;
                            i++;
                            count++;
                            lpom++;
                        }
                        i++;
                        if (count == 1)
                        {
                            wzory[lwzor] = mat[l - 1];
                            lwzor++;
                        }
                        else if (count > 1)
                        {
                            join_char = String.Join("", pom);
                            wzory[lwzor] = join_char;
                            lwzor++;
                        }
                        count = 0;
                        lpom = 0;
                    }
                }
                catch { MessageBox.Show("Dokument jest niepoprawny!\nNie znaleziono zamkniecia wyrazenia matematycznego lub niepoprawnie go uzyto!\nReszta operacji w programie moze zawierać błędy!"); }
            }
           
            j = 0;

            try
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if ((text[i].ToString() == @"$".ToString()) && (text[i + 1].ToString() == @"$".ToString()))
                    {
                        i += 2;
                        while ((text[i].ToString() != @"$".ToString()) && (text[i + 1].ToString() != @"$".ToString()))
                        {
                            znaki[j] = text[i].ToString();
                            j++;
                            i++;
                        }
                        znaki[j] = text[i].ToString();
                        i++;
                        j = 0;
                        join_char = String.Join("", znaki);
                        Array.Clear(znaki, 0, znaki.Length);
                        mat[l] = join_char;
                        wzory[lwzor] = mat[l];
                        lwzor++;
                        l++;
                    }
                    else if ((text[i].ToString() == @"$".ToString()) && (text[i + 1].ToString() != @"$".ToString()) && (text[i - 1].ToString() != @"$".ToString()))
                    {
                        while (text[i + 1].ToString() != @"$".ToString())
                        {
                            znaki[j] = text[i + 1].ToString();
                            j++;
                            i++;
                        }
                        i++;
                        j = 0;
                        join_char = String.Join("", znaki);
                        Array.Clear(znaki, 0, znaki.Length);
                        mat[l] = join_char;
                        wzory[lwzor] = mat[l];
                        lwzor++;
                        l++;
                    }
                    else if ((Equals(text[i].ToString(), @"\".ToString())) && (Equals(text[i + 1].ToString(), @"(".ToString())))
                    {
                        i += 2;
                        while ((text[i].ToString() != @"\".ToString()) && (text[i + 1].ToString() != @")".ToString()))
                        {
                            znaki[j] = text[i].ToString();
                            j++;
                            i++;
                        }
                        i++;
                        j = 0;
                        join_char = String.Join("", znaki);
                        Array.Clear(znaki, 0, znaki.Length);
                        mat[l] = join_char;
                        wzory[lwzor] = mat[l];
                        lwzor++;
                        l++;
                    }
                    else if ((Equals(text[i].ToString(), @"\".ToString())) && (Equals(text[i + 1].ToString(), @"[".ToString())))
                    {
                        i += 2;
                        while ((text[i].ToString() != @"\".ToString()) && (text[i + 1].ToString() != @"]".ToString()))
                        {
                            znaki[j] = text[i].ToString();
                            j++;
                            i++;
                        }
                        i++;
                        j = 0;
                        join_char = String.Join("", znaki);
                        Array.Clear(znaki, 0, znaki.Length);
                        mat[l] = join_char;
                        wzory[lwzor] = mat[l];
                        lwzor++;
                        l++;
                    }
                }
            }
            catch { MessageBox.Show("Dokument jest niepoprawny!\nNie znaleziono zamkniecia wyrazenia matematycznego lub niepoprawnie go uzyto!\nReszta operacji w programie moze zawierać błędy!"); }

            Console.WriteLine("Wyrazenia matematyczne: "); //wyswietlenie tablicy gdzie kazdy element to linia wzoru
            for (int i = 0; i < l; i++)
            {
                Console.WriteLine(i + " el: " + mat[i]);
            }

            Console.WriteLine("\nWzory matematyczne: "); //wyswietlenie tablicy gdzie kazdy element to wzor (czasami slabe wyswietlanie ale generalnie zawsze to mozna obrobic)
            for (int i = 0; i < lwzor; i++)
            {
                Console.WriteLine(i + " el: " + wzory[i]);
            }
            return File_Latex;
        }

        private void Same_Or_Not() //Porownanie plikow
        {













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
