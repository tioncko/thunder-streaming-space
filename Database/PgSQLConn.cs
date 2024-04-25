using Npgsql;
using System.Data;
using thunder_streaming_space.Settings;

namespace thunder_streaming_space.Database
{
    internal class PgSQLConn
    {
        private static readonly string connString = Parameters.Build().GetSection("ConnectionStrings").GetChildren().First().Value!;

        //Insert, Update, Delete commands
        public int ExecuteCommand(string query) {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            NpgsqlCommand command = new NpgsqlCommand();

            try {
                conn.Open();
                command.CommandText = query;
                command.Connection = conn;

                using (command) return command.ExecuteNonQuery();
            }
            catch (Exception ex) {
                throw new Exception($"Error: {ex.Message.ToString()}");
            }
            finally {
                CloseDB(conn);
            }
        }

        //Return a unique value
        public object ReturnUniqueValue(string query) {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            NpgsqlCommand command = new NpgsqlCommand();

            try {
                conn.Open();
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                command.Connection = conn;

                return command.ExecuteScalar()!;
            }
            catch (Exception ex) {
                throw new Exception($"Error: {ex.Message.ToString()}");
            }
            finally {
                CloseDB(conn);
            }
        }

        //Return all the informations about a table
        private NpgsqlDataReader Select(string query) {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            NpgsqlCommand command = new NpgsqlCommand();

            try
            {
                conn.Open();
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                command.Connection = conn;

                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error: {ex.Message.ToString()}");
            }
        }

        public NpgsqlDataReader ReturnTableData(string param) => Select($"SELECT * FROM {param}");
        
        private void CloseDB(NpgsqlConnection conn) {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
}
