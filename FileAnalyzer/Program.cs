using System;
using System.Text;
using System.Windows.Forms;
using FileAnalyzer.Tools;
using FileAnalyzer.Tools.Analysis;
using FileAnalyzer.Logging;
using System.Linq;

namespace FileAnalyzer
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Logger.Info("Uygulama başlatıldı.");

            while (true) // döngü
            {
                Console.Clear();
                Console.WriteLine("=== DOSYA ANALİZ UYGULAMASI ===");
                Console.WriteLine("Merhaba!");
                Console.WriteLine("1 - TXT dosyası seç");
                Console.WriteLine("2 - PDF dosyası seç");
                Console.WriteLine("3 - DOCX dosyası seç");
                Console.Write("Seçiminizi yapın: ");
                var choice = Console.ReadLine();

                string filter = "";
                switch (choice)
                {
                    case "1": filter = "Metin Dosyaları (*.txt)|*.txt"; break;
                    case "2": filter = "PDF Dosyaları (*.pdf)|*.pdf"; break;
                    case "3": filter = "Word Belgeleri (*.docx)|*.docx"; break;
                    default:
                        Console.WriteLine("Geçersiz seçim! Enter’a basın...");
                        Console.ReadLine();
                        continue;
                }

                try
                {
                    var filePath = ShowFilePicker(filter);
                    if (string.IsNullOrEmpty(filePath))
                    {
                        Console.WriteLine("Dosya seçilmedi.");
                        Logger.Info("Kullanıcı dosya seçmedi.");
                    }
                    else
                    {
                        Logger.Info($"Seçilen dosya: {filePath}");

                        var reader = FileReaderFactory.Create(filePath);
                        var text = reader.ReadAllText(filePath);

                        var analyzer = new TextAnalyzer();
                        var wordFreq = analyzer.ComputeWordFrequencies(text);
                        var punct = analyzer.CountPunctuation(text);

                        PrintSummary(wordFreq, punct);

                        Logger.Info("Analiz tamamlandı.");
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                    Console.ResetColor();
                    Logger.Error(ex.ToString());
                }

                Console.WriteLine();
                Console.WriteLine("Analize devam edilsin mi?");
                Console.WriteLine("1 - Evet, ana menüye dön");
                Console.WriteLine("2 - Hayır, çıkış yap");
                Console.Write("Seçiminiz: ");
                var again = Console.ReadLine();

                if (again == "2")
                {
                    Console.WriteLine("Program sonlandırılıyor...");
                    break;
                }
            }
        }

        private static string ShowFilePicker(string filter)
        {
            Application.EnableVisualStyles();

            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Analiz edilecek dosyayı seçin";
                dialog.Multiselect = false;
                dialog.Filter = filter;

                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    return dialog.FileName;
                }
            }

            return null;
        }

        private static void PrintSummary(
            System.Collections.Generic.Dictionary<string, int> wordFreq,
            System.Collections.Generic.Dictionary<char, int> punct)
        {
            Console.WriteLine();
            Console.WriteLine("========= ANALİZ ÖZETİ =========");
            Console.WriteLine($"Farklı kelime sayısı: {wordFreq.Count}");

            int totalWordCount = 0;
            foreach (var v in wordFreq.Values)
                totalWordCount += v;
            Console.WriteLine($"Toplam kelime (stopwords hariç): {totalWordCount}");

            Console.WriteLine();
            Console.WriteLine("En çok tekrar eden 20 kelime:");
            foreach (var kv in wordFreq.Take(20))
                Console.WriteLine($"{kv.Key,-20} : {kv.Value}");

            Console.WriteLine();
            Console.WriteLine("Noktalama işaretleri:");
            foreach (var kv in punct)
                Console.WriteLine($"'{kv.Key}' : {kv.Value}");
        }
    }
}
