public class LayerPoint
{
    public int Id { get; set; }

    public int CalculationId { get; set; }   // ← ВАЖНО

    public double Y { get; set; }
    public double RelativeY { get; set; }

    public double Theta1 { get; set; }
    public double Theta2 { get; set; }

    public double ThetaMaterial { get; set; }
    public double ThetaGas { get; set; }

    public double MaterialTemp { get; set; }
    public double GasTemp { get; set; }

    public double DeltaTemp { get; set; }
}
