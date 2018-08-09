using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrading
{
    class Program
    {
        static void Main(string[] args)
        {

            //int[] stockPrices = { 10, 7, 5, 8, 11, 9 };
            //Console.WriteLine($"{GetMaxProfit(stockPrices)}");

            //int[] stockPrices1 = { 11, 10, 9, 8, 7, 6, 5, 4 };
            //Console.WriteLine($"{GetMaxProfit(stockPrices1)}");


            //int[] stockPrices2 = { 4, 5, 6, 7, 8 };
            //Console.WriteLine($"{GetMaxProfit(stockPrices2)}");

            int[] stockPrices3 = { 5, 6, 2, 4, 3, 6, 5, 7, 5, 3, 4 };
            Console.WriteLine($"{GetMaxProfit(stockPrices3)}");

            int[] stockPrices4 = { 7, 2, 8, 9 };
            Console.WriteLine($"{GetMaxProfit(stockPrices4)}");
        }

        static int GetMaxProfit(int[] stockPrices)
        {
            if (stockPrices.Length < 2)
                return 0;

            int minIndex = 0;
            int maxIndex = 1;
            int currentMax = stockPrices[maxIndex] - stockPrices[minIndex];

            int j = 1;
            int i = 2;
            for (; i < stockPrices.Length - 1;)
            {
                int localMax = stockPrices[i] - stockPrices[minIndex];
                if (localMax > currentMax)
                {
                    maxIndex = i;
                    currentMax = localMax;
                }
                else
                {
                    i++;
                }

                if (maxIndex > j)
                {
                    int localMin = stockPrices[maxIndex] - stockPrices[j];
                    if (localMin > currentMax)
                    {
                        minIndex = j;
                        currentMax = localMin;
                    }
                    else if (j < i - 1)
                    {
                        j++;
                    }
                }
            }

            return currentMax;
        }
    }
}
