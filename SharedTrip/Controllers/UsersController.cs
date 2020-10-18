using SharedTrip.Services;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTrip.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserServices userServices;

        public UsersController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpGet]
        public HttpResponse Login()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }
        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.userServices.GetUserId(username, password);

            if (userId == null)
            {
                return this.Error("Invalid username or password!");
            }
            //TODO: Some logic here
            this.SignIn(userId);
            return this.Redirect("/Trips/All");
        }

        [HttpGet]
        public HttpResponse Register()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(string username, string email, string password, string confirmPassword)
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            if (password != confirmPassword)
            {
                return this.Error("Both passwords did not match!");
            }

            if (string.IsNullOrEmpty(username) || username.Length < 5 || username.Length > 20)
            {
                return this.Error("Invalid username. Username must be between 5 and 20 characters!");
            }

            if (string.IsNullOrEmpty(email) || !new EmailAddressAttribute().IsValid(email))
            {
                return this.Error("Invalid email address!");
            }

            this.userServices.RegisterUser(username, email, password);

            return this.Redirect("/Users/Login");
        }

        [HttpGet("/Users/Logout")]
        public HttpResponse Logout()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Error("In order to log out you need to log in first!");
            }

            this.SignOut();
            return this.Redirect("/");
        }
    }
}
