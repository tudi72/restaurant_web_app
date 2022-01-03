namespace demo.Data_Layer.Models
{
    /// <summary>
    /// An order object contains all the necessary information for a customer in order to make a delivery having a list of meals. For
    ///this case we will keep the id of basket and of the customer and some information about the status of the order and the
    ///preparation time for the list of meals.
    /// </summary>
    public class Order
    {
        public Guid id { get; set; }
        public Guid basketID { get; set; }
        public Guid customerID { get; set; }
        public string phone { get; set; }
        public string status { get; set; }
        public string preparation { get; set; }


    }
}
