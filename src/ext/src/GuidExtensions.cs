namespace CosmicLexicon.Foundation.Extensions;

using System;
using System.ComponentModel;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class GuidExtensions
{
    public static Guid NewIfNullOrEmpty(this Guid? value)
        => !value.HasValue || value.Value == Guid.Empty ? Guid.NewGuid() : value.Value;
    public static bool IsEmpty(this Guid value)
        => Guid.Empty == value;
}