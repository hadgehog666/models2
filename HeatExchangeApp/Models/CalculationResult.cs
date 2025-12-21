using System.Collections.Generic;
using System;

namespace HeatExchangeApp.Models
{
    public class CalculationResult
    {
        public HeatExchangeModel Parameters { get; set; } = new();
        public List<CalculationPoint> Points { get; set; } = new();
        public double HeatCapacityRatio { get; set; }  // m
        public double FullRelativeHeight { get; set; } // Y0
        public double Denominator { get; set; }        // 1 - m*exp(...)

        public class CalculationPoint
        {
            public double Y { get; set; }              // Координата, м
            public double RelativeHeight { get; set; } // Y
            public double MaterialTemp { get; set; }   // t, °C
            public double GasTemp { get; set; }        // T, °C
            public double TempDifference { get; set; } // Δt, °C
            public double Theta { get; set; }          // θ
            public double Phi { get; set; }            // φ
        }
    }
}