






namespace allspice.Repositories;

public class RecipesRepository
{

    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal void DeleteRecipe(int recipeId)
    {
        string sql = "DELETE FROM recipes WHERE id = @recipeId LIMIT 1;";
        int changed = _db.Execute(sql, new { recipeId });
        if(changed == 0)throw new Exception("None deleted");
        if(changed > 1)throw new Exception("YOU DELETED TOO WELL!!");
    }

    internal List<Recipe> GetAllRecipes()
    {
        string sql = @"
        SELECT 
        recipes.*,
        accounts.*
        FROM recipes
        JOIN accounts On accounts.id = recipes.creatorId;";
        List<Recipe> recipes = _db.Query<Recipe, Profile, Recipe>(sql, JoinCreator).ToList();

        return (recipes);
    }

    internal Recipe GetRecipeById(int recipeId)
    {
        string sql = @"
        SELECT
        recipes.*,
        accounts.*
        FROM recipes
        JOIN accounts On accounts.id = recipes.creatorId
        WHERE recipes.id = @recipeId;";

        Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, JoinCreator, new { recipeId }).FirstOrDefault();
        return recipe;
    }

    internal Recipe PostRecipe(Recipe recipeData)
    {
        string sql = @"
        INSERT INTO 
        recipes(title, instructions, img, category, creatorId)
        VALUES(@title, @instructions, @img, @category, @creatorId);
        
        SELECT 
        recipes.*,
        accounts.*
        FROM recipes
        JOIN accounts On accounts.id = recipes.creatorId
        WHERE recipes.id = LAST_INSERT_ID();";

        Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, JoinCreator, recipeData).FirstOrDefault();
        return recipe;

    }

    internal Recipe UpdateRecipe(Recipe recipeData)
    {
        string sql = @"
        UPDATE 
        recipes
        SET
        title = @title,
        instructions = @instructions,
        img = @img,
        category = @category
        WHERE recipes.id = @id;

        SELECT 
        recipes.*,
        accounts.*
        FROM recipes
        JOIN accounts On accounts.id = recipes.creatorId
        WHERE recipes.id = @id;";

        Recipe recipe = _db.Query<Recipe, Profile, Recipe>(sql, JoinCreator, recipeData).FirstOrDefault();
        return recipe;
    }

    private Recipe JoinCreator(Recipe recipe, Profile profile)
    {
        recipe.creator = profile;
        return (recipe);
    }
}