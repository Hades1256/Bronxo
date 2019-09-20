using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Task
    {
        //private UInt32 ID=0;
        public UInt32 ProjectID { get; set; }
        public UInt32 UserID { get; set; }
        private String Type="";
        private String Priority;
        private String Name="";
        private String Descr="";

        public String RemoveMaster()//
        {
            String Result = "";
            Master _master = new Master();
            Console.Clear();
            Console.WriteLine("Write Task name to remove");
            Name = Console.ReadLine();
            _master.WriteMessage(new String[] { "WARNING!", "You are about to remove task from Data Base.", "Continue? (y/n)" }, new char[] { 'Y', 'N' });
            if (_master.Key == 'Y') Result = SQLRemove();
            return Result;
        }
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
            _master.WriteMessage(true, new String[] { "Write Task theme to add"});
            Name = Console.ReadLine();
            _master.WriteMessage(true, new String[] { String.Format("Write Task Description with name: {0}", Name )});
            Descr = Console.ReadLine();
            _master.WriteMessage(true, new String[] { String.Format("Write Task Type with name: {0}", Name )});
            Type = Console.ReadLine();
            _master.WriteMessage(true, new String[] { String.Format("Write Task Priority with name: {0}", Name )});
            Priority = Console.ReadLine();
            _master.WriteMessage(new String[] { "You are about to add a new task to Data Base:", String.Format("| {0} | {1} | {2} | {3}", Name, Descr, Type, Priority ), "Continue? (y/n)" }, new char[] { 'Y', 'N' });
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
                Console.WriteLine("Write Task name to show");
                Name = Console.ReadLine();
                _master.WriteMessage(new String[] { String.Format("You are about to show a user: {0} from Data Base.", Name), "Continue? (y/n)" }, new char[] { 'Y', 'N' });
                if (_master.Key == 'Y') Result = SQLShow(Name);
            }
            return Result;
        }
        private String SQLAdd()
        {
            //SQL запрос
            String Result = "";
            Result = String.Format("INSERT INTO Tasks (ProjectID, Theme, Type, Priority, UserID, Description) VALUES('{0}','{1}','{2}','{3}','{4}','{5}'); ",
                ProjectID, Name, Type, Priority, UserID, Descr);
            return Result;
        }
        private String SQLRemove()
        {
            //SQL запрос
            String Result = "";
            Result = String.Format("Delete FROM Tasks WHERE Theme = '{0}'; ", Name);
            return Result;
        }
        private static String SQLShow(Boolean AllFlag)
        {
            //SQL запрос
            String Result = "";
            Result = @"SELECT * FROM 'Tasks.View'; ";
            return Result;
        }
        private static String SQLShow(String val)
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
