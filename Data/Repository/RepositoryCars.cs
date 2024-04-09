using Data.Contracts;
using Microsoft.Extensions.Options;
using Models.Cars;
using Models.ConexionDataBase;
using Models.Filters;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace Data.Repository
{
    public class RepositoryCars : IRepositoryCars
    {

        private readonly IMongoCollection<Car> _cars;

        public RepositoryCars(IOptions<MongoConexion> context)
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(context.Value.ConexionChain);
            settings.RetryWrites = false;
            MongoClient cliente = new(settings);
            IMongoDatabase _baseDatos = cliente.GetDatabase(context.Value.DataBase);
            _cars = _baseDatos.GetCollection<Car>("T_Cars");
        }

        //Metodo que inserta un carro en base de datos
        public async Task<Car> AddCar(Car car)
        {  
            await _cars.InsertOneAsync(car);
            Car insertedCar = await _cars.Find(c => c.Plate == car.Plate).FirstOrDefaultAsync();
            return await Task.Run(() => insertedCar);
        }


        //Metodo que busca carros segun localizacion del usuario y tipo de carro requerido
        public async Task<List<Car>> GetCars(LocationFilters filters, int carMarket)
        {
            var mongoBuilder = Builders<Car>.Filter;

            List<FilterDefinition<Car>> customFilters = GenerateFilters(filters, carMarket, mongoBuilder);

            var query = _cars
                .Find(mongoBuilder.And(customFilters))
                .Sort(Builders<Car>.Sort.Descending("model"));

            List<Car> queryCars = query.ToList();

            return await Task.Run(()=> queryCars);

        }

        private static List<FilterDefinition<Car>> GenerateFilters(LocationFilters filters, int carMarket, FilterDefinitionBuilder<Car> builder)
        {
            var filter = new List<FilterDefinition<Car>>();

            int? collect = filters.CollectLocation;
            int? delivery = filters.DeliveryLocation;
            int? market = carMarket;

            if (collect.HasValue)
            {
                filter.Add(builder.Eq(c => c.PickItUp, filters.CollectLocation));
            }

            if (delivery.HasValue)
            {
                filter.Add(builder.Eq(c => c.Delivery, filters.DeliveryLocation));
            }

            if (market.HasValue)
            {
                filter.Add(builder.Eq(c => c.Market, carMarket));
            }
            
            List<string> list = new();

            return filter;
        }

    }
}
