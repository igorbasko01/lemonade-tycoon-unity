using System.Collections.Generic;
using System.Linq;
using baskorp.Calendars.Runtime;

namespace baskorp.Recipes.Runtime
{
    public class RecipeEvaluator
    {
        /// <summary>
        /// List of strategies and their weights.
        /// </summary>
        private readonly List<(float, IRecipeEvaluatorStrategy)> strategies;
        private readonly float totalWeights;
        
        public RecipeEvaluator(List<(float, IRecipeEvaluatorStrategy)> strategies)
        {
            this.strategies = strategies.Where(s => s.Item1 > 0).ToList();
            totalWeights = this.strategies.Select(s => s.Item1).Sum();
        }

        /// <summary>
        /// Evaluates a recipe based on the strategies and their weights.
        /// </summary>
        /// <param name="recipe">The recipe to evaluate</param>
        /// <param name="date">The date at which this recipe is going to be consumed</param>
        /// <returns>The combined evaluation of the strategies</returns>
        public float Evaluate(Recipe recipe, Date date)
        {
            float result = 0.0f;
            foreach (var (weight, strategy) in strategies)
            {
                result += weight/totalWeights * strategy.Evaluate(recipe, date);
            }

            return result;
        }
    }
}