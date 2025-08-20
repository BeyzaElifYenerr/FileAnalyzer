using Xceed.Words.NET;

namespace FileAnalyzer.Readers
{
    public class DocxFileReader : IFileReader 
    {
        public string ReadAllText(string path)
        {
            using (var doc = DocX.Load(path))
            {
                return doc.Text;
            }
        }
    }
}
