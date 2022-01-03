using demo.Data_Layer.Models;

namespace demo.Business_Layer.VO
{
    /// <summary>
    /// Object used for showing the necessary information about the delivery made by an user , such as his name, email, address , the
    ///status of the delivery and the preparation time.
    /// </summary>
    public class OrderAndCustomerAndMealListVO
    {
        public List<Meal> meals { get; set; }   
        public List<OrderAndPersonAndCustomerVO> orders { get; set; }
    }
}
