using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teams2
{
    class Program
    {
        static void Main(string[] args)
        {
            string time1 = "1:45:10 PM";// 13:45:10
            //12AM -> 00:00
            //12PM -> 12

            string formattedTime = format24HR(time1);
            Console.WriteLine($"{formattedTime}");

            time1 = "12:10:10 AM"; // 00:10:10
            formattedTime = format24HR(time1);
            Console.WriteLine($"{formattedTime}");

            time1 = "12:10:10 PM"; // 12:10:10
            formattedTime = format24HR(time1);
            Console.WriteLine($"{formattedTime}");

            time1 = "12:10:10 PM"; // 12:10:10
            formattedTime = format24HR(time1);
            Console.WriteLine($"{formattedTime}");

            Dictionary<string, string> timeConvert = new Dictionary<string, string>();
            timeConvert.Add("12:10:10 AM", "00:10:10")
        }

        static string format24HR(string time)
        {
            if (time == null)
                return null;

            //[12]:[12]:[12]_"AM"/"PM"; 

            int h = 0;
            int m = 0;
            int s = 0;

            int add12 = 0;

            var timeSplit = time.Split(' ');
            var components = timeSplit[0].Split(':');

            h = Int32.Parse(components[0]);
            m = Int32.Parse(components[1]);
            s = Int32.Parse(components[2]);

            if (timeSplit[1] == "PM" && h != 12)//edge cases
            {
                add12 = 12;
            }
            else if (timeSplit[1] == "AM" && h == 12)
            {
                add12 = -12;
            }

            h += add12;

            return $"{h}:{m}:{s}";
        }
    }
}
