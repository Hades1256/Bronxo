using System;
using System.Diagnostics;
using System.IO;
using System.Text;


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
        public static Int32 GetArrayElementMaxLength(ref String[] a)
        {
            Int32 Result = 0;
            foreach (String str in a)
            {
                if (str.Length > Result)
                {
                    Result = str.Length;
                };
            }
            return Result;
        }
        public static void PrintLine(int TABLEWIDTH)
        {
            Console.WriteLine(new string('-', TABLEWIDTH));
        }
        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
        public static void PrintRow(int TABLEWIDTH, params string[] columns)
        {
            int width = (TABLEWIDTH - columns.Length) / columns.Length;
            string row = "|";

            foreach (string column in columns)
            {
                row += AlignCentre(column, width) + "|";
            }

            Console.WriteLine(row);
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