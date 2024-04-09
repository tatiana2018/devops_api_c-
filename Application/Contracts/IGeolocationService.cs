using Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IGeolocationService
    {
        Task<int> GetZone(LocationFilters filters);
    }
}
