using UnityEngine;

namespace baskorp.IngredientsCatalog.Runtime 
{
    [CreateAssetMenu(fileName = "New Ingredient Metadata", menuName = "Ingredients Catalog/Ingredient Metadata")]
    public class IngredientMetadata : ScriptableObject
    {
        public string Name;
        public string State;
        public Sprite Icon;
    }
}