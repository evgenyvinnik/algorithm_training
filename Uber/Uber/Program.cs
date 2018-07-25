using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uber
{
    public class CityTrie
    {
        public char Node { get; set; }
        public Dictionary<char, CityTrie> Leafs { get; set; }
        public bool End { get; set; }

        public CityTrie(char node)
        {
            this.Node = node;
            Leafs = new Dictionary<char, CityTrie>();
            End = false;
        }
    }

    class Program
    {


        static void Main(string[] args)
        {
            CityTrie cityDb = new CityTrie('\0');
            CreateInput(ref cityDb);
            FindCities("set", cityDb);
            FindCities("San", cityDb);
            FindCities("Ne", cityDb);
        }

        static void CreateInput(ref CityTrie cityDb)
        {
            // input creation
            List<string> cities = new List<string>
            {
                "san francisco",
                "san diego",
                "san antonio",
                "san",
                "new york",
                "new babylon",
                "newly"
            };

            foreach(string city in cities)
            {
                AddCityToDataSet(city, ref cityDb);
            }
        }

        static void AddCityToDataSet(string city, ref CityTrie cityDb)
        {
            CityTrie cityDbHelper = cityDb;
            for (int i = 0; i < city.Length; i++)
            {
                if(!cityDbHelper.Leafs.ContainsKey(city[i]))
                {
                    cityDbHelper.Leafs.Add(city[i], new CityTrie('\0'));
                }

                cityDbHelper = cityDbHelper.Leafs[city[i]];
                if (i == city.Length - 1)
                {
                    cityDbHelper.End = true;
                }
            }
        }

        static void FindCities(string city, CityTrie cityDb)
        {
            CityTrie cityDbHelper = cityDb;
            int i;
            for (i = 0; i < city.Length; i++)
            {
                if(cityDbHelper.Leafs.ContainsKey(city[i]))
                {
                    cityDbHelper = cityDbHelper.Leafs[city[i]];
                }
                else
                {
                    break;
                }
            }

            if(i == city.Length)
            {
                if(cityDbHelper.End)
                {
                    Console.WriteLine(city);
                }

                GetCitiesFromDb(city, cityDbHelper);
            }
        }

        static string GetCitiesFromDb(string s, CityTrie cityDb)
        {
            if(cityDb.Leafs.Count == 1)
            {
                return s;
            }
            else
            {
                foreach (var city in cityDb.Leafs)
                    return s + GetCitiesFromDb(s, city.Leafs[c]);
            }
        }
    }
}
