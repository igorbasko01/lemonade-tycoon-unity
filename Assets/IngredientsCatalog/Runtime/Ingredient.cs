namespace baskorp.IngredientsCatalog.Runtime
{
    [System.Serializable]
    public class Ingredient
    {
        public IngredientSO IngredientData { get; private set; }
        public float Quantity { get; private set; }

        public Ingredient(IngredientSO ingredientData, float quantity)
        {
            if (quantity <= 0)
            {
                throw new System.ArgumentException("Quantity must be greater than 0");
            }
            IngredientData = ingredientData;
            Quantity = quantity;
        }
    }
}