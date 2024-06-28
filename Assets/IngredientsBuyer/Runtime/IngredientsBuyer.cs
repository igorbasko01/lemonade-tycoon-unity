using System.Collections.Generic;
using baskorp.IngredientsCatalog.Runtime;
using baskorp.IngredientsInventory.Runtime;
using baskorp.Wallets.Runtime;

namespace baskorp.IngredientsBuyers.Runtime
{
    public class IngredientsBuyer
    {
        public PurchaseResult Buy(QuantifiableIngredient ingredient, IngredientsInventoryManager inventory, IngredientsCatalogManager catalog, Wallet wallet)
        {
            var totalCostResult = catalog.CalculateTotalCost(new List<QuantifiableIngredient> { ingredient });
            if (totalCostResult.ResultType != PurchaseResultType.Success)
            {
                return new PurchaseResult(totalCostResult.ResultType, ingredient);
            }

            var purchaseResult = catalog.PurchaseIngredient(ingredient, wallet.Balance);
            if (purchaseResult.ResultType != PurchaseResultType.Success)
            {
                return new PurchaseResult(purchaseResult.ResultType, ingredient);
            }

            wallet.Withdraw(totalCostResult.TotalCost);
            inventory.AddIngredient(purchaseResult.PurchasedIngredient);
            return new PurchaseResult(PurchaseResultType.Success);
        }

        public PurchaseResult Buy(List<QuantifiableIngredient> ingredients, IngredientsInventoryManager inventory, IngredientsCatalogManager catalog, Wallet wallet)
        {
            var totalCostResult = catalog.CalculateTotalCost(ingredients);
            if (totalCostResult.ResultType != PurchaseResultType.Success)
            {
                return new PurchaseResult(totalCostResult.ResultType);
            }

            foreach (var ingredient in ingredients)
            {
                var purchaseResult = catalog.PurchaseIngredient(ingredient, wallet.Balance);
                if (purchaseResult.ResultType != PurchaseResultType.Success)
                {
                    return new PurchaseResult(purchaseResult.ResultType, ingredient);
                }
            }

            wallet.Withdraw(totalCostResult.TotalCost);
            foreach (var ingredient in ingredients)
            {
                inventory.AddIngredient(ingredient);
            }
            return new PurchaseResult(PurchaseResultType.Success);
        }
    }
}