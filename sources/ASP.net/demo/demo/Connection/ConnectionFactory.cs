using Microsoft.Build.Utilities;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace demo.Connection
{
    /// <summary>
    /// This class make the connection between the database and the server.
    /// </summary>
    public class ConnectionFactory
    {
        private const string V = "An error occured while trying to connect to postgres";
        private static readonly ConnectionFactory instance = new ConnectionFactory();

        private ConnectionFactory(){}
        /// <summary>
        /// This method is creating the connection between the database
        ///and the server.
        /// </summary>
        /// <returns>NpgsqlConnection</returns>
        private NpgsqlConnection createConnection()
        {
            
            NpgsqlConnection connection = null;
            try
            {
               connection = new NpgsqlConnection("Server=localhost;Database=postgres;Port=5432;User Id=postgres;Password=Caramida2015.");
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return connection;
        }
        /// <summary>
        /// This method is getting the instance of a connection.
        /// </summary>
        /// <returns></returns>
        public static NpgsqlConnection getConnection()
        {
            return instance.createConnection();
        }
        /// <summary>
        /// This Methos is closing the connectioction.
        /// </summary>
        /// <param name="connection"></param>
        public static void close(NpgsqlConnection connection)
        {
            if(connection != null)
            {
                try
                {
                    connection.Close();
                }
                catch(Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
        }
        /// <summary>
        /// This method is closing a string received from postgresql.
        /// </summary>
        /// <param name="reader"></param>
        public static void close(NpgsqlDataReader reader)
        {
            if(reader != null)
            {
                try
                {
                   reader.Close();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
