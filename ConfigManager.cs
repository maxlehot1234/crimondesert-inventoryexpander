using System;
using System.IO;
using System.Globalization;
using System.Text;

namespace CrimsonDesertExpander
{
    public class PatchConfig
    {
        public string   GamePath       { get; set; }
        public ushort   DefaultSlots   { get; set; }
        public ushort   MaxSlots       { get; set; }
        public ushort   WarehouseSlots { get; set; }
        public string   PresetKey      { get; set; }
        public string   WPresetKey     { get; set; }
        public DateTime PatchedAt      { get; set; }
    }

    public static class ConfigManager
    {
        static readonly string ConfigPath = Path.Combine(
            Program.AppDataDir, "config.json");

        public static void Save(PatchConfig c)
        {
            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("{");
                sb.AppendLine($"  \"GamePath\": \"{Escape(c.GamePath)}\",");
                sb.AppendLine($"  \"DefaultSlots\": {c.DefaultSlots},");
                sb.AppendLine($"  \"MaxSlots\": {c.MaxSlots},");
                sb.AppendLine($"  \"WarehouseSlots\": {c.WarehouseSlots},");
                sb.AppendLine($"  \"PresetKey\": \"{Escape(c.PresetKey)}\",");
                sb.AppendLine($"  \"WPresetKey\": \"{Escape(c.WPresetKey)}\",");
                sb.AppendLine($"  \"PatchedAt\": \"{c.PatchedAt:yyyy-MM-ddTHH:mm:ss}\"");
                sb.AppendLine("}");
                File.WriteAllText(ConfigPath, sb.ToString(), Encoding.UTF8);
            }
            catch { }
        }

        public static PatchConfig Load()
        {
            try
            {
                if (!File.Exists(ConfigPath)) return null;
                var text = File.ReadAllText(ConfigPath, Encoding.UTF8);
                var c = new PatchConfig();
                c.GamePath       = ReadString(text, "GamePath");
                c.DefaultSlots   = (ushort)ReadInt(text, "DefaultSlots");
                c.MaxSlots       = (ushort)ReadInt(text, "MaxSlots");
                c.WarehouseSlots = (ushort)ReadInt(text, "WarehouseSlots");
                c.PresetKey      = ReadString(text, "PresetKey");
                c.WPresetKey     = ReadString(text, "WPresetKey");
                c.PatchedAt      = DateTime.TryParse(ReadString(text, "PatchedAt"),
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt) ? dt : DateTime.MinValue;
                return c;
            }
            catch { return null; }
        }

        public static bool Exists() => File.Exists(ConfigPath);

        static string Escape(string s) => s?.Replace("\\", "\\\\").Replace("\"", "\\\"") ?? "";

        static string ReadString(string json, string key)
        {
            var search = $"\"{key}\": \"";
            var idx = json.IndexOf(search, StringComparison.Ordinal);
            if (idx < 0) return "";
            idx += search.Length;
            var end = json.IndexOf('"', idx);
            if (end < 0) return "";
            return json.Substring(idx, end - idx).Replace("\\\\", "\\").Replace("\\\"", "\"");
        }

        static int ReadInt(string json, string key)
        {
            var search = $"\"{key}\": ";
            var idx = json.IndexOf(search, StringComparison.Ordinal);
            if (idx < 0) return 0;
            idx += search.Length;
            var end = idx;
            while (end < json.Length && char.IsDigit(json[end])) end++;
            return int.TryParse(json.Substring(idx, end - idx), out var v) ? v : 0;
        }
    }
}
