using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PNI.Models
{
    public class AmortizationViewModel
    {
        public AmortizationViewModel()
        {
            UnitID = 0;
            Date = DateTime.Now;
            Principal = 0;
            Interest = 0;
            Total = 0;
            Balance = 0;
            LoanAmount = 0;
            NoOfDays = 0;
        }

        public int ID { get; set; }

        public int UnitID { get; set; }

        public DateTime Date { get; set; }

        public decimal Principal { get; set; }

        public decimal Interest { get; set; }

        public decimal Total { get; set; }

        public decimal Balance { get; set; }

        public decimal LoanAmount { get; set; }

        public int NoOfDays { get; set; }
    }
}
