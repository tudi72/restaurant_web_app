using demo.Data_Layer.Dao;
using demo.Data_Layer.Models;
using System.Diagnostics;
namespace demo.Business_Layer.Manager
{
    /// <summary>
    /// <remarks>Manager used for handling the operation of inserting into a basket and catching the errors.</remarks>
    /// </summary>
    public class BasketManager
    {
        public readonly Dao<Basket> dao = new GenericDao<Basket>();
        /// <summary>
        /// Method for inserting an object of type basket into the data.
        /// </summary>
        /// <returns>Basket</returns>
        public Guid? insert()
        {
            try
            {
                Basket basket = new Basket();
                basket.id = Guid.NewGuid();
                dao.insert(basket);
                return basket.id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
                
            }
        }
    }
}
