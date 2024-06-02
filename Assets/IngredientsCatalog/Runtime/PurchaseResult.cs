namespace baskorp.IngredientsCatalog.Runtime
{
    public enum PurchaseResultType
    {
        Success,
        NotEnoughMoney,
        IngredientNotFound,
        InvalidQuantity
    }

    public class PurchaseResult
    {
        public PurchaseResultType ResultType { get; private set; }
        public Ingredient PurchasedIngredient { get; private set; }

        public PurchaseResult(PurchaseResultType resultType, Ingredient purchasedIngredient = null)
        {
            ResultType = resultType;
            PurchasedIngredient = purchasedIngredient;
        }
    }

    public class TotalCostResult
    {
        public float TotalCost { get; private set; }
        public PurchaseResultType ResultType { get; private set; }

        public TotalCostResult(float totalCost, PurchaseResultType resultType)
        {
            TotalCost = totalCost;
            ResultType = resultType;
        }
    }
}