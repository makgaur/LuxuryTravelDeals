// <copyright file="SendLeadViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.Deals
{
    using System;
    using System.Collections.Generic;
    using HiTours.Core;

    /// <summary>
    ///  Vendor Package Relation Model
    /// </summary>
    public class SendLeadViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int dealId { get; set; }

        /// <summary>
        /// Gets or sets the First Name.
        /// </summary>
        /// <value>
        /// The First Name.
        /// </value>
        public string first_Name { get; set; }

        /// <summary>
        /// Gets or sets the Last Name.
        /// </summary>
        /// <value>
        /// The Last Name.
        /// </value>
        public string last_Name { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string email { get; set; }

        /// <summary>
        /// Gets or sets the mobile.
        /// </summary>
        /// <value>
        /// The mobile.
        /// </value>
        public string mobile { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string phone { get; set; }

        /// <summary>
        /// Gets or sets the no_Of_Pax.
        /// </summary>
        /// <value>
        /// The no_Of_Pax.
        /// </value>
        public int no_Of_Pax { get; set; }

        /// <summary>
        /// Gets or sets the no_Of_Nights.
        /// </summary>
        /// <value>
        /// The no_Of_Nights.
        /// </value>
        public int no_Of_Nights { get; set; }

        /// <summary>
        /// Gets or sets the no_of_Rooms.
        /// </summary>
        /// <value>
        /// The no_of_Rooms.
        /// </value>
        public int no_of_Rooms { get; set; }

        /// <summary>
        /// Gets or sets the no_of_Adult.
        /// </summary>
        /// <value>
        /// The no_of_Adult.
        /// </value>
        public int no_of_Adult { get; set; }

        /// <summary>
        /// Gets or sets the no_of_Children.
        /// </summary>
        /// <value>
        /// The no_of_Children.
        /// </value>
        public int no_of_Children { get; set; }

        /// <summary>
        /// Gets or sets the no_of_Infant.
        /// </summary>
        /// <value>
        /// The no_of_Infant.
        /// </value>
        public int no_of_Infant { get; set; }

        /// <summary>
        /// Gets or sets the data amount.
        /// </summary>
        /// <value>
        /// The data amount.
        /// </value>
        public string SiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the data amount.
        /// </summary>
        /// <value>
        /// The data amount.
        /// </value>
        public string LeadName { get; set; }

        /// <summary>
        /// Gets or sets the js source.
        /// </summary>
        /// <value>
        /// The js source.
        /// </value>
        public string CardImage { get; set; }

        /// <summary>
        /// Gets or sets the data amount.
        /// </summary>
        /// <value>
        /// The data amount.
        /// </value>
        public string DealName { get; set; }

        /// <summary>
        /// Gets or sets the data key.
        /// </summary>
        /// <value>
        /// The data key.
        /// </value>
        public int DealType { get; set; }

        /// <summary>
        /// Gets or sets the data amount.
        /// </summary>
        /// <value>
        /// The data amount.
        /// </value>
        public string DealUrl { get; set; }

        /// <summary>
        /// Gets or sets the data amount.
        /// </summary>
        /// <value>
        /// The StartDate.
        /// </value>
        public DateTime startDate { get; set; }

        /// <summary>
        /// Gets or sets the data amount.
        /// </summary>
        /// <value>
        /// The data amount.
        /// </value>
        public DateTime endDate { get; set; }

        /// <summary>
        /// Gets or sets the Hotel Name.
        /// </summary>
        /// <value>
        /// The Hotel Name.
        /// </value>
        public string HotelName { get; set; }

        /// <summary>
        /// Gets or sets the Tour Name.
        /// </summary>
        /// <value>
        /// The Tour Name.
        /// </value>
        public string TourName { get; set; }
    }
}
