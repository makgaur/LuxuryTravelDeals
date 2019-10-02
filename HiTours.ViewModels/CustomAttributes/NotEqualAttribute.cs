// <copyright file="NotEqualAttribute.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels.CustomAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotEqualAttribute" /> class.
    /// </summary>
    public class NotEqualAttribute : ValidationAttribute, IClientModelValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotEqualAttribute" /> class.
        /// </summary>
        /// <param name="otherProperty">State Service</param>
        public NotEqualAttribute(string otherProperty)
        {
            this.OtherProperty = otherProperty;
        }

        private string OtherProperty { get; set; }

        /// <summary>
        /// Initializes a new instance of the AddValidation class.
        /// </summary>
        /// <param name="context">Context</param>
        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-equal", "First Name and Last Name Should not be Equal");
        }

        /// <inheritdoc/>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(this.OtherProperty);
            if (property == null)
            {
                return new ValidationResult(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "{0} is unknown property",
                        this.OtherProperty));
            }

            var otherValue = property.GetValue(validationContext.ObjectInstance, null);
            if (object.Equals(value.ToString().ToLower(), otherValue.ToString().ToLower()))
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }
}
