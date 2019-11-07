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
            Load_Plagiat_Files(); //SPR ---------------------------------------------------------
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
    }
}
