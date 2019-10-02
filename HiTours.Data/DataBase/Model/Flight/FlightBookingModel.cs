// <copyright file="FlightBookingModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    using HiTours.Core;
    using HiTours.Models;

    /// <summary>
    /// FlightBooking
    /// </summary>
    /// <seealso cref="HiTours.Core.BaseModel" />
    public class FlightBookingModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the row no.
        /// </summary>
        /// <value>
        /// The row no.
        /// </value>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RowNo { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the tbo booking identifier.
        /// </summary>
        /// <value>
        /// The tbo booking identifier.
        /// </value>
        public long? TboBookingId { get; set; }

        /// <summary>
        /// Gets or sets the booking date.
        /// </summary>
        /// <value>
        /// The booking date.
        /// </value>
        public DateTime? BookingDate { get; set; }

        /// <summary>
        /// Gets or sets the PNR.
        /// </summary>
        /// <value>
        /// The PNR.
        /// </value>
        public string Pnr { get; set; }

        /// <summary>
        /// Gets or sets the origin.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public string Origin { get; set; }

        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>
        /// The destination.
        /// </value>
        public string Destination { get; set; }

        /// <summary>
        /// Gets or sets the user transaction identifier.
        /// </summary>
        /// <value>
        /// The user transaction identifier.
        /// </value>
        public long UserTransactionId { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        public string Remark { get; set; }

        /////// <summary>
        /////// Gets or sets the user transaction.
        /////// </summary>
        /////// <value>
        /////// The user transaction.
        /////// </value>
        ////public UserTransactionModel UserTransaction { get; set; }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public string Response { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; set; }
    }
}