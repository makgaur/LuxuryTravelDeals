// <copyright file="DealRoomConfigViewModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    /// Hotelier Image model
    /// </summary>
    public class DealRoomConfigViewModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public int InclusionId { get; set; }

        /// <summary>
        /// Gets or sets the room type identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public short RoomTypeId { get; set; }

        /// <summary>
        /// Gets or sets the room type identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        public string RoomTypeName { get; set; }

        /// <summary>
        /// Gets or sets the no of adults.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        public int Adult { get; set; }

        /// <summary>
        /// Gets or sets the no of Child.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        public int Child { get; set; }

        /// <summary>
        /// Gets or sets the no of Infant.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        public int Infant { get; set; }

        /// <summary>
        /// Gets or sets the no of Child.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        public int FreeChild { get; set; }

        /// <summary>
        /// Gets or sets the no of Infant.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        public int FreeInfant { get; set; }

        /// <summary>
        /// Gets or sets the no of max occupants.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        public int Max { get; set; }

        /// <summary>
        /// Gets or sets the age of adults.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        public int AdultAge { get; set; }

        /// <summary>
        /// Gets or sets the age of adults.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        public int ChildAge { get; set; }

        /// <summary>
        /// Gets or sets the age of adults.
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        public int InfantAge { get; set; }

        /// <summary>
        /// Gets or sets the html description of Room
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the html description of Room
        /// </summary>
        /// <value>
        /// integer.
        /// </value>
        public string CardImg { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the is active.
        /// </summary>
        /// <value>
        /// bool.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the is active.
        /// </summary>
        /// <value>
        /// bool.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the is active.
        /// </summary>
        /// <value>
        /// bool.
        /// </value>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the is active.
        /// </summary>
        /// <value>
        /// bool.
        /// </value>
        public string RoomName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the is active.
        /// </summary>
        /// <value>
        /// bool.
        /// </value>
        public List<DealsRatePlanViewModel> DealsRatePlanViewModels { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the is active.
        /// </summary>
        /// <value>
        /// bool.
        /// </value>
        public RoomImageGalleryViewModel RoomImageGalleryViewModel { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets the is active.
        /// </summary>
        /// <value>
        /// bool.
        /// </value>
        public List<string> RoomAmenties { get; set; }
    }
}
