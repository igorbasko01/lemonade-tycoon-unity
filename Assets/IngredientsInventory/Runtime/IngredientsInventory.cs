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
            _ingredients.Add(ingredient);
        }

        public void UseIngredients(Ingredient ingredient)
        {
            throw new System.NotImplementedException();
        }
    }
}