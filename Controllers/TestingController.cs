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
        
        [HttpPost]
        public IActionResult Clear()
        {
            FlightStorage.ClearFlights();
            return Ok();
        }
    }
}
