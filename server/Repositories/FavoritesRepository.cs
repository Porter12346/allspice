




namespace allspice.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal FavoriteRecipe createFavorite(Favorite favoriteData)
    {
        string sql = @"
        INSERT INTO
        favorites( accountId, recipeId )
        VALUES( @accountId, @recipeId ) ;
        
        SELECT
        favorites.*,
        accounts.*,
        recipes.*
        FROM favorites
        JOIN accounts ON accounts.id = favorites.accountId
        JOIN recipes ON recipes.id = favorites.recipeId
        WHERE favorites.id = LAST_INSERT_ID();";

        FavoriteRecipe favoriteRecipe = _db.Query<Favorite, Profile, FavoriteRecipe, FavoriteRecipe>(sql, JoinVirtuals, favoriteData).FirstOrDefault();
        return favoriteRecipe;
    }

    internal void deleteFavorite(int favoriteId)
    {
        string sql = "DELETE FROM favorites WHERE id = @favoriteId LIMIT 1;";

        int changed = _db.Execute(sql, new { favoriteId });
        if(changed > 1)throw new Exception("DELETED MORE THAN ONE!");
        if(changed == 0)throw new Exception("Nothing deleted");
    }

    internal Favorite GetFavoriteById(int id)
    {
        string sql = @"
        SELECT
        *
        FROM favorites
        WHERE favorites.id = @id;";

        Favorite favorite = _db.Query<Favorite>(sql, new {id}).FirstOrDefault();
        return favorite;
    }

    internal List<FavoriteRecipe> getFavoritesByAccount(string userId)
    {
        string sql = @"
        SELECT
        favorites.*,
        accounts.*,
        recipes.*
        FROM favorites
        JOIN accounts ON accounts.id = favorites.accountId
        JOIN recipes ON recipes.id = favorites.recipeId
        WHERE favorites.accountId = @userId;";

        List<FavoriteRecipe> favoriteRecipes = _db.Query<Favorite, Profile, FavoriteRecipe, FavoriteRecipe>(sql, JoinVirtuals, new{userId}).ToList();
        return favoriteRecipes;
    }

    private FavoriteRecipe JoinVirtuals(Favorite favorite, Profile profile, FavoriteRecipe recipe)
    {
        recipe.favoriteId = favorite.id;
        recipe.creator = profile;
        return recipe;
    }
}
