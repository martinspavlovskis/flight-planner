using FlightPlanner.API.Models;
using Microsoft.AspNetCore.Mvc;

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
