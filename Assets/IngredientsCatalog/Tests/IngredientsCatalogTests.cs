using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using baskorp.IngredientsCatalog.Runtime;

namespace baskorp.IngredientsCatalog.Tests
{
    [TestFixture]
    public class IngredientsCatalogTests
    {
        private IngredientsCatalogManager _ingredientsCatalogManager;
        private float _playerMoney;
        private IngredientSO lemon;
        private IngredientSO sugar;

        [SetUp]
        public void SetUp()
        {
            lemon = ScriptableObject.CreateInstance<IngredientSO>();
            lemon.ingredientName = "Lemon";
            lemon.basePrice = 10f;
            sugar = ScriptableObject.CreateInstance<IngredientSO>();
            sugar.ingredientName = "Sugar";
            sugar.basePrice = 5f;
            var ingredientsDatabase = new List<IngredientSO> { lemon, sugar };
            _ingredientsCatalogManager = new IngredientsCatalogManager(ingredientsDatabase);
            _playerMoney = 100f;
        }

        [Test]
        public void PurchaseIngredient_Success()
        {
            PurchaseResult purchaseResult = _ingredientsCatalogManager.PurchaseIngredient(lemon, 10f, ref _playerMoney);
            Assert.AreEqual(PurchaseResultType.Success, purchaseResult.ResultType);
            Assert.AreEqual(0f, _playerMoney);
            Assert.AreEqual(10f, purchaseResult.PurchasedIngredient.Quantity);
        }

        [Test]
        public void PurchaseIngredient_NotEnoughMoney()
        {
            PurchaseResult purchaseResult = _ingredientsCatalogManager.PurchaseIngredient(lemon, 11f, ref _playerMoney);
            Assert.AreEqual(PurchaseResultType.NotEnoughMoney, purchaseResult.ResultType);
            Assert.AreEqual(100f, _playerMoney);
            Assert.IsNull(purchaseResult.PurchasedIngredient);
        }

        [Test]
        public void PurchaseIngredient_IngredientNotFound()
        {
            IngredientSO newIngredient = ScriptableObject.CreateInstance<IngredientSO>();
            newIngredient.ingredientName = "New Ingredient";
            newIngredient.basePrice = 15f;
            PurchaseResult purchaseResult = _ingredientsCatalogManager.PurchaseIngredient(newIngredient, 3f, ref _playerMoney);
            Assert.AreEqual(PurchaseResultType.IngredientNotFound, purchaseResult.ResultType);
            Assert.AreEqual(100f, _playerMoney);
            Assert.IsNull(purchaseResult.PurchasedIngredient);
        }

        [Test]
        public void PurchaseIngredient_QuantityZero()
        {
            PurchaseResult purchaseResult = _ingredientsCatalogManager.PurchaseIngredient(lemon, 0f, ref _playerMoney);
            Assert.AreEqual(PurchaseResultType.Success, purchaseResult.ResultType);
            Assert.AreEqual(100f, _playerMoney);
            Assert.AreEqual(0f, purchaseResult.PurchasedIngredient.Quantity);
        }

        [Test]
        public void PurchaseIngredient_NegativeQuantity()
        {
            PurchaseResult purchaseResult = _ingredientsCatalogManager.PurchaseIngredient(lemon, -1f, ref _playerMoney);
            Assert.AreEqual(PurchaseResultType.InvalidQuantity, purchaseResult.ResultType);
            Assert.AreEqual(100f, _playerMoney);
            Assert.IsNull(purchaseResult.PurchasedIngredient);
        }

        [Test]
        public void PurchaseIngredient_MultiplePurchases()
        {
            PurchaseResult purchaseResult1 = _ingredientsCatalogManager.PurchaseIngredient(lemon, 5f, ref _playerMoney);
            Assert.AreEqual(PurchaseResultType.Success, purchaseResult1.ResultType);
            Assert.AreEqual(50f, _playerMoney);
            Assert.AreEqual(5f, purchaseResult1.PurchasedIngredient.Quantity);
            PurchaseResult purchaseResult2 = _ingredientsCatalogManager.PurchaseIngredient(sugar, 3f, ref _playerMoney);
            Assert.AreEqual(PurchaseResultType.Success, purchaseResult2.ResultType);
            Assert.AreEqual(35f, _playerMoney);
            Assert.AreEqual(3f, purchaseResult2.PurchasedIngredient.Quantity);
        }
    }
}
