// <copyright file="BookingService.cs" company="Luxury Travel Deals">
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
    using HiTours.ViewModels;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// UserDetailService
    /// </summary>
    /// <seealso cref="IBookingService" />
    public class BookingService : IBookingService
    {
        private readonly IRepository<BookingInformationModel> bookingInformationRepository;
        private readonly IRepository<BookingPassengerModel> bookingPassengerRepository;
        private readonly IRepository<BookingHotelRoomModel> bookingHotelRoomRepository;
        private readonly IRepository<BookingVisaModel> bookingVisaRepository;
        private readonly IRepository<BookingFlightModel> bookingFlightRepository;
        private readonly IRepository<DealsPackageModel> dealPackageRepository;
        private readonly IRepository<HotelierInformationModel> hotelierInformationRepository;
        private readonly IRepository<BookingsGridViewModel> bookingGridRepository;
        private readonly IRepository<PackageCityModel> packageCityRepository;
        private readonly IRepository<PackageCountryModel> packageCountryRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookingService"/> class.
        /// Deals Service
        /// </summary>
        /// <param name="hotelierInformationRepository">Hotelier Content Repository</param>
        /// <param name="packageCityRepository">Package City Repository</param>
        /// <param name="packageCountryRepository">Package Country Repository</param>
        /// <param name="bookingFlightRepository">Booking Flight Repository</param>
        /// <param name="bookingGridRepository">Booking Grid Repo</param>
        /// <param name="dealPackageRepository">Deal Package Repository</param>
        /// <param name="bookingInformationRepository">Booking Infomartion Repository Repo</param>
        /// <param name="bookingPassengerRepository">Booking Passanger Repository</param>
        /// <param name="bookingHotelRoomRepository">Booking otel Room Repo</param>
        /// <param name="bookingVisaRepository">Booking Visa Repo</param>
        public BookingService(
            IRepository<HotelierInformationModel> hotelierInformationRepository,
            IRepository<PackageCityModel> packageCityRepository,
            IRepository<PackageCountryModel> packageCountryRepository,
            IRepository<BookingFlightModel> bookingFlightRepository,
            IRepository<BookingsGridViewModel> bookingGridRepository,
            IRepository<DealsPackageModel> dealPackageRepository,
            IRepository<BookingInformationModel> bookingInformationRepository,
            IRepository<BookingPassengerModel> bookingPassengerRepository,
            IRepository<BookingHotelRoomModel> bookingHotelRoomRepository,
            IRepository<BookingVisaModel> bookingVisaRepository)
        {
            this.hotelierInformationRepository = hotelierInformationRepository;
            this.packageCityRepository = packageCityRepository;
            this.packageCountryRepository = packageCountryRepository;
            this.bookingFlightRepository = bookingFlightRepository;
            this.bookingGridRepository = bookingGridRepository;
            this.dealPackageRepository = dealPackageRepository;
            this.bookingVisaRepository = bookingVisaRepository;
            this.bookingHotelRoomRepository = bookingHotelRoomRepository;
            this.bookingPassengerRepository = bookingPassengerRepository;
            this.bookingInformationRepository = bookingInformationRepository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlogService" /> class.
        /// </summary>
        /// <param name="model">Data Table</param>
        /// <param name="type">Curation Mode</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public async Task<DataTableResult> GetAllBookingsByType(DataTableParameter model, int type)
        {
            var result = this.bookingInformationRepository.Table.Where(x => x.DealsPackageModel.Type == type).Select(x => new BookingsGridViewModel
            {
                BookingDate = x.BookedDate,
                BookingRefrenceNumber = x.ReferenceNumber,
                DealType = x.DealsPackageModel.Type,
                DealName = x.DealsPackageModel.Name,
                BookingPerson = x.LeadFullName,
                DealId = x.DealId,
                Id = x.Id,
                Email = x.Email,
                Phone = x.PhoneNumber,
                TotalAmount = x.TotalAmount,
                Paid = x.IsConfirmed,
                PaymentStatus = x.IsConfirmed ? "Paid" : "Unpaid"
            }).OrderByDescending(x => x.Id);

            return await this.bookingGridRepository.ToPagedListAsync(result, model);
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="data">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<BookingInformationModel> AddBookingInformation(BookingInformationModel data)
        {
            try
            {
                await this.bookingInformationRepository.InsertAsync(data);
                return data;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="data">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<int> AddBookingFlightInformation(BookingFlightModel data)
        {
            try
            {
                await this.bookingFlightRepository.InsertAsync(data);
                return data.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="data">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<int> UpdateBookingFlightInformation(BookingFlightModel data)
        {
            try
            {
                await this.bookingFlightRepository.UpdateAsync(data);
                return data.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Deal package model</returns>
        public async Task<BookingFlightModel> GetBookingFlightById(int id)
        {
            try
            {
                return await this.bookingFlightRepository.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="bookingId">Booking indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<BookingInformationModel> GetBookingRecordById(int bookingId)
        {
            try
            {
                return await this.bookingInformationRepository.Table.Where(x => x.Id == bookingId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="model">Booking info Model</param>
        /// <returns>Deal package model</returns>
        public async Task<int> UpdateBookingInformationRecord(BookingInformationModel model)
        {
            try
            {
                return await this.bookingInformationRepository.UpdateAsync(model);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="data">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<int> AddPassenger(BookingPassengerModel data)
        {
            try
            {
                await this.bookingPassengerRepository.InsertAsync(data);
                return data.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="data">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<int> AddHotelRoom(BookingHotelRoomModel data)
        {
            try
            {
                await this.bookingHotelRoomRepository.InsertAsync(data);
                return data.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="data">Package indentifer</param>
        /// <returns>Deal package model</returns>
        public async Task<int> AddVisa(BookingVisaModel data)
        {
            try
            {
                await this.bookingVisaRepository.InsertAsync(data);
                return data.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="bookingId">Booking Id</param>
        /// <returns>Deal package model</returns>
        public async Task<BookingThankYouViewModel> GetBookingThankYouById(int bookingId)
        {
            try
            {
                int dealId = await this.bookingInformationRepository.Table.Where(x => x.Id == bookingId).Select(x => x.DealId).FirstOrDefaultAsync();
                DealsPackageModel dealModel = this.dealPackageRepository.Table
                    .Include("DealsDestinationModels")
                    .Include("DealContentModels")
                    .Include("DealsHighlightModels")
                    .Include("DealsNightModels.DealsItineraryModels.DealsInclusionModels")
                    .Where(x => x.Id == dealId).FirstOrDefault();

                BookingThankYouViewModel model = await this.bookingInformationRepository.Table.Where(x => x.Id == bookingId).Select(x => new BookingThankYouViewModel
                {
                    BookingId = x.Id,
                    BookingReferenceNumber = x.ReferenceNumber,
                    DealName = dealModel.Name,
                    CardImage = dealModel.DealContentModels.Select(y => y.CardImg).FirstOrDefault(),
                    DealType = dealModel.Type,
                    StartDate = x.Checkin,
                    EndDate = x.Checkout,
                    VisaOpted = x.BookingVisaModels.Count > 0,
                    FlightOpted = x.FlightRequired,
                    Childs = x.BookingHotelRoomModels.Sum(y => y.Child),
                    Adults = x.BookingHotelRoomModels.Sum(y => y.Adult),
                    Infants = x.BookingHotelRoomModels.Sum(y => y.Infant),
                    BookingStatus = x.FlightRequired ? x.BookingFlightModels.All(y => y.TicketGenerated) && x.BookingFlightModels.Select(y => string.IsNullOrEmpty(y.PNR)).Any() && x.FlightSuccessful : true,
                    PaymentStatus = true,
                    Highlights = dealModel.DealsHighlightModels.Select(y => y.Title).ToList(),
                    BookingSummarySerialized = x.BookingSummarySerialized
                }).FirstOrDefaultAsync();

                if (model != null)
                {
                    model.BookingStatusMessage = model.BookingStatus ? "Booking Confirmed" : "Booking Confirmed";
                    model.PaymentStatusMessage = model.PaymentStatus ? "Collected Succesfully" : "Payment Failed";
                    model.Locations = dealModel.DealsDestinationModels.Where(y => y.City > 0).Join(this.packageCityRepository.Table, d => d.City, c => c.Id, (d, c) => new { d, c }).Select(z => z.c.Name).ToList();
                    if (model.DealType == 1)
                    {
                        int hotelId = dealModel.DealsNightModels.Select(y => y.DealsItineraryModels.Select(z => z.DealsInclusionModels.Select(k => k.VendorInfoId).FirstOrDefault()).FirstOrDefault()).FirstOrDefault().HasValue ? dealModel.DealsNightModels.Select(y => y.DealsItineraryModels.Select(z => z.DealsInclusionModels.Select(k => k.VendorInfoId).FirstOrDefault()).FirstOrDefault()).FirstOrDefault().Value : 0;
                        model.HotelName = dealModel.Type == 1 ? this.hotelierInformationRepository.Table.Where(x => x.Id == hotelId).Select(x => x.Name).FirstOrDefault() : string.Empty;
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return new BookingThankYouViewModel();
            }
        }

        /// <summary>
        /// Get package information asynchromusly
        /// </summary>
        /// <param name="bookingId">Booking Id</param>
        /// <returns>Deal package model</returns>
        public async Task<BookingSendMailViewModel> GetBookingEmail(int bookingId)
        {
            try
            {
                var bookingRecord = await this.bookingInformationRepository.Table.Where(x => x.Id == bookingId).FirstOrDefaultAsync();
                BookingSendMailViewModel model = await this.dealPackageRepository.Table.Where(x => x.Id == bookingRecord.DealId).Select(x => new BookingSendMailViewModel
                {
                    BookingDate = bookingRecord.CreatedDate,
                    StartDate = bookingRecord.Checkin,
                    EndDate = bookingRecord.Checkout,
                    DealName = x.Name,
                    DealCode = x.Code,
                    BookingId = bookingRecord.Id,
                    BookingPrice = bookingRecord.TotalAmount
                }).FirstOrDefaultAsync();
                return model;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }
    }
}