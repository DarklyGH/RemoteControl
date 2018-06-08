using ServerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Database
{
    public class DBConnect
    {

        //User Utility Methods
        public static bool TryCreateUser(User NewUser, out User _NewUser)
        {
            using(DataHandler dataHandler = new DataHandler())
            {                
                var reader = dataHandler.RunCommandBool("INSERT INTO RemoteControl.Users (username, password, isAdmin) VALUES ('" + NewUser.Username + "', '" + NewUser.Password + "', " + NewUser.IsAdmin + ")");

                if(reader == true)
                {
                    _NewUser = NewUser;
                    _NewUser.Id = (int)GetLastInsertedUserId();
                    return true;
                } else {
                    _NewUser = null;
                    return false;
                }
            }
        }        
        public static long CountAllUsers()
        {
            DataHandler dataHandler = new DataHandler();            
            var reader = dataHandler.RunSelectAllCommand();
            reader.Read();
            return (long)reader[0];             
            
        }
        public static int GetLastInsertedUserId()
        { 
            DataHandler dataHandler = new DataHandler();
            return dataHandler.GetLastInsertedId("Users");             
        }
        public static List<User> TryReadAllUsers()
        {
            List<User> returnedUsers = new List<User>();
            using (DataHandler datahandler = new DataHandler()){
                var reader = datahandler.RunCommandObj("SELECT * FROM RemoteControl.Users");
                while (reader.Read())
                {
                    User user = new User()
                    {
                        Id = (int)reader["id"],
                        Username = (string)reader["username"],
                        Password = (string)reader["password"],
                        IsAdmin = (bool)reader["isAdmin"],
                        timeCreated = (int)reader["timestamp"]
                        
                    };
                    returnedUsers.Add(user);
                }
            }
            return returnedUsers;
        }
        public static User GetSingleUser(int UserId)
        {
            User _User = new User();
            using (DataHandler dataHandler = new DataHandler())
            {
                var reader = dataHandler.RunCommandObj("SELECT * FROM RemoteControl.Users WHERE id='" + UserId + "'");
                reader.Read();                

                    _User.Id = (int)reader["id"];
                    _User.Username = (string)reader["username"];
                    _User.Password = (string)reader["password"];
                    _User.IsAdmin = (bool)reader["isAdmin"];
                    _User.timeCreated = (int)reader["timestamp"];
                
            }
            return _User;

        }
        public static bool UpdateUserPass(User UserToChange, string NewPass, out User UpdatedUser)
        {
            using (DataHandler dataHandler = new DataHandler())
            {
                var reader = dataHandler.RunCommandBool("UPDATE RemoteControl.Users SET password='" + NewPass + "' WHERE id='" + UserToChange.Id+"'");
                if (reader == true)
                {
                    UpdatedUser = GetSingleUser(UserToChange.Id);
                    return true;
                }
                else
                {
                    UpdatedUser = null;
                    return false;                     
                }
            }
        }
        public static bool DeleteUser(int UserId)
        {
            using (DataHandler dataHandler = new DataHandler())
            {
                var reader = dataHandler.RunCommandBool("DELETE FROM RemoteControl.Users WHERE id='" + UserId + "'");
                
                if (reader == true)
                {
                    return true; //The row was deleted successfully.
                } else
                {
                    return false; //The row was not deleted successfully.                    
                }
            }            
        }
        public static User GetSingleUserByUserName(string UserName)
        {
            User _User = new User();
            using (DataHandler dataHandler = new DataHandler())
            {
                var reader = dataHandler.RunCommandObj("SELECT * FROM RemoteControl.Users WHERE username='" + UserName + "'");

                reader.Read();
                _User.Id = (int)reader["id"];
                _User.Username = (string)reader["username"];
                _User.Password = (string)reader["password"];
                _User.IsAdmin = (bool)reader["isAdmin"];             
            }
            return _User;

        }

        //Command Utility Methods        
        public static bool TryCreateCommand(Command NewCommand, out Command _NewCommand)
        {
            using (DataHandler dataHandler = new DataHandler())
            {
                var reader = dataHandler.RunCommandBool("INSERT INTO RemoteControl.Commands (userid, name) VALUES (" + NewCommand.UserId + ", '" + NewCommand.CommandName + "')");

                if (reader == true)
                {
                    _NewCommand = NewCommand;
                    _NewCommand.CommandId = (int)GetLastInsertedCommandId();
                    _NewCommand = GetSingleCommand(_NewCommand.CommandId);
                    return true;
                }
                else
                {
                    _NewCommand = null;
                    return false;
                }
            }
        }
        public static List<Command> TryReadAllCommands()
        {
            List<Command> returnedCommands = new List<Command>();
            using (DataHandler dataHandler = new DataHandler())
            {
                var reader = dataHandler.RunCommandObj("SELECT * FROM RemoteControl.Commands");
                while (reader.Read())
                {
                    Command command = new Command()
                    {
                        CommandId = (int)reader["id"],
                        UserId = (int)reader["userid"],
                        CommandName = (string)reader["name"],
                        TimeStamp = (int)reader["timestamp"],
                        ExpiryTimestamp = (int)reader["expirytime"]
                    };
                    returnedCommands.Add(command);
                }
            }
            return returnedCommands;
        }
        public static Command GetSingleCommand(int _CommandId)
        {
            Command command = new Command();
            using (DataHandler dataHandler = new DataHandler())
            {
                var reader = dataHandler.RunCommandObj("SELECT * FROM RemoteControl.Commands WHERE id=" + _CommandId);

                reader.Read();
                command.CommandId = (int)reader["id"];
                command.UserId = (int)reader["userid"];
                command.CommandName = (string)reader["name"];
                command.TimeStamp = (long.Parse(reader["timestamp"].ToString()));
                command.ExpiryTimestamp = (int.Parse(reader["expirytime"].ToString()));
                
            }
            return command;
        }
        public static bool DeleteCommand(int _CommandId)
        {
            using (DataHandler dataHandler = new DataHandler())
            {
                var reader = dataHandler.RunCommandBool("DELETE FROM Remotecontrol.Commands WHERE id=" + _CommandId);

                if (reader == true)
                {
                    return true;
                } else
                {
                    return false;
                }

            }
        }
        public static int GetLastInsertedCommandId()
        {
            using (DataHandler dataHandler = new DataHandler())
            {
                return dataHandler.GetLastInsertedId("Commands");

            }
        }

        public static (bool success, string CommandName, Command command, int RowAffected) TuplesTest()
        {
            return (true, "hello", new Command(), 0);
        }

        public static void tupleTest2()
        {
            var v = TuplesTest();
            
        }
        
    }
}
