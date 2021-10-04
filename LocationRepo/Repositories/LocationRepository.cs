using LocationCore.Interfaces;
using LocationCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocationRepo.Repositories
{
    public class LocationRepository :ILocationRepository
    {
        private readonly LocationDataContext _datacontext;

        public LocationRepository(LocationDataContext datacontext)
        {
            _datacontext = datacontext;
        }
        public Location SaveLocation(Location currentLocation)
        {
             _datacontext.Add<Location>(currentLocation);
            _datacontext.SaveChanges();

            return currentLocation;
        }
        public Location GetCurrent(string username)
        {
            return _datacontext.Set<Location>()
                .Where(x=>x.Username == username)
                .AsNoTracking()
                .OrderByDescending(x=>x.LocationDate)
                .FirstOrDefault();
        }
        public IEnumerable<Location> GetHistory(string username)
        {
            return _datacontext.Set<Location>()
                .Where(x => x.Username == username)
                .AsNoTracking()
                .OrderByDescending(x => x.LocationDate);
        }

        public IEnumerable<Location> GetAllCurrent()
        {
            //TODO - the group by is happening client-side - make it happen in SQL instead.
            return _datacontext.Set<Location>()
                .AsNoTracking()
                .AsEnumerable()
                .GroupBy(x => x.Username)
                .SelectMany(y => y.Where(z => z.LocationDate == y.Max(i => i.LocationDate)));

        }

    }
}
