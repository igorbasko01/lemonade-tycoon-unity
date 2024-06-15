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

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }
            if (other.GetType() != GetType())
            {
                return false;
            }
            var otherIngredient = (IngredientMetadata)other;
            return Name == otherIngredient.Name && State == otherIngredient.State;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ State.GetHashCode();
        }
    }
}