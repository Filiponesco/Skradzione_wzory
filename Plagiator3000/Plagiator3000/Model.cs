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

        public List<String[]> testy(List<String> sciezki) 
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

        public void Euclidan()
        {
            string Path_dir = path_dir + "\\";//zmieniam ścieżki trochę bo byłem "przygotowany" na co inne 
            List<string> Path = new List<string> { };
            Path.Add(path);

            List<String> rep = preEuclidan(baza(Path), testy(sciezki(Path_dir)));//WYWOLUJĘ POTĘŻNEGO EUCLIMANA ----------------------------> seniora (bo pre)
            //Report(Path_dir, rep, procent);
        }

        public Boolean Euclidan_algorithm(string wzor_bazowy, List<string[]> wzor_testowy)
        {
            return false;
        }

        //public void Report(string sciezka, List<String> raport, double procent)// raport wstępnie 
        //{
        //    //(https://www.sautinsoft.com/products/document/examples/create-html-document-net-csharp-vb.php)
        //    DocumentCore dc = new DocumentCore();
        //    dc.Content.End.Insert("Raport \n \n", new CharacterFormat() { FontName = "Verdana", Size = 65.5f, FontColor = Color.Black });
        //    dc.Content.End.Insert("Raport dotyczący folderu : " + sciezka + " \n", new CharacterFormat() { FontName = "Verdana", Size = 12f, FontColor = Color.Black });
        //    dc.Content.End.Insert("Wykryto " + String.Format("{0:F2}", procent) + " % splagiatowanych wzorów \n \n \n", new CharacterFormat() { FontName = "Verdana", Size = 12f, FontColor = Color.Black });
        //    dc.Content.End.Insert("Szczegółowe informacje :  \n", new CharacterFormat() { FontName = "Verdana", Size = 10, FontColor = Color.Black });
        //    foreach (string rap in raport)
        //    {
        //        dc.Content.End.Insert(rap + " \n", new CharacterFormat() { FontName = "Verdana", Size = 9, FontColor = Color.Black });
        //    }

        //    dc.Save(@"C:\Users\Arkad\Desktop\TEST_ORIG\Raport.html");

        //    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(@"C:\Users\Arkad\Desktop\TEST_ORIG\Raport.html") { UseShellExecute = true });
        //    Console.WriteLine("Raport");
        //}

    }
}
