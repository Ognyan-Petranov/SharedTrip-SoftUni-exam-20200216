using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedTrip.Data
{
    public class User
    {
        public User()
        {
            this.UserId = Guid.NewGuid().ToString();
            this.Trips = new HashSet<UserTrips>();
        }

        [Key]
        public string UserId { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public ICollection<UserTrips> Trips { get; set; }
    }
}
