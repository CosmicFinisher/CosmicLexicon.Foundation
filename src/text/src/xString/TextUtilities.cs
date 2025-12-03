using System.Collections.Generic;
using System.Linq;

namespace CosmicLexicon.Foundation.xText.xString // Updated namespace
{
    public static class TextUtilities
    {
        public static Tuple<int, int> GetLineFromPosition(int position, string sourceText)
        {
            int lineStart = position;
            int lineEnd = position;

            for (; lineStart > 0; lineStart--)
            {
                if (CharHelpers.IsLineBreakChar(sourceText[lineStart - 1]))
                {
                    break;
                }
            }

            for (; lineEnd < sourceText.Length - 1; lineEnd++)
            {
                if (CharHelpers.IsLineBreakChar(sourceText[lineEnd + 1]))
                {
                    break;
                }
            }

            return Tuple.Create(lineStart, lineEnd - lineStart + 1);
        }

        public static int GetLineNumber(int start, int[] lineLengths)
        {
            ArgumentNullException.ThrowIfNull(lineLengths);
            for (int i = 0; i < lineLengths.Length; i++)
            {
                if (start < lineLengths[i])
                {
                    return i;
                }

                start -= lineLengths[i];
            }

            return 0; // Or perhaps lineLengths.Length if start is beyond all lines
        }
    }
} 