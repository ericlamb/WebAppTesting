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
        }
    }
}
