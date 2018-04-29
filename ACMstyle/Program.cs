using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACMstyle
{
    class Program
    {
        static void Main()
        {
            int testCases = int.Parse(Console.ReadLine());

            for(int i =0; i < testCases; ++i)
            {
                int testNumbers = int.Parse(Console.ReadLine());

                if (testNumbers > 1)
                {
                    var test = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();
                    test.Sort();
                    int j;
                    for (j = 0; j < testNumbers- 1; ++j)
                    {
                        int current = test[j];
                        if(current != j + 1)
                        {
                            Console.WriteLine(j+1);
                            break;
                        }
                    }

                    if( j == testNumbers-1)
                    {
                        Console.WriteLine(testNumbers);
                    }
                }
                else
                {
                    Console.WriteLine(1);
                }
            }
        }


    }
}
