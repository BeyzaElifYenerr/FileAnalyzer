using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileAnalyzer.Logging;

namespace FileAnalyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Logger.EnsureLogsFolder();
            Logger.Info("Program Başlatıldı.");
            try
            {
                Console.WriteLine("Bu bir test logudur.");
                Logger.Info("Test logu yazıldı.");
            }
            catch (Exception ex)
            {
                Logger.Error("Bir hata oluştu", ex);
            }

            Console.WriteLine("Bitti. Çıkmak için bir tuşa basın...");
            Console.ReadKey();
        }
    }
}
