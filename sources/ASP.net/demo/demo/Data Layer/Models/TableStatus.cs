namespace demo.Models
{
    /// <summary>
    /// Table status was created with the purpose of making a mapping between the reservation and the table.
    /// </summary>
    public class TableStatus
    {
        public Guid tableID { get; set; }
        public Guid reservationID { get; set; }
        public int status { get; set; }
    }
}
