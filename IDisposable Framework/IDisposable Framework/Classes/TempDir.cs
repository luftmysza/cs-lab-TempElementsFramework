#nullable disable

using IDisposable_Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IDisposable_Framework.Classes
{
    public class TempDir : ITempDir
    {
    //Fields--------------------------------------------------------------------
        private bool disposed = false;
        public bool IsDestroyed { get; set; } = false;
        public string DirPath { get; }
        public bool IsEmpty { get; }
        private DirectoryInfo DirInfo { get; }

    //Constructors--------------------------------------------------------------
        public TempDir()
        {
            DirPath = Path.Combine(@"C:\Users\drboo\Downloads", "directory " + DateTime.Now.ToString("MM-dd-yy hh-mm-ss"));
            if (Directory.Exists(DirPath)) throw new IOException("A directory with such name already exists");
            DirInfo = new DirectoryInfo(DirPath);
            DirInfo.Create();
            Console.WriteLine($"A temp directory {DirInfo.FullName} created");
        }
        public TempDir(string path)
        {
            DirPath = path;
            if (Directory.Exists(DirPath)) throw new IOException("A directory with such name already exists");
            DirInfo = new DirectoryInfo(DirPath);
            DirInfo.Create();
            Console.WriteLine($"A temp directory {DirInfo.FullName} created");
        }

    //Disposal------------------------------------------------------------------
        ~TempDir()
        {
            Dispose(false);
        }
        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    DirInfo.Delete(true);
                }
                disposed = true;
                IsDestroyed = true;
            }
            Console.WriteLine($"The temp directory disposed");
        }
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    //Creates-------------------------------------------------------------------
        public void CreateSubdirectory()
        {
            string tempSubDirPath = "subdirectory " + DateTime.Now.ToString("MM-dd-yy hh-mm-ss");
            if (Directory.Exists(tempSubDirPath)) throw new IOException("A subdirectory with such name already exists");
            DirInfo.CreateSubdirectory(tempSubDirPath);
            Console.WriteLine($"A temp subdirectory {DirInfo.FullName} created");
        }
        public void CreateTempFile(string name)
        {
            string[] subdirs = DirInfo.GetDirectories().Select(x => x.Name).Concat([$"{DirInfo.Name}"]).Reverse().ToArray();
            string filePath = null;
            TempFile tempFile;

            if (subdirs.Length == 1) filePath = Path.Combine(DirPath, name);
            else
            {
                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("Select tempp file location:");
                    for (int i = 0; i < subdirs.Length; i++) 
                        Console.WriteLine($"{i} :: {subdirs[i]}");
                    try
                    {
                        int index = int.Parse(Console.ReadLine());
                        filePath = subdirs[index];
                        exit = true;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("The input is incorrect");
                    }
                }
            }
            tempFile = new TempFile(filePath);
        }
        public void CreateTempTxtFile(string name, string content = null)
        {
            string[] subdirs = DirInfo.GetDirectories().Select(x => x.Name).Concat([$"{DirInfo.Name}"]).Reverse().ToArray();
            string filePath = null;
            TempTxtFile tempFile;

            if (subdirs.Length == 1) filePath = Path.Combine(DirPath, name);
            else
            {
                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("Select temp file location:");
                    for (int i = 0; i < subdirs.Length; i++)
                        Console.WriteLine($"{i} :: {subdirs[i]}");
                    try
                    {
                        int index = int.Parse(Console.ReadLine());
                        filePath = subdirs[index];
                        exit = true;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("The input is incorrect");
                    }
                }
            }
            tempFile = new TempTxtFile(filePath);
            if (content is not null) tempFile?.WriteLine(content);

        }
    //Enumerates----------------------------------------------------------------
        public IEnumerable<FileSystemInfo> EnumerateFiles()
        {
            return DirInfo.EnumerateFiles();
        }
        public IEnumerable<FileSystemInfo> EnumerateDirectories()
        {
            return DirInfo.EnumerateDirectories();
        }
        public IEnumerable<FileSystemInfo> EnumerateFileSystemInfos()
        {
            return DirInfo.EnumerateFileSystemInfos();
        }
    //Gets----------------------------------------------------------------------
        public IEnumerable<FileSystemInfo> GetFiles()
        {
            return DirInfo.GetFiles();
        }
        public IEnumerable<FileSystemInfo> GetDirectories()
        {
            return DirInfo.GetDirectories();
        }
        public IEnumerable<FileSystemInfo> GetFileSystemInfos()
        {
            return DirInfo.GetFileSystemInfos();
        }
    //Miscellannious------------------------------------------------------------
        public void Empty() 
        {
            var directories = DirInfo.GetDirectories();
            var files = DirInfo.GetFiles();
            
            foreach (var directory in directories) directory.Delete(true);
            foreach (var file in files) file.Delete();

            Console.WriteLine($"The directory {DirInfo.FullName} successfully emptied.");
        }
        public void MoveTo()
        {
            //TODO
            string filePath = null;
            IDisposable file;

            string[] subdirs = DirInfo.GetDirectories().Select(x => x.Name).Concat([$"{DirInfo.Name}"]).Reverse().ToArray();
            //TODO string[] files = ; 
            bool exit = false;

            //TODO while (!exit)
            //TODO{
            //TODO Console.WriteLine("Select the file:");
            //TODOfor (int i = 0; i < subdirs.Length; i++)
            //TODOConsole.WriteLine($"{i} :: {subdirs[i]}");
            //TODOtry
            //TODO{
            //TODOint index = int.Parse(Console.ReadLine());
            //TODOfilePath = subdirs[index];
            //TODOexit = true;
            //TODO}
            //TODO   catch (Exception)
            //TODO {
            //TODO Console.WriteLine("The input is incorrect");
            //TODO }
            //TODO }
            exit = false;
            while (!exit)
            {
                Console.WriteLine("Select the new location:");
                for (int i = 0; i < subdirs.Length; i++)
                    Console.WriteLine($"{i} :: {subdirs[i]}");
                try
                {
                    int index = int.Parse(Console.ReadLine());
                    filePath = subdirs[index];
                    exit = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("The input is incorrect");
                }
            }

        }
        //TODOpublic override string ToString()
        //TODO {
        //TODOreturn $"";
        //TODO}
    }
}
