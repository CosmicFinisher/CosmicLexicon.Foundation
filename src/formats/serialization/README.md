# ConsmicLexicon.Foundation.Formats.Serialization

This project contains core runtime serialization functionalities. 

### `ConsmicLexicon.Foundation/Runtime/Serialization/`
*   **Full Path in Tree:** `ConsmicLexicon.Foundation/Runtime/Serialization/`
*   **Namespace:** `ConsmicLexicon.Foundation.Formats.Serialization`
*   **Goal:** To provide foundational utilities or interfaces for object serialization, aligning with `System.Runtime.Serialization` concepts if applicable, or providing base support for other `System.*` serializers.
*   **Purpose:** Offers base contracts or helper methods for serialization processes, without tying to a specific serialization engine if aiming for abstraction.
*   **Description:** Contains types related to the process of converting objects into a format suitable for storage or transmission. This is for *foundational* serialization helpers, not complete serializers.
*   **Rules & Policies:**
    *   If providing abstract serialization helpers, they should not depend on any specific third-party serialization library.
    *   Can provide base interfaces (e.g., `ICoreSerializer`) if higher layers will provide concrete implementations.
    *   If focusing on `System.Runtime.Serialization` (e.g. `DataContractSerializer`), utilities should directly support it. 