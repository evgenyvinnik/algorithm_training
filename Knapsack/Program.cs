using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knapsack
{
    class Program
    {
        class Item
        {
            public int weight;
            public int value;

            public Item(int value, int weight)
            {
                this.value = value;
                this.weight = weight;
            }
        }


        class SimpleItem
        {
            public float value;

            public SimpleItem(float value)
            {
                this.value = value;
            }
        }

        //int max()
        //{

        //}

        static void print_solution(int[,] solution)
        {
            for (int i = 0; i < solution.GetLength(0); i++)
            {
                for (int j = 0; j < solution.GetLength(1); j++)
                {
                    Console.Write($"{solution[i, j],5}");
                }
                Console.WriteLine("");
            }

            Console.WriteLine("");
            Console.WriteLine($"{solution[solution.GetLength(0) - 1, solution.GetLength(1) - 1],5}");
        }

        static void KnapSack(Item[] items, int weight_limit)
        {
            int[,] solution = new int[items.GetLength(0)+1, weight_limit+1];

            for (int i = 0; i < solution.GetLength(1); i++)
                solution[0, i] = 0;

            for (int i = 1; i < solution.GetLength(0); i++)
            {
                for (int j = 0; j < solution.GetLength(1); j++)
                {
                    if (items[i-1].weight > j)
                    {
                        solution[i, j] = solution[i - 1, j];
                    }
                    else
                    {
                        solution[i, j] = Math.Max(solution[i - 1, j], solution[i - 1, j - items[i-1].weight] + items[i-1].value);
                    }
                }
            }

            print_solution(solution);
        }

        static void SimpleKnapSack(SimpleItem[] items, int weight_limit)
        {
            List<SimpleItem> solution = new List<SimpleItem>();

            while (weight_limit > 0)
            {

            }

            for (int j = 0; j < solution.Count; j++)
            {
                Console.Write($"{solution[j],5}");
            }
            Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            int weight_limit = 10;
            Item[] items = new[] { new Item(5, 4), new Item(10, 8), new Item(3, 3), new Item(2, 5), new Item(3, 2) };

            KnapSack(items, weight_limit);


            int weight_limit2 = 1500;
            SimpleItem[] items2 = new[]
            {
                new SimpleItem(77),
                new SimpleItem(110),
                new SimpleItem(340),
                new SimpleItem(700)
            };

            SimpleKnapSack(items2, weight_limit2);
        }
    }
}
