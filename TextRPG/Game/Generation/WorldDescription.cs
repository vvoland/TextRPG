using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TextRPG.Game.Generation
{
    [JsonObject]
    public class WorldDescription
    {
        [JsonProperty("cities.count")]
        public int CitiesCount { get; set; }

        [JsonProperty("cities.names")]
        public List<string> CitiesNames { get; set; }

        [JsonProperty("cities.max_links")]
        public int CitiesMaxLinks { get; set; }
        [JsonProperty("cities.max_vendors")]
        public int CitiesMaxVendors { get; set; }


        [JsonProperty("people.names")]
        public List<string> PeopleNames { get; set; }

        public static WorldDescription FromFile(string file)
        {
            var data = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<WorldDescription>(data);
        }
    }
}