namespace tehnologiinet.Entities;

public class Recipe
{
    public long Id { get; set; }
    public long ItemId { get; set; }
    public long Amount { get; set; } // amount of the item produced by this recipe
    public Item Item { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; }
}
