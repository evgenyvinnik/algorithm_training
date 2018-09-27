using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind
{
    class Program
    {
        const int slots = 4;
        const int marbleTypes = 6;

        static void Main(string[] args)
        {
            var solution = new[]{5, 2, 2, 1};

            solveMasterMind(solution);
        }

        static Tuple<int, int> getResult(int[] solution, int[] guess)
        {
            int red = 0;
            int white = 0;
            var marbleCounts = new int[marbleTypes];

            for (int i = 0; i < slots; i++)
            {
                marbleCounts[solution[i]]++;
            }

            for (int i = 0; i < slots; i++)
            {
                if (solution[i] == guess[i])
                {
                    red++;
                    marbleCounts[guess[i]]--;
                }
            }

            for (int i = 0; i < slots; i++)
            {
                if (marbleCounts[guess[i]] > 0)
                {
                    marbleCounts[guess[i]]--;
                    white++;
                }
            }

            return new Tuple<int, int>(white, red);
        }

        static void solveMasterMind(int[] solution)
        {
            var intermediateSolution = new int[slots];

            List<int> marbles = new List<int>();
            int exclude = -1;

            int steps = 0;

            for (int i = 0; i < marbleTypes; i++)
            {
                for (int j = 0; j < slots; j++)
                {
                    intermediateSolution[j] = i;
                }

                steps++;

                var res = getResult(solution, intermediateSolution);

                Console.WriteLine($"step {steps}\t{string.Join(" ", intermediateSolution)}\twhite {res.Item1} red {res.Item2}");

                if (res.Item2 == 4)
                {
                    Console.WriteLine("You are a winner!");
                    return;
                }

                if (res.Item2 == 0)
                {
                    exclude = i;
                }

                for (int j = 0; j < res.Item2; j++)
                {
                    marbles.Add(i);
                }

                if (marbles.Count == slots)
                {
                    if (exclude == -1)
                    {
                        exclude = i + 1;
                    }

                    break;
                }

                if (i == marbleTypes - 2)
                {
                    while (marbles.Count < slots)
                    {
                        marbles.Add(i+1);
                    }

                    break;
                }
            }

            int pos = 0;
            var finalSolution = new int[slots];

            while (true)
            {
                for (int j = 0; j < slots; j++)
                {
                    intermediateSolution[j] = exclude;
                }

                for (int j = 0; j < marbles.Count; j++)
                {
                    if (j != 0 && marbles[j] == marbles[j - 1])
                    {
                        continue;
                    }

                    intermediateSolution[pos] = marbles[j];

                    steps++;
                    var res = getResult(solution, intermediateSolution);

                    Console.WriteLine($"step {steps}\t{string.Join(" ", intermediateSolution)}\twhite {res.Item1} red {res.Item2}");

                    if (marbles.Count == 2)
                    {
                        if (res.Item2 == 1)
                        {
                            finalSolution[pos] = marbles[j];
                            finalSolution[pos + 1] = marbles[j + 1];
                        }
                        else
                        {
                            finalSolution[pos] = marbles[j + 1];
                            finalSolution[pos + 1] = marbles[j];
                        }
                        marbles.RemoveAt(j + 1);
                        marbles.RemoveAt(j);
                        break;
                    }
                    else if (res.Item2 == 1)
                    {
                        finalSolution[pos] = marbles[j];
                        marbles.RemoveAt(j);
                        pos++;
                        break;
                    }
                    else if (j == marbles.Count - 2)
                    {
                        finalSolution[pos] = marbles[j+1];
                        marbles.RemoveAt(j + 1);
                        pos++;
                        break;
                    }
                }

                if (marbles.Count == 0)
                {
                    break;
                }
            }

            steps++;
            var resFinal = getResult(solution, finalSolution);
            Console.WriteLine($"step {steps}\t{string.Join(" ", finalSolution)}\twhite {resFinal.Item1} red {resFinal.Item2}");
        }
    }
}
