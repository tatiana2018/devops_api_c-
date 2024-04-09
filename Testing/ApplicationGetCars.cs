using Application.Cars;
using Application.Contracts;
using Data.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Cars;
using Models.Filters;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public class ApplicationGetCars
    {

        [Fact]
        public async Task SuccessfullGetReturnsOkResult()
        {

            LocationFilters filters = new();
            filters.CollectLocation = 1;
            filters.DeliveryLocation = 2;
            filters.Latitude = 6.263761990806296;
            filters.Longitude = -75.5212028829203;


            Car firstCar = new Car();
            firstCar.Plate = "YDH73D";
            firstCar.PickItUp = 1;
            firstCar.Delivery = 1;
            firstCar.Market = 3;
            firstCar.Model = 2023;

            Car secondCar = new Car();
            secondCar.Plate = "ZAH73D";
            secondCar.PickItUp = 1;
            secondCar.Delivery = 1;
            secondCar.Market = 3;
            secondCar.Model = 2020;

            List<Car> cars = new();
            cars.Add(firstCar);
            cars.Add(secondCar);

            // Arrange
            var mockRepository = new Mock<IRepositoryCars>();
            mockRepository
                .Setup(repo => repo.GetCars(It.IsAny<LocationFilters>(), It.IsAny<int>()))
                .ReturnsAsync(cars);

            var mockGeoService = new Mock<IGeolocationService>();
            mockGeoService.Setup(service => service.GetZone(It.IsAny<LocationFilters>()))
                          .ReturnsAsync(1);

            var mockDefService = new Mock<IDefinedService>();
            mockDefService.Setup(service => service.DefinedCarType(It.IsAny<int>(), It.IsAny<int>()))
                          .ReturnsAsync(1);

            var handler = new GetCars.Handler(mockRepository.Object, mockGeoService.Object, mockDefService.Object);
            var request = new GetCars.Query { filters = filters };

            // Act
            var result = await handler.Handle(request, CancellationToken.None) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }
    }
}
