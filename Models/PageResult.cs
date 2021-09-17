using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlanner.API.Models
{
    public class PageResult
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public Flight[] Items { get; set; } 
    }
}
