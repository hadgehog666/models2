using HeatExchangeCounterflow.Models;

namespace HeatExchangeCounterflow.Services;

public interface IHeatExchangeService
{
    List<LayerPoint> Calculate(CalculationInput input);
}
