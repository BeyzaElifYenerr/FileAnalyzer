using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FileAnalyzer.Tools.Analysis;
using FileAnalyzer.Logging;
using FileAnalyzer.Tools;

namespace FileAnalyzer
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Logger.Info("Uygulama başlatıldı.");

            try
            {
                var filePath = ShowFilePicker();
                if (string.IsNullOrEmpty(filePath))
                {
                    Console.WriteLine("Dosya seçilmedi. Çıkılıyor...");
                    Logger.Info("Kullanıcı dosya seçmeden çıktı.");
                    return;
                }

                Logger.Info($"Seçilen dosya: {filePath}");

                var reader = FileReaderFactory.Create(filePath);
                var text = reader.ReadAllText(filePath);
                Logger.Info($"Dosya okundu. Karakter sayısı: {text?.Length ?? 0}");

                var analyzer = new TextAnalyzer();

                var wordFreq = analyzer.ComputeWordFrequencies(text);
                var punct = analyzer.CountPunctuation(text);

                PrintSummary(wordFreq, punct);

                Logger.Info("Analiz tamamlandı.");
            }
            catch (NotSupportedException ex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Uyarı: {ex.Message}");
                Console.ResetColor();
                Logger.Error(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Bir hata oluştu: {ex.Message}");
                Console.ResetColor();
                Logger.Error(ex.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("Kapatmak için ENTER'a basın...");
            Console.ReadLine();
        }

        private static string ShowFilePicker()
        {
            Application.EnableVisualStyles();

            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Analiz edilecek dosyayı seçin";
                dialog.Multiselect = false;
                dialog.Filter = "Metin Dosyaları (*.txt)|*.txt|Word Belgeleri (*.docx)|*.docx|PDF Dosyaları (*.pdf)|*.pdf|Tüm Dosyalar (*.*)|*.*";

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

            var totalWordCount = wordFreq.Values.Sum();
            Console.WriteLine($"Toplam kelime (stopwords hariç) geçiş sayısı: {totalWordCount}");

            Console.WriteLine();
            Console.WriteLine("En çok tekrar eden 20 kelime:");
            Console.WriteLine("--------------------------------");
            foreach (var kv in wordFreq.Take(20))
            {
                Console.WriteLine($"{kv.Key,-20} : {kv.Value,5}");
            }

            Console.WriteLine();
            Console.WriteLine("Noktalama işaretleri (çoktan aza):");
            Console.WriteLine("----------------------------------");
            if (punct.Count == 0)
            {
                Console.WriteLine("(Bulunamadı)");
            }
            else
            {
                foreach (var kv in punct)
                {
                    Console.WriteLine($"'{kv.Key}' : {kv.Value}");
                }
            }
        }
    }
}
