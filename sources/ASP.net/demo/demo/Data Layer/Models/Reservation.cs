using demo.Models;
using System;
using System.ComponentModel.DataAnnotations;
/// <summary>
/// This specific class represents a model of what a reservation should keep in the database, such as the date and time and the
/// number of persons of the booking, and also the status of the reservation in case is accepted or declined.
/// </summary>
namespace demo.Models
{
    public class Reservation
    {
        public Guid id { get; set; }

        public int nrPersons { get; set; }

        public Guid customerID { get; set; }

        
        public DateTime date { get; set; }

        public TimeSpan  time { get; set; }

        public int status { get; set; }
        
        public Guid basketID { get; set; }
    }
}
