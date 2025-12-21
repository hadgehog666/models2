namespace HeatExchangeCalculator.Models
{
    public class CalculationResult
    {
        public double Y { get; set; } // Относительная высота
        public double MaterialTemperature { get; set; } // Температура материала
        public double GasTemperature { get; set; } // Температура газа
        public double TemperatureDifference { get; set; } // Разность температур
        public double HeightCoordinate { get; set; } // Координата по высоте

        // Для графиков
        public double Theta { get; set; } // Относительная температура газа
        public double Phi { get; set; } // Относительная температура материала
    }
}
