using Microsoft.EntityFrameworkCore;
using ProjectWithUnitTesting;
using ProjectWithUnitTesting.DTO;

namespace ProjectWithUnitTesting.Data
{

    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherForecastDTO> WeatherForecasts { get; set; } = null!;

        public DbSet<EmployeeDTO> Employees { get; set; } = null;
    }
}



