using DbApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .Build();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbAppContext>(
    builder => {
        builder.UseNpgsql(configuration.GetConnectionString("DbApp"));
    });

var app = builder.Build();

var db = app.Services.GetRequiredService<DbAppContext>();

await db.Database.MigrateAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", (DbAppContext db) =>
{
    return db.WeatherForecasts.ToList();
})
.WithName("GetWeatherForecast")
.WithOpenApi();


app.Run();