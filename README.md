[center]
[size=6][b]Crimson Desert — Inventory Expander[/b][/size]
[size=3][color=#5AA0F0]A simple GUI tool to expand your inventory and private storage slots[/color][/size][/center]

[b][i][u][center][size=3]For now, each new update need to be verified by NexusMod staff. I will work on that for the next version to avoid this. So sorry everyone.
[/size][/center][/u][/i][/b]

[size=4][b][color=#C8A03C]⚙ WHAT IT DOES[/color][/b][/size]

This tool patches values inside Crimson Desert's [b]0.paz[/b] file to increase both the number of [b]character inventory slots[/b] and [b]private storage (camp chest) slots[/b]. The vanilla game ships with [b]50 starting slots / 300 maximum[/b] for inventory, and [b]999 slots[/b] for your camp's private storage — this tool lets you raise both independently using safe, tested presets.

Rather than relying on hardcoded file offsets (which break with every game update), the tool dynamically scans the [b]0.paz[/b] file for the target signatures at runtime — so it will continue to work even after Pearl Abyss updates the game.

[b][color=#FF6B6B]⚠ This tool is compatible with the Steam version of Crimson Desert only.[/color][/b] Other distributions (Epic Games Store, console, etc.) are not supported and have not been tested.



[size=4][b][color=#C8A03C]📦 PRESET OPTIONS[/color][/b][/size]

[b]Inventory[/b] — use the [b]Modify[/b] checkbox to enable or skip patching this section:
[list]
[*][b]Vanilla[/b] — 50 start / 240 max (original game values, use to restore)
[*][b]Starter[/b] — 100 start / 240 max
[*][b]Adventurer[/b] — 150 start / 240 max
[*][b]Veteran[/b] — 200 start / 240 max
[*][b]Warload[/b]— 250 start / 300 max
[/list]

[b]Private Storage (camp chest)[/b] — use the [b]Modify[/b] checkbox to enable or skip patching this section:
[list]
[*][b]Vanilla[/b] — 240 slots (original game values, use to restore)
[*][b]Extended[/b] — 300 slots
[*][b]Large[/b] — 400 slots
[*][b]Massive[/b] — 500 slots
[*][b]Huge[/b] — 700 slots
[*][b]Insane[/b] — 800 slots
[*][b]Extreme[/b] — 900 slots
[*][b]Max[/b] — 999 slots
[/list]

[color=#FF6B6B]Custom values have been intentionally removed to prevent game crashes. All available presets have been tested for stability.[/color]



[size=4][b][color=#C8A03C]💾 BACKUP SYSTEM[/color][/b][/size]

Before applying any patch, the tool automatically creates a backup of your original [b]0.paz[/b] file, saved as [b]0.paz.backup[/b] in the same folder as the original file. You can restore it at any time using the [b]Restore Backup[/b] button.

The tool also detects when the game has been updated (by comparing file sizes). If a new game version is detected, the outdated backup is automatically replaced with a fresh one before patching.



[size=4][b][color=#C8A03C]⚡ ONE-CLICK RE-APPLY[/color][/b][/size]

Every time you apply a patch, your settings are saved in a configuration file located at:

[code]%AppData%\CD-InventoryExpander\config.json[/code]

On your next launch, the tool will display a status banner at the top showing your last applied patch — inventory and storage values included. If no patch has been saved yet, the banner will appear in orange as a reminder. Once a patch is saved, the banner turns green and a [b]Re-apply[/b] button lets you restore your settings in a single click — no need to re-select anything.



[size=4][b][color=#FF6B6B]⚠ IMPORTANT — STEAM UPDATES[/color][/b][/size]

[b]Every Steam update will overwrite the patched 0.paz file[/b] and revert your inventory and storage back to vanilla values. Any extra slots beyond the vanilla maximum may appear empty until you re-patch.

[b]Recommended routine after any Steam update:[/b]
[list=1]
[*]Launch [b]Crimson Desert Inventory Expander[/b] first
[*]Click [b]Re-apply[/b] in the green banner at the top
[*][b]Then[/b] launch the game
[/list]

The status banner always reflects the current state — use it as a quick visual check before launching the game.



[size=4][b][color=#C8A03C]🌐 MULTILINGUAL[/color][/b][/size]

The interface is fully available in four languages. Use the toggle buttons in the top-right corner to switch at any time:
[list]
[*][b]EN[/b] — English (default)
[*][b]FR[/b] — French / Français
[*][b]ES[/b] — Spanish / Español
[*][b]中文[/b] — Chinese / 中文
[/list]



[size=4][b][color=#C8A03C]🚀 HOW TO USE[/color][/b][/size]

[b]Step 1 — Launch the tool[/b]

Double-click [b]CD-InventoryExpander.exe[/b]. No installation required — the exe runs standalone from any folder.

On first launch, the tool will automatically scan your drives for a valid Steam installation of Crimson Desert. If found, the install path will be pre-filled. If not, you can enter it manually or use the [b]Browse[/b] / [b]Detect[/b] buttons.

[b]Step 2 — Verify the install path[/b]

Check that the [b]Install Path[/b] field points to your Crimson Desert folder, for example:
[code]C:\Program Files (x86)\Steam\steamapps\common\Crimson Desert[/code]
The tool looks for the file [b]0008\0.paz[/b] inside that folder to confirm the path is valid.

[b]Step 3 — Check current values[/b]

The [b]Current Values[/b] section shows what is currently stored in your [b]0.paz[/b] file:
[list]
[*][b]Starting Slots[/b] — how many inventory slots your character starts with
[*][b]Maximum Slots[/b] — the hard cap the game will allow for inventory
[*][b]Private Storage Slots[/b] — current size of your camp chest
[*][b]Backup[/b] — whether a backup of the original file exists
[/list]

[b]Step 4 — Select presets[/b]

The preset panel is split into two independent columns:
[list]
[*][b]Left column — Inventory[/b]: controls your character's bag slots
[*][b]Right column — Private Storage[/b]: controls your camp chest slots
[/list]
Use the [b]Modify[/b] checkbox at the top of each column to enable or disable patching that section. This lets you update only inventory, only storage, or both at once.

[b]Step 5 — Apply the patch[/b]

Click [b]Apply Patch[/b]. A confirmation dialog will show the exact values about to be written. Click [b]Yes[/b] to confirm.

The tool will:
[list=1]
[*]Create a backup of [b]0.paz[/b] as [b]0.paz.backup[/b] (if the option is checked and no valid backup exists)
[*]Write the new slot values at the correct locations in the file
[*]Verify the written values by re-reading the file
[*]Save your settings to [b]%AppData%\CD-InventoryExpander\config.json[/b] for future one-click re-apply
[/list]

[b]Step 6 — Launch the game[/b]

Start Crimson Desert normally through Steam. Your expanded inventory and storage will be active immediately.



[size=4][b][color=#C8A03C]📁 FILE LOCATIONS[/color][/b][/size]

[list]
[*][b]Config file[/b] (last saved patch): [font=Courier New]%AppData%\CD-InventoryExpander\config.json[/font]
[*][b]Game backup[/b]: [font=Courier New]<Steam>\steamapps\common\Crimson Desert\0008\0.paz.backup[/font]
[*][b]Patched file[/b]: [font=Courier New]<Steam>\steamapps\common\Crimson Desert\0008\0.paz[/font]
[/list]

To fully uninstall:
[list=1]
[*]Rename or delete [b]0.paz[/b]
[*]Rename [b]0.paz.backup[/b] to [b]0.paz[/b]
[*]Optionally delete [b]%AppData%\CD-InventoryExpander\[/b] to remove the saved config
[/list]

Alternatively, use the [b]Restore Backup[/b] button directly in the tool, which handles steps 1 and 2 automatically.



[size=4][b][color=#C8A03C]🙏 CREDITS[/color][/b][/size]

The original concept and offset research behind this tool is based on [b]kindiboy[/b]'s mod: [b]Inventory Expander [/b]and on yukentseeme's mod: Inventory and Private Storage Expander - Json Mod Manager
This GUI is a personal reimplementation built on top of that work. Full credit goes to kindiboy and yukentseeme for the initial discovery and the dynamic scan approach.



[size=4][b][color=#C8A03C]📋 DISCLAIMER & SUPPORT[/color][/b][/size]

This tool is provided [b]as-is[/b], without any warranty or guarantee of future updates. It may break after game patches if Pearl Abyss significantly changes the file structure. That said, suggestions and feedback are always welcome — feel free to leave a comment and I'll do my best to look into it.

[b]Compatibility:[/b] Steam version of Crimson Desert only. The tool auto-detects your Steam library path on launch.


[size=2][color=#666666]Not affiliated with Pearl Abyss. Steam version only. Use at your own risk. Always keep a backup.[/color][/size]
