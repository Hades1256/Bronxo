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
            //String decorStrStar = "",
            String decorStr = "";
            length = ext_utils.GetArrayElementMaxLength(ref hint);
            ext_utils.PrintLine(length + 4);
            for (int i = 0; i < (hint.Length-1); i++)
            {
                decorStr = "| " + hint[i];
                if (decorStr.Length < (length + 2))
                {
                    for (int j = decorStr.Length; j < (length + 2); j++)
                        decorStr += " ";
                }
                Console.WriteLine(decorStr + " |");
            }
            ext_utils.PrintLine(length + 4);
            Console.WriteLine();
            Console.Write(hint[hint.Length-1]);
            Key = Char.ToUpper(GetKeyPress(chars));
        }
        public void WriteMessage(String[] hint)
        {
            Console.Clear();
            Int32 length = 0;
            //String decorStrStar = "",
            String decorStr = "";
            length = ext_utils.GetArrayElementMaxLength(ref hint);
            //ConsoleKeyInfo keyPressed;
            ext_utils.PrintLine(length + 4);
            for (int i = 0; i < hint.Length; i++)
            {
                decorStr = "| " + hint[i];
                if (decorStr.Length<(length+2))
                {
                    for (int j = decorStr.Length; j < (length+2); j++)
                        decorStr += " ";
                }
                Console.WriteLine(decorStr+" |");
            }
            ext_utils.PrintLine(length + 4);
            ext_utils.Pause();
            Console.Clear();
        }
        public void WriteMessage(Boolean ReturnString,String[] hint)
        {
            //String Result = "";
            Console.Clear();
            Int32 length = 0;
            String decorStr = "";
            length = ext_utils.GetArrayElementMaxLength(ref hint);
            ext_utils.PrintLine(length + 4);
            for (int i = 0; i < hint.Length; i++)
            {
                decorStr = "| " + hint[i];
                if (decorStr.Length < (length + 2))
                {
                    for (int j = decorStr.Length; j < (length + 2); j++)
                        decorStr += " ";
                }
                Console.WriteLine(decorStr + " |");
            }
            ext_utils.PrintLine(length + 4);
            //ext_utils.Pause();
            Console.Clear();
            //return Result;
        }
        //Constructor
        public Master()
        {
            Key = '\u0000';            
        }
    }
}
