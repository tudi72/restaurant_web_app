using demo.Business_Layer.VO;
using demo.Data_Layer.Dao;
using demo.Data_Layer.Models;
using System.Diagnostics;

namespace demo.Business_Layer.Manager
{
    public class PortionManager
    {
        /// <summary>
        /// Manage the portion of the meals ordered by the customers.
        /// </summary>
        public readonly Dao<Portion> dao = new GenericDao<Portion>();
        /// <summary>
        /// Method for inserting a portion object into the database.
        /// </summary>
        /// <param name="mealID"></param>
        /// <param name="quantity"></param>
        /// <param name="basketID"></param>
        /// <returns>bool</returns>
        public bool insert(string mealID,int quantity,Guid basketID)
        {
            Portion portion = new Portion();
            portion.id = Guid.NewGuid();;
            try
            {

                portion.basketID = basketID;
                portion.mealID = Guid.Parse(mealID);
                portion.quantity = Convert.ToInt32(quantity);
                dao.insert(portion);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot insert portion into database");
                return false;
            }
        }
    
    }
}
