using LocationCore.Interfaces;
using LocationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly ILocationService _locationService;

        public LocationController(ILogger<LocationController> logger,
                                    ILocationService locationService)
        {
            _logger = logger;
            _locationService = locationService;
        }

        [HttpPost]
        public Location SaveLocation(Location currentLocation)
        {
            return _locationService.SaveLocation(currentLocation);
        }

        [HttpGet, Route("/history/{username}")]
        public IEnumerable<Location> GetHistory(string username)
        {
            return _locationService.GetHistory(username);
        }
        [HttpGet, Route("/{username}")]
        public Location GetCurrent(string username)
        {
            return _locationService.GetCurrent(username);
        }
        [HttpGet, Route("/all")]
        public IEnumerable<Location> GetAllCurrent()
        {
            return _locationService.GetAllCurrent();
        }
        [HttpPost, Route("/allinarea")]
        public IEnumerable<Location> GetAllInArea(double distance, Location location)
        {
            return _locationService.GetAllInArea(distance, location);
        }
    }
}
