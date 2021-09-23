namespace FlightPlanner.API.Models
{
    public class SearchFlightsRequest
    {
        public string To { get; set; }
        public string From { get; set; }
        public string DepartureDate { get; set; }
    }
}
