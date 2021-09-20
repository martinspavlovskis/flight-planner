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
    [Route("testing-api/clear")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly IFlightStorage _flightStorage;

        public TestingController(IFlightStorage flightStorage)
        {
            _flightStorage = flightStorage;
        }

        [HttpPost]
        public IActionResult Clear()
        {
            _flightStorage.ClearFlights();
            return Ok();
        }
    }
}
