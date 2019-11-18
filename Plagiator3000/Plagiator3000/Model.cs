using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
            // Load_Plagiat_Files(); //SPR ---------------------------------------------------------
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

            string[] mat = new string[new_text.Length];

            int l = 0;

            for (int i = 0; i < new_text.Length; i++)
            {
                try
                {
                    if (new_text[i] == @"\begin{math}")
                    {
                        while (new_text[i + 1] != @"\end{math}")
                        {
                            mat[l] = new_text[i + 1];
                            l++;
                            i++;
                        }
                        i++;
                    }
                    else if (new_text[i] == @"\begin{displaymath}")
                    {
                        while (new_text[i + 1] != @"\end{displaymath}")
                        {
                            mat[l] = new_text[i + 1];
                            l++;
                            i++;
                        }
                        i++;
                    }
                    else if (new_text[i] == @"\begin{equation}")
                    {
                        while (new_text[i + 1] != @"\end{equation}")
                        {
                            mat[l] = new_text[i + 1];
                            l++;
                            i++;
                        }
                        i++;
                    }
                }
                catch { MessageBox.Show("Dokument jest niepoprawny!\nNie znaleziono zamkniecia wyrazenia matematycznego lub niepoprawnie go uzyto!\nReszta operacji w programie moze zawierać błędy!"); }
            }

            string[] znaki = new string[text.Length];
            string traj = "";
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
                        traj = String.Join("", znaki);
                        Array.Clear(znaki, 0, znaki.Length);
                        mat[l] = traj;
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
                        traj = String.Join("", znaki);
                        Array.Clear(znaki, 0, znaki.Length);
                        mat[l] = traj;
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
                        traj = String.Join("", znaki);
                        Array.Clear(znaki, 0, znaki.Length);
                        mat[l] = traj;
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
                        traj = String.Join("", znaki);
                        Array.Clear(znaki, 0, znaki.Length);
                        mat[l] = traj;
                        l++;
                    }
                }
            }
            catch { MessageBox.Show("Dokument jest niepoprawny!\nNie znaleziono zamkniecia wyrazenia matematycznego lub niepoprawnie go uzyto!\nReszta operacji w programie moze zawierać błędy!"); }

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
    }
}
