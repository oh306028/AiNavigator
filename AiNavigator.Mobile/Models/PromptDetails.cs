using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiNavigator.Mobile.Models
{
    public class PromptDetails
    {
        public string QueryDate { get; set; }
        public Category Category { get; set; }
        public List<ModelInfo> TopModels { get; set; } = new List<ModelInfo>();
        public string GeneralSummary { get; set; }
    }
}
