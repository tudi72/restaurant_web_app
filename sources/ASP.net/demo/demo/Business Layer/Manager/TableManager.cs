using demo.Data_Layer.Dao;
using demo.Models;
using System.Diagnostics;

namespace demo.Business_Layer.Manager
{
    /// <summary>
    /// <remarks>Manager for a table, it will retrieve a table from the database.</remarks>
    /// </summary>
    public class TableManager
    {
        private static readonly Dao<Table> dao = new GenericDao<Table>();
        /// <summary>
        /// Method for taking an available table with the condition it
        ///available on the date and time of reservation and it can keep all
        ///of the persons passed as an argument.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <param name="nrPersons"></param>
        /// <returns>Guid?</returns>
        public Guid? takeAvailableTable(string date,string time,string nrPersons)
        {
            try
            {
                string query = "select \"table\".\"id\", \"table\".\"nrPersons\" from public.\"table\" " +
                                " left outer join public.\"tablestatus\" on \"table\".\"id\" = \"tablestatus\".\"tableID\" " +
                               "left outer join public.\"reservation\" on \"tablestatus\".\"reservationID\" = \"reservation\".\"id\" " +
                                "where \"table\".\"nrPersons\" >= '" + nrPersons + "' and " +
                                "( \"reservation\".\"date\" != '" + date + "'  or " +
                                "(\"reservation\".\"date\" = '" + date + "' and" +
                                "(abs(extract(epoch from \"reservation\".\"time\" -'" + time + "' ) / 60) > 0)) or " +
                                "\"reservation\".\"date\" is NULL)" +
                                "order by \"table\".\"nrPersons\" " +
                                "fetch first row only";
                Table table = dao.DataReaderMapToList<Table>(dao.executeQuery(query)).ToArray()[0];
                return table.id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot find an available table");
                return Guid.Empty;
            }
        }
    }
}
