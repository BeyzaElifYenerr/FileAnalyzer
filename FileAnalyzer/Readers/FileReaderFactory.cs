using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileAnalyzer.Readers
{
    public static class FileReaderFactory
    {
        public static IFileReader Create(string path)
        {
            var ext = Path.GetExtension(path)?.ToLowerInvariant();
            switch (ext)
            {
                case ".txt": return new TxtFileReader();
                case ".docx": return new DocxFileReader();
                case ".pdf": return new PdfFileReader();
                default:
                    throw new NotSupportedException($"Desteklenmeyen dosya uzantısı: {ext}");
            }
        }
    }
}
