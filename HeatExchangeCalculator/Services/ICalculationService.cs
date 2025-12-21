using HeatExchangeCalculator.Models;

namespace HeatExchangeCalculator.Services
{
    public interface ICalculationService
    {
        CalculationViewModel Calculate(HeatExchangeModel model);
        List<SavedCalculation> GetSavedCalculations();
        void SaveCalculation(HeatExchangeModel model, string name);
        HeatExchangeModel? LoadCalculation(int id);
        void DeleteCalculation(int id);
        void ClearAllCalculations();
    }
}
