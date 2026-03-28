using System;
using System.IO;

namespace CrimsonDesertExpander
{
    public class PazValues
    {
        public ushort DefaultSlots   { get; set; }
        public ushort MaxSlots       { get; set; }
        public ushort WarehouseSlots { get; set; }
    }

    public static class PatchEngine
    {
        private const string PAZ_RELATIVE = @"0008\0.paz";

        // ── Signature "Character" ─────────────────────────────────────────────
        // type_id(02 00) + name_len(09 00 00 00) + "Character" + null(00) + flag(01)
        private static readonly byte[] CHAR_SIGNATURE = new byte[]
        {
            0x02, 0x00,
            0x09, 0x00, 0x00, 0x00,
            0x43, 0x68, 0x61, 0x72, 0x61, 0x63, 0x74, 0x65, 0x72,
            0x00,
            0x01
        };
        private const int CHAR_OFFSET_DEFAULT = 17;
        private const int CHAR_OFFSET_MAX     = 19;

        // ── CampWareHouse — scan dynamique par nom ASCII ──────────────────────
        // Cherche "CampWareHouse\0" dans le PAZ.
        // name(13) + null(1) + flag(1) = +15 pour sx, +17 pour sy
        private static readonly byte[] WH_NAME = new byte[]
        {
            0x43, 0x61, 0x6D, 0x70, 0x57, 0x61, 0x72, 0x65, 0x48, 0x6F, 0x75, 0x73, 0x65, 0x00
        };
        private const int WH_NAME_TO_SX = 15;
        private const int WH_NAME_TO_SY = 17;

        private const int BUFFER_SIZE = 4 * 1024 * 1024;

        // ─────────────────────────────────────────────────────────────────────
        //  PATH / FILE HELPERS
        // ─────────────────────────────────────────────────────────────────────
        public static string GetPazPath(string gamePath)
            => Path.Combine(gamePath, PAZ_RELATIVE);

        public static bool PazExists(string gamePath)
            => File.Exists(GetPazPath(gamePath));

        public static bool BackupExists(string gamePath)
            => File.Exists(GetPazPath(gamePath) + ".backup");

        public static bool BackupMatchesCurrentVersion(string gamePath)
        {
            string paz    = GetPazPath(gamePath);
            string backup = paz + ".backup";
            if (!File.Exists(backup)) return false;
            return new FileInfo(paz).Length == new FileInfo(backup).Length;
        }

        // ─────────────────────────────────────────────────────────────────────
        //  GENERIC SIGNATURE SCANNER
        // ─────────────────────────────────────────────────────────────────────
        static long FindSignature(string pazFile, byte[] sig)
        {
            int    overlap = sig.Length - 1;
            byte[] buf     = new byte[BUFFER_SIZE + overlap];
            long   fileOff = 0;

            using (var fs = new FileStream(pazFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                while (true)
                {
                    fs.Position = fileOff;
                    int read = fs.Read(buf, 0, buf.Length);
                    if (read < sig.Length) break;

                    int limit = read - sig.Length;
                    for (int i = 0; i <= limit; i++)
                    {
                        bool match = true;
                        for (int j = 0; j < sig.Length; j++)
                            if (buf[i + j] != sig[j]) { match = false; break; }
                        if (match)
                            return fileOff + i;
                    }

                    fileOff += BUFFER_SIZE;
                }
            }
            return -1;
        }

        // ─────────────────────────────────────────────────────────────────────
        //  CHARACTER INVENTORY
        // ─────────────────────────────────────────────────────────────────────
        public static long FindSlotOffset(string gamePath)
        {
            string pazFile = GetPazPath(gamePath);
            long   sigPos  = FindSignature(pazFile, CHAR_SIGNATURE);
            if (sigPos < 0)
                throw new Exception("Character inventory record not found in PAZ file. The game may have updated its data format.");
            return sigPos + CHAR_OFFSET_DEFAULT;
        }

        // ─────────────────────────────────────────────────────────────────────
        //  CAMP WAREHOUSE
        // ─────────────────────────────────────────────────────────────────────
        public static long FindWarehouseOffset(string gamePath)
        {
            string pazFile = GetPazPath(gamePath);
            long   namePos = FindSignature(pazFile, WH_NAME);
            if (namePos < 0)
                throw new Exception("CampWareHouse record not found in PAZ file. The game may have renamed this entry.");
            return namePos + WH_NAME_TO_SX;
        }

        // ─────────────────────────────────────────────────────────────────────
        //  READ / WRITE
        // ─────────────────────────────────────────────────────────────────────
        public static PazValues ReadValues(string gamePath)
        {
            string pazFile = GetPazPath(gamePath);
            long   charOff = FindSlotOffset(gamePath);
            long   whOff   = FindWarehouseOffset(gamePath);
            byte[] buf     = new byte[4];

            using (var f = new FileStream(pazFile, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                f.Seek(charOff, SeekOrigin.Begin);
                f.Read(buf, 0, 4);
                ushort def = BitConverter.ToUInt16(buf, 0);
                ushort max = BitConverter.ToUInt16(buf, 2);

                f.Seek(whOff, SeekOrigin.Begin);
                f.Read(buf, 0, 2);
                ushort wh = BitConverter.ToUInt16(buf, 0);

                return new PazValues { DefaultSlots = def, MaxSlots = max, WarehouseSlots = wh };
            }
        }

        public static PazValues WriteValues(string gamePath, ushort defaultSlots, ushort maxSlots, ushort warehouseSlots)
        {
            string pazFile = GetPazPath(gamePath);
            long   charOff = FindSlotOffset(gamePath);
            long   whOff   = FindWarehouseOffset(gamePath);

            using (var f = new FileStream(pazFile, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                f.Seek(charOff, SeekOrigin.Begin);
                f.Write(BitConverter.GetBytes(defaultSlots), 0, 2);
                f.Seek(charOff + 2, SeekOrigin.Begin);
                f.Write(BitConverter.GetBytes(maxSlots), 0, 2);

                f.Seek(whOff, SeekOrigin.Begin);
                f.Write(BitConverter.GetBytes(warehouseSlots), 0, 2);
                f.Seek(whOff + 2, SeekOrigin.Begin);
                f.Write(BitConverter.GetBytes(warehouseSlots), 0, 2);

                f.Flush();
            }

            return ReadValues(gamePath);
        }

        // ─────────────────────────────────────────────────────────────────────
        //  BACKUP / RESTORE
        // ─────────────────────────────────────────────────────────────────────
        public static void CreateBackup(string gamePath)
        {
            string pazFile    = GetPazPath(gamePath);
            string backupFile = pazFile + ".backup";
            if (File.Exists(backupFile))
            {
                if (new FileInfo(pazFile).Length != new FileInfo(backupFile).Length)
                    File.Delete(backupFile);
                else
                    return;
            }
            File.Copy(pazFile, backupFile);
        }

        public static void RestoreBackup(string gamePath)
        {
            string pazFile    = GetPazPath(gamePath);
            string backupFile = pazFile + ".backup";
            if (!File.Exists(backupFile))
                throw new FileNotFoundException("Backup file not found.");
            File.Copy(backupFile, pazFile, true);
        }

        // ─────────────────────────────────────────────────────────────────────
        //  DETECT
        // ─────────────────────────────────────────────────────────────────────
        public static string DetectGamePath()
        {
            string[] defaultPaths =
            {
                @"C:\Program Files (x86)\Steam\steamapps\common\Crimson Desert",
                @"C:\Program Files\Steam\steamapps\common\Crimson Desert",
            };
            foreach (string p in defaultPaths)
                if (PazExists(p)) return p;

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType != DriveType.Fixed) continue;
                string candidate = Path.Combine(drive.Name, @"SteamLibrary\steamapps\common\Crimson Desert");
                if (PazExists(candidate)) return candidate;
            }
            return null;
        }
    }
}
