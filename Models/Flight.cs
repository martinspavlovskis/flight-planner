using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightPlanner.API.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        
        public string Carrier { get; set; }
        
        public Airport From { get; set; }
        
        public Airport To { get; set; }
        
        public string DepartureTime { get; set; }
        
        public string ArrivalTime { get; set; }
    }
}
