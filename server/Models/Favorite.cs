namespace allspice.Models;

public class Favorite
{
    public int id { get; set; }
    public DateTime createdAt { get; set; }
    public DateTime updatedAt { get; set; }
    public int recipeId { get; set; }
    public string accountId { get; set; }
}
