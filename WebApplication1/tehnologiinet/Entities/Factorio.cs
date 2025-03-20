namespace tehnologiinet.Entities;

using System.ComponentModel.DataAnnotations;

public class Factorio
{
    public long Id { get; set; }
    [Required]
    public string Item { get; set; }
    [Required]
    public double Value { get; set; }
    [Required]
    public string Unit { get; set; }
    [Required]
    public string Time { get; set; }
}