using LocationCore.Interfaces;
using LocationCore.Models;
using System;
using System.Collections.Generic;

namespace LocationCore.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private const int EarthRad = 6371000; // metres
        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public Location SaveLocation(Location currentLocation)
        {
            return _locationRepository.SaveLocation(currentLocation);
        }
        public Location GetCurrent(string username)
        {
            return _locationRepository.GetCurrent(username);
        }
        public IEnumerable<Location> GetHistory(string username)
        {
            return _locationRepository.GetHistory(username);
        }

        public IEnumerable<Location> GetAllCurrent()
        {
            return _locationRepository.GetAllCurrent();
        }

        public IEnumerable<Location> GetAllInArea(double distance, Location location)
        {
            //Using Haversine formula taken from https://www.movable-type.co.uk/scripts/latlong.html

            var currentLocationForAll = _locationRepository.GetAllCurrent();

            var φ1 = location.Latitude * Math.PI / 180; // φ, λ in radians
            var currentLocationsInArea = new List<Location>();


            foreach (var userLocation in currentLocationForAll)
            {
                var φ2 = userLocation.Latitude * Math.PI / 180;
                var Δφ = (userLocation.Latitude - location.Latitude) * Math.PI / 180;
                var Δλ = (userLocation.Longitude - location.Longitude) * Math.PI / 180;

                var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) + Math.Cos(φ1) * Math.Cos(φ2) * Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
                var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                var d = EarthRad * c; // in metres

                if (d < distance)
                    currentLocationsInArea.Add(userLocation);
            }

            return currentLocationsInArea;
        }
    }
}
