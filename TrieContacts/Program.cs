using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TrieContacts
{
    class Program
    {
        /*
             * Complete the contacts function below.
             */
        static int[] contacts(string[][] queries)
        {
            List<int> res = new List<int>();
            /*
             * Write your code here.
             */
            return res.ToArray();
        }

        static void Main(string[] args)
        {
            //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            int queriesRows = Convert.ToInt32(Console.ReadLine());

            string[][] queries = new string[queriesRows][];

            for (int queriesRowItr = 0; queriesRowItr < queriesRows; queriesRowItr++)
            {
                queries[queriesRowItr] = Console.ReadLine().Split(' ');
            }

            int[] result = contacts(queries);

            Console.WriteLine(string.Join("\n", result));

            //textWriter.Flush();
            //textWriter.Close();
        }
    }
}
