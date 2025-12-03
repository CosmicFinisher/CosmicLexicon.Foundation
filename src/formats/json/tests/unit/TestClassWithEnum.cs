using System.Text.Json.Serialization;

namespace CosmicLexicon.Foundation.Formats.Json.UnitTest
{
    internal class TestClassWithEnum
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TestEnumExample Status { get; set; }
    }
}