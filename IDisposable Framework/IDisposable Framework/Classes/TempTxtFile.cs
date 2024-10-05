using IDisposable_Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDisposable_Framework.Classes
{
    public class TempTxtFile : TempFile, ITempFile
    {
        private StreamReader StreamOut;
        private StreamWriter StreamIn;

        public TempTxtFile() : base()
        {
            StreamOut = new StreamReader(base.fileStream);
            StreamIn = new StreamWriter(base.fileStream);
            Console.WriteLine($"A temp text file {fileInfo.FullName} created");
        }
        public TempTxtFile(string path) : base(path)
        {
            StreamOut = new StreamReader(base.fileStream);
            StreamIn = new StreamWriter(base.fileStream);
            Console.WriteLine($"A temp text file {fileInfo.FullName} created");
        }
        public string? ReadLine()
        {
            if(base.fileStream.Position != 0) base.fileStream.Position = 0;
            return StreamOut.ReadLine();
        }
        public string ReadAllText()
        {
            base.fileStream.Position = 0;
            StreamOut.DiscardBufferedData();
            return StreamOut.ReadToEnd();
        }
        public void Write(string? text)
        {
            StreamIn.Write(text);
            StreamIn.Flush();
        }
        public void WriteLine(string? line)
        {
            StreamIn.WriteLine(line);
            StreamIn.Flush();
        }
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    fileInfo.Delete();
                    fileStream?.Dispose();
                    StreamOut?.Dispose();
                    StreamIn?.Dispose();
                }
                IsDestroyed = true;
                disposed = true;
            }
            Console.WriteLine($"The temp text file disposed");
        }
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
