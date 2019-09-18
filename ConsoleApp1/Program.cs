using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    //Preparations
    public partial class Program
    {
        static Database db;

        static void Preparing()
        {

            Console.WriteLine("Getting ready to work!");
            string dbName = "Database.sqlite";
            /*Console.WriteLine("Input the Data Base file name:");
            string dbName = Console.ReadLine();*/
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
            Console.WriteLine("System is shutting down.");
            ext_utils.Pause();
            db.Close();
        }
//Main
        static void Main(string[] args)
        {
            String answer="";
            string PatternCommand = "(^(add){1} |^(remove){1} |^(show){1} )";
            string PatternParameter = "((user |user){1}|(project |project){1}|(task |task){1})";
            string PatternSubParameter = "($(all)?)";
            Preparing();
            if (db.IsConnected)
            {
                //Запуск мастера сообщений
                Master _master = new Master();
                _master.WriteMessage(new String[] { "Welcome to Bug Tracker", "To work with the system, write command.","List of available commands:",
                    "Add:    for adding an object to Data Base",
                    "Remove: for removing an object from Data Base",
                    "Show: for removing an object from Data Base",
                    "List of perameters:",
                    "User:    for working with User table",
                    "Project: for working with User table",
                    "Task:    for working with User table",
                    "List of sub perameters:",
                    "All:    for working with User table"});
                while (answer != "exit")
                {
                    answer = Console.ReadLine();
                    Regex rxVerb = new Regex(PatternCommand, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex rxPrm = new Regex(PatternParameter, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex rxSPrm = new Regex(PatternSubParameter, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    MatchCollection mcVerb = rxVerb.Matches(answer);
                    answer = Regex.Replace(answer, PatternCommand, String.Empty);
                    MatchCollection mcPrm = rxPrm.Matches(answer);
                    answer = Regex.Replace(answer, PatternParameter, String.Empty);
                    MatchCollection mcSPrm = rxSPrm.Matches(answer);
                    Boolean allFlag = false;
                    if (mcSPrm.Count == 1) allFlag = true;

                    if ((mcVerb.Count == 1) & (mcPrm.Count == 1)&(answer.Length==0))
                    {
                        Match match = mcVerb[0];
                        switch (match.Value)
                        {
                            case "add":
                                match = mcPrm[0];
                                switch (match.Value)
                                {
                                    case "user":
                                        User usr = new User();
                                        usr.AddMaster();
                                        break;
                                    case "Task":
                                        Task tsk = new Task();
                                        tsk.AddMaster();
                                        break;
                                    case "Project":
                                        Project prj = new Project();
                                        prj.AddMaster();
                                        break;
                                    default:
                                        _master.WriteMessage(new String[] { "No match for \"Add Param\".", "Error!" });
                                        Closing();
                                        break;
                                }
                                break;
                            case "remove":
                                match = mcPrm[0];
                                switch (match.Value)
                                {
                                    case "user":
                                        User usr = new User();
                                        usr.RemoveMaster();
                                        break;
                                    case "Task":
                                        Task tsk = new Task();
                                        tsk.RemoveMaster();
                                        break;
                                    case "Project":
                                        Project prj = new Project();
                                        prj.RemoveMaster();
                                        break;
                                    default:
                                        _master.WriteMessage(new String[] { "No match for \"Remove Param\".", "Error!" });
                                        Closing();
                                        break;
                                }
                                break;
                            case "show":
                                match = mcPrm[0];
                                switch (match.Value)
                                {
                                    case "user":
                                        User usr = new User();
                                        usr.ShowMaster(allFlag);
                                        break;
                                    case "Task":
                                        Task tsk = new Task();
                                        tsk.ShowMaster(allFlag);
                                        break;
                                    case "Project":
                                        Project prj = new Project();
                                        prj.ShowMaster(allFlag);
                                        break;
                                    default:
                                        _master.WriteMessage(new String[] { "No match for \"Remove Param\".", "Error!" });
                                        Closing();
                                        break;
                                }
                                break;
                            default:
                                _master.WriteMessage(new String[] { "No match for Verb.", "Error!" });
                                Closing();
                                break;
                        }
                    }
                    else
                        _master.WriteMessage(new String[] { "Something went wrong with command string", "Please try again" });
                    /*foreach (Match match in Regex.Matches(answer, PatternCommand, RegexOptions.IgnoreCase))
                    {
                        Console.WriteLine("")
                    }*/
                };
            }
            else
            {
                Console.WriteLine("No valid Data Base is selected.\nApplication will be shut down now.");
            };
            Closing();
        }
    }
}
