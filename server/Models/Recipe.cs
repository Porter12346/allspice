namespace allspice.Models;

public class Recipe
{
    public int id { get; set; }

    public DateTime createdAt { get; set; }

    public DateTime updatedAt { get; set; }

    public string title { get; set; }

    public string instructions { get; set; }

    public string img { get; set; }

    public string category { get; set; }

    public string creatorId { get; set; }

    public Profile creator { get; set; }
}