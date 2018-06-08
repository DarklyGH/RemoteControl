using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Models
{
    public class Command
    {
        public int CommandId { get; set; }
        public int UserId { get; set; }
        public string CommandName { get; set; }
        public long TimeStamp { get; set; }
        public int ExpiryTimestamp { get; set; }

        public Command() { }
        public Command(int _UserId, string _CommandName)
        {
            this.UserId = _UserId;
            this.CommandName = _CommandName;
        }
        public Command(int _CommandId, int _UserId, string _CommandName, int _Timestamp, int _ExpiryTime)
        {
            this.CommandId = _CommandId;
            this.UserId = _UserId;
            this.CommandName = _CommandName;
            this.TimeStamp = _Timestamp;
            this.ExpiryTimestamp = _ExpiryTime;
        }


    }
}
