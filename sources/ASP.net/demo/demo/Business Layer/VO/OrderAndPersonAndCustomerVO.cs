namespace demo.Business_Layer.VO
{
    /// <summary>
    /// Object used for keeping the necessary information for both a customer and a person.
    /// </summary>
    public class OrderAndPersonAndCustomerVO
    {
        public Guid id  { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string preparation { get; set; }
        public string status { get; set; }

    }
}
