using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace FileAnalyzer.Tools
{
    public class DocxFileReader : FileAnalyzer.Contracts.IFileReader
    {
        public string ReadAllText(string path)
        {
            var sb = new StringBuilder();

            using (var doc = WordprocessingDocument.Open(path, false))
            {
                var body = doc.MainDocumentPart?.Document?.Body;
                if (body == null) return string.Empty;


                foreach (var para in body.Elements<Paragraph>())
                {
                    foreach (var text in para.Descendants<Text>())
                        sb.Append(text.Text);

                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }
    }
}
