using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Project
    {
        //private String DatabaseName { get; set; }
        private UInt32 ID { get; set; }
        public String Name { get; set; }
        public String Descr { get; set; }
        public String SQLstring { get; private set; }

        // Сводка:
        //     Возвращает SQL запрос.
        //
        // Возврат:
        //     Строка типа String.
        public String AddMaster()//        
        {
            String Result = "";
            Master _master = new Master();
            Console.Clear();
            Console.WriteLine("Write Project name to add");
            Name = Console.ReadLine();
            Console.WriteLine("Write Project Description with name: {0}",Name);
            Descr = Console.ReadLine();
            _master.WriteMessage(new String[] { "You are about to add a new project to Data Base:", String.Format("| {0} | {1} |",Name, Descr), "Continue? (y/n)" }, new char[] { 'Y', 'N' });
            if (_master.Key == 'Y') Result = SQLAdd();
            return Result;
        }
        public String RemoveMaster()//
        {
            String Result = "";
            Master _master = new Master();
            Console.Clear();
            Console.WriteLine("Write Project name to remove");
            Name = Console.ReadLine();
            _master.WriteMessage(new String[] { "WARNING!", "You are about to remove project from Data Base.", "Continue? (y/n)" }, new char[] { 'Y', 'N' });
            if (_master.Key == 'Y') Result = SQLRemove();
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
                Console.WriteLine("Write Project name to show");
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
            Result = String.Format("INSERT INTO Projects (Name, Description) VALUES('{0}','{1}'); ", Name, Descr);
            return Result;
        }
        private String SQLRemove()
        {
            //SQL запрос
            String Result = "";
            Result = String.Format("Delete FROM Projects WHERE Name = '{0}'; ", Name);
            return Result;
        }
        private static String SQLShow(Boolean AllFlag)
        {
            //SQL запрос
            String Result = "";
            Result = @"SELECT * FROM 'Projects'; ";
            return Result;
        }
        private static String SQLShow(String val)
        {
            //SQL запрос
            String Result = "";
            Result = String.Format(@"SELECT * FROM 'Projects' WHERE Name LIKE '{0}'; ", val);
            return Result;
        }
        //Конструктор
        public Project()
        {
            ID = 0;
            Name = "";
            SQLstring = "";
            //DatabaseName = DbName;
        }
    }
}
