using Application.Cars;
using Application.Contracts;
using Data.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Cars;
using Models.Filters;
using Moq;
using Xunit;

namespace Testing
{
    public class ApplicationAddCars
    {
        [Fact]
        public async Task SuccessfullAddReturnsOkResult()
        {
            Car car = new Car();
            car.Plate = "YDH73D";
            car.PickItUp = 1;
            car.Delivery = 2;
            car.Market = 5;
            car.Model = 2023;

            // Arrange
            var mockRepository = new Mock<IRepositoryCars>();
            mockRepository
                .Setup(repo => repo.AddCar(It.IsAny<Car>()))
                .ReturnsAsync(car);

            var handler = new AddCars.Handler(mockRepository.Object);
            var request = new AddCars.Query {
                Plate = "YDH73D",
                PickItUp = 1,
                Delivery = 2,
                Market = 5,
                Model = 2023
            };

            // Act
            var result = await handler.Handle(request, CancellationToken.None) as ObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

    }
}