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
            for(int y = 0; y <= array2D.GetUpperBound(1); y++)
            {
                for (int x = 0; x <= array2D.GetUpperBound(0); x++)
                {
                    licznikSumy[y] *= array2D[x, y];
                }
            }
            for(int x = 0; x <= array2D.GetUpperBound(0); x++)
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
    }
}
