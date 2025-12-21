using System;
using System.Collections.Generic;
using System.Linq;
using HeatExchangeCalculator.Models;

namespace HeatExchangeCalculator.Services
{
    public class CalculationService : ICalculationService
    {
        private readonly List<SavedCalculation> _savedCalculations = new();
        private int _nextId = 1;

        public CalculationViewModel Calculate(HeatExchangeModel model)
        {
            var viewModel = new CalculationViewModel { InputData = model };

            // Площадь поперечного сечения аппарата
            viewModel.CrossSection = Math.PI * Math.Pow(model.ApparatusDiameter / 2, 2);

            // Отношение теплоемкостей потоков (m)
            viewModel.M = (model.MaterialConsumption * model.MaterialHeatCapacity) /
                         (model.GasVelocity * viewModel.CrossSection * model.GasHeatCapacity);

            // Полная относительная высота слоя (Y0)
            // Умножаем на 1000 для перевода кДж в Дж
            viewModel.Y0 = (model.HeatTransferCoefficient * viewModel.CrossSection * model.Height) /
                          (model.GasVelocity * viewModel.CrossSection * model.GasHeatCapacity * 1000);

            // Знаменатель в формулах
            viewModel.Denominator = 1 - viewModel.M * Math.Exp((viewModel.M - 1) * viewModel.Y0 / viewModel.M);

            // Расчет для точек по высоте
            int steps = (int)Math.Ceiling(model.Height);
            double stepSize = model.Height / steps;

            for (int i = 0; i <= steps; i++)
            {
                double heightCoordinate = i * stepSize;

                // Относительная высота для текущей точки
                double Y = (model.HeatTransferCoefficient * heightCoordinate) /
                          (model.GasVelocity * model.GasHeatCapacity * 1000);

                // Экспоненциальный член
                double expTerm = Math.Exp((viewModel.M - 1) * Y / viewModel.M);

                // Относительные температуры
                double phi = (1 - expTerm) / viewModel.Denominator;  // ϑ - материал
                double theta = (1 - viewModel.M * expTerm) / viewModel.Denominator;  // θ - газ

                // Абсолютные температуры
                double materialTemp = model.InitialMaterialTemp +
                    (model.InitialGasTemp - model.InitialMaterialTemp) * phi;
                double gasTemp = model.InitialMaterialTemp +
                    (model.InitialGasTemp - model.InitialMaterialTemp) * theta;

                var result = new CalculationResult
                {
                    HeightCoordinate = Math.Round(heightCoordinate, 1),
                    Y = Math.Round(Y, 2),
                    Phi = Math.Round(phi, 3),
                    Theta = Math.Round(theta, 3),
                    MaterialTemperature = Math.Round(materialTemp, 1),
                    GasTemperature = Math.Round(gasTemp, 1),
                    TemperatureDifference = Math.Round(gasTemp - materialTemp, 1)
                };

                viewModel.Results.Add(result);
            }

            return viewModel;
        }

        public List<SavedCalculation> GetSavedCalculations()
        {
            return _savedCalculations.OrderByDescending(c => c.Date).ToList();
        }

        public void SaveCalculation(HeatExchangeModel model, string name)
        {
            var saved = new SavedCalculation
            {
                Id = _nextId++,
                Name = string.IsNullOrEmpty(name) ?
                    $"Расчет от {DateTime.Now:dd.MM.yyyy HH:mm}" : name,
                Date = DateTime.Now,
                Data = model
            };

            _savedCalculations.Add(saved);
        }

        public HeatExchangeModel? LoadCalculation(int id)
        {
            var saved = _savedCalculations.FirstOrDefault(s => s.Id == id);
            return saved?.Data;
        }

        public void DeleteCalculation(int id)
        {
            var saved = _savedCalculations.FirstOrDefault(s => s.Id == id);
            if (saved != null)
                _savedCalculations.Remove(saved);
        }

        public void ClearAllCalculations()
        {
            _savedCalculations.Clear();
            _nextId = 1;
        }
    }
}