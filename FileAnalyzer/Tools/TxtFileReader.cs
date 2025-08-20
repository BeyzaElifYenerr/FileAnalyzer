using System.IO;
using FileAnalyzer.Contracts;

namespace FileAnalyzer.Tools
{
    public class TxtFileReader : IFileReader
    {
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
