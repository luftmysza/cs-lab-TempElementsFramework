using IDisposable_Framework.Classes;
using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\drboo\Downloads\tempfile.txt";
           
            TempFile file = new TempFile(path);
   
            
            Console.WriteLine("the file is being disposed of now..");
            
            Console.WriteLine("the file is disposed of.");

            try
            {
                string text = "Hello World!";
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
                try
                {
                    ((IDisposable)file).Dispose();
                }
                catch (Exception)
                {
                    Console.WriteLine("Exception caught while disposing");
                }
            }
        }
    }
}
