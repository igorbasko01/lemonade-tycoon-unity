using System;
using baskorp.Calendars.Runtime;
using baskorp.IngredientsCatalog.Runtime;
using baskorp.IngredientsInventory.Runtime;
using baskorp.Recipes.Runtime;
using baskorp.Wallets.Runtime;
using UnityEngine;

namespace baskorp.DayEnd.Runtime
{
    public class DayEndSystem
    {
        private readonly RecipeEvaluator recipeEvaluator;
        private readonly IngredientsCatalogManager ingredientsCatalog;
        private readonly IngredientsInventoryManager ingredientsInventory;
        private readonly int maxOrders;
        public DayEndSystem(RecipeEvaluator recipeEvaluator, IngredientsCatalogManager ingredientsCatalog, IngredientsInventoryManager ingredientsInventory, int maxOrders)
        {
            this.recipeEvaluator = recipeEvaluator;
            this.ingredientsCatalog = ingredientsCatalog;
            this.ingredientsInventory = ingredientsInventory;
            this.maxOrders = maxOrders;
        }

        public int CalculateDemand(Recipe recipe, Date date, float recipePrice)
        {
            var basePrice = recipe.CalculatePrice(ingredientsCatalog);
            if (basePrice.ResultType != PurchaseResultType.Success)
            {
                return 0;
            }
            var deltaPricePercent = (recipePrice - basePrice.TotalCost) / basePrice.TotalCost;
            var recipeEvaluatorResult = recipeEvaluator.Evaluate(recipe, date);
            return (int)((recipeEvaluatorResult - deltaPricePercent) * maxOrders);
        }

        public DayEndResult EndDay(Recipe recipe, Date date, float recipePrice, Wallet wallet)
        {
            var demand = CalculateDemand(recipe, date, recipePrice);
            Debug.Log($"Demand for {recipe.Name} is {demand}");
            var availableQuantity = ingredientsInventory.GetIngredientQuantity(IngredientMetadata.Create(recipe.Name));
            Debug.Log($"Available quantity for {recipe.Name} is {availableQuantity}");
            var quantityToSell = Math.Min(demand, availableQuantity);
            Debug.Log($"Quantity to sell for {recipe.Name} is {quantityToSell}");
            var totalIncome = quantityToSell * recipePrice;
            Debug.Log($"Total income for {recipe.Name} is {totalIncome}");
            var dayEndResult = new DayEndResult(date, recipe, recipePrice, quantityToSell, totalIncome);

            if (quantityToSell > 0)
            {
                wallet.Deposit(totalIncome);
                ingredientsInventory.UseIngredients(QuantifiableIngredient.Create(IngredientMetadata.Create(recipe.Name), quantityToSell));
            }

            return dayEndResult;
        }
    }
}