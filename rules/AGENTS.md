# Global Agent Operational Architecture (Cross-Project Standard)

This document establishes the mandatory engineering standards, patterns, and protocols applied across all current and future projects, workspaces, and sessions. Derived from industry best practices (Microsoft Research TokenEconomizer, Aider Repo Map, Anthropic Multi-Agent Architecture, SWE-Bench harness methodology).

---

## 1. Loop Guard & Anti-Churn Protocol (TokenEconomizer / SWE-Bench)

When executing builds, scripts, commands, or unit tests:
* **Failure Count Limit (Max 2 Attempts):**
  * If a command or test fails with the same error twice, **STOP immediately**.
  * **Never** repeat the same command or make cosmetic micro-changes hoping it will pass on the 3rd attempt.
* **Root-Cause Diagnosis First:**
  * Before any second attempt, explicitly identify the root cause (e.g. missing dependency, path error, syntax/type mismatch).
  * If the second attempt fails, pause execution and ask the user for clarification or explain the blockage rather than burning steps and tokens in a loop.
* **Edit Verification:**
  * When editing files, verify changes cleanly. Do not repeatedly overwrite the same contiguous block in tiny exploratory cycles.

---

## 2. Aider-Style AST / Repo Map Navigation (Context Compactness)

Avoid dumping full source files into context:
* **Signature & Contract Inspection First:**
  * When exploring unknown code or dependencies, inspect only class definitions, public interfaces, and method signatures using targeted line ranges (`StartLine`/`EndLine`).
  * Never read entire 500+ line implementation files if only the API contract or data model is required.
* **Respect Repository Conventions:**
  * Inspect existing configuration files (`Directory.Build.props`, `.slnx`, `pyproject.toml`, `package.json`, `Cargo.toml`) before assuming tooling commands.
  * Use idiomatic project build and test runners (`dotnet test`, `python -m pytest`, `npm test`, `cargo test`).
* **Output Hygiene:**
  * Suppress voluminous repetitive command logs. Extract and report only actionable compiler/test errors.

---

## 3. Test-Driven Agent Loop (TDD / Reproducer Cycle)

When fixing bugs, implementing complex business logic, or refactoring:
1. **Reproduce First:**
   * Create or locate an automated test that isolates the bug or verifies the expected behavior before making code edits.
2. **Minimal Differential Edit:**
   * Apply the smallest atomic patch necessary to satisfy the contract.
3. **Verify:**
   * Run the targeted test to confirm resolution without side effects.

---

## 4. Multi-Agent Delegation & Tool Discipline (Anthropic Patterns)

* **Tool Discipline (Tool Pruning):**
  * Do not call speculative or unrelated tools (e.g., Figma / design tools during backend work; external API searches when local codebase facts suffice).
* **Research Subagent Delegation:**
  * For broad explorations across hundreds of files or extensive external documentation, delegate to read-only subagents (`research`) to preserve the primary context window.
* **Atomic Summaries:**
  * Keep communication compact, focused on diffs, concrete diagnostics, and verified test results.
