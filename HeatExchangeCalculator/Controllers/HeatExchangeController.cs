using Microsoft.AspNetCore.Mvc;
using HeatExchangeCalculator.Models;
using HeatExchangeCalculator.Services;

namespace HeatExchangeCalculator.Controllers
{
    public class HeatExchangeController : Controller
    {
        private readonly ICalculationService _calculationService;

        public HeatExchangeController(ICalculationService calculationService)
        {
            _calculationService = calculationService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Calculate");
        }

        [HttpGet]
        public IActionResult Calculate()
        {
            var model = new HeatExchangeModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Calculate(HeatExchangeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = _calculationService.Calculate(model);
            return View("Calculate", result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCalculation(HeatExchangeModel model, string calculationName)
        {
            if (!ModelState.IsValid)
            {
                return View("Calculate", model);
            }

            if (string.IsNullOrEmpty(calculationName))
            {
                calculationName = $"Расчет от {DateTime.Now:dd.MM.yyyy HH:mm}";
            }

            _calculationService.SaveCalculation(model, calculationName);
            return RedirectToAction("SavedCalculations");
        }

        [HttpGet]
        public IActionResult SavedCalculations()
        {
            var calculations = _calculationService.GetSavedCalculations();
            return View(calculations);
        }

        [HttpGet]
        public IActionResult LoadCalculation(int id)
        {
            var model = _calculationService.LoadCalculation(id);
            if (model == null)
            {
                return NotFound();
            }

            var result = _calculationService.Calculate(model);
            return View("Calculate", result);
        }

        [HttpGet]
        public IActionResult DeleteCalculation(int id)
        {
            _calculationService.DeleteCalculation(id);
            return RedirectToAction("SavedCalculations");
        }

        [HttpGet]
        public IActionResult ExportToExcel(int id)
        {
            var model = _calculationService.LoadCalculation(id);
            if (model == null)
            {
                return NotFound();
            }

            var result = _calculationService.Calculate(model);
            // Здесь будет код экспорта в Excel
            return Content("Экспорт в Excel (функция в разработке)");
        }

        [HttpGet]
        public IActionResult About()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClearAll()
        {
            _calculationService.ClearAllCalculations();
            return RedirectToAction("SavedCalculations");
        }
    }
}