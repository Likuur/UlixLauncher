using System;
using System.Net;
using System.Net.Http;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Ulix.Utils
{
    public class Updater
    {
        private const string url = "https://raw.githubusercontent.com/Likuur/UlixLauncher/refs/heads/main/OtherShit/version.txt";
        public static string CurrentAppVersion = "1.0.5 BETA-TEST";
        public static async void DownladNewVersion()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string linkContent = await response.Content.ReadAsStringAsync();

                    string linkPrefix = "Link: ";
                    if (linkContent.Contains(linkPrefix))
                    {
                        int startIndex = linkContent.IndexOf(linkPrefix) + linkPrefix.Length;
                        string link = linkContent.Substring(startIndex).Trim();

                        using (WebClient webClient = new WebClient())
                        {
                            Logger.WriteNewLogItem("Загрузка обновления Ulix...");
                            webClient.DownloadFile(link, "Updater.zip");
                        }
                    }

                    Logger.WriteNewLogItem("Распаковка файлов установщика..");
                    ZipFile.ExtractToDirectory("Updater.zip", Path.GetTempPath());

                    Logger.WriteNewLogItem("Запуск установщика...");
                    Process.Start(Path.Combine(Path.GetTempPath(), "UlixUpdater.exe"));

                    Logger.WriteNewLogItem("Выход из приложения");
                    Application.Exit();
                }
                catch (HttpRequestException ex)
                {
                    Logger.WriteNewLogItem($"Ошибка при скачивании новой версии Ulix: {ex.Message}");
                }
            }
        }
        public static string GetUlixVersion()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string versionContent = client.GetStringAsync(url).Result;

                    string versionPrefix = "Version: ";
                    if (versionContent.Contains(versionPrefix))
                    {
                        int startIndex = versionContent.IndexOf(versionPrefix) + versionPrefix.Length;
                        string version = versionContent.Substring(startIndex).Trim();
                        Logger.WriteNewLogItem("Версия Ulix: " + version);
                        return version;
                    }
                }
                catch (AggregateException ex)
                {
                    foreach (var innerEx in ex.InnerExceptions)
                    {
                        if (innerEx is HttpRequestException httpEx)
                        {
                            Logger.WriteNewLogItem($"Ошибка при получении версии Ulix: {httpEx.Message}");
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}