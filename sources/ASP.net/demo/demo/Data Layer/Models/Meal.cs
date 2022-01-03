namespace demo.Data_Layer.Models
{
    /// <summary>
    /// An object which contains the necessary about the meal such as ingredients, allergens, name price.
    /// </summary>
    public class Meal
    {
        public Guid id { get; set; }
        public string ingredients { get; set; }

        public string allergens { get; set; }

        public double weight { get; set; }

        public string name { get; set; }

        public double price { get; set; }
    }
}
