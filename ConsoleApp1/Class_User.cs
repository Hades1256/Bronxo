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

        public Rights GetRights()
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
        }


        static void AddUser()
        {
            //SQL запрос
        }

        static void DeleteUser()
        {
            //SQL запрос
        }
    }
}
