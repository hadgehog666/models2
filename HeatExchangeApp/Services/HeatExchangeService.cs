using HeatExchangeApp.Models;
using System.Collections.Generic;
using System;

namespace HeatExchangeApp.Services
{
    public class HeatExchangeService
    {
        public CalculationResult Calculate(HeatExchangeModel parameters)
        {
            var result = new CalculationResult
            {
                Parameters = parameters
            };

            // Площадь сечения аппарата
            double S = Math.PI * Math.Pow(parameters.ApparatusDiameter / 2, 2);

            // Отношение теплоемкостей потоков (m)
            double numerator = parameters.MaterialFlow * parameters.MaterialHeatCapacity;
            double denominator = parameters.GasVelocity * S * parameters.GasHeatCapacity;
            double m = numerator / denominator;
            result.HeatCapacityRatio = m;

            // Полная относительная высота слоя (Y0)
            double Y0 = (parameters.HeatTransferCoefficient * S * parameters.Height) /
                       (parameters.GasVelocity * S * parameters.GasHeatCapacity * 1000);
            result.FullRelativeHeight = Y0;

            // Знаменатель
            double denominatorValue = 1 - m * Math.Exp(-(1 - m) * Y0 / m);
            result.Denominator = denominatorValue;

            // Генерация точек расчета (каждые 0.5 метра)
            result.Points = new List<CalculationResult.CalculationPoint>();
            double step = 0.5;
            int pointsCount = (int)(parameters.Height / step);

            for (int i = 0; i <= pointsCount; i++)
            {
                double y = i * step;
                if (y > parameters.Height) y = parameters.Height;

                // Относительная высота для точки
                double Y = (parameters.HeatTransferCoefficient * S * y) /
                          (parameters.GasVelocity * S * parameters.GasHeatCapacity * 1000);

                // Вычисление φ и θ
                double phiNumerator = 1 - Math.Exp((m - 1) * Y / m);
                double thetaNumerator = 1 - m * Math.Exp((m - 1) * Y / m);

                double phi = phiNumerator / denominatorValue;
                double theta = thetaNumerator / denominatorValue;

                // Температуры
                double t = parameters.InitialMaterialTemp +
                          (parameters.InitialGasTemp - parameters.InitialMaterialTemp) * phi;
                double T = parameters.InitialMaterialTemp +
                          (parameters.InitialGasTemp - parameters.InitialMaterialTemp) * theta;

                result.Points.Add(new CalculationResult.CalculationPoint
                {
                    Y = y,
                    RelativeHeight = Y,
                    MaterialTemp = Math.Round(t, 1),
                    GasTemp = Math.Round(T, 1),
                    TempDifference = Math.Round(T - t, 1),
                    Theta = Math.Round(theta, 3),
                    Phi = Math.Round(phi, 3)
                });
            }

            return result;
        }
    }
}
