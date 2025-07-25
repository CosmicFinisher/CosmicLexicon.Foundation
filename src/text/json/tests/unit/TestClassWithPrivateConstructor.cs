namespace OpenEchoSystem.Core.xText.Json
{
    internal class TestClassWithPrivateConstructor
    {
        public int Id { get; }
        public string? Description { get; }

        private TestClassWithPrivateConstructor() { } // Private constructor for deserialization

        public TestClassWithPrivateConstructor(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}