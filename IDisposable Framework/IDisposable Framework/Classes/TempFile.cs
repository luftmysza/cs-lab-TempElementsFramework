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
        public bool IsDestroyed { get; set; }
        public readonly FileStream fileStream;
        public readonly FileInfo fileInfo;
        public TempFile() 
        {
            FilePath = Path.GetTempFileName();
            fileInfo = new FileInfo(FilePath);
            fileStream = fileInfo.Open
            (
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite
            );
            Console.WriteLine($"File {fileInfo.FullName} created");
        }
        public TempFile(string path)
        {
            FilePath = path;
            fileInfo = new FileInfo(FilePath);
            fileStream = fileInfo.Open
            (
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite
            );
            Console.WriteLine($"File {fileInfo.FullName} created");
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
                    fileStream?.Dispose();
                }
                IsDestroyed = true;
                disposed = true;
            }
        }
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}