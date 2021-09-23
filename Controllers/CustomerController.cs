using FlightPlanner.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FlightPlanner.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IFlightStorage _flightStorage;       

        public CustomerController(IFlightStorage flightStorage)
        {
            _flightStorage = flightStorage;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirportsByPhrase(string search)
        {
            var airports = _flightStorage.GetAirportByPhrase(search);
            if (airports.Count == 0)
            {
                return NotFound();
            }
            return Ok(airports);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlightById(int id)
        {
            var flight = _flightStorage.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight([FromBody] SearchFlightsRequest request)
        {
            var allFlights = _flightStorage.GetAllFlights();
            var filteredFlights = allFlights.Where(x =>
            x.To.AirportCode == request.To &&
            x.From.AirportCode == request.From &&
            x.DepartureTime.Substring(0, 10) == request.DepartureDate).ToList();

            var page = new PageResult { Items = filteredFlights.ToArray(), Page = 0, TotalItems = filteredFlights.Count };

            if (WrongFormat(request) || SameAirport(request))
            {
                return BadRequest();
            }
            return Ok(page);
        }

        private static bool SameAirport(SearchFlightsRequest flight)
        {
            return flight.To == flight.From;
        }

        private static bool WrongFormat(SearchFlightsRequest flight)
        {
            if (flight == null ||
                string.IsNullOrEmpty(flight.To) ||
                string.IsNullOrEmpty(flight.From) ||
                string.IsNullOrEmpty(flight.DepartureDate))
            {
                return true;
            }
            return false;
        }
    }
}
