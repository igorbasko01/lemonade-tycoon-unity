using System;
using System.Collections.Generic;
using baskorp.IngredientsCatalog.Runtime;

namespace baskorp.Recipes.Runtime {
    public class Recipe {
        public string Name { get; }
        public List<Ingredient> Ingredients { get; }

        public Recipe(string name, List<Ingredient> ingredients) {
            if (ingredients == null) {
                throw new ArgumentException("A recipe must have at least one ingredient");
            }
            if (ingredients.Count == 0) {
                throw new ArgumentException("A recipe must have at least one ingredient");
            }
            Name = name;
            Ingredients = ingredients;
        }
    }
}