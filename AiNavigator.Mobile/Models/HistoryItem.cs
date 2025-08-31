using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AiNavigator.Mobile.Models
{
    public class HistoryItem
    {
        [JsonPropertyName("category")]
        public int Category { get; set; }
        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

}
