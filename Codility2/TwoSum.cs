using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codility2
{
    public class TwoSum
    {

        List<int> numbers = new List<int>();
        HashSet<int> sums = new HashSet<int>();

        /** Initialize your data structure here. */
        public TwoSum()
        {

        }

        /** Add the number to an internal data structure.. */
        public void Add(int number)
        {
            foreach (var n in numbers)
            {
                sums.Add(number + n);
            }

            numbers.Add(number);
        }

        /** Find if there exists any pair of numbers which sum is equal to the value. */
        public bool Find(int value)
        {
            return sums.Contains(value);
        }
    }
}
