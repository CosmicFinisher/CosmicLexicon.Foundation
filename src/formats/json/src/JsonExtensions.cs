using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;


namespace CosmicLexicon.Foundation.Formats.Json;

public static class JsonExtensions
{
    private static readonly Lazy<JsonSerializerOptions> LazyOptions =
        new(() => new JsonSerializerOptions().Configure(), isThreadSafe: true);

    extension(string value)
    {
        /// <summary>
        /// Converts a JSON string to an object of type T.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="value">The JSON string to deserialize.</param>
        /// <returns>The deserialized object of type T.</returns>
        public T FromJson<T>() =>
            value != null ? JsonSerializer.Deserialize<T>(value, LazyOptions.Value) : default!;
    }

    extension<T>(T value)
    {
        /// <summary>
        /// Converts an object to JSON string.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="value">The object to convert.</param>
        /// <returns>The JSON string representation of the object.</returns>
        public string? ToJson() =>
            value is null ? null : JsonSerializer.Serialize(value, LazyOptions.Value);
    }

    extension(JsonSerializerOptions jsonSettings)
    {
        /// <summary>
        /// Configures the JsonSerializerOptions instance.
        /// </summary>
        /// <param name="jsonSettings">The JsonSerializerOptions instance to configure.</param>
        /// <returns>The configured JsonSerializerOptions instance.</returns>
        public JsonSerializerOptions Configure()
        {
            jsonSettings.WriteIndented = false;
            jsonSettings.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            jsonSettings.ReadCommentHandling = JsonCommentHandling.Skip;
            jsonSettings.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonSettings.TypeInfoResolver = new PrivateConstructorContractResolver();
            jsonSettings.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
            return jsonSettings;
        }
    }
}

/// <summary>
/// Provides a custom <see cref="JsonTypeInfoResolver"/> for <see cref="System.Text.Json"/> that enables deserialization
/// of types with private constructors by using <see cref="Activator.CreateInstance(Type, bool)"/> with the `nonPublic` flag set to true.
/// This resolver is crucial for scenarios where objects are designed with private constructors
/// (e.g., for immutability patterns, factory methods, or singletons) but still need to be
/// deserialized by <see cref="System.Text.Json"/>.
/// </summary>
internal sealed class PrivateConstructorContractResolver : DefaultJsonTypeInfoResolver
{
    /// <summary>
    /// Overrides the default <see cref="GetTypeInfo(Type, JsonSerializerOptions)"/> method to provide
    /// custom JSON type information, specifically enabling the creation of objects that have
    /// private constructors but no public ones.
    /// </summary>
    /// <param name="type">The type for which to get the JSON type information.</param>
    /// <param name="options">The <see cref="JsonSerializerOptions"/> currently in use for serialization/deserialization.</param>
    /// <returns>A <see cref="JsonTypeInfo"/> instance for the specified <paramref name="type"/>,
    /// potentially modified to support private constructor instantiation.</returns>
    /// <remarks>
    /// This method checks if the type is an object type, has no public constructors, and its <see cref="JsonTypeInfo.CreateObject"/>
    /// factory is not already set. If these conditions are met, it assigns a new factory that
    /// attempts to create an instance using <see cref="Activator.CreateInstance(Type, bool)"/> with the `nonPublic`
    /// parameter set to <c>true</c>. This allows <see cref="System.Text.Json"/> to instantiate types
    /// that would otherwise be un-deserializable due to constructor visibility.
    /// Robust error handling is included to catch instantiation failures, providing clear
    /// <see cref="InvalidOperationException"/> messages.
    /// </remarks>
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

        // Check if the type is an object, has no public constructor, and CreateObject is not already set
        if (jsonTypeInfo.Kind == JsonTypeInfoKind.Object
            && jsonTypeInfo.CreateObject is null
            && jsonTypeInfo.Type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Length == 0)
        {
            // Set CreateObject to a lambda expression that creates an instance using a private constructor
            jsonTypeInfo.CreateObject = () =>
            {
                try
                {
                    return Activator.CreateInstance(jsonTypeInfo.Type, nonPublic: true)
                    ?? throw new InvalidProgramException($"Failed to instantiate type '{jsonTypeInfo.Type.FullName}'. Activator.CreateInstance returned null.");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Failed to create instance of type '{jsonTypeInfo.Type.FullName}' using private constructor.", ex);
                }
            };
        }

        return jsonTypeInfo;
    }
}
