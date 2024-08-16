

namespace allspice.Services;

public class IngredientsService
{

    private readonly IngredientsRepository _ingredientsRepository;

    private readonly RecipesService _recipesService;

    public IngredientsService(IngredientsRepository ingredientsRepository, RecipesService recipesService)
    {
        _ingredientsRepository = ingredientsRepository;
        _recipesService = recipesService;
    }

    internal void DeleteIngredient(int ingredientId, string userId)
    {
        Ingredient ingredient = GetIngredientById(ingredientId);
        Recipe recipe = _recipesService.GetRecipeById(ingredient.recipeId);
        if(recipe.creatorId != userId)throw new Exception("You do not have permission to delete this ingredient!");
        _ingredientsRepository.DeleteIngredient(ingredientId);
    }

    private Ingredient GetIngredientById(int ingredientId)
    {
        Ingredient ingredient = _ingredientsRepository.GetIngredientById(ingredientId);
        if(ingredient == null)throw new Exception("ingredient not found with given id");
        return(ingredient);
    }

    internal List<Ingredient> GetRecipeIngredients(int recipeId)
    {
        List<Ingredient> ingredients = _ingredientsRepository.GetRecipeIngredients(recipeId);
        return(ingredients);
    }

    internal Ingredient PostIngredient(Ingredient ingredientData, string userId)
    {
        Recipe recipe = _recipesService.GetRecipeById(ingredientData.recipeId);
        if(recipe.creatorId != userId)throw new Exception("You cannot add an ingredient to a recipe you didnt make");
        Ingredient ingredient = _ingredientsRepository.PostIngredient(ingredientData, userId);
        return ingredient;
    }
}