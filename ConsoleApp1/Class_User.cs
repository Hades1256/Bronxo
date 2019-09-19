using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class User
    {
        private UInt16 Id;
        private Byte AccessLevel=0;
        public String Name { get; set; }
        public String SQLstring { get; set; }
        /*public Rights GetRights()
        {
            Rights Result = 0;
            switch (AccessLevel)
            {
                case 1:
                    Result = Rights.eUser;
                    break;
                case 2:
                    Result = Rights.eAdmin;
                    break;
                default:
                    Result = Rights.eUser;
                    break;
            }
            return Result;
        }*/
        public String AddMaster()//
        // Сводка:
        //     Возвращает SQL запрос.
        //
        // Возврат:
        //     Строка типа String.
        {
            String Result = "";
            Master _master = new Master();
            Console.Clear();
            Console.WriteLine("Write User name to add");
            Name = Console.ReadLine();
            _master.WriteMessage(new String[] { "You are about to add a new user to Data Base.", "Continue? (y/n)" },new char[] {'Y'});
            if (_master.Key == 'Y') Result = SQLAdd();
            return Result;
        }
        public String ShowMaster(Boolean allFlag)//
        // Сводка:
        //     Возвращает SQL запрос.
        //
        // Возврат:
        //     Строка типа String.
        {
            String Result = "";
            if (allFlag)
            {
                Result = SQLShow(true);
            }
            else
            {
                Master _master = new Master();
                Console.Clear();
                Console.WriteLine("Write User name to show");
                Name = Console.ReadLine();
                _master.WriteMessage(new String[] { String.Format("You are about to show a user: {0} from Data Base.", Name), "Continue? (y/n)" }, new char[] { 'Y' });
                if (_master.Key == 'Y') Result = SQLShow(Name);
            }
            return Result;
        }
        public String SQLAdd()
        {
            //SQL запрос
            String Result = "";
            Result = String.Format("INSERT INTO Users (Name) VALUES('{0}'); ", Name);
            return Result;
        }
        public static String SQLShow(Boolean AllFlag)
        {
            //SQL запрос
            String Result = "";
            Result = @"SELECT * FROM 'Users'; ";
            return Result;
        }
        public static String SQLShow(String val)
        {
            //SQL запрос
            String Result = "";
            Result = String.Format(@"SELECT * FROM 'Users' WHERE Name LIKE '{0}'; ", val);
            return Result;
        }
        static void DeleteUser(String name)
        {
            //SQL запрос command.CommandText = String.Format(@"DELETE FROM 'users' WHERE ""Name"" = '{0}'; ",Name);

        }
        public User()
        {
            Name = "";
            SQLstring = "";
        }
    }
}
