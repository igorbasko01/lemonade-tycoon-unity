using UnityEngine;

namespace baskorp.IngredientsCatalog.Runtime 
{
    [CreateAssetMenu(fileName = "New Ingredient Metadata", menuName = "Ingredients Catalog/Ingredient Metadata")]
    public class IngredientMetadata : ScriptableObject
    {
        public string Name;
        public string State;
        public Sprite Icon;

        public static IngredientMetadata Create(string name, string state = null, Sprite icon = null)
        {
            var ingredient = CreateInstance<IngredientMetadata>();
            ingredient.Name = name;
            ingredient.State = state;
            ingredient.Icon = icon;
            return ingredient;
        }
    }
}