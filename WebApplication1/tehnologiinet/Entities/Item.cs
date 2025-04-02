namespace tehnologiinet.Entities;

public class Item
{
    public long Id { get; set; }
    public string Name { get; set; }
    public double Value { get; set; }
    
    public Recipe Recipe { get; set; }
    public long? RecipeId { get; set; }
}
