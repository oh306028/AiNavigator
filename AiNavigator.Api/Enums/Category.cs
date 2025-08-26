using System.ComponentModel;

namespace AiNavigator.Api.Enums
{
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
