namespace CosmicLexicon.Foundation.Introspection
{
    public struct CustomStruct
    {
        public int Value { get; set; }
        public bool IsActive { get; set; }
    }

    public class CustomClass
    {
        public required string Name { get; set; }
    }
}