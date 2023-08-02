using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectWithUnitTesting.Data;
using ProjectWithUnitTesting.DTO;

namespace ProjectWithUnitTesting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly TodoContext _context;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, TodoContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecastDTO> Get()
        {
            _logger.LogDebug("hello");


            return  _context.WeatherForecasts.ToList();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecastDTO>> GetTodoItem(int id)
        {
            var todoItem = await _context.WeatherForecasts.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<WeatherForecast>> Post(WeatherForecastDTO weatherForecast)
        {
            _context.WeatherForecasts.Add(weatherForecast);
            await _context.SaveChangesAsync();

            //    return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(Post), new { id = weatherForecast.Id }, weatherForecast);

           
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var forecast = await _context.WeatherForecasts.FindAsync(id);
            if (forecast == null)
            {
                return NotFound();
            }

            _context.WeatherForecasts.Remove(forecast);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, WeatherForecastDTO forecast)
        {
            if (id != forecast.Id)
            {
                return BadRequest();
            }

            var todoItem = await _context.WeatherForecasts.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Date = forecast.Date;
            todoItem.TemperatureC = forecast.TemperatureC;
            todoItem.Summary = forecast.Summary;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.WeatherForecasts.Any(e => e.Id == id);
        }

        //public int add(int a, int b)
        //{
        //    return a + b; 
        //}

    }
}