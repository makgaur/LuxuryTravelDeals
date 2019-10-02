// <copyright file="Extensions.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    /// <summary>
    /// Extesnions Methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// To the display name.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Get Display Property Name : Enums</returns>
        public static string ToDisplayName(this Enum value)
        {
            var type = value.GetType();
            var members = type.GetMember(value.ToString());
            if (members.Length == 0)
            {
                return string.Empty;
            }

            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();
            var name = attributes == null ? string.Empty : ((DisplayAttribute)attributes).Name;
            return !string.IsNullOrEmpty(name) ? name : string.Empty;
        }

        /// <summary>
        /// Gets the default attribute.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Get Default Attribute </returns>
        public static ButtonAttribute ToButtonAttribute(this Enum value)
        {
            var type = value.GetType();
            var members = type.GetMember(value.ToString());
            if (members.Length == 0)
            {
                return new ButtonAttribute();
            }

            var member = members[0];
            var attribute = member.GetCustomAttributes(typeof(ButtonAttribute), false).FirstOrDefault();
            return attribute == null ? new ButtonAttribute() : attribute as ButtonAttribute;
        }

        /// <summary>
        /// Errors the free object.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>SetDefault</returns>
        public static TEntity SetDefaults<TEntity>(this TEntity entity)
            where TEntity : class
        {
            if (entity == null)
            {
                return entity;
            }

            try
            {
                foreach (PropertyDescriptor propertyInfo in TypeDescriptor.GetProperties(typeof(TEntity)))
                {
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        if (propertyInfo.GetValue(entity) == null)
                        {
                            propertyInfo.SetValue(entity, string.Empty);
                        }
                    }
                    else if (propertyInfo.PropertyType == typeof(int))
                    {
                        if (propertyInfo.GetValue(entity) == null)
                        {
                            propertyInfo.SetValue(entity, 0);
                        }
                    }
                    else if (propertyInfo.PropertyType == typeof(DateTime))
                    {
                        if (propertyInfo.GetValue(entity) == null || (DateTime)propertyInfo.GetValue(entity) == DateTime.MinValue)
                        {
                            propertyInfo.SetValue(entity, DateTime.Now);
                        }
                    }
                }
            }
            catch
            {
            }

            return entity;
        }
    }
}