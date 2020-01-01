using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Plagiator3000
{
    class MATL
    {
        public static string[] Orig_Latex_Operation_Mat(string path)
        {
            string File_Latex = path;
            string text_load = File.ReadAllText(File_Latex);

            string[] text_opt = new string[text_load.Length + 20]; //do poprawy wczytywan wczytywan

            text_load = File.ReadAllText(File_Latex).Replace(" ", "");     

            text_opt = Optymalizacja.Optymalizacje(text_load);           

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
            string[] pom = new string[new_text.Length]; //tablica pomocnicza do zapisu calego wzoru
            string[] znaki = new string[text.Length]; //tablica pomocnicza do zapisu znakow (chary przeciazone na stringi metoda toString)
            string join_char = ""; //pomocniczy string do laczenia tablicy charow w stringi

            int l = 0; // licznik lini wzorow
            int lpom = 0; //licznik pomocniczy

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
                            lpom++;
                        }
                        i++;
                    }
                    else if (new_text[i] == @"\begin{displaymath}")
                    {
                        while (new_text[i + 1] != @"\end{displaymath}")
                        {
                            mat[l] = new_text[i + 1];
                            pom[lpom] = new_text[i + 1];
                            l++;
                            i++;
                            lpom++;
                        }
                        i++;
                    }
                    else if (new_text[i] == @"\begin{equation}")
                    {
                        while (new_text[i + 1] != @"\end{equation}")
                        {
                            mat[l] = new_text[i + 1];
                            pom[lpom] = new_text[i + 1];
                            l++;
                            i++;
                            lpom++;
                        }
                        i++;
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
                        l++;
                    }
                }
            }
            catch { MessageBox.Show("Dokument jest niepoprawny!\nNie znaleziono zamkniecia wyrazenia matematycznego lub niepoprawnie go uzyto!\nReszta operacji w programie moze zawierać błędy!"); }

            Console.WriteLine("Wyrazenia matematyczne: "); //wyswietlenie tablicy gdzie kazdy element to linia wzoru
            string[] mat_dobre = new string[l];
            for (int i = 0; i < l; i++)
            {
                Console.WriteLine(i + " el: " + mat[i]);
                mat_dobre[i] = mat[i];
            }
            return mat_dobre;
        }
    }
}
