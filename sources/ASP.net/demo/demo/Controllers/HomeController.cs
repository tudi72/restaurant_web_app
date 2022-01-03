using demo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Npgsql;
using System.Data;
using System.Globalization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using demo.Business_Layer.Manager;
using demo.Data_Layer.Models;
using demo.Data_Layer.Dao;
using Microsoft.AspNetCore.Mvc.Rendering;
using demo.Business_Layer.VO;

namespace demo.Controllers
{
    /// <summary>
    /// This class is handles the data received from the client and also the 
    /// data retrieved from the data base and makes the connection
    /// between the server and the client with the business functions.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ReservationManager resManager = new ReservationManager();
        private readonly BasketManager basketManager = new BasketManager();
        private readonly PortionManager portionManager = new PortionManager();
        private readonly MealManager mealManager = new MealManager();
        private readonly PersonManager personManager = new PersonManager();
        private readonly CustomerManager customerManager = new CustomerManager();
        private readonly TableManager tableManager = new TableManager();
        private readonly TableStatusManager tableStatusManager = new TableStatusManager();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Reservation: client views all his reservations
        public IActionResult showReservation()
        {
            var customerID = customerManager.getFirstCustomer().id;
            List<Reservation> list = resManager.getByCustomerId(customerID);
            return View(list);
        }
        //TODO :solve the list of deliveries 
        public IActionResult showDelivery()
        {
            OrderAndCustomerAndMealListVO obj = new OrderAndCustomerAndMealListVO();
            var customerID = customerManager.getFirstCustomer().id;
            obj.meals = mealManager.getAllByCustomerID(customerID);
            obj.orders = OrderManager.Instance.getOrderWithPersonAndCustomerById(customerID);
            return View(obj);
        }


        public IActionResult client()
        {
            PersonAndCustomerVO obj = new PersonAndCustomerVO();
            try
            {
                obj.customer = customerManager.getFirstCustomer();
                obj.person = personManager.dao.DataReaderMapToList<Person>(personManager.dao.getById(obj.customer.personID)).ToArray()[0];
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message);
            }
            return View(obj);
        }

        public IActionResult admin()
        {
            return View();
        }
        public IActionResult Delivery()
        {
            NrPortionWithMealListVO obj = new NrPortionWithMealListVO();
            obj.list = mealManager.getAll();
            return View(obj);
        }

        public ActionResult adminShowDeliveries()
        {
                return View(OrderManager.Instance.getOrderWithPersonAndCustomer());
        }
        public ActionResult adminShowReservations()
        {

            //sends a list of reservation no matter if they are available or not 
            return View(ReservationManager.manager.getReservationWithName());
        }

        public IActionResult createReservation()
        {
            ReservationAndMealListVO obj = new ReservationAndMealListVO();
            obj.toDisplay = mealManager.getAll();
            return View(obj);
        }
        public ActionResult AccessClientPage()
        {
            return RedirectToAction("Index");
        }

        //TODO: get the value of numberP from delivery.cshtml 
        public ActionResult AcceptDelivery(Guid? id,int numberP)
        {
            Debug.WriteLine(numberP);
            //using the reservationID find the reservation and update the status to 1
            OrderManager.Instance.updateStatus(id,Convert.ToString(numberP), "1");
            return RedirectToAction("showDelivery");
        }


        // Reservation: update status to rejected
        public ActionResult DeclineDelivery(Guid? id)
        {
            //if the delivery is declined , it will be delete from the database 
            OrderManager.Instance.updateStatus(id,"0","-1");
            return RedirectToAction("showDelivery");
        }
        //Reservation: updating status from pending -> accept 
        public ActionResult AcceptReservation(Guid? id) {

            //using the reservationID find the reservation and update the status to 1
            resManager.updateStatus((Guid)id, 1);
            return RedirectToAction("Index");
        }


        // Reservation: update status to rejected
        public ActionResult DeclineReservation(Guid? id)
        {
            resManager.updateStatus((Guid)id, -1);
            return RedirectToAction("Index");
        }
        public IActionResult Payment() {
            return View();
        }
        [HttpPost]
        public IActionResult ConfirmPayment()
        {
            TempData["Msg"] = "Your payment was confirmed, see the show delivery page for more details";
            return RedirectToAction("client");
        }
        [HttpPost]
        public ActionResult SaveReservation(ReservationAndMealListVO model,DateTime date,TimeSpan hour,string[] mealIds,int[] numberP)
        {
            if (date == null || hour == null || model == null || model.email == null || model.name == null || model.pers== null)
            {

                TempData["AlertMessage"] = "Please make sure that every field is completed correctly";
                Debug.WriteLine("Please make sure that every field is completed correctly");
                return RedirectToAction("createReservation");
            }
            //create basket for meals 
            Guid tableID = (Guid)tableManager.takeAvailableTable(date.ToString(), hour.ToString(), model.pers.ToString());
            if (tableID != Guid.Empty)
            {
                //create basket for meals 
                Guid basketID = (Guid)(basketManager.insert());

                for (int i = 0; i < mealIds.Length; i++)
                {
                    if(numberP != null && numberP[i] != 0)
                        portionManager.insert(mealIds[i], numberP[i], basketID);
                }

                Guid personID = (Guid)personManager.insert(model);
                Guid customerID= (Guid)customerManager.insert(model.email,personID);
                Guid reservationID = (Guid)resManager.InsertReservation(model,customerID,basketID);
                tableStatusManager.insertTableStatus(reservationID, tableID);
                return RedirectToAction("Index");
            }
            else
            {
                TempData["AlertMessage"] = "There is no table available for this date and time";
                Debug.WriteLine("There is no table available for this date and time");
                    return RedirectToAction("createReservation");

            }
        }
        public ActionResult SaveDelivery(string[] mealIds, int[] numberP,string Cname,string Address,string PhoneNumber,string Email ) {

            //create basket for meals 
            Guid basketID = (Guid)basketManager.insert();

            for (int i = 0;i < mealIds.Length;i++)
            {
                if(numberP[i] != 0)
                    portionManager.insert(mealIds[i], numberP[i], basketID);
            }

            //create customer based on the object
            Guid personID = (Guid)personManager.insert(Cname);
            Guid customerID = (Guid)customerManager.insert(Address,Email, personID);
            OrderManager.Instance.insert(PhoneNumber, basketID, customerID);

            return RedirectToAction("Payment");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}