using demo.Business_Layer.VO;
using demo.Data_Layer.Dao;
using demo.Models;
using System.Diagnostics;
namespace demo.Business_Layer.Manager
{
    /// <summary>
    /// <remarks>Manager for handling the reservation objects such as checking the status, 
    /// inserting correctly an object or selecting reservation
    /// based on different criteria.</remarks>
    /// </summary>
    public class ReservationManager
    {
        public static readonly ReservationManager manager = new ReservationManager();
        public static readonly Dao<Reservation> dao = new GenericDao<Reservation>();
        /// <summary>
        /// Method for inserting a reservation object with the information
        ///given from the model passed as a parameter, the basket id for
        ///the list of meals and the customer ID of this reservation.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="customerID"></param>
        /// <param name="basketID"></param>
        /// <returns>Guid?</returns>
        public Guid? InsertReservation(ReservationAndMealListVO model,Guid customerID,Guid basketID) 
        {
            Reservation res = new Reservation();
            res.id = Guid.NewGuid();
            try
            {
                res.status = 0;
                res.customerID = customerID;
                res.time = model.hour;
                res.date = model.date;
                res.nrPersons = model.pers;
                res.basketID = basketID;
                dao.insert(res);
                return res.id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot execute checking table status");
                return null;
            }
        }
        /// <summary>
        /// Method for retrieving only one reservation based on the
        ///customer ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Reservation</returns>
        public Reservation getFirstReservationByCustomerId(Guid id )
        {
            try
            {
               var list = dao.DataReaderMapToList<Reservation>(dao.getById(id));
               return list.FirstOrDefault();
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Cannot get customer's reservation by id");
                return null;
            }
        }
        /// <summary>
        /// Retrieves a list of all reservation from the database.
        /// </summary>
        /// <returns>List<Reservation></returns>
        public List<Reservation> getAll()
        {
            List<Reservation> display = new List<Reservation>();
            try
            {
                return dao.DataReaderMapToList<Reservation>(dao.getAll());
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ResManager: Cannot take all reservations from database");
            }
            return display;
        }
        /// <summary>
        /// Gets a specific reservation for a customer based on the
        /// customer ID.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns>List<Reservation></returns>
        public List<Reservation> getByCustomerId(Guid customerID)
        {
            try
            {
                return dao.DataReaderMapToList<Reservation>(dao.getByWhere("\"customerID\" = '" + customerID + "'"));
            
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ResManager: Cannot show reservations of by customer id");
                return null;
            }
        }
        /// <summary>
        /// Updates the status of the reservation having as an argument
        /// the ID reservation, and the status as an integer with the values
        ///-1 for rejected, 0 for impeding and 1 for accepted.
        /// </summary>
        /// <param name="reservationID"></param>
        /// <param name="status"></param>
        /// <returns>bool</returns>
        public bool updateStatus(Guid reservationID,int status)
        {
            Reservation src;
            Reservation dst;
   
            try
            {
                src = dao.DataReaderMapToList<Reservation>(dao.getById(reservationID)).ToArray()[0];
                dst = src;
                dst.status = status;
                dao.update(src, dst);   
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Retrieves the list of custom VO objects having information
        ///about the reservation and some information about the
        ///customer.
        /// </summary>
        /// <returns>List<ReservationAndCustomerNameVO></returns>
        public List<ReservationAndCustomerNameVO> getReservationWithName()
        {
            string query = "select \"reservation\".\"id\" as \"reservationID\",\"reservation\".\"nrPersons\" as \"nrPersons\"," +
                            "\"reservation\".\"status\" as \"status\",\"person\".\"name\" as \"name\" from public.\"reservation\" " +
                            "inner join public.\"customer\" on \"customer\".\"id\" = \"reservation\".\"customerID\"" +
                            "inner join public.\"person\" on \"person\".\"id\" = \"customer\".\"personID\"" +
                            "where \"reservation\".\"status\"=0";
            try
            {
                return dao.DataReaderMapToList<ReservationAndCustomerNameVO>(dao.executeQuery(query));

            }
            catch (Exception ex)
            {
                Debug.Write("cannot get reservation with names");
                return null;
            }
        }
    }
}
