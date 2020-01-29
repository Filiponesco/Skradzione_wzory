using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiator3000
{
    class Algorytm
    {
        const double maxOfEuclidean = 44.65424;
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
        public static double EuclideanDistance(string wzorOrig, string wzorCopy)
        {
            char[] lettersOrig = PatternToLetters(wzorOrig);
            char[] lettersCopy = PatternToLetters(wzorCopy);

            Dictionary<char, int> frqLtrsOrig = CountFrequentlyOfLetters(lettersOrig);
            Dictionary<char, int> frqLtrsCopy = CountFrequentlyOfLetters(lettersCopy);

            frqLtrsCopy = DeleteOtherCharInCopy(frqLtrsOrig, frqLtrsCopy);

            double euclideanDistance;
            double roznica;
            double suma = 0;

            for (int i = 0; i < frqLtrsOrig.Count; i++)
            {
                roznica = frqLtrsOrig.ElementAt(i).Value - frqLtrsCopy.ElementAt(i).Value;
                roznica *= roznica;
                suma += roznica;
            }
            euclideanDistance = Math.Sqrt(suma);
            return euclideanDistance;
        }
        public static double ToPercent(string algorytm, double lb)
        {
            double result = 0;
            if(algorytm == "CosineDistance")
            {
                result = Scale(lb, 0, 1, 1, 0);
            }
            else
            {
                result = Scale(lb, 0, maxOfEuclidean, 1, 0);
            }
            return result * 100; //percent
        }
        public static double Scale(double lb, double min, double max, double minScale, double maxScale)
        {
            //y=mx+c
            double m = (maxScale - minScale) / (max - min);
            double c = minScale - min * m;
            double result = m * lb + c;
            return result;
        }
    }
}
