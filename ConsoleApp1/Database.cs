using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace ConsoleApp1
{
    class Database
    {
        private SQLiteConnection dbConnect;
        private SQLiteCommand dbCmd;
        public Boolean IsConnected { get; private set; }
        public SQLiteDataReader ExecuteReader
        {
            get
            {
                return dbCmd.ExecuteReader();
            }

        }
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
            dbCmd.CommandText = SQLcommand;
            try
            {
                Result = dbCmd.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {

                switch (ex.ErrorCode)
                {
                    //case 1:                 //Message = "SQL logic error\r\ntable Projects has no column named Descr" ex.ErrorCode==1 ;Message = "SQL logic error\r\nnear \")\": syntax error"
                    //break;
                    case 19:                //Message = "constraint failed\r\nUNIQUE constraint failed: Users.Name" ex.ErrorCode==19
                        new Master().WriteMessage(new String[] { "Non unique value inserted" });
                        break;
                    default:
                        new Master().WriteMessage(new String[] { ex.Message });
                        break;
                }
                if (ex.ErrorCode == 19)
                {

                }
            }
            //
            return Result;
        }
        private String ShowInputMaster(string colname)
        {
            String Result = "";
            new Master().WriteMessage(true, new String[] { "Input parameter value, you wish to show the row of" });
            Result = Console.ReadLine();
            return Result;
        }
        public void printTable(String tblname, String colname, int TABLEWIDTH)
        {

            using (SQLiteDataReader dr = dbCmd.ExecuteReader())
            {
                if (dr.HasRows)
                {
                    String[] row = new String[dr.FieldCount];
                    //чтение заголовка таблицы(имена столбцов)}
                    for (Int32 i = 0; i < dr.FieldCount; i++)
                    {
                        row[i] = dr.GetName(i).ToString().Trim();
                    }
                    ext_utils.PrintRow(TABLEWIDTH, row);
                    Int32 RowCounter = 0;
                    while (dr.Read())
                    {
                        for (Int32 i = 0; i < dr.FieldCount; i++)
                        {
                            row[i] = dr.GetValue(i).ToString().Trim();
                        }
                        ext_utils.PrintRow(TABLEWIDTH, row);
                        RowCounter++;
                    }
                    Console.WriteLine("Number of found rows: {0}", RowCounter);
                }
                else
                {
                    new Master().WriteMessage(new string[] { "The query did't give any results." });
                }
            }
        }
        //Конструктор
        public Database(String FileName)
        {
            IsConnected = false;
            dbConnect = new SQLiteConnection();
            dbCmd = new SQLiteCommand();
            dbConnect.ConnectionString = "Data Source = " + FileName + "; Version = 3; FailIfMissing=True; Journal Mode=Persist;";
            if (File.Exists(FileName))
            {
                try
                {
                    Connect();
                    dbCmd.Connection = dbConnect;
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