
using Microsoft.EntityFrameworkCore;

namespace DbApp.Data;

public class DbAppContext(DbContextOptions<DbAppContext> options) : DbContext(options)
{
    public DbSet<WeatherForecast> WeatherForecasts {get;set;}
}