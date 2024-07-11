using baskorp.Calendars.Runtime;
using baskorp.IngredientsCatalog.Runtime;
using baskorp.IngredientsInventory.Runtime;
using baskorp.Recipes.Runtime;
using baskorp.Weather.Runtime;

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
    }
}