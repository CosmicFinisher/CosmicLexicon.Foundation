namespace CosmicLexicon.Foundation.Structures.Enums
{
    public sealed class EnumDictionary<TEnum, TValue>
        where TEnum : struct, Enum
    {
        private readonly TValue[] values;
        private readonly string[] members;
        private readonly Dictionary<TEnum, int> memberIndices = [];
        public EnumDictionary(TValue defaultValue)
        {
            DefaultValue = defaultValue;
            members = Enum.GetNames<TEnum>();
            values = new TValue[members.Length];
            for (int i = 0; i < members.Length; i++)
            {
                values[i] = defaultValue;
                memberIndices.Add(GetMember(members[i]), i);
            }
        }
        public int Count => values.Length;
        public Type EnumType => typeof(TEnum);

        public TValue DefaultValue { get; set; }
        public void ResetValues(TValue val)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = val;
            }
        }
        public void SetValueFor(TEnum forMember, TValue value)
        {
            values[GetIndex(forMember)] = value;
        }

        public void SetValueFor(TEnum[] forMember, TValue value)
        {
            if (forMember is null)
                throw new ArgumentNullException(nameof(forMember));

            for (int i = 0; i < forMember.Length; i++)
            {
                if (!memberIndices.TryGetValue(forMember[i], out int index))
                {
                    continue;
                }
                values[index] = value;
            }
        }

        public TValue Member(TEnum forMember)
        {
            return values[GetIndex(forMember)];
        }

        public TEnum WhereValue(TValue withValue)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] is IComparable comparable && comparable.CompareTo(withValue) == 0)
                {
                    return GetMember(members[i]);
                }
            }
            return default;

        }
        public KeyValuePair<TEnum, TValue>[] SelectValues(TValue[] withValues)
        {
            var collection = new List<KeyValuePair<TEnum, TValue>>();

            for (int i = 0; i < values.Length; i++)
            {
                if (withValues.Any(o => values[i] is IComparable comparable && comparable.CompareTo(o) == 0))
                {
                    collection.Add(new (GetMember(members[i]), values[i]));
                }
            }

            return [.. collection.ToArray()];

        }

        public KeyValuePair<TEnum, TValue>[] SelectValues(TValue withValue)
        {
            var collection = new List<KeyValuePair<TEnum, TValue>>();

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] is IComparable comparable && comparable.CompareTo(withValue) == 0)
                {
                    collection.Add(new(GetMember(members[i]), values[i]));
                }
            }

            return [.. collection];
        }

        public TEnum[] WhereValues(TValue withValue)
        {
            var arr = new List<TEnum>();

            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] is IComparable comparable && comparable.CompareTo(withValue) == 0)
                {
                    arr.Add(GetMember(members[i]));
                }
            }

            return [.. arr.ToArray()];

        }

        private TEnum GetMember(string memberName)
        {
            if (string.IsNullOrEmpty(memberName))
            {
                throw new ArgumentException($"'{nameof(memberName)}' cannot be null or empty.", nameof(memberName));
            }

            if (!Enum.TryParse<TEnum>(memberName, out var member))
            {
                throw new ArgumentException(nameof(GetMember));
            }

            return member;
        }

        private int GetIndex(TEnum forMember)
        {
            if (!memberIndices.TryGetValue(forMember, out int index))
            {
                throw new ArgumentException(nameof(forMember));
            }
            return index;
        }

        private static bool ComapreMember(TEnum forMember, string member)
        {
            return forMember.ToString() == member;
        }


        public string[] GetMembers()
        {
            return members;
        }
    }
}
