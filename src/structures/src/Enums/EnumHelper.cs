namespace CosmicLexicon.Foundation.Structures.Enums
{

    public static class EnumHelper
    {
        public static TRetun[] MakeArray<TEnum, TRetun>()
            where TEnum : struct, Enum
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enum type.");
            }

            return new TRetun[Enum.GetNames(typeof(TEnum)).Length];
        }
        public static TEnum[] MakeArray<TEnum>()
            where TEnum : struct, Enum => MakeArray<TEnum, TEnum>();

    }
}
