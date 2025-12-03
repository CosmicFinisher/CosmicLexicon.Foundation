### `ConsmicLexicon.Foundation/Text/RegularExpressions/`
*   **Full Path in Tree:** `ConsmicLexicon.Foundation/Text/RegularExpressions/`
*   **Namespace:** `ConsmicLexicon.Foundation.Formats.RegularExpressions`
*   **Goal:** To provide utilities and pre-compiled regex instances for common patterns, complementing `System.Text.RegularExpressions`.
*   **Purpose:** Offers helper methods for validating or extracting data using regular expressions, and can provide commonly used, compiled `Regex` objects for performance.
*   **Description:** Contains types that simplify working with regular expressions.
*   **Rules & Policies:**
    *   Aligns with `System.Text.RegularExpressions`.
    *   If providing pre-defined `Regex` objects, ensure they are compiled with `RegexOptions.Compiled` for repeated use.
    *   Utilities should make common regex tasks easier or safer. 