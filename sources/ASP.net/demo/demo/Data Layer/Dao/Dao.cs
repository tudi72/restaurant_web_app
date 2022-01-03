using demo.Connection;
using Npgsql;

namespace demo.Data_Layer.Dao
{
    /// <summary>
    /// DAO represents the classes accessing the database using 
    /// different queries. It is divided by the business layer using an
    ///interface and an abstract generic class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface Dao<T>
    {
        public NpgsqlDataReader executeQuery(string query);
        

        public NpgsqlDataReader getByWhere(string param);
        
        public List<T> DataReaderMapToList<T>(NpgsqlDataReader dr);

        public NpgsqlDataReader insert(object src);
        
        public NpgsqlDataReader update(object src,object dst);

        public NpgsqlDataReader delete(Guid id);

        public NpgsqlDataReader getById(Guid id);

        public NpgsqlDataReader getAll();


    }
}
