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
        }

        public static Recipe Create(string name, List<QuantifiableIngredient> ingredients)
        {
            var recipe = CreateInstance<Recipe>();
            recipe.recipeName = name;
            recipe.ingredients = ingredients;
            recipe.OnValidate();
            return recipe;
        }
    }
}