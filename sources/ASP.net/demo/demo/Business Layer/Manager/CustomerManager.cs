using demo.Business_Layer.VO;
using demo.Data_Layer.Dao;
using demo.Models;
using System.Diagnostics;

namespace demo.Business_Layer.Manager
{
    /// <summary>
    /// <remarks>Manager used for retrieving a customer by it's name or id , or for inserting a customer information (encapsulated in different VO objects).</remarks>
    /// </summary>
    public class CustomerManager
    {
        public readonly Dao<Customer> dao = new GenericDao<Customer>();

        public static readonly CustomerManager manager = new CustomerManager();
        /// <summary>
        /// 
        /// </summary>
        /// Method for searching into the database a customer by its ID.
        /// <param name="id"></param>
        /// <returns>Customer</returns>
        public Customer getById(Guid id)
        {
            try
            {
                return dao.DataReaderMapToList<Customer>(dao.getById(id)).ToArray()[0];
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot get customer by id");
                return null;
            }
        }
        /// <summary>
        /// Method for retrieving the first customer from database and
        ///creating an object.
        /// </summary>
        /// <returns>Customer</returns>
        public Customer getFirstCustomer()
        {
            try
            {
                return dao.DataReaderMapToList<Customer>(dao.getAll()).ToArray()[0];
            }
            catch(Exception ex)
            {
                return null;
                Debug.WriteLine("cannot retrieve first customer from database");
            }
        }
        /// <summary>
        /// Method for inserting a customer into the database. It takes the
        ///necessary information from the VO object passed as an
        ///argument and also it requires a person ID.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="personID"></param>
        /// <returns>Guid?</returns>
        public Guid? insert(ReservationAndMealListVO model,Guid personID) {

            Customer customer = new Customer();
            customer.id = Guid.NewGuid();
            customer.address = "";
            customer.email = model.email;
            customer.personID = personID;

            try
            {
                dao.insert(customer);
                return customer.id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DB:Cannot insert customer into database");
                return Guid.Empty;
            }
        }
        /// <summary>
        /// Method for inserting into the database a customer based on his
        /// email, address and the person ID.
        /// </summary>
        /// <param name="Address"></param>
        /// <param name="email"></param>
        /// <param name="personID"></param>
        /// <returns>Guid?</returns>
        public Guid? insert(string Address,string email, Guid personID)
        {
            Customer customer = new Customer();
            customer.id = Guid.NewGuid();
            customer.address = Address;
            customer.email = email;
            customer.personID = personID;

            try
            {
                dao.insert(customer);
                return customer.id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DB:Cannot insert customer into database");
                return Guid.Empty;
            }
        }
        /// <summary>
        /// Inserts a customer into the database having as arguments his
        ///email and the ID of the person created into the database.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="personID"></param>
        /// <returns>Guid?</returns>
        public Guid? insert(string email, Guid personID)
        {
            Customer customer = new Customer();
            customer.id = Guid.NewGuid();
            customer.address = "";
            customer.email = email;
            customer.personID = personID;

            try
            {       
                dao.insert(customer);
                return customer.id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("DB:Cannot insert customer into database");
                return Guid.Empty;
            }
        }
    }
}
