// <copyright file="ApiFare.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.FlightApi
{
    using System.Collections.Generic;

    /// <summary>
    /// ApiFare
    /// </summary>
    public class ApiFare
    {
        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the base fare.
        /// </summary>
        /// <value>
        /// The base fare.
        /// </value>
        public decimal BaseFare { get; set; }

        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        public decimal Tax { get; set; }

        /// <summary>
        /// Gets or sets the yq tax.
        /// </summary>
        /// <value>
        /// The yq tax.
        /// </value>
        public decimal YQTax { get; set; }

        /// <summary>
        /// Gets or sets the additional TXN fee o.
        /// </summary>
        /// <value>
        /// The additional TXN fee o.
        /// </value>
        public decimal AdditionalTxnFeeOfrd { get; set; }

        /// <summary>
        /// Gets or sets the additional TXN fee p.
        /// </summary>
        /// <value>
        /// The additional TXN fee p.
        /// </value>
        public decimal AdditionalTxnFeePub { get; set; }

        /// <summary>
        /// Gets or sets the other charges.
        /// </summary>
        /// <value>
        /// The other charges.
        /// </value>
        public decimal OtherCharges { get; set; }

        /// <summary>
        /// Gets or sets the charge bu.
        /// </summary>
        /// <value>
        /// The charge bu.
        /// </value>
        public List<ApiChargeBU> ChargeBU { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the published fare.
        /// </summary>
        /// <value>
        /// The published fare.
        /// </value>
        public decimal PublishedFare { get; set; }

        /// <summary>
        /// Gets or sets the commission earned.
        /// </summary>
        /// <value>
        /// The commission earned.
        /// </value>
        public decimal CommissionEarned { get; set; }

        /// <summary>
        /// Gets or sets the PLB earned.
        /// </summary>
        /// <value>
        /// The PLB earned.
        /// </value>
        public decimal PLBEarned { get; set; }

        /// <summary>
        /// Gets or sets the incentive earned.
        /// </summary>
        /// <value>
        /// The incentive earned.
        /// </value>
        public decimal IncentiveEarned { get; set; }

        /// <summary>
        /// Gets or sets the offered fare.
        /// </summary>
        /// <value>
        /// The offered fare.
        /// </value>
        public decimal OfferedFare { get; set; }

        /// <summary>
        /// Gets or sets the TDS on commission.
        /// </summary>
        /// <value>
        /// The TDS on commission.
        /// </value>
        public decimal TdsOnCommission { get; set; }

        /// <summary>
        /// Gets or sets the TDS on PLB.
        /// </summary>
        /// <value>
        /// The TDS on PLB.
        /// </value>
        public decimal TdsOnPLB { get; set; }

        /// <summary>
        /// Gets or sets the TDS on incentive.
        /// </summary>
        /// <value>
        /// The TDS on incentive.
        /// </value>
        public decimal TdsOnIncentive { get; set; }

        /// <summary>
        /// Gets or sets the service fee.
        /// </summary>
        /// <value>
        /// The service fee.
        /// </value>
        public decimal ServiceFee { get; set; }
    }
}