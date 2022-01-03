using demo.Data_Layer.Dao;
using demo.Models;

namespace demo.Business_Layer.Manager
{
    /// <summary>
    /// Manager of table status, auxiliary table between reservations and tables.
    /// </summary>
    public class TableStatusManager
    {
        public readonly Dao<TableStatus> dao = new GenericDao<TableStatus>();
        /// <summary>
        /// Method for inserting a mapping between the reservation and
        ///the table into the table status.
        /// </summary>
        /// <param name="reservationID"></param>
        /// <param name="tableID"></param>
        /// <returns>bool</returns>
        public bool insertTableStatus(Guid reservationID,Guid tableID)
        {
            TableStatus obj = new TableStatus();
            try
            {
                obj.reservationID = reservationID;
                obj.tableID = tableID;
                obj.status = 0;
                dao.insert(obj);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
