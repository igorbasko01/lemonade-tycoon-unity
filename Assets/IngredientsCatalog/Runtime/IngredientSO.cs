using UnityEngine;

namespace baskorp.IngredientsCatalog.Runtime
{
    [CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredients Catalog/Ingredient")]
    public class IngredientSO : ScriptableObject
    {
        public string ingredientName;
        public float basePrice;
    }
}