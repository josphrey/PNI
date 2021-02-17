using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PNI.Entities;
using PNI.Models;
using PNI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PNI.Services
{
    public class PNIServices : IPNIServices
    {
        private readonly DataContext _context;

        public PNIServices(DataContext context)
        {
            _context = context;
        }

        public List<UnitViewModel> ListUnits()
        {
            List<UnitViewModel> result = (from U in _context.Unit.ToList()
                                          join B in _context.Buyers on U.BuyersID equals B.ID
                                          select new UnitViewModel
                                          {
                                              ID = U.ID,
                                              BuyersID = U.BuyersID,
                                              Name = B.Name,
                                              Address = B.Address,
                                              UnitPurchased = U.UnitPurchased,
                                              LoanAmount = U.LoanAmount,
                                              Terms = U.Terms,
                                              LoanDate = U.LoanDate,
                                              PaymentStart = U.PaymentStart,
                                              InterestRate = U.InterestRate,
                                          }).ToList();

            return result;
        }

        public UnitViewModel Unit(int Id)
        {
            UnitViewModel result = ListUnits().Find(x => x.ID == Id);
            List<Amortization> lst = new List<Amortization>();
            lst = _context.Amortization.Where(x => x.UnitID == result.ID).ToList();
            result.LstAmortization = lst;

            return result;
        }

        public List<Amortization> ListAmortization(int id, UnitViewModel unitViewModel)
        {
            List<Amortization> lstAmortizations = new List<Amortization>();
            Dictionary<int, DateTime> dict = new Dictionary<int, DateTime>();
            DateTime dtLoadDate = unitViewModel.LoanDate;
            DateTime startdate = unitViewModel.PaymentStart;

            for (int i = 0; i < unitViewModel.Terms; i++)
            {
                if (i == 0)
                    dict.Add(i, startdate);
                else
                    dict.Add(i, new DateTime(startdate.AddMonths(i).Year, startdate.AddMonths(i).Month, startdate.Day));
            }

            for (int i = 0; i < dict.Count; i++)
            {
                Amortization am = new Amortization();
                //int dID = dict.ElementAt(i).Key;
                am.UnitID = unitViewModel.ID;
                am.LoanAmount = unitViewModel.LoanAmount;
                am.Date = dict.ElementAt(i).Value;

                TimeSpan difference = new TimeSpan();
                if (i == 0)
                {
                    difference = (dict.ElementAt(i).Value - dtLoadDate);
                    am.NoOfDays = difference.Days;
                    am.Principal = decimal.Round((unitViewModel.LoanAmount / unitViewModel.Terms), 2);
                    am.Interest = decimal.Round(((unitViewModel.LoanAmount * am.NoOfDays * unitViewModel.InterestRate / 365) / 100), 2);
                    am.Total = decimal.Round((am.Principal + am.Interest), 2);
                    am.Balance = decimal.Round((unitViewModel.LoanAmount - am.Principal), 2);
                }
                else
                {
                    difference = (dict.ElementAt(i).Value - dict.ElementAt((i - 1)).Value);
                    Amortization m = lstAmortizations.Find(x => x.UnitID == unitViewModel.ID && x.Date == dict.ElementAt((i - 1)).Value);
                    am.NoOfDays = difference.Days;

                    am.Principal = decimal.Round((unitViewModel.LoanAmount / unitViewModel.Terms), 2);
                    am.Interest = decimal.Round(((m.Balance * am.NoOfDays * unitViewModel.InterestRate / 365) / 100), 2);
                    am.Total = decimal.Round((am.Principal + am.Interest), 2);
                    am.Balance = decimal.Round((m.Balance - am.Principal), 2);
                }
                AddAmortization(am);
                lstAmortizations.Add(am);
            }

            return lstAmortizations;
        }

        public List<SelectListItem> Company()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            _context.Buyers.ToList().ForEach(x => list.Add(new SelectListItem(x.Name, x.ID.ToString())));
            return list;
        }

        public Buyers Buyer(int? Id)
        {
            Buyers buyer = new Buyers();
            if (Id != null)
            {
                buyer = _context.Buyers.Find(Id);
            }
            return buyer;
        }

        public void AddUnit(Unit unit)
        {
            if (unit != null)
            {
                _context.Unit.Add(unit);
                _context.SaveChanges();
            }
        }

        public void UpdateUnit(Unit unit)
        {
            if (unit != null)
            {
                _context.Unit.Update(unit);
                _context.SaveChanges();
            }
        }

        public void DeleteUnit(Unit unit)
        {
            if (unit != null)
            {
                _context.Unit.Remove(unit);
                _context.SaveChanges();
            }
        }

        public void AddAmortization(Amortization amortization)
        {
            if (amortization != null)
            {
                _context.Amortization.Add(amortization);
                _context.SaveChanges();
            }
        }
    }
    public interface IPNIServices
    {
        List<UnitViewModel> ListUnits();
        UnitViewModel Unit(int Id);
        List<SelectListItem> Company();
        Buyers Buyer(int? Id);
        List<Amortization> ListAmortization(int id, UnitViewModel unitViewModel);
        void AddUnit(Unit unit);
        void UpdateUnit(Unit unit);
        void DeleteUnit(Unit unit);
        void AddAmortization(Amortization amortization);
    }
}