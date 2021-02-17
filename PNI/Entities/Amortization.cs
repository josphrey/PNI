using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PNI.Entities
{
    public class Amortization
    {
        public int ID { get; set; }
        [Required]
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
