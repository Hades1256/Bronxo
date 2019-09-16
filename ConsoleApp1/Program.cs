using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting ready to work!");
            if (ConnectToDB())
            {
                //Some code here
                CloseConnectionToDB();
            };
            Pause();
        }
    }
}
