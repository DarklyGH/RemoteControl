using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Models
{
    public class User
    {
        public int Id { set; get; }
        public string Username { set; get; }
        public string Password { get; set; }
        public Boolean IsAdmin { get; set; }
        public long timeCreated { get; set; }
        public Command UserCommand;

        public User() { }
        public User(string username, string pass, bool admin)
        {
            this.Username = username;
            this.Password = pass;
            this.IsAdmin = admin;
        }

        //Commands are issued by users - therefore user class should be able to create a command
        public Command CreateCommand(string _CommandName)
        {
            UserCommand = new Command();
            UserCommand.UserId = this.Id;
            UserCommand.CommandName = _CommandName;
            return UserCommand;
        }
           


    }
}
