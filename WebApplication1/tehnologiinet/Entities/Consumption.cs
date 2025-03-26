namespace tehnologiinet.Entities;

public class Consumption
{
    public long Id { get; set; }
    public double Value { get; set; }
    public Item Items { get; set; }
    public long ItemId { get; set; }
    public string ItemName { get; set; }
}