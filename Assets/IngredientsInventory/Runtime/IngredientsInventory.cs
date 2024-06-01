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

        public UsageResultType UseIngredients(Ingredient ingredient)
        {
            var existingIngredient = _ingredients.Find(i => i.IngredientData == ingredient.IngredientData);
            if (existingIngredient != null) 
            {
                var newQuantity = existingIngredient.Quantity - ingredient.Quantity;
                if (newQuantity == 0)
                {
                    _ingredients.Remove(existingIngredient);
                    return UsageResultType.Success;
                }
                else if (newQuantity < 0)
                {
                    return UsageResultType.NotEnoughQuantity;
                }
                var newIngredient = new Ingredient(ingredient.IngredientData, newQuantity);
                _ingredients.Remove(existingIngredient);
                _ingredients.Add(newIngredient);
                return UsageResultType.Success;
            }
            return UsageResultType.IngredientNotFound;
        }

        public UsageResultType UseIngredients(List<Ingredient> ingredients)
        {
            var validationResult = HasIngredients(ingredients);
            if (validationResult != UsageResultType.Success)
            {
                return validationResult;
            }

            foreach (var ingredient in ingredients)
            {
                var result = UseIngredients(ingredient);
                if (result != UsageResultType.Success)
                {
                    return result;
                }
            }
            return UsageResultType.Success;
        }

        private UsageResultType HasIngredients(List<Ingredient> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                var existingIngredient = _ingredients.Find(i => i.IngredientData == ingredient.IngredientData);
                if (existingIngredient == null)
                {
                    return UsageResultType.IngredientNotFound;
                }
                if (existingIngredient.Quantity < ingredient.Quantity)
                {
                    return UsageResultType.NotEnoughQuantity;
                }
            }
            return UsageResultType.Success;
        }
    }
}