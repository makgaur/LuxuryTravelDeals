using System.ComponentModel.DataAnnotations;

namespace HiTours.TBO.Models
{
    public class Fare
    {
        [Required]
        [Display(Name = "Base Fare")]
        public decimal BaseFare { get; set; }

        [Required]
        [Display(Name = "Tax")]
        public decimal Tax { get; set; }

        [Required]
        [Display(Name = "TransactionFee")]
        public decimal TransactionFee { get; set; }

        [Required]
        [Display(Name = "YQTax")]
        public decimal YQTax { get; set; }

        [Required]
        [Display(Name = "Additional Transaction Fee Offered")]
        public decimal AdditionalTxnFeeOfrd { get; set; }

        [Required]
        [Display(Name = "Additional Transaction Fee Published")]
        public decimal AdditionalTxnFeePub { get; set; }

        [Required]
        [Display(Name = "Air Trans Fee")]
        public decimal AirTransFee { get; set; }

        [Display(Name = "ChargeBU")]
        public ChargeBU[] ChargeBU { get; set; }

        [Display(Name = "CommissionEarned")]
        public double CommissionEarned { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "Discount")]
        public long Discount { get; set; }

        [Display(Name = "IncentiveEarned")]
        public double IncentiveEarned { get; set; }

        [Display(Name = "OfferedFare")]
        public double OfferedFare { get; set; }

        [Display(Name = "OtherCharges")]
        public long OtherCharges { get; set; }

        [Display(Name = "PGCharge")]
        public long PGCharge { get; set; }

        [Display(Name = "PLBEarned")]
        public double PLBEarned { get; set; }

        [Display(Name = "PublishedFare")]
        public long PublishedFare { get; set; }

        [Display(Name = "ServiceFee")]
        public long ServiceFee { get; set; }

        [Display(Name = "TaxBreakup")]
        public ChargeBU[] TaxBreakup { get; set; }

        [Display(Name = "TdsOnCommission")]
        public double TdsOnCommission { get; set; }

        [Display(Name = "TdsOnIncentive")]
        public double TdsOnIncentive { get; set; }

        [Display(Name = "TdsOnPLB")]
        public double TdsOnPLB { get; set; }

        [Display(Name = "TotalBaggageCharges")]
        public long TotalBaggageCharges { get; set; }

        [Display(Name = "TotalMealCharges")]
        public long TotalMealCharges { get; set; }

        [Display(Name = "TotalSeatCharges")]
        public long TotalSeatCharges { get; set; }

        [Display(Name = "TotalSpecialServiceCharges")]
        public long TotalSpecialServiceCharges { get; set; }
    }
}