using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnbTest
{
    class Program
    {
        static int[] getRoundedTaxes(double[] taxes, out int outSum)
        {
            outSum = (int) Math.Round(taxes.Sum());

            double rem = outSum;
            Dictionary<Tuple<int, double>, double> dic = new Dictionary<Tuple<int, double>, double>();
            //double[] remAr = new double[taxes.Length];

            for(int i = 0; i < taxes.Length; i++)
            {
                var floor = Math.Floor(taxes[i]);
                rem -= floor;
                dic.Add(new Tuple<int, double>(i, taxes[i]), taxes[i]-floor);
            }

            var ordered = dic.OrderBy(x => x.Value);

            int K = taxes.Length - (int)rem;

            List<int> rounded = new List<int>();

            for(int i = 0; i < taxes.Length;i++)
            {
                if(i < K)
                {
                    rounded.Add((int)Math.Floor(ordered.ElementAt(i).Key.Item2));
                }
                else
                {
                    rounded.Add((int)Math.Ceiling(ordered.ElementAt(i).Key.Item2));
                }
            }


            return rounded.ToArray();
        }
        static void Main(string[] args)
        {
            double[] taxes = new double[] { 1.4, 2.5, 4.8 };

            int outSum;
            var res = getRoundedTaxes(taxes, out outSum);

            Console.WriteLine($"{string.Join(" ", taxes)}");
            Console.WriteLine($"{string.Join(" ", res)}");
            Console.WriteLine($"{outSum}");
        }


    }
}
