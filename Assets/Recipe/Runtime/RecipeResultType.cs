using System.Collections.Generic;
using System.Linq;
using baskorp.IngredientsCatalog.Runtime;

namespace baskorp.Recipes.Runtime
{
    public enum RecipeResultType
    {
        Success,
        InvalidQuantity
    }

    public class RecipeResult
    {
        public RecipeResultType ResultType { get; private set; }
        public QuantifiableIngredient ProducedIngredient { get; private set; }
        public List<QuantifiableIngredient> MissingIngredients { get; private set; }

        public RecipeResult(RecipeResultType resultType, QuantifiableIngredient producedIngredient = null, List<QuantifiableIngredient> missingIngredients = null)
        {
            ResultType = resultType;
            ProducedIngredient = producedIngredient;
            MissingIngredients = missingIngredients ?? new List<QuantifiableIngredient>();
        }

        public override bool Equals(object obj)
        {   
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            var other = (RecipeResult) obj;
            return ResultType == other.ResultType 
            && ProducedIngredient == other.ProducedIngredient 
            && MissingIngredients.All(other.MissingIngredients.Contains);
        }
        
        public override int GetHashCode()
        {
            return ResultType.GetHashCode() ^ ProducedIngredient.GetHashCode() ^ MissingIngredients.GetHashCode();
        }
    }
}