using Application.Contracts;
using Application.Services;
using Data.Contracts;
using Models.Cars;
using Models.Enums;
using Models.Filters;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public class ApplicationServices
    {


        [Fact]
        public async Task DefinedCarType_ReturnsCorrectCode()
        {
            int expectedCode = 3;
            int locationCod = 1;
            int collectCode = 2;

            // Arrange
            var definedService = new DefinedService();

            // Act
            var result = await definedService.DefinedCarType(locationCod, collectCode);

            // Assert
            Assert.Equal(expectedCode, result);
        }

        [Fact]
        public async Task DefinedCarType_WithInvalidInput_ReturnsDefault()
        {
            // Arrange
            var definedService = new DefinedService();
            int locationCod = 0;
            int collectCode = 0;

            // Act
            var result = await definedService.DefinedCarType(locationCod, collectCode);

            // Assert
            Assert.Equal(0, result);
        }

    }
}
