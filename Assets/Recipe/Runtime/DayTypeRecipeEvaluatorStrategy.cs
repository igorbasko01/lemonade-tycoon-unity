using baskorp.Calendars.Runtime;

namespace baskorp.Recipes.Runtime
{
    public class DayTypeRecipeEvaluatorStrategy : IRecipeEvaluatorStrategy
    {
        public float Evaluate(Recipe recipe, Date date)
        {
            return date.DayType == DayType.Weekday ? 0.5f : 1.0f;
        }
    }
}