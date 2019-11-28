using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiator3000
{
    static class Algorytm
    {
        public static double[] CosineDistance(int[,] array2D)
        {
            double licznik;
            double mianownik;
            double mianOrig = 0; //pierwiastek(ai ^2)
            double mianCopy; //pierwiastek(bi ^2)
            // Length cosDistance jest rowna lb kopii
            double[] cosDistance = new double[array2D.GetUpperBound(0)];

            //petla ktora wykona sie jeden raz
            for (int y = 0; y <= array2D.GetUpperBound(1); y++)
            {
                mianOrig += Math.Pow(array2D[0, y], 2);
            }
            mianOrig = Math.Sqrt(mianOrig);
            for(int x = 1; x <= array2D.GetUpperBound(0); x++)
            {
                mianCopy = 0;
                licznik = 0;
                for (int y = 0; y <= array2D.GetUpperBound(1); y++)
                {
                    licznik += array2D[0, y] * array2D[x, y];
                }
                for (int y = 0; y <= array2D.GetUpperBound(1); y++)
                {
                    mianCopy += Math.Pow(array2D[x, y], 2);
                }
                mianCopy = Math.Sqrt(mianCopy);
                mianownik = mianOrig * mianCopy;
                double cosSimilarity = licznik / mianownik;
                cosDistance[x - 1] = 1 - cosSimilarity;
            }
            return cosDistance;
        }
        public static double[] EuclideanDistance(int[,] array2D)
        {
            double[] EuclDist = new double[array2D.GetUpperBound(0) + 1];
            //roznica czestosci wystepowan posczegolnych liń dla oryg i 1 kopii
            int[] roznica = new int[array2D.GetUpperBound(1) + 1];
            int sumaRoznic;
            double result;
            //od 1 bo dokument 0 to oryginal do ktorego wszystko bedzie porownywane
            for (int x = 1; x <= array2D.GetUpperBound(0); x++)
            {
                sumaRoznic = 0;
                for (int y = 0; y <= array2D.GetUpperBound(1); y++)
                {
                    roznica[y] = array2D[0, y] - array2D[x, y];
                    roznica[y] *= roznica[y];
                    sumaRoznic += roznica[y];
                }
                EuclDist[x-1] = Math.Sqrt(sumaRoznic);
            }
            return EuclDist;
        }
        public static int[,] ToArray(Dictionary<String, int> orig, List<Dictionary<String, int>> copyFiles)
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
        private static Dictionary<String, int> SortDict(Dictionary<String, int> dict)
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
