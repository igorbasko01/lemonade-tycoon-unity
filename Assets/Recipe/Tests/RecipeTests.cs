using NUnit.Framework;
using baskorp.Recipes.Runtime;
using baskorp.IngredientsCatalog.Runtime;
using System.Collections.Generic;

namespace baskorp.Recipes.Tests
{
    [TestFixture]
    public class RecipeTests
    {
        [Test]
        public void RecipeInitializedWithANameAndAListOfIngredients()
        {
            var recipe = Recipe.Create("Test Recipe", new List<QuantifiableIngredient> { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 5f) });
            Assert.AreEqual("Test Recipe", recipe.Name);
            Assert.AreEqual(1, recipe.Ingredients.Count);
        }

        [Test]
        public void RecipeInitializedWithANameAndAnEmptyListOfIngredients_ThrowsArgumentException()
        {
            Assert.Throws<System.ArgumentException>(() => Recipe.Create("Test Recipe", new List<QuantifiableIngredient>()));
        }

        [Test]
        public void RecipeInitializedWithANameAndNullListOfIngredients_ThrowsArgumentException()
        {
            Assert.Throws<System.ArgumentException>(() => Recipe.Create("Test Recipe", null));
        }

        [Test]
        public void RecipeInitializedWithANameAndAListOfIngredients_IngredientsAreTheSame_ThrowArgumentException()
        {
            Assert.Throws<System.ArgumentException>(() => Recipe.Create("Test Recipe", new List<QuantifiableIngredient> {
                QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 5f),
                QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 2f)
                })
            );
        }

        [Test]
        public void RecipeInitializedWithANameAndAListOfIngredients_SomeIngredientsAreSame_ThrowArgumentException()
        {
            Assert.Throws<System.ArgumentException>(() => Recipe.Create("Test Recipe", new List<QuantifiableIngredient> {
                QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 5f),
                QuantifiableIngredient.Create(IngredientMetadata.Create("Sugar"), 2f),
                QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 2f)
                })
            );
        }

        [Test]
        public void RecipeInitializedWithANameAndAListOfIngredients_IngredientsAreDifferent_Success()
        {
            var recipe = Recipe.Create("Test Recipe", new List<QuantifiableIngredient> {
                QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 5f),
                QuantifiableIngredient.Create(IngredientMetadata.Create("Sugar"), 2f)
                });
            Assert.AreEqual("Test Recipe", recipe.Name);
            Assert.AreEqual(2, recipe.Ingredients.Count);
        }

        [Test]
        public void RecipeInitializedWithAnEmptyName_ThrowsArgumentException()
        {
            Assert.Throws<System.ArgumentException>(() => Recipe.Create("", new List<QuantifiableIngredient> { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 5f) }));
        }

        [Test]
        public void RecipeInitializedWithANullName_ThrowsArgumentException()
        {
            Assert.Throws<System.ArgumentException>(() => Recipe.Create(null, new List<QuantifiableIngredient> { QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 5f) }));
        }

        [Test]
        public void RecipeMake_WithIngredients_Success()
        {
            var recipe = Recipe.Create("Test Recipe", new List<QuantifiableIngredient> {
                QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 5f),
                QuantifiableIngredient.Create(IngredientMetadata.Create("Sugar"), 2f)
                });
            var ingredients = new List<QuantifiableIngredient> {
                QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 5f),
                QuantifiableIngredient.Create(IngredientMetadata.Create("Sugar"), 2f)
            };
            var result = recipe.Make(ingredients);
            Assert.AreEqual(RecipeResultType.Success, result.ResultType);
            Assert.AreEqual(QuantifiableIngredient.Create(IngredientMetadata.Create("Test Recipe"), 1f), result.ProducedIngredient);
            Assert.IsEmpty(result.MissingIngredients);
        }

        [Test]
        public void RecipeMake_MissingIngredient_ReturnsMissingIngredientsList()
        {
            var recipe = Recipe.Create("Test Recipe", new List<QuantifiableIngredient> {
                QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 5f),
                QuantifiableIngredient.Create(IngredientMetadata.Create("Sugar"), 2f)
                });
            var ingredients = new List<QuantifiableIngredient> {
                QuantifiableIngredient.Create(IngredientMetadata.Create("Lemon"), 5f),
                QuantifiableIngredient.Create(IngredientMetadata.Create("Ice"), 1f)
            };
            var result = recipe.Make(ingredients);
            Assert.AreEqual(RecipeResultType.InvalidQuantity, result.ResultType);
            Assert.IsNull(result.ProducedIngredient);
            Assert.That(new List<QuantifiableIngredient> { QuantifiableIngredient.Create(IngredientMetadata.Create("Sugar"), 2f) }, Is.EquivalentTo(result.MissingIngredients));
        }

        [Test]
        [Ignore("Not implemented yet")]
        public void RecipeMake_MissingQuantity_ReturnsNotEnoughQuantity()
        {

        }

        [Test]
        [Ignore("Not implemented yet")]
        public void RecipeMake_EmptyIngredientsList_ReturnsMissingIngredientsList()
        {

        }

        [Test]
        [Ignore("Not implemented yet")]
        public void RecipeMake_NullIngredientsList_ReturnsMissingIngredientList()
        {

        }
    }
}