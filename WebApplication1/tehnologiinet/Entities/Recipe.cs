namespace tehnologiinet.Entities;

public class Recipe
{
    public long Id { get; set; }
    public long ResultItemId { get; set; }
    public Item ResultItem { get; set; }
    public double Value { get; set; }
    public ICollection<Ingredient> Ingredients { get; set; }
}
