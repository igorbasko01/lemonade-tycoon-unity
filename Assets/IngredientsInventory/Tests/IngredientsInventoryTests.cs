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
        [Ignore("Not implemented")]
        public void AddIngredient_IngredientAlreadyExists()
        {

        }

        [Test]
        [Ignore("Not implemented")]
        public void UseIngredients_Success()
        {

        }

        [Test]
        [Ignore("Not implemented")]
        public void UseIngredients_IngredientNotFound()
        {

        }

        [Test]
        [Ignore("Not implemented")]
        public void UseIngredients_NotEnoughQuantity()
        {

        }

        [Test]
        [Ignore("Not implemented")]
        public void UseIngredients_NegativeQuantity()
        {

        }

        [Test]
        [Ignore("Not implemented")]
        public void UseIngredients_ZeroQuantity()
        {

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
