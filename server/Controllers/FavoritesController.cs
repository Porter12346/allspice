namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]

public class FavoritesController : ControllerBase
{
    private readonly FavoritesService _favoritesService;
    private readonly Auth0Provider _auth0Provider;

    public FavoritesController(FavoritesService favoritesService, Auth0Provider auth0Provider)
    {
        _favoritesService = favoritesService;
        _auth0Provider = auth0Provider;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<FavoriteRecipe>> createFavorite([FromBody] Favorite favoriteData)
    {
        try
        {
            Profile userInfo = await _auth0Provider.GetUserInfoAsync<Profile>(HttpContext);
            favoriteData.accountId = userInfo.Id;
            FavoriteRecipe favorite = _favoritesService.createFavorite(favoriteData);
            return favorite;
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{favoriteId}")]
    [Authorize]

    public async Task<ActionResult<String>> deleteFavorite(int favoriteId)
    {
        try
        {
            Profile userInfo = await _auth0Provider.GetUserInfoAsync<Profile>(HttpContext);
            _favoritesService.deleteFavorite(favoriteId, userInfo.Id);
            return "Deleted Successfully";
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
