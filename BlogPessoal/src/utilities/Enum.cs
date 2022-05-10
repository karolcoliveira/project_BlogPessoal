using System.Text.Json.Serialization;

namespace BlogPessoal.src.utilities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TypeUser
    {
        NORMAL,
        ADMINISTRATOR
    }
}
