### `OpenEchoSystem.Core/Security/Cryptography/`
*   **Full Path in Tree:** `OpenEchoSystem.Core/Security/Cryptography/`
*   **Namespace:** `OpenEchoSystem.Core.xSecurity.Cryptography`
*   **Goal:** To provide utilities and extensions for cryptographic operations, complementing `System.Security.Cryptography`.
*   **Purpose:** Offers helper methods for common cryptographic tasks, such as hashing, encryption/decryption wrappers (using `System` providers), or secure random number generation utilities.
*   **Description:** Contains types that simplify the use of .NET's cryptographic services.
*   **Rules & Policies:**
    *   Aligns with `System.Security.Cryptography`.
    *   Do NOT implement custom cryptographic algorithms. Only use and provide helpers for the vetted algorithms within `System.Security.Cryptography`.
    *   Focus on simplifying common patterns, like encrypting/decrypting strings or streams using standard algorithms, or generating hashes. 