using System.Collections.Generic;
using baskorp.Calendars.Runtime;
using baskorp.IngredientsCatalog.Runtime;
using baskorp.Recipes.Runtime;
using baskorp.Weather.Runtime;
using Moq;
using NUnit.Framework;

namespace baskorp.Recipes.Tests
{
    [TestFixture]
    public class RecipeEvaluatorStrategyTests
    {
        [Test]
        public void DayTypeRecipeEvaluatorStrategy_Returns50pctOnWeekdays()
        {
            var strategy = new DayTypeRecipeEvaluatorStrategy();
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = strategy.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0.5f, result);
        }

        [Test]
        public void DayTypeRecipeEvaluatorStrategy_Returns100pctOnWeekends()
        {
            var strategy = new DayTypeRecipeEvaluatorStrategy();
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = strategy.Evaluate(recipe, new Calendar(6, 3, 2021).CurrentDate);
            Assert.AreEqual(1f, result);
        }

        [Test]
        public void WeatherRecipeEvaluatorStrategy_Returns100pctOnClearAndHot() {
            var mockForecaster = new Mock<IWeatherForecaster>();
            mockForecaster.Setup(f => f.GetForecast(It.IsAny<Date>())).Returns(new WeatherForecast(SkyType.Clear, TemperatureType.Hot));
            var strategy = new WeatherRecipeEvaluatorStrategy(mockForecaster.Object);
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = strategy.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(1f, result);
        }

        [Test]
        public void WeatherRecipeEvaluatorStrategy_Returns80pctOnClearAndNormal() {
            var mockForecaster = new Mock<IWeatherForecaster>();
            mockForecaster.Setup(f => f.GetForecast(It.IsAny<Date>())).Returns(new WeatherForecast(SkyType.Clear, TemperatureType.Mild));
            var strategy = new WeatherRecipeEvaluatorStrategy(mockForecaster.Object);
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = strategy.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0.8f, result);
        }

        [Test]
        public void WeatherRecipeEvaluatorStrategy_Returns50pctOnClearAndCold() {
            var mockForecaster = new Mock<IWeatherForecaster>();
            mockForecaster.Setup(f => f.GetForecast(It.IsAny<Date>())).Returns(new WeatherForecast(SkyType.Clear, TemperatureType.Cold));
            var strategy = new WeatherRecipeEvaluatorStrategy(mockForecaster.Object);
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = strategy.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0.5f, result);
        }

        [Test]
        public void WeatherRecipeEvaluatorStrategy_Returns70pctOnRainyAndHot() {
            var mockForecaster = new Mock<IWeatherForecaster>();
            mockForecaster.Setup(f => f.GetForecast(It.IsAny<Date>())).Returns(new WeatherForecast(SkyType.Rainy, TemperatureType.Hot));
            var strategy = new WeatherRecipeEvaluatorStrategy(mockForecaster.Object);
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = strategy.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0.7f, result);
        }

        [Test]
        public void WeatherRecipeEvaluatorStrategy_Returns40pctOnRainyAndMild() {
            var mockForecaster = new Mock<IWeatherForecaster>();
            mockForecaster.Setup(f => f.GetForecast(It.IsAny<Date>())).Returns(new WeatherForecast(SkyType.Rainy, TemperatureType.Mild));
            var strategy = new WeatherRecipeEvaluatorStrategy(mockForecaster.Object);
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = strategy.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0.4f, result);
        }

        [Test]
        public void WeatherRecipeEvaluatorStrategy_Returns20pctRainyAndCold() {
            var mockForecaster = new Mock<IWeatherForecaster>();
            mockForecaster.Setup(f => f.GetForecast(It.IsAny<Date>())).Returns(new WeatherForecast(SkyType.Rainy, TemperatureType.Cold));
            var strategy = new WeatherRecipeEvaluatorStrategy(mockForecaster.Object);
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = strategy.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0.2f, result);
        }
    }
}