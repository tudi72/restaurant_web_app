using demo.Business_Layer.VO;
using demo.Data_Layer.Dao;
using demo.Models;
using System.Diagnostics;

namespace demo.Business_Layer.Manager
{
    /// <summary>
    /// <remarks>Manager for handling the person objects. This class will make sure to retrieve or insert the person.</remarks>
    /// </summary>
    public class PersonManager
    {   
        public static readonly PersonManager manager = new PersonManager();
        public readonly Dao<Person> dao = new GenericDao<Person>();
        /// <summary>
        /// Method for inserting a person into the database based on the
        ///information passed by the model given as an argument to the
        ///function.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Guid?</returns>
        public Guid? insert(ReservationAndMealListVO model)
        {
            Person person = new Person();
            person.id = Guid.NewGuid();
            person.name = model.name;

            try
            {
                dao.DataReaderMapToList<Person>(dao.insert(person));
                return person.id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DB: Cannot insert person into database");
                return null;
            }
        }
        /// <summary>
        /// Method for inserting a person having the name passed as an
        // argument.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Guid?</returns>
        public Guid? insert(string name)
        {
            Person person = new Person();
            person.id = Guid.NewGuid();
            person.name = name;

            try
            {
                dao.DataReaderMapToList<Person>(dao.insert(person));
                return person.id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DB: Cannot insert person into database");
                return null;
            }
        }
        /// <summary>
        /// Gets the name of a person object based on his id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string</returns>
        public string getName(Guid id)
        {
            try
            {
                return dao.DataReaderMapToList<Person>(dao.getById(id)).ToArray()[0].name;
            }
            catch(Exception ex)
            {
                return " ";
            }
        }
    }
}
