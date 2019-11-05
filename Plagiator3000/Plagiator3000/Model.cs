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
                    path = win.SelectedPath;
                }
            }
            return path;
        }

        public string Orig_Latex_Operation() //Operacje na oryginalnym latexie
        {
            string File_Latex = path;
            string text = File.ReadAllText(File_Latex);

            Console.WriteLine(text);

            return File_Latex;
        }

        private void Same_Or_Not() //Porownanie plikow
        {

        }
    }
}
