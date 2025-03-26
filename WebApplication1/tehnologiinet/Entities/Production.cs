namespace tehnologiinet.Entities;

public class Production
{
    public long Id { get; set; }
    public Item Items { get; set; }
    public string ItemName { get; set; }
    public long ItemId { get; set; }
    public int TotalQuantity { get; set; }
}