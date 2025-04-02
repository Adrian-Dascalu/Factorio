namespace tehnologiinet.Entities;

public class Production
{
    public long Id { get; set; }
    public Item Item { get; set; }
    public int TotalQuantity { get; set; }
}