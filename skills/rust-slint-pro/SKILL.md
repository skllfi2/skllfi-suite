---
name: rust-slint-pro
description: Expert Rust & Slint UI engineering. Designs lightweight, native, high-performance desktop GUI applications with Slint, Windows Fluent Design, Win32 API integration, frameless mica windows, multithreaded event-loops, and robust MSVC builds. Use PROACTIVELY for Rust desktop apps, Slint UI layout/styling, Win32 window management, and native system integration.
metadata:
  model: inherit
---

## Use this skill when

- Developing native desktop applications in Rust using the Slint UI framework.
- Designing modern Windows 11 Fluent Design / dark mode interfaces in Slint.
- Implementing custom frameless windows (`no-frame: true`), window dragging via Win32 non-client areas, and custom titlebars.
- Handling background threads, asynchronous operations, and dispatching UI updates via Slint's event loop.
- Structuring adaptive layouts, avoiding deprecated Slint properties, fixing scroll view autoscroll, and optimizing performance.
- Integrating Rust with Windows Service Control Manager (SCM), WinDivert, Win32 API (`windows` crate), and system tray.
- Organizing clean single-executable desktop architecture, `%LOCALAPPDATA%` isolation, and dynamic config parsers.

## Do not use this skill when

- The project uses web frontend technologies (Electron, Tauri, React/Vue) without Slint.
- The task is purely non-Rust backend/cloud infrastructure unrelated to desktop GUI or native Windows tooling.

---

## 1. Slint UI Architecture & Layout Best Practices

### Layout Rules & Stretch Hygiene
- **Avoid Vertical Dead Space**: When embedding terminals or log viewers inside dialogs or layouts, always set `vertical-stretch: 0` on headers, status bars, and progress bars. Give the main log/terminal container `vertical-stretch: 1; min-height: ...px;`.
- **Card Content Bounding**: Prevent horizontal card overflow by using vertical wrapping containers (`SettingsGroupCard`) rather than placing multiline explanation text alongside inline buttons in rigid `HorizontalLayout`s.

### Slint 1.8+ / 1.18+ Syntax Invariants
- **Scroll View Properties**: Never use deprecated `viewport-y` or `viewport-height`. Always use:
  - `content-y` (read/write position)
  - `content-height` (inner content height)
  - `visible-height` (viewport visible height)
- **Terminal Autoscroll Formula**:
  ```slint
  ScrollView {
      content-y: self.content-height > self.visible-height ? (self.visible-height - self.content-height) : 0px;
      // ... log lines ...
  }
  ```
- **Rectangle Borders**: Slint `Rectangle` does **not** have `border-bottom-width` or `border-right-width`. Only `border-width: 1px` is valid. For single-edge borders, place an explicit 1px height/width `Rectangle` separator.

### User Selection vs. Toggle Churn
- If a setting has 3+ discrete options (e.g. modes, themes, languages), **do not use a single cycling toggle button**.
- Provide a segmented group of explicit action buttons (`active: current_index == target_index`), allowing users to click their desired target directly in 1 click.

---

## 2. Frameless Windows & Win32 Integration (`windows` crate)

### Pure Frameless Fluent Window
Set `no-frame: true;` in Slint `MainWindow`:
```slint
export component MainWindow inherits Window {
    no-frame: true;
    background: Theme.window_bg;
    // ...
}
```

### Window Dragging via Win32 Non-Client Area
Provide an interactive touch area in Slint:
```slint
TouchArea {
    horizontal-stretch: 1;
    moved => { root.drag_window(); }
}
```

In Rust (`main.rs`), capture the mouse release and send `WM_NCLBUTTONDOWN`:
```rust
use windows::Win32::Foundation::{HWND, LPARAM, WPARAM};
use windows::Win32::UI::Input::KeyboardAndMouse::ReleaseCapture;
use windows::Win32::UI::WindowsAndMessaging::{
    EnumWindows, GetWindowThreadProcessId, SendMessageW, ShowWindow,
    HTCAPTION, SW_MAXIMIZE, SW_MINIMIZE, SW_RESTORE, WM_NCLBUTTONDOWN,
};

pub fn get_main_hwnd() -> Option<HWND> {
    let current_pid = std::process::id();
    let mut found_hwnd: Option<HWND> = None;
    unsafe {
        let _ = EnumWindows(
            Some(enum_window_callback),
            LPARAM(&mut found_hwnd as *mut _ as isize),
        );
    }
    found_hwnd
}

// In drag callback:
if let Some(wnd) = get_main_hwnd() {
    unsafe {
        let _ = ReleaseCapture();
        let _ = SendMessageW(
            wnd,
            WM_NCLBUTTONDOWN,
            Some(WPARAM(HTCAPTION as usize)), // Note: wrapped in Some() in windows-rs 0.62+
            Some(LPARAM(0)),
        );
    }
}
```

---

## 3. Multithreading & Slint Event Loop Integration

### Non-Blocking Background Operations
Never block Slint's GUI thread with network requests (`curl`, ping) or process spawning.
```rust
let mw_weak = main_window.as_weak();
std::thread::spawn(move || {
    // 1. Perform background compute / I/O
    let result = heavy_operation();

    // 2. Dispatch back to Slint GUI event loop
    let _ = mw_weak.upgrade_in_event_loop(move |w| {
        w.set_progress(1.0);
        w.set_status_text(result.into());
    });
});
```

### High-Speed Network Probing
Run protocol checks (HTTP, TLS, Ping) in parallel using `std::thread::scope`:
```rust
std::thread::scope(|s| {
    let t_http = s.spawn(|| check_http(&target, timeout));
    let t_tls12 = s.spawn(|| check_tls(&target, "1.2", timeout));
    let t_tls13 = s.spawn(|| check_tls(&target, "1.3", timeout));
    let t_ping = s.spawn(|| check_ping(&target, ping_timeout));

    let res_http = t_http.join().unwrap_or(false);
    // ...
});
```

---

## 4. Build Environment, Portability & AppData Hygiene

### MSVC Toolchain & Linking
When compiling on Windows with MSVC, always initialize the developer command prompt to prevent `LNK1104: cannot open msvcrt.lib`:
```cmd
cmd /c "`"C:\Program Files (x86)\Microsoft Visual Studio\18\BuildTools\VC\Auxiliary\Build\vcvars64.bat`" && cargo build --release"
```

### Application Identity & Storage Hygiene
- **App Naming Consistency**: Ensure application binary name (`<app>.exe`), window titles, user agents, and storage directories (`%LOCALAPPDATA%\<app>`) are strictly aligned. Never leave outdated prototype names in paths.
- **Directory Hierarchy**:
  1. `%LOCALAPPDATA%\<app>\engine\` — binaries, drivers (`WinDivert`), preset scripts.
  2. `%LOCALAPPDATA%\<app>\logs\` — application log files (`app.log`) with rotation.
  3. `%LOCALAPPDATA%\<app>\reports\` — user-facing diagnostic and benchmark reports.
  4. `%LOCALAPPDATA%\<app>\ui_settings.json` — user configuration.
- **Dynamic Configuration Discovery**:
  - Never hardcode script presets into Rust code.
  - Dynamically scan and parse configuration scripts (`.bat` / `.conf`) on the fly so upstream engine updates take effect automatically without recompilation.
- **Seamless Migration**:
  - When migrating paths (e.g. from an old prototype path to the final app name), automatically detect the old location, copy settings/data forward, and preserve user state.
