using System.Collections.Generic;
using NUnit.Framework;
using baskorp.IngredientsCatalog.Runtime;

namespace baskorp.IngredientsCatalog.Tests
{
    [TestFixture]
    public class IngredientsCatalogTests
    {
        private IngredientsCatalogManager _ingredientsCatalogManager;
        private float _playerMoney;
        private IngredientMetadata lemon;
        private IngredientMetadata sugar;
        private SellableIngredient lemonSellable;
        private SellableIngredient sugarSellable;

        [SetUp]
        public void SetUp()
        {
            lemon = IngredientMetadata.Create("Lemon");
            sugar = IngredientMetadata.Create("Sugar");
            lemonSellable = SellableIngredient.Create(lemon, 10f);
            sugarSellable = SellableIngredient.Create(sugar, 5f);
            var ingredientsDatabase = new List<SellableIngredient> { lemonSellable, sugarSellable };
            _ingredientsCatalogManager = new IngredientsCatalogManager(ingredientsDatabase);
            _playerMoney = 100f;
        }

        [Test]
        public void PurchaseIngredient_Success()
        {
            var lemonQuantifiable = QuantifiableIngredient.Create(lemon, 10f);
            PurchaseResult purchaseResult = _ingredientsCatalogManager.PurchaseIngredient(lemonQuantifiable, _playerMoney);
            Assert.AreEqual(PurchaseResultType.Success, purchaseResult.ResultType);
            Assert.AreEqual(10f, purchaseResult.PurchasedIngredient.Quantity);
        }

        [Test]
        public void PurchaseIngredient_NotEnoughMoney()
        {
            var lemonQuantifiable = QuantifiableIngredient.Create(lemon, 11f);
            PurchaseResult purchaseResult = _ingredientsCatalogManager.PurchaseIngredient(lemonQuantifiable, _playerMoney);
            Assert.AreEqual(PurchaseResultType.NotEnoughMoney, purchaseResult.ResultType);
            Assert.IsNull(purchaseResult.PurchasedIngredient);
        }

        [Test]
        public void PurchaseIngredient_IngredientNotFound()
        {
            var newIngredientMetadata = IngredientMetadata.Create("New Ingredient");
            var newIngredientQuantifiable = QuantifiableIngredient.Create(newIngredientMetadata, 15f);
            PurchaseResult purchaseResult = _ingredientsCatalogManager.PurchaseIngredient(newIngredientQuantifiable, _playerMoney);
            Assert.AreEqual(PurchaseResultType.IngredientNotFound, purchaseResult.ResultType);
            Assert.IsNull(purchaseResult.PurchasedIngredient);
        }

        [Test]
        public void PurchaseIngredient_MultiplePurchases()
        {
            var lemonQuantifiable = QuantifiableIngredient.Create(lemon, 5f);
            PurchaseResult purchaseResult1 = _ingredientsCatalogManager.PurchaseIngredient(lemonQuantifiable, _playerMoney);
            Assert.AreEqual(PurchaseResultType.Success, purchaseResult1.ResultType);
            Assert.AreEqual(5f, purchaseResult1.PurchasedIngredient.Quantity);
            var sugarQuantifiable = QuantifiableIngredient.Create(sugar, 3f);
            PurchaseResult purchaseResult2 = _ingredientsCatalogManager.PurchaseIngredient(sugarQuantifiable, _playerMoney);
            Assert.AreEqual(PurchaseResultType.Success, purchaseResult2.ResultType);
            Assert.AreEqual(3f, purchaseResult2.PurchasedIngredient.Quantity);
        }

        [Test]
        public void CalculateTotalCost_Success()
        {
            List<QuantifiableIngredient> ingredients = new() { QuantifiableIngredient.Create(lemon, 5f), QuantifiableIngredient.Create(sugar, 3f) };
            var totalCostResult = _ingredientsCatalogManager.CalculateTotalCost(ingredients);
            Assert.AreEqual(65f, totalCostResult.TotalCost);
            Assert.AreEqual(PurchaseResultType.Success, totalCostResult.ResultType);
        }

        [Test]
        public void CalculateTotalCost_IngredientNotFound()
        {
            var newIngredientMetadata = IngredientMetadata.Create("New Ingredient");
            List<QuantifiableIngredient> ingredients = new() { QuantifiableIngredient.Create(lemon, 5f), QuantifiableIngredient.Create(sugar, 3f), QuantifiableIngredient.Create(newIngredientMetadata, 2f) };
            var totalCostResult = _ingredientsCatalogManager.CalculateTotalCost(ingredients);
            Assert.AreEqual(0f, totalCostResult.TotalCost);
            Assert.AreEqual(PurchaseResultType.IngredientNotFound, totalCostResult.ResultType);
        }

        [Test]
        public void CalculateTotalCost_SingleIngredient_Success()
        {
            List<QuantifiableIngredient> ingredients = new() { QuantifiableIngredient.Create(lemon, 5f) };
            var totalCostResult = _ingredientsCatalogManager.CalculateTotalCost(ingredients);
            Assert.AreEqual(50f, totalCostResult.TotalCost);
            Assert.AreEqual(PurchaseResultType.Success, totalCostResult.ResultType);
        }
    }
}
