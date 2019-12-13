﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiator3000
{
    static class Algorytm
    {
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
