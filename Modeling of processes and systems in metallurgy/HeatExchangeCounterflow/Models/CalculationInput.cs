using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace HeatExchangeCounterflow.Models;

public class CalculationInput
{
    public int Id { get; set; }

    [Required]
    public string LayerHeightRaw { get; set; } = "";

    public double LayerHeight =>
        double.Parse(LayerHeightRaw.Replace(',', '.'), CultureInfo.InvariantCulture);

    public double InitialMaterialTemp { get; set; }
    public double InitialGasTemp { get; set; }

    public double GasVelocity { get; set; }
    public double GasHeatCapacity { get; set; }

    public double MaterialFlow { get; set; }
    public double MaterialHeatCapacity { get; set; }

    public double AlphaV { get; set; }
    public double ApparatusDiameter { get; set; }
}
