using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
