// Controllers/InsureeController.cs
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarInsurance.Models;

// Namespace for the MVC application
namespace CarInsurance.Controllers
{
    // Controller for handling insuree-related requests
    public class InsureeController : Controller
    {
        // Constant for maximum title length
        private const int MaxTitleLength = 100;
        private readonly InsuranceEntities _context;

        // Constructor with dependency injection
        public InsureeController(InsuranceEntities context)
        {
            _context = context;
        }

        // Validates title length
        private void ValidateTitle(string title)
        {
            if (title.Length > MaxTitleLength)
            {
                throw new ArgumentException($"Title exceeds {MaxTitleLength} characters.");
            }
        }

        // GET: Insuree - Displays list of insurees
        public async Task<IActionResult> Index()
        {
            try
            {
                var title = "Insurees";
                ValidateTitle(title);
                ViewData["Title"] = title;
                return View(await _context.Insurees.ToListAsync());
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = ex.Message });
            }
        }

        // GET: Insuree/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentException("ID cannot be null.");
                }
                var insuree = await _context.Insurees.FirstOrDefaultAsync(m => m.Id == id);
                if (insuree == null)
                {
                    throw new ArgumentException("Insuree not found.");
                }
                var title = "Insuree Details";
                ValidateTitle(title);
                ViewData["Title"] = title;
                return View(insuree);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = ex.Message });
            }
        }

        // GET: Insuree/Create
        public IActionResult Create()
        {
            try
            {
                var title = "Create Insuree";
                ValidateTitle(title);
                ViewData["Title"] = title;
                return View();
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = ex.Message });
            }
        }

        // POST: Insuree/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType")] Insuree insuree)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Calculate quote
                    insuree.Quote = CalculateQuote(insuree);
                    _context.Add(insuree);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                var title = "Create Insuree";
                ValidateTitle(title);
                ViewData["Title"] = title;
                return View(insuree);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = ex.Message });
            }
        }

        // GET: Insuree/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentException("ID cannot be null.");
                }
                var insuree = await _context.Insurees.FindAsync(id);
                if (insuree == null)
                {
                    throw new ArgumentException("Insuree not found.");
                }
                var title = "Edit Insuree";
                ValidateTitle(title);
                ViewData["Title"] = title;
                return View(insuree);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = ex.Message });
            }
        }

        // POST: Insuree/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType")] Insuree insuree)
        {
            try
            {
                if (id != insuree.Id)
                {
                    throw new ArgumentException("ID mismatch.");
                }
                if (ModelState.IsValid)
                {
                    insuree.Quote = CalculateQuote(insuree);
                    _context.Update(insuree);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                var title = "Edit Insuree";
                ValidateTitle(title);
                ViewData["Title"] = title;
                return View(insuree);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = ex.Message });
            }
        }

        // GET: Insuree/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    throw new ArgumentException("ID cannot be null.");
                }
                var insuree = await _context.Insurees.FirstOrDefaultAsync(m => m.Id == id);
                if (insuree == null)
                {
                    throw new ArgumentException("Insuree not found.");
                }
                var title = "Delete Insuree";
                ValidateTitle(title);
                ViewData["Title"] = title;
                return View(insuree);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = ex.Message });
            }
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var insuree = await _context.Insurees.FindAsync(id);
                if (insuree != null)
                {
                    _context.Insurees.Remove(insuree);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = ex.Message });
            }
        }

        // GET: Insuree/Admin - Displays all quotes for admin
        public async Task<IActionResult> Admin()
        {
            try
            {
                var title = "Admin - All Quotes";
                ValidateTitle(title);
                ViewData["Title"] = title;
                var insurees = await _context.Insurees
                    .Select(i => new { i.FirstName, i.LastName, i.EmailAddress, i.Quote })
                    .ToListAsync();
                return View(insurees);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = HttpContext.TraceIdentifier, ErrorMessage = ex.Message });
            }
        }

        // Calculates the insurance quote based on assignment rules
        private decimal CalculateQuote(Insuree insuree)
        {
            const decimal baseRate = 50m;
            var quote = baseRate;

            // Age-based adjustments
            var age = DateTime.Now.Year - insuree.DateOfBirth.Year;
            if (insuree.DateOfBirth > DateTime.Now.AddYears(-age)) age--;
            if (age <= 18)
                quote += 100m;
            else if (age >= 19 && age <= 25)
                quote += 50m;
            else if (age >= 26)
                quote += 25m;

            // Car year adjustments
            if (insuree.CarYear < 2000 || insuree.CarYear > 2015)
                quote += 25m;

            // Car make and model adjustments
            if (insuree.CarMake?.Equals("Porsche", StringComparison.OrdinalIgnoreCase) == true)
            {
                quote += 25m;
                if (insuree.CarModel?.Equals("911 Carrera", StringComparison.OrdinalIgnoreCase) == true)
                    quote += 25m;
            }

            // Speeding tickets
            quote += insuree.SpeedingTickets * 10m;

            // DUI adjustment
            if (insuree.DUI)
                quote *= 1.25m;

            // Coverage type adjustment
            if (insuree.CoverageType)
                quote *= 1.5m;

            return Math.Round(quote, 2);
        }
    }
}