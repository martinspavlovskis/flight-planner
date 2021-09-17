using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlanner.API.Models
{
    public class SearchFlightsRequest
    {
        public string To { get; set; }
        public string From { get; set; }
        public string DepartureDate { get; set; }
    }
}
