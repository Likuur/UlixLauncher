using System;
using System.IO;
using CmlLib.Core;
using CmlLib.Core.ProcessBuilder;
using System.Windows.Forms;
using CmlLib.Core.Auth;
using System.Diagnostics;
using CmlLib.Core.Installers;
using System.Net.Http;
using CmlLib.Core.Auth.Microsoft;
using CmlLib.Core.Auth.Microsoft.Sessions;
using System.Data.SQLite;
using System.Net;
using CmlLib.Core.Installer.Forge;
using System.Drawing;
using Ulix.Utils;
using Ulix.Properties;

namespace Ulix
{
    public partial class MainMenu : Form
    {
        private readonly string UlixMinecraftPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "UlixMINECRAFT");
        public Process Minecraft;
        public ProcessWrapper processUtil;
        private DiscordRpc.EventHandlers handlers;
        private DiscordRpc.RichPresence presence;

        public MainMenu()
        {
            InitializeComponent();

            if (!File.Exists("discord-rpc-w32.dll"))
            {
                File.WriteAllBytes("discord-rpc-w32.dll", Resources.discord_rpc_w321);
                Logger.WriteNewLogItem("Распакован файл: discord-rpc-w32.dll");
            }

            if (File.Exists(Path.Combine(UlixMinecraftPath, "application.log")))
            {
                Logger.DeleteLog(Path.Combine(UlixMinecraftPath, "application.log"));
            }

            Logger.CreateNewLog(Path.Combine(UlixMinecraftPath, "application.log"));

            Logger.WriteNewLogItem("Логирование запущено");

            LoadSettings();
            UpdateTimer.Start();

            Logger.WriteNewLogItem("Загрузка версий Minecraft...");

            LoadVersionsMethodAsync();
            LoadNewsToTextBox();

            string UlixVersion = Updater.GetUlixVersion();

            if (UlixVersion != Updater.CurrentAppVersion)
            {
                Logger.WriteNewLogItem("Найдена новая версия приложения");

                DialogResult = MessageBox.Show("Найдена новая версия приложения. Установить?", "UlixLauncher", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (DialogResult == DialogResult.Yes)
                {
                    Updater.DownladNewVersion();
                }
            }

            if (MultiAccountComboBox.Items.Count > 0)
            {
                MultiAccountComboBox.SelectedIndex = 0;
            }

            Logger.WriteNewLogItem("Приложение запущено");
        }
        private void LoadNewsToTextBox()
        {
            string url = "https://raw.githubusercontent.com/Likuur/UlixLauncher/refs/heads/main/OtherShit/news.db";
            string dbFilePath = Path.Combine(UlixMinecraftPath, "news.db");

            txtNewsContent.Clear();
            txtNewsContent.Enabled = true;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (WebClient client = new WebClient())
            {
                try
                {
                    txtNewsContent.Text = "Загрузка содержимого файла...";
                    Logger.WriteNewLogItem("Начата загрузка новостей.");

                    if (File.Exists(dbFilePath))
                    {
                        File.Delete(dbFilePath);
                    }

                    client.DownloadFile(url, dbFilePath);

                    Logger.WriteNewLogItem("Файл новостей успешно загружен.");

                    ReadNews(dbFilePath);
                }
                catch (WebException ex)
                {
                    txtNewsContent.Text = "Ошибка при загрузке файла: " + ex.Message;
                    Logger.WriteNewLogItem($"Ошибка при загрузке новостей: {ex.Message}");
                }
                finally
                {
                    txtNewsContent.Enabled = false;
                }
            }
        }
        private void ReadNews(string dbPath)
        {
            string connectionString = $"Data Source={dbPath};Version=3;";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT Title, Content, Author, DateAdded FROM News ORDER BY DateAdded DESC";

                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        txtNewsContent.Clear();
                        while (reader.Read())
                        {
                            string title = reader["Title"].ToString();
                            string content = reader["Content"].ToString();
                            string author = reader["Author"].ToString();
                            string dateAdded = reader["DateAdded"].ToString();

                            txtNewsContent.AppendText($"Заголовок: {title}\nСодержание: {content}\nАвтор: {author}\nДата: {dateAdded}\n\n");

                            Logger.WriteNewLogItem($"Добавлена новость: {title}");
                        }
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Focus();

            handlers = new DiscordRpc.EventHandlers();
            DiscordRpc.Initialize("1316250493520973865", ref handlers, true, null);

            presence = new DiscordRpc.RichPresence();
            presence.details = "In main menu";
            DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            long startTimestamp = (long)(DateTime.UtcNow - d).TotalSeconds;
            presence.startTimestamp = startTimestamp;
            presence.largeImageKey = "https://raw.githubusercontent.com/Likuur/UlixLauncher/refs/heads/main/OtherShit/Ulix.PNG";

            DiscordRpc.UpdatePresence(ref presence);
        }
        private void UpdateTimer_Tick(object sender, EventArgs e) => LoadNewsToTextBox();
        private void UpdateProgressBar(int value, int max)
        {
            if (ProgressBarLaunch.InvokeRequired)
            {
                ProgressBarLaunch.Invoke(new Action(() =>
                {
                    ProgressBarLaunch.Value = value;
                    ProgressBarLaunch.Maximum = max;
                }));
            }
            else
            {
                ProgressBarLaunch.Value = value;
                ProgressBarLaunch.Maximum = max;
            }
        }
        private void ReleaseToogleSwitch_CheckedChanged(object sender, EventArgs e) => LoadVersionsMethodAsync();
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            UlixHelp uh = new UlixHelp();
            uh.ShowDialog();
        }
        private void DownButtonApp_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
        private void CloseAppControlBox_Click(object sender, EventArgs e)
        {
            SaveSettings();
            Application.Exit();
        }
        private void UpdateProgress(object sender, InstallerProgressChangedEventArgs e)
        {
            Logger.WriteNewLogItem("Загрузка: " + e.Name);
            UpdateLaunchEvent("Загрузка: " + e.Name);
            UpdateProgressBar(e.ProgressedTasks, e.TotalTasks);
        }
        private async void GameButton_Click(object sender, EventArgs e)
        {
            DateTime d;
            long startTimestamp;

            Logs.Clear();

            UpdateLaunchEvent("Запуск Minecraft...");

            string version = string.Empty;

            if (!Directory.Exists(UlixMinecraftPath))
            {
                Directory.CreateDirectory(UlixMinecraftPath);
            }

            var PathToGame = new MinecraftPath(UlixMinecraftPath);
            var UlixLauncher = new MinecraftLauncher(PathToGame);
            Process minecraftProcess;

            MLaunchOption LaunchOption;

            UlixLauncher.FileProgressChanged += UpdateProgress;

            if (string.IsNullOrEmpty(MultiAccountComboBox.SelectedItem?.ToString()))
            {
                UpdateLaunchEvent("Ошибка: отсутствует имя пользователя.");
                MessageBox.Show("Пожалуйста, введите имя пользователя перед запуском Minecraft!", "UlixError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteNewLogItem("Ошибка: отсутствует имя пользователя");
                return;
            }

            if (VersionList.SelectedIndex != -1)
            {
                version = VersionList.SelectedItem.ToString();
                if (!fabric.Checked && !forge.Checked)
                {
                    Logger.WriteNewLogItem("Началась загрузка версии " + version);
                    await UlixLauncher.InstallAsync(version);
                }
                else if (forge.Checked)
                {
                    if (SelectedSupportedForgeVersion())
                    {
                        Logger.WriteNewLogItem("Началась загрузка Forge версии " + version);

                        ForgeInstaller forgeInstaller = new ForgeInstaller(UlixLauncher);

                        var versionName = await forgeInstaller.Install(version, new ForgeInstallOptions
                        {
                            FileProgress = new SyncProgress<InstallerProgressChangedEventArgs>(ForgeFileChanged),
                            SkipIfAlreadyInstalled = true
                        });

                        LaunchOption = new MLaunchOption
                        {
                            Session = MSession.CreateOfflineSession(MultiAccountComboBox.SelectedItem.ToString()),
                            MaximumRamMb = (int)OperativeMemoryUp.Value,
                            GameLauncherName = "UlixLauncher",
                        };

                        Minecraft = await UlixLauncher.CreateProcessAsync(version, LaunchOption);

                        Minecraft.EnableRaisingEvents = true;
                        Minecraft.Exited += Minecraft_Exited;
                        Minecraft.Start();

                        UpdateLaunchEvent("Minecraft " + version);

                        Logger.WriteNewLogItem($"Minecraft запущен, версия: {version}");

                        presence = new DiscordRpc.RichPresence();
                        presence.details = "Playing Minecraft " + version + " Forge";
                        d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                        startTimestamp = (long)(DateTime.UtcNow - d).TotalSeconds;
                        presence.startTimestamp = startTimestamp;
                        presence.largeImageKey = "https://raw.githubusercontent.com/Likuur/UlixLauncher/refs/heads/main/Ulix.PNG";

                        DiscordRpc.UpdatePresence(ref presence);

                        return;
                    }
                    else
                    {
                        UpdateLaunchEvent("Версия Forge не поддерживается");
                        MessageBox.Show($"Версия {version} в Forge не поддреживается!", "UlixError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Logger.WriteNewLogItem("Ошибка: Версия Forge не поддерживается");
                        return;
                    }
                }
                else if (fabric.Checked)
                {
                    Logger.WriteNewLogItem("Началась загрузка Fabric версии " + version);

                    //await FabricInstaller.InstallAndLaunch(version, UlixLauncher.MinecraftPath);

                    Minecraft = null; //**

                    Minecraft.EnableRaisingEvents = true;
                    Minecraft.Exited += Minecraft_Exited;

                    processUtil = new ProcessWrapper(Minecraft);
                    processUtil.OutputReceived += (s, e) => Logs.AppendText(e);
                    processUtil.StartWithEvents();

                    presence = new DiscordRpc.RichPresence();
                    presence.details = "Playing Minecraft " + version + " Fabric";
                    d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                    startTimestamp = (long)(DateTime.UtcNow - d).TotalSeconds;
                    presence.startTimestamp = startTimestamp;
                    presence.largeImageKey = "https://raw.githubusercontent.com/Likuur/UlixLauncher/refs/heads/main/Ulix.PNG";

                    DiscordRpc.UpdatePresence(ref presence);
                }
                Logger.WriteNewLogItem($"Установлена версия Minecraft: {version}");
            }
            else
            {
                UpdateLaunchEvent("Вы берите версию Minecraft!");
                MessageBox.Show("Выберите версию Minecraft перед запуском!", "UlixError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteNewLogItem("Ошибка: версия Minecraft не выбрана.");
                return;
            }

            if (Minecraft != null)
            {
                Minecraft.Exited -= Minecraft_Exited;
                Minecraft = null;
            }

            LaunchOption = new MLaunchOption
            {
                Session = MSession.CreateOfflineSession(MultiAccountComboBox.SelectedItem.ToString()),
                MaximumRamMb = (int)OperativeMemoryUp.Value,
                GameLauncherName = "UlixLauncher",
                IsDemo = DemoVersion.Checked
            };

            minecraftProcess = await UlixLauncher.BuildProcessAsync(version, LaunchOption);

            minecraftProcess.StartInfo.Arguments = JavaArgsTextBox.Text + " " + minecraftProcess.StartInfo.Arguments;

            Minecraft = minecraftProcess;
            Minecraft.EnableRaisingEvents = true;
            Minecraft.Exited += Minecraft_Exited;

            processUtil = new ProcessWrapper(Minecraft);
            processUtil.OutputReceived += (s, e) => PrintGameLog(s, e);
            processUtil.StartWithEvents();

            UpdateLaunchEvent("Minecraft " + version);

            Logger.WriteNewLogItem($"Minecraft запущен, версия: {version}");

            presence = new DiscordRpc.RichPresence();
            presence.details = "Playing Minecraft " + version;
            d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            startTimestamp = (long)(DateTime.UtcNow - d).TotalSeconds;
            presence.startTimestamp = startTimestamp;
            presence.largeImageKey = "https://raw.githubusercontent.com/Likuur/UlixLauncher/refs/heads/main/Ulix.PNG";

            DiscordRpc.UpdatePresence(ref presence);

            Settings.Default.lastPlayVersionIndex = VersionList.SelectedIndex;
            SaveSettings();
        }
        private void ForgeFileChanged(InstallerProgressChangedEventArgs e)
        {
            UpdateLaunchEvent("Загрузка: " + e.Name);
            UpdateProgressBar(e.ProgressedTasks, e.TotalTasks);
        }
        private bool SelectedSupportedForgeVersion()
        {
            string version = VersionList.SelectedItem.ToString();
            if (Array.Exists(SupportedForgeVersions, v => v == version))
                return true;
            else
               return false;
        }
        string[] SupportedForgeVersions = new string[]
        {
            "1.21.4",
            "1.20.6",
            "1.20.1",
            "1.20",
            "1.19.4",
            "1.19.3",
            "1.19.2",
            "1.19.1",
            "1.19",
            "1.18.2",
            "1.18.1",
            "1.18",
            "1.17.1",
            "1.16.5",
            "1.16.4",
            "1.16.3",
            "1.16.2",
            "1.16.1",
            "1.15.2",
            "1.15.1",
            "1.15",
            "1.14.4",
            "1.14.3",
            "1.14.2",
            "1.13.2",
            "1.12.2",
            "1.12.1",
            "1.12",
            "1.11.2",
            "1.11",
            "1.10.2",
            "1.10",
            "1.9.4",
            "1.9",
            "1.8.9",
            "1.8.8",
            "1.7.10"
        };
        private void PrintGameLog(object sender, string Event)
        {
            if (Logs.InvokeRequired)
            {
                Logger.WriteNewLogItem(Event);

                Logs.Invoke(new Action<string>(AppendTextToLog), Event);
            }
            else
            {
                AppendTextToLog(Event);
            }
        }
        private void AppendTextToLog(string logEntry)
        {
            string level = ExtractLogLevel(logEntry);
            string message = ExtractLogMessage(logEntry);

            if (string.IsNullOrEmpty(message) || message == "No message" || message == "UNKNOWN")
            {
                return;
            }

            Color color = GetColorForLogLevel(level);

            Logs.SelectionColor = color;

            string currentTime = DateTime.Now.ToString("HH:mm:ss");

            string formattedLog = $"{currentTime} [{level}] {message}";

            Logs.AppendText(formattedLog + Environment.NewLine);

            Logs.SelectionColor = Logs.ForeColor;
        }
        private string ExtractLogLevel(string logEntry)
        {
            var match = System.Text.RegularExpressions.Regex.Match(logEntry, @"level=""(.*?)""");

            return match.Success ? match.Groups[1].Value : "UNKNOWN";
        }
        private string ExtractLogMessage(string logEntry)
        {
            var match = System.Text.RegularExpressions.Regex.Match(logEntry, @"<log4j:Message><!\[CDATA\[(.*?)\]\]></log4j:Message>");

            return match.Success ? match.Groups[1].Value : "UNKNOWN";
        }
        private Color GetColorForLogLevel(string level)
        {
            switch (level)
            {
                case "ERROR":
                    return Color.Red;
                case "WARN":
                    return Color.Orange;
                case "INFO":
                    return Color.White;
                case "DEBUG":
                    return Color.SkyBlue;
                default:
                    return Color.White;
            }
        }
        private void Minecraft_Exited(object sender, EventArgs e)
        {
            Minecraft = null;
            UpdateLaunchEvent("Ничего");
            ProgressBarLaunch.Value = 0;
            Logger.WriteNewLogItem("Minecraft завершил работу.");
        }
        private async void LoadVersionsMethodAsync()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var UlixLauncher = new MinecraftLauncher();
                var versionsMinecraft = await UlixLauncher.GetAllVersionsAsync();
                Logger.WriteNewLogItem("Версии успешно получены.");

                if (versionsMinecraft == null)
                {
                    throw new Exception("Не удалось получить версии Minecraft.");
                }

                VersionList.Items.Clear();

                foreach (var version in versionsMinecraft)
                {
                    if (!ReleaseToogleSwitch.Checked || version.Type == "release")
                    {
                        VersionList.Items.Add(version.Name);
                    }
                }

                string localVersionsPath = Path.Combine(UlixMinecraftPath, "versions");

                if (Directory.Exists(localVersionsPath))
                {
                    var localVersions = Directory.GetDirectories(localVersionsPath);

                    foreach (var versionPath in localVersions)
                    {
                        string versionName = Path.GetFileName(versionPath);
                        if (!VersionList.Items.Contains(versionName))
                        {
                            VersionList.Items.Add(versionName);
                        }
                    }
                }

                if (Settings.Default.lastPlayVersionIndex != -1)
                {
                    if (Settings.Default.lastPlayVersionIndex >= 0 &&
                        Settings.Default.lastPlayVersionIndex < VersionList.Items.Count)
                    {
                        VersionList.SelectedIndex = Settings.Default.lastPlayVersionIndex;
                    }
                    else
                    {
                        VersionList.SelectedIndex = 0;
                    }
                }

                Logger.WriteNewLogItem("Версии Minecraft успешно загружены.");
            }
            catch (IOException)
            {
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("Ошибка сети: " + ex.Message, "UlixError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteNewLogItem($"Ошибка сети: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex.Message, "UlixError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteNewLogItem($"Произошла ошибка: {ex.Message}");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            Logger.WriteNewLogItem("Приложение закрывается...");
            Application.Exit();
        }
        private void SaveSettings()
        {
            Settings.Default.Save();

            Logger.WriteNewLogItem("Настройки сохранены");
        }
        private void LoadSettings()
        {
            foreach (string acc in Settings.Default.accaunts)
            {
                if (!string.IsNullOrEmpty(acc) && !string.IsNullOrWhiteSpace(acc) && acc != "Урозы:")
                {
                    MultiAccountComboBox.Items.Add(acc);
                }
            }
        }
        private void OpenGameFolderButton_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(UlixMinecraftPath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = UlixMinecraftPath,
                    UseShellExecute = true,
                    Verb = "open"
                });
                Logger.WriteNewLogItem("Открыта папка с Minecraft.");
            }
            else
            {
                MessageBox.Show("Папка с Minecraft не найдена", "UlixError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.WriteNewLogItem("Ошибка: папка с Minecraft не найдена.");
            }
        }
        private void ResetSettingsButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("После нажатия 'Да' настройки будут сброшены. Если вы не хотите сбрасывать настройки, нажмите 'Нет'", "UlixWarning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                JavaArgsTextBox.Text = "-Xmx4G";
                OperativeMemoryUp.Value = 2040;
                ReleaseToogleSwitch.Checked = false;

                SaveSettings();
                Logger.WriteNewLogItem("Настройки сброшены");
            }
        }
        private void CreateNewMultiAccount_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(MultiAccountTextBox.Text) && !string.IsNullOrWhiteSpace(MultiAccountTextBox.Text))
            {
                if (!Settings.Default.accaunts.Contains(MultiAccountTextBox.Text))
                {
                    MultiAccountComboBox.Items.Add(MultiAccountTextBox.Text);
                    Settings.Default.accaunts.Add(MultiAccountTextBox.Text);
                    Settings.Default.Save();
                    Logger.WriteNewLogItem($"Создан новый аккаунт: {MultiAccountTextBox.Text}");
                    MultiAccountTextBox.Clear();
                }
                else
                {
                    MessageBox.Show("Такой аккаунт уже существует!", "UlixError", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ник не может быть пустым!", "UlixError", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void AuthorizationButton_Click(object sender, EventArgs e) //Авторизация через Microsoft
        {
            var loginHandler = JELoginHandlerBuilder.BuildDefault();
            var session = await loginHandler.AuthenticateInteractively();
            var accounts = loginHandler.AccountManager.GetAccounts();
            foreach (var acc in accounts)
            {
                if (acc is not JEGameAccount jEGameAcc)
                    continue;

                Settings.Default.accaunts.Add(jEGameAcc.Profile?.Username);
                MultiAccountComboBox.Items.Add(jEGameAcc.Profile?.Username);
                MultiAccountTextBox.Clear();
                SaveSettings();
                Logger.WriteNewLogItem($"Авторизован аккаунт: {jEGameAcc.Profile?.Username}");
            }
        }
        private void UpdateLaunchEvent(string text)
        {
            if (LaunchEvent.InvokeRequired)
            {
                LaunchEvent.Invoke(new Action(() => LaunchEvent.Text = text));
            }
            else
            {
                LaunchEvent.Text = text;
            }
        }
        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            Logger.PrintLogInConsole = guna2ToggleSwitch1.Checked;
            Logger.WriteNewLogItem($"Логирование в консоль: {(Logger.PrintLogInConsole ? "включено" : "выключено")}");
        }
        private void GetSkinButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(uuidTextBox.Text) && !string.IsNullOrWhiteSpace(uuidTextBox.Text))
            {
                Settings.Default.uid = uuidTextBox.Text;
                SaveSettings();
            }
        }
        private void guna2GradientButton3_Click(object sender, EventArgs e) => Clipboard.SetText(Logs.Text);
        private void guna2GradientButton4_Click(object sender, EventArgs e)
        {
            UlixModInstaller ulixModInstaller = new UlixModInstaller();
            ulixModInstaller.ShowDialog();
        }
        private void LikuurYT_Click(object sender, EventArgs e) => Process.Start("https://www.youtube.com/@likuur");
        private void Unnamed0a0YT_Click(object sender, EventArgs e) => Process.Start("https://www.youtube.com/@Unnamed0a0");
        private void LikuurGitHub_Click(object sender, EventArgs e) => Process.Start("https://github.com/Likuur/");
    }
}