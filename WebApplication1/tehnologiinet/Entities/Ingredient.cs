namespace tehnologiinet.Entities;

public class Ingredient
{
    public long RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    
    public long IngredientItemId { get; set; }
    public Item IngredientItem { get; set; }
    
    public int Amount { get; set; }
}