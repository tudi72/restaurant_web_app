using demo.Models;

namespace demo.Business_Layer.VO
{
    /// <summary>
    /// Object used for keeping the necessary information for both a customer and a person.
    /// </summary>
    public class PersonAndCustomerVO
    {
        public Customer customer { get; set; }
        public Person person { get; set; }
    }
}
