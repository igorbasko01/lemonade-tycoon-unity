using baskorp.Calendars.Runtime;
using baskorp.Weather.Runtime;

namespace baskorp.Recipes.Runtime {
    public class WeatherRecipeEvaluatorStrategy : IRecipeEvaluatorStrategy {

        private IWeatherForecaster forecaster;

        public WeatherRecipeEvaluatorStrategy(IWeatherForecaster forecaster) {
            this.forecaster = forecaster;
        }
        public float Evaluate(Recipe recipe, Date date) {
            var forecast = forecaster.GetForecast(date);
            if (forecast.SkyType == SkyType.Clear && forecast.TemperatureType == TemperatureType.Hot) {
                return 1f;
            }
            return 0f;
        }
    }
}