# 🎮 Overcooked 2 — DualShock 4 Bluetooth Fix

A BepInEx-based mod that fixes **DualShock 4 Bluetooth controller detection** in *Overcooked 2* on macOS.
ReadMe may seem like an "AI Slop" since i asked chatGPT to write it but code has been tested and run by real person

---

## ✅ What This Fixes

By default, *Overcooked 2* does **not recognize DualShock 4 controllers over Bluetooth**, due to how the game’s input system (using [InControl](https://github.com/pbhogan/InControl)) identifies controllers **only by Bluetooth device name** — not by vendor or product ID.

This mod forces the game to recognize DualShock controllers even over Bluetooth if their name contains `"DUALSHOCK"`.

---

## ⚠️ Important: Controller Name Must Include `"DUALSHOCK"`

To ensure detection:

1. Open **System Preferences → Bluetooth**
2. Rename your controller to include `DUALSHOCK` (case-insensitive)

   * Example: `DUALSHOCK Black`

If your controller name doesn’t include this keyword, the game will treat it as an unknown device.

---

## 🛠 Installation

### 1. Install the Mod

```bash
cd /Users/Shared/
curl -LO https://github.com/AlvinHV/Overcooked2DS4BT/releases/download/v1.0.0/Overcooked2Mod.zip
unzip Overcooked2Mod.zip
```

### 2. Set Steam Launch Options

In Steam:

1. Right-click **Overcooked 2** → **Properties**
2. In **Launch Options**, enter:

```
/Users/Shared/Overcooked2Mod/run_bepinex.sh %command%
```

---

## 🐞 Known Issues

* **D-Pad is not working properly.**

---

## 🔍 Development Notes

Originally suspected Unity’s IOKit-based input system was the issue. After reverse engineering (including hooking C++ functions and IOKit exports), I confirmed that:

* Unity **does** receive controller input properly via `IOHIDDevice`.
* The game uses the [InControl](https://github.com/pbhogan/InControl) C# library to manage controllers.
* InControl fails to recognize controllers **if their Bluetooth name doesn't match any predefined strings or regexes**.

This mod patches InControl’s logic to ensure the controller is not "unknown".
