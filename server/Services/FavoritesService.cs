namespace allspice.Services;

public class FavoritesService
{
    private readonly FavoritesRepository _favoritesRepository;

    public FavoritesService(FavoritesRepository favoritesRepository)
    {
        _favoritesRepository = favoritesRepository;
    }

    internal FavoriteRecipe createFavorite(Favorite favoriteData)
    {
        FavoriteRecipe favorite = _favoritesRepository.createFavorite(favoriteData);
        return favorite;
    }

    internal Favorite GetFavoriteById(int id)
    {
        Favorite favorite = _favoritesRepository.GetFavoriteById(id);
        if (favorite == null) throw new Exception("no restaurant found with given id");
        return favorite;
    }

    internal void deleteFavorite(int favoriteId, string userId)
    {
        Favorite favorite = GetFavoriteById(favoriteId);
        if (favorite.accountId != userId) throw new Exception("This is not your favorite");
        _favoritesRepository.deleteFavorite(favoriteId);
    }

    internal List<FavoriteRecipe> getFavoritesByAccount(string userId)
    {
        List<FavoriteRecipe> favoriteRecipe = _favoritesRepository.getFavoritesByAccount(userId);
        return favoriteRecipe;
    }
}
