using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PNI.Data;
using PNI.Entities;
using PNI.Services;
using PNI.Models;

namespace PNI.Controllers
{
    public class UnitsController : Controller
    {
        /*private readonly DataContext _context;*/
        private readonly IPNIServices _services;
        public UnitsController(IPNIServices services)
        {
            //_context = context;
            _services = services;
        }

        // GET: Units
        public IActionResult Index()
        {
            return View(_services.ListUnits());
        }

        //GET: Units/Details/5
        public IActionResult Details(int id)
        {
            UnitViewModel unit = _services.Unit(id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // GET: Units/Create
        public IActionResult Create()
        {
            Unit unit = new Unit();
            PopulateCompany();
            return View(unit);
        }

        // POST: Units/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ID,BuyersID,UnitPurchased,LoanAmount,Terms,LoanDate,PaymentStart,InterestRate")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                _services.AddUnit(unit);
                return RedirectToAction(nameof(Index));
            }
            return View(unit);
        }

        // GET: Units/Edit/5
        public IActionResult Edit(int id)
        {
            var unit = _services.Unit(id);
            if (unit == null)
            {
                return NotFound();
            }
            return View(unit);
        }

        // POST: Units/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,BuyersID,UnitPurchased,LoanAmount,Terms,LoanDate,PaymentStart,InterestRate")] UnitViewModel unitviewmodel)
        {
            if (id != unitviewmodel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Unit unit = new Unit();
                    unit.ID = unitviewmodel.ID;
                    unit.UnitPurchased = unitviewmodel.UnitPurchased;
                    unit.LoanAmount = unitviewmodel.LoanAmount;
                    unit.Terms = unitviewmodel.Terms;
                    unit.LoanDate = unitviewmodel.LoanDate;
                    unit.PaymentStart = unitviewmodel.PaymentStart;
                    unit.InterestRate = unitviewmodel.InterestRate;

                    _services.UpdateUnit(unit);
                }
                catch
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(unitviewmodel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BuildAmortization(int id, [Bind("ID,BuyersID,UnitPurchased,LoanAmount,Terms,LoanDate,PaymentStart,InterestRate")] UnitViewModel unitviewmodel)
        {
            _services.ListAmortization(id, unitviewmodel);
            return RedirectToAction("Details", new { id = id });
            //return RedirectToAction("Details", id);

        }

        //// GET: Units/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var unit = await _context.Unit
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (unit == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(unit);
        //}

        //// POST: Units/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var unit = await _context.Unit.FindAsync(id);
        //    _context.Unit.Remove(unit);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool UnitExists(int id)
        //{
        //    return _context.Unit.Any(e => e.ID == id);
        //}

        private void PopulateCompany()
        {
            List<SelectListItem> list = _services.Company();
            ViewBag.CompanyList = list;
        }

        private Buyers Buyer(int? Id)
        {
            Buyers buyer = new Buyers();
            if (Id != null)
            {
                buyer = _services.Buyer(Id);
            }
            return buyer;
        }
    }
}
