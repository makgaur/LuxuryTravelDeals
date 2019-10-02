// <copyright file="FlightBookingService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.ViewModels.FlightApi;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// FlightBookingService
    /// </summary>
    public class FlightBookingService : IFlightBookingService
    {
        /// <summary>
        /// The flight booking repository
        /// </summary>
        private readonly IRepository<FlightBookingModel> flightBookingRepository;

        /// <summary>
        /// The view model repository
        /// </summary>
        private readonly IRepository<FlightBookingViewModel> viewModelRepository;

        /// <summary>
        /// The user transaction repository
        /// </summary>
        private readonly IRepository<UserTransactionModel> userTransactionRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlightBookingService" /> class.
        /// </summary>
        /// <param name="flightBookingRepository">The flight booking repository.</param>
        /// <param name="userTransactionRepository">The user transaction repository.</param>
        /// <param name="viewModelRepository">The view model repository.</param>
        public FlightBookingService(IRepository<FlightBookingModel> flightBookingRepository, IRepository<UserTransactionModel> userTransactionRepository, IRepository<FlightBookingViewModel> viewModelRepository)
        {
            this.flightBookingRepository = flightBookingRepository;
            this.userTransactionRepository = userTransactionRepository;
            this.viewModelRepository = viewModelRepository;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Insert Fligt Booking</returns>
        public async Task<FlightBookingModel> InsertAsync(FlightBookingModel model)
        {
            await this.flightBookingRepository.InsertAsync(model);
            return model;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="models">The models.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        public async Task<IEnumerable<FlightBookingModel>> InsertAsync(IEnumerable<FlightBookingModel> models)
        {
            foreach (var model in models)
            {
                this.flightBookingRepository.AddToContext(model);
            }

            try
            {
                await this.flightBookingRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return models;
        }

        /// <summary>
        /// Gets all by user identifier asynchronous.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns>GetAllByUserIdAsync</returns>
        public async Task<List<FlightBookingViewModel>> GetAllByUserIdAsync(int userid)
        {
            try
            {
                var query = this.flightBookingRepository.Table.Where(x => x.UserId == userid).Select(x => new FlightBookingViewModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    TboBookingId = x.TboBookingId,
                    BookingDate = x.BookingDate,
                    Pnr = x.Pnr,
                    Origin = x.Origin,
                    Destination = x.Destination,
                    UserTransactionId = x.UserTransactionId,
                    Remark = x.Remark,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    Response = x.Response,
                    Error = x.Error
                });

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="bookingId">The booking identifier.</param>
        /// <returns>GetByIdAsync</returns>
        public async Task<FlightBookingViewModel> GetByIdAsync(Guid bookingId)
        {
            try
            {
                var query = this.flightBookingRepository.Table.Where(x => x.Id == bookingId).Select(x => new FlightBookingViewModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    TboBookingId = x.TboBookingId,
                    BookingDate = x.BookingDate,
                    Pnr = x.Pnr,
                    Origin = x.Origin,
                    Destination = x.Destination,
                    UserTransactionId = x.UserTransactionId,
                    Remark = x.Remark,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    Response = x.Response,
                    Error = x.Error
                });

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        public async Task<FlightBookingModel> UpdateAsync(FlightBookingModel model)
        {
            var record = await this.flightBookingRepository.Table.FirstOrDefaultAsync(m => m.Id == model.Id);
            if (record != null)
            {
                record.Pnr = model.Pnr;
                record.Origin = model.Origin;
                record.Destination = model.Destination;
                record.TboBookingId = model.TboBookingId;
                record.BookingDate = model.BookingDate;
                record.Remark = model.Remark;
                record.UpdateAuditInfo(model.UpdatedBy);
            }

            await this.flightBookingRepository.SaveChangesAsync();

            return model;
        }
    }
}