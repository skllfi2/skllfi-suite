---
name: local-ai-runtimes
description: Setup, configuration, optimization, and benchmarking of local LLM runtimes (GGUF, 1-bit/ternary quantization, llama.cpp, OpenWebUI, Speculative Decoding, KV/Prompt caching) on Windows with CUDA. Use PROACTIVELY when configuring local model servers, benchmarking inference speeds, or integrating Python/PowerShell agent harnesses with local AI engines.
metadata:
  model: inherit
---

# Local AI Runtimes & Model Optimization

Engineering standards and workflows for deploying, benchmarking, and integrating high-performance local AI runtimes on Windows + CUDA workstations.

## When to use

- Deploying and configuring local model runners (llama.cpp, GGUF runtimes, vLLM/OpenWebUI).
- Implementing ultra-low bit quantization (1-bit, Ternary, AWQ, EXL2).
- Configuring inference optimizations: Prompt Caching, KV-cache quantization, Speculative Decoding.
- Building automation scripts (`setup.ps1`, `uv` dependency isolation) for AI runtimes.

---

## 1. Runtime Architecture & Cache Management

To preserve SSD life and system drive capacity on Windows:
- **Relocate uv & Hugging Face caches:** Always set `UV_CACHE_DIR` and `HF_HOME` to dedicated drives (e.g., `E:\AI-Apps\uv-cache`, `E:\AI-Models`).
- **Virtual Environment Isolation:** Use `uv venv` with exact Python versions (3.11/3.12 recommended for CUDA/Torch compatibility).

```powershell
$env:UV_CACHE_DIR = "E:\AI-Apps\uv-cache"
$env:HF_HOME = "E:\AI-Models\hf-cache"
uv pip install -r requirements.txt
```

---

## 2. Low-Bit & Extreme Quantization Workflow

When working with Ternary and 1-bit models (e.g. Bonsai architectures):
1. **CUDA Kernel Alignment:** Verify `torch.cuda.is_available()` and compute capability before loading custom GEMM/GEMV bit-packing kernels.
2. **Speculative Decoding Pairing:** Pair ultra-low-bit models (as fast draft models) with larger verification models (e.g. 8B draft + 27B/70B verifier) to achieve high generation speed with zero accuracy loss.
3. **KV-Cache Strategies:** For long-context sessions, configure 8-bit or 4-bit KV caching (`--cache-type-k q8_0 --cache-type-v q8_0`) to prevent out-of-memory errors on consumer VRAM.

---

## 3. OpenWebUI & Agent Harness Integration

- **Port & Host Conventions:** Ensure local endpoints bind explicitly to `127.0.0.1` unless cross-network access is required.
- **Tools & Function Calling:** When serving models to agent frameworks, verify that chat templates support OpenAI-compatible tool calling schemas (`tools`, `tool_choice`).
- **Prompt Caching Verification:** Ensure system prompts (rules, skills) are static at the head of the context to maximize prompt cache hits and minimize TTFT (Time-To-First-Token).
