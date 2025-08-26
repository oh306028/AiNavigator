namespace AiNavigator.Api.Models
{
    public class ModelInfo
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Link { get; set; }
        public List<string> Pros { get; set; }
        public List<string> Cons { get; set; }
        public int Rank { get; set; }
    }
}
