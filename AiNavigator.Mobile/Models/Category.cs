using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AiNavigator.Mobile.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Category
    {
        [Description("Generowanie tekstu")]
        Text,

        [Description("Generowanie video")]
        Video,

        [Description("Programowanie")]
        Development,

        [Description("Generowanie grafiki")]
        Graphics,

        [Description("Agenci AI")]
        Agents
    }
}
