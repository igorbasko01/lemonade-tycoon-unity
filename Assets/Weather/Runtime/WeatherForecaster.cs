using System;
using baskorp.Calendars.Runtime;

namespace baskorp.Weather.Runtime {
    public class WeatherForecaster {
        public WeatherForecast GetForecast(Date date) {
            var random = new Random(date.GetHashCode());
            var skyTypes = Enum.GetValues(typeof(SkyType));
            var randomSkyType = (SkyType) skyTypes.GetValue(random.Next(skyTypes.Length));
            var temperatureTypes = Enum.GetValues(typeof(TemperatureType));
            var randomTemperatureType = (TemperatureType) temperatureTypes.GetValue(random.Next(temperatureTypes.Length));
            return new WeatherForecast(randomSkyType, randomTemperatureType);
        }
    }
}