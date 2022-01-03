using demo.Business_Layer.VO;
using demo.Data_Layer.Dao;
using demo.Data_Layer.Models;
using System.Diagnostics;

namespace demo.Business_Layer.Manager
{
    /// <summary>
    /// <remarks>Manager for making sure that deliveries are inserted and handled correctly.</remarks>
    /// </summary>
    public class OrderManager
    {
        public static readonly OrderManager Instance = new OrderManager();
        public static Dao<Order> dao = new GenericDao<Order>();
        /// <summary>
        /// Method for retrieving all the orders from the data layer.
        /// </summary>
        /// <returns>List<Order></returns>
        public List<Order> getAll()
        {
            try
            {
                return dao.DataReaderMapToList<Order>(dao.getAll());
            }catch(Exception ex)
            {
                Debug.WriteLine("Cannot retrieve list of orders from database ");
                return null;
            }
        }
        /// <summary>
        /// Method for inserting an order object passing as arguments the
        ///phone number for the delivery, the id of customer with his
        ///necessary information and the id of the basket with the client's
        ///meals.
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="basketID"></param>
        /// <param name="customerID"></param>
        /// <returns>Guid</returns>
        public Guid insert(string phone,Guid basketID,Guid customerID)
        {
            Order order = new Order();
            try
            {
                order.id = Guid.NewGuid();
                order.phone = phone;
                order.status = "0";
                order.basketID = basketID;
                order.customerID = customerID;
                order.preparation = "0";
                dao.insert(order);
                return order.id;
            }
            catch (Exception ex)
            {
                return Guid.Empty;
            }
        }
        /// <summary>
        /// Method for retrieving a list of custom objects having also
        ///information about the client.
        /// </summary>
        /// <returns>List<OrderAndPersonAndCustomerVO></returns>
        internal List<OrderAndPersonAndCustomerVO> getOrderWithPersonAndCustomer()
        {
            string query = "select \"order\".\"id\",\"person\".\"name\", \"order\".\"phone\",\"customer\".\"email\",\"order\".\"preparation\",\"order\".\"status\" from public.\"order\""+ 
                            "inner join public.\"customer\" on \"order\".\"customerID\" = \"customer\".\"id\""+
                            "inner join public.\"person\" on \"person\".\"id\" = \"customer\".\"personID\"";
            try
            {
                return dao.DataReaderMapToList<OrderAndPersonAndCustomerVO>(dao.executeQuery(query));
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Cannot retrieve order with person and customer name");
                return null;
            }
        }
        /// <summary>
        /// Method for retrieving a list of custom objects having also
        ///information about the client by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List<OrderAndPersonAndCustomerVO></returns>
        internal List<OrderAndPersonAndCustomerVO> getOrderWithPersonAndCustomerById(Guid id)
        {
            string query = "select \"order\".\"id\",\"person\".\"name\", \"order\".\"phone\",\"customer\".\"email\",\"order\".\"preparation\",\"order\".\"status\" from public.\"order\"" +
                            "inner join public.\"customer\" on \"order\".\"customerID\" = \"customer\".\"id\"" +
                            "inner join public.\"person\" on \"person\".\"id\" = \"customer\".\"personID\"" +
                            " where \"order\".\"customerID\" = '"+ id + "'";
            try
            {
                return dao.DataReaderMapToList<OrderAndPersonAndCustomerVO>(dao.executeQuery(query));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot retrieve order with person and customer name");
                return new List<OrderAndPersonAndCustomerVO> ();
            }
        }
        /// <summary>
        /// Method for updating the status of an order. The status can be
        ///rejected,impeding or accepted( -1 , 0 or 1).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="prTime"></param>
        /// <param name="status"></param>
        internal void updateStatus(Guid? id,string prTime,string status)
        {
            Order src;
            Order dst;

            try
            {
                src = dao.DataReaderMapToList<Order>(dao.getById((Guid)id)).ToArray()[0];
                dst = src;
                dst.status = status;
                dst.preparation = prTime;
                dao.update(src, dst);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot update status of order in database");
            }
        }
    }
}
