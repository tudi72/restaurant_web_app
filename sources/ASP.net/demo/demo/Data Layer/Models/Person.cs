using System.ComponentModel.DataAnnotations;

namespace demo.Models
{
    /// <summary>
    /// A model for creating a client having a name.
    /// </summary>
    public class Person
    {
        [Key]
        public Guid id{ get; set; }
        public string? name { get; set; }
    }
}
