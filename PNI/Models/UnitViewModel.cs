using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PNI.Entities;

namespace PNI.Models
{
    public class UnitViewModel
    {
        public UnitViewModel()
        {
            BuyersID = 0;
            Name = "";
            Address = "";
            UnitPurchased = "";
            LoanAmount = 0;
            Terms = 24;
            LoanDate = DateTime.Now;
            PaymentStart = DateTime.Now.AddDays(30);
            InterestRate = decimal.Parse("7.5");
            LstAmortization = new List<Amortization>();
        }

        public int ID { get; set; }
        [Required]
        public int BuyersID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string UnitPurchased { get; set; }
        [Required]
        public decimal LoanAmount { get; set; }
        [Required]
        public int Terms { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LoanDate { get; set; }
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime PaymentStart { get; set; }
        [Required]
        public decimal InterestRate { get; set; }
        public List<Amortization> LstAmortization { get; set; }
    }
}
