using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using baskorp.IngredientsInventory.Runtime;
using baskorp.IngredientsCatalog.Runtime;

namespace baskorp.IngredientsInventory.Tests
{
    [TestFixture]
    public class IngredientsInventoryTests
    {
        private IngredientsInventoryManager _ingredientsInventoryManager;
        private IngredientMetadata lemon;
        private IngredientMetadata sugar;

        [SetUp]
        public void SetUp()
        {
            lemon = IngredientMetadata.Create("Lemon");
            sugar = IngredientMetadata.Create("Sugar");
            _ingredientsInventoryManager = new IngredientsInventoryManager();
        }

        [Test]
        public void AddIngredient_Success()
        {
            var ingredient = QuantifiableIngredient.Create(lemon, 5f);
            _ingredientsInventoryManager.AddIngredient(ingredient);
            Assert.AreEqual(1, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(ingredient, _ingredientsInventoryManager.Ingredients[0]);
            Assert.AreEqual(5f, _ingredientsInventoryManager.Ingredients[0].Quantity);
        }

        [Test]
        public void AddIngredient_IngredientAlreadyExists()
        {
            var ingredient = QuantifiableIngredient.Create(lemon, 5f);
            var moreIngredients = QuantifiableIngredient.Create(lemon, 3f);
            _ingredientsInventoryManager.AddIngredient(ingredient);
            _ingredientsInventoryManager.AddIngredient(moreIngredients);
            Assert.AreEqual(1, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(8f, _ingredientsInventoryManager.Ingredients[0].Quantity);
        }

        [Test]
        public void UseIngredients_Success()
        {
            var ingredient = QuantifiableIngredient.Create(lemon, 5f);
            _ingredientsInventoryManager.AddIngredient(ingredient);
            var result = _ingredientsInventoryManager.UseIngredients(ingredient);
            Assert.AreEqual(0, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(UsageResultType.Success, result);
        }
        
        [Test]
        public void UseIngredients_PartialQuantity_Success()
        {
            var ingredient = QuantifiableIngredient.Create(lemon, 5f);
            _ingredientsInventoryManager.AddIngredient(ingredient);
            var result = _ingredientsInventoryManager.UseIngredients(QuantifiableIngredient.Create(lemon, 3f));
            Assert.AreEqual(1, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(2f, _ingredientsInventoryManager.Ingredients[0].Quantity);
            Assert.AreEqual(UsageResultType.Success, result);
        }

        [Test]
        public void UseIngredients_IngredientNotFound()
        {
            var result = _ingredientsInventoryManager.UseIngredients(QuantifiableIngredient.Create(lemon, 5f));
            Assert.AreEqual(0, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(UsageResultType.IngredientNotFound, result);
        }

        [Test]
        public void UseIngredients_NotEnoughQuantity()
        {
            var ingredient = QuantifiableIngredient.Create(lemon, 5f);
            _ingredientsInventoryManager.AddIngredient(ingredient);
            var result = _ingredientsInventoryManager.UseIngredients(QuantifiableIngredient.Create(lemon, 6f));
            Assert.AreEqual(1, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(5f, _ingredientsInventoryManager.Ingredients[0].Quantity);
            Assert.AreEqual(UsageResultType.NotEnoughQuantity, result);
        }

        [Test]
        public void UseIngredients_MultipleIngredients_Success()
        {
            var lemonIngredient = QuantifiableIngredient.Create(lemon, 5f);
            var sugarIngredient = QuantifiableIngredient.Create(sugar, 3f);
            _ingredientsInventoryManager.AddIngredient(lemonIngredient);
            _ingredientsInventoryManager.AddIngredient(sugarIngredient);
            var ingredients = new List<QuantifiableIngredient> { lemonIngredient, sugarIngredient };
            var result = _ingredientsInventoryManager.UseIngredients(ingredients);
            Assert.AreEqual(0, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(UsageResultType.Success, result);
        }

        [Test]
        public void UseIngredients_MultipleIngredients_IngredientNotFound()
        {
            var lemonIngredient = QuantifiableIngredient.Create(lemon, 5f);
            var sugarIngredient = QuantifiableIngredient.Create(sugar, 3f);
            _ingredientsInventoryManager.AddIngredient(lemonIngredient);
            _ingredientsInventoryManager.AddIngredient(sugarIngredient);
            var iceMetadata = IngredientMetadata.Create("Ice");
            var iceIngredient = QuantifiableIngredient.Create(iceMetadata, 2f);
            var ingredients = new List<QuantifiableIngredient> { lemonIngredient, iceIngredient };
            var result = _ingredientsInventoryManager.UseIngredients(ingredients);
            Assert.AreEqual(2, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(5f, _ingredientsInventoryManager.Ingredients[0].Quantity);
            Assert.AreEqual(3f, _ingredientsInventoryManager.Ingredients[1].Quantity);
            Assert.AreEqual(UsageResultType.IngredientNotFound, result);
        }

        [Test]
        public void UseIngredients_MultipleIngredients_NotEnoughQuantity()
        {
            var lemonIngredient = QuantifiableIngredient.Create(lemon, 5f);
            var sugarIngredient = QuantifiableIngredient.Create(sugar, 3f);
            _ingredientsInventoryManager.AddIngredient(lemonIngredient);
            _ingredientsInventoryManager.AddIngredient(sugarIngredient);
            var ingredients = new List<QuantifiableIngredient> { lemonIngredient, QuantifiableIngredient.Create(sugar, 4f)};
            var result = _ingredientsInventoryManager.UseIngredients(ingredients);
            Assert.AreEqual(2, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(5f, _ingredientsInventoryManager.Ingredients[0].Quantity);
            Assert.AreEqual(3f, _ingredientsInventoryManager.Ingredients[1].Quantity);
            Assert.AreEqual(UsageResultType.NotEnoughQuantity, result);
        }

        [Test]
        public void GetIngredientQuantity_Success()
        {
            var ingredient = QuantifiableIngredient.Create(lemon, 5f);
            _ingredientsInventoryManager.AddIngredient(ingredient);
            var quantity = _ingredientsInventoryManager.GetIngredientQuantity(lemon);
            Assert.AreEqual(5f, quantity);
        }

        [Test]
        public void GetIngredientQuantity_IngredientNotFound_ReturnZeroQuantity()
        {
            var quantity = _ingredientsInventoryManager.GetIngredientQuantity(lemon);
            Assert.AreEqual(0f, quantity);
        }
    }
}
