using System;
using System.Diagnostics;
using System.IO;


namespace ConsoleApp1
{
    public enum Rights { eUser =1, eAdmin = 2 };
    public class ext_utils       
    {
        public static bool IsValidFilename(string filename)
        {
            try
            {
                File.OpenRead(filename).Close();
            }
            catch (ArgumentException) { return false; }
            catch (Exception) { }
            return true;
        }
        public static void Pause()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }
        public static void Closeapp()
        {
            Pause();
            Process.GetCurrentProcess().Kill();
        }
    }
}