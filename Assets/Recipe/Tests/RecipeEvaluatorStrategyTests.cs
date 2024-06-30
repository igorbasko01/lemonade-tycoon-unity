using System.Collections.Generic;
using baskorp.Calendars.Runtime;
using baskorp.IngredientsCatalog.Runtime;
using baskorp.Recipes.Runtime;
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
    }
}