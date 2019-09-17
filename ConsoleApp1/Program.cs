using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    //Preparations
    public partial class Program
    {
        static Database db;

        static void Preparing()
        {

            Console.WriteLine("Getting ready to work!");
            //string dbName = "Database.sqlite";
            Console.WriteLine("Input the Data Base file name:");
            string dbName = Console.ReadLine();
            if (ext_utils.IsValidFilename(dbName))
            {
                Console.WriteLine("Trying to connect to: " + Environment.CurrentDirectory + "\\" + dbName);
                db = new Database(dbName);
            }
            else
            {
                Console.WriteLine("Invalid File Name: "+ dbName+"\nName should be string without quotes with or without full path to file.\nProgram will be closed.");
                ext_utils.Closeapp();
            }
        }
        static void Closing()
        {
            ext_utils.Pause();
            db.Close();
        }
//Main
        static void Main(string[] args)
        {
            Preparing();
            if (db.IsConnected)
            {
                //Some code here                
            }
            else
            {
                Console.WriteLine("No valid Data Base is selected.\nApplication will be shut down now.");
            };
            Closing();
        }
    }
}
