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
            int[,] array2D = ToArray(countsOriginal, copies);
            //jedynka juz zajeta przez oryginal
            for (int y = 0; y <= array2D.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= array2D.GetUpperBound(0); x++)
                {
                    textBox1.Text += (array2D[x, y] + " ");
                }
                textBox1.Text += "\r\n";
            }
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
        private int[,] ToArray(Dictionary<String, int> orig, List<Dictionary<String, int>> copyFiles)
        {
            List<Dictionary<String, int>> sortedCopies = new List<Dictionary<string, int>>();
            int[,] array2D = new int[1 + copyFiles.Count, orig.Count]; //lb dokumentow x lb slow w oryginal
            var sortedOrig = SortDict(orig);
            for (int y = 0; y <= array2D.GetUpperBound(1); y++)
            {
                array2D[0, y] = sortedOrig.ElementAt(y).Value;
            }
            for (int i = 0; i < copyFiles.Count; i++)
            {
                var KeysToDelete = new List<string>();
                foreach (var dictCopy in copyFiles[i])
                {
                    //jesli kopia zawiera klucz ktory nie nalezy do oryginalu to usun go
                    if (!orig.ContainsKey(dictCopy.Key))
                    {
                        KeysToDelete.Add(dictCopy.Key);
                    }
                }
                foreach (var k in KeysToDelete)
                {
                    copyFiles[i].Remove(k);
                }
                //klucze ktore nie wystepuja w kopiach musza byc dodane z wartoscia 0
                foreach (var dictOrig in orig)
                {
                    if (!copyFiles[i].ContainsKey(dictOrig.Key))
                    {
                        copyFiles[i].Add(dictOrig.Key, 0);
                    }
                }
                //sortowanie kopii alfabetycznie
                var sortedCopy = SortDict(copyFiles[i]);
                sortedCopies.Add(sortedCopy);
            }
            //teraz lista sortedCopies posaida tylko pliki, ktore sie powtarzaj w oryginale, posortowane tak samo jak oryginal
            for (int x = 1; x <= array2D.GetUpperBound(0); x++) //jedynka juz zajeta przez oryginal
            {
                for (int y = 0; y <= array2D.GetUpperBound(1); y++)
                {
                    array2D[x, y] = sortedCopies[x - 1].ElementAt(y).Value;
                }
            }
            return array2D;
        }
        private Dictionary<String, int> SortDict(Dictionary<String, int> dict)
        {
            var keys = dict.Keys.ToList();
            keys.Sort();
            var sortedDict = new Dictionary<String, int>();
            foreach (var key in keys)
            {
                sortedDict.Add(key, dict[key]);
            }
            return sortedDict;
        }
    }
}
