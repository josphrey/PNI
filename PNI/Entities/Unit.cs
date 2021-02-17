using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PNI.Entities
{
    public class Unit
    {
        public Unit()
        {
            BuyersID = 0;
            UnitPurchased = "";
            LoanAmount = 0;
            Terms = 24;
            LoanDate = DateTime.Now;
            PaymentStart = DateTime.Now.AddDays(30);
            InterestRate = decimal.Parse("7.5");
        }

        public int ID { get; set; }
        [Required]
        public int BuyersID { get; set; }
        [Required]
        public string UnitPurchased { get; set; }
        [Required]
        [Range(0, 9999999999999999.99)]
        public decimal LoanAmount { get; set; }
        [Required]
        public int Terms { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LoanDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PaymentStart { get; set; }
        [Required]
        public decimal InterestRate { get; set; }
    }
}
