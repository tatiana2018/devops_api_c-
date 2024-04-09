using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts;

namespace Application.Services
{
    public class DefinedService : IDefinedService
    {
        public Task<int> DefinedCarType(int locationCod, int collectCode)
        {
            int codArea = 0;
            if (locationCod != 0 && collectCode != 0)
            {
                codArea = (locationCod == 1 && collectCode == 1) ? 1 :
                          (locationCod == 2 && collectCode == 2) ? 2 : 3;
            }

            return Task.FromResult(codArea);
        }
    }
}
