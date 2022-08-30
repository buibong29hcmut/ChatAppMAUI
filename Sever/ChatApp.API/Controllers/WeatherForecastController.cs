using ChatApp.Domain.Entities;
using ChatApp.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ChatDbContext _Db;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, ChatDbContext db)
        {
            _logger = logger;
            _Db = db;   
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<User> Get()
        {
            return _Db.Users.ToList();
        }
    }
}