namespace baskorp.IngredientsCatalog.Runtime
{
    [System.Serializable]
    public class Ingredient
    {
        public IngredientSO IngredientData { get; private set; }
        public float Quantity { get; private set; }

        public Ingredient(IngredientSO ingredientData, float quantity)
        {
            IngredientData = ingredientData;
            Quantity = quantity;
        }
    }
}