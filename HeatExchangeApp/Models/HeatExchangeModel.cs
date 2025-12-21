using System.ComponentModel.DataAnnotations;


namespace HeatExchangeApp.Models
{
    public class HeatExchangeModel
    {
        [Required]
        [Display(Name = "Высота слоя, м")]
        public double Height { get; set; } = 6;

        [Required]
        [Display(Name = "Начальная температура материала, °C")]
        public double InitialMaterialTemp { get; set; } = 15;

        [Required]
        [Display(Name = "Начальная температура газа, °C")]
        public double InitialGasTemp { get; set; } = 600;

        [Required]
        [Display(Name = "Скорость газа, м/с")]
        public double GasVelocity { get; set; } = 0.73;

        [Required]
        [Display(Name = "Теплоемкость газа, кДж/(м³·К)")]
        public double GasHeatCapacity { get; set; } = 1.09;

        [Required]
        [Display(Name = "Расход материалов, кг/с")]
        public double MaterialFlow { get; set; } = 1.68;

        [Required]
        [Display(Name = "Теплоемкость материалов, кДж/(кг·К)")]
        public double MaterialHeatCapacity { get; set; } = 1.49;

        [Required]
        [Display(Name = "Коэффициент теплоотдачи, Вт/(м³·К)")]
        public double HeatTransferCoefficient { get; set; } = 2440;

        [Required]
        [Display(Name = "Диаметр аппарата, м")]
        public double ApparatusDiameter { get; set; } = 2.2;

        [Display(Name = "Название расчета")]
        public string? CalculationName { get; set; }
    }
}