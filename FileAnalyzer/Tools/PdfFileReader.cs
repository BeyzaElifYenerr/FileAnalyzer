using System.Text;
using UglyToad.PdfPig;

namespace FileAnalyzer.Readers
{
    public class PdfFileReader : IFileReader
    {
        public string ReadAllText(string path)
        {
            var sb = new StringBuilder();

            using (var doc = PdfDocument.Open(path))
            {
                foreach (var page in doc.GetPages())
                {
                    sb.AppendLine(page.Text);
                }
            }
            return sb.ToString();
        }
    }
}
