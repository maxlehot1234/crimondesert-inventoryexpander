using System.Collections.Generic;

namespace CrimsonDesertExpander
{
    public enum Language { EN, FR, ES, ZH }

    public static class Loc
    {
        public static Language Current = Language.EN;

        private static readonly Dictionary<string, Dictionary<Language, string>> _strings = new Dictionary<string, Dictionary<Language, string>>
        {
            ["enable_patch"] = new Dictionary<Language, string> {
                [Language.EN] = "Modify",
                [Language.FR] = "Modifier",
                [Language.ES] = "Modificar",
                [Language.ZH] = "修改"
            },
            ["err_nothing_selected"] = new Dictionary<Language, string> {
                [Language.EN] = "Please enable at least one section to patch.",
                [Language.FR] = "Activez au moins une section à patcher.",
                [Language.ES] = "Activa al menos una sección para parchear.",
                [Language.ZH] = "请至少启用一个要修补的部分。"
            },
            ["title"] = new Dictionary<Language, string> {
                [Language.EN] = "Crimson Desert — Inventory Expander",
                [Language.FR] = "Crimson Desert — Expanseur d'inventaire",
                [Language.ES] = "Crimson Desert — Expansor de Inventario",
                [Language.ZH] = "Crimson Desert — 背包扩展工具"
            },
            ["subtitle"] = new Dictionary<Language, string> {
                [Language.EN] = "Inventory Slot Modifier",
                [Language.FR] = "Modification des emplacements d'inventaire",
                [Language.ES] = "Modificador de Espacios de Inventario",
                [Language.ZH] = "背包栏位修改器"
            },
            ["game_path_label"] = new Dictionary<Language, string> {
                [Language.EN] = "Install Path",
                [Language.FR] = "Chemin d'installation",
                [Language.ES] = "Ruta de instalación",
                [Language.ZH] = "安装路径"
            },
            ["browse"] = new Dictionary<Language, string> {
                [Language.EN] = "Browse",
                [Language.FR] = "Parcourir",
                [Language.ES] = "Examinar",
                [Language.ZH] = "浏览"
            },
            ["detect"] = new Dictionary<Language, string> {
                [Language.EN] = "Detect",
                [Language.FR] = "Détecter",
                [Language.ES] = "Detectar",
                [Language.ZH] = "检测"
            },
            ["current_values"] = new Dictionary<Language, string> {
                [Language.EN] = "Current Values",
                [Language.FR] = "Valeurs actuelles",
                [Language.ES] = "Valores actuales",
                [Language.ZH] = "当前值"
            },
            ["starting_slots"] = new Dictionary<Language, string> {
                [Language.EN] = "Starting Slots",
                [Language.FR] = "Emplacements de départ",
                [Language.ES] = "Espacios iniciales",
                [Language.ZH] = "初始栏位"
            },
            ["max_slots"] = new Dictionary<Language, string> {
                [Language.EN] = "Maximum Slots",
                [Language.FR] = "Emplacements maximum",
                [Language.ES] = "Espacios máximos",
                [Language.ZH] = "最大栏位"
            },
            ["warehouse_slots"] = new Dictionary<Language, string> {
                [Language.EN] = "Private Storage Slots",
                [Language.FR] = "Emplacements coffre privé",
                [Language.ES] = "Espacios almacén privado",
                [Language.ZH] = "私人仓库栏位"
            },
            ["vanilla"] = new Dictionary<Language, string> {
                [Language.EN] = "vanilla",
                [Language.FR] = "vanilla",
                [Language.ES] = "original",
                [Language.ZH] = "原版"
            },
            ["preset_label"] = new Dictionary<Language, string> {
                [Language.EN] = "Preset",
                [Language.FR] = "Préréglage",
                [Language.ES] = "Preajuste",
                [Language.ZH] = "预设"
            },
            // ── Quick repatch banner ─────────────────────────────────────────
            ["quick_no_patch_title"] = new Dictionary<Language, string> {
                [Language.EN] = "⚠  NO SAVED PATCH",
                [Language.FR] = "⚠  AUCUN PATCH SAUVEGARDÉ",
                [Language.ES] = "⚠  SIN PARCHE GUARDADO",
                [Language.ZH] = "⚠  无已保存的补丁"
            },
            ["quick_no_patch_info"] = new Dictionary<Language, string> {
                [Language.EN] = "Apply a patch to be able to quickly re-apply it after a game update.",
                [Language.FR] = "Appliquez un patch pour pouvoir le réappliquer rapidement après une mise à jour.",
                [Language.ES] = "Aplica un parche para poder reaplicarlo rápidamente tras una actualización.",
                [Language.ZH] = "应用补丁后，游戏更新时可一键重新应用。"
            },
            ["quick_repatch_title"] = new Dictionary<Language, string> {
                [Language.EN] = "⚡  LAST SAVED PATCH",
                [Language.FR] = "⚡  DERNIER PATCH SAUVEGARDÉ",
                [Language.ES] = "⚡  ÚLTIMO PARCHE GUARDADO",
                [Language.ZH] = "⚡  上次已保存的补丁"
            },
            ["quick_patched_on"] = new Dictionary<Language, string> {
                [Language.EN] = "applied on",
                [Language.FR] = "appliqué le",
                [Language.ES] = "aplicado el",
                [Language.ZH] = "应用于"
            },
            ["quick_repatch_btn"] = new Dictionary<Language, string> {
                [Language.EN] = "Re-apply",
                [Language.FR] = "Réappliquer",
                [Language.ES] = "Reaplicar",
                [Language.ZH] = "重新应用"
            },
            ["log_config_loaded"] = new Dictionary<Language, string> {
                [Language.EN] = "Config loaded, last patch:",
                [Language.FR] = "Config chargée, dernier patch :",
                [Language.ES] = "Config cargada, último parche:",
                [Language.ZH] = "配置已加载，上次补丁："
            },
            // ── Inventory presets (left column) ──────────────────────────────
            ["preset_max_label"] = new Dictionary<Language, string> {
                [Language.EN] = "Inventory  —  start / max",
                [Language.FR] = "Inventaire  —  départ / max",
                [Language.ES] = "Inventario  —  inicio / máx",
                [Language.ZH] = "背包  —  初始 / 上限"
            },
            ["preset_p1"] = new Dictionary<Language, string> {
                [Language.EN] = "Vanilla      —   50 start  /  240 max",
                [Language.FR] = "Vanilla      —   50 départ  /  240 max",
                [Language.ES] = "Vanilla      —   50 inicio  /  240 máx",
                [Language.ZH] = "原版         —   50 初始  /  240 上限"
            },
            ["preset_p2"] = new Dictionary<Language, string> {
                [Language.EN] = "Starter      —  100 start  /  240 max",
                [Language.FR] = "Débutant     —  100 départ  /  240 max",
                [Language.ES] = "Principiante —  100 inicio  /  240 máx",
                [Language.ZH] = "新手         —  100 初始  /  240 上限"
            },
            ["preset_p3"] = new Dictionary<Language, string> {
                [Language.EN] = "Adventurer   —  150 start  /  240 max",
                [Language.FR] = "Aventurier   —  150 départ  /  240 max",
                [Language.ES] = "Aventurero   —  150 inicio  /  240 máx",
                [Language.ZH] = "冒险者       —  150 初始  /  240 上限"
            },
            ["preset_p4"] = new Dictionary<Language, string> {
                [Language.EN] = "Veteran      —  200 start  /  240 max",
                [Language.FR] = "Vétéran      —  200 départ  /  240 max",
                [Language.ES] = "Veterano     —  200 inicio  /  240 máx",
                [Language.ZH] = "老手         —  200 初始  /  240 上限"
            },
            ["preset_p5"] = new Dictionary<Language, string> {
                [Language.EN] = "Warlord      —  250 start  /  300 max",
                [Language.FR] = "Seigneur     —  250 départ  /  300 max",
                [Language.ES] = "Señor guerra —  250 inicio  /  300 máx",
                [Language.ZH] = "领主         —  250 初始  /  300 上限"
            },
            ["preset_recommended"] = new Dictionary<Language, string> {
                [Language.EN] = "", [Language.FR] = "", [Language.ES] = "", [Language.ZH] = ""
            },
            ["preset_max"] = new Dictionary<Language, string> {
                [Language.EN] = "", [Language.FR] = "", [Language.ES] = "", [Language.ZH] = ""
            },
            ["preset_unlocked"] = new Dictionary<Language, string> {
                [Language.EN] = "", [Language.FR] = "", [Language.ES] = "", [Language.ZH] = ""
            },
            ["preset_custom"] = new Dictionary<Language, string> {
                [Language.EN] = "Custom",
                [Language.FR] = "Personnalisé",
                [Language.ES] = "Personalizado",
                [Language.ZH] = "自定义"
            },
            // ── Warehouse presets (right column) ─────────────────────────────
            ["preset_wh_label"] = new Dictionary<Language, string> {
                [Language.EN] = "Private Storage  —  slots",
                [Language.FR] = "Coffre privé  —  emplacements",
                [Language.ES] = "Almacén privado  —  espacios",
                [Language.ZH] = "私人仓库  —  栏位"
            },
            ["preset_wh1"] = new Dictionary<Language, string> {
                [Language.EN] = "Vanilla     —  240 slots",
                [Language.FR] = "Vanilla     —  240 empl.",
                [Language.ES] = "Vanilla     —  240 esp.",
                [Language.ZH] = "原版        —  240 栏位"
            },
            ["preset_wh2"] = new Dictionary<Language, string> {
                [Language.EN] = "Extended    —  300 slots",
                [Language.FR] = "Étendu      —  300 empl.",
                [Language.ES] = "Extendido   —  300 esp.",
                [Language.ZH] = "扩展        —  300 栏位"
            },
            ["preset_wh3"] = new Dictionary<Language, string> {
                [Language.EN] = "Large       —  400 slots",
                [Language.FR] = "Grand       —  400 empl.",
                [Language.ES] = "Grande      —  400 esp.",
                [Language.ZH] = "大型        —  400 栏位"
            },
            ["preset_wh4"] = new Dictionary<Language, string> {
                [Language.EN] = "Massive     —  500 slots",
                [Language.FR] = "Massif      —  500 empl.",
                [Language.ES] = "Masivo      —  500 esp.",
                [Language.ZH] = "超大        —  500 栏位"
            },
            ["preset_wh5"] = new Dictionary<Language, string> {
                [Language.EN] = "Huge        —  700 slots",
                [Language.FR] = "Énorme      —  700 empl.",
                [Language.ES] = "Enorme      —  700 esp.",
                [Language.ZH] = "巨大        —  700 栏位"
            },
            ["preset_wh6"] = new Dictionary<Language, string> {
                [Language.EN] = "Insane      —  800 slots",
                [Language.FR] = "Démesuré    —  800 empl.",
                [Language.ES] = "Descomunal  —  800 esp.",
                [Language.ZH] = "超级         —  800 栏位"
            },
            ["preset_wh7"] = new Dictionary<Language, string> {
                [Language.EN] = "Extreme     —  900 slots",
                [Language.FR] = "Extrême     —  900 empl.",
                [Language.ES] = "Extremo     —  900 esp.",
                [Language.ZH] = "极限        —  900 栏位"
            },
            ["preset_wh8"] = new Dictionary<Language, string> {
                [Language.EN] = "Max         —  999 slots",
                [Language.FR] = "Maximum     —  999 empl.",
                [Language.ES] = "Máximo      —  999 esp.",
                [Language.ZH] = "最大        —  999 栏位"
            },
            ["preset_wh_custom"] = new Dictionary<Language, string> {
                [Language.EN] = "Custom",
                [Language.FR] = "Personnalisé",
                [Language.ES] = "Personalizado",
                [Language.ZH] = "自定义"
            },
            // ── Custom panel ─────────────────────────────────────────────────
            ["custom_values"] = new Dictionary<Language, string> {
                [Language.EN] = "Custom Values",
                [Language.FR] = "Valeurs personnalisées",
                [Language.ES] = "Valores personalizados",
                [Language.ZH] = "自定义值"
            },
            ["new_starting"] = new Dictionary<Language, string> {
                [Language.EN] = "New Starting Slots",
                [Language.FR] = "Nouveaux emplacements de départ",
                [Language.ES] = "Nuevos espacios iniciales",
                [Language.ZH] = "新初始栏位"
            },
            ["new_max"] = new Dictionary<Language, string> {
                [Language.EN] = "New Maximum Slots",
                [Language.FR] = "Nouveaux emplacements maximum",
                [Language.ES] = "Nuevos espacios máximos",
                [Language.ZH] = "新最大栏位"
            },
            ["new_warehouse"] = new Dictionary<Language, string> {
                [Language.EN] = "New Storage Slots",
                [Language.FR] = "Nouveaux emplacements coffre",
                [Language.ES] = "Nuevos espacios de almacén",
                [Language.ZH] = "新仓库栏位"
            },
            ["warning_above_230"] = new Dictionary<Language, string> {
                [Language.EN] = "⚠  Values above 240 may cause crashes when looting or buying items.",
                [Language.FR] = "⚠  Au-dessus de 240 peut causer des crashes lors du loot ou des achats.",
                [Language.ES] = "⚠  Valores superiores a 240 pueden causar errores al recoger o comprar objetos.",
                [Language.ZH] = "⚠  超过240可能导致拾取或购买物品时崩溃。"
            },
            ["warning_wh_above_500"] = new Dictionary<Language, string> {
                [Language.EN] = "⚠  Above 500 storage slots may cause instability.",
                [Language.FR] = "⚠  Au-dessus de 500 emplacements coffre peut causer de l'instabilité.",
                [Language.ES] = "⚠  Más de 500 espacios de almacén puede causar inestabilidad.",
                [Language.ZH] = "⚠  超过500仓库栏位可能导致不稳定。"
            },
            // ── Backup ───────────────────────────────────────────────────────
            ["backup_label"] = new Dictionary<Language, string> {
                [Language.EN] = "Backup",
                [Language.FR] = "Sauvegarde",
                [Language.ES] = "Copia de seguridad",
                [Language.ZH] = "备份"
            },
            ["backup_auto"] = new Dictionary<Language, string> {
                [Language.EN] = "Create automatic backup before patching",
                [Language.FR] = "Créer une sauvegarde automatique avant le patch",
                [Language.ES] = "Crear copia de seguridad automática antes de parchear",
                [Language.ZH] = "打补丁前自动创建备份"
            },
            ["apply_patch"] = new Dictionary<Language, string> {
                [Language.EN] = "Apply Patch",
                [Language.FR] = "Appliquer le patch",
                [Language.ES] = "Aplicar Parche",
                [Language.ZH] = "应用补丁"
            },
            ["restore_backup"] = new Dictionary<Language, string> {
                [Language.EN] = "Restore Backup",
                [Language.FR] = "Restaurer la sauvegarde",
                [Language.ES] = "Restaurar copia",
                [Language.ZH] = "恢复备份"
            },
            // ── Status ───────────────────────────────────────────────────────
            ["status_ready"] = new Dictionary<Language, string> {
                [Language.EN] = "Ready",
                [Language.FR] = "Prêt",
                [Language.ES] = "Listo",
                [Language.ZH] = "就绪"
            },
            ["status_detecting"] = new Dictionary<Language, string> {
                [Language.EN] = "Detecting...",
                [Language.FR] = "Détection en cours...",
                [Language.ES] = "Detectando...",
                [Language.ZH] = "检测中..."
            },
            ["status_found"] = new Dictionary<Language, string> {
                [Language.EN] = "Game found!",
                [Language.FR] = "Jeu trouvé !",
                [Language.ES] = "¡Juego encontrado!",
                [Language.ZH] = "游戏已找到！"
            },
            ["status_not_found"] = new Dictionary<Language, string> {
                [Language.EN] = "Game not found. Please enter the path manually.",
                [Language.FR] = "Jeu introuvable. Entrez le chemin manuellement.",
                [Language.ES] = "Juego no encontrado. Ingresa la ruta manualmente.",
                [Language.ZH] = "未找到游戏，请手动输入路径。"
            },
            ["status_patched"] = new Dictionary<Language, string> {
                [Language.EN] = "Patch applied successfully!",
                [Language.FR] = "Patch appliqué avec succès !",
                [Language.ES] = "¡Parche aplicado con éxito!",
                [Language.ZH] = "补丁应用成功！"
            },
            ["status_backup_created"] = new Dictionary<Language, string> {
                [Language.EN] = "Backup created.",
                [Language.FR] = "Sauvegarde créée.",
                [Language.ES] = "Copia de seguridad creada.",
                [Language.ZH] = "备份已创建。"
            },
            ["status_backup_exists"] = new Dictionary<Language, string> {
                [Language.EN] = "Backup already exists.",
                [Language.FR] = "Sauvegarde déjà existante.",
                [Language.ES] = "La copia de seguridad ya existe.",
                [Language.ZH] = "备份已存在。"
            },
            ["status_restored"] = new Dictionary<Language, string> {
                [Language.EN] = "Backup restored successfully!",
                [Language.FR] = "Sauvegarde restaurée avec succès !",
                [Language.ES] = "¡Copia restaurada con éxito!",
                [Language.ZH] = "备份恢复成功！"
            },
            // ── Errors ───────────────────────────────────────────────────────
            ["err_no_path"] = new Dictionary<Language, string> {
                [Language.EN] = "Error: game path not set.",
                [Language.FR] = "Erreur : chemin de jeu non défini.",
                [Language.ES] = "Error: ruta del juego no definida.",
                [Language.ZH] = "错误：未设置游戏路径。"
            },
            ["err_file_not_found"] = new Dictionary<Language, string> {
                [Language.EN] = "Error: 0.paz file not found at this path.",
                [Language.FR] = "Erreur : fichier 0.paz introuvable dans ce chemin.",
                [Language.ES] = "Error: archivo 0.paz no encontrado en esta ruta.",
                [Language.ZH] = "错误：在该路径下未找到0.paz文件。"
            },
            ["err_no_backup"] = new Dictionary<Language, string> {
                [Language.EN] = "Error: no backup file found.",
                [Language.FR] = "Erreur : aucune sauvegarde trouvée.",
                [Language.ES] = "Error: no se encontró copia de seguridad.",
                [Language.ZH] = "错误：未找到备份文件。"
            },
            ["err_invalid_values"] = new Dictionary<Language, string> {
                [Language.EN] = "Error: invalid values.",
                [Language.FR] = "Erreur : valeurs invalides.",
                [Language.ES] = "Error: valores no válidos.",
                [Language.ZH] = "错误：无效值。"
            },
            ["err_patch_failed"] = new Dictionary<Language, string> {
                [Language.EN] = "Error while patching: ",
                [Language.FR] = "Erreur lors du patch : ",
                [Language.ES] = "Error al parchear: ",
                [Language.ZH] = "补丁错误："
            },
            // ── Confirm dialogs ──────────────────────────────────────────────
            ["confirm_patch_title"] = new Dictionary<Language, string> {
                [Language.EN] = "Confirm Patch",
                [Language.FR] = "Confirmer le patch",
                [Language.ES] = "Confirmar parche",
                [Language.ZH] = "确认补丁"
            },
            ["confirm_patch_msg"] = new Dictionary<Language, string> {
                [Language.EN] = "Apply patch with these values?\n\nInventory — Starting: {0}  /  Maximum: {1}\nPrivate Storage — Slots: {2}",
                [Language.FR] = "Appliquer le patch avec ces valeurs ?\n\nInventaire — Départ : {0}  /  Maximum : {1}\nCoffre privé — Emplacements : {2}",
                [Language.ES] = "¿Aplicar parche con estos valores?\n\nInventario — Inicio: {0}  /  Máximo: {1}\nAlmacén privado — Espacios: {2}",
                [Language.ZH] = "使用以下值应用补丁？\n\n背包 — 初始：{0}  /  最大：{1}\n私人仓库 — 栏位：{2}"
            },
            ["confirm_restore_title"] = new Dictionary<Language, string> {
                [Language.EN] = "Confirm Restore",
                [Language.FR] = "Confirmer la restauration",
                [Language.ES] = "Confirmar restauración",
                [Language.ZH] = "确认恢复"
            },
            ["confirm_restore_msg"] = new Dictionary<Language, string> {
                [Language.EN] = "Restore original backup? The current patch will be lost.",
                [Language.FR] = "Restaurer la sauvegarde originale ? Le patch actuel sera perdu.",
                [Language.ES] = "¿Restaurar la copia original? Se perderá el parche actual.",
                [Language.ZH] = "恢复原始备份？当前补丁将丢失。"
            },
            ["select_folder"] = new Dictionary<Language, string> {
                [Language.EN] = "Select Crimson Desert installation folder",
                [Language.FR] = "Sélectionner le dossier d'installation de Crimson Desert",
                [Language.ES] = "Seleccionar carpeta de instalación de Crimson Desert",
                [Language.ZH] = "选择Crimson Desert安装文件夹"
            },
            ["uninstall_hint"] = new Dictionary<Language, string> {
                [Language.EN] = "To uninstall: restore backup or verify game files integrity via Steam.",
                [Language.FR] = "Pour désinstaller : restaurez la sauvegarde ou vérifiez l'intégrité des fichiers via Steam.",
                [Language.ES] = "Para desinstalar: restaura la copia o verifica la integridad de archivos en Steam.",
                [Language.ZH] = "卸载方式：恢复备份或通过Steam验证游戏文件完整性。"
            },
            ["log_reading"] = new Dictionary<Language, string> {
                [Language.EN] = "Reading file...",
                [Language.FR] = "Lecture du fichier...",
                [Language.ES] = "Leyendo archivo...",
                [Language.ZH] = "读取文件中..."
            },
            ["log_writing"] = new Dictionary<Language, string> {
                [Language.EN] = "Writing patch...",
                [Language.FR] = "Écriture du patch...",
                [Language.ES] = "Escribiendo parche...",
                [Language.ZH] = "写入补丁中..."
            },
            ["log_verifying"] = new Dictionary<Language, string> {
                [Language.EN] = "Verifying...",
                [Language.FR] = "Vérification...",
                [Language.ES] = "Verificando...",
                [Language.ZH] = "验证中..."
            },
            ["backup_exists_info"] = new Dictionary<Language, string> {
                [Language.EN] = "Backup: ✓ present",
                [Language.FR] = "Sauvegarde : ✓ présente",
                [Language.ES] = "Copia: ✓ presente",
                [Language.ZH] = "备份：✓ 存在"
            },
            ["backup_missing_info"] = new Dictionary<Language, string> {
                [Language.EN] = "Backup: ✗ not found",
                [Language.FR] = "Sauvegarde : ✗ absente",
                [Language.ES] = "Copia: ✗ no encontrada",
                [Language.ZH] = "备份：✗ 未找到"
            },
        };

        public static string Get(string key)
        {
            if (_strings.TryGetValue(key, out var dict))
            {
                if (dict.TryGetValue(Current, out var val))
                    return val;
                if (dict.TryGetValue(Language.EN, out var fallback))
                    return fallback;
            }
            return key;
        }
    }
}
