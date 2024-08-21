
using Dapper;

namespace allspice.Repositories;

public class IngredientsRepository
{

    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal void DeleteIngredient(int ingredientId)
    {
        string sql = "DELETE FROM ingredients WHERE id = @ingredientId LIMIT 1;";

        int rowsAffected = _db.Execute(sql, new { ingredientId });
        if (rowsAffected == 0) throw new Exception("none deleted");
        if (rowsAffected > 1) throw new Exception("multiple deleted!!");
    }

    internal Ingredient GetIngredientById(int ingredientId)
    {
        string sql = @"
        SELECT
        *
        FROM ingredients
        WHERE ingredients.id = @ingredientId;";

        Ingredient ingredient = _db.Query<Ingredient>(sql, new { ingredientId }).FirstOrDefault();
        return ingredient;
    }

    internal List<Ingredient> GetRecipeIngredients(int recipeId)
    {
        string sql = @"
        SELECT 
        *
        FROM ingredients
        WHERE ingredients.recipeId = @recipeId;";

        List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
        return (ingredients);
    }

    internal Ingredient PostIngredient(Ingredient ingredientData, string userId)
    {
        string sql = @"
        INSERT INTO
        ingredients(name, quantity, recipeId)
        VALUES(@name, @quantity, @recipeId);
        
        SELECT 
        *
        FROM ingredients
        WHERE ingredients.id = LAST_INSERT_ID();";

        Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();
        return ingredient;
    }


}
