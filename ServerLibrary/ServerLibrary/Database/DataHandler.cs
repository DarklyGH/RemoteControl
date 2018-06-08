using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Database
{
    class DataHandler : IDisposable

    {
        private MySql.Data.MySqlClient.MySqlConnection Connection;
        private MySql.Data.MySqlClient.MySqlCommand Command;

        public DataHandler()
        {
            Connection = new MySql.Data.MySqlClient.MySqlConnection("Server=localhost; Database = RemoteControl; Uid=root; Pwd=root; SslMode=none;");
            
            Connection.Open();
        }

        public MySql.Data.MySqlClient.MySqlDataReader RunCommandObj(string sql)
        {
            Command = Connection.CreateCommand();
            Command.CommandType = System.Data.CommandType.Text;
            Command.CommandText = sql;
            return Command.ExecuteReader();
        }

        public bool RunCommandBool(string sql)
        {
            Command = Connection.CreateCommand();
            Command.CommandType = System.Data.CommandType.Text;
            Command.CommandText = sql;
            return Command.ExecuteNonQuery() == 1;
        }

        /*
         * Assistant call to find the number of rows returned by the reader on SELECT COUNT(*) From Users table
         * default return type is a long/bigint for some retarded reason.
         */
        public MySql.Data.MySqlClient.MySqlDataReader RunSelectAllCommand()
        {
            Command = Connection.CreateCommand();
            Command.CommandType = System.Data.CommandType.Text;
            Command.CommandText = "SELECT COUNT(*) FROM RemoteControl.Users";
            return Command.ExecuteReader();
        }

        public int GetLastInsertedId(string _TableName)
        {
            Command = Connection.CreateCommand();
            Command.CommandType = System.Data.CommandType.Text;
            Command.CommandText = "SELECT id FROM RemoteControl." + _TableName +" ORDER BY timestamp DESC LIMIT 1";          
            return (int)Command.ExecuteScalar();
        }

        public void Dispose()
        {
            Command.Dispose();
            Connection.Dispose();
        }
    }
}
