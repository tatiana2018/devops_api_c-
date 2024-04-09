using Models.Cars;
using Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IRepositoryCars
    {
        Task<Car> AddCar(Car car);
        Task<List<Car>> GetCars(LocationFilters filters, int carMarket);
    }
}
