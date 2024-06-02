using System.Collections.Generic;

namespace baskorp.IngredientsCatalog.Runtime
{
    public class IngredientsCatalogManager
    {
        public List<IngredientSO> AvailableIngredients { get; private set; }

        public IngredientsCatalogManager(List<IngredientSO> ingredientsDatabase)
        {
            AvailableIngredients = ingredientsDatabase;
        }
        public PurchaseResult PurchaseIngredient(IngredientSO ingredient, float quantity, ref float playerMoney)
        {
            var ingredientFound = AvailableIngredients.Find(i => i == ingredient);
            if (ingredientFound == null)
            {
                return new PurchaseResult(PurchaseResultType.IngredientNotFound);
            }
            if (quantity <= 0)
            {
                return new PurchaseResult(PurchaseResultType.InvalidQuantity);
            }
            var totalCost = ingredient.basePrice * quantity;
            if (playerMoney < totalCost)
            {
                return new PurchaseResult(PurchaseResultType.NotEnoughMoney);
            }
            playerMoney -= totalCost;
            return new PurchaseResult(PurchaseResultType.Success, new Ingredient(ingredient, quantity));
        }

        public TotalCostResult CalculateTotalCost(List<Ingredient> ingredients)
        {
            float totalCost = 0;
            foreach (var ingredient in ingredients)
            {
                var ingredientFound = AvailableIngredients.Find(i => i == ingredient.IngredientData);
                if (ingredientFound == null)
                {
                    return new TotalCostResult(0, PurchaseResultType.IngredientNotFound);
                }
                totalCost += ingredientFound.basePrice * ingredient.Quantity;
            }
            return new TotalCostResult(totalCost, PurchaseResultType.Success);
        }
    }
}