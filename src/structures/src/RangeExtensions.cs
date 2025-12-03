using System.Runtime.Versioning;

namespace CosmicLexicon.Foundation.Structures
{
  
    [RequiresPreviewFeatures]
    public static class RangeExtensions
    {
        extension(Range range)
        {
            public CustomRangeEnumerator GetEnumerator() => new CustomRangeEnumerator(range);
        }

        public ref struct CustomRangeEnumerator
        {
            private int _current;
            private int _end;
            public bool Reversed { get; }
            public int Current => _current;

            public CustomRangeEnumerator(Range range)
            {
                //if (range.Start.Value < 0) throw new ArgumentOutOfRangeException(nameof(range.Start));
                //if (range.End.Value < 0) throw new ArgumentOutOfRangeException(nameof(range.End));

                if (range.End.IsFromEnd || range.Start.IsFromEnd)
                {
                    throw new NotSupportedException(nameof(range));
                }

                Reversed = range.Start.Value > range.End.Value;

                if (Reversed)
                {
                    _current = range.Start.Value + 1;
                    _end = range.End.Value;
                }
                else
                {
                    _current = range.Start.Value - 1;
                    _end = range.End.Value;
                }
            }
            public bool MoveNext()
            {
                if (!Reversed)
                {
                    _current++;
                    return _current <= _end;
                }
                _current--;
                return _current >= _end;
            }
        }
    }
} 