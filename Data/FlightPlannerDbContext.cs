using FlightPlanner.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlanner.API.Data
{
    public class FlightPlannerDbContext : DbContext
    {
        public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext> options)
            : base(options)
        {
        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
