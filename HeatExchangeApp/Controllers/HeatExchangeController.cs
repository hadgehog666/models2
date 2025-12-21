using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HeatExchangeApp.Models;
using HeatExchangeApp.Services;
using HeatExchangeApp.Data;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HeatExchangeApp.Controllers
{
    public class HeatExchangeController : Controller
    {
        private readonly HeatExchangeService _calculationService;
        private readonly ApplicationDbContext _context;

        public HeatExchangeController(HeatExchangeService calculationService, ApplicationDbContext context)
        {
            _calculationService = calculationService;
            _context = context;
        }

        // Главная страница с формой ввода
        public IActionResult Index()
        {
            return View(new HeatExchangeModel());
        }

        // Выполнение расчета
        [HttpPost]
        public IActionResult Calculate(HeatExchangeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var result = _calculationService.Calculate(model);
            return View("Results", result);
        }

        // Сохранение расчета
        [HttpPost]
        public async Task<IActionResult> SaveCalculation([FromBody] SaveCalculationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Название обязательно");
            }

            var savedCalculation = new SavedCalculation
            {
                Name = request.Name,
                ParametersJson = JsonConvert.SerializeObject(request.Parameters),
                ResultsJson = JsonConvert.SerializeObject(request.Results),
                CreatedDate = DateTime.Now
            };

            _context.SavedCalculations.Add(savedCalculation);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, id = savedCalculation.Id });
        }

        // Просмотр сохраненных расчетов
        public async Task<IActionResult> SavedCalculations()
        {
            var calculations = await _context.SavedCalculations
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();

            return View(calculations);
        }

        // Загрузка сохраненного расчета
        public async Task<IActionResult> LoadCalculation(int id)
        {
            var calculation = await _context.SavedCalculations.FindAsync(id);
            if (calculation == null)
            {
                return NotFound();
            }

            var parameters = JsonConvert.DeserializeObject<HeatExchangeModel>(calculation.ParametersJson);
            var results = JsonConvert.DeserializeObject<CalculationResult>(calculation.ResultsJson);

            ViewBag.IsLoaded = true;
            return View("Results", results);
        }

        // Удаление сохраненного расчета
        [HttpPost]
        public async Task<IActionResult> DeleteCalculation(int id)
        {
            var calculation = await _context.SavedCalculations.FindAsync(id);
            if (calculation != null)
            {
                _context.SavedCalculations.Remove(calculation);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("SavedCalculations");
        }
    }

    public class SaveCalculationRequest
    {
        public string Name { get; set; } = string.Empty;
        public HeatExchangeModel Parameters { get; set; } = new();
        public CalculationResult Results { get; set; } = new();
    }
}