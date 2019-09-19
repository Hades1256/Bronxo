using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Task
    {
        private UInt32 ID;
        private UInt32 ProjectID;
        private String Type;
        private String Priority;
        private String Creator;
        private String Name;
        private String Description;

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
                Console.WriteLine("Write Task name to show");
                Name = Console.ReadLine();
                _master.WriteMessage(new String[] { String.Format("You are about to show a user: {0} from Data Base.", Name), "Continue? (y/n)" }, new char[] { 'Y' });
                if (_master.Key == 'Y') Result = SQLShow(Name);
            }
            return Result;
        }
        static void SQLAdd()
        {
            //SQL запрос
        }
        public static String SQLShow(Boolean AllFlag)
        {
            //SQL запрос
            String Result = "";
            Result = @"SELECT * FROM 'Tasks.View'; ";
            return Result;
        }
        public static String SQLShow(String val)
        {
            //SQL запрос
            String Result = "";
            Result = String.Format(@"SELECT * FROM 'Tasks.View' WHERE Тема LIKE '{0}'; ", val);
            return Result;
        }
        static void DeleteTask()
        {
            //SQL запрос
        }
    }
}
