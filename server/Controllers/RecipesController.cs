namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class RecipesController : ControllerBase
{

    private readonly RecipesService _recipesService;
    private readonly Auth0Provider _auth0Provider;
    private readonly IngredientsService _ingredientsService;

    public RecipesController(RecipesService recipesService, Auth0Provider auth0Provider, IngredientsService ingredientsService)
    {
        _recipesService = recipesService;
        _auth0Provider = auth0Provider;
        _ingredientsService = ingredientsService;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Recipe>> PostRecipe([FromBody] Recipe recipeData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            recipeData.creatorId = userInfo.Id;
            Recipe recipe = _recipesService.PostRecipe(recipeData);
            return recipe;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }


    }

    [HttpGet]

    public ActionResult<List<Recipe>> GetAllRecipes()
    {
        try
        {
            List<Recipe> recipes = _recipesService.GetAllRecipes();
            return (recipes);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{recipeId}")]

    public ActionResult<Recipe> GetRecipeById(int recipeId)
    {
        try
        {
            Recipe recipe = _recipesService.GetRecipeById(recipeId);
            return (recipe);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{recipeId}")]
    [Authorize]

    public async Task<ActionResult<Recipe>> UpdateRecipe([FromBody] Recipe recipeData, int recipeId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            recipeData.id = recipeId;
            recipeData.creatorId = userInfo.Id;
            Recipe recipe = _recipesService.UpdateRecipe(recipeData);
            return (recipe);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{recipeId}")]
    [Authorize]

    public async Task<ActionResult<String>> DeleteRecipe(int recipeId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            _recipesService.DeleteRecipe(recipeId, userInfo.Id);
            return ("deleted");
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet(("{recipeId}/ingredients"))]

    public ActionResult<List<Ingredient>> GetRecipeIngredients(int recipeId)
    {
        try
        {
            List<Ingredient> ingredients = _ingredientsService.GetRecipeIngredients(recipeId);
            return ingredients;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}