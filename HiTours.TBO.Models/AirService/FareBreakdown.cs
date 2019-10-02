using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class FareBreakdown
    {
        [Display(Name = "Additional TxnFee Of RD")]
        public long AdditionalTxnFeeOfrd { get; set; }

        [Display(Name = "Additional TxnFee")]
        public long AdditionalTxnFee { get; set; }

        [Display(Name = "Additional TxnFee Pub")]
        public double AdditionalTxnFeePub { get; set; }

        [Display(Name = "Base Fare")]
        public long BaseFare { get; set; }

        [Display(Name = "Currency")]
        public string Currency { get; set; }

        [Display(Name = "PG Charge")]
        public long PGCharge { get; set; }

        [Display(Name = "Passenger Count")]
        public long PassengerCount { get; set; }

        [Display(Name = "Passenger Type")]
        public long PassengerType { get; set; }

        [Display(Name = "Tax")]
        public long Tax { get; set; }

        [Display(Name = "YQTax")]
        public long YQTax { get; set; }

        public int MarkupPrice { get; set; }
    }
}
