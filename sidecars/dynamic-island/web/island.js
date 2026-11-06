document.addEventListener("DOMContentLoaded", () => {
  const island = document.getElementById("fluent-island");
  const compactBar = document.getElementById("compact-bar");
  const dragHandle = document.getElementById("drag-handle");
  const btnQuickSettings = document.getElementById("btn-quick-settings");
  const statusDot = document.getElementById("status-dot");
  const statusLabel = document.getElementById("status-label");
  const guardChip = document.getElementById("guard-chip");
  const consoleStream = document.getElementById("console-stream");
  const pivotItems = document.querySelectorAll(".pivot-item");
  const viewPages = document.querySelectorAll(".view-page");
  
  // Settings controls
  const presetBtns = document.querySelectorAll(".preset-btn");
  const opacitySlider = document.getElementById("opacity-slider");
  const opacityVal = document.getElementById("opacity-val");
  const topmostToggle = document.getElementById("topmost-toggle");
  const btnCloseApp = document.getElementById("btn-close-app");

  let isExpanded = false;
  let guardCount = 0;

  // Send message to C# host
  function sendHost(msg) {
    if (window.chrome && window.chrome.webview) {
      window.chrome.webview.postMessage(msg);
    }
  }

  // Toggle Flyout
  compactBar.addEventListener("click", (e) => {
    // Don't toggle if clicking gear or grip
    if (e.target.closest("#btn-quick-settings") || e.target.closest(".grip-icon")) return;
    isExpanded = !isExpanded;
    syncState();
  });

  // Direct Settings button
  btnQuickSettings.addEventListener("click", (e) => {
    e.stopPropagation();
    isExpanded = true;
    syncState();
    // Switch to settings tab
    const settingsTab = document.querySelector('[data-page="page-settings"]');
    settingsTab?.click();
  });

  document.addEventListener("click", (e) => {
    if (isExpanded && !island.contains(e.target)) {
      isExpanded = false;
      syncState();
    }
  });

  function syncState() {
    if (isExpanded) {
      island.classList.remove("is-compact");
      island.classList.add("is-expanded");
    } else {
      island.classList.remove("is-expanded");
      island.classList.add("is-compact");
    }
  }

  // Tab switching
  pivotItems.forEach(item => {
    item.addEventListener("click", (e) => {
      e.stopPropagation();
      pivotItems.forEach(i => i.classList.remove("active"));
      viewPages.forEach(p => p.classList.remove("active"));

      item.classList.add("active");
      const targetPage = item.getAttribute("data-page");
      document.getElementById(targetPage)?.classList.add("active");
    });
  });

  // Position Presets
  presetBtns.forEach(btn => {
    btn.addEventListener("click", (e) => {
      e.stopPropagation();
      presetBtns.forEach(b => b.classList.remove("active"));
      btn.classList.add("active");
      const preset = btn.getAttribute("data-preset");
      sendHost({ action: "set_preset", preset: preset });
      appendLog(`Docking shifted to: ${btn.innerText}`);
    });
  });

  // Opacity Slider
  opacitySlider?.addEventListener("input", (e) => {
    const val = e.target.value;
    opacityVal.innerText = `${val}%`;
    sendHost({ action: "set_opacity", value: val / 100.0 });
  });

  // Topmost Toggle
  topmostToggle?.addEventListener("change", (e) => {
    sendHost({ action: "set_topmost", value: e.target.checked });
  });

  // Close App
  btnCloseApp?.addEventListener("click", (e) => {
    e.stopPropagation();
    sendHost({ action: "close_app" });
  });

  // Action Buttons
  const btnSim = document.getElementById("btn-simulate");
  const btnFail = document.getElementById("btn-fail");

  btnSim?.addEventListener("click", (e) => {
    e.stopPropagation();
    statusDot.style.background = "#60cdff";
    statusLabel.innerText = "Verifying...";
    appendLog("▶ Running AST check & Roslyn build...");

    setTimeout(() => {
      statusDot.style.background = "#6ccb5f";
      statusLabel.innerText = "Antigravity";
      appendLog("✔ Build succeeded cleanly (0 warnings).");
    }, 1100);
  });

  btnFail?.addEventListener("click", (e) => {
    e.stopPropagation();
    guardCount++;
    if (guardCount === 1) {
      guardChip.innerText = "Guard: 1/2";
      guardChip.style.color = "#ffaa44";
      guardChip.style.background = "rgba(255, 170, 68, 0.15)";
      appendLog("⚠ [Attempt 1 Failed] Isolating root cause before retry.");
    } else {
      guardChip.innerText = "Guard: 2/2 STOP";
      guardChip.style.color = "#ff99a4";
      guardChip.style.background = "rgba(255, 153, 164, 0.2)";
      statusDot.style.background = "#ff99a4";
      statusLabel.innerText = "Guard Stopped";
      appendLog("⛔ [Anti-Churn Triggered] Max 2 attempts reached. Pausing loop.");
      guardCount = 0;
    }
  });

  function appendLog(line) {
    const d = new Date();
    const time = `${String(d.getHours()).padStart(2, '0')}:${String(d.getMinutes()).padStart(2, '0')}:${String(d.getSeconds()).padStart(2, '0')}`;
    const row = document.createElement("div");
    row.innerText = `[${time}] ${line}`;
    consoleStream.appendChild(row);
    consoleStream.scrollTop = consoleStream.scrollHeight;
  }
});
