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
            string text = File.ReadAllText(File_Latex).Replace(" ", "");

            string[] text_split = text.Split(new char[] {' '});

            //.Split(new Char[] { ' ' })

            //foreach (string tx in text)
            //{
            //    Console.WriteLine(tx);
            //}

            string newtext = string.Concat(text_split);
            Console.WriteLine(newtext);

            for (int i = 0; i < text_split.Length; i++)
            {
                Console.WriteLine(i + " el: " + text_split[i]); 
            }
            
            return File_Latex;
        }

        private void Same_Or_Not() //Porownanie plikow
        {

        }
    }
}
