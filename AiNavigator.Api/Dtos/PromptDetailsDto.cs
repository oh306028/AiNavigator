using AiNavigator.Api.Models;

namespace AiNavigator.Api.Dtos
{
    public class PromptDetailsDto
    {
        public string QueryDate { get; set; }
        public int Category { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Link { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }
        public int Rank { get; set; }
        public string GeneralSummary { get; set; }
    }
}
