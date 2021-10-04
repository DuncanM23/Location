using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationCore.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        //TODO turn this into a userId and hook up to real users
        public string Username { get; set; }
        public DateTime LocationDate { get; set; }
    }
}
