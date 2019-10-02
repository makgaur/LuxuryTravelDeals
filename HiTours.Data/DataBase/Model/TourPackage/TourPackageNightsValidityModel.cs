// <copyright file="TourPackageNightsValidityModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Data.DataBase.Model
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// TourPackageNightsValidityModel
    /// </summary>
    /// <seealso cref="HiTours.Data.DataBase.Model.PckageBaseModel" />
    public class TourPackageNightsValidityModel : PckageBaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the tour package nights identifier.
        /// </summary>
        /// <value>
        /// The tour package nights identifier.
        /// </value>
        public Guid TourPackageNightsId { get; set; }

        /// <summary>
        /// Gets or sets the hotel room type identifier.
        /// </summary>
        /// <value>
        /// The hotel room type identifier.
        /// </value>
        public short HotelRoomTypeId { get; set; }

        /// <summary>
        /// Gets or sets the rate valid from.
        /// </summary>
        /// <value>
        /// The rate valid from.
        /// </value>
        public DateTime RateValidFrom { get; set; }

        /// <summary>
        /// Gets or sets the rate valid to.
        /// </summary>
        /// <value>
        /// The rate valid to.
        /// </value>
        public DateTime RateValidTo { get; set; }

        /// <summary>
        /// Gets or sets the triple rate week days.
        /// </summary>
        /// <value>
        /// The triple rate week days.
        /// </value>
        public decimal TripleRateWeekDays { get; set; }

        /// <summary>
        /// Gets or sets the twin rate week days.
        /// </summary>
        /// <value>
        /// The twin rate week days.
        /// </value>
        public decimal TwinRateWeekDays { get; set; }

        /// <summary>
        /// Gets or sets the single rate week days.
        /// </summary>
        /// <value>
        /// The single rate week days.
        /// </value>
        public decimal SingleRateWeekDays { get; set; }

        /// <summary>
        /// Gets or sets the child with bed rate week days.
        /// </summary>
        /// <value>
        /// The child with bed rate week days.
        /// </value>
        public decimal ChildWithBedRateWeekDays { get; set; }

        /// <summary>
        /// Gets or sets the child without bed rate week days.
        /// </summary>
        /// <value>
        /// The child without bed rate week days.
        /// </value>
        public decimal ChildWithoutBedRateWeekDays { get; set; }

        /// <summary>
        /// Gets or sets the triple rate weekend.
        /// </summary>
        /// <value>
        /// The triple rate weekend.
        /// </value>
        public decimal TripleRateWeekend { get; set; }

        /// <summary>
        /// Gets or sets the twin rate weekend.
        /// </summary>
        /// <value>
        /// The twin rate weekend.
        /// </value>
        public decimal TwinRateWeekend { get; set; }

        /// <summary>
        /// Gets or sets the single rate weekend.
        /// </summary>
        /// <value>
        /// The single rate weekend.
        /// </value>
        public decimal SingleRateWeekend { get; set; }

        /// <summary>
        /// Gets or sets the child with bed rate weekend.
        /// </summary>
        /// <value>
        /// The child with bed rate weekend.
        /// </value>
        public decimal ChildWithBedRateWeekend { get; set; }

        /// <summary>
        /// Gets or sets the child without bed rate weekend.
        /// </summary>
        /// <value>
        /// The child without bed rate weekend.
        /// </value>
        public decimal ChildWithoutBedRateWeekend { get; set; }

        /// <summary>
        /// Gets or sets the depart city identifier.
        /// </summary>
        /// <value>
        /// The depart city identifier.
        /// </value>
        public int DepartCityId { get; set; }

        /// <summary>
        /// Gets or sets the tour package night.
        /// </summary>
        /// <value>
        /// The tour package night.
        /// </value>
        public TourPackageNightModel TourPackageNight { get; set; }

        /// <summary>
        /// Gets or sets the type of the package hotel room.
        /// </summary>
        /// <value>
        /// The type of the package hotel room.
        /// </value>
        [NotMapped]
        public PackageHotelRoomTypeModel PackageHotelRoomType { get; set; }

        /// <summary>
        /// Gets or sets the rate type applied.
        /// </summary>
        /// <value>
        /// The rate type applied.
        /// </value>
        public byte RateTypeApplied { get; set; }

        /// <summary>
        /// Gets or sets the room capacity.
        /// </summary>
        /// <value>
        /// The room capacity.
        /// </value>
        public short RoomCapacity { get; set; }

        /// <summary>
        /// Gets or sets the maximum adult.
        /// </summary>
        /// <value>
        /// The maximum adult.
        /// </value>
        public short MaxAdult { get; set; }

        /// <summary>
        /// Gets or sets the maximum child.
        /// </summary>
        /// <value>
        /// The maximum child.
        /// </value>
        public short MaxChild { get; set; }

        /// <summary>
        /// Gets or sets the descriptions.
        /// </summary>
        /// <value>
        /// The descriptions.
        /// </value>
        public string Descriptions { get; set; }

        /// <summary>
        /// Gets or sets the name of the depart city.
        /// </summary>
        /// <value>
        /// The name of the depart city.
        /// </value>
        [NotMapped]
        public string DepartCityName { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        [NotMapped]
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the state of the object.
        /// </summary>
        /// <value>
        /// The state of the object.
        /// </value>
        [NotMapped]
        public Microsoft.EntityFrameworkCore.EntityState ObjectState { get; set; }
    }
}