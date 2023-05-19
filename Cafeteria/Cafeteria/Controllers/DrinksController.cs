using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cafeteria.Data;
using Cafeteria.Models;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis;

namespace Cafeteria.Controllers
{
    public class DrinksController : Controller
    {
        private readonly CafeteriaContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DrinksController(CafeteriaContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Drinks
        public async Task<IActionResult> Index()
        {
            return _context.Drinks != null ? 
                         View(await _context.Drinks.ToListAsync()) :
                         Problem("Entity set 'CafeteriaContext.Drinks'  is null.");
        }

        // GET: Drinks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Drinks == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks
                .FirstOrDefaultAsync(m => m.DrinkId == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // GET: Drinks/Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Drinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DrinkId,DrinkName,Price,CalculationUnit,Image,Description,CreatedAt,UpdatedAt,IsActive,CategoryId")] Drink drink, IFormFile formFile)
        {
            if (ModelState.IsValid)
            {
                drink.DrinkId = Guid.NewGuid();

                // Upload images.
                string fileName = Path.GetFileName(formFile.FileName);
                string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);
                var stream = new FileStream(uploadpath, FileMode.Create);
                await formFile.CopyToAsync(stream);
                ViewBag.Message = "Tải lên thành công!";
                ViewBag.ImageURL = "img\\" + fileName;
                var path = "/img/" + fileName;

                drink.Image = path;
                _context.Add(drink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(drink);
        }

        // GET: Drinks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Drinks == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            ViewBag.CategoryId = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            return View(drink);
        }

        // POST: Drinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DrinkId,DrinkName,Price,CalculationUnit,Image,Description,CreatedAt,UpdatedAt,IsActive,CategoryId")] Drink drink, IFormFile formFile)
        {
            if (id != drink.DrinkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Upload images.
                    string fileName = Path.GetFileName(formFile.FileName);
                    string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", fileName);
                    var stream = new FileStream(uploadpath, FileMode.Create);
                    await formFile.CopyToAsync(stream);
                    ViewBag.Message = "Tải lên thành công!";
                    ViewBag.ImageURL = "img\\" + fileName;
                    var path = "/img/" + fileName;
                    drink.Image = path;
                    _context.Update(drink);
                    await _context.SaveChangesAsync();
                    return View("Index", await _context.Drinks.ToListAsync());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrinkExists(drink.DrinkId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(drink);
        }

        // GET: Drinks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Drinks == null)
            {
                return NotFound();
            }

            var drink = await _context.Drinks
                .FirstOrDefaultAsync(m => m.DrinkId == id);
            if (drink == null)
            {
                return NotFound();
            }

            return View(drink);
        }

        // POST: Drinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Drinks == null)
            {
                return Problem("Entity set 'CafeteriaContext.Drinks'  is null.");
            }
            var drink = await _context.Drinks.FindAsync(id);
            if (drink != null)
            {
                _context.Drinks.Remove(drink);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DrinkExists(Guid id)
        {
          return (_context.Drinks?.Any(e => e.DrinkId == id)).GetValueOrDefault();
        }
    }
}
