namespace CosmicLexicon.Foundation.Extensions;

using System;
using System.ComponentModel;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class GuidExtensions
{
    extension(Guid? value)
    {
        public Guid NewIfNullOrEmpty()
        => !value.HasValue || value.Value == Guid.Empty ? Guid.NewGuid() : value.Value;
    }

    extension(Guid value)
    {
        public bool IsEmpty()
        => Guid.Empty == value;
    }
}