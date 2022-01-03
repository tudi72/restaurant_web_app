using demo.Data_Layer.Models;

namespace demo.Business_Layer.VO
{
    /// <summary>
    /// Contains a list of meals to be displayed and some fields for keeping the information about the customer. This object will be
    ///used for the view of creating a reservation(having on the same view both the form for completing the information about the
    ///client and also the list of meals from which to choose).
    /// </summary>
    public class ReservationAndMealListVO
    {
        public List<Meal> toDisplay { get; set; } 
        //public List<ReservationMealVO> toDisplay { get; set; }

        public string name { get; set; }
        public string email { get; set; }
        public DateTime date    { get; set; }
        public TimeSpan hour { get; set; }
        public int pers { get; set; }
    }
}
