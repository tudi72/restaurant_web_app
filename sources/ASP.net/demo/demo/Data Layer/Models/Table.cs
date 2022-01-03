namespace demo.Models
{
    /// <summary>
    ///Represents the entity of a physical table in the restaurant.
    /// </summary>
    public class Table
    {
        public Guid id { get; set; }
        
        public int nrPersons { get; set; }
    }
}
