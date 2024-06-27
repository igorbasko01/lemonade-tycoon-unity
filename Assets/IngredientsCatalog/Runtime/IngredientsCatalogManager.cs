using System.Collections.Generic;
using UnityEngine;

namespace baskorp.IngredientsCatalog.Runtime
{
    public class IngredientsCatalogManager
    {
        public List<SellableIngredient> AvailableIngredients { get; private set; }

        public IngredientsCatalogManager(List<SellableIngredient> ingredientsDatabase)
        {
            AvailableIngredients = ingredientsDatabase;
        }
        
        public PurchaseResult PurchaseIngredient(QuantifiableIngredient ingredient, float playerMoney)
        {
            var ingredientFound = AvailableIngredients.Find(i => i.Metadata.Equals(ingredient.Metadata));
            if (ingredientFound == null)
            {
                return new PurchaseResult(PurchaseResultType.IngredientNotFound);
            }
            if (ingredient.Quantity <= 0)
            {
                return new PurchaseResult(PurchaseResultType.InvalidQuantity);
            }
            var totalCost = ingredientFound.Price * ingredient.Quantity;
            if (playerMoney < totalCost)
            {
                return new PurchaseResult(PurchaseResultType.NotEnoughMoney);
            }
            var soldIngredient = QuantifiableIngredient.Create(ingredient.Metadata, ingredient.Quantity);
            return new PurchaseResult(PurchaseResultType.Success, soldIngredient);
        }

        public TotalCostResult CalculateTotalCost(List<QuantifiableIngredient> ingredients)
        {
            float totalCost = 0;
            foreach (var ingredient in ingredients)
            {
                var ingredientFound = AvailableIngredients.Find(i => i.Metadata.Equals(ingredient.Metadata));
                if (ingredientFound == null)
                {
                    return new TotalCostResult(0, PurchaseResultType.IngredientNotFound);
                }
                totalCost += ingredientFound.Price * ingredient.Quantity;
            }
            return new TotalCostResult(totalCost, PurchaseResultType.Success);
        }
    }
}