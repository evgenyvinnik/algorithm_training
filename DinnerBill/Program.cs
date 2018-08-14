using System;
using System.Collections.Generic;

/*
I visited a restaurant several times for a meal with one or more guests, and spent some amount of money. Given the following menu, which item(s) did I purchase for the receipt totals below?

The Menu. My guests and I can order more than one of the same item.

veggie sandwich: 6.85
extra veggies: 2.20
chicken sandwich: 7.85
extra chicken: 3.20
cheese: 1.25
chips: 1.40
nachos: 3.45
soda: 2.05

Each of the following receipt totals could have one or more solutions that result in $0.00 money left over. Give preference to a solution of items which is shorter than others for that receipt total.

13.75
4.85
17.75
11.05
19.40
28.25
40.30
75.00

map<float, string> menu_items = new Dictionary<float, string>();
menu_items.put(6.85, "veggie sandwich");
menu_items.put(2.20, "extra veggies");
menu_items.put(7.85, "chicken sandwich");
menu_items.put(3.20, "extra chicken");
menu_items.put(1.25, "cheese");
menu_items.put(1.40, "chips");
menu_items.put(3.45, "nachos");
menu_items.put(2.05, "soda");

*/


namespace DinnerBill
{
    class Program
    {
        static void Main(string[] args)
        {
            //price - item
            Dictionary<int, string> menu = new Dictionary<int, string>();
            menu.Add(785, "chicken sandwich");
            menu.Add(685, "veggie sandwich");
            menu.Add(345, "nachos");
            menu.Add(320, "extra chicken");
            menu.Add(220, "extra veggies");
            menu.Add(205, "soda");
            menu.Add(140, "chips");
            menu.Add(125, "cheese");

            List<int> ordersToDecifer = new List<int> { 1375, 485, 1775, 1105, 1825, 1940, 2825, 4030, 7500 };

            foreach (var order in ordersToDecifer)
            { 
                Dictionary<string, Tuple<int, int>> res = decipherOrder(order, menu);

                Console.WriteLine($"order {order}");

                foreach (var item in res)
                {
                    if (item.Value.Item1 > 0)
                        Console.WriteLine($"{item.Key} {item.Value.Item1} * {item.Value.Item2}");
                }

                Console.WriteLine();
            }
        }

        static int sumDict(Dictionary<string, Tuple<int, int>> solution)
        {
            int sum = 0;
            foreach (var item in solution)
            {
                sum += item.Value.Item2 * item.Value.Item1;
            }

            return sum;
        }

        static Dictionary<string, Tuple<int, int>> decipherOrder(int orderValue, Dictionary<int, string> menu)
        {
            if (orderValue == 0)
            {
                return new Dictionary<string, Tuple<int, int>>();
            }

            foreach (var menuItem in menu)
            {
                int diff = orderValue - menuItem.Key;

                if (diff >= 0)
                {
                    var potentialSolution = decipherOrder(diff, menu);
                    if (potentialSolution.ContainsKey(menuItem.Value))
                    {
                        var t = potentialSolution[menuItem.Value];
                        potentialSolution[menuItem.Value] = new Tuple<int, int>(t.Item1+1, menuItem.Key);
                    }
                    else
                    {
                        potentialSolution.Add(menuItem.Value, new Tuple<int, int>(1, menuItem.Key));
                    }

                    if (sumDict(potentialSolution) == orderValue)
                    {
                        return potentialSolution;
                    }
                }
            }

            return new Dictionary<string, Tuple<int, int>>();
        }
    }
}
