using System;
using System.IO;
using FileAnalyzer.Contracts;

namespace FileAnalyzer.Tools
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
