using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plagiator3000
{
    static class Algorytm
    {
        public static double CosineDistance(int[,] array2D)
        {
            double licznik = 0;
            double mianownik = 1;
            double[] licznikSumy = new double[array2D.GetUpperBound(1) + 1];
            double[] mianSumy = new double[array2D.GetUpperBound(0) + 1];
            for (int i = 0; i < licznikSumy.Length; i++)
            {
                licznikSumy[i] = 1;
            }
            for (int i = 0; i < mianSumy.Length; i++)
            {
                mianSumy[i] = 0;
            }
            for (int y = 0; y <= array2D.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= array2D.GetUpperBound(0); x++)
                {
                    licznikSumy[y] *= array2D[x, y];
                }
            }
            for (int x = 0; x <= array2D.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= array2D.GetUpperBound(1); y++)
                {
                    mianSumy[x] += Math.Pow(array2D[x, y], 2);
                }
            }
            for (int i = 0; i < licznikSumy.Length; i++)
            {
                licznik += licznikSumy[i];
            }
            for (int i = 0; i < mianSumy.Length; i++)
            {
                mianownik *= Math.Sqrt(mianSumy[i]);
            }
            double cosSimilarity = licznik / mianownik;
            double cosDistance = 1 - cosSimilarity;
            return cosDistance;
        }
        public static double EuclideanDistance(int[,] array2D)
        {
            //roznica czestosci wystepowan posczegolnych liń dla oryg i 1 kopii
            int[] roznica = new int[array2D.GetUpperBound(1) + 1];
            int sumaRoznic = 0;
            double result = 0.0;
            //for (int i = 0; i < mianSumy.Length; i++)
            //{
            //    mianSumy[i] = 0;
            //}
            //od 1 bo dokument 0 to oryginal do ktorego wszystko bedzie porownywane
            for (int x = 1; x <= array2D.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= array2D.GetUpperBound(1); y++)
                {
                    roznica[y] = array2D[0, y] - array2D[x, y];
                    roznica[y] *= roznica[y];
                    sumaRoznic += roznica[y];
                }
            }
            result = Math.Sqrt(sumaRoznic);
            return result;
        }
    }
}
