using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Plagiator3000
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Dictionary<String, int> countsOriginal = CountLines(@"gawel_testy/oryg.txt");
            Dictionary<String, int> countsCopy1 = CountLines(@"gawel_testy/copy1.txt");
            Dictionary<String, int> countsCopy2 = CountLines(@"gawel_testy/copy2.txt");
            List<Dictionary<String, int>> copies = new List<Dictionary<String, int>>() { countsCopy1, countsCopy2 };
            int[,] array2D = Algorytm.ToArray(countsOriginal, copies);
            //jedynka juz zajeta przez oryginal
            for (int y = 0; y <= array2D.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= array2D.GetUpperBound(0); x++)
                {
                    textBox1.Text += (array2D[x, y] + " ");
                }
                textBox1.Text += "\r\n";
            }
            textBox1.Text += Algorytm.CosineDistance(array2D)[1];
            textBox1.Text += "   ";
            textBox1.Text += Algorytm.EuclideanDistance(array2D)[1];
        }
        private Dictionary<String, int> CountLines(String path)
        {
            String[] lines;
            Dictionary<String, int> counts = new Dictionary<string, int>();
            lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                if (counts.ContainsKey(lines[i]))
                {
                    counts[lines[i]]++;
                }
                else
                {
                    counts.Add(lines[i], 1);
                }
            }
            return counts;
        }
    }
}
