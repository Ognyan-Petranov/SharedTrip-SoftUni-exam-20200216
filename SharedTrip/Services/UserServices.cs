using Microsoft.EntityFrameworkCore;
using SharedTrip.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SharedTrip.Services
{
    public class UserServices : IUserServices
    {
        private readonly ApplicationDbContext dbContext;

        public UserServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void RegisterUser(string username, string email, string password)
        {
            var user = new User()
            {
                Username = username,
                Email = email,
                Password = this.GenerateSHA512String(password)
            };
            this.dbContext.Users.Add(user);
            this.dbContext.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var user = this.dbContext.Users.FirstOrDefault(x => x.Username == username && x.Password == this.GenerateSHA512String(password));

            if (user is null)
            {
                return null;
            }
            return user.UserId;
        }

        public bool IsValidEmail(string email)
        {
            return this.dbContext.Users.FirstOrDefault(x => x.Email == email) == null;
        }

        public bool IsValidUser(string username)
        {
            return this.dbContext.Users.FirstOrDefault(x => x.Username == username) == null;
        }

        private string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        private string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
