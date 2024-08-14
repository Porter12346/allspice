
using allspice.Repositories;


public class RecipesService
{

    private readonly RecipesRepository _recipesRepository;

    public RecipesService(RecipesRepository recipesRepository)
    {
        _recipesRepository = recipesRepository;
    }

    internal void DeleteRecipe(int recipeId, string userId)
    {
        Recipe recipeToUpdate = GetRecipeById(recipeId);
        
        if (recipeToUpdate.creatorId != userId) throw new Exception("YOU DIDNT MAKE THIS RECIPE!!!");

        _recipesRepository.DeleteRecipe(recipeId);
    }

    internal List<Recipe> GetAllRecipes()
    {
        List<Recipe> recipes = _recipesRepository.GetAllRecipes();
        return (recipes);
    }

    internal Recipe GetRecipeById(int recipeId)
    {
        Recipe recipe = _recipesRepository.GetRecipeById(recipeId);
        if(recipe == null)throw new Exception("No Recipe found");
        return (recipe);
    }

    internal Recipe PostRecipe(Recipe recipeData)
    {
        Recipe recipe = _recipesRepository.PostRecipe(recipeData);
        return (recipe);
    }

    internal Recipe UpdateRecipe(Recipe recipeData)
    {
        Recipe recipeToUpdate = GetRecipeById(recipeData.id);
        
        if (recipeToUpdate.creatorId != recipeData.creatorId) throw new Exception("YOU DIDNT MAKE THIS RECIPE!!!");
        recipeToUpdate.category = recipeData.category ?? recipeToUpdate.category;
        recipeToUpdate.title = recipeData.title ?? recipeToUpdate.title;
        recipeToUpdate.img = recipeData.img ?? recipeToUpdate.img;
        recipeToUpdate.instructions = recipeData.instructions ?? recipeToUpdate.instructions;
        Recipe recipe = _recipesRepository.UpdateRecipe(recipeToUpdate);
        return recipe;
    }
}