using System.ComponentModel.DataAnnotations;

namespace ProjectWithUnitTesting
{
    public class WeatherForecast
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }

        public string? Secret { get; set; }

    }
}