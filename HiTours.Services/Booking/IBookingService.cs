// <copyright file="IBookingService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.ViewModels;

    /// <summary>
    /// IVendorService
    /// </summary>
    public interface IBookingService
    {
        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="data">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<int> UpdateBookingFlightInformation(BookingFlightModel data);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Deal package model</returns>
        Task<BookingFlightModel> GetBookingFlightById(int id);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="data">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<int> AddBookingFlightInformation(BookingFlightModel data);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Booking info Model</param>
        /// <returns>Deal package model</returns>
        Task<int> UpdateBookingInformationRecord(BookingInformationModel model);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="bookingId">Booking indentifer</param>
        /// <returns>Deal package model</returns>
        Task<BookingInformationModel> GetBookingRecordById(int bookingId);

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogService" /> class.
        /// </summary>
        /// <param name="model">Data Table</param>
        /// <param name="type">Curation Mode</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        Task<DataTableResult> GetAllBookingsByType(DataTableParameter model, int type);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="data">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<BookingInformationModel> AddBookingInformation(BookingInformationModel data);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="data">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<int> AddPassenger(BookingPassengerModel data);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="data">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<int> AddHotelRoom(BookingHotelRoomModel data);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="data">Package indentifer</param>
        /// <returns>Deal package model</returns>
        Task<int> AddVisa(BookingVisaModel data);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="bookingId">Booking Id</param>
        /// <returns>Deal package model</returns>
        Task<BookingSendMailViewModel> GetBookingEmail(int bookingId);

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="bookingId">Booking Id</param>
        /// <returns>Deal package model</returns>
        Task<BookingThankYouViewModel> GetBookingThankYouById(int bookingId);
    }
}