using System;

namespace WebAppTesting.Web.Models
{
    public class WeatherForecastEntity
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }
    }
}
