using UnityEngine;

namespace baskorp.IngredientsCatalog.Runtime
{
    [CreateAssetMenu(fileName = "New Sellable Ingredient", menuName = "Ingredients Catalog/Sellable Ingredient")]
    public class SellableIngredient : ScriptableObject
    {
        public IngredientMetadata Metadata;
        [SerializeField]
        private float price;
        public float Price 
        {
            get { return price; }
        }

        private void OnValidate() {
            if (Metadata == null) {
                throw new System.ArgumentException("Metadata cannot be null");
            }
            if (price <= 0) {
                throw new System.ArgumentException("Price should be greater than 0");
            }
        }

        public static SellableIngredient Create(IngredientMetadata metadata, float price) {
            var ingredient = CreateInstance<SellableIngredient>();
            ingredient.Metadata = metadata;
            ingredient.price = price;
            ingredient.OnValidate();
            return ingredient;
        }
    }
}