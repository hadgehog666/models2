using HeatExchangeCounterflow.Data;
using HeatExchangeCounterflow.Models;
using HeatExchangeCounterflow.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeatExchangeCounterflow.Controllers;

public class CalculationsController : Controller
{
    private readonly AppDbContext _db;
    private readonly IHeatExchangeService _service;

    public CalculationsController(AppDbContext db, IHeatExchangeService service)
    {
        _db = db;
        _service = service;
    }

    public IActionResult Index()
        => View(_db.Calculations.ToList());

    public IActionResult Create() => View(new Calculation());

    [HttpPost]
    public IActionResult Create(Calculation c)
    {
        if (!ModelState.IsValid) return View(c);

        c.Points = _service.Calculate(c.Input);

        foreach (var p in c.Points)
            p.CalculationId = c.Id;

        _db.Calculations.Add(c);
        _db.SaveChanges();

        return RedirectToAction("Details", new { id = c.Id });
    }

    public IActionResult Details(int id)
        => View(_db.Calculations
            .Include(x => x.Points)
            .First(x => x.Id == id));

    public IActionResult Edit(int id)
    {
        var calc = _db.Calculations
            .Include(c => c.Input)
            .FirstOrDefault(c => c.Id == id);

        if (calc == null)
            return NotFound();

        return View(calc);
    }


    [HttpPost]
    public IActionResult Edit(Calculation c)
    {
        var oldPoints = _db.LayerPoints
            .Where(p => p.CalculationId == c.Id);

        _db.LayerPoints.RemoveRange(oldPoints);

        c.Points = _service.Calculate(c.Input);

        foreach (var p in c.Points)
            p.CalculationId = c.Id;

        _db.Calculations.Update(c);
        _db.SaveChanges();

        return RedirectToAction("Details", new { id = c.Id });
    }



    public IActionResult Delete(int id)
    {
        var c = _db.Calculations.Find(id);
        if (c != null)
        {
            _db.Calculations.Remove(c);
            _db.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
