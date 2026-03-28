using System;
using System.IO;
using System.Windows.Forms;

namespace CrimsonDesertExpander
{
    static class Program
    {
        public static readonly string AppDataDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "CD-InventoryExpander");

        [STAThread]
        static void Main()
        {
            Directory.CreateDirectory(AppDataDir);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
