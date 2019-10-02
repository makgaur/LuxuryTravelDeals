// <copyright file="SelectListController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Data;
    using HiTours.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// SelectListController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class SelectListController : Controller
    {
        /// <summary>
        /// The service provider
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        private readonly DataBaseContext dataBaseContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectListController" /> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <param name="dataBaseContext">The data base context.</param>
        public SelectListController(IServiceProvider serviceProvider, DataBaseContext dataBaseContext)
        {
            this.serviceProvider = serviceProvider;
            this.dataBaseContext = dataBaseContext;
        }

        /// <summary>
        /// Packages the type.
        /// </summary>
        /// <returns>json data for smart combo</returns>
        public JsonResult PackageTypeSelectList()
        {
            var service = this.serviceProvider.GetService<IPackageService>();

            return this.Json(string.Empty);
        }

        /// <summary>
        /// Packages the select list.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// json data for smart combo
        /// </returns>
        public async Task<JsonResult> Packages(string search, short page)
        {
            var packageService = this.serviceProvider.GetService<IPackageService>();
            var selectList = await packageService.GetDropDownListAsync(search ?? string.Empty, page);

            return this.Json(selectList.ToPaggedList());
        }

        /// <summary>
        /// Tours the packages.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>Tour package list of dropdown</returns>
        public async Task<JsonResult> TourPackages(string search, short page)
        {
            var packageService = this.serviceProvider.GetService<ITourPackageService>();
            var selectList = await packageService.GetDropDownListAsync(search ?? string.Empty, page);

            return this.Json(selectList.ToPaggedList());
        }

        /// <summary>
        /// Categorieses the specified search.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>json data for category list</returns>
        public async Task<JsonResult> Categories(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetCategorySelectListAsync(search ?? string.Empty, page);
            return this.Json(selectList.ToPaggedList());
        }

        /// <summary>
        /// Packages the select list.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// json data for smart combo
        /// </returns>
        public async Task<JsonResult> DealTypes(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetDealTypeSelectListAsync(search ?? string.Empty, page);
            return this.Json(selectList.ToPaggedList());
        }

        /// <summary>
        /// Packages the select list.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// json data for smart combo
        /// </returns>
        public async Task<JsonResult> RoomTypes(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetRoomTypesSelectListAsync(search ?? string.Empty, page);
            return this.Json(selectList.ToPaggedList());
        }

        /// <summary>
        /// Citieses the specified search.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryid">The countryid.</param>
        /// <returns>
        /// Cities
        /// </returns>
        public async Task<JsonResult> Cities(string search, short page, Guid countryid)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetCitySelectListAsync(search ?? string.Empty, page, countryid);
            return this.Json(selectList.ToPaggedList());
        }

        /// <summary>
        /// Countrieses the specified search.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// Package Country list
        /// </returns>
        public async Task<JsonResult> Countries(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetCountrySelectListAsync(search ?? string.Empty, page);
            return this.Json(selectList.ToPaggedList());
        }

        /// <summary>
        /// Filters the countries.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>
        /// Json list of string
        /// </returns>
        public async Task<JsonResult> FilterCountries(string search = "")
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetFilterCountrySelectListAsync(search);
            return this.Json(selectList);
        }

        /// <summary>
        /// Filters the city.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>List of city</returns>
        public async Task<JsonResult> FilterCity(string search = "")
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetFilterCitySelectListAsync(search);
            return this.Json(selectList);
        }

        /// <summary>
        /// Filters the hotels.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>List of city</returns>
        public async Task<JsonResult> FilterHotel(string search = "")
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetFilterHotelSelectListAsync(search);
            return this.Json(selectList);
        }

        /// <summary>
        /// Filters the product.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>List of product</returns>
        public async Task<JsonResult> FilterProduct(string search = "")
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetFilterProductSelectListAsync(search);
            return this.Json(selectList);
        }

        /// <summary>
        /// Filters the state.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>
        /// List of state
        /// </returns>
        public async Task<JsonResult> FilterState(string search = "")
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetFilterStateSelectListAsync(search);
            return this.Json(selectList);
        }

        /// <summary>
        /// Filters the region.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>
        /// List of state
        /// </returns>
        public async Task<JsonResult> FilterRegion(string search = "")
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetFilterRegionSelectListAsync(search);
            return this.Json(selectList);
        }

        /// <summary>
        /// Hotelses the specified search.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="cityid">The cityid.</param>
        /// <returns>Hotels</returns>
        public async Task<JsonResult> Hotels(string search, short page, Guid cityid)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetHoteSelectListAsync(search ?? string.Empty, page, cityid);
            return this.Json(selectList.ToPaggedList());
        }

        /// <summary>
        /// Hotels the validity.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="hotelid">The hotelid.</param>
        /// <returns>Hotel Validity List</returns>
        public async Task<JsonResult> HotelValidity(string search, short page, Guid hotelid)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetHotelValditySelectListAsync(search ?? string.Empty, page, hotelid);
            return this.Json(selectList.ToPaggedList());
        }

        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <param name="emailId">The email identifier.</param>
        /// <returns>user information</returns>
        public async Task<ViewModels.MyInformationViewModel> GetUserInformation(string emailId)
        {
            var masterService = this.serviceProvider.GetService<IUserDetailService>();
            var selectList = await masterService.GetUserProfileByEmailId(emailId);
            return selectList;
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GetFlightDestinationsList(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetFlightDestination(search ?? string.Empty, page);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.CityCode,
                Name = x.SearchIn
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GetFlightVendors(string search, short page)
        {
            var vendorService = this.serviceProvider.GetService<IVendorService>();
            var selectList = await vendorService.GetFlightVendors(search ?? string.Empty, page, 0);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id,
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GetAirports(string search, short page)
        {
            var dealService = this.serviceProvider.GetService<IDealService>();
            var selectList = await dealService.GetAirportsCodesDropdownAsync(search ?? string.Empty, page, string.Empty);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id,
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight contries.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>GetFlightContries</returns>
        public async Task<JsonResult> GetFlightContries(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetFlightCountries(search ?? string.Empty, page);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.CountryCode,
                Name = x.CountryName
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight cities.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countrycode">The countrycode.</param>
        /// <returns>
        /// GetFlightCities
        /// </returns>
        public async Task<JsonResult> GetFlightCities(string search, short page, string countrycode)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetFlightCitiesByCountry(search ?? string.Empty, page, countrycode);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.CityCode,
                Name = x.CityName + "(" + x.CityCode + ")"
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="country">Country</param>
        /// <returns>
        /// GetFlightDestinations
        /// </returns>
        [HttpGet]
        public async Task<JsonResult> GetCountryList(string search, short page, short country)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetPackageCountryListAsync(search ?? string.Empty, page, 0, 0);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="amenetiesId">Ameneties Identifier</param>
        /// <returns>
        /// GetFlightDestinations
        /// </returns>
        [HttpGet]
        public async Task<JsonResult> GetAmenetiesList(string search, short page, int? amenetiesId)
        {
            var hotelierService = this.serviceProvider.GetService<IHotelierService>();
            var selectList = await hotelierService.GetAmenitiesListAsync(search ?? string.Empty, page, null);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="amenetiesId">Ameneties Identifier</param>
        /// <returns>
        /// GetFlightDestinations
        /// </returns>
        [HttpGet]
        public async Task<JsonResult> GetHotelierAmenetiesList(string search, short page, int? amenetiesId)
        {
            var hotelierService = this.serviceProvider.GetService<IHotelierService>();
            var selectList = await hotelierService.GetHotelierAmenitiesListAsync(search ?? string.Empty, page, null, string.Empty);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="nightId">Ameneties Identifier</param>
        /// <returns>
        /// GetFlightDestinations
        /// </returns>
        [HttpGet]
        public async Task<JsonResult> GetInclusionHoteliersFromNightId(string search, short page, int nightId)
        {
            var dealService = this.serviceProvider.GetService<IDealService>();
            var selectList = await dealService.GetInclusionHoteliersFromNightId(search ?? string.Empty, page, nightId, null);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="propertyId">Property Type Identifier</param>
        /// <returns>
        /// GetFlightDestinations
        /// </returns>
        public async Task<JsonResult> GetPropertyType(string search, short page, int? propertyId)
        {
            var hotelierService = this.serviceProvider.GetService<IHotelierService>();
            var selectList = await hotelierService.GetHotelierPropertyTypeDropDownListAsync(search ?? string.Empty, page, 0);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryid">The countryid.</param>
        /// <param name="regionId">The regionId.</param>
        /// <returns>
        /// GetFlightDestinations
        /// </returns>
        public async Task<JsonResult> GetPackageCountryList(string search, short page, short countryid, short regionId)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetPackageCountryListAsync(search ?? string.Empty, page, 0, regionId);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the holiday menu country list.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryid">The countryid.</param>
        /// <param name="name">The region identifier.</param>
        /// <returns>GetHolidayMenuCountryList</returns>
        public async Task<JsonResult> GetHolidayMenuCountryList(string search, short page, short countryid, string name)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetHolidayMenuCountryListAsync(search ?? string.Empty, page, 0, name);
            return this.Json(selectList.Select(x => new Core.Dropdown
            {
                Id = x.Id,
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the tour package country by reagion identifier.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryid">The countryid.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <returns>GetTourPackageCountryByReagionId</returns>
        public async Task<JsonResult> GetTourPackageCountryByReagionId(string search, short page, short countryid, short regionId)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetTourPackageCountryByReagionId(search ?? string.Empty, page, regionId, countryid);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the tour package country by reagion identifier.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryid">The countryid.</param>
        /// <param name="stateid">The stateid.</param>
        /// <returns>
        /// GetTourPackageCountryByReagionId
        /// </returns>
        public async Task<JsonResult> GetTourPackageStatesByCountrId(string search, short page, short countryid, short stateid)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetTourPackageStatesByCountrId(search ?? string.Empty, page, countryid, stateid);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the tour package country by reagion identifier.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryid">The countryid.</param>
        /// <param name="stateid">The stateid.</param>
        /// <param name="cityid">The cityid.</param>
        /// <returns>
        /// GetTourPackageCountryByReagionId
        /// </returns>
        public async Task<JsonResult> GetTourPackageCityByCounryIdorStateIdAsync(string search, short page, short countryid, short stateid, short cityid)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetTourPackageCityByCounryIdorStateIdAsync(search ?? string.Empty, page, countryid, stateid, cityid);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the city by counry identifier asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="name">The name.</param>
        /// <param name="cityid">The cityid.</param>
        /// <returns>GetCityByCounryIdAsync</returns>
        public async Task<JsonResult> GetHolidayMenuCityByCounryIdAsync(string search, short page, string name, short cityid)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetCityByCounryIdAsync(search ?? string.Empty, page, name, cityid);
            return this.Json(selectList.Select(x => new Core.Dropdown
            {
                Id = x.Id,
                Name = x.Name,
            }).ToPaggedList());
        }

        /// <summary>
        /// GetPackagedCountryListByRegion
        /// </summary>
        /// <param name="search">search</param>
        /// <param name="page">page</param>
        /// <param name="regionId">regionId</param>
        /// <returns>CountryList</returns>
        public async Task<JsonResult> GetPackagedCountryListByRegion(string search, short page, short regionId)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetPackagedCountryListByRegionIdAsync(search ?? string.Empty, page, regionId);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryId">The countryId.</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GetPackageSateList(string search, short page, short countryId)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetPackageStateListAsync(search ?? string.Empty, page, 0, countryId);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryid">The country.</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GetPackageSateByCountryIdList(string search, short page, short countryid)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetPackageStateListAsync(search ?? string.Empty, page, countryid);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GetPackageCategoryList(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetPackageCategoryListAsync(search ?? string.Empty, page, 0);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GetPackageRegionList(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetPackageRegionListAsync(search ?? string.Empty, page, 0);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the holiday menu region list.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>List of Region</returns>
        public async Task<JsonResult> GetHolidayMenuRegionList(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetHolidayMenuRegionListAsync(search ?? string.Empty, page, 0);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GetPackageDealTypeList(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetPackageDealTypeListAsync(search ?? string.Empty, page, 0);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GetHoteliersForDeal(string search, short page)
        {
            var hotelierService = this.serviceProvider.GetService<IHotelierService>();
            var selectList = await hotelierService.GetActiveHoteliersForDeals(search ?? string.Empty, page, 0);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="destinationCities">Cities</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GetHoteliersForDealBasedOnDestinations(string search, short page, string destinationCities)
        {
            var hotelierService = this.serviceProvider.GetService<IHotelierService>();
            var selectList = await hotelierService.GetActiveHoteliersInCities(search ?? string.Empty, page, 0, string.IsNullOrEmpty(destinationCities) ? new List<int>() : destinationCities.Split(',').Select(x => Convert.ToInt32(x)).ToList());
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="cityId">The city identifier.</param>
        /// <returns>
        /// GetFlightDestinations
        /// </returns>
        public async Task<JsonResult> GetPackageHotelList(string search, short page, int cityId)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetPackageHotelListAsync(search ?? string.Empty, page, 0, cityId);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GetPackageTravelStyleList(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetPackageTravelStyleListAsync(search ?? string.Empty, page, 0);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GetPackageHoteRoomTypeList(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetPackageHotelRoomTypeListAsync(search ?? string.Empty, page, 0);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="roomTypeIds">Room Type IDs Comma Seperated</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GetPackageHoteRoomTypeListByTypeIds(string search, short page, string roomTypeIds)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            short[] roomTypeIdsCon;
            if (string.IsNullOrEmpty(roomTypeIds))
            {
                roomTypeIdsCon = new short[0];
            }
            else
            {
                roomTypeIdsCon = roomTypeIds.Split(',').ToList().Select(x => Convert.ToInt16(x)).ToArray();
            }

            var selectList = await masterService.GetPackageHotelRoomTypeListByIdsAsync(search ?? string.Empty, page, roomTypeIdsCon);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="packageId">Package Id</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GeDestinationForValidity(string search, short page, Guid packageId)
        {
            var destinationService = this.serviceProvider.GetService<IDestinationService>();
            var selectList = await destinationService.GetDestinationForValidityListAsync(search ?? string.Empty, page, 0, packageId);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="stateId">The stateId.</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GetPackageCityList(string search, short page, short stateId)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetPackageCityListAsync(search ?? string.Empty, page, 0, stateId);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the package city list.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryId">The state identifier.</param>
        /// <returns>
        /// GetPackageCityByCountryList
        /// </returns>
        public async Task<JsonResult> GetPackageCityListByCountry(string search, short page, short countryId)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetPackageCityListByCountryAsync(search ?? string.Empty, page, 0, countryId);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the package city list.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="cityId">The City identifier.</param>
        /// <returns>
        /// GetPackageCityByCountryList
        /// </returns>
        public async Task<JsonResult> GetPackageAreaListByCity(string search, short page, int cityId)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetAreaByCityIdAsync(search ?? string.Empty, page, cityId, 0);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight cities.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// GetFlightCities
        /// </returns>
        public async Task<JsonResult> GetVendorGroup(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IVendorService>();
            var selectList = await masterService.GetVendorGroupDropDownListAsync(search ?? string.Empty, page, null);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id,
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight cities.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="vendorTypeId">vendorType</param>
        /// <param name="vendorGroupId">vendorGroupId</param>
        /// <returns>
        /// GetFlightCities
        /// </returns>
        public async Task<JsonResult> GetVendorsByVendorType(string search, short page, int? vendorTypeId, int vendorGroupId)
        {
            var masterService = this.serviceProvider.GetService<IVendorService>();
            var selectList = await masterService.GetVendorByVendorType(search,  page, vendorTypeId, vendorGroupId);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id,
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight cities.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// GetVisaVendors
        /// </returns>
        public async Task<JsonResult> GetVendorForVisa(string search, short page)
        {
            var visaService = this.serviceProvider.GetService<IVisaService>();
            var selectList = await visaService.GetVendorVisaDropDownListAsync(search ?? string.Empty, page, null);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id,
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight cities.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// GetInsuranceVendors
        /// </returns>
        public async Task<JsonResult> GetVendorForInsurance(string search, short page)
        {
            var insuranceServices = this.serviceProvider.GetService<IInsuranceService>();
            var selectList = await insuranceServices.GetVendorInsuranceDropDownListAsync(search ?? string.Empty, page, null);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id,
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight cities.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// GetFlightCities
        /// </returns>
        public async Task<JsonResult> GetDesignation(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetDesignationMaster(search ?? string.Empty, page);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id,
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight cities.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// GetFlightCities
        /// </returns>
        public async Task<JsonResult> GetSalutation(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetSalutationMaster(search ?? string.Empty, page);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id,
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight cities.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// GetFlightCities
        /// </returns>
        public async Task<JsonResult> GetMarginTypes(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetMarginTypeMaster(search ?? string.Empty, page, 0);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id,
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight cities.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// GetFlightCities
        /// </returns>
        public async Task<JsonResult> GetVendorCategoryList(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IVendorService>();
            var selectList = await masterService.GetCategoryDropDownListAsync(search ?? string.Empty, page, null);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id,
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight cities.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="packageId">Package Id</param>
        /// <returns>
        /// GetFlightCities
        /// </returns>
        public async Task<JsonResult> GetRoomConfigListForPackage(string search, short page, int packageId)
        {
            var dealService = this.serviceProvider.GetService<IDealService>();
            var selectList = await dealService.GetRoomConfigDropDownListForRatePlanAsync(search ?? string.Empty, page, null, packageId);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id,
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight cities.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>
        /// GetFlightCities
        /// </returns>
        public async Task<JsonResult> GetCurrencyDropDownList(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IVendorService>();
            var selectList = await masterService.GetCurrencyDropDownListAsync(search ?? string.Empty, page, null);
            return this.Json(selectList.Select(x => new HiTours.Core.Dropdown
            {
                Id = x.Id,
                Name = x.Name
            }).ToPaggedList());
        }

        /// <summary>
        /// Gets the flight destinations.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>GetFlightDestinations</returns>
        public async Task<JsonResult> GetFlightDestinations(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetFlightDestination(search ?? string.Empty, page);
            return this.Json(selectList.ToPaggedList());
        }

        /// <summary>
        /// Statics the page masters.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>json result</returns>
        public async Task<JsonResult> StaticPageMasters(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetStaticPageMasterAsync(search ?? string.Empty, page);
            return this.Json(selectList.ToPaggedList());
        }

        /// <summary>
        /// Gets the detail.
        /// </summary>
        /// <param name="pageType">Type of the page.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>json result of SEO detail for Pages</returns>
        public async Task<JsonResult> GetDetail(string pageType, string pageId)
        {
            var masterService = this.serviceProvider.GetService<ISeoDetailServices>();
            var result = await masterService.GetByIdAsync(pageType, pageId);

            return this.Json(result);
        }

        /// <summary>
        /// Gets the detail.
        /// </summary>
        /// <param name="url">Deal URL.</param>
        /// <returns>json result of SEO detail for Pages</returns>
        public async Task<JsonResult> GetSeoDetail(string url)
        {
            ISeoDetailServices seoService = this.serviceProvider.GetService<ISeoDetailServices>();
            var result = await seoService.GetSeoDetail(url);

            return this.Json(result);
        }

        /// <summary>
        /// Gets the menu country city.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>json list of holiday for seo</returns>
        public async Task<JsonResult> GetMenuCountryCity(string search, short page)
        {
            var masterService = this.serviceProvider.GetService<IMasterService>();
            var selectList = await masterService.GetMenuCountryCityAsync(search ?? string.Empty, page);
            return this.Json(selectList.ToPaggedList());
        }

        /// <summary>
        /// Gets the menu country city.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>json list of holiday for seo</returns>
        public async Task<JsonResult> GetPromotionsList(string search, short page)
        {
            try
            {
                var promotionService = this.serviceProvider.GetService<IPackagePromotionService>();
                var selectList = await promotionService.GetDropdownPromotionListAsync(search ?? string.Empty, page, new List<int>());
                return this.Json(selectList.ToPaggedList());
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return null;
            }
        }
    }
}