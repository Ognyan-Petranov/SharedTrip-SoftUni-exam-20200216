using SharedTrip.Data;
using SharedTrip.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SharedTrip.Services
{
    public class TripServices : ITripServices
    {
        private readonly ApplicationDbContext dbContext;

        public TripServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int AddTrip(string startPoint, string endPoint, DateTime departureTime, int seats, string description, string imagePath)
        {
            var trip = new Trip() {
            StartPoint = startPoint,
            EndPoint = endPoint,
            DepartureTime = departureTime,
            Seats = seats,
            Description = description,
            ImagePath = imagePath
            };

            this.dbContext.Trips.Add(trip);
            this.dbContext.SaveChanges();
            return trip.TripId;
        }

        public IEnumerable<TripViewModel> GetAllTrips()
        {
            return this.dbContext.Trips.Select(x => new TripViewModel() { 
            StartPoint = x.StartPoint,
            EndPoint = x.EndPoint,
            DepartureTime = x.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
            Seats = x.Seats,
            Description = x.Description,
            ImagePath = x.ImagePath
            }).ToList();
        }

        public IEnumerable<TripViewModel> GetAllTripsByUser(string userId)
        {
            return this.dbContext.UserTrips.Where(u => u.UserId == userId).Select(x => new TripViewModel()
            {
                StartPoint = x.Trip.StartPoint,
                EndPoint = x.Trip.EndPoint,
                DepartureTime = x.Trip.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                Seats = x.Trip.Seats,
                Description = x.Trip.Description,
                ImagePath = x.Trip.ImagePath
            }).ToList();
        }
    }
}
