using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Project
    {
        private String DatabaseName { get; set; }
        private UInt32 ID { get; set; }
        public String Name { get; set; }
        public String SQLstring { get; private set; }
        public void AddProject()
        {
            //SQL запрос
        }
        public void DeleteProject()
        {
            //SQL запрос
        }
        public String SQLAdd()
        {
            String Result = "";
            Result = "INSERT INTO Projects (Name) VALUES('" + Name + "');";
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
