using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
