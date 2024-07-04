using baskorp.Calendars.Runtime;

namespace baskorp.Weather.Runtime {
    public interface IWeatherForecaster {
        WeatherForecast GetForecast(Date date);
    }
}