using System.ComponentModel.DataAnnotations;

namespace HeatExchangeCounterflow.Models;

public class Calculation
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public CalculationInput Input { get; set; } = new();

    public List<LayerPoint> Points { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
