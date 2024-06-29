using System;
using System.Collections.Generic;
using baskorp.IngredientsCatalog.Runtime;
using UnityEngine;

namespace baskorp.Recipes.Runtime
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
    public class Recipe : ScriptableObject
    {
        [SerializeField]
        private string recipeName;
        [SerializeField]
        private List<QuantifiableIngredient> ingredients;
        public string Name { get => recipeName; }
        public List<QuantifiableIngredient> Ingredients { get => ingredients; }

        private void OnValidate()
        {
            if (ingredients == null)
            {
                throw new ArgumentException("A recipe must have at least one ingredient");
            }
            if (ingredients.Count == 0)
            {
                throw new ArgumentException("A recipe must have at least one ingredient");
            }
            if (string.IsNullOrEmpty(recipeName))
            {
                throw new ArgumentException("A recipe must have a name");
            }
            ValidateNoDuplicateIngredients();
        }

        private void ValidateNoDuplicateIngredients()
        {
            var ingredientNames = new List<string>();
            foreach (var ingredient in ingredients)
            {
                if (ingredientNames.Contains(ingredient.Metadata.Name))
                {
                    throw new ArgumentException("A recipe cannot have duplicate ingredients");
                }
                ingredientNames.Add(ingredient.Metadata.Name);
            }
        }

        public static Recipe Create(string name, List<QuantifiableIngredient> ingredients)
        {
            var recipe = CreateInstance<Recipe>();
            recipe.recipeName = name;
            recipe.ingredients = ingredients;
            recipe.OnValidate();
            return recipe;
        }

        public RecipeResult Make(List<QuantifiableIngredient> ingredients, int quantity = 1)
        {
            var missingIngredients = CalculateMissingIngredients(ingredients);
            if (missingIngredients.Count > 0)
            {
                return new RecipeResult(
                    RecipeResultType.InvalidQuantity,
                    null,
                    missingIngredients
                    );
            }
            return new RecipeResult(
                RecipeResultType.Success, 
                QuantifiableIngredient.Create(IngredientMetadata.Create(recipeName), quantity),
                new List<QuantifiableIngredient>()
                );
        }

        public List<QuantifiableIngredient> CalculateMissingIngredients(List<QuantifiableIngredient> ingredients)
        {
            var safeIngredients = ingredients ?? new List<QuantifiableIngredient>();
            var missingIngredients = new List<QuantifiableIngredient>();
            foreach (var ingredient in this.ingredients)
            {
                var existingIngredient = safeIngredients.Find(i => i.Metadata.Name == ingredient.Metadata.Name);
                if (existingIngredient == null)
                {
                    missingIngredients.Add(ingredient);
                }
                else if (existingIngredient.Quantity < ingredient.Quantity)
                {
                    missingIngredients.Add(QuantifiableIngredient.Create(ingredient.Metadata, ingredient.Quantity - existingIngredient.Quantity));
                }
            }
            return missingIngredients;
        }
    }
}