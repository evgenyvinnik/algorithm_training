using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    class ShortestPath
    {
        int minDistance(int[] dist, bool[] verticesIncluded)
        {
            int min = Int32.MaxValue;
            int min_index = -1;

            for (int v = 0; v < dist.Length; v++)
            {
                if (verticesIncluded[v] == false && dist[v] < min)
                {
                    min = dist[v];
                    min_index = v;
                }
            }

            return min_index;
        }

        void printSolution(int[] dist)
        {
            Console.WriteLine("Vertex distance from the Source");

            for (int i = 0; i < dist.Length; i++)
            {
                Console.WriteLine($"{i} tt {dist[i]}");
            }
        }

        public void dijkstra(int[,] graph, int origin)
        {
            var graph_size = graph.GetLength(0);

            var dist = new int[graph_size];

            var verticesIncluded = new bool[graph_size];

            for (int i = 0; i < graph_size; i++)
            {
                dist[i] = int.MaxValue;
                verticesIncluded[i] = false;
            }

            dist[origin] = 0;

            for (int count = 0; count < graph_size; count++)
            {
                int u = minDistance(dist, verticesIncluded);
                verticesIncluded[u] = true;

                for (int v = 0; v < graph_size; v++)
                {
                    if(!verticesIncluded[v] && graph[u,v] != 0 &&
                       dist[u] != int.MaxValue && dist[u] + graph[u,v] < dist[v])
                    {
                        dist[v] = dist[u] + graph[u, v];
                    }
                }
            }

            printSolution(dist);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var graph = new int[,]
                {{0, 4, 0, 0, 0, 0, 0, 8, 0},
                {4, 0, 8, 0, 0, 0, 0, 11, 0},
                {0, 8, 0, 7, 0, 4, 0, 0, 2},
                {0, 0, 7, 0, 9, 14, 0, 0, 0},
                {0, 0, 0, 9, 0, 10, 0, 0, 0},
                {0, 0, 4, 14, 10, 0, 2, 0, 0},
                {0, 0, 0, 0, 0, 2, 0, 1, 6},
                {8, 11, 0, 0, 0, 0, 1, 0, 7},
                {0, 0, 2, 0, 0, 0, 6, 7, 0}};
            ShortestPath t = new ShortestPath();
            t.dijkstra(graph, 0);

            var graph1 = new int[,]
            {{0, 2, 0, 6, 0},
                {2, 0, 3, 8, 5},
                {0, 3, 0, 0, 7},
                {6, 8, 0, 0, 9},
                {0, 5, 7, 9, 0},
            };
            t.dijkstra(graph1, 0);
        }
    }
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        int testCases = int.Parse(Console.ReadLine());

    //        for (int i = 0; i < testCases; ++i)
    //        {

    //        }
    //    }
    //}
}
