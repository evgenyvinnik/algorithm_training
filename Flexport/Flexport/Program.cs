using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flexport
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tuple<string, Tuple<int, int>> > guestStays = new List<Tuple<string, Tuple<int, int>>>();

            guestStays.Add(new Tuple<string, Tuple<int, int>>("A", new Tuple<int, int>(1, 5)));
            guestStays.Add(new Tuple<string, Tuple<int, int>>("B", new Tuple<int, int>(3, 6)));
            guestStays.Add(new Tuple<string, Tuple<int, int>>("C", new Tuple<int, int>(6, 8)));
            guestStays.Add(new Tuple<string, Tuple<int, int>>("D", new Tuple<int, int>(9, 11)));

            CalculateDisjointRanges(guestStays);

            var result = CalculateDisjointRanges(guestStays);
        }

        static List<Tuple<List<string>, Tuple<int, int>>> CalculateDisjointRanges(
            List<Tuple<string, Tuple<int, int>>> guestStays)// N - number of guests
        {
            List<Tuple<List<string>, Tuple<int, int>>> res = new List<Tuple<List<string>, Tuple<int, int>>>();

            SortedSet<int> stayDates = new SortedSet<int>();

            foreach(var guest in guestStays) // O(N)
            {
                var stay = guest.Item2;

                stayDates.Add(stay.Item1);

                stayDates.Add(stay.Item2);
            }

            var listDates = stayDates.ToList();// max O(2*N) -> O(N)

            for(int i = 0; i < listDates.Count-1; i++)// O(2*N)
            {
                var begin = listDates[i];
                var end = listDates[i + 1];

                List<string> guestsInRange = new List<string>();

                foreach(var guest in guestStays) // O(N)
                {
                    if(CheckGuestInRange(guest, begin, end))
                    {
                        guestsInRange.Add(guest.Item1);
                    }
                }

                if(guestsInRange.Count >0)
                {
                    res.Add(new Tuple<List<string>, Tuple<int, int>>(guestsInRange, new Tuple<int, int>(begin, end)));
                }
            }

            // 2 cycles O(N^N)

            return res;
        }

        static bool CheckGuestInRange(Tuple<string, Tuple<int, int>> guestStay, int begin, int end)
        {
            bool res = false;

            var stayDates = guestStay.Item2;//A -> 1 - 5

            if (stayDates.Item2 <= begin)//begin = 1
                return res;

            if (stayDates.Item1 >= end) //end = 3
                return res;

            res = true;

            return res;
        }
    }
}
