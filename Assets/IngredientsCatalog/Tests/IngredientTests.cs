using NUnit.Framework;
using UnityEngine;
using baskorp.IngredientsCatalog.Runtime;

namespace baskorp.IngredientsCatalog.Tests
{
    [TestFixture]
    public class IngredientsTests
    {
        private IngredientMetadata lemonMetadata;
        [SetUp]
        public void Setup()
        {
            lemonMetadata = IngredientMetadata.Create("Lemon");
        }

        [Test]
        public void Create_IngredientMetadata_Success()
        {
            Assert.AreEqual("Lemon", lemonMetadata.Name);
        }

        [Test]
        public void Create_Ingredient_WithNegativeQuantity()
        {
            Assert.Throws<System.ArgumentException>(() => QuantifiableIngredient.Create(lemonMetadata, -1f));
        }

        [Test]
        public void Create_Ingredient_WithZeroQuantity()
        {
            Assert.Throws<System.ArgumentException>(() => QuantifiableIngredient.Create(lemonMetadata, 0f));
        }

        [Test]
        public void Create_Ingredient_WithNegativePrice()
        {
            Assert.Throws<System.ArgumentException>(() => SellableIngredient.Create(lemonMetadata, -1f));
        }

        [Test]
        public void Create_Ingredient_WithZeroPrice()
        {
            Assert.Throws<System.ArgumentException>(() => SellableIngredient.Create(lemonMetadata, 0f));
        }

        [Test]
        public void Create_Ingredient_WithNullMetadata()
        {
            Assert.Throws<System.ArgumentException>(() => QuantifiableIngredient.Create(null, 1f));
        }

        [Test]
        public void Create_Ingredient_WithNullMetadata_Sellable()
        {
            Assert.Throws<System.ArgumentException>(() => SellableIngredient.Create(null, 1f));
        }
    }
}