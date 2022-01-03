using demo.Data_Layer.Models;

namespace demo.Business_Layer.VO
{
    /// <summary>
    /// VO object containing the list of meals to be shown and an argument for the number of portions chosen for a meal.
    /// </summary>
    public class NrPortionWithMealListVO
    {
        public List<Meal> list { get; set; }

        public int nrPortion { get; set; }

    }
}
