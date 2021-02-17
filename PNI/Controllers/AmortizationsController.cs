using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PNI.Data;
using PNI.Entities;

namespace PNI.Controllers
{
    public class AmortizationsController : Controller
    {
        private readonly DataContext _context;

        public AmortizationsController(DataContext context)
        {
            _context = context;
        }

        // GET: Amortizations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Amortization.ToListAsync());
        }

        // GET: Amortizations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amortization = await _context.Amortization
                .FirstOrDefaultAsync(m => m.ID == id);
            if (amortization == null)
            {
                return NotFound();
            }

            return View(amortization);
        }

        // GET: Amortizations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Amortizations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UnitID,Date,Principal,Interest,Total,Balance,LoanAmount,NoOfDays,isActive")] Amortization amortization)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amortization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(amortization);
        }

        // GET: Amortizations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amortization = await _context.Amortization.FindAsync(id);
            if (amortization == null)
            {
                return NotFound();
            }
            return View(amortization);
        }

        // POST: Amortizations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UnitID,Date,Principal,Interest,Total,Balance,LoanAmount,NoOfDays,isActive")] Amortization amortization)
        {
            if (id != amortization.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amortization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmortizationExists(amortization.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(amortization);
        }

        // GET: Amortizations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var amortization = await _context.Amortization
                .FirstOrDefaultAsync(m => m.ID == id);
            if (amortization == null)
            {
                return NotFound();
            }

            return View(amortization);
        }

        // POST: Amortizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var amortization = await _context.Amortization.FindAsync(id);
            _context.Amortization.Remove(amortization);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AmortizationExists(int id)
        {
            return _context.Amortization.Any(e => e.ID == id);
        }
    }
}
