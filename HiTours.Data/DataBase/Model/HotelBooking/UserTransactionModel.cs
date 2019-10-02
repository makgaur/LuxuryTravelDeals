// <copyright file="UserTransactionModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    /// UserTransactionModel
    /// </summary>
    public class UserTransactionModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the row no.
        /// </summary>
        /// <value>
        /// The row no.
        /// </value>
        [Key]
        public long RowNo { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the hotel booking identifier.
        /// </summary>
        /// <value>
        /// The hotel booking identifier.
        /// </value>
        public Guid? HotelBookingId { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets or sets the receipt no.
        /// </summary>
        /// <value>
        /// The receipt no.
        /// </value>
        public string ReceiptNo { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the payment identifier.
        /// </summary>
        /// <value>
        /// The payment identifier.
        /// </value>
        public string PaymentId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is international.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is international; otherwise, <c>false</c>.
        /// </value>
        public bool IsInternational { get; set; }

        /// <summary>
        /// Gets or sets the payment method.
        /// </summary>
        /// <value>
        /// The payment method.
        /// </value>
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the card identifier.
        /// </summary>
        /// <value>
        /// The card identifier.
        /// </value>
        public string CardId { get; set; }

        /// <summary>
        /// Gets or sets the bank.
        /// </summary>
        /// <value>
        /// The bank.
        /// </value>
        public string Bank { get; set; }

        /// <summary>
        /// Gets or sets the name of the wallet.
        /// </summary>
        /// <value>
        /// The name of the wallet.
        /// </value>
        public string WalletName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the contact no.
        /// </summary>
        /// <value>
        /// The contact no.
        /// </value>
        public string ContactNo { get; set; }

        /// <summary>
        /// Gets or sets the fee.
        /// </summary>
        /// <value>
        /// The fee.
        /// </value>
        public decimal? Fee { get; set; }

        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        public decimal? Tax { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        /// <value>
        /// The error description.
        /// </value>
        public string ErrorDescription { get; set; }

        /// <summary>
        /// Gets or sets the payment date.
        /// </summary>
        /// <value>
        /// The payment date.
        /// </value>
        public DateTime? PaymentDate { get; set; }

        /// <summary>
        /// Gets or sets the payment status.
        /// </summary>
        /// <value>
        /// The payment status.
        /// </value>
        public string PaymentStatus { get; set; }

        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        /// <value>
        /// The order status.
        /// </value>
        public string OrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the flight detail.
        /// </summary>
        /// <value>
        /// The flight detail.
        /// </value>
        public string FlightDetail { get; set; }

        /// <summary>
        /// Gets or sets the flight booking.
        /// </summary>
        /// <value>
        /// The flight booking.
        /// </value>
        public ICollection<FlightBookingModel> FlightBooking { get; set; }
    }
}