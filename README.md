# skllfi-suite: Personal Antigravity Agent & Engineering Suite

A customized, production-grade plugin suite for **Google Antigravity / Gemini Agentic Workflow**.

`skllfi-suite` standardizes agent behavior, enforces operational discipline (Loop Guard, AST navigation, TDD reproducer loops), and provides battle-tested engineering skills tailored for modern desktop, AI runtime, and low-level Windows systems development.

---

## 🏛️ Core Operational Rules (`rules/AGENTS.md`)

All agent interactions automatically inherit mandatory engineering guardrails:
1. **Loop Guard & Anti-Churn Protocol (TokenEconomizer / SWE-Bench):**
   - Maximum 2 failed attempts per command/test. Immediate halt and root-cause analysis on failure. No cosmetic retry loops.
2. **Aider-Style AST / Repo Map Navigation:**
   - Inspection strictly via interfaces, types, and targeted line ranges. Full-file dumping is prohibited.
3. **Test-Driven Reproducer Cycle (TDD):**
   - Reproduce bug with a test first $\rightarrow$ apply minimal differential patch $\rightarrow$ verify with isolated test runner.
4. **Tool Discipline & Context Hygeine:**
   - Context preservation, no speculative tool invocations, atomic summaries.

---

## 🛠️ Specialized Skills Catalog (`skills/`)

### 1. System & Native Architecture
* **`hybrid-dotnet-rust`**: Architecture and FFI bridge patterns for hybrid **C# (.NET 10 / WinUI 3)** and **Rust** (`windows-rs`, `csbindgen`, C-ABI cdylib). Zero-copy memory mapping (`memmap2`), Restart Manager API, Authenticode inspection, and managed fallbacks.
* **`csharp-pro`**: Advanced modern C# (.NET 9/10), nullable reference types, pattern matching, TPL concurrency, high-performance memory patterns.
* **`rust-slint-pro`**: Native desktop UI development with Rust, Slint, Windows Fluent Design, and direct Win32 integration.

### 2. AI Inference & Local Runtimes
* **`local-ai-runtimes`**: Windows + CUDA setup and optimization for local LLM engines (llama.cpp, GGUF, 1-bit / ternary quantization, Bonsai, OpenWebUI). Speculative decoding (draft + verifier), KV-cache quantization, and prompt cache hit rate maximization.
* **`python-pro`**: Modern Python 3.12+, `uv`, `ruff`, async architecture, typed pipelines.

### 3. Engineering Documentation & Security
* **`gost-spec-architect`**: Formal Technical Specifications (ТЗ) adhering to ГОСТ 34.602-2020, ГОСТ 19.201-78, and ISO/IEC/IEEE 29148. Multi-echelon defense architectures, cross-layer entanglement, and quantitative security metrics (CTBR).
* **`security-audit`**: Deep vulnerability research, attack-class hunting, and verification across IPC, memory safety, protocols, and supply chains.

### 4. Quality, Design & Prototyping
* **`vibe-dev`**: Rapid interactive UI/vibe prototyping and web mockups.
* **`hallmark`**: Distinctive, artisanal web styling and creative frontends.
* **`impeccable`**: Polished interface reviews, typography, UX hardening, and design system alignment.
* **`unit-testing-test-generate`**: Comprehensive unit test generation across multiple languages.
* **`strip-ai-tells`**: Stylistic editing to eliminate formulaic, synthetic AI writing patterns.
* **`project-memory` & `repo-roadmap`**: In-repo tracking of architecture decisions, invariants, and progress.

---

## 🚀 Sidecars & Native Extensions (`sidecars/`)

* **`dynamic-island`**: Native Windows floating status pill and companion UI (`DynamicIslandWinUI` / WPF) for real-time agent observability.

---

## 📦 Installation & Setup

Clone directly into the local Antigravity plugin directory:

```powershell
# Default Antigravity plugin path on Windows
cd $env:USERPROFILE\.gemini\config\plugins
git clone https://github.com/skllfi2/skllfi-suite.git
```

Verify in Antigravity or check installed plugins:
```powershell
agy plugin list
```
