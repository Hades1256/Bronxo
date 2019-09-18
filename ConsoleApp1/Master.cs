using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Master
    {
        public Char Key { get; private set; }
        private static Int32 GetArrayElementMaxLength(ref String[] a)
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
        public static Char GetKeyPress(Char[] validChars)
        {
            ConsoleKeyInfo keyPressed;
            bool valid = false;

            Console.WriteLine();
            do
            {
                keyPressed = Console.ReadKey();
                Console.WriteLine();
                if (Array.Exists(validChars, ch => ch.Equals(Char.ToUpper(keyPressed.KeyChar))))
                    valid = true;

            } while (!valid);
            return keyPressed.KeyChar;
        }
        public void WriteMessage(String[] hint, Char[] chars)
        {
            Console.Clear();
            Int32 length = 0;
            String decorStrStar = "",
                decorStr = "";
            length = GetArrayElementMaxLength(ref hint);
            for (Int32 i = 0; i < length + 4; i++)
            {
                decorStrStar += "*";
            }
            Console.WriteLine(decorStrStar);
            for (int i = 1; i < hint.Length; i++)
            {
                decorStr = "* " + hint[i];
                if (decorStr.Length < (length + 2))
                {
                    for (int j = decorStr.Length; j < (length + 2); j++)
                        decorStr += " ";
                }
                Console.WriteLine(decorStr + " *");
            }
            Console.WriteLine(decorStrStar);
            Console.WriteLine("");
            Console.Write(hint[0]);
            Key = Char.ToUpper(GetKeyPress(chars));
        }
        public void WriteMessage(String[] hint)
        {
            Console.Clear();
            Int32 length = 0;
            String decorStrStar = "",
                decorStr = "";
            length = GetArrayElementMaxLength(ref hint);
            ConsoleKeyInfo keyPressed;
            for (Int32 i = 0; i < length + 4; i++)
            {
                decorStrStar += "*";
            }
            Console.WriteLine(decorStrStar);
            for (int i = 0; i < hint.Length; i++)
            {
                decorStr = "* " + hint[i];
                if (decorStr.Length<(length+2))
                {
                    for (int j = decorStr.Length; j < (length+2); j++)
                        decorStr += " ";
                }
                Console.WriteLine(decorStr+" *");
            }
            Console.WriteLine(decorStrStar);
            ext_utils.Pause();
            Console.Clear();
        }
        //Constructor
        public Master()
        {
            Key = '\u0000';            
        }
    }
}
