using NUnit.Framework;
using baskorp.Weather.Runtime;
using baskorp.Calendars.Runtime;
using UnityEngine;

namespace baskorp.Weather.Tests {
    [TestFixture]
    public class WeatherForecasterTests {
        [Test]
        public void WeatherForecastForSameDateReturnsSameForecast() {
            var forecaster = new WeatherForecaster();
            var forecast1 = forecaster.GetForecast(new Date(1, 1, 2020));
            var forecast2 = forecaster.GetForecast(new Date(1, 1, 2020));
            Assert.AreEqual(forecast1, forecast2);
        }
        [Test]
        public void WeatherForecastForDifferentDateReturnsDifferentForecast() {
            var forecaster = new WeatherForecaster();
            var forecast1 = forecaster.GetForecast(new Date(1, 1, 2020));
            var forecast2 = forecaster.GetForecast(new Date(2, 1, 2020));
            Assert.AreNotEqual(forecast1, forecast2);
        }
    }
}