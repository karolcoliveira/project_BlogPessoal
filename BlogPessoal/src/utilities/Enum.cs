using System.Text.Json.Serialization;

namespace BlogPessoal.src.utilities
{
    /// <summary>
    /// <para>Summary: Enum responsible for defining System User Types</para>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TypeUser
    {
        NORMAL,
        ADMINISTRATOR
    }
}
