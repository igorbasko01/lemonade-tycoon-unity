using NUnit.Framework;
using baskorp.DayEnd.Runtime;
using baskorp.IngredientsCatalog.Runtime;
using System.Collections.Generic;
using baskorp.Recipes.Runtime;
using baskorp.Calendars.Runtime;
using baskorp.IngredientsInventory.Runtime;
using Moq;
using baskorp.Wallets.Runtime;

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

        [Test]
        public void DayEndSystem_CalculateDemand_ReturnsZeroWhenIngredientNotInCatalog() 
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
                QuantifiableIngredient.Create(IngredientMetadata.Create("Sugar"), 0.5f),
                QuantifiableIngredient.Create(IngredientMetadata.Create("Mint"), 0.5f)  // Mint is not in the catalog
            });
            var ingredientsInventory = new IngredientsInventoryManager();
            ingredientsInventory.AddIngredient(QuantifiableIngredient.Create(IngredientMetadata.Create("Lemonade"), 3));
            var date = new Date(1, 1, 2024);
            var recipePrice = 2.5f;
            var dayEndSystem = new DayEndSystem(recipeEvaluator, ingredientsCatalog, ingredientsInventory, maxOrders: 1000);
            var demand = dayEndSystem.CalculateDemand(recipe, date, recipePrice);
            Assert.AreEqual(0, demand);
        }

        [Test]
        public void DayEndSystem_CalculateDemand_ReturnCorrectValueWhenRecipePriceLowerThanBase()
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
            var expectedBasePrice = (1.5f * 1f) + (0.5f * 0.5f);  // lemon_price * lemon_quantity + sugar_price * sugar_quantity
            var recipePrice = expectedBasePrice - 0.2f;
            var dayEndSystem = new DayEndSystem(recipeEvaluator, ingredientsCatalog, ingredientsInventory, maxOrders: 1000);
            var demand = dayEndSystem.CalculateDemand(recipe, date, recipePrice);
            var expectedDeltaPricePercent = (recipePrice - expectedBasePrice) / expectedBasePrice;
            var expectedRecipeEvaluatorResult = (0.5f * 0.5f) + (1f * 0.5f);  // weekday * weight + clear_hot * weight
            var expectedDemand = (int)((expectedRecipeEvaluatorResult - expectedDeltaPricePercent) * 1000);
            Assert.AreEqual(expectedDemand, demand);
        }

        [Test]
        public void DayEndSystem_EndDay_UpdatesStateCorrectlyWhenDemandHigherThanInventory() 
        {
            var wallet = new Wallet(20);
            var mockDayEvaluatorStrategy = new Mock<IRecipeEvaluatorStrategy>();
            mockDayEvaluatorStrategy.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(0.5f);
            var mockWeatherEvaluatorStrategy = new Mock<IRecipeEvaluatorStrategy>();
            mockWeatherEvaluatorStrategy.Setup(s => s.Evaluate(It.IsAny<Recipe>(), It.IsAny<Date>())).Returns(1f);
            var recipeEvaluator = new RecipeEvaluator(new List<(float, IRecipeEvaluatorStrategy)> {
                (0.5f, mockDayEvaluatorStrategy.Object),
                (0.5f, mockWeatherEvaluatorStrategy.Object)
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
            dayEndSystem.EndDay(recipe, date, recipePrice, wallet);
            Assert.AreEqual(20 + (2.5f * 3), wallet.Balance);
            Assert.AreEqual(0, ingredientsInventory.GetIngredientQuantity(IngredientMetadata.Create("Lemonade")));
        }

        [Test, Ignore("This test is not implemented yet")]
        public void DayEndSystem_EndDay_UpdatesStateCorrectlyWhenInventoryHigherThanDemand()
        {

        }

        [Test, Ignore("This test is not implemented yet")]
        public void DayEndSystem_EndDay_DoesNothingWhenDemandIsZero()
        {

        }

        [Test, Ignore("This test is not implemented yet")]
        public void DayEndSystem_EndDay_DoesNothingWhenIngredientNotInInventory()
        {

        }

        [Test, Ignore("This test is not implemented yet")]
        public void DayEndSystem_EndDay_DoesNothingWhenIngredientNotInCatalog()
        {

        }
    }
}