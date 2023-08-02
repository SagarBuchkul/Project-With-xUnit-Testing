
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectWithUnitTesting.Controllers;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;


namespace ProjectWithUnitTesting.Tests
{
    public class WeatherForecastControllerTests
    {
        [Fact]
        public void Get_ReturnsWeatherForecasts()
        {
            // Arrange
            var controller = new WeatherForecastController(Mock.Of<ILogger<WeatherForecastController>>());

            // Act
            var result = controller.Get();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(result);
        }
    }
}
