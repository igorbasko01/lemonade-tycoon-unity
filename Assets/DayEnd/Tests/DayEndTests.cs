using NUnit.Framework;
using baskorp.DayEnd.Runtime;
using baskorp.Weather.Runtime;
using baskorp.IngredientsCatalog.Runtime;
using System.Collections.Generic;
using baskorp.Recipes.Runtime;
using baskorp.Calendars.Runtime;
using baskorp.IngredientsInventory.Runtime;
using UnityEditorInternal.VersionControl;
using Moq;

namespace baskorp.DayEnd.Tests
{
    [TestFixture]
    public class DayEndSystemTests
    {
        [Test]
        public void DayEndSystem_CalculateDemand_ReturnsCorrectValue()
        {
            var mockDayStrategy = new Mock<IRecipeEvaluatorStrategy>();
            mockDayStrategy.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.5f);
            var mockWeatherStrategy = new Mock<IRecipeEvaluatorStrategy>();
            mockWeatherStrategy.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(1f);
            var recipeEvaluator = new RecipeEvaluator(new List<(float, IRecipeEvaluatorStrategy)> {
                (0.5f, mockDayStrategy.Object),
                (0.5f, mockWeatherStrategy.Object)
            });
            var ingredientsCatalog = new IngredientsCatalogManager(new List<SellableIngredient> {
                SellableIngredient.Create(IngredientMetadata.Create("Lemon"), 1.5f),
                SellableIngredient.Create(IngredientMetadata.Create("Sugar"), 0.5f)
            });
            var recipe = Recipe.Create("Lemonade", new List<QuantifiableIngredient> {
                QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 1),
                QuantifiableIngredient.Create(IngredientMetadata.Create("Sugar"), 0.5f)
            });
            var ingredientsInventory = new IngredientsInventoryManager();
            ingredientsInventory.AddIngredient(QuantifiableIngredient.Create(IngredientMetadata.Create("Lemonade"), 3));
            var date = new Date(1, 1, 2024);
            var recipePrice = 2.5f;
            var dayEndSystem = new DayEndSystem(recipeEvaluator, ingredientsCatalog, ingredientsInventory, maxOrders: 1000);
            var demand = dayEndSystem.CalculateDemand(recipe, date, recipePrice);
            var expectedBasePrice = (1.5f * 1f) + (0.5f * 0.5f);  // lemon_price * lemon_quantity + sugar_price * sugar_quantity
            var expectedDeltaPricePercent = (recipePrice - expectedBasePrice) / expectedBasePrice;
            var expectedRecipeEvaluatorResult = (0.5f * 0.5f) + (1f * 0.5f);  // weekday * weight + clear_hot * weight
            var expectedDemand = (int)((expectedRecipeEvaluatorResult - expectedDeltaPricePercent) * 1000);
            Assert.AreEqual(expectedDemand, demand);
        }
    }
}