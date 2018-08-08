using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace JsonTest
{
    public enum SwearType
    {
        Phrase,
    }

    public class Swear
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public SwearType Type { get; set; }

        [JsonProperty]
        public string SwearPhrase { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            //List<Swear> Swears = JsonConvert.DeserializeObject<List<Swear>>(File.ReadAllText("assets\\swears.json"));
            List<Swear> Swears = new List<Swear>();
            Swears.Add(new Swear(){ Type = SwearType.Phrase, SwearPhrase = "bla"});
            Swears.Add(new Swear() { Type = SwearType.Phrase, SwearPhrase = "bla2" });

            Console.Write(JsonConvert.SerializeObject(Swears, Formatting.Indented));
        }



    }
}
