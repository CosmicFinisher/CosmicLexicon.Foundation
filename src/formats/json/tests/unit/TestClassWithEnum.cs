using System.Text.Json.Serialization;

namespace CosmicLexicon.Foundation.xText.Json
{
    internal class TestClassWithEnum
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TestEnumExample Status { get; set; }
    }
}