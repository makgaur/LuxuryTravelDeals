// <copyright file="MinimumAgeAttribute.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.TBO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// MinimumAgeAttribute
    /// </summary>
    public class MinimumAgeAttribute : ValidationAttribute
    {
        /// <summary>
        /// The minimum age
        /// </summary>
        private int minimumAge;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinimumAgeAttribute"/> class.
        /// </summary>
        /// <param name="minimumAge">The minimum age.</param>
        public MinimumAgeAttribute(int minimumAge)
        {
            this.minimumAge = minimumAge;
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if the specified value is valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValid(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date.AddYears(this.minimumAge) < DateTime.Now;
            }

            return false;
        }
    }
}