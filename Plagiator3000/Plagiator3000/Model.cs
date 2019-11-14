using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using SautinSoft.Document;

namespace Plagiator3000
{
    class Model
    {
        string path = "d:\\";
        string path_dir = "d:\\";
        double procent = 0.0;
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

            string[] text_split = text.Split(new char[] {});
           
            string[] new_text = new string[text_split.Length];

            int j = 0;
            for (int i = 0; i < text_split.Length; i++)
            {
                if(text_split[i] != "")
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
                if(new_text[i] == @"\begin{math}")
                {
                    mat[l] = new_text[i + 1];
                    l++;
                }
                else if(new_text[i] == @"\begin{displaymath}")
                {
                    mat[l] = new_text[i + 1];
                    l++;
                }
                else if(new_text[i] == @"\begin{equation}")
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
            double sprawdzenie_wzoru = 0.0;
            double plagiat = 0.0;

            for (int i = 0; i < dl_testow; i++)
            {
                string wzor = testy[i][0];//pobiera 1 wzór z listy
                string s_pliku = testy[i][1];//pobiera ścieżkę do danego pliku
                for (int o = 0; o < dl_bazy; o++)
                {
                    if (wzor == baza[o])// tu docelowo podstawiam funkcje algorytmu Euclidan_agorithm
                    {
                        Raport.Add("Wzór : " + wzor + " z pliku : " + s_pliku + " jest plagiatem wzoru z bazy : " + baza[o] + " o indeksie : " + o);
                        plagiat++;
                    }
                }
                sprawdzenie_wzoru++;
            }
            procent = (plagiat / sprawdzenie_wzoru) * 100.0;

            //Console.WriteLine("Raport Euclidan: ");//testowy print
            //foreach (string raport in Raport)
            //{
            //    Console.WriteLine(raport);
            //}

            return Raport;
        }

        public List<String> baza(List<String> sciezki) //Wyciąga wzory kopia od Matiego ale wyciągnąłem to co potrzebuje
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

        public List<String[]> testy(List<String> sciezki) //Wyciąga wzory kopia od Matiego ale wyciągnąłem to co potrzebuje i zwraca dodatkowo ścieżkędla każdego wzoru
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

            List<String> rep = preEuclidan(baza(Path), testy(sciezki(Path_dir)));//WYWOLUJĘ POTĘŻNEGO EUCLIMANA ----------------------------> seniora (bo pre)
            Report(Path_dir, rep, procent);
        }

        public Boolean Euclidan_algorithm(string wzor_bazowy, string wzor_testowy)//algorytm Euclidan sprawdza plagiat i daje boola
        {
            string dany_wzor="ax+b=c";//tu dopisuje wzór
            char[] p = new char[200];
            int p0 = 0;

            for (int i=0; i< dany_wzor.Length; i++)//leci po każdym polu wzoru
            {
                if(dany_wzor[i] == '0' || dany_wzor[i] == '1' || dany_wzor[i] == '2' || dany_wzor[i] == '3' || dany_wzor[i] == '4' || dany_wzor[i] == '5' || dany_wzor[i] == '6' || dany_wzor[i] == '7' || dany_wzor[i] == '8' || dany_wzor[i] == '9')
                {
                    Console.WriteLine(dany_wzor[i]);
                    p[p0] = dany_wzor[i];
                    p0++;
                }
                else
                {
                    if(dany_wzor[i] == '+' || dany_wzor[i] == '-' || dany_wzor[i] == '*' || dany_wzor[i] == '/' || dany_wzor[i] == '=')
                    {
                        Console.WriteLine(dany_wzor[i]);
                        p[p0] = dany_wzor[i];
                        p0++;
                    }
                    else
                    {
                        Console.WriteLine("#");
                        p[p0] = '*';
                        p0++;
                        p[p0] = '1';
                        p0++;
                    }
                }
            }
            Console.WriteLine(p);











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

        public void Report(string sciezka, List<String> raport, double procent)// raport wstępnie 
        {
            //(https://www.sautinsoft.com/products/document/examples/create-html-document-net-csharp-vb.php)
            DocumentCore dc = new DocumentCore();
            dc.Content.End.Insert("Raport \n \n", new CharacterFormat() { FontName = "Verdana", Size = 65.5f, FontColor = Color.Black });
            dc.Content.End.Insert("Raport dotyczący folderu : " + sciezka + " \n", new CharacterFormat() { FontName = "Verdana", Size = 12f, FontColor = Color.Black });
            dc.Content.End.Insert("Wykryto " + String.Format("{0:F2}", procent) + " % splagiatowanych wzorów \n \n \n", new CharacterFormat() { FontName = "Verdana", Size = 12f, FontColor = Color.Black });
            dc.Content.End.Insert("Szczegółowe informacje :  \n", new CharacterFormat() { FontName = "Verdana", Size = 10, FontColor = Color.Black });
            foreach (string rap in raport)
            {
                dc.Content.End.Insert(rap + " \n", new CharacterFormat() { FontName = "Verdana", Size = 9, FontColor = Color.Black });
            }

            dc.Save(@"C:\Users\Arkad\Desktop\TEST_ORIG\Raport.html");

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(@"C:\Users\Arkad\Desktop\TEST_ORIG\Raport.html") { UseShellExecute = true });
            Console.WriteLine("Raport");
        }

    }
}
