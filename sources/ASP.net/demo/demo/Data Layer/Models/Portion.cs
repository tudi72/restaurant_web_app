namespace demo.Data_Layer.Models
{
    /// <summary>
    /// It represents an object containing the amount of meals ordered in a 
    /// delivery or in a table reservation. For this specific reason
    ///we save the meal ID.
    /// </summary>
    public class Portion
    {
        public Guid id { get; set; }
        public int quantity { get; set; }
        public Guid basketID { get; set; }
        public Guid mealID { get; set; }

    }
}
