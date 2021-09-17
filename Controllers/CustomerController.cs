using FlightPlanner.API.Models;
using FlightPlanner.API.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlanner.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirportsByPhrase(string search)
        {
            var airports = FlightStorage.GetAirportByPhrase(search);
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
            var flight = FlightStorage.GetFlightById(id);
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
            var allFlights = FlightStorage.GetAllFlights();
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
