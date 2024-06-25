using System;
using System.Collections.Generic;
using baskorp.Calendars.Runtime;
using UnityEngine;

namespace baskorp.Weather.Runtime {
    public class WeatherForecaster {
        private Dictionary<Season, Dictionary<SkyType, double>> skyTypeProbabilities = new() {
            {Season.Spring, new Dictionary<SkyType, double> {{SkyType.Clear, 0.7}, {SkyType.Rainy, 0.3}}},
            {Season.Summer, new Dictionary<SkyType, double> {{SkyType.Clear, 0.9}, {SkyType.Rainy, 0.1}}},
            {Season.Autumn, new Dictionary<SkyType, double> {{SkyType.Clear, 0.6}, {SkyType.Rainy, 0.4}}},
            {Season.Winter, new Dictionary<SkyType, double> {{SkyType.Clear, 0.3}, {SkyType.Rainy, 0.7}}}
        };

        private Dictionary<Season, Dictionary<TemperatureType, double>> temperatureTypeProbabilities = new() {
            {Season.Spring, new Dictionary<TemperatureType, double> {{TemperatureType.Hot, 0.1}, {TemperatureType.Cold, 0.1}, {TemperatureType.Mild, 0.8}}},
            {Season.Summer, new Dictionary<TemperatureType, double> {{TemperatureType.Hot, 0.8}, {TemperatureType.Cold, 0.1}, {TemperatureType.Mild, 0.1}}},
            {Season.Autumn, new Dictionary<TemperatureType, double> {{TemperatureType.Hot, 0.1}, {TemperatureType.Cold, 0.8}, {TemperatureType.Mild, 0.1}}},
            {Season.Winter, new Dictionary<TemperatureType, double> {{TemperatureType.Hot, 0.1}, {TemperatureType.Cold, 0.8}, {TemperatureType.Mild, 0.1}}}
        };

        public WeatherForecast GetForecast(Date date) {
            var random = new System.Random(date.GetHashCode());
            var skyTypes = Enum.GetValues(typeof(SkyType));
            var skyTypeProbabilitiesForSeason = skyTypeProbabilities[date.Season];
            var randomSkyType = GetRandomWeatherType(random, skyTypes, skyTypeProbabilitiesForSeason);
            var temperatureTypeProbabilitiesForSeason = temperatureTypeProbabilities[date.Season];
            var temperatureTypes = Enum.GetValues(typeof(TemperatureType));
            var randomTemperatureType = GetRandomWeatherType(random, temperatureTypes, temperatureTypeProbabilitiesForSeason);
            return new WeatherForecast(randomSkyType, randomTemperatureType);
        }

        private T GetRandomWeatherType<T>(System.Random random, Array weatherTypes, Dictionary<T, double> probabilities) {
            var randomValue = random.NextDouble();
            var cumulativeProbability = 0.0;
            foreach (var weatherType in weatherTypes) {
                cumulativeProbability += probabilities[(T) weatherType];
                if (randomValue < cumulativeProbability) {
                    return (T) weatherType;
                }
            }
            return (T) weatherTypes.GetValue(weatherTypes.Length - 1);
        }
    }
}