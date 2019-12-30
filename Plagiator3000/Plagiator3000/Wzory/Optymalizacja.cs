using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiator3000
{
    class Optymalizacja
    {
        public static string[] Optymalizacje(string text_load)
        {
            string[] text_opt = new string[text_load.Length + 20]; //do poprawy wczytywan wczytywan

            int k = 0, n = 0;
            for (int i = 0; i < text_load.Length; i++)
            {
                if ((text_load[i].ToString() == @"\".ToString()) && (text_load[i + 1].ToString() == @"e".ToString()) && (text_load[i + 5].ToString() == @"m".ToString()) && (text_load[i + 9].ToString() == @"}".ToString()))
                {
                    for (int m = 0; m < 11; m++)
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
                else if ((text_load[i].ToString() == @"\".ToString()) && (text_load[i + 1].ToString() == @"e".ToString()) && (text_load[i + 5].ToString() == @"d".ToString()) && (text_load[i + 9].ToString() == @"l".ToString()) && (text_load[i + 16].ToString() == @"}".ToString()))
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

            return text_opt;
        }
    }
}
