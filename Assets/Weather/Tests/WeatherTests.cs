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
        [Test]
        public void WeatherForecastForDifferentYearReturnsDifferentForecast() {
            var forecaster = new WeatherForecaster();
            var forecast1 = forecaster.GetForecast(new Date(1, 1, 2020));
            var forecast2 = forecaster.GetForecast(new Date(1, 1, 2021));
            Assert.AreNotEqual(forecast1, forecast2);
        }
        [Test]
        public void WeatherForecastForDifferentMonthReturnsDifferentForecast() {
            var forecaster = new WeatherForecaster();
            var forecast1 = forecaster.GetForecast(new Date(1, 1, 2020));
            var forecast2 = forecaster.GetForecast(new Date(1, 2, 2020));
            Assert.AreNotEqual(forecast1, forecast2);
        }
        [Test]
        public void WeatherForecastSummerClearDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int clearDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var summer = new Date(1, 6, i);
                var forecast = forecaster.GetForecast(summer);
                if (forecast.SkyType == SkyType.Clear) {
                    clearDays++;
                }
            }
            float clearDayProbability = (float) clearDays / numOfIterations;
            Assert.That(clearDayProbability, Is.InRange(0.89f, 0.91f));
        }
        [Test]
        public void WeatherForecastSpringClearDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int clearDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var spring = new Date(1, 3, i);
                var forecast = forecaster.GetForecast(spring);
                if (forecast.SkyType == SkyType.Clear) {
                    clearDays++;
                }
            }
            float clearDayProbability = (float) clearDays / numOfIterations;
            Assert.That(clearDayProbability, Is.InRange(0.69f, 0.71f));
        }
        [Test]
        public void WeatherForecastAutumnClearDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int clearDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var autumn = new Date(1, 9, i);
                var forecast = forecaster.GetForecast(autumn);
                if (forecast.SkyType == SkyType.Clear) {
                    clearDays++;
                }
            }
            float clearDayProbability = (float) clearDays / numOfIterations;
            Assert.That(clearDayProbability, Is.InRange(0.59f, 0.61f));
        }
        [Test]
        public void WeatherForecastWinterClearDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int clearDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var winter = new Date(1, 12, i);
                var forecast = forecaster.GetForecast(winter);
                if (forecast.SkyType == SkyType.Clear) {
                    clearDays++;
                }
            }
            float clearDayProbability = (float) clearDays / numOfIterations;
            Assert.That(clearDayProbability, Is.InRange(0.29f, 0.31f));
        }
        [Test]
        public void WeatherForecastSummerRainyDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int rainyDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var summer = new Date(1, 6, i);
                var forecast = forecaster.GetForecast(summer);
                if (forecast.SkyType == SkyType.Rainy) {
                    rainyDays++;
                }
            }
            float rainyDayProbability = (float) rainyDays / numOfIterations;
            Assert.That(rainyDayProbability, Is.InRange(0.09f, 0.11f));
        }
        [Test]
        public void WeatherForecastSpringRainyDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int rainyDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var spring = new Date(1, 3, i);
                var forecast = forecaster.GetForecast(spring);
                if (forecast.SkyType == SkyType.Rainy) {
                    rainyDays++;
                }
            }
            float rainyDayProbability = (float) rainyDays / numOfIterations;
            Assert.That(rainyDayProbability, Is.InRange(0.29f, 0.31f));
        }
        [Test]
        public void WeatherForecastAutumnRainyDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int rainyDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var autumn = new Date(1, 9, i);
                var forecast = forecaster.GetForecast(autumn);
                if (forecast.SkyType == SkyType.Rainy) {
                    rainyDays++;
                }
            }
            float rainyDayProbability = (float) rainyDays / numOfIterations;
            Assert.That(rainyDayProbability, Is.InRange(0.39f, 0.41f));
        }
        [Test]
        public void WeatherForecastWinterRainyDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int rainyDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var winter = new Date(1, 12, i);
                var forecast = forecaster.GetForecast(winter);
                if (forecast.SkyType == SkyType.Rainy) {
                    rainyDays++;
                }
            }
            float rainyDayProbability = (float) rainyDays / numOfIterations;
            Assert.That(rainyDayProbability, Is.InRange(0.69f, 0.71f));
        }
        [Test]
        public void WeatherForecastSummerHotDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int hotDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var summer = new Date(1, 6, i);
                var forecast = forecaster.GetForecast(summer);
                if (forecast.TemperatureType == TemperatureType.Hot) {
                    hotDays++;
                }
            }
            float hotDayProbability = (float) hotDays / numOfIterations;
            Assert.That(hotDayProbability, Is.InRange(0.79f, 0.81f));
        }
        [Test]
        public void WeatherForecastSpringHotDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int hotDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var spring = new Date(1, 3, i);
                var forecast = forecaster.GetForecast(spring);
                if (forecast.TemperatureType == TemperatureType.Hot) {
                    hotDays++;
                }
            }
            float hotDayProbability = (float) hotDays / numOfIterations;
            Assert.That(hotDayProbability, Is.InRange(0.09f, 0.11f));
        }
        [Test]
        public void WeatherForecastAutumnHotDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int hotDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var autumn = new Date(1, 9, i);
                var forecast = forecaster.GetForecast(autumn);
                if (forecast.TemperatureType == TemperatureType.Hot) {
                    hotDays++;
                }
            }
            float hotDayProbability = (float) hotDays / numOfIterations;
            Assert.That(hotDayProbability, Is.InRange(0.09f, 0.11f));
        }
        [Test]
        public void WeatherForecastWinterHotDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int hotDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var winter = new Date(1, 12, i);
                var forecast = forecaster.GetForecast(winter);
                if (forecast.TemperatureType == TemperatureType.Hot) {
                    hotDays++;
                }
            }
            float hotDayProbability = (float) hotDays / numOfIterations;
            Assert.That(hotDayProbability, Is.InRange(0.09f, 0.11f));
        }
        [Test]
        public void WeatherForecastSummerColdDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int coldDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var summer = new Date(1, 6, i);
                var forecast = forecaster.GetForecast(summer);
                if (forecast.TemperatureType == TemperatureType.Cold) {
                    coldDays++;
                }
            }
            float coldDayProbability = (float) coldDays / numOfIterations;
            Assert.That(coldDayProbability, Is.InRange(0.09f, 0.11f));
        }
        [Test]
        public void WeatherForecastSpringColdDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int coldDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var spring = new Date(1, 3, i);
                var forecast = forecaster.GetForecast(spring);
                if (forecast.TemperatureType == TemperatureType.Cold) {
                    coldDays++;
                }
            }
            float coldDayProbability = (float) coldDays / numOfIterations;
            Assert.That(coldDayProbability, Is.InRange(0.09f, 0.11f));
        }
        [Test]
        public void WeatherForecastAutumnColdDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int coldDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var autumn = new Date(1, 9, i);
                var forecast = forecaster.GetForecast(autumn);
                if (forecast.TemperatureType == TemperatureType.Cold) {
                    coldDays++;
                }
            }
            float coldDayProbability = (float) coldDays / numOfIterations;
            Assert.That(coldDayProbability, Is.InRange(0.79f, 0.81f));
        }
        [Test]
        public void WeatherForecastWinterColdDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int coldDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var winter = new Date(1, 12, i);
                var forecast = forecaster.GetForecast(winter);
                if (forecast.TemperatureType == TemperatureType.Cold) {
                    coldDays++;
                }
            }
            float coldDayProbability = (float) coldDays / numOfIterations;
            Assert.That(coldDayProbability, Is.InRange(0.79f, 0.81f));
        }
        [Test]
        public void WeatherForecastSummerMildDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int mildDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var summer = new Date(1, 6, i);
                var forecast = forecaster.GetForecast(summer);
                if (forecast.TemperatureType == TemperatureType.Mild) {
                    mildDays++;
                }
            }
            float mildDayProbability = (float) mildDays / numOfIterations;
            Assert.That(mildDayProbability, Is.InRange(0.09f, 0.11f));
        }
        [Test]
        public void WeatherForecastSpringMildDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int mildDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var spring = new Date(1, 3, i);
                var forecast = forecaster.GetForecast(spring);
                if (forecast.TemperatureType == TemperatureType.Mild) {
                    mildDays++;
                }
            }
            float mildDayProbability = (float) mildDays / numOfIterations;
            Assert.That(mildDayProbability, Is.InRange(0.79f, 0.81f));
        }
        [Test]
        public void WeatherForecastAutumnMildDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int mildDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var autumn = new Date(1, 9, i);
                var forecast = forecaster.GetForecast(autumn);
                if (forecast.TemperatureType == TemperatureType.Mild) {
                    mildDays++;
                }
            }
            float mildDayProbability = (float) mildDays / numOfIterations;
            Assert.That(mildDayProbability, Is.InRange(0.09f, 0.11f));
        }
        [Test]
        public void WeatherForecastWinterMildDayProbability() {
            int numOfIterations = 1000;
            var forecaster = new WeatherForecaster();
            int mildDays = 0;
            for (int i = 0; i < numOfIterations; i++) {
                var winter = new Date(1, 12, i);
                var forecast = forecaster.GetForecast(winter);
                if (forecast.TemperatureType == TemperatureType.Mild) {
                    mildDays++;
                }
            }
            float mildDayProbability = (float) mildDays / numOfIterations;
            Assert.That(mildDayProbability, Is.InRange(0.09f, 0.11f));
        }
    }
}