using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AiNavigator.Api.Enums
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
