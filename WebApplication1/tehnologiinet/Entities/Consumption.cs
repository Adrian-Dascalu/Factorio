namespace tehnologiinet.Entities;

public class Consumption
{
    public long Id { get; set; }
    public double Value { get; set; }
    public string Unit { get; set; }
    public string Time { get; set; }
    public Items Item { get; set; }
}