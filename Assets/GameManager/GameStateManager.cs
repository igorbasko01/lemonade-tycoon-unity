using System.Collections;
using System.Collections.Generic;
using baskorp.Calendars.Runtime;
using baskorp.DayEnd.Runtime;
using baskorp.IngredientsBuyers.Runtime;
using baskorp.IngredientsCatalog.Runtime;
using baskorp.IngredientsInventory.Runtime;
using baskorp.Recipes.Runtime;
using baskorp.Wallets.Runtime;
using baskorp.Weather.Runtime;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public Calendar Calendar { get; private set; }
    public DayEndSystem DayEndSystem { get; private set; }
    public IngredientsBuyer IngredientsBuyer { get; private set; }
    public IngredientsCatalogManager IngredientsCatalog { get; private set; }
    public IngredientsInventoryManager IngredientsInventory { get; private set; }
    public Recipe Recipe { get; private set; }
    public Wallet Wallet { get; private set; }
    public WeatherForecaster Weather { get; private set; }

    private RecipeEvaluator recipeEvaluator;

    [SerializeField] private List<SellableIngredient> sellableIngredients;
    [SerializeField] private Recipe initialRecipe;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        Calendar = new Calendar();
        IngredientsCatalog = new IngredientsCatalogManager(sellableIngredients);
        IngredientsInventory = new IngredientsInventoryManager();
        IngredientsBuyer = new IngredientsBuyer();
        Recipe = initialRecipe;
        Weather = new();
        var dayTypeEvaluatorStrategy = new DayTypeRecipeEvaluatorStrategy();
        var weatherRecipeEvaluatorStrategy = new WeatherRecipeEvaluatorStrategy(Weather);
        recipeEvaluator = new(new() {(0.5f, dayTypeEvaluatorStrategy), (0.5f, weatherRecipeEvaluatorStrategy)});
        Wallet = new Wallet(10);
        DayEndSystem = new DayEndSystem(recipeEvaluator, IngredientsCatalog, IngredientsInventory, 100);
    }
}
