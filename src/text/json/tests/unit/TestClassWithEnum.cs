using System.Text.Json.Serialization;

namespace OpenEchoSystem.Core.xText.Json
{
    internal class TestClassWithEnum
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TestEnumExample Status { get; set; }
    }
}