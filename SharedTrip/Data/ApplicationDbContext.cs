namespace SharedTrip
{
    using Microsoft.EntityFrameworkCore;
    using SharedTrip.Data;

    public class ApplicationDbContext : DbContext
    { 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserTrips>().HasKey(x => new { x.UserId, x.TripId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
