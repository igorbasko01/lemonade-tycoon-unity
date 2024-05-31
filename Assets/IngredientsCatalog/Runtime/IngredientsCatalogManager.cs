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
            if (quantity < 0)
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
    }
}