# Test Plan for ConsmicLexicon.Foundation.Formats Module Granular Testing

## 1. Introduction

This document outlines the detailed granular test plan for the `ConsmicLexicon.Foundation.Formats` module, specifically targeting critical functions and classes within the [`src/text/src/xString/`](src/text/src/xString/) directory. The primary goal is to significantly increase code coverage from its current low of 9.75% line coverage and 7.53% branch coverage, as identified in [`docs/analysis/code_analysis_report.md`](docs/analysis/code_analysis_report.md), by implementing comprehensive unit tests. This plan adheres to London School Test-Driven Development (TDD) principles, emphasizing interaction-based testing and collaborator mocking, and defines a robust recursive testing strategy to ensure continuous stability and early regression detection. Every element within this test plan, from scope definition to individual test case outcomes, is designed to be AI verifiable.

## 2. Test Scope

The granular tests defined in this plan directly contribute to the successful achievement of the following AI Verifiable End Results from [`docs/PRDMasterPlan.md`](docs/PRDMasterPlan.md):

*   **Phase 4: Test Expansion and Validation**
    *   **Phase AI Verifiable End Goal:** Test coverage is significantly improved, and all high-level acceptance tests pass. AI can verify by running the tests and checking for a 100% pass rate.
    *   **Task 4.1: Expand test coverage to address identified gaps.**
        *   **AI Verifiable Deliverable/Completion Criteria:** Test coverage is expanded, and a script can verify that the new tests cover the previously identified gaps.

This test plan specifically details the unit tests required to address the identified test coverage gaps within the `ConsmicLexicon.Foundation.Formats` module, thereby contributing directly to Task 4.1's deliverable and the overarching Phase 4 goal of improved test coverage and a 100% test pass rate.

## 3. Test Strategy: London School TDD Principles

Our testing strategy for the `ConsmicLexicon.Foundation.Formats` module will strictly adhere to London School TDD principles. This approach focuses on testing the observable behavior of a unit (class or method) by examining its interactions with its collaborators rather than inspecting its internal state.

*   **Interaction-Based Testing:** Tests will assert that the unit under test sends the correct messages (method calls) with the correct arguments to its collaborators at the correct times, and that it reacts appropriately to messages received from its collaborators.
*   **Mocking Collaborators:** For any external dependencies or collaborators (e.g., `System.Text.Encoding`, custom formatters like `IStringFormatter`), mock objects will be used. These mocks will simulate the behavior of real collaborators, allowing us to isolate the unit under test and verify its interactions without relying on the actual implementation of the dependencies. This ensures that failures are attributed directly to the unit being tested.
*   **Observable Outcomes:** The primary focus of assertions will be on the observable outcomes of the unit's behavior. This includes return values, changes in parameters passed by reference, and, critically, the verification of expected method calls on mocked collaborators.

## 4. Recursive Testing Strategy (Frequent Regression)

To ensure ongoing stability and to catch regressions early as the system is built towards passing high-level acceptance tests, a comprehensive recursive testing strategy will be implemented.

### 4.1. Triggers for Re-execution

Test suites or subsets thereof will be re-executed based on the following Software Development Life Cycle (SDLC) touch points:

*   **Local Development (Before Commit/Push):** Developers will run unit tests for the specific features/modules they are actively working on.
    *   **AI Verifiable Criterion:** Test runner output indicates all relevant unit tests pass for the changed files.
*   **Pull Request (PR) Submission:** All unit tests related to the changed module(s) and their direct dependencies will be executed as part of the CI pipeline.
    *   **AI Verifiable Criterion:** CI pipeline report confirms 100% pass rate for `@Unit` and `@Feature:xText` tagged tests in affected areas.
*   **Nightly Builds:** A full suite of all `@Unit` and `@Regression` tagged tests across the entire `ConsmicLexicon.Foundation` library will be executed.
    *   **AI Verifiable Criterion:** Automated nightly build report shows 100% pass rate for all `@Unit` and `@Regression` tests.
*   **Pre-Release Builds:** All `@Unit`, `@Integration`, and `@Regression` tests will be executed, along with a selected subset of high-level acceptance tests.
    *   **AI Verifiable Criterion:** Pre-release CI/CD pipeline report confirms 100% pass rate across all test categories and the specified high-level acceptance test subset.
*   **Dependency Updates:** When external or internal library dependencies are updated, unit tests of modules directly affected by the dependency change, along with relevant integration tests, will be re-run.
    *   **AI Verifiable Criterion:** CI pipeline triggered by dependency update confirms 100% pass rate for re-executed tests.

### 4.2. Test Prioritization and Tagging

Tests will be categorized and tagged to facilitate efficient subset selection for regression purposes:

*   **`@Unit`:** Granular tests focusing on isolated units of code, with collaborators mocked. These form the foundation of our test suite.
*   **`@Integration`:** (Less focus for this granular plan, but for future context) Tests verifying interactions between multiple units or components without mocking all external systems.
*   **`@Regression`:** Critical tests identified as essential for maintaining core system functionality and preventing regressions. All new `@Unit` tests will implicitly be included in the regression suite.
*   **`@Feature:xText`:** Tags all tests specifically related to the `ConsmicLexicon.Foundation.Formats` module, allowing for targeted execution.

### 4.3. Test Subset Selection for Regression Triggers

*   **Local Development:** Run tests tagged `@Unit` and `@Feature:xText` that correspond to the actively modified files or features.
*   **PR Submission:** Execute all tests tagged `@Unit` and `@Feature:xText` within the `src/text/` module and any other modules directly impacted by the changes in the PR.
*   **Nightly Builds:** Run all tests tagged `@Unit` and `@Regression` across the entire `ConsmicLexicon.Foundation` project.
*   **Pre-Release Builds:** Execute all tests (all `@Unit`, `@Integration`, and `@Regression` tests) and a representative set of high-level acceptance tests (e.g., those verifying core user flows).

## 5. Detailed Test Cases for ConsmicLexicon.Foundation.Formats Module

This section details specific test cases for key functions and classes within `src/text/src/xString/`, outlining the AI Verifiable End Result from `PRDMasterPlan.md` they target, the interactions to test, required mock configurations, precise observable outcomes, and recursive testing scope guidance.

---

### 5.1. `StringHelpers.FormatWithFormatter`

**Targeted AI Verifiable End Result (from `PRDMasterPlan.md`):** `Phase 4, Task 4.1: Test coverage is expanded, and a script can verify that the new tests cover the previously identified gaps.` This test case specifically verifies the correct formatting behavior when an `IStringFormatter` collaborator is provided.

**Method Signature:** `public static string FormatWithFormatter(string? input, string format, IStringFormatter? formatter)`

**Test Case 5.1.1: Valid Input with Custom Formatter**

*   **Scenario:** Verify that `FormatWithFormatter` correctly uses a provided `IStringFormatter` to format a string with a valid format pattern.
*   **Interactions to Test on Unit (`StringHelpers.FormatWithFormatter`):**
    *   The unit should call the `Format` method on the provided `formatter` collaborator.
*   **Collaborators to Mock:** `IStringFormatter`
*   **Mock Configuration:**
    *   Create a mock `IStringFormatter` instance.
    *   Configure the mock's `Format(string? input, string formatPattern)` method to return a predefined formatted string (e.g., `mockFormatter.Setup(f => f.Format(It.IsAny<string>(), It.IsAny<string>())).Returns("MOCKED_FORMATTED_STRING");`).
*   **Test Data:**
    *   `input`: "Hello {0}"
    *   `format`: "WORLD"
*   **Precise Observable Outcome (AI Verifiable):**
    *   The return value of `StringHelpers.FormatWithFormatter` is equal to "MOCKED_FORMATTED_STRING".
    *   The `Format` method on the mocked `IStringFormatter` was called exactly once with the expected `input` ("Hello {0}") and `format` ("WORLD").
*   **Inclusion in Recursive Testing Scopes:** `@Unit`, `@Regression`, `@Feature:xText`

**Test Case 5.1.2: Null Input with Custom Formatter**

*   **Scenario:** Verify `FormatWithFormatter` handles null input gracefully when a custom formatter is provided.
*   **Interactions to Test on Unit (`StringHelpers.FormatWithFormatter`):**
    *   The unit should still attempt to call the `Format` method on the provided `formatter` with a null input string.
*   **Collaborators to Mock:** `IStringFormatter`
*   **Mock Configuration:**
    *   Create a mock `IStringFormatter` instance.
    *   Configure the mock's `Format(string? input, string formatPattern)` method to return a predefined string or null when null input is passed (e.g., `mockFormatter.Setup(f => f.Format(null, It.IsAny<string>())).Returns("NULL_INPUT_HANDLED");`).
*   **Test Data:**
    *   `input`: `null`
    *   `format`: "DEFAULT"
*   **Precise Observable Outcome (AI Verifiable):**
    *   The return value of `StringHelpers.FormatWithFormatter` is equal to "NULL_INPUT_HANDLED".
    *   The `Format` method on the mocked `IStringFormatter` was called exactly once with `null` as the input string and the expected `format` ("DEFAULT").
*   **Inclusion in Recursive Testing Scopes:** `@Unit`, `@Regression`, `@Feature:xText`

**Test Case 5.1.3: Null Formatter Provided**

*   **Scenario:** Verify `FormatWithFormatter` handles cases where a null `formatter` is provided, expecting it to revert to default string formatting behavior (likely `string.Format`).
*   **Interactions to Test on Unit (`StringHelpers.FormatWithFormatter`):**
    *   The unit should *not* attempt to call any methods on a null `formatter`.
*   **Collaborators to Mock:** None (as the formatter is null)
*   **Test Data:**
    *   `input`: "Value {0}"
    *   `format`: "123"
    *   `formatter`: `null`
*   **Precise Observable Outcome (AI Verifiable):**
    *   The return value of `StringHelpers.FormatWithFormatter` is equal to "Value 123".
    *   No exceptions are thrown due to the null formatter.
*   **Inclusion in Recursive Testing Scopes:** `@Unit`, `@Regression`, `@Feature:xText`

---

### 5.2. `ByteArrayHelpers.ToString(byte[] input, Encoding? encodingUsing, int index, int count)`

**Targeted AI Verifiable End Result (from `PRDMasterPlan.md`):** `Phase 4, Task 4.1: Test coverage is expanded, and a script can verify that the new tests cover the previously identified gaps.` This test case verifies correct string conversion using a specified encoding.

**Method Signature:** `public static string ToString(byte[] input, Encoding? encodingUsing, int index = 0, int count = -1)`

**Test Case 5.2.1: Valid UTF8 Encoding Conversion**

*   **Scenario:** Verify `ToString` correctly converts a byte array to a string using UTF8 encoding.
*   **Interactions to Test on Unit (`ByteArrayHelpers.ToString`):**
    *   The unit should call the `GetString` method on the provided `Encoding` collaborator.
*   **Collaborators to Mock:** `System.Text.Encoding` (or a specific implementation like `UTF8Encoding`)
*   **Mock Configuration:**
    *   Create a mock `UTF8Encoding` instance.
    *   Configure the mock's `GetString(byte[] bytes, int index, int count)` method to return a predefined string (e.g., `mockEncoding.Setup(e => e.GetString(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>())).Returns("MOCKED_DECODED_STRING");`).
*   **Test Data:**
    *   `input`: `new byte[] { 72, 101, 108, 108, 111 }` (ASCII for "Hello")
    *   `encodingUsing`: `Encoding.UTF8` (mocked)
    *   `index`: 0
    *   `count`: -1
*   **Precise Observable Outcome (AI Verifiable):**
    *   The return value of `ByteArrayHelpers.ToString` is equal to "MOCKED_DECODED_STRING".
    *   The `GetString` method on the mocked `Encoding` was called exactly once with the provided byte array, index, and count.
*   **Inclusion in Recursive Testing Scopes:** `@Unit`, `@Regression`, `@Feature:xText`

**Test Case 5.2.2: Null Encoding Provided (Default Behavior)**

*   **Scenario:** Verify `ToString` uses `Encoding.Default` when a null `encodingUsing` is provided.
*   **Interactions to Test on Unit (`ByteArrayHelpers.ToString`):**
    *   The unit should internally use `Encoding.Default.GetString()`. We cannot mock a static property directly, but we can verify the outcome, assuming `Encoding.Default` behaves as expected.
*   **Collaborators to Mock:** None, as we are testing the default behavior of the static method.
*   **Test Data:**
    *   `input`: `new byte[] { 72, 101, 108, 108, 111 }`
    *   `encodingUsing`: `null`
    *   `index`: 0
    *   `count`: -1
*   **Precise Observable Outcome (AI Verifiable):**
    *   The return value of `ByteArrayHelpers.ToString` is equal to `Encoding.Default.GetString(new byte[] { 72, 101, 108, 108, 111 })`.
*   **Inclusion in Recursive Testing Scopes:** `@Unit`, `@Regression`, `@Feature:xText`

---

### 5.3. `StringExtension.TruncateWithEllipsis`

**Targeted AI Verifiable End Result (from `PRDMasterPlan.md`):** `Phase 4, Task 4.1: Test coverage is expanded, and a script can verify that the new tests cover the previously identified gaps.` This test case verifies correct string truncation with ellipsis.

**Method Signature:** `public static string TruncateWithEllipsis(this string str, int maxLength)`

**Test Case 5.3.1: String Shorter Than Max Length**

*   **Scenario:** Verify that `TruncateWithEllipsis` returns the original string unchanged if its length is less than or equal to `maxLength`.
*   **Interactions to Test on Unit:** None (pure function).
*   **Collaborators to Mock:** None.
*   **Test Data:**
    *   `str`: "Short string"
    *   `maxLength`: 20
*   **Precise Observable Outcome (AI Verifiable):**
    *   The return value of `TruncateWithEllipsis` is equal to "Short string".
*   **Inclusion in Recursive Testing Scopes:** `@Unit`, `@Regression`, `@Feature:xText`

**Test Case 5.3.2: String Longer Than Max Length**

*   **Scenario:** Verify that `TruncateWithEllipsis` correctly truncates a string longer than `maxLength` and appends "..."
*   **Interactions to Test on Unit:** None (pure function).
*   **Collaborators to Mock:** None.
*   **Test Data:**
    *   `str`: "This is a very long string that needs to be truncated."
    *   `maxLength`: 10
*   **Precise Observable Outcome (AI Verifiable):**
    *   The return value of `TruncateWithEllipsis` is equal to "This is...".
*   **Inclusion in Recursive Testing Scopes:** `@Unit`, `@Regression`, `@Feature:xText`

**Test Case 5.3.3: Max Length Too Small (Edge Case)**

*   **Scenario:** Verify that `TruncateWithEllipsis` handles `maxLength` values that are too small to even accommodate the ellipsis (e.g., less than 3).
*   **Interactions to Test on Unit:** None (pure function).
*   **Collaborators to Mock:** None.
*   **Test Data:**
    *   `str`: "Hello"
    *   `maxLength`: 2
*   **Precise Observable Outcome (AI Verifiable):**
    *   The return value of `TruncateWithEllipsis` is equal to "..". (Or a defined behavior for such edge cases, this needs to be confirmed by the actual implementation). Assuming it returns a string of `maxLength` with ellipsis.
*   **Inclusion in Recursive Testing Scopes:** `@Unit`, `@Regression`, `@Feature:xText`

---

### 5.4. `CharHelpers.IsLineBreakChar`

**Targeted AI Verifiable End Result (from `PRDMasterPlan.md`):** `Phase 4, Task 4.1: Test coverage is expanded, and a script can verify that the new tests cover the previously identified gaps.` This test case verifies the correct identification of line break characters.

**Method Signature:** `public static bool IsLineBreakChar(char c)`

**Test Case 5.4.1: Valid Line Break Characters**

*   **Scenario:** Verify `IsLineBreakChar` returns `true` for common line break characters.
*   **Interactions to Test on Unit:** None (pure function).
*   **Collaborators to Mock:** None.
*   **Test Data:**
    *   `c`: '\n' (newline)
    *   `c`: '\r' (carriage return)
*   **Precise Observable Outcome (AI Verifiable):**
    *   For `'\n'`, the return value of `IsLineBreakChar` is `true`.
    *   For `'\r'`, the return value of `IsLineBreakChar` is `true`.
*   **Inclusion in Recursive Testing Scopes:** `@Unit`, `@Regression`, `@Feature:xText`

**Test Case 5.4.2: Non-Line Break Characters**

*   **Scenario:** Verify `IsLineBreakChar` returns `false` for characters that are not line breaks.
*   **Interactions to Test on Unit:** None (pure function).
*   **Collaborators to Mock:** None.
*   **Test Data:**
    *   `c`: 'A'
    *   `c`: ' ' (space)
    *   `c`: '\t' (tab)
*   **Precise Observable Outcome (AI Verifiable):**
    *   For 'A', the return value of `IsLineBreakChar` is `false`.
    *   For ' ', the return value of `IsLineBreakChar` is `false`.
    *   For '\t', the return value of `IsLineBreakChar` is `false`.
*   **Inclusion in Recursive Testing Scopes:** `@Unit`, `@Regression`, `@Feature:xText`

---

## 6. Conclusion

This detailed granular test plan for the `ConsmicLexicon.Foundation.Formats` module, focusing on the `src/text/src/xString/` directory, has been meticulously crafted to ensure comprehensive test coverage and adherence to robust testing principles. By explicitly adopting London School TDD, we will verify the observable behaviors of units through their interactions with mocked collaborators, rather than their internal states. The comprehensive recursive testing strategy, with clearly defined triggers, prioritization, and test subset selection, will ensure that continuous regression testing is an integral part of the development lifecycle, catching issues early and maintaining system stability. Every test case and phase detailed herein includes AI verifiable completion criteria, directly aligning with the AI Verifiable End Results outlined in [`docs/PRDMasterPlan.md`](docs/PRDMasterPlan.md). This document serves as a clear and actionable guide for human programmers and subsequent AI testing agents, ensuring that the `ConsmicLexicon.Foundation.Formats` module is thoroughly tested and robust.