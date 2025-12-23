using HeatExchangeCounterflow.Models;

namespace HeatExchangeCounterflow.Services;

public class HeatExchangeService : IHeatExchangeService
{
    public List<LayerPoint> Calculate(CalculationInput p)
    {
        var list = new List<LayerPoint>();

        double S = Math.PI * Math.Pow(p.ApparatusDiameter / 2, 2);

        double m =
            (p.MaterialFlow * p.MaterialHeatCapacity) /
            (p.GasVelocity * S * p.GasHeatCapacity);

        double Y0 =
            (p.AlphaV * p.LayerHeight) /
            (p.GasVelocity * p.GasHeatCapacity * 1000);

        double denominator = 1 - m * Math.Exp(((m - 1) * Y0) / m);

        for (double y = 0; y <= p.LayerHeight + 1e-6; y += 0.5)
        {
            double Y =
                (p.AlphaV * y) /
                (p.GasVelocity * p.GasHeatCapacity * 1000);

            double expVal = Math.Exp(((m - 1) * Y) / m);

            double theta1 = (1 - expVal) / denominator;
            double theta2 = (1 - m * expVal) / denominator;

            double tm =
                p.InitialMaterialTemp +
                (p.InitialGasTemp - p.InitialMaterialTemp) * theta1;

            double tg =
                p.InitialMaterialTemp +
                (p.InitialGasTemp - p.InitialMaterialTemp) * theta2;

            list.Add(new LayerPoint
            {
                CalculationId = p.Id, // ВАЖНО
                Y = Math.Round(y, 1),
                RelativeY = Math.Round(Y, 2),
                Theta1 = Math.Round(1 - expVal, 4),
                Theta2 = Math.Round(1 - m * expVal, 4),
                ThetaMaterial = Math.Round(theta1, 4),
                ThetaGas = Math.Round(theta2, 4),
                MaterialTemp = Math.Round(tm, 2),
                GasTemp = Math.Round(tg, 2),
                DeltaTemp = Math.Round(Math.Abs(tm - tg), 2)
            });

        }

        return list;
    }
}
