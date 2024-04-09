using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Filters
{
    public class LocationFilters
    {
        public int CollectLocation { get;  set; }

        public int DeliveryLocation { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
