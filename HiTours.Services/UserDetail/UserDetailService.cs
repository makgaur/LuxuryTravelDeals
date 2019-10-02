// <copyright file="UserDetailService.cs" company="Luxury Travel Deals">
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
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    /// <summary>
    /// UserDetailService
    /// </summary>
    /// <seealso cref="HiTours.Services.IUserDetailService" />
    /// <seealso cref="IUserDetailService" />
    public class UserDetailService : IUserDetailService
    {
        /// <summary>
        /// The user detail repository
        /// </summary>
        private IRepository<UserDetailModel> userDetailRepository;
        private IRepository<BookingInformationModel> bookingInfomationRepository;
        private IRepository<BookingVisaModel> bookingVisaRepository;
        private IRepository<DealsPackageModel> dealPackageRepository;
        private IRepository<DealsContentModel> dealContentRepository;
        private IRepository<DealVisaModel> dealVisaRepository;
        private IRepository<DealsImageModel> dealImageRepository;
        private IRepository<PackageCityModel> packageCityRepository;
        private IRepository<PackageCountryModel> packageCountryRepository;
        private IRepository<DealRoomConfigurationModel> dealRoomConfigRepository;
        private IRepository<BookingHotelRoomModel> bookingHotelRoomRepository;
        private IRepository<DealsRatePlanModel> dealRatePlanRepository;
        private IRepository<HotelierInformationModel> hotelierRepository;
        private IRepository<BookingFlightModel> bookingFlightRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDetailService" /> class.
        /// </summary>
        /// <param name="bookingFlightRepository">Booking Flight Repository</param>
        /// <param name="dealImageRepository">Deal Image Repository</param>
        /// <param name="dealContentRepository">Deal Content Repository</param>
        /// <param name="bookingHotelRoomRepository">Booking Hotel Room Repository</param>
        /// <param name="dealRoomConfigRepository">Deal Room Config Repository</param>
        /// <param name="dealRatePlanRepository">Deal Rate Plan Repository</param>
        /// <param name="dealVisaRepository">Deal Visa Repository</param>
        /// <param name="bookingVisaRepository">Booking Visa Repository</param>
        /// <param name="hotelierRepository">Hotelier Repository</param>
        /// <param name="packageCountryRepository">Package Country Repo</param>
        /// <param name="packageCityRepository">PackageCity</param>
        /// <param name="userDetailRepository">The user detail repository.</param>
        /// <param name="bookingInfomationRepository">Booking Information Repository</param>
        /// <param name="dealPackageRepository">Deal Package Repository</param>
        public UserDetailService(
            IRepository<BookingFlightModel> bookingFlightRepository,
            IRepository<DealsImageModel> dealImageRepository,
            IRepository<DealsContentModel> dealContentRepository,
            IRepository<BookingHotelRoomModel> bookingHotelRoomRepository,
            IRepository<DealRoomConfigurationModel> dealRoomConfigRepository,
            IRepository<DealsRatePlanModel> dealRatePlanRepository,
            IRepository<DealVisaModel> dealVisaRepository,
            IRepository<BookingVisaModel> bookingVisaRepository,
            IRepository<HotelierInformationModel> hotelierRepository,
            IRepository<PackageCountryModel> packageCountryRepository,
            IRepository<PackageCityModel> packageCityRepository,
            IRepository<UserDetailModel> userDetailRepository,
            IRepository<BookingInformationModel> bookingInfomationRepository,
            IRepository<DealsPackageModel> dealPackageRepository)
        {
            this.bookingFlightRepository = bookingFlightRepository;
            this.dealImageRepository = dealImageRepository;
            this.dealContentRepository = dealContentRepository;
            this.bookingHotelRoomRepository = bookingHotelRoomRepository;
            this.dealRatePlanRepository = dealRatePlanRepository;
            this.dealRoomConfigRepository = dealRoomConfigRepository;
            this.dealVisaRepository = dealVisaRepository;
            this.bookingVisaRepository = bookingVisaRepository;
            this.hotelierRepository = hotelierRepository;
            this.packageCityRepository = packageCityRepository;
            this.packageCountryRepository = packageCountryRepository;
            this.dealPackageRepository = dealPackageRepository;
            this.bookingInfomationRepository = bookingInfomationRepository;
            this.userDetailRepository = userDetailRepository;
        }

        /// <summary>
        /// send otp.
        /// </summary>
        /// <param name="mobileNo">Themobile number.</param>
        /// <param name="from">from.</param>
        /// <returns>
        /// UserDetailModel
        /// </returns>
        public async Task<UserDetailModel> CheckMobile(string mobileNo, string from)
        {
            try
            {
                UserDetailModel record = null;
                if (from == "signin")
                {
                    record = await this.userDetailRepository.Table.Where(x => x.MobileNo == mobileNo && !x.IsDelete).FirstOrDefaultAsync();
                }
                else
                {
                    record = await this.userDetailRepository.Table.Where(x => x.MobileNo == mobileNo).FirstOrDefaultAsync();
                }

                return record;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// update otp.
        /// </summary>
        /// <param name='id'>The Mobile Number.</param>
        /// <param name="otp">The otp.</param>
        /// <returns>Record Id</returns>
        public async Task<int> InsertOTP(int id, int otp)
        {
            try
            {
                var record = await this.userDetailRepository.Table.FirstOrDefaultAsync(x => x.Id == id && !x.IsDelete);
                record.OTP = otp.ToString();
                record.OtpExpiryDate = DateTime.Now;
                await this.userDetailRepository.UpdateAsync(record);
                return record.Id;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return 0;
            }
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="userDetail">The user detail.</param>
        /// <returns>
        /// Insert Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">userDetail</exception>
        public async Task<int> InsertAsync(UserDetailModel userDetail)
        {
            if (userDetail == null)
            {
                throw new ArgumentNullException("userDetail");
            }

            try
            {
                await this.userDetailRepository.InsertAsync(userDetail);
                return userDetail.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return 0;
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="userDetail">The user detail.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">userDetail</exception>
        public async Task<int> UpdateAsync(UserDetailModel userDetail)
        {
            if (userDetail == null)
            {
                throw new ArgumentNullException("userDetail");
            }

            return await this.userDetailRepository.UpdateAsync(userDetail);
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="userDetail">The user detail.</param>
        /// <returns>
        /// Delete Recored Async
        /// </returns>
        /// <exception cref="ArgumentNullException">userDetail</exception>
        public async Task<int> DeleteAsync(UserDetailModel userDetail)
        {
            if (userDetail == null)
            {
                throw new ArgumentNullException("userDetail");
            }

            return await this.userDetailRepository.DeleteAsync(userDetail);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="userid">The user detail identifier.</param>
        /// <returns>
        /// Get Record By Id
        /// </returns>
        public async Task<UserDetailModel> GetByIdAsync(int userid)
        {
            if (userid == 0)
            {
                return null;
            }

            return await this.userDetailRepository.Table.FirstOrDefaultAsync(m => m.Id == userid);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// Get All List
        /// </returns>
        public async Task<IList<UserDetailModel>> GetAllAsync()
        {
            return await this.userDetailRepository.Table.ToListAsync();
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Get All PaggedList
        /// </returns>
        public async Task<DataTableResult> GetAllAsync(DataTableParameter model)
        {
            var query = this.userDetailRepository.Table.OrderByDescending(x => x.CreatedDate);
            var records = await this.userDetailRepository.ToPagedListAsync(query, model);
            return records;
        }

        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <param name="from">From.</param>
        /// <param name="checkPassword">if set to <c>true</c> [check password].</param>
        /// <returns>
        /// LoginAsync
        /// </returns>
        public async Task<UserDetailModel> LoginAsync(string email, string password, string from, bool checkPassword = true)
        {
            UserDetailModel record = null;
            if (from == "register")
            {
                record = await this.userDetailRepository.Table.FirstOrDefaultAsync(x => x.EmailId.ToLower() == email.ToLower());
            }
            else
            {
                record = await this.userDetailRepository.Table.FirstOrDefaultAsync(x => x.EmailId.ToLower() == email.ToLower() && x.IsActive && !x.IsDelete);
            }

            return record == null ? null : ((record != null && !checkPassword) ? record : ((record.Password == password && record.IsActive && !record.IsDelete) ? record : null));
        }

        /// <summary>
        /// Logins via otp asynchronous.
        /// </summary>
        /// <param name="mobile">The email.</param>
        /// <param name="otp">The password.</param>
        /// <param name="checkotp">if set to <c>true</c> [check otp].</param>
        /// <returns>
        /// LoginAsync
        /// </returns>
        public async Task<UserDetailModel> LoginOTPAsync(string mobile, string otp, bool checkotp = true)
        {
            var record = await this.userDetailRepository.Table.FirstOrDefaultAsync(x => x.MobileNo == mobile && !x.IsDelete);
            return record == null ? null : (!checkotp ? record : ((record.OTP == otp && !record.IsDelete) ? record : null));
        }

        /// <summary>
        /// Determines whether [is duplicate asyc] [the specified name].
        /// </summary>
        /// <param name="emailId">EmailId</param>
        /// <returns>GetDuplicateAsync</returns>
        public async Task<bool> IsDuplicateAsync(string emailId)
        {
            var email =
              await this.userDetailRepository.Table.FirstOrDefaultAsync(x => x.EmailId == emailId && x.IsActive && !x.IsDelete);
            return email == null;
        }

        /// <summary>
        /// Determines whether [is duplicate asyc] [the specified name].
        /// </summary>
        /// <param name="mobile">mobile number</param>
        /// <returns>GetDuplicateAsync</returns>
        public async Task<bool> IsDuplicateMobileAsync(string mobile)
        {
            var mob =
              await this.userDetailRepository.Table.FirstOrDefaultAsync(x => x.MobileNo == mobile && x.IsActive && !x.IsDelete);
            return mob == null;
        }

        /// <summary>
        /// Logins via otp asynchronous.
        /// </summary>
        /// <param name="emailId">The email ID.</param>
        /// <returns>
        /// LoginAsync
        /// </returns>
        public async Task<UserDetailModel> GetUserRecordByEmailId(string emailId)
        {
            var record = await this.userDetailRepository.Table.FirstOrDefaultAsync(x => x.EmailId == emailId && x.IsActive && !x.IsDelete);
            return record;
        }

        /// <summary>
        /// Logins via otp asynchronous.
        /// </summary>
        /// <param name="mobile">The Mobile Number.</param>
        /// <returns>
        /// LoginAsync
        /// </returns>
        public async Task<UserDetailModel> GetUserRecordByMobile(string mobile)
        {
            var record = await this.userDetailRepository.Table.FirstOrDefaultAsync(x => x.MobileNo == mobile && x.IsActive && !x.IsDelete);
            return record;
        }

        /// <summary>
        /// Counts the asynchronous.
        /// </summary>
        /// <returns>Total Number of users</returns>
        public async Task<int> CountAsync()
        {
            return await this.userDetailRepository.Table.Where(u => !u.IsDelete).CountAsync();
        }

        /// <summary>
        /// Gets the user identifier asynchronous.
        /// </summary>
        /// <param name="emailId">The email identifier.</param>
        /// <returns>int</returns>
        public async Task<int> GetUserIdAsync(string emailId)
        {
            var result = await this.userDetailRepository.Table.Where(u => u.EmailId == emailId).FirstOrDefaultAsync();

            return result.Id;
        }

        /// <summary>
        /// gets the profile deatils.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns>
        /// getprofiledeatils
        /// </returns>
        public async Task<MyInformationViewModel> GetUserProfileByEmailId(string userid)
        {
            var query = await this.userDetailRepository.Table.Where(x => x.EmailId == userid).Select(x => new MyInformationViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                MobileNo = x.MobileNo,
                Gender = x.Gender,
                DateOfBirth = x.DateOfBirth,
                NationalityId = x.NationalityId,
                Address = x.Address,
                ZipCode = x.ZipCode,
                City = x.City,
                CountryId = x.CountryId,
                PhoneNumber = x.PhoneNumber,
                PassportNo = x.PassportNo,
                CountryofIssueId = x.CountryofIssueId,
                ExpiryDate = x.ExpiryDate
            }).FirstOrDefaultAsync();
            return query;
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="password">The password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>ChangePassword</returns>
        public async Task<bool> UserChangePassword(string userId, string password, string newPassword)
        {
            var record = await this.userDetailRepository.Table.FirstOrDefaultAsync(x => x.EmailId == userId && x.Password == password);
            if (record == null)
            {
                return false;
            }
            else
            {
                record.Password = newPassword;
                await this.userDetailRepository.UpdateAsync(record);
                return true;
            }
        }

        /// <summary>
        /// Users the set password.
        /// </summary>
        /// <param name="emailId">The email identifier.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>true or false</returns>
        public async Task<bool> UserSetPassword(string emailId, string newPassword)
        {
            var record = await this.userDetailRepository.Table.FirstOrDefaultAsync(x => x.EmailId == emailId);
            if (record == null)
            {
                return false;
            }
            else
            {
                record.Password = newPassword;
                await this.userDetailRepository.UpdateAsync(record);
                return true;
            }
        }

        /// <summary>
        /// Activates the user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>ActivateUser</returns>
        public async Task<UserDetailModel> ActivateUser(int id)
        {
            var record = await this.userDetailRepository.Table.FirstOrDefaultAsync(x => x.Id == id && !x.IsActive);
            if (record == null)
            {
                return null;
            }
            else
            {
                record.IsActive = true;
                await this.userDetailRepository.UpdateAsync(record);
                return record;
            }
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <param name="emailId">The email identifier.</param>
        /// <returns>Password</returns>
        public async Task<UserDetailModel> GetPassword(string emailId)
        {
            var record = await this.userDetailRepository.Table.FirstOrDefaultAsync(x => x.EmailId == emailId && x.IsActive);
            if (record == null)
            {
                return null;
            }
            else
            {
                return record;
            }
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <param name="userId">The User identifier.</param>
        /// <returns>Password</returns>
        public async Task<List<MyBookingsListViewModel>> GetDealBookingsByUserId(int userId)
        {
            try
            {
                List<MyBookingsListViewModel> models = new List<MyBookingsListViewModel>();
                List<BookingInformationModel> bookingInformationModel = await this.bookingInfomationRepository.Table.Where(x => x.CustomerId == userId && x.IsCompleted).ToListAsync();
                if (bookingInformationModel.Count > 0)
                {
                    foreach (var item in bookingInformationModel)
                    {
                        MyBookingsListViewModel model = this.dealPackageRepository.Table.Where(x => x.Id == item.DealId).Select(x => new MyBookingsListViewModel
                        {
                            BookingId = item.Id,
                            ReferenceNumber = item.ReferenceNumber,
                            CardImage = x.DealContentModels.Select(y => y.CardImg).FirstOrDefault(),
                            CheckInDate = item.Checkin,
                            CheckOutDate = item.Checkout,
                            DealName = x.Name,
                            Confirmed = item.IsConfirmed,
                            FlightsConfirmed = this.bookingFlightRepository.Table.Where(y => y.BookingId == item.Id).Count() > 0 ? this.bookingFlightRepository.Table.Where(y => y.BookingId == item.Id).All(y => y.TicketGenerated) : true,
                            DealId = item.DealId,
                            DealType = x.Type,
                            LocationNames = new List<string>(),
                            HotelName = string.Empty
                        }).FirstOrDefault();
                        models.Add(model);
                    }

                    for (int i = 0; i < models.Count; i++)
                    {
                        models[i].LocationNames = new List<string>();
                        if (models[i].DealType == 1)
                        {
                            int hotelId = this.dealPackageRepository.Table.Where(x => x.Id == models[i].DealId).Select(x => x.DealsNightModels.Select(y => y.DealsItineraryModels.Select(z => z.DealsInclusionModels.Select(a => a.VendorInfoId.Value).FirstOrDefault()).FirstOrDefault()).FirstOrDefault()).FirstOrDefault();
                            models[i].HotelName = this.hotelierRepository.Table.Where(x => x.Id == hotelId).Select(x => x.Name).FirstOrDefault();
                            models[i].LocationNames.Add(this.dealPackageRepository.Table.Where(x => x.Id == models[i].DealId).SelectMany(x => x.DealsDestinationModels.Join(this.packageCityRepository.Table, y => y.City, ci => ci.Id, (y, ci) => new { y, ci }).Select(k => k.ci.Name)).FirstOrDefault());
                            models[i].LocationNames.Add(this.dealPackageRepository.Table.Where(x => x.Id == models[i].DealId).SelectMany(x => x.DealsDestinationModels.Join(this.packageCountryRepository.Table, y => y.Country, co => co.Id, (y, co) => new { y, co }).Select(k => k.co.Name)).FirstOrDefault());
                        }
                        else
                        {
                            models[i].LocationNames.AddRange(this.dealPackageRepository.Table.Where(x => x.Id == models[i].DealId).SelectMany(x => x.DealsDestinationModels.Join(this.packageCountryRepository.Table, y => y.Country, co => co.Id, (y, co) => new { y, co }).Select(k => k.co.Name)).ToList());
                        }
                    }
                }

                return models;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return new List<MyBookingsListViewModel>();
            }
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        /// <param name="bookingId">The Booking identifier.</param>
        /// <returns>Password</returns>
        public async Task<MyBookingDescriptionViewModel> GetMyBookingDescriptionByBookingId(int bookingId)
        {
            MyBookingDescriptionViewModel model = new MyBookingDescriptionViewModel();
            model.bookingInformationViewModel = await this.bookingInfomationRepository.Table.Where(x => x.Id == bookingId).Select(x => new BookingInformationViewModel
            {
                Id = x.Id,
                ReferenceNumber = x.ReferenceNumber,
                BookedDate = x.BookedDate,
                Checkin = x.Checkin,
                Checkout = x.Checkout,
                Currency = x.Currency,
                DealId = x.DealId,
                CustomerId = x.CustomerId,
                Discount = x.Discount,
                DiscountAmount = x.DiscountAmount,
                DiscountApplied = x.DiscountAmount > 0 && string.IsNullOrEmpty(x.DiscountCoupon) && x.Discount > 0 ? true : false,
                DiscountType = x.DiscountType,
                DiscountCoupon = x.DiscountCoupon,
                Email = x.Email,
                FlightOptionAvailable = x.FlightOptionAvailable,
                FlightRequired = x.FlightRequired,
                FlightSuccessful = x.FlightSuccessful,
                FlightTraceId = x.FlightTraceId,
                GST = x.GST,
                HoldFromDate = x.HoldFromDate,
                HoldToDate = x.HoldToDate,
                IsCompleted = x.IsCompleted,
                IsConfirmed = x.IsConfirmed,
                IsHold = x.IsHold,
                LeadFullName = x.LeadFullName,
                MobileNumber = x.PhoneNumber,
                NightId = x.NightId,
                PackagePrice = x.PackagePrice,
                TaxAmount = x.TaxAmount,
                TotalAmount = x.TotalAmount,
                VisaOptionAvailable = x.VisaOptionAvailable,
                BookingSummarySerialized = x.BookingSummarySerialized
            }).FirstOrDefaultAsync();
            model.dealPackageViewModel = this.dealPackageRepository.Table.Where(x => x.Id == model.bookingInformationViewModel.DealId).Select(x => new DealsPackageViewModel
            {
                Code = x.Code,
                Name = x.Name,
                Id = x.Id,
                Type = x.Type,
                DealsHighlightViewModels = x.DealsHighlightModels.OrderBy(y => y.SortOrder).Select(y => new DealsHighlightViewModel { Title = y.Title, Description = y.Description, SortOrder = y.SortOrder, StarRating = y.StarRating }).ToList()
            }).FirstOrDefault();
            model.LocationNames = new List<string>();
            if (model.dealPackageViewModel.Type == 1)
            {
                int hotelId = this.dealPackageRepository.Table.Where(x => x.Id == model.dealPackageViewModel.Id).Select(x => x.DealsNightModels.Select(y => y.DealsItineraryModels.Select(z => z.DealsInclusionModels.Select(a => a.VendorInfoId.Value).FirstOrDefault()).FirstOrDefault()).FirstOrDefault()).FirstOrDefault();
                model.LocationNames.Add(this.hotelierRepository.Table.Where(x => x.Id == hotelId).Select(x => x.Name).FirstOrDefault());
                model.LocationNames.Add(this.dealPackageRepository.Table.Where(x => x.Id == model.dealPackageViewModel.Id).SelectMany(x => x.DealsDestinationModels.Join(this.packageCityRepository.Table, y => y.City, ci => ci.Id, (y, ci) => new { y, ci }).Select(k => k.ci.Name)).FirstOrDefault());
                model.LocationNames.Add(this.dealPackageRepository.Table.Where(x => x.Id == model.dealPackageViewModel.Id).SelectMany(x => x.DealsDestinationModels.Join(this.packageCountryRepository.Table, y => y.Country, co => co.Id, (y, co) => new { y, co }).Select(k => k.co.Name)).FirstOrDefault());
            }
            else
            {
                model.LocationNames.AddRange(this.dealPackageRepository.Table.Where(x => x.Id == model.dealPackageViewModel.Id).SelectMany(x => x.DealsDestinationModels.Join(this.packageCountryRepository.Table, y => y.Country, co => co.Id, (y, co) => new { y, co }).Select(k => k.co.Name)).ToList());
            }

            model.dealContentViewModel = this.dealContentRepository.Table.Where(x => x.PackageId == model.dealPackageViewModel.Id).Select(x => new DealsContentViewModel
            {
                LogoImg = x.LogoImg,
                BannerImg2x2_1 = x.BannerImg2x2_1,
                BannerImg2x2_2 = x.BannerImg2x2_2,
                BannerImg2x2_3 = x.BannerImg2x2_3,
                BannerImg2x2_4 = x.BannerImg2x2_4,
                BannerImg2x4 = x.BannerImg2x4,
                BannerImg4x4 = x.BannerImg4x4,
                CardImg = x.CardImg
            }).FirstOrDefault();
            model.dealsImageViewModels = this.dealImageRepository.Table.Where(x => x.PackageId == model.dealPackageViewModel.Id).OrderBy(x => x.SortOrder).Select(x => new DealsImageViewModel
            {
                Id = x.Id,
                Image = x.Image,
                SortOrder = x.SortOrder,
                Caption = x.Caption
            }).ToList();
            model.flightAvailable = model.bookingInformationViewModel.FlightOptionAvailable;
            model.flightRequired = model.bookingInformationViewModel.FlightRequired;
            model.bookingVisaViewModel = await this.bookingVisaRepository.Table.Where(x => x.BookingId == bookingId).Select(x => new BookingVisaViewModel
            {
                Id = x.Id,
                CountryName = this.dealVisaRepository.Table.Where(y => y.Id == x.VisaId).Join(this.packageCountryRepository.Table, v => v.CountryId, c => c.Id, (v, c) => new { v, c }).Select(y => y.c.Name).FirstOrDefault(),
                Count = x.Count,
                BookingId = x.BookingId,
                Markup = x.MarkUp,
                Price = x.Price,
                TotalAmount = x.TotalAmount,
                VisaId = x.VisaId
            }).ToListAsync();
            List<BookingHotelRoomModel> roomModels = this.bookingHotelRoomRepository.Table.Where(x => x.BookingId == bookingId).ToList();
            model.bookingHotelViewModel = new List<BookingHotelRoomViewModel>();
            if (model.dealPackageViewModel.Type == 1)
            {
                foreach (var item in roomModels)
                {
                    BookingHotelRoomViewModel hotelSubModel = new BookingHotelRoomViewModel
                    {
                        Id = item.Id,
                        RatePlanId = item.RatePlanId,
                        Adult = item.Adult,
                        BookingId = item.BookingId,
                        CheckinDate = item.CheckinDate,
                        CheckoutDate = item.CheckoutDate,
                        Child = item.Child,
                        hotelRoomConfigViewModel = JsonConvert.DeserializeObject<DealRoomConfigViewModel>(item.RoomConfigSerialized)
                    };
                    model.bookingHotelViewModel.Add(hotelSubModel);
                }
            }
            else
            {
                foreach (var item in roomModels)
                {
                    BookingHotelRoomViewModel hotelSubModel = new BookingHotelRoomViewModel
                    {
                        Id = item.Id,
                        RatePlanId = item.RatePlanId,
                        Adult = item.Adult,
                        BookingId = item.BookingId,
                        CheckinDate = item.CheckinDate,
                        CheckoutDate = item.CheckoutDate,
                        Child = item.Child,
                        tourRoomConfigViewModel = JsonConvert.DeserializeObject<DealTourHotelInfoViewModel>(item.RoomConfigSerialized)
                    };
                    model.bookingHotelViewModel.Add(hotelSubModel);
                }
            }

            if (model.bookingInformationViewModel.FlightOptionAvailable && model.bookingInformationViewModel.FlightRequired && model.bookingInformationViewModel.FlightSuccessful)
            {
                model.bookingFlightViewModel = this.bookingFlightRepository.Table.Where(x => x.BookingId == model.bookingInformationViewModel.Id).Select(x => new BookingFlightViewModel
                {
                    Id = x.Id,
                    BookingId = x.BookingId,
                    BookingDate = x.BookingDate,
                    DayOfItinerary = x.DayOfItinerary,
                    Destination = x.Destination,
                    ItineraryId = x.ItineraryId,
                    PNR = x.PNR,
                    FlightCabinClass = x.FlightCabinClass,
                    TBOBookingId = x.TBOBookingId,
                    TokenId = x.TokenId,
                    TraceId = x.TraceId
                }).ToList();
            }

            return model;
        }
    }
}
