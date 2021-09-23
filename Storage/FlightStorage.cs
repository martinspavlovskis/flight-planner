using FlightPlanner.API.Data;
using FlightPlanner.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.API.Storage
{
    public class FlightStorage : IFlightStorage
    {
        private readonly FlightPlannerDbContext _db;
        public FlightStorage(FlightPlannerDbContext db)
        {
            _db = db;
        }

        public Flight GetFlightById(int id)
        {
            return _db.Flights
                .Include(a=> a.To)
                .Include(a => a.From)
                .FirstOrDefault(x => x.Id == id);
        }

        public ICollection<Flight> GetAllFlights()
        {
            return _db.Flights.Include(a=>a.To).Include(a=>a.From).ToList();
        }

        public void ClearFlights()
        {
            _db.Flights.RemoveRange(_db.Flights);             
            _db.SaveChanges();
        }

        public Flight AddFlight(Flight flight)
        {         
            _db.Flights.Add(flight);
            _db.SaveChanges();
            return flight;
        }

        public void DeleteFlight(int id)
        {         
            var flight = _db.Flights
                .Include(a => a.To)
                .Include(a => a.From)
                .FirstOrDefault(x => x.Id == id);
            if(flight != null)
            {
                _db.Flights.Remove(flight);
                _db.SaveChanges();
            }

        }

        public Flight MakeFlight(AddFlightRequest flight)
        {
            return new Flight
            {
                ArrivalTime = flight.ArrivalTime,
                DepartureTime = flight.DepartureTime,
                From = flight.From,
                To = flight.To,
                Carrier = flight.Carrier
            };
        }

        public List<Airport> GetAirportByPhrase(string input)
        {
            input = input.Trim().ToLower();
            return _db.Flights.Select(x => x.From).Where
                (x =>
            x.AirportCode.ToLower().Contains(input) ||
            x.City.ToLower().Contains(input) ||
            x.Country.ToLower().Contains(input)).ToList();
        }              
    }
}
