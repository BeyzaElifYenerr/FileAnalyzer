using System.IO;

namespace FileAnalyzer.Readers
{
    public class TxtFileReader : IFileReader
    {
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
