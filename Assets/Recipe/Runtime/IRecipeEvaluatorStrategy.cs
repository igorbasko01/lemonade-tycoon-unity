using baskorp.Calendars.Runtime;

namespace baskorp.Recipes.Runtime
{
    public interface IRecipeEvaluatorStrategy
    {
        float Evaluate(Recipe recipe, Date date);
    }
}