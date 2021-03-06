using FlightPlanner.API.Models;
using FlightPlanner.API.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlanner.API.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private static readonly object _locker = new();

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            lock (_locker)
            {
                var flight = FlightStorage.GetFlightById(id);
                if (flight is null)
                {
                    return NotFound();
                }
                return Ok(flight);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(AddFlightRequest flight)
        {
            lock (_locker)
            {
                var newFlight = FlightStorage.MakeFlight(flight);

                if (FlightAlreadyExists(flight))
                {
                    return Conflict();
                }

                if (WrongFormat(flight) || SameAirport(flight) || InvalidDates(flight))
                {
                    return BadRequest();
                }

                FlightStorage.AddFlight(newFlight);
                return Created("", newFlight);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_locker)
            {
                FlightStorage.DeleteFlight(id);
                return Ok();
            }
        }

        private static bool SameAirport(AddFlightRequest flight)
        {
            return flight.To.AirportCode.ToUpper().Trim() == flight.From.AirportCode.ToUpper().Trim();
        }

        private static bool InvalidDates(AddFlightRequest flight)
        {
            return DateTime.Parse(flight.DepartureTime) >= DateTime.Parse(flight.ArrivalTime);
        }

        private static bool FlightAlreadyExists(AddFlightRequest request)
        {
            return FlightStorage.GetAllFlights().Any(x =>
                x.From.AirportCode == request.From.AirportCode &&
                x.From.City == request.From.City &&
                x.From.Country == request.From.Country &&
                x.To.AirportCode == request.To.AirportCode &&
                x.To.City == request.To.City &&
                x.To.Country == request.To.Country &&
                x.Carrier == request.Carrier &&
                x.ArrivalTime == request.ArrivalTime &&
                x.DepartureTime == request.DepartureTime
            );
        }

        private static bool WrongFormat(AddFlightRequest flight)
        {
            return (flight.From == null ||
                 flight.To == null ||
                 string.IsNullOrEmpty(flight.Carrier) ||
                 string.IsNullOrEmpty(flight.DepartureTime) ||
                 string.IsNullOrEmpty(flight.ArrivalTime) ||
                 string.IsNullOrEmpty(flight.To.Country) ||
                 string.IsNullOrEmpty(flight.To.City) ||
                 string.IsNullOrEmpty(flight.To.AirportCode) ||
                 string.IsNullOrEmpty(flight.From.Country) ||
                 string.IsNullOrEmpty(flight.From.City) ||
                 string.IsNullOrEmpty(flight.From.AirportCode)
                 );
        }
    }
}
