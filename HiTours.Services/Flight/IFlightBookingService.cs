// <copyright file="IFlightBookingService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.ViewModels.FlightApi;

    /// <summary>
    /// IFlightBookingService
    /// </summary>
    public interface IFlightBookingService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Insert Fligt Booking</returns>
        Task<FlightBookingModel> InsertAsync(FlightBookingModel model);

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        Task<IEnumerable<FlightBookingModel>> InsertAsync(IEnumerable<FlightBookingModel> models);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        Task<FlightBookingModel> UpdateAsync(FlightBookingModel model);

        /// <summary>
        /// Gets all by user identifier asynchronous.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns>GetAllByUserIdAsync</returns>
        Task<List<FlightBookingViewModel>> GetAllByUserIdAsync(int userid);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="bookingId">The booking identifier.</param>
        /// <returns>GetByIdAsync</returns>
        Task<FlightBookingViewModel> GetByIdAsync(Guid bookingId);
    }
}