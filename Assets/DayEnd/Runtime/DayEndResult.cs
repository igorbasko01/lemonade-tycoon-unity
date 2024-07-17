using baskorp.Calendars.Runtime;
using baskorp.Recipes.Runtime;

namespace baskorp.DayEnd.Runtime
{
    public class DayEndResult
    {
        Date date;
        Recipe recipe;
        float recipePrice;
        float amountSold;
        float totalIncome;

        public Date Date => date;
        public Recipe Recipe => recipe;
        public float RecipePrice => recipePrice;
        public float AmountSold => amountSold;
        public float TotalIncome => totalIncome;
        
        public DayEndResult(Date date, Recipe recipe, float recipePrice, float amountSold, float totalIncome)
        {
            this.date = date;
            this.recipe = recipe;
            this.recipePrice = recipePrice;
            this.amountSold = amountSold;
            this.totalIncome = totalIncome;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            var other = (DayEndResult)obj;
            return date.Equals(other.date) && recipe.Equals(other.recipe) && recipePrice.Equals(other.recipePrice) && amountSold.Equals(other.amountSold) && totalIncome.Equals(other.totalIncome);
        }

        public override int GetHashCode()
        {
            return date.GetHashCode() ^ recipe.GetHashCode() ^ recipePrice.GetHashCode() ^ amountSold.GetHashCode() ^ totalIncome.GetHashCode();
        }
    }
}