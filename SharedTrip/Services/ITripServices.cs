using SharedTrip.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface ITripServices
    {
        int AddTrip(string startPoint, string endPoint, DateTime departureTime, int seats, string description, string imagePath);

        IEnumerable<TripViewModel> GetAllTrips();

        IEnumerable<TripViewModel> GetAllTripsByUser(string userId);

        TripViewModel GetTripById(int Id);

        bool AddUserToTrip(string userId, int tripId);
    }
}
