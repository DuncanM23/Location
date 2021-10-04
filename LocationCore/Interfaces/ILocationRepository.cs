using LocationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationCore.Interfaces
{
    public interface ILocationRepository
    {
        Location SaveLocation(Location currentLocation);
        Location GetCurrent(string username);
        IEnumerable<Location> GetHistory(string username);
        IEnumerable<Location> GetAllCurrent();
    }
}
