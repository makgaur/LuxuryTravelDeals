// <copyright file="MinAge.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Min Age DateofBirth Validation
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    public class MinAge : ValidationAttribute
    {
        private int age;
        private string errorMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinAge" /> class.
        /// </summary>
        /// <param name="age">The limit.</param>
        /// <param name="errorMessage">The error message.</param>
        public MinAge(int age, string errorMessage)
        {
            this.age = age;
            this.errorMessage = errorMessage;
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>
        /// An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult"></see> class.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateOfBirth = DateTime.Parse(value.ToString());
            DateTime today = DateTime.Today;
            int currentAge = today.Year - dateOfBirth.Year;
            if (dateOfBirth > today.AddYears(-currentAge))
            {
                currentAge--;
            }

            if (currentAge < this.age)
            {
                var result = new ValidationResult(!string.IsNullOrEmpty(this.errorMessage) ? this.errorMessage : "Sorry you are not old enough");
                return result;
            }

            return null;
        }
    }
}