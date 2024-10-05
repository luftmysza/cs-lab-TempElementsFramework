using IDisposable_Framework.Classes;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Main_1_1();
            //Main_1_2();
            //Main_1_3();
            //Main_2_1();
            Main_4_1();
        }

        static void Main_1_1()
        {
            string path = @"C:\Users\drboo\Downloads\tempfile.txt";

            TempFile file = new TempFile(path);

            try
            {
                string text = "Hello World from try catch clause";
                file.AddText(text);
                Console.WriteLine("text added");
                ((IDisposable)file).Dispose();
            }
            catch (Exception)
            {
                Console.WriteLine("Exception caught");
            }
            finally
            {
                Console.WriteLine("the file is being disposed of now..");
                try
                {
                    ((IDisposable)file).Dispose();
                    Console.WriteLine("the file is disposed of.");

                }
                catch (Exception)
                {
                    Console.WriteLine("Exception caught while disposing");
                }
            }
        }
        static void Main_1_2()
        {
            string path = @"C:\Users\drboo\Downloads\tempfile.txt";

            using (TempFile file = new TempFile(path))
            {
                file.AddText("Hello World from using clause");
            }
        }
        static void Main_1_3()
        {
            string path = @"C:\Users\drboo\Downloads\tempfile.txt";

            TempFile file = new TempFile(path);
            
            file.AddText("Hello World from main method");

            ((IDisposable)file).Dispose();

            if (!file.IsDestroyed) Console.WriteLine("The file is in use");
            else Console.WriteLine("The file is disposed of");
        }
        static void Main_2_1()
        {
            string path = $"C:{Path.DirectorySeparatorChar}Users{Path.DirectorySeparatorChar}drboo{Path.DirectorySeparatorChar}Downloads{Path.DirectorySeparatorChar}tempfile.txt";

            TempTxtFile file = new TempTxtFile(path);

            for (int i = 1; i <= 10; i++) file.WriteLine($"line {i}");

            string[] fileRead = file.ReadAllText().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in fileRead) Console.WriteLine(line);

            string? fileLineRead = file.ReadLine();
            if (fileLineRead is not null) Console.WriteLine(fileLineRead);
            fileLineRead = file.ReadLine();
            if (fileLineRead is not null) Console.WriteLine(fileLineRead);
        }
        static void Main_4_1()
        {
            //TempTxtFile file = new TempTxtFile(path);

            //Directory.Delete(@"C:\Users\drboo\Downloads\Temp");

            //file.AddText("Hello World!");

            TempDir dir = new TempDir();

            dir.CreateSubdirectory();

            dir.CreateTempTxtFile("smth", "Hello World!");

            dir.Empty();
        }
    }
}
