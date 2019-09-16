using System;
using System.Data.SQLite;
using System.IO;



namespace ConsoleApp1
{
    public enum Rights { eUser =1, eAdmin = 2 };
    public partial class Program
    {

        static SQLiteConnection dbConnect;
        static SQLiteCommand sqlCmd;
        static string lbStatusText = "";
        static void Pause()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }

        static Boolean CreateDB(string dbFileName)
        {
            Boolean Result = false;
            SQLiteConnection.CreateFile(dbFileName);

            try
            {
                dbConnect.Open();
                sqlCmd.Connection = dbConnect;

                sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Catalog (id INTEGER PRIMARY KEY AUTOINCREMENT, author TEXT, book TEXT)";
                sqlCmd.ExecuteNonQuery();

                dbConnect.Close();
                Result = true;
            }
            catch (SQLiteException ex)
            {
                lbStatusText = "Disconnected";
                Console.WriteLine("Error: " + ex.Message);
            }
            return Result;
        }
        static Boolean ConnectToDB()
        {
            Boolean Result = false;
            string dbName = "Database.sqlite";
            dbConnect = new SQLiteConnection();
            sqlCmd = new SQLiteCommand();
            dbConnect.ConnectionString = "Data Source = " + dbName + "; Version = 3; ";
            if (!File.Exists(dbName))
                if (!CreateDB(dbName))
                {
                    Console.WriteLine("Couldn't create or connect to Data Base. Call system administrator, please \n Error 0x0001");
                }
                else
                {
                    try
                    {
                        //dbConnect.ConnectionString = "Data Source = " + dbName + "; Version = 3; ";
                        dbConnect.Open();
                        Result = true;
                    }
                    catch (SQLiteException ex)
                    {
                        lbStatusText = "Disconnected";
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            return Result;
        }
        static void CloseConnectionToDB()
        {
            dbConnect.Close();
        }
    }
}