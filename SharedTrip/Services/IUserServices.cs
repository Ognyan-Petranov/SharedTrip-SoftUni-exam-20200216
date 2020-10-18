using System;
using System.Collections.Generic;
using System.Text;

namespace SharedTrip.Services
{
    public interface IUserServices
    {
        void RegisterUser(string username, string email, string password);

        bool IsValidEmail(string email);

        bool IsValidUser(string username);

        string GetUserId(string username, string password);
    }
}
