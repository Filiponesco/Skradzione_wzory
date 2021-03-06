﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Plagiator3000
{
    class Algorytm
    {
        const double maxOfEuclidean = 10;
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
            ReturnExceptionIfNullOrEmpty(wzorOrig, wzorCopy);

            wzorOrig = ChangeMathSymbolToOneLetter(wzorOrig);
            wzorCopy = ChangeMathSymbolToOneLetter(wzorCopy);

            ReturnExceptionIfNullOrEmpty(wzorOrig, wzorCopy);

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

            //jeśli mianownik jest równe 0 oznacza, że żadna litera się nie powtórzyła w plagiacie
            if (mianownik == 0)
            {
                return 1.0;
            }

            double cosSimilarity = licznik / mianownik;
            cosDistance = 1 - cosSimilarity;
            return cosDistance;
        }
        public static double EuclideanDistance(string wzorOrig, string wzorCopy)
        {
            ReturnExceptionIfNullOrEmpty(wzorOrig, wzorCopy);

            wzorOrig = ChangeMathSymbolToOneLetter(wzorOrig);
            wzorCopy = ChangeMathSymbolToOneLetter(wzorCopy);

            ReturnExceptionIfNullOrEmpty(wzorOrig, wzorCopy);

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

        public static double Trzeci(string[] wzorOrig, string[] wzorCopy)
        {
            int sumoforig = 0;
            int sumofcopy = 0;
            double third;
            for (int k = 0; k < wzorOrig.Length; k++)
                sumoforig += wzorOrig[k].ToString().Length;
            for (int k = 0; k < wzorCopy.Length; k++)
                sumofcopy += wzorCopy[k].ToString().Length;
            third = sumoforig - sumofcopy;
            Console.WriteLine(sumofcopy);
            Console.WriteLine(sumoforig);
            return third;
        }

        private static void ReturnExceptionIfNullOrEmpty(string o, string c)
        {
            if (String.IsNullOrEmpty(o))
                throw new Exception("wzor oryginalny jest pusty!");

            if (String.IsNullOrEmpty(c))
                throw new Exception("wzor plagiat jest pusty!");
        }
        public static double ToPercent(string algorytm, double lb)
        {
            double result = 0;
            if(algorytm == "CosineDistance")
            {
                result = (double) (1 - lb);
            }
            else
            {
                double pom = (double) (maxOfEuclidean - lb);
                result = Scale(pom, 0.0, maxOfEuclidean, 0.0, 1.0);
            }
            return result * 100; //percent
        }
        private static double Scale(double value, double min, double max, double toMin, double toMax)
        {
            //y=mx+c
            double result = (value - min) / (max - min) * (toMax - toMin) + toMin;
            return result;
        }
        private static string ChangeMathSymbolToOneLetter(string pattern)
        {
            string result = pattern;
            string[] symbols = new string[15];
            symbols[0] = @"\\frac";
            symbols[1] = @"\\sqrt";
            symbols[2] = @"\\lim";
            symbols[3] = @"{";
            symbols[4] = @"}";
            symbols[5] = @"\\left";
            symbols[6] = @"\\right";
            symbols[7] = @"\\int";
            symbols[8] = @"\\limits";
            symbols[9] = @"\\in";
            symbols[10] = @"\\prod";
            symbols[11] = @"\cdot";
            symbols[12] = @"\\";
            symbols[13] = @"!";
            symbols[14] = @"_";

            //string symbol = @"\\frac";

            result = Regex.Replace(result, symbols[0], "");
            result = Regex.Replace(result, symbols[1], "");
            result = Regex.Replace(result, symbols[2], "");
            result = Regex.Replace(result, symbols[3], String.Empty);
            result = Regex.Replace(result, symbols[4], String.Empty);
            result = Regex.Replace(result, symbols[5], String.Empty);
            result = Regex.Replace(result, symbols[6], String.Empty);
            result = Regex.Replace(result, symbols[7], "");
            result = Regex.Replace(result, symbols[8], "");
            result = Regex.Replace(result, symbols[9], "");
            result = Regex.Replace(result, symbols[10], "");
            result = Regex.Replace(result, symbols[11], "");
            result = Regex.Replace(result, symbols[12], String.Empty);
            result = Regex.Replace(result, symbols[13], String.Empty);
            result = Regex.Replace(result, symbols[14], String.Empty);
            Console.WriteLine("REGEX: " + result);
            return result;

        }
    }
}
