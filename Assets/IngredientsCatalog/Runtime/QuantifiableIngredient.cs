using UnityEngine;

namespace baskorp.IngredientsCatalog.Runtime
{
    [CreateAssetMenu(fileName = "New Quantifiable Ingredient", menuName = "Ingredients Catalog/Quantifiable Ingredient")]
    public class QuantifiableIngredient : ScriptableObject 
    {
        public IngredientMetadata Metadata;

        [SerializeField]
        private float quantity;
        public float Quantity {
            get => quantity;
        }

        private void OnValidate() {
            if (Metadata == null) {
                throw new System.ArgumentException("Metadata cannot be null");
            }
            if (quantity <= 0) {
                throw new System.ArgumentException("Quantity should be greater than 0");
            }
        }

        public static QuantifiableIngredient Create(IngredientMetadata metadata, float quantity) {
            var ingredient = CreateInstance<QuantifiableIngredient>();
            ingredient.Metadata = metadata;
            ingredient.quantity = quantity;
            ingredient.OnValidate();
            return ingredient;
        }
    }
}