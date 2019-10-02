// <copyright file="HotelPriceModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// HotelPriceModel
    /// </summary>
    public class HotelPriceModel
    {
        /// <summary>
        /// Gets or sets the hotel price identifier.
        /// </summary>
        /// <value>
        /// The hotel price identifier.
        /// </value>
        [Key]
        public Guid HotelPriceId { get; set; }

        /// <summary>
        /// Gets or sets the accommodation identifier.
        /// </summary>
        /// <value>
        /// The accommodation identifier.
        /// </value>
        public Guid AccommodationID { get; set; }

        /// <summary>
        /// Gets or sets the market identifier.
        /// </summary>
        /// <value>
        /// The market identifier.
        /// </value>
        public Guid MarketId { get; set; }

        /// <summary>
        /// Gets or sets the validity from.
        /// </summary>
        /// <value>
        /// The validity from.
        /// </value>
        public DateTime ValidityFrom { get; set; }

        /// <summary>
        /// Gets or sets the validity to.
        /// </summary>
        /// <value>
        /// The validity to.
        /// </value>
        public DateTime ValidityTo { get; set; }

        /// <summary>
        /// Gets or sets the type of the room.
        /// </summary>
        /// <value>
        /// The type of the room.
        /// </value>
        public Guid RoomType { get; set; }

        /// <summary>
        /// Gets or sets the break fast wt.
        /// </summary>
        /// <value>
        /// The break fast wt.
        /// </value>
        public double BreakFastWT { get; set; }

        /// <summary>
        /// Gets or sets the lunch wt.
        /// </summary>
        /// <value>
        /// The lunch wt.
        /// </value>
        public double LunchWT { get; set; }

        /// <summary>
        /// Gets or sets the dinner wt.
        /// </summary>
        /// <value>
        /// The dinner wt.
        /// </value>
        public double DinnerWT { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is meal service tax applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is meal service tax applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsMealServiceTaxApplicable { get; set; }

        /// <summary>
        /// Gets or sets the meal service tax.
        /// </summary>
        /// <value>
        /// The meal service tax.
        /// </value>
        public double MealServiceTax { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is meal vat applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is meal vat applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsMealVATApplicable { get; set; }

        /// <summary>
        /// Gets or sets the meal vat.
        /// </summary>
        /// <value>
        /// The meal vat.
        /// </value>
        public double MealVAT { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is meal other tax applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is meal other tax applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsMealOtherTaxApplicable { get; set; }

        /// <summary>
        /// Gets or sets the meal other tax.
        /// </summary>
        /// <value>
        /// The meal other tax.
        /// </value>
        public double MealOtherTax { get; set; }

        /// <summary>
        /// Gets or sets the break fast.
        /// </summary>
        /// <value>
        /// The break fast.
        /// </value>
        public double BreakFast { get; set; }

        /// <summary>
        /// Gets or sets the lunch.
        /// </summary>
        /// <value>
        /// The lunch.
        /// </value>
        public double Lunch { get; set; }

        /// <summary>
        /// Gets or sets the dinner.
        /// </summary>
        /// <value>
        /// The dinner.
        /// </value>
        public double Dinner { get; set; }

        /// <summary>
        /// Gets or sets the type of the plan.
        /// </summary>
        /// <value>
        /// The type of the plan.
        /// </value>
        public Guid PlanType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [calculate discounted by].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [calculate discounted by]; otherwise, <c>false</c>.
        /// </value>
        public bool CalculateDiscountedBy { get; set; }

        /// <summary>
        /// Gets or sets the discounted by.
        /// </summary>
        /// <value>
        /// The discounted by.
        /// </value>
        public double DiscountedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [calculate service charge].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [calculate service charge]; otherwise, <c>false</c>.
        /// </value>
        public bool CalculateServiceCharge { get; set; }

        /// <summary>
        /// Gets or sets the service charge.
        /// </summary>
        /// <value>
        /// The service charge.
        /// </value>
        public double ServiceCharge { get; set; }

        /// <summary>
        /// Gets or sets the sales markup.
        /// </summary>
        /// <value>
        /// The sales markup.
        /// </value>
        public double SalesMarkup { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HotelPriceModel"/> is monday.
        /// </summary>
        /// <value>
        ///   <c>true</c> if monday; otherwise, <c>false</c>.
        /// </value>
        public bool Monday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HotelPriceModel"/> is tuesday.
        /// </summary>
        /// <value>
        ///   <c>true</c> if tuesday; otherwise, <c>false</c>.
        /// </value>
        public bool Tuesday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HotelPriceModel"/> is wednesday.
        /// </summary>
        /// <value>
        ///   <c>true</c> if wednesday; otherwise, <c>false</c>.
        /// </value>
        public bool Wednesday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HotelPriceModel"/> is thursday.
        /// </summary>
        /// <value>
        ///   <c>true</c> if thursday; otherwise, <c>false</c>.
        /// </value>
        public bool Thursday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HotelPriceModel"/> is friday.
        /// </summary>
        /// <value>
        ///   <c>true</c> if friday; otherwise, <c>false</c>.
        /// </value>
        public bool Friday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HotelPriceModel"/> is saterday.
        /// </summary>
        /// <value>
        ///   <c>true</c> if saterday; otherwise, <c>false</c>.
        /// </value>
        public bool Saterday { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HotelPriceModel"/> is sunday.
        /// </summary>
        /// <value>
        ///   <c>true</c> if sunday; otherwise, <c>false</c>.
        /// </value>
        public bool Sunday { get; set; }

        /// <summary>
        /// Gets or sets the currency identifier.
        /// </summary>
        /// <value>
        /// The currency identifier.
        /// </value>
        public Guid CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the sequence number.
        /// </summary>
        /// <value>
        /// The sequence number.
        /// </value>
        public short SequenceNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [calculate ta con TDS].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [calculate ta con TDS]; otherwise, <c>false</c>.
        /// </value>
        public bool CalculateTAConTDS { get; set; }

        /// <summary>
        /// Gets or sets the tac.
        /// </summary>
        /// <value>
        /// The tac.
        /// </value>
        public double TAC { get; set; }

        /// <summary>
        /// Gets or sets the TDS.
        /// </summary>
        /// <value>
        /// The TDS.
        /// </value>
        public double TDS { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rate increase by per.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rate increase by per; otherwise, <c>false</c>.
        /// </value>
        public bool IsRateIncreaseByPer { get; set; }

        /// <summary>
        /// Gets or sets the rate increase.
        /// </summary>
        /// <value>
        /// The rate increase.
        /// </value>
        public double RateIncrease { get; set; }

        /// <summary>
        /// Gets or sets the fit double rate increase.
        /// </summary>
        /// <value>
        /// The fit double rate increase.
        /// </value>
        public double FITDoubleRateIncrease { get; set; }

        /// <summary>
        /// Gets or sets the fit extra bed rate increase.
        /// </summary>
        /// <value>
        /// The fit extra bed rate increase.
        /// </value>
        public double FITExtraBedRateIncrease { get; set; }

        /// <summary>
        /// Gets or sets the git single rate increase.
        /// </summary>
        /// <value>
        /// The git single rate increase.
        /// </value>
        public double GITSingleRateIncrease { get; set; }

        /// <summary>
        /// Gets or sets the git double rate increase.
        /// </summary>
        /// <value>
        /// The git double rate increase.
        /// </value>
        public double GITDoubleRateIncrease { get; set; }

        /// <summary>
        /// Gets or sets the git extra bed rate increase.
        /// </summary>
        /// <value>
        /// The git extra bed rate increase.
        /// </value>
        public double GITExtraBedRateIncrease { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is rack rate.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is rack rate; otherwise, <c>false</c>.
        /// </value>
        public bool IsRackRate { get; set; }

        /// <summary>
        /// Gets or sets the break fast child.
        /// </summary>
        /// <value>
        /// The break fast child.
        /// </value>
        public double BreakFastChild { get; set; }

        /// <summary>
        /// Gets or sets the lunch child.
        /// </summary>
        /// <value>
        /// The lunch child.
        /// </value>
        public double LunchChild { get; set; }

        /// <summary>
        /// Gets or sets the dinner child.
        /// </summary>
        /// <value>
        /// The dinner child.
        /// </value>
        public double DinnerChild { get; set; }

        /// <summary>
        /// Gets or sets the break fast child wt.
        /// </summary>
        /// <value>
        /// The break fast child wt.
        /// </value>
        public double BreakFastChildWT { get; set; }

        /// <summary>
        /// Gets or sets the lunch child wt.
        /// </summary>
        /// <value>
        /// The lunch child wt.
        /// </value>
        public double LunchChildWT { get; set; }

        /// <summary>
        /// Gets or sets the dinner child wt.
        /// </summary>
        /// <value>
        /// The dinner child wt.
        /// </value>
        public double DinnerChildWT { get; set; }

        /// <summary>
        /// Gets or sets the maximum no of room per day.
        /// </summary>
        /// <value>
        /// The maximum no of room per day.
        /// </value>
        public int? MaxNoOfRoomPerDay { get; set; }
    }
}