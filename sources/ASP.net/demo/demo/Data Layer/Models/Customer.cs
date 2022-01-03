using System.ComponentModel.DataAnnotations;
namespace demo.Models
{
    /// <summary>
    /// An object representing a customer of the restaurant having an email and a address in order to make booking and deliveries.
    /// </summary>
    public class Customer
    {
        [Key]
        public Guid id { get; set; }
        
        public Guid personID { get; set; }
        public String email { get; set; }
        
        public string address { get; set; }
        
    }
}
