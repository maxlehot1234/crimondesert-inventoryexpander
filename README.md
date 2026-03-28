# Crimson Desert — Inventory Expander
**A simple GUI tool to expand your inventory and private storage slots**

> ⚠️ **Steam version only.** Epic Games Store, console, and other distributions are not supported.

## Download
👉 **[Latest release](https://github.com/maxlehot1234/crimondesert-inventoryexpander/releases/latest)**

Or here if you want to support me on Nexus Mods: https://www.nexusmods.com/crimsondesert/mods/67

---

## What it does

Patches values inside Crimson Desert's `0.paz` file to increase both **character inventory slots** and **private storage (camp chest) slots** independently. The vanilla game ships with 50 starting / 240 maximum for inventory, and 240 slots for private storage.

Rather than relying on hardcoded file offsets (which break with every game update), the tool **dynamically scans** the `0.paz` file for the target signatures at runtime — so it continues to work after Pearl Abyss updates the game.

---
## ℹ️ How the slot limit works

The value you set acts as a hard cap — the game still counts bag purchases in the background, but your displayed slot count will never exceed your configured maximum.

**Example:** if your max is set to 200 and you buy a bag that would normally give you slot 91, your inventory stays at 200. However, that purchase is still tracked internally. If you later restore vanilla values, you won't see 90 slots — you'll see 91, because that bag was counted all along.

> 💡 **Removing the patch does not reset your progress** — it just lifts the cap and reveals the slots you already earned.

---

## Presets

### Inventory
| Preset | Start | Max |
|---|---|---|
| Vanilla | 50 | 240 |
| Starter | 100 | 240 |
| Adventurer | 150 | 240 |
| Veteran | 200 | 240 |
| Warlord | 250 | 300 |

### Private Storage (camp chest)
| Preset | Slots |
|---|---|
| Vanilla | 240 |
| Extended | 300 |
| Large | 400 |
| Massive | 500 |
| Huge | 700 |
| Insane | 800 |
| Extreme | 900 |
| Max | 999 |

Use the **Modify** checkbox at the top of each column to patch one section, the other, or both at once.

> Custom values have been intentionally removed to prevent game crashes. All listed presets have been tested for stability.

---

## Backup system

Before patching, the tool creates a backup of your original `0.paz` as `0.paz.backup` in the same folder. Restore it at any time with the **Restore Backup** button. If a game update is detected (file size change), the outdated backup is automatically replaced.

---

## One-click re-apply

Every patch saves your settings to:
```
%AppData%\CD-InventoryExpander\config.json
```
On next launch, a banner shows your last applied patch. Click **Re-apply** to restore it in one click — no need to re-select anything.

> ⚠️ Every Steam update overwrites the patched `0.paz`. Always re-apply before launching the game after an update.

---

## How to build

Requires [.NET 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48) and the [.NET SDK](https://dotnet.microsoft.com/download).

```powershell
git clone https://github.com/maxlehot1234/crimondesert-inventoryexpander.git
cd crimondesert-inventoryexpander
dotnet build -c Release
```

The compiled exe will be in the `bin/` folder.

---

## Languages

English · Français · Español · 中文

---

## Credits

The original concept and offset research behind this tool is based on kindiboy's mod: Inventory Expander and on yukentseeme's mod: Inventory and Private Storage Expander - Json Mod Manager
This GUI is a personal reimplementation built on top of that work. Full credit goes to kindiboy and yukentseeme for the initial discovery and the dynamic scan approach.

---

## Disclaimer

Provided as-is, without warranty. May break after major game patches if Pearl Abyss changes the file structure significantly. Steam version only. Use at your own risk. Always keep a backup.

© 2026 — All Rights Reserved
