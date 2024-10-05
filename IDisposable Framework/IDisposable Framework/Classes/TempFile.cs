using IDisposable_Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDisposable_Framework.Classes
{
    public class TempFile : ITempFile
    {
        protected bool disposed = false;
        public string FilePath { get; }
        public bool IsDestroyed { get; set; } = false;
        public readonly FileStream fileStream;
        public readonly FileInfo fileInfo;
        public TempFile() 
        {
            FilePath = Path.Combine(@"C:\Users\drboo\Downloads", Path.GetRandomFileName());
            if (File.Exists(FilePath)) throw new IOException("A file with such name already exists");
            fileInfo = new FileInfo(FilePath);
            fileStream = fileInfo.Open
            (
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite
            );
            Console.WriteLine($"A temp file {fileInfo.FullName} created");
        }
        public TempFile(string path)
        {
            FilePath = path;
            if (File.Exists(FilePath)) throw new IOException("A file with such name already exists");
            fileInfo = new FileInfo(FilePath);
            fileStream = fileInfo.Open
            (
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite
            );
            Console.WriteLine($"A temp file {fileInfo.FullName} created");
        }
        ~TempFile() 
        {
            Dispose(false);
        }
        public void AddText(string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fileStream.Write(info, 0, info.Length);
            fileStream.Flush();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    fileInfo.Delete();
                    fileStream?.Dispose();
                }
                IsDestroyed = true;
                disposed = true;
            }
            Console.WriteLine($"The temp file disposed");
        }
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}