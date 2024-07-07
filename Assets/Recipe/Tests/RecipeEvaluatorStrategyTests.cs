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

        [Test]
        public void RecipeEvaluatorAcceptsTwoStrategies50pctWeightsEach() {
            var strategy1 = new Mock<IRecipeEvaluatorStrategy>();
            strategy1.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.8f);
            var strategy2 = new Mock<IRecipeEvaluatorStrategy>();
            strategy2.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.4f);
            var evaluator = new RecipeEvaluator(new List<(float, IRecipeEvaluatorStrategy)> { (0.5f, strategy1.Object), (0.5f, strategy2.Object) });
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = evaluator.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0.6f, result);
        }

        [Test]
        public void RecipeEvaluatorAcceptsTwoStrategiesWithDifferentWeights() {
            var strategy1 = new Mock<IRecipeEvaluatorStrategy>();
            strategy1.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.8f);
            var strategy2 = new Mock<IRecipeEvaluatorStrategy>();
            strategy2.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.4f);
            var evaluator = new RecipeEvaluator(new List<(float, IRecipeEvaluatorStrategy)> { (0.7f, strategy1.Object), (0.3f, strategy2.Object) });
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = evaluator.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0.68f, result);
        }

        [Test]
        public void RecipeEvaluatorAcceptsStrategiesWithCombinedWeightsLowerThan100pct() {
            var strategy1 = new Mock<IRecipeEvaluatorStrategy>();
            strategy1.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.8f);
            var strategy2 = new Mock<IRecipeEvaluatorStrategy>();
            strategy2.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.4f);
            var evaluator = new RecipeEvaluator(new List<(float, IRecipeEvaluatorStrategy)> { (0.7f, strategy1.Object), (0.2f, strategy2.Object) });
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = evaluator.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0.711111128f, result);
        }

        [Test]
        public void RecipeEvaluatorAcceptsStrategiesWithCombinedWeightsOver100pct() {
            var strategy1 = new Mock<IRecipeEvaluatorStrategy>();
            strategy1.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.8f);
            var strategy2 = new Mock<IRecipeEvaluatorStrategy>();
            strategy2.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.4f);
            var evaluator = new RecipeEvaluator(new List<(float, IRecipeEvaluatorStrategy)> { (0.7f, strategy1.Object), (0.4f, strategy2.Object) });
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = evaluator.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0.654545426f, result);
        }

        [Test]
        public void RecipeEvaluatorAcceptsZeroStrategies_ReturnsZero() {
            var evaluator = new RecipeEvaluator(new List<(float, IRecipeEvaluatorStrategy)>());
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = evaluator.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0f, result);
        }

        [Test]
        public void RecipeEvaluatorAcceptsOneStrategy_ReturnsItsResult() {
            var strategy = new Mock<IRecipeEvaluatorStrategy>();
            strategy.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.8f);
            var evaluator = new RecipeEvaluator(new List<(float, IRecipeEvaluatorStrategy)> { (1f, strategy.Object) });
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = evaluator.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0.8f, result);
        }

        [Test]
        public void RecipeEvaluatorAcceptsMultipleStrategies_IgnoresZeroWeightStrategies() {
            var strategy1 = new Mock<IRecipeEvaluatorStrategy>();
            strategy1.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.8f);
            var strategy2 = new Mock<IRecipeEvaluatorStrategy>();
            strategy2.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.4f);
            var evaluator = new RecipeEvaluator(new List<(float, IRecipeEvaluatorStrategy)> { (0f, strategy1.Object), (1f, strategy2.Object) });
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = evaluator.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0.4f, result);
        }

        [Test]
        public void RecipeEvaluatorAcceptsOneStrategyWithZeroWeight_ReturnsZero() {
            var strategy = new Mock<IRecipeEvaluatorStrategy>();
            strategy.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.8f);
            var evaluator = new RecipeEvaluator(new List<(float, IRecipeEvaluatorStrategy)> { (0f, strategy.Object) });
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = evaluator.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0f, result);
        }

        [Test]
        public void RecipeEvaluatorAcceptsMultipleStrategies_IgnoresNegativeWeights() {
            var strategy1 = new Mock<IRecipeEvaluatorStrategy>();
            strategy1.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.8f);
            var strategy2 = new Mock<IRecipeEvaluatorStrategy>();
            strategy2.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.4f);
            var evaluator = new RecipeEvaluator(new List<(float, IRecipeEvaluatorStrategy)> { (-0.1f, strategy1.Object), (1f, strategy2.Object) });
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = evaluator.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0.4f, result);
        }

        [Test]
        public void RecipeEvaluatorAcceptsOneStrategyWithNegativeWeight_ReturnsZero() {
            var strategy = new Mock<IRecipeEvaluatorStrategy>();
            strategy.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.8f);
            var evaluator = new RecipeEvaluator(new List<(float, IRecipeEvaluatorStrategy)> { (-0.1f, strategy.Object) });
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = evaluator.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0f, result);
        }

        [Test]
        public void RecipeEvaluatorAcceptsMultipleStrategies_IgnoresNegativeWeightsAndZeroWeights() {
            var strategy1 = new Mock<IRecipeEvaluatorStrategy>();
            strategy1.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.8f);
            var strategy2 = new Mock<IRecipeEvaluatorStrategy>();
            strategy2.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.4f);
            var evaluator = new RecipeEvaluator(new List<(float, IRecipeEvaluatorStrategy)> { (-0.1f, strategy1.Object), (0f, strategy2.Object) });
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient>() { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1) });
            var result = evaluator.Evaluate(recipe, new Calendar(1, 3, 2021).CurrentDate);
            Assert.AreEqual(0f, result);
        }
    }
}