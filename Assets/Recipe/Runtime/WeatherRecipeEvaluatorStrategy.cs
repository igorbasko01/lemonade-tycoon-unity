using baskorp.Calendars.Runtime;
using baskorp.Weather.Runtime;

namespace baskorp.Recipes.Runtime
{
    public class WeatherRecipeEvaluatorStrategy : IRecipeEvaluatorStrategy
    {

        private IWeatherForecaster forecaster;

        public WeatherRecipeEvaluatorStrategy(IWeatherForecaster forecaster)
        {
            this.forecaster = forecaster;
        }
        public float Evaluate(Recipe recipe, Date date)
        {
            var forecast = forecaster.GetForecast(date);
            if (forecast.SkyType == SkyType.Clear && forecast.TemperatureType == TemperatureType.Hot)
            {
                return 1f;
            }
            else if (forecast.SkyType == SkyType.Clear && forecast.TemperatureType == TemperatureType.Mild)
            {
                return 0.8f;
            }
            else if (forecast.SkyType == SkyType.Clear && forecast.TemperatureType == TemperatureType.Cold)
            {
                return 0.5f;
            }
            else if (forecast.SkyType == SkyType.Rainy && forecast.TemperatureType == TemperatureType.Hot)
            {
                return 0.7f;
            }
            else if (forecast.SkyType == SkyType.Rainy && forecast.TemperatureType == TemperatureType.Mild) 
            { 
                return 0.4f;
            }
            else if (forecast.SkyType == SkyType.Rainy && forecast.TemperatureType == TemperatureType.Cold)
            {
                return 0.2f;
            }
            else
            {
                return 0f;
            }
        }
    }
}