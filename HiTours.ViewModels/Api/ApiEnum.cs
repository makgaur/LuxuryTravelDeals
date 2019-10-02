// <copyright file="ApiEnum.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// ApiStatus
    /// </summary>
    public enum ApiStatus
    {
        /// <summary>
        /// The not set
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// The successful
        /// </summary>
        Successful = 1,

        /// <summary>
        /// The failed
        /// </summary>
        Failed = 2,

        /// <summary>
        /// The in correct user name
        /// </summary>
        InCorrectUserName = 3,

        /// <summary>
        /// The in correct password
        /// </summary>
        InCorrectPassword = 4,

        /// <summary>
        /// The password expired
        /// </summary>
        PasswordExpired = 5
    }

    /// <summary>
    /// ApiAgencyType
    /// </summary>
    public enum ApiAgencyType
    {
        /// <summary>
        /// The not set
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// The cash
        /// </summary>
        Cash = 1,

        /// <summary>
        /// The credit
        /// </summary>
        Credit = 2
    }

    /// <summary>
    /// JourneyType
    /// </summary>
    public enum ApiJourneyType
    {
        /// <summary>
        /// The one way
        /// </summary>
        OneWay = 1,

        /// <summary>
        /// The return
        /// </summary>
        Return = 2,

        /// <summary>
        /// The multi
        /// </summary>
        MultiStop = 3,

        /// <summary>
        /// The advance search
        /// </summary>
        AdvanceSearch = 4,

        /// <summary>
        /// The 1
        /// </summary>
        SpecialReturn = 5
    }

    /// <summary>
    /// Cabin class
    /// </summary>
    public enum FlightCabinClass
    {
        /// <summary>
        /// All
        /// </summary>
        All = 1,

        /// <summary>
        /// The economy
        /// </summary>
        Economy = 2,

        /// <summary>
        /// The premium economy
        /// </summary>
        PremiumEconomy = 3,

        /// <summary>
        /// The business
        /// </summary>
        Business = 4,

        /// <summary>
        /// The premium business
        /// </summary>
        PremiumBusiness = 5,

        /// <summary>
        /// The first
        /// </summary>
        First = 6
    }
}
