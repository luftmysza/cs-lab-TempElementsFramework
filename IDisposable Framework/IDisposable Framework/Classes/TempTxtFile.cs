using IDisposable_Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDisposable_Framework.Classes
{
    internal class TempTxtFile : TempFile, ITempFile
    {
        private StreamReader StreamOut;
        private StreamWriter StreamIn;

        public TempTxtFile() : base()
        {
            StreamOut = new StreamReader(base.fileStream);
            StreamIn = new StreamWriter(base.fileStream);
        }
        public TempTxtFile(string path) : base(path)
        {
            StreamOut = new StreamReader(base.fileStream);
            StreamIn = new StreamWriter(base.fileStream);
        }
        public string? ReadLine()
        {
            return StreamOut.ReadLine();
        }
        public string ReadAllText()
        {
            return StreamOut.ReadToEnd();
        }
        public void Write(string? text)
        {
            StreamIn.Write(text);
        }
        public void WriteLine(string? line)
        {
            StreamIn.WriteLine(line);
        }
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    fileStream?.Dispose();
                    StreamOut?.Dispose();
                    StreamIn?.Dispose();
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
