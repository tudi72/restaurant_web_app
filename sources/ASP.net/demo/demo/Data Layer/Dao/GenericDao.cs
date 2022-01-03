    using demo.Connection;
using Npgsql;
using System;
using System.Data;
using System.Reflection;

namespace demo.Data_Layer.Dao
{
    /// <summary>
    /// This is the class that constructs the queries and retrieves data 
    /// for making the operations (inserting,deleting,updating, selecting) on the database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericDao<T> : Dao<T>
    {
        private static readonly Type type = typeof(T);
        /// <summary>
        /// This method is deleting a object from the data base, using the
        ///id passed as an argument.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NpgsqlDataReader</returns>
        /// <exception cref="NotImplementedException"></exception>
        public NpgsqlDataReader delete(Guid id)
        {

            throw new NotImplementedException();
        }
        /// <summary>
        /// This method makes a selection of some T type objects.
        /// </summary>
        /// <returns>NpgsqlDataReader</returns>
        public NpgsqlDataReader getAll()
        {
            NpgsqlCommand command = ConnectionFactory.getConnection().CreateCommand();
            command.CommandText = "select * from public.\"" + type.Name.ToLower()+"\"";
            NpgsqlDataReader rd = command.ExecuteReader();
            return rd;
        }
        /// <summary>
        /// This method makes a selection of some T type objects.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>NpgsqlDataReader</returns>
        public NpgsqlDataReader getById(Guid id)
        {
            NpgsqlCommand command  = ConnectionFactory.getConnection().CreateCommand();
            command.CommandText = "select * from \"" + type.Name.ToLower() + "\" where id ='" + id +"'";
            NpgsqlDataReader rd = command.ExecuteReader();
            return rd;
        }
        /// <summary>
        /// This method executes a query received as an argument.
        /// </summary>
        /// <param name="query"></param>
        /// <returns>NpgsqlDataReader</returns>
        public NpgsqlDataReader executeQuery(string query)
        {
            NpgsqlCommand cmd = ConnectionFactory.getConnection().CreateCommand();
            cmd.CommandText = query;
            return cmd.ExecuteReader();
        }

        /// <summary>
        /// This method makes a selection over the T entity having a
        /// where condition which receives a query as parameter.
        /// </summary>
        /// <param name="param"></param>
        /// <returns>NpgsqlDataReader</returns>
        public NpgsqlDataReader getByWhere(string param)
        {
            NpgsqlCommand command = ConnectionFactory.getConnection().CreateCommand();
            command.CommandText = "select * from " + type.Name + " where " + param;
            NpgsqlDataReader rd = command.ExecuteReader();
            return rd;
        }
        /// <summary>
        /// This method inserts into the T object the source s given as a
        /// parameter.
        /// </summary>
        /// <param name="src"></param>
        /// <returns>NpgsqlDataReader</returns>
        public NpgsqlDataReader insert(object src)
        {
            NpgsqlCommand command = ConnectionFactory.getConnection().CreateCommand();
            String query = "Insert into public.\"" + type.Name.ToLower() + "\" values ('";
            var fields = src.GetType().GetRuntimeFields().Select(x => x.GetValue(src)).ToList();
            foreach (var field in fields)
            {
                query = query + field.ToString() + "','";
            }
            query = query.Substring(0, query.Length - 2);
            query = query + ")";
            command.CommandText = query;
            return command.ExecuteReader();

        }
        /// <summary>
        /// This method makes an update over the src object and updates
        /// it with the dst object.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <returns>NpgsqlDataReader</returns>
        public NpgsqlDataReader update(object src,object dst)
        {
            NpgsqlCommand command = ConnectionFactory.getConnection().CreateCommand();
            String query = "Update public." + type.Name + " set ";
            var fields = src.GetType().GetProperties().ToArray();
            var values = src.GetType().GetRuntimeFields().Select(x => x.GetValue(dst)).ToArray();
            for(int i = 1;i < fields.Length; i++)
            {
                query = query + " \"" + fields[i].Name + "\" = '" + values[i].ToString() + "',";
            }

            query = query.Substring(0,query.Length - 1);
            query = query + " where \"id\" = '" + values[0].ToString() +"' ";
            command.CommandText = query;
            return command.ExecuteReader();
        }
        /// <summary>
        /// This method is mapping a data string to a list of objects of T
        /// type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns>List<T> DataReaderMapToList<T></returns>
        public List<T> DataReaderMapToList<T>(NpgsqlDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {

                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);

                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}
