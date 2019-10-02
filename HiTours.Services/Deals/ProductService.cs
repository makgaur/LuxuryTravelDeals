// <copyright file="ProductService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.ViewModels;
    using HiTours.ViewModels.Deals.Product;
    using HiTours.ViewModels.Deals.Product.Hotel;
    using HiTours.ViewModels.Deals.Product.Tour;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using static HiTours.Core.Enums;

    /// <summary>
    /// PackageService
    /// </summary>
    /// <seealso cref="HiTours.Services.IProductService" />
    public class ProductService : IProductService
    {
        private readonly IRepository<Dropdown> dropdownRespository;
        private readonly IRepository<DealsDepartureDatesModel> dealDepartureDateRespository;
        private readonly IRepository<DealsPackageModel> dealPackageRespository;
        private readonly IRepository<DealsNightModel> dealNightRespository;
        private readonly IRepository<DealsInclusionModel> dealInclusionRespository;
        private readonly IRepository<AmenitiesMasterModel> amenetiesRespository;
        private readonly IRepository<DealRoomConfigurationModel> dealRoomConfigurationRespository;
        private readonly IRepository<DealsRatePlanModel> dealRatePlanRespository;
        private readonly IRepository<PackageHotelRoomTypeModel> packageHotelRoomTypeRespository;
        private readonly IRepository<PackageCityModel> packageCityRespository;
        private readonly IRepository<PackageCountryModel> packageCountryRespository;
        private readonly IRepository<CurrencyModel> currencyRepository;
        private readonly IRepository<DealsReviewModel> dealReviewRespository;
        private readonly IRepository<HotelierInformationModel> hotelierInfoRepository;
        private readonly IRepository<DealInventoryModel> dealInventoryRepository;
        private readonly int localCountry = 61;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService" /> class.
        /// </summary>
        /// <param name="dealInventoryRepository">Deal Inventory Repository</param>
        /// <param name="dealDepartureDateRespository">Deal Departure Dates Repository</param>
        /// <param name="dealNightRespository">Deal Night Repo</param>
        /// <param name="packageCountryRespository">Package Country</param>
        /// <param name="packageCityRespository">Package City</param>
        /// <param name="amenetiesRespository">Ameneties Repository</param>
        /// <param name="dealInclusionRespository">Deal Inclusion Repository</param>
        /// <param name="packageHotelRoomTypeRespository">Package Hotel Room Type Repository</param>
        /// <param name="dealRatePlanRespository">Deal Rate Plan Repo</param>
        /// <param name="dealRoomConfigurationRespository">Deal Room Configuration Repo</param>
        /// <param name="dropdownRespository">Dropdown</param>
        /// <param name="dealReviewRespository">Deal Review Repository</param>
        /// <param name="dealPackageRespository">Deal Package Repo</param>
        /// <param name="hotelierInfoRepository">Hotelier Information Repository</param>
        /// <param name="currencyRepository">Currency Repository</param>
        public ProductService(
            IRepository<DealInventoryModel> dealInventoryRepository,
            IRepository<DealsDepartureDatesModel> dealDepartureDateRespository,
            IRepository<DealsNightModel> dealNightRespository,
            IRepository<PackageCountryModel> packageCountryRespository,
            IRepository<PackageCityModel> packageCityRespository,
            IRepository<AmenitiesMasterModel> amenetiesRespository,
            IRepository<DealsInclusionModel> dealInclusionRespository,
            IRepository<PackageHotelRoomTypeModel> packageHotelRoomTypeRespository,
            IRepository<DealsRatePlanModel> dealRatePlanRespository,
            IRepository<DealRoomConfigurationModel> dealRoomConfigurationRespository,
            IRepository<Dropdown> dropdownRespository,
            IRepository<DealsReviewModel> dealReviewRespository,
            IRepository<DealsPackageModel> dealPackageRespository,
            IRepository<HotelierInformationModel> hotelierInfoRepository,
            IRepository<CurrencyModel> currencyRepository)
        {
            this.dealInventoryRepository = dealInventoryRepository;
            this.dealDepartureDateRespository = dealDepartureDateRespository;
            this.dealReviewRespository = dealReviewRespository;
            this.currencyRepository = currencyRepository;
            this.dealNightRespository = dealNightRespository;
            this.packageCityRespository = packageCityRespository;
            this.packageCountryRespository = packageCountryRespository;
            this.amenetiesRespository = amenetiesRespository;
            this.dealInclusionRespository = dealInclusionRespository;
            this.packageHotelRoomTypeRespository = packageHotelRoomTypeRespository;
            this.dealRatePlanRespository = dealRatePlanRespository;
            this.dealRoomConfigurationRespository = dealRoomConfigurationRespository;
            this.hotelierInfoRepository = hotelierInfoRepository;
            this.dealPackageRespository = dealPackageRespository;
            this.dropdownRespository = dropdownRespository;
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="address">hotel address.</param>
        /// <param name="key">key.</param>
        /// <returns>
        /// Latitude  and Longitude
        /// </returns>
        public Tuple<decimal, decimal> GetLatLong(int id, string address, string key)
        {
            try
            {
                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}&key=" + key, Uri.EscapeDataString(address));
                WebRequest request = WebRequest.Create(requestUri);
                WebResponse response = request.GetResponse();
                XDocument xdoc = XDocument.Load(response.GetResponseStream());
                XElement result = xdoc.Element("GeocodeResponse").Element("result");
                XElement locationElement = result.Element("geometry").Element("location");
                decimal lat = decimal.Parse(locationElement.Element("lat").Value);
                decimal lng = decimal.Parse(locationElement.Element("lng").Value);
                var row = this.hotelierInfoRepository.Table.FirstOrDefault(x => x.Id == id);
                row.Lat = lat;
                row.Long = lng;
                this.hotelierInfoRepository.Update(row);
                Tuple<decimal, decimal> tuple = new Tuple<decimal, decimal>(lat, lng);
                return tuple;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return new Tuple<decimal, decimal>(0, 0);
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="dealId">Deal Id.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<int?> UpdateDealCounter(int dealId)
        {
            var record = await this.dealPackageRespository.Table.Where(x => x.Id == dealId).FirstOrDefaultAsync();
            record.ViewCount++;
            return await this.dealPackageRespository.UpdateAsync(record);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="url">The model.</param><param name="key">The map api key.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<HotelProductViewModel> GetHotelDealByUrl(string url, string key)
        {
            try
            {
                HotelProductViewModel model = new HotelProductViewModel();
                var dealId = await this.dealPackageRespository.Table.Where(x => x.Url == url && x.IsActive && !x.IsDeleted).Select(x => x.Id).FirstOrDefaultAsync();
                if (dealId > 0)
                {
                    int? hotelId = this.dealPackageRespository.Table.Where(x => x.Id == dealId).Select(x => x.DealsNightModels.Select(y => y.DealsItineraryModels.Select(z => z.DealsInclusionModels.Select(a => a.VendorInfoId).FirstOrDefault()).FirstOrDefault()).FirstOrDefault()).FirstOrDefault();
                    decimal? thislat = this.hotelierInfoRepository.Table.Where(z => z.Id == hotelId).Select(z => (z.Lat.HasValue ? z.Lat : 0)).FirstOrDefault();
                    decimal? thislong = this.hotelierInfoRepository.Table.Where(z => z.Id == hotelId).Select(z => (z.Long.HasValue ? z.Long : 0)).FirstOrDefault();
                    if (thislat == 0 && thislong == 0)
                    {
                        string thisaddress = this.hotelierInfoRepository.Table.Where(z => z.Id == hotelId).Select(z => (string.IsNullOrEmpty(z.Name) ? string.Empty : z.Name + ", ") + (string.IsNullOrEmpty(z.Address1) ? string.Empty : z.Address1 + ", ") + (string.IsNullOrEmpty(z.Address2) ? string.Empty : z.Address2 + ", ") + this.packageCityRespository.Table.Where(c => c.Id == z.City).Select(c => c.Name).FirstOrDefault() + ", " + this.packageCountryRespository.Table.Where(d => d.Id == z.Country).Select(d => d.Name).FirstOrDefault() + ", " + (string.IsNullOrEmpty(z.PostalCode) ? string.Empty : z.PostalCode)).FirstOrDefault();
                        Tuple<decimal, decimal> tuple = this.GetLatLong(hotelId.Value, thisaddress, key);
                        thislat = tuple.Item1;
                        thislong = tuple.Item2;
                    }

                    model = await this.dealPackageRespository.Table.Where(x => x.Id == dealId).Select(x => new HotelProductViewModel
                    {
                        Id = x.Id,
                        NightId = x.DealsNightModels.Select(y => y.Id).FirstOrDefault(),
                        ////FullAddress = this.hotelierInfoRepository.Table.Where(z => z.Id == hotelId).Select(z => z.Address1 + ", " + (string.IsNullOrEmpty(z.Address2) ? string.Empty : z.Address2 + ", ") + this.packageCityRespository.Table.Where(c => c.Id == z.City).Select(c => c.Name).FirstOrDefault() + ", " + this.packageCountryRespository.Table.Where(d => d.Id == z.Country).Select(d => d.Name).FirstOrDefault() + ", " + z.PostalCode).FirstOrDefault(),
                        Lat = thislat,
                        Long = thislong,
                        ////FullAddress = this.hotelierInfoRepository.Table.Where(z => z.Id == hotelId).Select(z => (string.IsNullOrEmpty(z.Name) ? string.Empty : z.Name + ", ") + (string.IsNullOrEmpty(z.Address1) ? string.Empty : z.Address1 + ", ") + (string.IsNullOrEmpty(z.Address2) ? string.Empty : z.Address2 + ", ") + this.packageCityRespository.Table.Where(c => c.Id == z.City).Select(c => c.Name).FirstOrDefault() + ", " + this.packageCountryRespository.Table.Where(d => d.Id == z.Country).Select(d => d.Name).FirstOrDefault() + (string.IsNullOrEmpty(z.PostalCode) ? string.Empty : z.PostalCode)).FirstOrDefault(),
                        InclusionId = x.DealsNightModels.Select(y => y.DealsItineraryModels.Select(z => z.DealsInclusionModels.Select(m => m.Id).FirstOrDefault()).FirstOrDefault()).FirstOrDefault(),
                        AboutHotel = x.DealContentModels.Select(z => z.About).FirstOrDefault(),
                        City = this.packageCityRespository.Table.Where(k => k.Id == this.hotelierInfoRepository.Table.Where(z => z.Id == hotelId).Select(z => z.City).FirstOrDefault()).Select(k => k.Name).FirstOrDefault(),
                        Country = this.hotelierInfoRepository.Table.Where(z => z.Id == hotelId).Select(z => z.Country).FirstOrDefault() == this.localCountry ? string.Empty : this.packageCountryRespository.Table.Where(k => k.Id == this.hotelierInfoRepository.Table.Where(z => z.Id == hotelId).Select(z => z.Country).FirstOrDefault()).Select(k => k.Name).FirstOrDefault(),
                        CountryId = x.DealsDestinationModels.Select(y => y.Country).FirstOrDefault(),
                        Name = x.Name,
                        MinimumLOS = x.DealsNightModels.Select(y => y.Value).FirstOrDefault(),
                        HotelName = this.hotelierInfoRepository.Table.Where(z => z.Id == hotelId).Select(z => z.Name).FirstOrDefault(),
                        Cleanlinessrating = x.DealContentModels.Select(y => y.OverallCleaninessRating).FirstOrDefault(),
                        Locationrating = x.DealContentModels.Select(y => y.OverallComfortRating).FirstOrDefault(),
                        Overallrating = x.DealContentModels.Select(y => y.OverallRating).FirstOrDefault(),
                        HotelLogo = x.DealContentModels.Select(z => z.LogoImg).FirstOrDefault(),
                        AboutImage = x.DealContentModels.Select(z => z.AboutImg).FirstOrDefault(),
                        TripadvisorLink = x.DealContentModels.Select(z => z.TAUrl).FirstOrDefault(),
                        TripadvisorLogo = string.Empty,
                        Valuerating = x.DealContentModels.Select(z => z.OverallValueRating).FirstOrDefault(),
                        Url = x.Url,
                        BannerViewModel = x.DealContentModels.Select(y => new ProductBannerViewModel
                        {
                            BannerImg2x2_1 = y.BannerImg2x2_1,
                            BannerImg2x2_2 = y.BannerImg2x2_2,
                            BannerImg2x2_3 = y.BannerImg2x2_3,
                            BannerImg2x4 = y.BannerImg2x4,
                            BannerImg4x4 = y.BannerImg4x4,
                            MoreImage = x.DealsImageModels.Select(z => z.Image).FirstOrDefault()
                        }).FirstOrDefault(),
                        HighlightsViewModels = x.DealsHighlightModels.OrderByDescending(y => y.SortOrder.HasValue).ThenBy(y => y.SortOrder).Select(y => new HotelHighlightsViewModel
                        {
                            Title = y.Title,
                            StarRating = y.StarRating,
                            Description = y.Description
                        }).ToList(),
                        HotelAmenetiesViewModels = this.hotelierInfoRepository.Table.Where(z => z.Id == hotelId).Select(z => z.HotelierAmenitiesModels.Select(y => new HotelAmenetiesViewModel
                        {
                            Image = y.AmenitiesMasterModel.Image,
                            Name = y.AmenitiesMasterModel.Name
                        }).ToList()).FirstOrDefault(),

                        AllReviewViewModels = x.DealsReviewModels.Where(y => y.IsActive).Select(y => new ProductReviewViewModel
                        {
                            Name = y.FName + " " + y.LName,
                            Review = y.Comment,
                            ReviewDate = y.CreatedDate,
                            UserRecommend = y.UserRecommend
                        }).ToList(),
                        ReviewCount = x.DealsReviewModels.Where(y => y.IsActive).Count(),
                        CardImage = x.DealContentModels.Select(y => y.CardImg).FirstOrDefault(),
                        ////RatePlanViewModels = this.De
                        RatePlanViewModels = x.DealsNightModels.SelectMany(y => y.DealsRatePlanModel.Where(k => k.IsActive && k.ValidTo >= DateTime.Now).Join(this.dealRoomConfigurationRespository.Table, drate => drate.RoomConfigId, droom => droom.Id, (drate, droom) => new { drate, droom }).Where(z => z.droom.IsActive).Select(z => z.drate).Select(k => new DealsRatePlanViewModel
                        {
                            Currency = k.Currency,
                            ExtraAdult = k.MarkUp != null && k.MarkUp != 0 ? k.ExtraAdult + ((Convert.ToDecimal(k.MarkUp) / 100) * k.ExtraAdult) : k.ExtraAdult,
                            ExtraChild_WB = k.MarkUp != null && k.MarkUp != 0 ? k.ExtraChild_WB + ((Convert.ToDecimal(k.MarkUp) / 100) * k.ExtraChild_WB) : k.ExtraChild_WB,
                            ExtraChild_NB = k.MarkUp != null && k.MarkUp != 0 ? k.ExtraChild_NB + ((Convert.ToDecimal(k.MarkUp) / 100) * k.ExtraChild_NB) : k.ExtraChild_NB,
                            ExtraInfant = k.MarkUp != null && k.MarkUp != 0 ? k.ExtraInfant + ((Convert.ToDecimal(k.MarkUp) / 100) * k.ExtraInfant) : k.ExtraInfant,
                            Price = k.MarkUp != null && k.MarkUp != 0 ? k.Price + ((Convert.ToDecimal(k.MarkUp) / 100) * k.Price) : k.Price,
                            Name = k.Name,
                            RackRate = k.MarkUp != null && k.MarkUp != 0 ? k.RackRate + ((Convert.ToDecimal(k.MarkUp) / 100) * k.RackRate) : k.RackRate,
                            RatePlanId = k.Id,
                            SingleSupplement = k.MarkUp != null && k.MarkUp != 0 ? k.SingleSupplement + ((Convert.ToDecimal(k.MarkUp) / 100) * k.SingleSupplement) : k.SingleSupplement,
                            ValidFrom = k.ValidFrom,
                            ValidTo = k.ValidTo,
                            DealInventoryModels = k.DealInventoryModels.Where(r => r.Date > DateTime.Now.Date).ToList()
                        })).ToList(),
                        DealVisaViewModels = x.DealsVisaModels.Where(y => y.IsActive && y.CountryId != null).Select(y => new DealVisaViewModel
                        {
                            Id = y.Id,
                            AdultPrice = y.AdultPrice != null ? Convert.ToDecimal(y.AdultPrice) : 0,
                            BufferDays = y.BufferDays != null ? Convert.ToInt32(y.BufferDays) : 0,
                            ChildPrice = y.ChildPrice != null ? Convert.ToDecimal(y.ChildPrice) : 0,
                            CountryId = Convert.ToInt16(y.CountryId),
                            CountryName = this.packageCountryRespository.Table.Where(z => z.Id == y.CountryId).Select(z => z.Name).FirstOrDefault(),
                            DocumentsRequired = y.DocumentsRequired,
                            GeneralPolicy = y.GeneralPolicy,
                            ProcessingTime = y.ProcessingTime,
                            PhotoSpecification = y.PhotoSpecification,
                            Markup = y.Markup
                        }).ToList(),
                        ProductReviewViewModels = x.DealsReviewModels.Where(y => y.IsActive).Select(y => new ProductReviewViewModel
                        {
                            Name = y.FName + " " + y.LName,
                            Review = y.Comment,
                            ReviewDate = y.CreatedDate,
                            UserRecommend = y.UserRecommend
                        }).ToList(),
                        ImageGalleryViewModel = new ImageGalleryViewModel
                        {
                            DealsImageViewModels = x.DealsImageModels.OrderByDescending(y => y.SortOrder.HasValue).ThenBy(y => y.SortOrder).Select(y => new DealsImageViewModel
                            {
                                Caption = y.Caption,
                                Id = y.Id,
                                Image = y.Image,
                                PackageId = y.Id
                            }).ToList(),
                            ProductReviewViewModels = new List<ProductReviewViewModel>()
                        },
                        FlightIncluded = x.DealsNightModels.SelectMany(y => y.DealsItineraryModels.SelectMany(z => z.DealsInclusionModels.Select(k => k.DealsFlightModels))).Any(),
                        PriceWithoutFlight = x.DealsNightModels
                        .SelectMany(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now).OrderBy(z => z.Price)
                        .Join(this.dealRoomConfigurationRespository.Table, drate => drate.RoomConfigId, droom => droom.Id, (drate, droom) => new { drate, droom })
                        .Where(z => z.droom.IsActive)
                        .Select(z => z.drate)
                        .Join(this.currencyRepository.Table, r => r.Currency, p => p.Id, (r, p) => new { r, p })
                        .Select(z => z.r.MarkUp != null && z.r.MarkUp != 0 ?
                                ((((z.r.Price + (Convert.ToDecimal(z.r.MarkUp) / 100 * z.r.Price)) * z.r.DealsNightModel.Value) / 2)
                                + //// Room Price + Extra Supplement
                                ((z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0) + ((Convert.ToDecimal(z.r.MarkUp) / 100) * (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0))))
                                *
                                Convert.ToDecimal(z.p.ExchangeRate)
                            :
                            z.r.DealsNightModel.DealsPackageModel.MarkUp != null && z.r.DealsNightModel.DealsPackageModel.MarkUp != 0 ?
                                    ((((z.r.Price + ((Convert.ToDecimal(z.r.DealsNightModel.DealsPackageModel.MarkUp) / 100) * z.r.Price)) * z.r.DealsNightModel.Value) / 2)
                                    + //// Room Price + Extra Supplement
                                    ((z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0) + ((Convert.ToDecimal(z.r.DealsNightModel.DealsPackageModel.MarkUp) / 100) * (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0))))
                                    *
                                    Convert.ToDecimal(z.p.ExchangeRate)
                                    :
                                    (((z.r.Price * z.r.DealsNightModel.Value) / 2)
                                    + //// Room Price + Extra Supplement
                                    (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0))
                                    * Convert.ToDecimal(z.p.ExchangeRate))).OrderBy(z => z).FirstOrDefault(),
                        RackPriceWithoutFlight = x.DealsNightModels
                        .SelectMany(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now)
                        .Join(this.dealRoomConfigurationRespository.Table, drate => drate.RoomConfigId, droom => droom.Id, (drate, droom) => new { drate, droom })
                        .Where(z => z.droom.IsActive)
                        .Select(z => z.drate)
                        .OrderBy(z => z.Price)
                        .Select(z => z.RackRate)).FirstOrDefault() != null
                            ?
                            x.DealsNightModels
                        .SelectMany(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now)
                        .Join(this.dealRoomConfigurationRespository.Table, drate => drate.RoomConfigId, droom => droom.Id, (drate, droom) => new { drate, droom })
                        .Where(z => z.droom.IsActive)
                        .Select(z => z.drate)
                        .Join(this.currencyRepository.Table, r => r.Currency, p => p.Id, (r, p) => new { r, p })
                        .Select(z => z.r.MarkUp != null && z.r.MarkUp != 0 ?
                                ((((z.r.RackRate.Value + (Convert.ToDecimal(z.r.MarkUp) / 100 * z.r.RackRate.Value)) * z.r.DealsNightModel.Value) / 2)
                                + //// Room Price + Extra Supplement
                                (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0 + ((Convert.ToDecimal(z.r.MarkUp) / 100) * (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0))))
                                *
                                Convert.ToDecimal(z.p.ExchangeRate)
                            :
                            z.r.DealsNightModel.DealsPackageModel.MarkUp != null && z.r.DealsNightModel.DealsPackageModel.MarkUp != 0 ?
                                    ((((z.r.RackRate.Value + ((Convert.ToDecimal(z.r.DealsNightModel.DealsPackageModel.MarkUp) / 100) * z.r.RackRate.Value)) * z.r.DealsNightModel.Value) / 2)
                                    + //// Room Price + Extra Supplement
                                    ((z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0) + ((Convert.ToDecimal(z.r.DealsNightModel.DealsPackageModel.MarkUp) / 100) * (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0))))
                                    *
                                    Convert.ToDecimal(z.p.ExchangeRate)
                                    :
                                    (((z.r.RackRate.Value * z.r.DealsNightModel.Value) / 2)
                                    + //// Room Price + Extra Supplement
                                    (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0))
                                    * Convert.ToDecimal(z.p.ExchangeRate))).OrderBy(z => z).FirstOrDefault()
                            :
                        x.DealsNightModels
                        .SelectMany(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now)
                        .Join(this.dealRoomConfigurationRespository.Table, drate => drate.RoomConfigId, droom => droom.Id, (drate, droom) => new { drate, droom })
                        .Where(z => z.droom.IsActive)
                        .Select(z => z.drate)
                        .Join(this.currencyRepository.Table, r => r.Currency, p => p.Id, (r, p) => new { r, p })
                        .Select(z => z.r.MarkUp != null && z.r.MarkUp != 0 ?
                                ((((z.r.Price + (Convert.ToDecimal(z.r.MarkUp) / 100 * z.r.Price)) * z.r.DealsNightModel.Value) / 2)
                                + //// Room Price + Extra Supplement
                                (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0 + ((Convert.ToDecimal(z.r.MarkUp) / 100) * (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0))))
                                *
                                Convert.ToDecimal(z.p.ExchangeRate)
                            :
                            z.r.DealsNightModel.DealsPackageModel.MarkUp != null && z.r.DealsNightModel.DealsPackageModel.MarkUp != 0 ?
                                    ((((z.r.Price + ((Convert.ToDecimal(z.r.DealsNightModel.DealsPackageModel.MarkUp) / 100) * z.r.Price)) * z.r.DealsNightModel.Value) / 2)
                                    + //// Room Price + Extra Supplement
                                    ((z.r.ExtraSupplement.HasValue ? z.r.ExtraSupplement.Value : 0) + ((Convert.ToDecimal(z.r.DealsNightModel.DealsPackageModel.MarkUp) / 100) * (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0))))
                                    *
                                    Convert.ToDecimal(z.p.ExchangeRate)
                                    :
                                    (((z.r.Price * z.r.DealsNightModel.Value) / 2)
                                    + //// Room Price + Extra Supplement
                                    (z.r.ExtraSupplement.HasValue ? (z.r.ExtraSupplement.Value / 2) : 0))
                                    * Convert.ToDecimal(z.p.ExchangeRate))).OrderBy(z => z).FirstOrDefault(),
                        DealsFlightViewModels = x.DealsNightModels.SelectMany(y => y.DealsItineraryModels.SelectMany(z => z.DealsInclusionModels.SelectMany(k => k.DealsFlightModels.Where(r => r.IsActive).Select(r => new DealsFlightViewModel
                        {
                            AllDay = r.AllDay,
                            CabinClass = r.CabinClass,
                            Destination = r.Destination,
                            Origin = r.Origin
                        })))).FirstOrDefault(),
                        TravelStyle = (x.TravelCategory.Contains(",2") || x.TravelCategory.Contains("2,") || x.TravelCategory.Contains("2,")) ? new Tuple<int, string>(2, "Flash Deals") : null
                    }).FirstOrDefaultAsync();
                }

                return model;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="url">The model.</param><param name="key">key.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<TourProductViewModel> GetTourDealByUrl(string url, string key)
        {
            try
            {
                TourProductViewModel model = new TourProductViewModel();
                var dealId = await this.dealPackageRespository.Table.Where(x => x.Url == url).Select(x => x.Id).FirstOrDefaultAsync();
                if (dealId > 0)
                {
                    List<int> availableHotels = this.dealNightRespository.Table.Where(x => x.PackageId == dealId).SelectMany(y => y.DealsItineraryModels.Where(z => z.IsActive).SelectMany(z => z.DealsInclusionModels.Where(k => k.TypeId == 1 && k.VendorInfoId != null).Select(k => Convert.ToInt32(k.VendorInfoId)).Distinct())).Distinct().ToList();
                    var idLatLong = new List<Tuple<int, decimal?, decimal?>>();
                    idLatLong = availableHotels != null && availableHotels.Count > 0 ? this.hotelierInfoRepository.Table.Where(y => availableHotels.Contains(y.Id)).Select(y => new Tuple<int, decimal?, decimal?>(y.Id, y.Lat.HasValue ? y.Lat : 0, y.Long.HasValue ? y.Long : 0)).ToList() : new List<Tuple<int, decimal?, decimal?>>();
                    var lL = new List<Tuple<decimal, decimal>>();
                    for (int i = 0; i < idLatLong.Count; ++i)
                    {
                        if (idLatLong[i].Item2 == 0 && idLatLong[i].Item3 == 0)
                        {
                            string thisaddress = this.hotelierInfoRepository.Table.Where(z => z.Id == idLatLong[i].Item1).Select(z => (string.IsNullOrEmpty(z.Name) ? string.Empty : z.Name + ", ") + (string.IsNullOrEmpty(z.Address1) ? string.Empty : z.Address1 + ", ") + (string.IsNullOrEmpty(z.Address2) ? string.Empty : z.Address2 + ", ") + this.packageCityRespository.Table.Where(c => c.Id == z.City).Select(c => c.Name).FirstOrDefault() + ", " + this.packageCountryRespository.Table.Where(d => d.Id == z.Country).Select(d => d.Name).FirstOrDefault() + (string.IsNullOrEmpty(z.PostalCode) ? string.Empty : z.PostalCode)).FirstOrDefault();
                            Tuple<decimal, decimal> tuple = this.GetLatLong(idLatLong[i].Item1, thisaddress, key);
                            lL.Add(new Tuple<decimal, decimal>(tuple.Item1, tuple.Item2));
                        }
                        else
                        {
                            lL.Add(new Tuple<decimal, decimal>(idLatLong[i].Item2.Value, idLatLong[i].Item3.Value));
                        }
                    }

                    // idLatLong = availableHotels != null && availableHotels.Count > 0 ? this.hotelierInfoRepository.Table.Where(y => availableHotels.Contains(y.Id) && y.Lat != null && y.Long != null).Select(y => new Tuple<int, decimal, decimal>(y.Id, Convert.ToDecimal(y.Lat), Convert.ToDecimal(y.Long))).ToList() : new List<Tuple<int, decimal, decimal>>();
                    // 3 possible cases 1.idLatLong have full details 2.only id 3.none
                    model = await this.dealPackageRespository.Table.Where(x => x.Id == dealId).Select(x => new TourProductViewModel
                    {
                        Id = x.Id,
                        LatLong = lL,
                        IsFixedDeparture = x.IsFixedDeparture,
                        //// FullAddress = availableHotels != null && availableHotels.Count > 0 ? this.hotelierInfoRepository.Table.Where(y => availableHotels.Contains(y.Id)).Select(y => (string.IsNullOrEmpty(y.Name) ? string.Empty : y.Name + ", ") + (string.IsNullOrEmpty(y.Address1) ? string.Empty : y.Address1 + ", ") + (string.IsNullOrEmpty(y.Address2) ? string.Empty : y.Address2 + ", ") + this.packageCityRespository.Table.Where(c => c.Id == y.City).Select(c => c.Name).FirstOrDefault() + ", " + this.packageCountryRespository.Table.Where(d => d.Id == y.Country).Select(d => d.Name).FirstOrDefault() + "," + (string.IsNullOrEmpty(y.PostalCode) ? string.Empty : y.PostalCode)).ToList() : new List<string>(),
                        NightIds = x.DealsNightModels.Select(y => y.Id).ToList(),
                        InclusionIds = x.DealsNightModels.SelectMany(y => y.DealsItineraryModels.SelectMany(z => z.DealsInclusionModels.Select(m => m.Id))).ToList(),
                        FlightInclusionId = x.DealsNightModels.SelectMany(y => y.DealsItineraryModels.SelectMany(z => z.DealsInclusionModels.Where(m => m.TypeId == 2).Select(m => m.Id))).FirstOrDefault(),
                        AboutDestination = x.DealContentModels.Select(z => z.About).FirstOrDefault(),
                        City = x.DealsDestinationModels.SelectMany(y => this.packageCityRespository.Table.Where(z => z.Id == y.City).Select(z => z.Name)).ToList(),
                        Name = x.Name,
                        MinimumLOS = x.DealsNightModels.OrderBy(y => y.Value).Select(y => y.Value).FirstOrDefault(),
                        Cleanlinessrating = x.DealContentModels.Select(y => y.OverallCleaninessRating).FirstOrDefault(),
                        Locationrating = x.DealContentModels.Select(y => y.OverallComfortRating).FirstOrDefault(),
                        Overallrating = x.DealContentModels.Select(y => y.OverallRating).FirstOrDefault(),
                        Valuerating = x.DealContentModels.Select(z => z.OverallValueRating).FirstOrDefault(),
                        Url = x.Url,
                        BannerViewModel = x.DealContentModels.Select(y => new ProductBannerViewModel
                        {
                            BannerImg2x2_1 = y.BannerImg2x2_1,
                            BannerImg2x2_2 = y.BannerImg2x2_2,
                            BannerImg2x2_3 = y.BannerImg2x2_3,
                            BannerImg2x4 = y.BannerImg2x4,
                            BannerImg4x4 = y.BannerImg4x4,
                            MoreImage = x.DealsImageModels.Select(z => z.Image).FirstOrDefault()
                        }).FirstOrDefault(),
                        HighlightsViewModels = x.DealsHighlightModels.OrderByDescending(y => y.SortOrder.HasValue).ThenBy(y => y.SortOrder).Select(y => new HotelHighlightsViewModel
                        {
                            Title = y.Title,
                            StarRating = y.StarRating,
                            Description = y.Description
                        }).ToList(),
                        ProductReviewViewModels = x.DealsReviewModels.Where(y => y.IsActive).Select(y => new ProductReviewViewModel
                        {
                            Name = y.FName + " " + y.LName,
                            Review = y.Comment,
                            ReviewDate = y.CreatedDate,
                            UserRecommend = y.UserRecommend
                        }).ToList(),
                        AllReviewViewModels = x.DealsReviewModels.Where(y => y.IsActive).Select(y => new ProductReviewViewModel
                        {
                            Name = y.FName + " " + y.LName,
                            Review = y.Comment,
                            ReviewDate = y.CreatedDate,
                            UserRecommend = y.UserRecommend
                        }).ToList(),
                        ReviewCount = x.DealsReviewModels.Where(y => y.IsActive).Count(),
                        CardImage = x.DealContentModels.Select(y => y.CardImg).FirstOrDefault(),
                        LogoImage = x.DealContentModels.Select(y => y.LogoImg).FirstOrDefault(),
                        RatePlanViewModels = x.DealsNightModels.Select(y => y.DealsRatePlanModel.Where(k => k.IsActive && k.ValidTo >= DateTime.Now).Select(k => new DealsRatePlanViewModel
                        {
                            Currency = k.Currency,
                            ExtraAdult = k.ExtraAdult,
                            ExtraChild_WB = k.ExtraChild_WB,
                            ExtraChild_NB = k.ExtraChild_NB,
                            ExtraInfant = k.ExtraInfant,
                            Price = k.Price,
                            Name = k.Name,
                            RackRate = k.RackRate,
                            RatePlanId = k.Id,
                            SingleSupplement = k.SingleSupplement,
                            ExtraSupplementPerHead = k.ExtraSupplementPerHead.HasValue ? k.ExtraSupplementPerHead.Value : 0,
                            MarkUp = k.MarkUp.HasValue && k.MarkUp.Value > 0 ? k.MarkUp.Value : k.DealsNightModel.DealsPackageModel.MarkUp.HasValue && k.DealsNightModel.DealsPackageModel.MarkUp.Value > 0 ? k.DealsNightModel.DealsPackageModel.MarkUp.Value : 0,
                            ValidFrom = k.ValidFrom,
                            ValidTo = k.ValidTo
                        }).ToList()).FirstOrDefault(),
                        DealVisaViewModels = x.DealsVisaModels.Where(y => y.IsActive && y.CountryId != null).Select(y => new DealVisaViewModel
                        {
                            Id = y.Id,
                            AdultPrice = y.AdultPrice != null ? Convert.ToDecimal(y.AdultPrice) : 0,
                            BufferDays = y.BufferDays != null ? Convert.ToInt32(y.BufferDays) : 0,
                            ChildPrice = y.ChildPrice != null ? Convert.ToDecimal(y.ChildPrice) : 0,
                            CountryId = Convert.ToInt16(y.CountryId),
                            Markup = y.Markup,
                            DocumentsRequired = y.DocumentsRequired,
                            GeneralPolicy = y.GeneralPolicy,
                            ProcessingTime = y.ProcessingTime,
                            PhotoSpecification = y.PhotoSpecification,
                            CountryName = this.packageCountryRespository.Table.Where(z => z.Id == y.CountryId).Select(z => z.Name).FirstOrDefault()
                        }).ToList(),
                        ImageGalleryViewModel = new ImageGalleryViewModel
                        {
                            DealsImageViewModels = x.DealsImageModels.OrderByDescending(y => y.SortOrder.HasValue).ThenBy(y => y.SortOrder).Select(y => new DealsImageViewModel
                            {
                                Caption = y.Caption,
                                Id = y.Id,
                                Image = y.Image,
                                PackageId = y.Id
                            }).ToList(),
                            ProductReviewViewModels = new List<ProductReviewViewModel>()
                        },
                        DealsFlightViewModels = x.DealsNightModels.SelectMany(y => y.DealsItineraryModels.SelectMany(z => z.DealsInclusionModels.SelectMany(k => k.DealsFlightModels.Where(r => r.IsActive).Select(r => new DealsFlightViewModel
                        {
                            Id = r.Id,
                            AllDay = r.AllDay,
                            CabinClass = r.CabinClass,
                            Destination = r.Destination,
                            Origin = r.Origin
                        })))).FirstOrDefault(),
                        DealsNightsViewModels = x.DealsNightModels.Select(y => new DealsNightViewModel
                        {
                            Id = y.Id,
                            Value = y.Value,
                            PackageId = x.Id,
                            VisaRequired = y.VisaRequired,
                            DealsItineraryViewModels = y.DealsItineraryModels.Where(z => z.IsActive).OrderBy(z => z.StartDay).Select(z => new DealsItineraryViewModel
                            {
                                Id = z.Id,
                                CardImg = z.CardImg,
                                Days = z.Days,
                                Description = z.Description,
                                EndDay = z.EndDay,
                                NightId = z.NightId,
                                Nights = z.Nights,
                                StartDay = z.StartDay,
                                Title = z.Title
                            }).ToList(),
                            DealsRatePlanViewModels = y.DealsRatePlanModel.Where(z => z.IsActive).Join(this.currencyRepository.Table, r => r.Currency, c => c.Id, (r, c) => new { r, c }).Select(z => new DealsRatePlanViewModel
                            {
                                Id = z.r.Id,
                                ExtraChild_WB = z.r.ExtraChild_WB * Convert.ToDecimal(z.c.ExchangeRate),
                                ExtraAdult = z.r.ExtraAdult * Convert.ToDecimal(z.c.ExchangeRate),
                                LOS = z.r.LOS,
                                MarkUp = z.r.MarkUp.HasValue && z.r.MarkUp != 0 ? z.r.MarkUp.Value : z.r.DealsNightModel.DealsPackageModel.MarkUp.HasValue && z.r.DealsNightModel.DealsPackageModel.MarkUp != 0 ? z.r.DealsNightModel.DealsPackageModel.MarkUp.Value : 0,
                                Name = z.r.Name,
                                ExtraInfant = z.r.ExtraInfant * Convert.ToDecimal(z.c.ExchangeRate),
                                SingleSupplement = z.r.SingleSupplement * Convert.ToDecimal(z.c.ExchangeRate),
                                Price = z.r.Price * Convert.ToDecimal(z.c.ExchangeRate),
                                RackRate = z.r.RackRate * Convert.ToDecimal(z.c.ExchangeRate),
                                ExtraSupplement = (z.r.ExtraSupplement.HasValue ? z.r.ExtraSupplement.Value : 0) * Convert.ToDecimal(z.c.ExchangeRate),
                            }).ToList()
                        }).ToList(),
                        FlightIncluded = x.DealsNightModels.OrderBy(y => y.Value).SelectMany(y => y.DealsItineraryModels.SelectMany(z => z.DealsInclusionModels.Select(k => k.DealsFlightModels))).Any(),
                        PriceWithoutFlight = x.DealsNightModels
                        .OrderBy(y => y.Value)
                        .SelectMany(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now)
                        .OrderBy(z => z.Price)
                        .Join(this.currencyRepository.Table, r => r.Currency, p => p.Id, (r, p) => new { r, p })
                        .Select(z => ((z.r.Price
                        +
                        (z.r.Price * (z.r.MarkUp.HasValue && z.r.MarkUp != 0 ? z.r.MarkUp.Value : (z.r.DealsNightModel.DealsPackageModel.MarkUp.HasValue ? z.r.DealsNightModel.DealsPackageModel.MarkUp.Value : 0)) / 100))
                        +
                        ((z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0)
                        +
                        ((z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0) * (z.r.MarkUp.HasValue && z.r.MarkUp != 0 ? z.r.MarkUp.Value : (z.r.DealsNightModel.DealsPackageModel.MarkUp.HasValue ? z.r.DealsNightModel.DealsPackageModel.MarkUp.Value : 0)) / 100))) * Convert.ToDecimal(z.p.ExchangeRate))).FirstOrDefault(),
                        RackPriceWithoutFlight = x.DealsNightModels.OrderBy(y => y.Value)
                        .SelectMany(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidTo >= DateTime.Now).OrderBy(z => z.Price).Select(z => z.RackRate)).FirstOrDefault() != null
                        ?
                        x.DealsNightModels.SelectMany(y => y.DealsRatePlanModel.Where(z => z.IsActive).OrderBy(z => z.Price)
                        .Join(this.currencyRepository.Table, r => r.Currency, p => p.Id, (r, p) => new { r, p })
                        .Select(z => ((z.r.RackRate.Value
                        +
                        (z.r.RackRate.Value * (z.r.MarkUp.HasValue && z.r.MarkUp != 0 ? z.r.MarkUp.Value : (z.r.DealsNightModel.DealsPackageModel.MarkUp.HasValue ? z.r.DealsNightModel.DealsPackageModel.MarkUp.Value : 0)) / 100))
                        +
                        ((z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0)
                        +
                        ((z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0) * (z.r.MarkUp.HasValue && z.r.MarkUp != 0 ? z.r.MarkUp.Value : (z.r.DealsNightModel.DealsPackageModel.MarkUp.HasValue ? z.r.DealsNightModel.DealsPackageModel.MarkUp.Value : 0)) / 100))) * Convert.ToDecimal(z.p.ExchangeRate))).FirstOrDefault()
                        :
                        x.DealsNightModels.SelectMany(y => y.DealsRatePlanModel.Where(z => z.IsActive && z.ValidFrom <= DateTime.Now && z.ValidTo >= DateTime.Now).OrderBy(z => z.Price)
                        .Join(this.currencyRepository.Table, r => r.Currency, p => p.Id, (r, p) => new { r, p })
                        .Select(z => (((z.r.Price
                        +
                        (z.r.Price * (z.r.MarkUp.HasValue && z.r.MarkUp != 0 ? z.r.MarkUp.Value : (z.r.DealsNightModel.DealsPackageModel.MarkUp.HasValue ? z.r.DealsNightModel.DealsPackageModel.MarkUp.Value : 0)) / 100)) / 2)
                        +
                        ((z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0)
                        +
                        ((z.r.ExtraSupplementPerHead.HasValue ? z.r.ExtraSupplementPerHead.Value : 0) * (z.r.MarkUp.HasValue && z.r.MarkUp != 0 ? z.r.MarkUp.Value : (z.r.DealsNightModel.DealsPackageModel.MarkUp.HasValue ? z.r.DealsNightModel.DealsPackageModel.MarkUp.Value : 0)) / 100))) * Convert.ToDecimal(z.p.ExchangeRate))).FirstOrDefault(),
                        TravelStyle = (x.TravelCategory.Contains(",2") || x.TravelCategory.Contains("2,") || x.TravelCategory.Contains("2")) ? new Tuple<int, string>(2, "Flash Deals") : null
                    }).FirstOrDefaultAsync();
                }

                return model;
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="dealId">Deal Id</param>
        /// <param name="nightId">Night Id</param>
        /// <param name="inclusionId">Inclusion Id</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<List<DealRoomConfigurationModel>> GetHotelRoomConfiguration(int dealId, int nightId, int inclusionId, DateTime startDate, DateTime endDate)
        {
            return await this.dealRoomConfigurationRespository.Table.Where(x => x.InclusionId == inclusionId && x.IsActive).ToListAsync();
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="dealId">Deal Id</param>
        /// <param name="nightId">Night Id</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<List<DealTourHotelInfoViewModel>> GetTourRatePlans(int dealId, int nightId, DateTime startDate, DateTime endDate)
        {
            try
            {
                List<DealTourHotelInfoViewModel> viewModel = new List<DealTourHotelInfoViewModel>();
                var ratePlans = this.dealRatePlanRespository.Table
                    .Where(y => y.NightId == nightId && y.IsActive && y.ValidFrom <= startDate && y.IsActive)
                    .OrderBy(y => y.Price)
                    .Include(x => x.DealsNightModel.DealsPackageModel)
                    .Join(this.currencyRepository.Table, r => r.Currency, p => p.Id, (r, p) => new { r, p })
                    .Join(this.dealInventoryRepository.Table, rpc => rpc.r.Id, inv => inv.RatePlanId, (rpc, inv) => new { rpc, inv })
                    .Where(c => c.inv.Date == startDate && c.inv.Inventory > 0 && !c.inv.BlackOut && c.inv.IsActive)
                    .ToList();
                if (ratePlans.Count > 0)
                {
                    foreach (var item in ratePlans)
                    {
                        List<int> inclusions = item.rpc.r.Inclusions.Split(',').Select(x => Convert.ToInt32(x)).Distinct().ToList();
                        List<Tuple<int, int>> inclusionHotels = await this.dealInclusionRespository.Table.Where(x => inclusions.Contains(x.Id) && x.VendorInfoId != null).Select(x => new Tuple<int, int>(x.Id, Convert.ToInt32(x.VendorInfoId))).ToListAsync();
                        List<int> inclusionsOnly = inclusionHotels.Select(y => y.Item2).ToList();
                        List<HotelierInformationModel> hotels = this.hotelierInfoRepository.Table.Include("HotelierContentsModels").Include("HotelierImageModels").Where(x => inclusionsOnly.Contains(x.Id)).OrderBy(x => x.StarRating).ToList();
                        HotelierInformationModel singleHotel = hotels.FirstOrDefault();
                        int singleHotelInclusion = inclusionHotels.Where(x => x.Item2 == singleHotel.Id).Select(x => x.Item1).FirstOrDefault();
                        DealRoomConfigurationModel dealRoomConfig = this.dealRoomConfigurationRespository.Table.Where(x => x.InclusionId == singleHotelInclusion).FirstOrDefault();
                        DealTourHotelInfoViewModel dealTourHotel = new DealTourHotelInfoViewModel
                        {
                            inclusionId = singleHotelInclusion,
                            HotelName = singleHotel.StarRating.ToString() + " Star Package",
                            HotelAmeneties = hotels
                            .Join(this.packageCityRespository.Table, hotel => hotel.City, city => city.Id, (hotel, city) => new { hotel, city })
                            .Select(x => x.hotel.Name + " " + ToUpperFirstLetter(x.city.Name.ToLower())).Take(6).ToList(),
                            HotelierId = singleHotel.Id,
                            Image = hotels.FirstOrDefault().HotelierContentsModels.Count > 0 ? hotels.FirstOrDefault().HotelierContentsModels.FirstOrDefault().CardImg : string.Empty,
                            Adults = dealRoomConfig.Adult,
                            Childs = dealRoomConfig.Child,
                            Infants = dealRoomConfig.Infant,
                            FreeChild = dealRoomConfig.FreeChild,
                            FreeInfant = dealRoomConfig.FreeInfant,
                            Description = hotels.FirstOrDefault().HotelierContentsModels.Count > 0 ? hotels.FirstOrDefault().HotelierContentsModels.FirstOrDefault().About : string.Empty,
                            Max = dealRoomConfig.Max,
                            Price = item.inv.Price.Value + Convert.ToInt32(item.rpc.p.ExchangeRate),
                            DealsRatePlanViewModels = new List<DealsRatePlanViewModel>(),
                            RoomImageGalleryViewModel = new RoomImageGalleryViewModel
                            {
                                HotelierRoomImageViewModels = hotels.SelectMany(x => x.HotelierImageModels).OrderBy(y => y.SortOrder).Select(y => new HotelierRoomImageViewModel
                                {
                                    Id = y.Id,
                                    Image = y.Image,
                                    Caption = y.Caption,
                                    SortOrder = y.SortOrder
                                }).ToList(),
                                ProductReviewViewModels = this.dealReviewRespository.Table.Where(y => y.IsActive && y.PackageId == dealId).Select(y => new ProductReviewViewModel
                                {
                                    Name = y.FName + " " + y.LName,
                                    Review = y.Comment,
                                    ReviewDate = y.CreatedDate,
                                    UserRecommend = y.UserRecommend
                                }).ToList()
                            }
                        };
                        dealTourHotel.DealsRatePlanViewModels.Add(new DealsRatePlanViewModel
                        {
                            Id = item.rpc.r.Id,
                            BufferDays = item.rpc.r.BufferDays,
                            Currency = item.rpc.r.Currency,
                            ExtraAdult = item.inv.ExtraAdult.Value * Convert.ToDecimal(item.rpc.p.ExchangeRate),
                            ExtraChild_NB = item.inv.ExtraChild_NB.Value * Convert.ToDecimal(item.rpc.p.ExchangeRate),
                            ExtraChild_WB = item.inv.ExtraChild_WB.Value * Convert.ToDecimal(item.rpc.p.ExchangeRate),
                            ExtraInfant = item.inv.ExtraInfant.Value * Convert.ToDecimal(item.rpc.p.ExchangeRate),
                            LOS = item.rpc.r.LOS,
                            MarkUp = item.rpc.r.MarkUp == 0 || item.rpc.r.MarkUp == null ? item.rpc.r.DealsNightModel.DealsPackageModel.MarkUp == 0 || item.rpc.r.DealsNightModel.DealsPackageModel.MarkUp == null ? 0 : item.rpc.r.DealsNightModel.DealsPackageModel.MarkUp : item.rpc.r.MarkUp,
                            Price = item.inv.Price.Value * Convert.ToDecimal(item.rpc.p.ExchangeRate),
                            SingleSupplement = item.inv.SingleSupplement.Value * Convert.ToDecimal(item.rpc.p.ExchangeRate),
                            ValidFrom = item.rpc.r.ValidFrom,
                            ValidTo = item.rpc.r.ValidTo,
                            RackRate = item.rpc.r.RackRate * Convert.ToDecimal(item.rpc.p.ExchangeRate),
                            Name = item.rpc.r.Name,
                            InventorySerialized = JsonConvert.SerializeObject(new DealInventoryModel
                            {
                                Date = item.inv.Date,
                                Id = item.inv.Id,
                                Price = item.inv.Price * Convert.ToDecimal(item.rpc.p.ExchangeRate),
                                BlackOut = item.inv.BlackOut,
                                Day = item.inv.Day,
                                Booking = item.inv.Booking,
                                ExtraAdult = item.inv.ExtraAdult * Convert.ToDecimal(item.rpc.p.ExchangeRate),
                                ExtraChild_NB = item.inv.ExtraChild_NB * Convert.ToDecimal(item.rpc.p.ExchangeRate),
                                CreatedBy = item.inv.CreatedBy,
                                ExtraChild_WB = item.inv.ExtraChild_WB * Convert.ToDecimal(item.rpc.p.ExchangeRate),
                                CreatedDate = item.inv.CreatedDate,
                                ExtraInfant = item.inv.ExtraInfant * Convert.ToDecimal(item.rpc.p.ExchangeRate),
                                Inventory = item.inv.Inventory,
                                IsActive = item.inv.IsActive,
                                RatePlanId = item.inv.RatePlanId,
                                SingleSupplement = item.inv.SingleSupplement * Convert.ToDecimal(item.rpc.p.ExchangeRate),
                                Surgcharge = item.inv.Surgcharge * Convert.ToDecimal(item.rpc.p.ExchangeRate),
                                UpdatedBy = item.inv.UpdatedBy,
                                UpdatedDate = item.inv.UpdatedDate
                            }),
                            ExtraSupplement = item.rpc.r.ExtraSupplement != null ? Convert.ToDecimal(item.rpc.r.ExtraSupplement) * Convert.ToDecimal(item.rpc.p.ExchangeRate) : 0,
                            ExtraSupplementPerHead = item.rpc.r.ExtraSupplementPerHead != null ? Convert.ToDecimal(item.rpc.r.ExtraSupplementPerHead) * Convert.ToDecimal(item.rpc.p.ExchangeRate) : 0
                        });
                        viewModel.Add(dealTourHotel);
                    }
                }

                return viewModel;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return new List<DealTourHotelInfoViewModel>();
            }
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="roomConfigId">RoomConfig Id</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<List<DealsRatePlanModel>> GetDealRatePlanByRoomConfig(int roomConfigId, DateTime startDate, DateTime endDate)
        {
            return await this.dealRatePlanRespository.Table.Where(x => x.RoomConfigId == roomConfigId && x.IsActive && (((startDate >= x.ValidFrom && startDate <= x.ValidTo) || (endDate <= x.ValidTo && endDate >= x.ValidFrom)) && !x.DealInventoryModels.Where(y => (y.Date >= startDate && y.Date <= endDate) && (y.BlackOut || y.Inventory == 0)).Any()))
                    .Join(this.currencyRepository.Table, r => r.Currency, p => p.Id, (r, p) => new { r, p })
                    .Select(x => new DealsRatePlanModel
                    {
                        BufferDays = x.r.BufferDays,
                        CreatedBy = x.r.CreatedBy,
                        CreatedDate = x.r.CreatedDate,
                        Currency = x.r.Currency,
                        ExtraAdult = x.r.ExtraAdult * Convert.ToDecimal(x.p.ExchangeRate),
                        ExtraChild_NB = x.r.ExtraChild_NB * Convert.ToDecimal(x.p.ExchangeRate),
                        ExtraChild_WB = x.r.ExtraChild_WB * Convert.ToDecimal(x.p.ExchangeRate),
                        ExtraInfant = x.r.ExtraInfant * Convert.ToDecimal(x.p.ExchangeRate),
                        ExtraSupplementPerHead = x.r.ExtraSupplementPerHead.HasValue ? x.r.ExtraSupplementPerHead.Value * Convert.ToDecimal(x.p.ExchangeRate) : 0,
                        Id = x.r.Id,
                        IsActive = x.r.IsActive,
                        LOS = x.r.LOS,
                        MarkUp = (x.r.MarkUp == 0 || x.r.MarkUp == null) ? x.r.DealsNightModel.DealsPackageModel.MarkUp == 0 || x.r.DealsNightModel.DealsPackageModel.MarkUp == null ? 0 : x.r.DealsNightModel.DealsPackageModel.MarkUp : x.r.MarkUp,
                        Name = x.r.Name,
                        RackRate = x.r.RackRate * Convert.ToDecimal(x.p.ExchangeRate),
                        NightId = x.r.NightId,
                        Price = x.r.Price * Convert.ToDecimal(x.p.ExchangeRate),
                        RoomConfigId = x.r.RoomConfigId,
                        SingleSupplement = x.r.SingleSupplement * Convert.ToDecimal(x.p.ExchangeRate),
                        UpdatedBy = x.r.UpdatedBy,
                        UpdatedDate = x.r.UpdatedDate,
                        ValidFrom = x.r.ValidFrom,
                        ValidTo = x.r.ValidTo,
                        DealInventoryModels = x.r.DealInventoryModels.Where(y => (y.Date >= startDate && y.Date < endDate) && y.IsActive).Select(y => new DealInventoryModel
                        {
                            Id = y.Id,
                            Inventory = y.Inventory,
                            BlackOut = y.BlackOut,
                            Booking = y.Booking,
                            Date = y.Date,
                            Day = y.Day,
                            ExtraAdult = y.ExtraAdult * Convert.ToDecimal(x.p.ExchangeRate),
                            ExtraChild_NB = y.ExtraChild_NB * Convert.ToDecimal(x.p.ExchangeRate),
                            ExtraChild_WB = y.ExtraChild_WB * Convert.ToDecimal(x.p.ExchangeRate),
                            ExtraInfant = y.ExtraInfant * Convert.ToDecimal(x.p.ExchangeRate),
                            Price = y.Price * Convert.ToDecimal(x.p.ExchangeRate),
                            RatePlanId = y.RatePlanId,
                            IsActive = y.IsActive,
                            Surgcharge = y.Surgcharge.HasValue ? y.Surgcharge.Value : 0,
                            SingleSupplement = y.SingleSupplement * Convert.ToDecimal(x.p.ExchangeRate)
                        }).ToList(),
                        ExtraSupplement = x.r.ExtraSupplement != null ? Convert.ToDecimal(x.r.ExtraSupplement) * Convert.ToDecimal(x.p.ExchangeRate) : 0
                    }).ToListAsync();
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="roomTypeId">Room Type Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<PackageHotelRoomTypeModel> GetRoomTypeRecord(int roomTypeId)
        {
            return await this.packageHotelRoomTypeRespository.Table.Where(x => x.Id == roomTypeId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="roomTypeId">Room Type Id</param>
        /// <param name="inclusionId">Inclusion Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<List<string>> GetRoomAmeneties(int roomTypeId, int inclusionId)
        {
            int hotelId = await this.dealInclusionRespository.Table.Where(x => x.Id == inclusionId).Select(x => Convert.ToInt32(x.VendorInfoId)).FirstOrDefaultAsync();
            List<int> roomAmeneties = await this.hotelierInfoRepository.Table.Where(x => x.Id == hotelId).SelectMany(x => x.HotelierRoomConfigModels.Where(y => y.IsActive).SelectMany(y => y.HotelierRoomAmenetiesModels.Select(z => z.AmenetieId))).Take(6).ToListAsync();
            return await this.amenetiesRespository.Table.Where(x => roomAmeneties.Contains(x.Id)).Select(x => x.Name).ToListAsync();
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="nightId">Night Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<List<DealsRatePlanViewModel>> GetDealRatePlanByNightId(int nightId)
        {
            return await this.dealRatePlanRespository.Table.Where(x => x.NightId == nightId && x.IsActive).Select(x => new DealsRatePlanViewModel
            {
                Id = x.Id,
                Price = x.Price,
                IsActive = x.IsActive,
                NightId = x.NightId,
                ExtraAdult = x.ExtraAdult,
                ExtraChild_NB = x.ExtraChild_NB,
                ExtraChild_WB = x.ExtraChild_WB,
                ExtraInfant = x.ExtraInfant,
                LOS = x.LOS,
                MarkUp = x.MarkUp,
                Name = x.Name,
                ValidFrom = x.ValidFrom,
                ValidTo = x.ValidTo,
                SingleSupplement = x.SingleSupplement,
                RackRate = x.RackRate,
                RoomConfigId = x.RoomConfigId,
                Currency = x.Currency,
                DealInventoryModels = x.DealInventoryModels.Where(y => y.Date > DateTime.Now).ToList()
            }).ToListAsync();
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="nightId">Night Id</param>
        /// <param name="bufferDays">Buffer Dayys</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<List<DealFixedDepartureDateViewModel>> GetDealFixedDepartureDates(int nightId, int bufferDays)
        {
            var dealDepartureDates = await (from depDates in this.dealDepartureDateRespository.Table
                                     join ratePlan in this.dealRatePlanRespository.Table on depDates.NightId equals ratePlan.NightId
                                     join inventory in this.dealInventoryRepository.Table on new { a = depDates.Date, b = ratePlan.Id } equals new { a = inventory.Date, b = inventory.RatePlanId }
                                     join currency in this.currencyRepository.Table on ratePlan.Currency equals currency.Id
                                     where depDates.NightId == nightId && depDates.Date > DateTime.Now.AddDays(bufferDays) && inventory.Inventory > 0 && !inventory.BlackOut && inventory.IsActive && ratePlan.IsActive && ratePlan.ValidTo > DateTime.Now.Date
                                    select new DealFixedDepartureDateViewModel
                                     {
                                         Id = depDates.Id,
                                         StartDate = depDates.Date,
                                         EndDate = depDates.Date.AddDays(depDates.DealsNightsModel.Value),
                                         IsSoldOut = false,
                                         StartingPrice = (inventory.Price.Value
                                            +
                                            ((inventory.Price.Value / 100) * ((ratePlan.MarkUp.HasValue && ratePlan.MarkUp != 0) ? ratePlan.MarkUp.Value : (ratePlan.DealsNightModel.DealsPackageModel.MarkUp.HasValue ? ratePlan.DealsNightModel.DealsPackageModel.MarkUp.Value : 0)))
                                            +
                                            (ratePlan.ExtraSupplement.HasValue ? ratePlan.ExtraSupplement.Value : 0)
                                            +
                                            ((ratePlan.ExtraSupplement.HasValue ? ratePlan.ExtraSupplement.Value / 100 : 0) * ((ratePlan.MarkUp.HasValue && ratePlan.MarkUp != 0) ? ratePlan.MarkUp.Value : (ratePlan.DealsNightModel.DealsPackageModel.MarkUp.HasValue ? ratePlan.DealsNightModel.DealsPackageModel.MarkUp.Value : 0)))) * Convert.ToDecimal(currency.ExchangeRate)
                                     }).OrderBy(x => x.StartingPrice).GroupBy(x => x.Id).Select(x => x.FirstOrDefault()).OrderBy(x => x.StartDate).ToListAsync();
            return dealDepartureDates;
        }

        private static string ToUpperFirstLetter(string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            //// convert to char array of the string
            char[] letters = source.ToCharArray();
            //// upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            ////return the array made of the new char array
            return new string(letters);
        }
    }
}