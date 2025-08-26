using AiNavigator.Api.Enums;

namespace AiNavigator.Api.Models
{
    public class PromptDetails
    {
        public string QueryDate { get; set; }
        public Category Category { get; set; }
        public List<ModelInfo> TopModels { get; set; } = new List<ModelInfo>();
        public string GeneralSummary { get; set; }
    }
}
