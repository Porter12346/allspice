namespace allspice.Models;

public class ingredients
{
    public int id { get; set; }
    public DateTime createdAt { get; set; }

    public string name { get; set; }

    public string quantity { get; set; }

    public int recipeId { get; set; }
}