using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAppTesting.Web.Models
{
    public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecastEntity>
    {
        public void Configure(EntityTypeBuilder<WeatherForecastEntity> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id).ValueGeneratedOnAdd();
            builder.HasAlternateKey(w => w.Date);
            builder.Property(w => w.Summary).IsRequired();
            builder.Property(w => w.TemperatureC).IsRequired();

            builder.HasData(
                new WeatherForecastEntity
                {
                    Id = 1,
                    Date = DateTime.Parse("2021-03-13T05:00:00.0000-05:00"),
                    TemperatureC = 47,
                    Summary = "Hot"
                },
                new WeatherForecastEntity
                {
                    Id = 2,
                    Date = DateTime.Parse("2021-03-14T04:00:00.0000-04:00"),
                    TemperatureC = -15,
                    Summary = "Balmy"
                },
                new WeatherForecastEntity
                {
                    Id = 3,
                    Date = DateTime.Parse("2021-03-15T04:00:00.0000-04:00"),
                    TemperatureC = 39,
                    Summary = "Cool"
                },
                new WeatherForecastEntity
                {
                    Id = 4,
                    Date = DateTime.Parse("2021-03-16T05:00:00.0000-04:00"),
                    TemperatureC = 18,
                    Summary = "Hot"
                },
                new WeatherForecastEntity
                {
                    Id = 5,
                    Date = DateTime.Parse("2021-03-17T05:00:00.0000-04:00"),
                    TemperatureC = 50,
                    Summary = "Mild"
                });
        }
    }
}
