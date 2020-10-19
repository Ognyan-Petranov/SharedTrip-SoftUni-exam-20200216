using SharedTrip.Services;
using SharedTrip.ViewModels;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripServices tripServices;
        private readonly IUserServices userServices;

        public TripsController(ITripServices tripServices, IUserServices userServices)
        {
            this.tripServices = tripServices;
            this.userServices = userServices;
        }

        [HttpGet]
        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(string startPoint, string endPoint, DateTime departureTime, int seats, string description, string imagePath)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(startPoint))
            {
                return this.Error("Starting point is required!");
            }

            if (string.IsNullOrEmpty(endPoint))
            {
                return this.Error("End point is required!");
            }

            if (departureTime == null)
            {
                return this.Error("Invalid departure time! Departure time is required!");
            }

            if (seats < 2 || seats > 6)
            {
                return this.Error("Seats must be between 2 and 6");
            }

            if (string.IsNullOrEmpty(description))
            {
                return this.Error("Description is required! Max length is 80 characters!");
            }

            this.tripServices.AddTrip(startPoint, endPoint, departureTime, seats, description, imagePath);
            return this.Redirect("/Trips/All");
        }

        [HttpGet]
        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }
            ViewAllTripsModel trips = new ViewAllTripsModel() { Trips = this.tripServices.GetAllTrips().ToList() };
            return this.View(trips);
        }

        [HttpGet]
        public HttpResponse Details()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }
    }
}
