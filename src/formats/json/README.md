### `ConsmicLexicon.Foundation/Text/Json/`
*   **Full Path in Tree:** `ConsmicLexicon.Foundation/Text/Json/`
*   **Namespace:** `ConsmicLexicon.Foundation.xText.Json`
*   **Goal:** To provide utilities and custom converters specifically for `System.Text.Json`, if such low-level utilities are needed in `Core`.
*   **Purpose:** Offers helper methods or `JsonConverter<T>` implementations that are foundational and don't introduce unwanted dependencies.
*   **Description:** Contains types that assist with `System.Text.Json` serialization and deserialization. Use with extreme caution in `Core`.
*   **Rules & Policies:**
    *   **Dependency Alert:** While `System.Text.Json` is part of the BCL, ensure its usage here doesn't inadvertently pull in a complex dependency graph or features not strictly "core".
    *   Only include if the utilities are genuinely foundational (e.g., a very common, simple `JsonConverter<T>`).
    *   Most application-specific JSON converters belong in higher layers. If this folder implies a significant dependency on `System.Text.Json`'s more advanced features, it might be better suited for `AreaX`. 