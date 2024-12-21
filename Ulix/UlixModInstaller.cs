using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ulix.Properties;
using Ulix.Utils;
using System.Text.RegularExpressions;

namespace Ulix
{
    public partial class UlixModInstaller : Form
    {
        private string mPath = null;
        private readonly string UlixMinecraftPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UlixMINECRAFT");
        public UlixModInstaller()
        {
            InitializeComponent();
        }
        private void InstallMod(string modPath)
        {
            string modsPath = Path.Combine(UlixMinecraftPath, "mods");
            string newModPath = Path.Combine(modsPath, Path.GetFileName(modPath));

            if (!Directory.Exists(modsPath))
            {
                MessageBox.Show($"Не найден установленный Forge", "UlixError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteNewLogItem("Ошибка: не найден Forge");
                return;
            }

            File.Copy(modPath, newModPath, true);
            installedMods.Items.Add(Path.GetFileName(modPath));
        }
        private void CloseAppControlBox_Click(object sender, EventArgs e) => Hide();
        private void InstallModButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(mPath))
            {
                 InstallMod(mPath);
            }
            else
            {
                MessageBox.Show($"Выберите мод!", "UlixError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteNewLogItem("Ошибка: мод не выбран");
            }
        }
        private void SelectMod_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new())
            {
                openFileDialog.Filter = "JAR файлы (*.jar)|*.jar";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    label1.Location = new Point(252, 250);
                    label1.Text = openFileDialog.FileName;
                    mPath= openFileDialog.FileName;
                }
            }
        }
    }
}