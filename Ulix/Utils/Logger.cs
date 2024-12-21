using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ulix.Utils
{
    public static class Logger
    {
        /// <summary>
        /// Определяет путь к файлу логов
        /// </summary>
        public static string LogFilePath;

        /// <summary>
        /// Если true, записывает лог не в лог файл, а в консоль
        /// </summary>
        public static bool PrintLogInConsole { get; set; } = false;

        /// <summary>
        /// Если true, добавляет к каждому логу время
        /// </summary>
        public static bool AddLogTime { get; set; } = true;

        /// <summary>
        /// Создаёт новый лог файл по указанному пути
        /// </summary>
        public static void CreateNewLog(string path)
        {
            LogFilePath = path;
            if (File.Exists(LogFilePath))
            {
                File.Delete(LogFilePath);
            }
            File.Create(LogFilePath).Dispose();
        }

        /// <summary>
        /// Удаляет лог файл по указанному пути
        /// </summary>
        public static void DeleteLog(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                Console.WriteLine($"Файл не найден: {path}");
            }
        }

        /// <summary>
        /// Удаляет строчку в лог файле с указанным контентом
        /// </summary>
        public static void DeleteLogItem(string itemContent, string path = null)
        {
            string logPath = path ?? LogFilePath;

            if (!File.Exists(logPath))
            {
                Console.WriteLine("Лог файл не найден.");
                return;
            }

            var lines = File.ReadAllLines(logPath).ToList();
            lines.RemoveAll(line => line.Contains(itemContent));

            File.WriteAllLines(logPath, lines);
        }

        /// <summary>
        /// Записывает новую строчку в лог файл с указанным текстом
        /// </summary>
        public static void WriteNewLogItem(string content, string path = null)
        {
            string logPath = path ?? LogFilePath;

            if (string.IsNullOrEmpty(logPath) && !PrintLogInConsole)
            {
                Console.WriteLine("Путь к лог файлу не задан и PrintLogInConsole не установлен.");
                return;
            }

            string logEntry = AddLogTime ? $"{DateTime.Now}: {content}" : content;

            if (PrintLogInConsole)
            {
                Console.WriteLine(logEntry);
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine(logEntry);
                }
            }
        }

        /// <summary>
        /// Получает все записи из лог файла
        /// </summary>
        public static IEnumerable<string> GetLogs(string logPath)
        {
            if (File.Exists(logPath))
            {
                return File.ReadAllLines(logPath);
            }
            else
            {
                Console.WriteLine($"Лог файл не найден: {logPath}");
                return Enumerable.Empty<string>();
            }
        }

        /// <summary>
        /// Получает путь к лог файлу
        /// </summary>
        public static string GetLogFilePath()
        {
            return LogFilePath;
        }

        /// <summary>
        /// Очищает содержимое лог файла
        /// </summary>
        public static void ClearLog(string path)
        {
            string logPath = path;

            if (!File.Exists(logPath))
            {
                Console.WriteLine("Лог файл не найден.");
                return;
            }

            File.WriteAllText(logPath, string.Empty);
        }
        /// <summary>
        /// Очищает лог
        /// </summary>
        public static void ClearLog()
        {
            if (!File.Exists(LogFilePath))
            {
                Console.WriteLine("Лог файл не найден.");
                return;
            }

            File.WriteAllText(LogFilePath, string.Empty);
        }
    }
}