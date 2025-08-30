using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiNavigator.Mobile.Models
{
    public class ModelInfo
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Link { get; set; }
        public List<string> Pros { get; set; } = new List<string>();
        public List<string> Cons { get; set; } = new List<string>();
        public int Rank { get; set; }
    }
}
