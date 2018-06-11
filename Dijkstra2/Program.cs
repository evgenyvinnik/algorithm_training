using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra2
{
    class Program
    {
        static int findNextVertex(int[] distances, bool[] reached)
        {
            int next_vertex = -1;
            int min_distance = int.MaxValue;

            for (int i = 0; i < distances.Length; i++)
            {
                if (distances[i] < min_distance && !reached[i])
                {
                    min_distance = distances[i];
                    next_vertex = i;
                }
            }
            return next_vertex;
        }

        static void printResuts(int[] distances)
        {
            Console.WriteLine("distances to vertice of choice");
            for(int i = 0; i < distances.Length; i++)
            {
                Console.WriteLine($"vertice {i} distance {distances[i]}");
            }
        }

        static void dijkstra(int[,] graph, int vertex)
        {
            int vertices = graph.GetLength(0);

            int[] distances = new int[vertices];
            bool[] reached = new bool[vertices];

            for (int i = 0; i < vertices; i++)
            {
                distances[i] = int.MaxValue;
                reached[i] = false;
            }

            distances[vertex] = 0;

            for (int i = 0; i < vertices; i++)
            {
                int chosen_vertex = findNextVertex(distances, reached);
                if (chosen_vertex != -1)
                {
                    reached[chosen_vertex] = true;

                    for (int j = 0; j < vertices; j++)
                    {
                        if (!reached[j])
                        if (graph[j, chosen_vertex] != 0)
                            if (distances[j] > distances[chosen_vertex] + graph[j, chosen_vertex])
                            {
                                distances[j] = distances[chosen_vertex] + graph[j, chosen_vertex];
                            }
                    }
                }
            }

            printResuts(distances);
        }

        static void Main(string[] args)
        {
            int[,] graph = new int[,]
            {
                {0,  5, 1, 4, 13},
                {5,  0, 0, 0, 7},
                {1,  0, 0, 2, 0},
                {4,  0, 2, 0, 0 },
                {13, 7, 0, 0 ,0 }
            };

            dijkstra(graph, 0);
        }
    }
}
