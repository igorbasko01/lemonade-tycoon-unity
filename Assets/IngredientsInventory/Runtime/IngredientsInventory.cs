using System.Collections.Generic;
using baskorp.IngredientsCatalog.Runtime;
using UnityEngine;

namespace baskorp.IngredientsInventory.Runtime
{
    public class IngredientsInventoryManager
    {
        private List<QuantifiableIngredient> _ingredients = new();
        public List<QuantifiableIngredient> Ingredients => _ingredients;
        public void AddIngredient(QuantifiableIngredient ingredient)
        {
            var existingIngredient = _ingredients.Find(i => i.Metadata.Name == ingredient.Metadata.Name);
            if (existingIngredient != null)
            {
                var newIngredient = QuantifiableIngredient.Create(ingredient.Metadata, existingIngredient.Quantity + ingredient.Quantity);
                _ingredients.Remove(existingIngredient);
                _ingredients.Add(newIngredient);
                return;
            }
            _ingredients.Add(ingredient);
        }

        public UsageResultType UseIngredients(QuantifiableIngredient ingredient)
        {
            var existingIngredient = _ingredients.Find(i => i.Metadata.Name == ingredient.Metadata.Name);
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
                var newIngredient = QuantifiableIngredient.Create(ingredient.Metadata, newQuantity);
                _ingredients.Remove(existingIngredient);
                _ingredients.Add(newIngredient);
                return UsageResultType.Success;
            }
            return UsageResultType.IngredientNotFound;
        }

        public UsageResultType UseIngredients(List<QuantifiableIngredient> ingredients)
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

        public float GetIngredientQuantity(IngredientMetadata ingredient)
        {
            var existingIngredient = _ingredients.Find(i => i.Metadata.Equals(ingredient));
            return existingIngredient != null ? existingIngredient.Quantity : 0;
        }

        private UsageResultType HasIngredients(List<QuantifiableIngredient> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                var existingIngredient = _ingredients.Find(i => i.Metadata.Name == ingredient.Metadata.Name);
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