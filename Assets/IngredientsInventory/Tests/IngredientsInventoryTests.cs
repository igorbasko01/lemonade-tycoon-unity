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

        [SetUp]
        public void SetUp()
        {
            _ingredientsInventoryManager = new IngredientsInventoryManager();
        }

        [Test]
        public void AddIngredient_Success()
        {
            var lemon = ScriptableObject.CreateInstance<IngredientSO>();
            lemon.ingredientName = "Lemon";
            lemon.basePrice = 10f;
            var ingredient = new Ingredient(lemon, 5f);
            _ingredientsInventoryManager.AddIngredient(ingredient);
            Assert.AreEqual(1, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(ingredient, _ingredientsInventoryManager.Ingredients[0]);
            Assert.AreEqual(5f, _ingredientsInventoryManager.Ingredients[0].Quantity);
        }

        [Test]
        public void AddIngredient_IngredientAlreadyExists()
        {
            var lemon = ScriptableObject.CreateInstance<IngredientSO>();
            lemon.ingredientName = "Lemon";
            lemon.basePrice = 10f;
            var ingredient = new Ingredient(lemon, 5f);
            var moreIngredients = new Ingredient(lemon, 3f);
            _ingredientsInventoryManager.AddIngredient(ingredient);
            _ingredientsInventoryManager.AddIngredient(moreIngredients);
            Assert.AreEqual(1, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(8f, _ingredientsInventoryManager.Ingredients[0].Quantity);
        }

        [Test]
        public void UseIngredients_Success()
        {
            var lemon = ScriptableObject.CreateInstance<IngredientSO>();
            lemon.ingredientName = "Lemon";
            lemon.basePrice = 10f;
            var ingredient = new Ingredient(lemon, 5f);
            _ingredientsInventoryManager.AddIngredient(ingredient);
            var result = _ingredientsInventoryManager.UseIngredients(ingredient);
            Assert.AreEqual(0, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(UsageResultType.Success, result);
        }
        
        [Test]
        public void UseIngredients_PartialQuantity_Success()
        {
            var lemon = ScriptableObject.CreateInstance<IngredientSO>();
            lemon.ingredientName = "Lemon";
            lemon.basePrice = 10f;
            var ingredient = new Ingredient(lemon, 5f);
            _ingredientsInventoryManager.AddIngredient(ingredient);
            var result = _ingredientsInventoryManager.UseIngredients(new Ingredient(lemon, 3f));
            Assert.AreEqual(1, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(2f, _ingredientsInventoryManager.Ingredients[0].Quantity);
            Assert.AreEqual(UsageResultType.Success, result);
        }

        [Test]
        public void UseIngredients_IngredientNotFound()
        {
            var lemon = ScriptableObject.CreateInstance<IngredientSO>();
            lemon.ingredientName = "Lemon";
            lemon.basePrice = 10f;
            var result = _ingredientsInventoryManager.UseIngredients(new Ingredient(lemon, 3f));
            Assert.AreEqual(0, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(UsageResultType.IngredientNotFound, result);
        }

        [Test]
        public void UseIngredients_NotEnoughQuantity()
        {
            var lemon = ScriptableObject.CreateInstance<IngredientSO>();
            lemon.ingredientName = "Lemon";
            lemon.basePrice = 10f;
            var ingredient = new Ingredient(lemon, 5f);
            _ingredientsInventoryManager.AddIngredient(ingredient);
            var result = _ingredientsInventoryManager.UseIngredients(new Ingredient(lemon, 6f));
            Assert.AreEqual(1, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(5f, _ingredientsInventoryManager.Ingredients[0].Quantity);
            Assert.AreEqual(UsageResultType.NotEnoughQuantity, result);
        }

        [Test]
        [Ignore("Not implemented")]
        public void UseIngredients_MultipleIngredients_Success()
        {

        }

        [Test]
        [Ignore("Not implemented")]
        public void UseIngredients_MultipleIngredients_IngredientNotFound()
        {

        }

        [Test]
        [Ignore("Not implemented")]
        public void UseIngredients_MultipleIngredients_NotEnoughQuantity()
        {

        }

        [Test]
        [Ignore("Not implemented")]
        public void UseIngredients_MultipleIngredients_NegativeQuantity()
        {

        }

        [Test]
        [Ignore("Not implemented")]
        public void UseIngredients_MultipleIngredients_ZeroQuantity()
        {

        }

        [Test]
        [Ignore("Not implemented")]
        public void GetIngredientQuantity_Success()
        {

        }

        [Test]
        [Ignore("Not implemented")]
        public void GetIngredientQuantity_IngredientNotFound()
        {

        }
    }
}
