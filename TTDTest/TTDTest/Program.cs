using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTDTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //RLE encoding
            //bitmap image
            //0s and 1s
            //000011 -> 0:4 1:2

            //2d array of ones and zeros
            // return compressed version

            var bitmap = new int[,]
            {
                { 1, 0 , 1, 1},
                { 1, 1 , 1, 1},
                { 0, 1 , 1, 0},
            };

            // output
            // 0:2, 1:2
            // 0:1, 1:3
            // 0:1, 1:2, 0:1

            // output
            // 0:2, 1:6, 0:1, 1:2, 0:1
            // 

            var compressed = RleCompression(bitmap);

            if (compressed[0] == 0)
                compressed.RemoveAt(0);

            foreach(int val in compressed)
            {
                Console.WriteLine(val);
            }
        }
        
        static List<int> RleCompression(int[,] bitmap)
        {
            int prev = 0;
            List<int> res = new List<int>() { 0 };


            for (int i = 0; i < bitmap.GetLength(0); i++)
            {
                for (int j = 0; j < bitmap.GetLength(1); j++)
                {
                    if(bitmap[i, j] == prev)
                    {
                        res[res.Count - 1]++;
                    }
                    else
                    {
                        res.Add(1);
                        prev = bitmap[i, j];
                    }
                }
            }

            return res;
        }

    }
}
