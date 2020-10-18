﻿namespace SharedTrip
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
           
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}
