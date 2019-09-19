using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace ConsoleApp1
{
    class Database
    {
        private SQLiteConnection dbConnect;
        private SQLiteCommand sqlCmd;
        public Boolean IsConnected { get; private set; }
        private void Connect()
        {
            Console.WriteLine(dbConnect.State);
            dbConnect.Open();
            Console.WriteLine(dbConnect.State);
            IsConnected = true;
        }
        private void Disconnect()
        {
            dbConnect.Close();
            IsConnected = false;
        }
        /*public Boolean CreateDB(string dbFileName)
        {
            Boolean Result = false;
            SQLiteConnection.CreateFile(dbFileName);

            try
            {
                Connect();
                sqlCmd.Connection = dbConnect;

                //sqlCmd.CommandText = "CREATE TABLE IF NOT EXISTS Catalog (id INTEGER PRIMARY KEY AUTOINCREMENT, author TEXT, book TEXT)";
                //"CREATE TABLE IF NOT EXISTS Users (ID INTEGER PRIMARY KEY ASC AUTOINCREMENT UNIQUE, Access INT(2) DEFAULT(0) NOT NULL, Name STRING(64) NOT NULL UNIQUE ON CONFLICT FAIL)";
                //"PRAGMA foreign_keys = ON";

                sqlCmd.ExecuteNonQuery();

                Disconnect();
                Result = true;
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            return Result;
        }*/
        public int ExecuteCommand(String SQLcommand)
        {
            int Result = 0;
            sqlCmd.CommandText = SQLcommand;
            try
            {
                Result = sqlCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                if (ex.ErrorCode==19) //ex.Message = "constraint failed\r\nUNIQUE constraint failed: Users.Name"
                //if (ex.Message.Contains("UNIQUE constraint")) //ex.Message = "constraint failed\r\nUNIQUE constraint failed: Users.Name"
                {
                    new Master().WriteMessage(new String[] { "Non unique value inserted" });
                }
            }
            //
            return Result;
        }
        //Конструктор
        public Database(String FileName)
        {
            IsConnected = false;
            dbConnect = new SQLiteConnection();
            sqlCmd = new SQLiteCommand();
            dbConnect.ConnectionString = "Data Source = " + FileName + "; Version = 3; FailIfMissing=True; Journal Mode=Persist;";
            if (File.Exists(FileName))
            {
                try
                {
                    Connect();
                    sqlCmd.Connection = dbConnect;
                }
                catch (SQLiteException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    ext_utils.Closeapp();
                }
            }
        }
        public void Close()
        {
            try
            {
                if (IsConnected)
                    Disconnect();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}