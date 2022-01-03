using demo.Models;

namespace demo.Business_Layer.VO
{
    /// <summary>
    /// VO object which contains the details about a customer and his reservation for a table in the restaurant. It will keep in touch
    ///with the status of reservation ,the number of persons, the name of the customer who created the reservation and so on.
    /// </summary>
    public class ReservationAndCustomerNameVO
    {
        public Guid reservationID { get; set; }
        public int nrPersons { get; set; }
        public int status { get; set; }

        public string name { get; set; }
    }
}
