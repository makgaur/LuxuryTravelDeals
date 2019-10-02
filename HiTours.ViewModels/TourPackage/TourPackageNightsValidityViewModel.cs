// <copyright file="TourPackageNightsValidityViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;

    /// <summary>
    /// TourPackageNightsValidityViewModel
    /// </summary>
    public class TourPackageNightsValidityViewModel
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
        [Required(ErrorMessage = "required")]
        public short HotelRoomTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the room type.
        /// </summary>
        /// <value>
        /// The name of the room type.
        /// </value>
        public string RoomTypeName { get; set; }

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
        [Required(ErrorMessage = "required")]
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
        [Required(ErrorMessage = "required")]
        [Display(Name = "Depart City")]
        public int DepartCityId { get; set; }

        /// <summary>
        /// Gets or sets the name of the depart city.
        /// </summary>
        /// <value>
        /// The name of the depart city.
        /// </value>
        public string DepartCityName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> HotelList { get; set; }

        /// <summary>
        /// Gets or sets the type of the deal.
        /// </summary>
        /// <value>
        /// The type of the deal.
        /// </value>
        public IEnumerable<SelectListItem> CityList { get; set; }

        /// <summary>
        /// Gets or sets the type of the package hotel room.
        /// </summary>
        /// <value>
        /// The type of the package hotel room.
        /// </value>
        public PackageHotelRoomTypeViewModel PackageHotelRoomType { get; set; }

        /// <summary>
        /// Gets or sets the booked on dates.
        /// </summary>
        /// <value>
        /// The booked on dates.
        /// </value>
        public List<RoomAvailabilityViewModel> BookedOnDates { get; set; }

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
        [Required(ErrorMessage = "required")]
        [Display(Name = "Room Capacity")]
        [Range(1, 10, ErrorMessage = "{0} Must Be Between {1} And {2}")]
        public short RoomCapacity { get; set; }

        /// <summary>
        /// Gets or sets the maximum adult.
        /// </summary>
        /// <value>
        /// The maximum adult.
        /// </value>
        [Required(ErrorMessage = "required")]
        [Display(Name = "Maximum Adult")]
        [Range(1, 5, ErrorMessage = "{0} Must Be Between {1} And {2}")]
        public short MaxAdult { get; set; }

        /// <summary>
        /// Gets or sets the maximum child.
        /// </summary>
        /// <value>
        /// The maximum child.
        /// </value>
        [Display(Name = "Maximum Child")]
        [Range(0, 4, ErrorMessage = "{0} Must Be Between {1} And {2}")]
        public short? MaxChild { get; set; }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public string RowIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the tr indentifier.
        /// </summary>
        /// <value>
        /// The tr indentifier.
        /// </value>
        public string TrIndentifier { get; set; }

        /// <summary>
        /// Gets or sets the row identifier.
        /// </summary>
        /// <value>
        /// The row identifier.
        /// </value>
        public string RowId { get; set; }

        /// <summary>
        /// Gets or sets the discount.
        /// </summary>
        /// <value>
        /// The discount.
        /// </value>
        public decimal Discount { get; set; }

        /// <summary>
        /// Gets or sets the descriptions.
        /// </summary>
        /// <value>
        /// The descriptions.
        /// </value>
        public string Descriptions { get; set; }
    }
}