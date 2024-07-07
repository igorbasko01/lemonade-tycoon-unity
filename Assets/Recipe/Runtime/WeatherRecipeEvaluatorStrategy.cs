using System.Collections.Generic;
using baskorp.Calendars.Runtime;
using baskorp.Weather.Runtime;

namespace baskorp.Recipes.Runtime
{
    public class WeatherRecipeEvaluatorStrategy : IRecipeEvaluatorStrategy
    {

        private IWeatherForecaster forecaster;
        private readonly Dictionary<(SkyType, TemperatureType), float> evaluationMap; 

        public WeatherRecipeEvaluatorStrategy(IWeatherForecaster forecaster)
        {
            this.forecaster = forecaster;
            evaluationMap = new Dictionary<(SkyType, TemperatureType), float>
            {
                {(SkyType.Clear, TemperatureType.Hot), 1f},
                {(SkyType.Clear, TemperatureType.Mild), 0.8f},
                {(SkyType.Clear, TemperatureType.Cold), 0.5f},
                {(SkyType.Rainy, TemperatureType.Hot), 0.7f},
                {(SkyType.Rainy, TemperatureType.Mild), 0.4f},
                {(SkyType.Rainy, TemperatureType.Cold), 0.2f}
            };
        }
        public float Evaluate(Recipe recipe, Date date)
        {
            var forecast = forecaster.GetForecast(date);
            var key = (forecast.SkyType, forecast.TemperatureType);
            return evaluationMap.TryGetValue(key, out float value) ? value : 0.0f;
        }
    }
}