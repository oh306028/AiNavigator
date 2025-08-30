using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AiNavigator.Mobile.Models
{
    public class ApiResult
    {
        public string RequestId { get; set; }

        [JsonPropertyName("topModels")]
        public List<ModelInfo> Models { get; set; } = new List<ModelInfo>();

        [JsonPropertyName("generalSummary")]
        public string Summary { get; set; }
        public string RequestDate { get; set; }
    }
}
