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
}