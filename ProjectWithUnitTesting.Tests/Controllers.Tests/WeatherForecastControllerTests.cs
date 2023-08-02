//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;
//using Xunit;
//using System.Collections.Generic;
//using System.Linq;
//using System;
//using ProjectWithUnitTesting.Controllers;
//using AutoFixture;
//using Microsoft.EntityFrameworkCore;
//using ProjectWithUnitTesting.Data;

//namespace ProjectWithUnitTesting.Tests.Controllers.Tests
//{
//    public class WeatherForecastControllerTests
//    {
//        private readonly WeatherForecastController _weatherForecastController;
//        private readonly Mock<ILogger<WeatherForecastController>> _loggerMock;
//        private readonly TodoContext _dbContext;
//        private readonly Fixture _fixture;
//        private DbContextOptions<TodoContext> options;

//        public WeatherForecastControllerTests()
//        {
//            _loggerMock = new Mock<ILogger<WeatherForecastController>>();
//            _fixture = new Fixture();
//            _dbContext = new TodoContext(options);

//            _weatherForecastController = new WeatherForecastController(_loggerMock.Object, _dbContext);
//        }

//        [Fact]
//        public void ReturnFiveWeatherForecasts()
//        {
//            // Act
//            var result = _weatherForecastController.Get();

//            // Assert
//            Assert.NotNull(result);
//            var weatherForecasts = Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(result);
//            Assert.Equal(5, weatherForecasts.Count());
//        }

//        [Fact]
//        public void ReturnValidWeatherForecasts()
//        {
//            // Arrange

//            // Act
//            var result = _weatherForecastController.Get();

//            // Assert
//            Assert.NotNull(result);
//            var weatherForecasts = Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(result);

//            //foreach (var forecast in weatherForecasts)
//            //{
//            //    Assert.InRange(forecast.TemperatureC, -20, 54);
//            //    Assert.Contains(forecast.Summary, WeatherForecastController.Summaries);
//            //}
//        }


//        // want to fuck your step aunt in ass? - Family Fantasy


//        [Fact]
//        public void VerifyLoggerIsCalled()
//        {
//            // Act
//            var result = _weatherForecastController.Get();

//            // Assert
//            _loggerMock.Verify(logger => logger.Log(
//                It.IsAny<LogLevel>(),
//                It.IsAny<EventId>(),
//                It.IsAny<It.IsAnyType>(),
//                It.IsAny<Exception>(),
//                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
//            ), Times.Once);
//        }

//        // Add other test cases as per the provided list in the previous response.
//    }
//}








using Xunit;
using Moq;
using ProjectWithUnitTesting.Controllers;
using ProjectWithUnitTesting.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectWithUnitTesting.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ProjectWithUnitTesting.Tests.Controllers.Tests
{
    public class WeatherForecastControllerTests
    {
        //private readonly WeatherForecastController _weatherForecastController;
        //private readonly Mock<ILogger<WeatherForecastController>> _loggerMock;
        //private readonly Mock<TodoContext> _contextMock;

        //public WeatherForecastControllerTests()
        //{
        //    _loggerMock = new Mock<ILogger<WeatherForecastController>>();
        //    _contextMock = new Mock<TodoContext>();
        //    _weatherForecastController = new WeatherForecastController(_loggerMock.Object, _contextMock.Object);
        //}

        private readonly WeatherForecastController _weatherForecastController;
        private readonly TodoContext _context;

        public WeatherForecastControllerTests()
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            _context = new TodoContext(options);

            _weatherForecastController = new WeatherForecastController(new LoggerFactory().CreateLogger<WeatherForecastController>(), _context);
        }

        //[Fact]
        //public async Task Get_ReturnsValidWeatherForecasts()
        //{
        //    // Arrange
        //    var weatherForecasts = new List<WeatherForecastDTO>
        //    {
        //        new WeatherForecastDTO { Id = 1, Date = new DateTime(2023, 07, 20), TemperatureC = 25, Summary = "Sunny" },
        //        new WeatherForecastDTO { Id = 2, Date = new DateTime(2023, 07, 21), TemperatureC = 28, Summary = "Cloudy" },
        //        new WeatherForecastDTO { Id = 3, Date = new DateTime(2023, 07, 22), TemperatureC = 30, Summary = "Rainy" }
        //    };

        //    var weatherForecastsQueryable = weatherForecasts.AsQueryable();
        //    var weatherForecastsDbSetMock = new Mock<DbSet<WeatherForecastDTO>>();
        //    weatherForecastsDbSetMock.As<IQueryable<WeatherForecastDTO>>().Setup(m => m.Provider).Returns(weatherForecastsQueryable.Provider);
        //    weatherForecastsDbSetMock.As<IQueryable<WeatherForecastDTO>>().Setup(m => m.Expression).Returns(weatherForecastsQueryable.Expression);
        //    weatherForecastsDbSetMock.As<IQueryable<WeatherForecastDTO>>().Setup(m => m.ElementType).Returns(weatherForecastsQueryable.ElementType);
        //    weatherForecastsDbSetMock.As<IQueryable<WeatherForecastDTO>>().Setup(m => m.GetEnumerator()).Returns(weatherForecastsQueryable.GetEnumerator());

        //    _contextMock.Setup(c => c.WeatherForecasts).Returns(weatherForecastsDbSetMock.Object);

        //    // Act
        //    var result = await _weatherForecastController.Get();

        //    // Assert
        //    var actionResult = Assert.IsType<ActionResult<IEnumerable<WeatherForecastDTO>>>(result);
        //    var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        //    var weatherForecastList = Assert.IsAssignableFrom<IEnumerable<WeatherForecastDTO>>(okObjectResult.Value);

        //    Assert.Equal(3, weatherForecastList.Count());
        //    Assert.Contains(weatherForecasts[0], weatherForecastList);
        //    Assert.Contains(weatherForecasts[1], weatherForecastList);
        //    Assert.Contains(weatherForecasts[2], weatherForecastList);
        //}

        //[Fact]
        //public async Task GetTodoItem_ReturnsWeatherForecastById()
        //{
        //    // Arrange
        //    var weatherForecast = new WeatherForecastDTO { Id = 1, Date = new DateTime(2023, 07, 20), TemperatureC = 25, Summary = "Sunny" };

        //    _contextMock.Setup(c => c.WeatherForecasts.FindAsync(1)).ReturnsAsync(weatherForecast);

        //    // Act
        //    var result = await _weatherForecastController.GetTodoItem(1);

        //    // Assert
        //    var actionResult = Assert.IsType<ActionResult<WeatherForecastDTO>>(result);
        //    var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        //    var weatherForecastResult = Assert.IsAssignableFrom<WeatherForecastDTO>(okObjectResult.Value);

        //    Assert.Equal(weatherForecast.Id, weatherForecastResult.Id);
        //    Assert.Equal(weatherForecast.Date, weatherForecastResult.Date);
        //    Assert.Equal(weatherForecast.TemperatureC, weatherForecastResult.TemperatureC);
        //    Assert.Equal(weatherForecast.Summary, weatherForecastResult.Summary);
        //}

        //[Fact]
        //public async Task GetTodoItem_ReturnsWeatherForecastById()
        //{
        //    // Arrange
        //    int forecastId = 1;
        //    var weatherForecasts = new[]
        //    {
        //        new WeatherForecastDTO { Id = 1, Date = DateTime.Now, TemperatureC = 25, Summary = "Sunny" },
        //        new WeatherForecastDTO { Id = 2, Date = DateTime.Now.AddDays(1), TemperatureC = 30, Summary = "Cloudy" },
        //        new WeatherForecastDTO { Id = 3, Date = DateTime.Now.AddDays(2), TemperatureC = 20, Summary = "Rainy" },
        //    };
        //    _context.WeatherForecasts.AddRange(weatherForecasts);
        //    _context.SaveChanges();

        //    // Act
        //    var actionResult = await _weatherForecastController.GetTodoItem(forecastId);

        //    // Assert
        //    var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        //    var forecast = Assert.IsType<WeatherForecastDTO>(okObjectResult.Value);
        //    Assert.Equal(forecastId, forecast.Id);
        //}

        //[Fact]
        //public void GetSum()
        //{
        //    int sum = _weatherForecastController.add(4, 4);

        //    Assert.Equal(8, sum);
        //}

        [Fact]
        public async Task GetTodoItem_ReturnsWeatherForecastById()
        {
            // Arrange
            int forecastId = 1;
            var weatherForecasts = new[]
            {
                new WeatherForecastDTO { Id = 1, Date = DateTime.Now, TemperatureC = 25, Summary = "Sunny" },
                new WeatherForecastDTO { Id = 2, Date = DateTime.Now.AddDays(1), TemperatureC = 30, Summary = "Cloudy" },
                new WeatherForecastDTO { Id = 3, Date = DateTime.Now.AddDays(2), TemperatureC = 20, Summary = "Rainy" },
            };

            // Add the test data to the in-memory database
            foreach (var forecast1 in weatherForecasts)
            {
                _context.WeatherForecasts.Add(forecast1);
            }
            _context.SaveChanges();

            // Act
            var actionResult = await _weatherForecastController.GetTodoItem(forecastId);

            // Assert
            Assert.NotNull(actionResult);
            Assert.IsType<ActionResult<WeatherForecastDTO>>(actionResult);

           // var okObjectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
           // var forecast = Assert.IsType<WeatherForecastDTO>(okObjectResult.Value);
            Assert.Equal(forecastId, actionResult.Value.Id);
        }



        // Implement other test cases for Post, Put, and Delete methods as needed.
    }
}
