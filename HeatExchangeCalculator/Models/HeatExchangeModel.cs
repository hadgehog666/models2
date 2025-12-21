using System.ComponentModel.DataAnnotations;

namespace HeatExchangeCalculator.Models
{
    public class HeatExchangeModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Высота слоя обязательна")]
        [Range(0.1, 100, ErrorMessage = "Высота слоя должна быть от 0.1 до 100 м")]
        [Display(Name = "Высота слоя, м")]
        public double Height { get; set; } = 6;

        [Required(ErrorMessage = "Начальная температура материала обязательна")]
        [Display(Name = "Начальная температура материала, °C")]
        public double InitialMaterialTemp { get; set; } = 15;

        [Required(ErrorMessage = "Начальная температура газа обязательна")]
        [Display(Name = "Начальная температура газа, °C")]
        public double InitialGasTemp { get; set; } = 600;

        [Required(ErrorMessage = "Скорость газа обязательна")]
        [Range(0.01, 10, ErrorMessage = "Скорость газа должна быть от 0.01 до 10 м/с")]
        [Display(Name = "Скорость газа, м/с")]
        public double GasVelocity { get; set; } = 0.73;

        [Required(ErrorMessage = "Теплоемкость газа обязательна")]
        [Range(0.1, 5, ErrorMessage = "Теплоемкость газа должна быть от 0.1 до 5 кДж/(м³·К)")]
        [Display(Name = "Теплоемкость газа, кДж/(м³·К)")]
        public double GasHeatCapacity { get; set; } = 1.09;

        [Required(ErrorMessage = "Расход материала обязателен")]
        [Range(0.1, 100, ErrorMessage = "Расход материала должен быть от 0.1 до 100 кг/с")]
        [Display(Name = "Расход материала, кг/с")]
        public double MaterialConsumption { get; set; } = 1.68;

        [Required(ErrorMessage = "Теплоемкость материала обязательна")]
        [Range(0.1, 5, ErrorMessage = "Теплоемкость материала должна быть от 0.1 до 5 кДж/(кг·К)")]
        [Display(Name = "Теплоемкость материала, кДж/(кг·К)")]
        public double MaterialHeatCapacity { get; set; } = 1.49;

        [Required(ErrorMessage = "Коэффициент теплоотдачи обязателен")]
        [Range(100, 10000, ErrorMessage = "Коэффициент теплоотдачи должен быть от 100 до 10000 Вт/(м³·К)")]
        [Display(Name = "Коэффициент теплоотдачи, Вт/(м³·К)")]
        public double HeatTransferCoefficient { get; set; } = 2440;

        [Required(ErrorMessage = "Диаметр аппарата обязателен")]
        [Range(0.1, 10, ErrorMessage = "Диаметр аппарата должен быть от 0.1 до 10 м")]
        [Display(Name = "Диаметр аппарата, м")]
        public double ApparatusDiameter { get; set; } = 2.2;

        [Display(Name = "Название расчета")]
        [StringLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
        public string CalculationName { get; set; } = "Новый расчет";

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}