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
    }
}
