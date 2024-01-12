using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TheHiddenTreasuresWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        private const string ConnectionString =
            @"Data Source=cec-magshimim;" +
            @"Initial Catalog=TheHiddenTreasuresDB;" +
            @"Integrated Security=True";

        public bool ValidateUser(User user)
        {
            string query = $"select * from Users where username='{user.username}' and password='{user.password}'";
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(query, connection);
            
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                return reader.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
                return false;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool RegisterUser(User user)
        {
            if (HasUsername(user.username))
                return false;

            string query = $"insert into Users (username, password) values('{user.username}', '{user.password}')";
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                return cmd.ExecuteNonQuery() != 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
                return false;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool HasUsername(string username)
        {
            string query = $"select * from Users where username='{username}'";
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                return reader.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
                return false;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }

        public bool UpdateStatistics(string username, int levels, int time)
        {
            string query = $"select count(username) as 'hasStatistics' from StatisticsTbl where username='{username}';";
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();
                bool hasStatistics = (int)reader["hasStatistics"] != 0;
                reader.Close();

                if(hasStatistics)
                {
                    query = $"select levels, time from StatisticsTbl where username='{username}';";
                    cmd = new SqlCommand(query, connection);
                    reader = cmd.ExecuteReader();

                    reader.Read();
                    levels += (int)reader["levels"];
                    time = Math.Min(time, (int)reader["time"]);
                    reader.Close();

                    query = $"update StatisticsTbl set levels='{levels}', time='{time}' where username='{username}';";
                    cmd = new SqlCommand(query, connection);
                    return cmd.ExecuteNonQuery() != 0;
                }
                
                query = $"insert into StatisticsTbl (username, levels, time) values('{username}', '{levels}', '{time}');";
                cmd = new SqlCommand(query, connection);
                return cmd.ExecuteNonQuery() != 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
                return false;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }
        }
    }
}
