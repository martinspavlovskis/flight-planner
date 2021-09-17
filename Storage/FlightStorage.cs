using FlightPlanner.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlanner.API.Storage
{
    public class FlightStorage
    {
        private static readonly List<Flight> _flights = new();
        private static int _id;

        public static Flight GetFlightById(int id)
        {
            return _flights.FirstOrDefault(x => x.Id == id);
        }

        public static List<Flight> GetAllFlights()
        {
            return _flights.ToList();
        }

        public static void ClearFlights()
        {
            _flights.Clear();
        }

        public static Flight AddFlight(Flight flight)
        {
            flight.Id = _id;
            _id++;
            _flights.Add(flight);
            return flight;
        }

        public static void DeleteFlight(int id)
        {
            _flights.Remove(GetFlightById(id));
        }

        public static Flight MakeFlight(AddFlightRequest flight)
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

        public static List<Airport> GetAirportByPhrase(string input)
        {
            input = input.Trim().ToLower();
            return _flights.Select(x => x.From).Where
                (x =>
            x.AirportCode.ToLower().Contains(input) ||
            x.City.ToLower().Contains(input) ||
            x.Country.ToLower().Contains(input)).ToList();
        }
    }
}
