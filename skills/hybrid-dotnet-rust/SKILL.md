---
name: hybrid-dotnet-rust
description: Architecture, FFI bridging, and development patterns for hybrid C# (.NET 9/10 / WinUI 3) and Rust (cdylib, windows-rs, csbindgen) desktop applications. Use PROACTIVELY when integrating native Rust performance/safety with .NET UI, Windows APIs, zero-copy memory mapping, or native system resource management.
metadata:
  model: inherit
---

# Hybrid C# (.NET) & Rust Architecture

Specialized engineering guide for combining modern .NET (WinUI 3, WPF, CLI) with native Rust modules using zero-overhead FFI bindings.

## When to use

- Interfacing C# with native Windows subsystems via `windows-rs` (Restart Manager, WinVerifyTrust, NTFS ADS, tokens, handles).
- Generating robust P/Invoke definitions automatically without manual fragile boilerplate.
- Implementing zero-copy inspection (`memmap2`, PE headers, Authenticode).
- Designing fallbacks: providing graceful degradation in C# when native binaries are absent or during isolated CI builds.

---

## 1. Core Architecture Pattern

```
┌────────────────────────────────────────────────────────┐
│ UI & High-Level Business Logic (.NET / C# / WinUI 3)  │
└──────────────────────────┬─────────────────────────────┘
                           │ (Safe C# API Calls)
┌──────────────────────────▼─────────────────────────────┐
│ C# Bridge Layer (NativeMethods.g.cs / Safe Handle)     │
│ - csbindgen auto-generated C-ABI P/Invoke signatures    │
│ - Managed Fallback implementation if DLL is missing    │
└──────────────────────────┬─────────────────────────────┘
                           │ (cdylib C-ABI FFI)
┌──────────────────────────▼─────────────────────────────┐
│ Native Rust Engine (cdylib crate)                      │
│ - windows-rs (Safe Win32 wrappers, RAII HANDLEs)       │
│ - memmap2 (Zero-copy file mapping)                     │
│ - Mutex/Atomic synchronization                         │
└────────────────────────────────────────────────────────┘
```

---

## 2. Rust Crate Configuration (`Cargo.toml`)

Ensure the crate emits a dynamic C library:

```toml
[package]
name = "app_native"
version = "0.1.0"
edition = "2024"

[lib]
crate-type = ["cdylib"]

[dependencies]
windows = { version = "0.58", features = [
    "Win32_Foundation",
    "Win32_System_RestartManager",
    "Win32_Security_Cryptography",
    "Win32_Storage_FileSystem",
] }
memmap2 = "0.9"

[build-dependencies]
csbindgen = "1.9"
```

---

## 3. Automatic FFI Generation with `csbindgen` (`build.rs`)

Generate C# P/Invoke classes directly during `cargo build`:

```rust
fn main() {
    // Generate C# bindings into the .NET project directory
    csbindgen::Builder::default()
        .input_extern_file("src/lib.rs")
        .csharp_namespace("App.Infrastructure.Native")
        .csharp_class_name("NativeMethods")
        .csharp_dll_name("app_native")
        .csharp_use_function_pointer(false)
        .generate_csharp_file("../App.Infrastructure/Native/NativeMethods.g.cs")
        .unwrap();
}
```

---

## 4. Critical Rules & Guardrails

1. **Memory & Handle Safety:**
   - Always wrap raw OS handles (`HANDLE`, `HKEY`, `SC_HANDLE`) in Rust types implementing `Drop` (or `windows::core::Owned`).
   - Never pass raw non-pinned managed pointers into background Rust threads.
   - For string exchange, prefer UTF-16 pointer + length (`*const u16, usize`) to match C# `string` without conversion penalties.

2. **Error Propagation Protocol:**
   - Functions returning status must return an explicit `int32` error code (e.g. `0` = OK, `Win32 Error Code` or custom enum).
   - Alternatively, pass an output error buffer for detailed diagnostic messages.

3. **Managed Fallback Rule:**
   - In C#, always test `NativeLibrary.TryLoad("app_native")` or encapsulate calls in an adapter interface (`INativeOperations`).
   - If loading fails, fallback to managed logic or surface a clear environment diagnostic.
