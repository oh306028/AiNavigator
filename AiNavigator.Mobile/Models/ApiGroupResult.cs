using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AiNavigator.Mobile.Models
{
    public class ApiGroupResult
    {
        [JsonPropertyName("requestId")]
        public Guid RequestId { get; set; }

        [JsonPropertyName("models")]
        public List<HistoryItem> Models { get; set; } = new List<HistoryItem>();

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("requestDate")]
        public string RequestDate { get; set; }
    }

}
