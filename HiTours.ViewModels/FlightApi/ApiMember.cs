// <copyright file="ApiMember.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.FlightApi
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// ApiMember
    /// </summary>
    public class ApiMember
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the member identifier.
        /// </summary>
        /// <value>
        /// The member identifier.
        /// </value>
        public string MemberId { get; set; }

        /// <summary>
        /// Gets or sets the agency identifier.
        /// </summary>
        /// <value>
        /// The agency identifier.
        /// </value>
        public string AgencyId { get; set; }

        /// <summary>
        /// Gets or sets the name of the login.
        /// </summary>
        /// <value>
        /// The name of the login.
        /// </value>
        public string LoginName { get; set; }

        /// <summary>
        /// Gets or sets the login details.
        /// </summary>
        /// <value>
        /// The login details.
        /// </value>
        public string LoginDetails { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is primary agent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is primary agent; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrimaryAgent { get; set; }
    }
}
