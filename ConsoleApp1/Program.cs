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
            //string dbName = "Database.sqlite";
            Console.WriteLine("Input the Data Base file name:");
            string dbName = (Console.ReadLine()).Replace("\"", "");
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
        static void FillingTask(ref Task tsk, ref Master _master)
        {
            _master.WriteMessage(true, new String[] { "", "", "" });
            tsk.ProjectID=0;
            do
            {
                _master.WriteMessage(true, new String[] { "Write ID of chosen Project" });
                db.ExecuteCommand(new Project().ShowMaster(true));
                db.printTable(40);
                if (Int32.TryParse(Console.ReadLine(), out int j))
                    tsk.UserID = (uint)j;
                else
                    break;

                break;
            }
            while (true);
            
            
            //db.ExecuteCommand();
            //db.printTable()
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
            string PatternCommand = "(^(add){1}|^(remove){1}|^(show){1})( )?";
            string PatternParameter = "((user){1}|(project){1}|(task){1})( )?";
            string PatternSubParameter = "(all)( )?";
            Preparing();
            if (db.IsConnected)
            {
                //Запуск мастера сообщений
                Master _master = new Master();
                _master.WriteMessage(new String[] { "Welcome to Bug Tracker", "To work with the system, write command.","List of available commands:",
                    " Add:    for adding an object to Data Base",
                    " Remove: for removing an object from Data Base",
                    " Show: for removing an object from Data Base",
                    "List of perameters:",
                    " User:    for working with User table",
                    " Project: for working with User table",
                    " Task:    for working with User table",
                    "List of sub perameters:",
                    " All:    for working with User table"});
                while (answer != "exit")
                {
                    Console.WriteLine("Input command:");
                    answer = Console.ReadLine();
                    Regex rxVerb = new Regex(PatternCommand, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex rxPrm = new Regex(PatternParameter, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex rxSPrm = new Regex(PatternSubParameter, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    MatchCollection mcVerb = rxVerb.Matches(answer);
                    answer = Regex.Replace(answer, PatternCommand, String.Empty);
                    MatchCollection mcPrm = rxPrm.Matches(answer);
                    answer = Regex.Replace(answer, PatternParameter, String.Empty);
                    MatchCollection mcSPrm = rxSPrm.Matches(answer);
                    answer = Regex.Replace(answer, PatternSubParameter, String.Empty);
                    Boolean allFlag = false;
                    if (mcSPrm.Count == 1) allFlag = true;

                    if ((mcVerb.Count == 1) & (mcPrm.Count == 1)&(answer.Length==0))
                    {
                        Match match = mcVerb[0];
                        switch (match.Groups[1].Value)
                        {
                            case "add":
                                match = mcPrm[0];
                                switch (match.Groups[1].Value)
                                {
                                    case "user":
                                        User usr = new User();
                                        _master.WriteMessage(new String[] { String.Format("You are about to start a master of adding a {0}", match.Groups[1].Value), "Begin? (y/n)"}, 
                                            new char[] { 'Y','N'});
                                        if (_master.Key == 'N') break;
                                        Console.WriteLine(match.Value + " plug");
                                        Console.WriteLine(db.IsConnected?"DB Connected":"DB is NOt connected!!!");
                                        if (db.ExecuteCommand(usr.AddMaster()) == 0)
                                            _master.WriteMessage(new String[] { String.Format("No successful queries executed for adding a {0}", match.Groups[1].Value) });
                                        else
                                            _master.WriteMessage(new String[] { String.Format("All queries successfully executed for adding a {0}", match.Groups[1].Value) });
                                        break;
                                    case "task":
                                        Task tsk = new Task();
                                        _master.WriteMessage(new String[] { String.Format("You are about to start a master of adding a {0}", match.Groups[1].Value), "Begin? (y/n)" },
                                            new char[] { 'Y', 'N' });
                                        if (_master.Key == 'N') break;
                                        Console.WriteLine(match.Value + " plug");
                                        Console.WriteLine(db.IsConnected ? "DB Connected" : "DB is NOt connected!!!");
                                        FillingTask(ref tsk, ref _master);
                                        if (db.ExecuteCommand(tsk.AddMaster()) == 0)
                                            _master.WriteMessage(new String[] { String.Format("No successful queries executed for adding a {0}", match.Groups[1].Value) });
                                        else
                                            _master.WriteMessage(new String[] { String.Format("All queries successfully executed for adding a {0}", match.Groups[1].Value) });
                                        break;
                                    case "project":
                                        Project prj = new Project();
                                        _master.WriteMessage(new String[] { String.Format("You are about to start a master of adding a {0}", match.Groups[1].Value), "Begin? (y/n)" },
                                            new char[] { 'Y', 'N' });
                                        if (_master.Key == 'N') break;
                                        Console.WriteLine(match.Value + " plug");
                                        Console.WriteLine(db.IsConnected ? "DB Connected" : "DB is NOt connected!!!");
                                        if (db.ExecuteCommand(prj.AddMaster()) == 0)
                                            _master.WriteMessage(new String[] { String.Format("No successful queries executed for adding a {0}", match.Groups[1].Value) });
                                        else
                                            _master.WriteMessage(new String[] { String.Format("All queries successfully executed for adding a {0}", match.Groups[1].Value) });
                                        break;
                                    default:
                                        _master.WriteMessage(new String[] { string.Format(@"No match for ""Add {0}"".", match.Groups[1].Value), "Error!" });
                                        if (_master.Key != 'Y') break;
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
                                        if (db.ExecuteCommand(usr.RemoveMaster()) == 0)
                                            _master.WriteMessage(new String[] { String.Format("No successful queries executed for {0}", match.Groups[1].Value) });
                                        else
                                        {
                                            _master.WriteMessage(new String[] { String.Format("All queries successfully executed for {0}", match.Groups[1].Value) });
                                        }
                                        break;
                                    case "task":
                                        Task tsk = new Task();
                                        if (db.ExecuteCommand(tsk.RemoveMaster()) == 0)
                                            _master.WriteMessage(new String[] { String.Format("No successful queries executed for {0}", match.Groups[1].Value) });
                                        else
                                        {
                                            _master.WriteMessage(new String[] { String.Format("All queries successfully executed for {0}", match.Groups[1].Value) });
                                        }
                                        break;
                                    case "project":
                                        Project prj = new Project();
                                        if (db.ExecuteCommand(prj.RemoveMaster()) == 0)
                                            _master.WriteMessage(new String[] { String.Format("No successful queries executed for {0}", match.Groups[1].Value) });
                                        else
                                        {
                                            _master.WriteMessage(new String[] { String.Format("All queries successfully executed for {0}", match.Groups[1].Value) });
                                        }
                                        break;
                                    default:
                                        _master.WriteMessage(new String[] { "No match for \"Remove Param\".", "Error!" });
                                        Closing();
                                        break;
                                }
                                break;
                            case "show":
                                match = mcPrm[0];
                                switch (match.Groups[1].Value)
                                {
                                    case "user":
                                        User usr = new User();
                                        if (db.ExecuteCommand(usr.ShowMaster(allFlag)) == 0)
                                            _master.WriteMessage(new String[] { String.Format("No successful queries executed for {0}", match.Groups[1].Value) });
                                        else
                                        {
                                            _master.WriteMessage(new String[] { String.Format("All queries successfully executed for {0}", match.Groups[1].Value) });
                                            db.printTable(60);
                                        }
                                        break;
                                        break;
                                    case "task":
                                        Task tsk = new Task();
                                        if (db.ExecuteCommand(tsk.ShowMaster(allFlag)) == 0)
                                            _master.WriteMessage(new String[] { String.Format("No successful queries executed for {0}", match.Groups[1].Value) });
                                        else
                                        {
                                            _master.WriteMessage(new String[] { String.Format("All queries successfully executed for {0}", match.Groups[1].Value) });
                                            db.printTable(100);
                                        }
                                        break;
                                    case "project":
                                        Project prj = new Project();
                                        if (db.ExecuteCommand(prj.ShowMaster(allFlag)) == 0)
                                            _master.WriteMessage(new String[] { String.Format("No successful queries executed for {0}", match.Groups[1].Value) });
                                        else
                                        {
                                            _master.WriteMessage(new String[] { String.Format("All queries successfully executed for {0}", match.Groups[1].Value) });
                                            db.printTable(60);
                                        }
                                        break;
                                    default:
                                        _master.WriteMessage(new String[] { string.Format(@"No match for ""Add {0}"".", match.Groups[1].Value), "Error!" });
                                        if (_master.Key != 'Y') break;
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
