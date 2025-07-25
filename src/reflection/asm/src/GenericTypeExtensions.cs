namespace OpenEchoSystem.Core.xReflection.xAssembly
{
    public static class GenericTypeExtensions
    {
        public static bool IsDefault<T>(this T value) =>
        EqualityComparer<T>.Default.Equals(value, default!);

    }
}