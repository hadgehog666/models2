namespace HeatExchangeCalculator.Models
{
    public class SavedCalculation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public HeatExchangeModel Data { get; set; } = new();

        // Краткая информация для отображения в списке
        public string ShortInfo =>
            $"{Data.Height} м, материал: {Data.InitialMaterialTemp}°C → газ: {Data.InitialGasTemp}°C";
    }
}
