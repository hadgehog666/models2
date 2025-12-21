namespace HeatExchangeCalculator.Models
{
    public class CalculationViewModel
    {
        public HeatExchangeModel InputData { get; set; } = new();
        public List<CalculationResult> Results { get; set; } = new();

        // Промежуточные параметры
        public double M { get; set; } // Отношение теплоемкостей
        public double Y0 { get; set; } // Полная относительная высота
        public double Denominator { get; set; } // Знаменатель в формулах
        public double CrossSection { get; set; } // Площадь сечения
    }
}