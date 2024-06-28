using NUnit.Framework;
using System.Collections.Generic;
using baskorp.IngredientsBuyers.Runtime;
using baskorp.IngredientsInventory.Runtime;
using baskorp.IngredientsCatalog.Runtime;
using baskorp.Wallets.Runtime;

namespace baskorp.IngredientsBuyers.Tests
{
    [TestFixture]
    public class IngredientsBuyerTests
    {

        private List<SellableIngredient> _catalogIngredients = new()
        {
            SellableIngredient.Create(IngredientMetadata.Create("Lemon"), 10f),
            SellableIngredient.Create(IngredientMetadata.Create("Sugar"), 5f),
            SellableIngredient.Create(IngredientMetadata.Create("Ice"), 3f)
        };

        [Test]
        public void IngredientsBuyer_Buy()
        {
            var buyer = new IngredientsBuyer();
            var inventory = new IngredientsInventoryManager();
            var catalog = new IngredientsCatalogManager(_catalogIngredients);
            var wallet = new Wallet(100);
            var ingredientToBuy = QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 5f);

            var result = buyer.Buy(ingredientToBuy, inventory, catalog, wallet);
            Assert.AreEqual(PurchaseResultType.Success, result.ResultType);
            Assert.AreEqual(1, inventory.Ingredients.Count);
            Assert.AreEqual(5f, inventory.Ingredients[0].Quantity);
            Assert.AreEqual(50f, wallet.Balance);
            Assert.AreEqual("Lemon", inventory.Ingredients[0].Metadata.Name);
        }

        [Test]
        public void IngredientsBuyer_Buy_NotEnoughMoney()
        {
            var buyer = new IngredientsBuyer();
            var inventory = new IngredientsInventoryManager();
            var catalog = new IngredientsCatalogManager(_catalogIngredients);
            var wallet = new Wallet(1);
            var ingredientToBuy = QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 5f);

            var result = buyer.Buy(ingredientToBuy, inventory, catalog, wallet);
            Assert.AreEqual(PurchaseResultType.NotEnoughMoney, result.ResultType);
            Assert.AreEqual(0, inventory.Ingredients.Count);
            Assert.AreEqual(1f, wallet.Balance);
        }

        [Test]
        public void IngredientsBuyer_Buy_IngredientNotFound()
        {
            var buyer = new IngredientsBuyer();
            var inventory = new IngredientsInventoryManager();
            var catalog = new IngredientsCatalogManager(_catalogIngredients);
            var wallet = new Wallet(100);
            var ingredientToBuy = QuantifiableIngredient.Create(IngredientMetadata.Create("New Ingredient"), 5f);

            var result = buyer.Buy(ingredientToBuy, inventory, catalog, wallet);
            Assert.AreEqual(PurchaseResultType.IngredientNotFound, result.ResultType);
            Assert.AreEqual(0, inventory.Ingredients.Count);
            Assert.AreEqual(100f, wallet.Balance);
        }
    }
}