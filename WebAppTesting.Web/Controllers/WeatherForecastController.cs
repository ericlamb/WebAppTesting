using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebAppTesting.Web.Models;

namespace WebAppTesting.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly WeatherContext _context;

        public WeatherForecastController(WeatherContext context) => _context = context;

        [HttpGet]
        public IEnumerable<WeatherForecast> Get() =>
            _context.Forecasts.Select(f =>
                new WeatherForecast { Id = f.Id, Date = f.Date, TemperatureC = f.TemperatureC, Summary = f.Summary });

        [HttpDelete("{id}")]
        public OkResult Delete(int id)
        {
            var forecast = _context.Forecasts.FirstOrDefault(x => x.Id == id);
            if (forecast != null)
            {
                _context.Forecasts.Remove(forecast);
                _context.SaveChanges();
            }

            return Ok();
        }
    }
}
