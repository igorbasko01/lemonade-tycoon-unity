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
        private IngredientSO lemon;
        private IngredientSO sugar;

        [SetUp]
        public void SetUp()
        {
            _ingredientsInventoryManager = new IngredientsInventoryManager();
            lemon = ScriptableObject.CreateInstance<IngredientSO>();
            lemon.ingredientName = "Lemon";
            lemon.basePrice = 10f;
            sugar = ScriptableObject.CreateInstance<IngredientSO>();
            sugar.ingredientName = "Sugar";
            sugar.basePrice = 5f;
        }

        [Test]
        public void AddIngredient_Success()
        {
            var ingredient = new Ingredient(lemon, 5f);
            _ingredientsInventoryManager.AddIngredient(ingredient);
            Assert.AreEqual(1, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(ingredient, _ingredientsInventoryManager.Ingredients[0]);
            Assert.AreEqual(5f, _ingredientsInventoryManager.Ingredients[0].Quantity);
        }

        [Test]
        public void AddIngredient_IngredientAlreadyExists()
        {
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
            var ingredient = new Ingredient(lemon, 5f);
            _ingredientsInventoryManager.AddIngredient(ingredient);
            var result = _ingredientsInventoryManager.UseIngredients(ingredient);
            Assert.AreEqual(0, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(UsageResultType.Success, result);
        }
        
        [Test]
        public void UseIngredients_PartialQuantity_Success()
        {
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
            var result = _ingredientsInventoryManager.UseIngredients(new Ingredient(lemon, 3f));
            Assert.AreEqual(0, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(UsageResultType.IngredientNotFound, result);
        }

        [Test]
        public void UseIngredients_NotEnoughQuantity()
        {
            var ingredient = new Ingredient(lemon, 5f);
            _ingredientsInventoryManager.AddIngredient(ingredient);
            var result = _ingredientsInventoryManager.UseIngredients(new Ingredient(lemon, 6f));
            Assert.AreEqual(1, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(5f, _ingredientsInventoryManager.Ingredients[0].Quantity);
            Assert.AreEqual(UsageResultType.NotEnoughQuantity, result);
        }

        [Test]
        public void UseIngredients_MultipleIngredients_Success()
        {
            var lemonIngredient = new Ingredient(lemon, 5f);
            var sugarIngredient = new Ingredient(sugar, 3f);
            _ingredientsInventoryManager.AddIngredient(lemonIngredient);
            _ingredientsInventoryManager.AddIngredient(sugarIngredient);
            var ingredients = new List<Ingredient> { lemonIngredient, sugarIngredient };
            var result = _ingredientsInventoryManager.UseIngredients(ingredients);
            Assert.AreEqual(0, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(UsageResultType.Success, result);
        }

        [Test]
        public void UseIngredients_MultipleIngredients_IngredientNotFound()
        {
            var lemonIngredient = new Ingredient(lemon, 5f);
            var sugarIngredient = new Ingredient(sugar, 3f);
            _ingredientsInventoryManager.AddIngredient(lemonIngredient);
            _ingredientsInventoryManager.AddIngredient(sugarIngredient);
            var ice = ScriptableObject.CreateInstance<IngredientSO>();
            ice.ingredientName = "Ice";
            ice.basePrice = 3f;
            var iceIngredient = new Ingredient(ice, 2f);
            var ingredients = new List<Ingredient> { lemonIngredient, iceIngredient };
            var result = _ingredientsInventoryManager.UseIngredients(ingredients);
            Assert.AreEqual(2, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(5f, _ingredientsInventoryManager.Ingredients[0].Quantity);
            Assert.AreEqual(3f, _ingredientsInventoryManager.Ingredients[1].Quantity);
            Assert.AreEqual(UsageResultType.IngredientNotFound, result);
        }

        [Test]
        public void UseIngredients_MultipleIngredients_NotEnoughQuantity()
        {
            var lemonIngredient = new Ingredient(lemon, 5f);
            var sugarIngredient = new Ingredient(sugar, 3f);
            _ingredientsInventoryManager.AddIngredient(lemonIngredient);
            _ingredientsInventoryManager.AddIngredient(sugarIngredient);
            var ingredients = new List<Ingredient> { lemonIngredient, new Ingredient(sugar, 4f) };
            var result = _ingredientsInventoryManager.UseIngredients(ingredients);
            Assert.AreEqual(2, _ingredientsInventoryManager.Ingredients.Count);
            Assert.AreEqual(5f, _ingredientsInventoryManager.Ingredients[0].Quantity);
            Assert.AreEqual(3f, _ingredientsInventoryManager.Ingredients[1].Quantity);
            Assert.AreEqual(UsageResultType.NotEnoughQuantity, result);
        }

        [Test]
        public void GetIngredientQuantity_Success()
        {
            var ingredient = new Ingredient(lemon, 5f);
            _ingredientsInventoryManager.AddIngredient(ingredient);
            var quantity = _ingredientsInventoryManager.GetIngredientQuantity(lemon);
            Assert.AreEqual(5f, quantity);
        }

        [Test]
        [Ignore("Not implemented")]
        public void GetIngredientQuantity_IngredientNotFound_ReturnZeroQuantity()
        {

        }
    }
}
