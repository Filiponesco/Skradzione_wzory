using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiator3000
{
    class Algorytm
    {
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
                EuclDist[x - 1] = Math.Sqrt(sumaRoznic);
            }
            return EuclDist;
        }
        private static char[] PatternToLetters(string pattern)
        {
            char[] letters = new char[pattern.Length];
            for (int i = 0; i < pattern.Length; i++)
            {
                letters[i] = pattern[i];
            }
            return letters;
        }
        private static Dictionary<char, int> CountFrequentlyOfLetters(char[] letters)
        {
            var freqLet = new Dictionary<char, int>();
            Array.Sort(letters);
            var actualChar = letters[0];
            var sum = 0;
            foreach (var c in letters)
            {
                if (c == actualChar)
                {
                    sum++;
                }
                else
                {
                    freqLet.Add(actualChar, sum);
                    actualChar = c;
                    sum = 1;
                }
            }
            freqLet.Add(actualChar, sum);
            return freqLet;
        }
        private static Dictionary<char, int> SortDictChar(Dictionary<char, int> dict)
        {
            var chars = dict.Keys.ToList();
            chars.Sort();
            var sortedDictChar = new Dictionary<char, int>();
            foreach (var c in chars)
            {
                sortedDictChar.Add(c, dict[c]);
            }
            return sortedDictChar;
        }
        private static Dictionary<char, int> DeleteOtherCharInCopy(
            Dictionary<char, int> frqLtrsOrig, Dictionary<char, int> frqLtrsCopy)
        {
            var charToDelete = new List<char>();
            foreach (var c in frqLtrsCopy)
            {
                //jesli kopia zawiera litere ktora nie nalezy do oryginalu to usun ja
                if (!frqLtrsOrig.ContainsKey(c.Key))
                {
                    charToDelete.Add(c.Key);
                }
            }
            foreach (var c in charToDelete)
            {
                frqLtrsCopy.Remove(c);
            }
            //znaki ktore nie wystepuja w kopii musza byc dodane z wartoscia 0 jako, że nie wystąpiły
            foreach (var c in frqLtrsOrig)
            {
                if (!frqLtrsCopy.ContainsKey(c.Key))
                {
                    frqLtrsCopy.Add(c.Key, 0);
                }
            }
            //sortowanie kopii alfabetycznie
            SortDictChar(frqLtrsOrig);
            return frqLtrsCopy;
        }
        public static double CosineDistance(string wzorOrig, string wzorCopy)
        {
            double licznik = 0;
            double mianownik;
            double mianOrig = 0; //pierwiastek(ai ^2)
            double mianCopy = 0; //pierwiastek(bi ^2)
            double cosDistance;

            char[] lettersOrig = PatternToLetters(wzorOrig);
            char[] lettersCopy = PatternToLetters(wzorCopy);

            Dictionary<char, int> frqLtrsOrig = CountFrequentlyOfLetters(lettersOrig);
            Dictionary<char, int> frqLtrsCopy = CountFrequentlyOfLetters(lettersCopy);

            frqLtrsCopy = DeleteOtherCharInCopy(frqLtrsOrig, frqLtrsCopy);

            foreach (var c in frqLtrsOrig)
            {
                mianOrig += Math.Pow(c.Value, 2);
            }
            mianOrig = Math.Sqrt(mianOrig);

            foreach (var c in frqLtrsCopy)
            {
                mianCopy += Math.Pow(c.Value, 2);
            }
            mianCopy = Math.Sqrt(mianCopy);

            mianownik = mianOrig * mianCopy;

            for (int i = 0; i < frqLtrsOrig.Count; i++)
            {
                licznik += frqLtrsOrig.ElementAt(i).Value * frqLtrsCopy.ElementAt(i).Value;
            }

            double cosSimilarity = licznik / mianownik;
            cosDistance = 1 - cosSimilarity;
            return cosDistance;
        }
    }
}
