using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
<<<<<<< Updated upstream
=======
using SautinSoft.Document;
using System.Diagnostics;
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

namespace Plagiator3000
{
    class Model
    {
        string path = "d:\\";
        string path_dir = "d:\\";
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

        public string Load_Plagiat_Direc() //Wczytanie folderu z plikami
        {
            using (FolderBrowserDialog win = new FolderBrowserDialog())
            {
                if (win.ShowDialog() == DialogResult.OK)
                {
                    path_dir = win.SelectedPath;
                }
            }
            //Load_Plagiat_Files(); //SPR ---------------------------------------------------------
            return path_dir;
        }

        public void Load_Plagiat_Files() //Wczytuje pliki z folderu
        {
            var files = Directory.EnumerateFiles(path_dir, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".tex"));

            //string[] fileEntries = Directory.GetFiles(path_dir);
            foreach (string fileName in files)
            {
                Console.WriteLine(fileName);
                string File_Latex = fileName;
                string text = File.ReadAllText(File_Latex);
                Console.WriteLine(text);
            }
            // ProcessFile(fileName);
        }

        public string Orig_Latex_Operation() //Operacje na oryginalnym latexie
        {
            string File_Latex = path;
            string text = File.ReadAllText(File_Latex);

            Console.WriteLine("Tekst: " + text);

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

            //Console.WriteLine("\nNowy text:");

            //for (int i = 0; i < j; i++)
            //{
            //    Console.WriteLine(i + " el: " + new_text[i]);
            //}

            string[] mat = new string[new_text.Length];

            int l = 0;

            for (int i = 0; i < new_text.Length; i++)
            {
                if (new_text[i] == @"\begin{math}")
                {
                    mat[l] = new_text[i + 1];
                    l++;
                }
                else if (new_text[i] == @"\begin{displaymath}")
                {
                    mat[l] = new_text[i + 1];
                    l++;
                }
                else if (new_text[i] == @"\begin{equation}")
                {
                    mat[l] = new_text[i + 1];
                    l++;
                }
            }


            Console.WriteLine("Wyrazenia matematyczne: ");
            for (int i = 0; i < l; i++)
            {
                Console.WriteLine(i + " el: " + mat[i]);
            }


            return File_Latex;
        }

        private void Same_Or_Not() //Porownanie plikow
        {

        }
        public List<String> preEuclidan(List<String> baza, List<String[]> testy)//zbiera wszystko i daje do Euclidan
        {
            List<String> Raport = new List<string> { };
            int dl_bazy = baza.Count;
            int dl_testow = testy.Count;

            for (int i = 0; i < dl_testow; i++)
            {
                string wzor = testy[i][0];//pobiera 1 wzór z listy
                string s_pliku = testy[i][1];//pobiera ścieżkę do danego pliku
                for (int o = 0; o < dl_bazy; o++)
                {
                    if (wzor == baza[o])// tu docelowo podstawiam funkcje algorytmu Euclidan_agorithm
                    {
                        Raport.Add("Wzór : " + wzor + " z pliku : " + s_pliku + " jest plagiatem wzoru z bazy : " + baza[o] + " o indeksie : " + o);
                    }
                }
            }

            Console.WriteLine("Raport Euclidan: ");//testowy print
            foreach (string raport in Raport)
            {
                Console.WriteLine(raport);
            }

            return Raport;
        }

<<<<<<< Updated upstream
<<<<<<< Updated upstream
        public List<String> baza(List<String> sciezki) //Wyciąga wzory kopia od Matiego ale wyciągnąłem to co potrzebuje
=======
        public List<String> baza(List<String> sciezki)
>>>>>>> Stashed changes
=======
        public List<String> baza(List<String> sciezki)
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
<<<<<<< Updated upstream
        public List<String[]> testy(List<String> sciezki) //Wyciąga wzory kopia od Matiego ale wyciągnąłem to co potrzebuje i zwraca dodatkowo ścieżkędla każdego wzoru
=======
        public List<String[]> testy(List<String> sciezki)
>>>>>>> Stashed changes
=======
        public List<String[]> testy(List<String> sciezki)
>>>>>>> Stashed changes
        {
            List<String[]> wzory_testy = new List<string[]> { };//główna lista z bazą wzorów

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
                        wzory_testy.Add(new string[] { new_text[i + 1], sciezka });
                        l++;
                    }
                    else if (new_text[i] == @"\begin{displaymath}")
                    {
                        mat[l] = new_text[i + 1];
                        wzory_testy.Add(new string[] { new_text[i + 1], sciezka });
                        l++;
                    }
                    else if (new_text[i] == @"\begin{equation}")
                    {
                        mat[l] = new_text[i + 1];
                        wzory_testy.Add(new string[] { new_text[i + 1], sciezka });
                        l++;
                    }
                }
            }

            return wzory_testy;//zwraca bazę
        }

        public List<String> sciezki(string Path)//z ścieżki pobiera wszystkie pliki tex i daje do listy 
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

        public void Euclidan()// zbiera wszystko żeby bylo ładnie w presenterze
        {
            string Path_dir = path_dir + "\\";//zmieniam ścieżki trochę bo byłem "przygotowany" na co inne 
            List<string> Path = new List<string> { };
            Path.Add(path);

            preEuclidan(baza(Path), testy(sciezki(Path_dir)));//WYWOLUJĘ POTĘŻNEGO EUCLIMANA ----------------------------> seniora (bo pre)
        }

        private Boolean Euclidan_algorithm(string wzor_bazowy, string wzor_testowy)//algorytm Euclidan sprawdza plagiat i daje boola
        {
            int GCDRecursive(int a, int b)
            {
                //Base cases
                if (a == 0)
                    return b;

                if (b == 0)
                    return a;

                if (a > b)
                    return GCDRecursive(a % b, b);
                else
                    return GCDRecursive(a, b % a);
            }

            int GCD(int a, int b)
            {
                while (a != 0 && b != 0)
                {
                    if (a > b)
                        a %= b;
                    else
                        b %= a;
                }

                if (a == 0)
                    return b;
                else
                    return a;
            }

            return false;
        }

        public void raport(int[] tablica_wynikow)
        {
            string PATH = path_dir + "\\raport.html";
            List<string> wzory_oryg = baza(new List<string> { path });
            List<string> wzory_test = baza(sciezki(path_dir));
            List<string> sciezki_test= sciezki(path_dir);
            int dl = tablica_wynikow.Length;
            //string reszta = "";
            string reszta2 = "";
            int inkr = 0;

            //for (int i = 0; i < wzory_test.Count; i++) // reszta to porównanie pojednczych wzorów
            //{
            //    for (int j = 0; j < wzory_oryg.Count; j++)
            //    {
            //        reszta += "<p>wzór : " + wzory_test[i] + " jest splagiatowany w: " + tablica_wynikow[inkr] + " ptocentach z bazą - wzór: " + wzory_oryg[j] + "</p>\n";
            //        inkr++;
            //    }
            //}

            for (int i = 0; i < sciezki_test.Count; i++) // reszta2 to porównanie plików
            {
                    reszta2 += "<p>plik : " + sciezki_test[i] + " jest splagiatowany w: " + tablica_wynikow[i] + " ptocentach z plikiem bazowym: " + path + "</p>\n";

            }
            Console.WriteLine(reszta2);

            var raport = "<!DOCTYPE html>\n<html>\n<body>\n<h1>Raport</h1>\n" +reszta2+ "</body>\n</html>";
            System.IO.File.WriteAllText(PATH, raport);
            Process.Start("chrome.exe", PATH);
        }
<<<<<<< Updated upstream
=======

        public void raport(int[] tablica_wynikow)
        {
            string PATH = path_dir + "\\raport.html";
            List<string> wzory_oryg = baza(new List<string> { path });
            List<string> wzory_test = baza(sciezki(path_dir));
            List<string> sciezki_test= sciezki(path_dir);
            int dl = tablica_wynikow.Length;
            //string reszta = "";
            string reszta2 = "";
            int inkr = 0;

            //for (int i = 0; i < wzory_test.Count; i++) // reszta to porównanie pojednczych wzorów
            //{
            //    for (int j = 0; j < wzory_oryg.Count; j++)
            //    {
            //        reszta += "<p>wzór : " + wzory_test[i] + " jest splagiatowany w: " + tablica_wynikow[inkr] + " ptocentach z bazą - wzór: " + wzory_oryg[j] + "</p>\n";
            //        inkr++;
            //    }
            //}

            for (int i = 0; i < sciezki_test.Count; i++) // reszta2 to porównanie plików
            {
                    reszta2 += "<p>plik : " + sciezki_test[i] + " jest splagiatowany w: " + tablica_wynikow[i] + " ptocentach z plikiem bazowym: " + path + "</p>\n";

            }
            Console.WriteLine(reszta2);

            var raport = "<!DOCTYPE html>\n<html>\n<body>\n<h1>Raport</h1>\n" +reszta2+ "</body>\n</html>";
            System.IO.File.WriteAllText(PATH, raport);
            Process.Start("chrome.exe", PATH);
        }
>>>>>>> Stashed changes
    }
}
