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
            set {
                if (value <= 0) {
                    throw new System.ArgumentException("Price should be greater than 0");
                }
                price = value;
            }
        }

        private void OnValidate() {
            if (Metadata == null) {
                throw new System.ArgumentException("Metadata cannot be null");
            }
            if (price <= 0) {
                throw new System.ArgumentException("Price should be greater than 0");
            }
        }
    }
}