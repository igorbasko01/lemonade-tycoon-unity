using NUnit.Framework;
using UnityEngine;
using baskorp.IngredientsCatalog.Runtime;

namespace baskorp.IngredientsCatalog.Tests
{
    [TestFixture]
    public class IngredientsTests
    {
        [Test]
        public void Create_Ingredient_Success()
        {
            var ingredient = ScriptableObject.CreateInstance<IngredientSO>();
            ingredient.ingredientName = "Lemon";
            ingredient.basePrice = 10f;
            Assert.AreEqual("Lemon", ingredient.ingredientName);
            Assert.AreEqual(10f, ingredient.basePrice);
        }

        [Test]
        public void Create_Ingredient_WithNegativeQuantity()
        {
            var ingredient = ScriptableObject.CreateInstance<IngredientSO>();
            ingredient.ingredientName = "Lemon";
            ingredient.basePrice = 10f;
            Assert.Throws<System.ArgumentException>(() => new Ingredient(ingredient, -5f));
        }

        [Test]
        public void Create_Ingredient_WithZeroQuantity()
        {
            var ingredient = ScriptableObject.CreateInstance<IngredientSO>();
            ingredient.ingredientName = "Lemon";
            ingredient.basePrice = 10f;
            Assert.Throws<System.ArgumentException>(() => new Ingredient(ingredient, 0f));
        }
    }
}