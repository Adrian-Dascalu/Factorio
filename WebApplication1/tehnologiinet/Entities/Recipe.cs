namespace tehnologiinet.Entities;

public class Recipe
{
    public long Id { get; set; }
    public double Value { get; set; }
    public long ItemId { get; set; }
    public Item Item { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; }
}
