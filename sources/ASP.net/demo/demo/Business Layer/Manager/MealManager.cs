using demo.Data_Layer.Dao;
using demo.Data_Layer.Models;

namespace demo.Business_Layer.Manager
{
    /// <summary>
    /// <remarks>Manager for getting the list of all meals and inserting different ones into the database.</remarks>
    /// </summary>
    public class MealManager 
    {
        private static readonly Dao<Meal> dao = new GenericDao<Meal>();
        /// <summary>
        /// Method for retrieving all the meals ordered by customers from the database by ID.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns>List<Meal></returns>
        public List<Meal> getAllByCustomerID(Guid customerID)
        {
            string query = "select \"meal\".\"name\",\"order\".\"preparation\",\"order\".\"status\" from public.\"order\""+
                "inner join public.\"basket\" on \"basket\".\"id\" = \"order\".\"basketID\"" +
                "inner join public.\"portion\" on \"portion\".\"basketID\" = \"basket\".\"id\"" +
                "inner join public.\"meal\" on \"meal\".\"id\" = \"portion\".\"mealID\""+
                "where \"order\".\"customerID\" = '" + customerID + "'";
            try
            {
               return  dao.DataReaderMapToList<Meal>(dao.executeQuery(query));

            }
            catch (Exception ex)
            {
                return new List<Meal>();
            }
        }
        /// <summary>
        /// Method for retrieving all the meals from the database.
        /// </summary>
        /// <returns>List<Meal></returns>
        public List<Meal> getAll()
        {
            List<Meal> list = null;
            try
            {
                list = dao.DataReaderMapToList<Meal>(dao.getAll());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return list;
        }
        /// <summary>
        /// Inserts into the database a meal object.
        /// </summary>
        /// <param name="meal"></param>
        /// <returns>bool</returns>
        public bool insert(Meal meal)
        {
            meal.id = Guid.NewGuid();
            try
            {
                dao.insert(meal);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
