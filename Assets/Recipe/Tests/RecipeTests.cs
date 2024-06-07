using NUnit.Framework;
using baskorp.Recipes.Runtime;
using baskorp.IngredientsCatalog.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace baskorp.Recipes.Tests
{
    [TestFixture]
    public class RecipeTests
    {
        [Test]
        public void RecipeInitializedWithANameAndAListOfIngredients()
        {
            var recipe = new Recipe("Test Recipe", new List<QuantifiableIngredient> { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 5f)});
            Assert.AreEqual("Test Recipe", recipe.Name);
            Assert.AreEqual(1, recipe.Ingredients.Count);
        }

        [Test]
        public void RecipeInitializedWithANameAndAnEmptyListOfIngredients_ThrowsArgumentException()
        {
            Assert.Throws<System.ArgumentException>(() => new Recipe("Test Recipe", new List<QuantifiableIngredient>()));
        }

        [Test]
        public void RecipeInitializedWithANameAndNullListOfIngredients_ThrowsArgumentException()
        {
            Assert.Throws<System.ArgumentException>(() => new Recipe("Test Recipe", null));
        }

        [Test]
        [Ignore("Need to implement")]
        public void RecipeInitializedWithANameAndAListOfIngredients_IngredientsAreTheSame()
        {
            
        }

        [Test]
        [Ignore("Need to implement")]
        public void RecipeInitializedWithANameAndAListOfIngredients_IngredientsAreDifferent()
        {
            
        }

        [Test]
        [Ignore("Need to implement")]
        public void RecipeInitializedWithAnEmptyName_ThrowsArgumentException()
        {
            
        }

        [Test]
        [Ignore("Need to implement")]
        public void RecipeInitializedWithANullName_ThrowsArgumentException()
        {
            
        }
    }
}