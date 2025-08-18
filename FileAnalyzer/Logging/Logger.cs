using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAnalyzer.Logging
{
    public static class Logger
    {
        private static readonly object _lock = new object();
        private static readonly string _logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");

        private static string CurrentLogFile =>
            Path.Combine(_logDir, $"log-{DateTime.Now:yyyyMMdd}.txt");
        static Logger()
        {
            Directory.CreateDirectory(_logDir);
        }

        public static void Info(string message) => Write("INFO", message);
        public static void Error(string message) => Write("ERROR", message);

        private static void Write(string level, string message)
        {
            var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
            lock (_lock)
            {
                File.AppendAllText(CurrentLogFile, line + Environment.NewLine, Encoding.UTF8);
            }
        }
    }
}
