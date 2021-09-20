using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlanner.API.Models
{
    public interface IFlightStorage
    {
        public Flight GetFlightById(int id);
        public ICollection<Flight> GetAllFlights();
        public void ClearFlights();
        public Flight AddFlight(Flight flight);
        public void DeleteFlight(int id);
        public Flight MakeFlight(AddFlightRequest flight);
        public List<Airport> GetAirportByPhrase(string input);
    }
}
