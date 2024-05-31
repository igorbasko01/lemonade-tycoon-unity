using System.Collections.Generic;
using baskorp.IngredientsCatalog.Runtime;

namespace baskorp.IngredientsInventory.Runtime
{
    public class IngredientsInventoryManager
    {
        private List<Ingredient> _ingredients = new();
        public List<Ingredient> Ingredients => _ingredients;
        public void AddIngredient(Ingredient ingredient)
        {
            var existingIngredient = _ingredients.Find(i => i.IngredientData == ingredient.IngredientData);
            if (existingIngredient != null)
            {
                var newIngredient = new Ingredient(ingredient.IngredientData, existingIngredient.Quantity + ingredient.Quantity);
                _ingredients.Remove(existingIngredient);
                _ingredients.Add(newIngredient);
                return;
            }
            _ingredients.Add(ingredient);
        }

        public void UseIngredients(Ingredient ingredient)
        {
            throw new System.NotImplementedException();
        }
    }
}