namespace OpenEchoSystem.Core.xText.Json
{
    internal class TestClassExample
    {
        public string? Name { get; set; }
        public int Value { get; set; }

        public TestClassExample() { } // Parameterless constructor for deserialization

        public TestClassExample(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}