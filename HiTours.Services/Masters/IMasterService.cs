// <copyright file="IMasterService.cs" company="Luxury Travel Deals">
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
    /// IMasterService
    /// </summary>
    public interface IMasterService
    {
        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="roomtypeId">The roomtype identifier.</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        Task<IList<Dropdown>> GetPackageHotelRoomTypeListByIdsAsync(string search, short page, short[] roomtypeId);

        /// <summary>
        /// Gets the package travel style list asynchronous.
        /// </summary>
        /// <returns>List of Travel Style</returns>
        Task<List<PackageTravelStyleModel>> GetHomeTravelStyleListAsync();

        /// <summary>
        /// Gets the city by counry identifier asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="cityId">The cityid.</param>
        /// <param name="areaId">Area Id</param>
        /// <returns>
        /// GetCityByCounryIdAsync
        /// </returns>
        Task<IList<Dropdown>> GetAreaByCityIdAsync(string search, short page, int cityId, int areaId);

        /// <summary>
        /// Gets the filter region select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">Page</param>
        /// <param name="id">Margin Identifier</param>
        /// <returns>List of region</returns>
        Task<IList<Dropdown>> GetMarginTypeMaster(string search, short page, int id);

        /// <summary>
        /// Gets the package country by identifier asynchronous.
        /// </summary>
        /// <param name="stateId">The State Id.</param>
        /// <returns>GetPackageCountryByIdAsync</returns>
        Task<PackageStateModel> GetPackageStateByIdAsync(int stateId);

        /// <summary>
        /// Gets the filter region select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">Page</param>
        /// <returns>List of region</returns>
        Task<IList<Dropdown>> GetSalutationMaster(string search, short page);

        /// <summary>
        /// Gets the filter region select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">Page</param>
        /// <returns>List of region</returns>
        Task<IList<Dropdown>> GetDesignationMaster(string search, short page);

        /// <summary>
        /// Gets the filter region select list asynchronous.
        /// </summary>
        /// <param name="model">The Salutation Model</param>
        /// <returns>List of region</returns>
        Task<int?> AddDesignation(DesignationModel model);

        /// <summary>
        /// Gets the filter region select list asynchronous.
        /// </summary>
        /// <param name="model">The Salutation Model</param>
        /// <returns>List of region</returns>
        Task<int?> AddSalutation(SalutationModel model);

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="plantypeid">The plantypeid.</param>
        /// <returns>
        /// GetOptions for select list
        /// </returns>
        Task<IList<Dropdown>> GetDealTypeSelectListAsync(string search, short page, string plantypeid = "");

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="roomtypeid">The roomtypeid.</param>
        /// <returns>
        /// GetOptions for select list
        /// </returns>
        Task<IList<Dropdown>> GetRoomTypesSelectListAsync(string search, short page, string roomtypeid = "");

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="promotionTypeId">The promotionTypeId.</param>
        /// <returns>
        /// GetOptions for select list
        /// </returns>
        Task<IList<Dropdown>> GetPromotionTypeSelectListAsync(string search, short page, string promotionTypeId = "");

        /// <summary>
        /// Gets the category select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="categoryid">The categoryid.</param>
        /// <returns>Getoption category list</returns>
        Task<IList<Dropdown>> GetCategorySelectListAsync(string search, short page, string categoryid = "");

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryid">The countryid.</param>
        /// <param name="cityid">The cityid.</param>
        /// <returns>
        /// GetOptions for select list
        /// </returns>
        Task<IList<Dropdown>> GetCitySelectListAsync(string search, short page, Guid countryid, string cityid = "");

        /// <summary>
        /// Gets the hote select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="cityid">The cityid.</param>
        /// <param name="hotelid">The hotelid.</param>
        /// <returns>
        /// GetHoteSelectListAsync
        /// </returns>
        Task<IList<Dropdown>> GetHoteSelectListAsync(string search, short page, Guid cityid, string hotelid = "");

        /// <summary>
        /// Gets the hotel valdity select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="hotelid">The hotelid.</param>
        /// <param name="hotelvalidityid">The hotelvalidityid.</param>
        /// <returns>
        /// Get Hotel validity list
        /// </returns>
        Task<IList<Dropdown>> GetHotelValditySelectListAsync(string search, short page, Guid hotelid, string hotelvalidityid = "");

        /// <summary>
        /// Gets the country select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>Package country list</returns>
        Task<IList<Dropdown>> GetCountrySelectListAsync(string search, short page, string countryId = "");

        /// <summary>
        /// Gets the filter country select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>list of string</returns>
        Task<List<Tuple<string, int, int>>> GetFilterCountrySelectListAsync(string search);

       /////// <summary>
       /////// gets searchviewModels
       /////// </summary>
       /////// <param name="type">search type</param>
       /////// <param name="keyWord">search key word</param>
       /////// <returns>search view model</returns>
        ////Task<SearchTermViewModel> GetSearchTerm(string type, string keyWord);

        /// <summary>
        /// Gets the filter city select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>List of city</returns>
        Task<List<Tuple<string, int, int, string>>> GetFilterCitySelectListAsync(string search);

        /// <summary>
        /// Gets the filter state select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>List of state</returns>
        Task<IList<string>> GetFilterStateSelectListAsync(string search);

        /// <summary>
        /// Gets the filter Hotel select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>List of hotels</returns>
        Task<List<Tuple<string, int, int, string, string>>> GetFilterHotelSelectListAsync(string search);

        /// <summary>
        /// Gets the filter product select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>List of product</returns>
        Task<List<Tuple<string, int, int, string>>> GetFilterProductSelectListAsync(string search);

        /// <summary>
        /// Gets the filter region select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>List of region</returns>
        Task<IList<string>> GetFilterRegionSelectListAsync(string search);

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>GetFlightDestination</returns>
        Task<IList<CityView>> GetFlightDestination(string search, short page);

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="regionId">The regionId identifier.</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        Task<IList<Dropdown>> GetPackageCountryListAsync(string search, short page, short? countryId, short regionId = 0);

        /// <summary>
        /// Gets the holiday menu country list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <returns>GetHolidayMenuCountryListAsync</returns>
        Task<IList<Dropdown>> GetHolidayMenuCountryListAsync(string search, short page, short countryId, string regionId);

        /// <summary>
        /// GetPackagedCountryListByRegionIdAsync
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <returns>CountryList</returns>
        Task<IList<Dropdown>> GetPackagedCountryListByRegionIdAsync(string search, short page, short regionId);

        /// <summary>
        /// Gets the package state list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="countryId">The countryI identifier.</param>
        /// <returns>
        /// StateList
        /// </returns>
        Task<IList<Dropdown>> GetPackageStateListAsync(string search, short page, int? stateId, short countryId = 0);

        /// <summary>
        /// Gets the package category list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="hotecategoryid">The hotecategoryid.</param>
        /// <returns>GetPackageCategoryListAsync</returns>
        Task<IList<Dropdown>> GetPackageCategoryListAsync(string search, short page, int hotecategoryid);

        /// <summary>
        /// Gets the package city list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="cityId">The city identifier.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <returns>CityList</returns>
        Task<IList<Dropdown>> GetPackageCityListAsync(string search, short page, int cityId, short stateId);

        /// <summary>
        /// Gets the package city list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="cityId">The city identifier.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>GetPackageCityListByCountryAsync</returns>
        Task<IList<Dropdown>> GetPackageCityListByCountryAsync(string search, short page, int cityId, short countryId);

        /// <summary>
        /// Gets the static page master asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>static page master list</returns>
        Task<IList<Dropdown>> GetStaticPageMasterAsync(string search, short page);

        /// <summary>
        /// Gets the package region list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <returns>GetPackageRegionListAsync</returns>
        Task<IList<Dropdown>> GetPackageRegionListAsync(string search, short page, short? regionId);

        /// <summary>
        /// Gets the holiday menu region list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <returns>List of Region</returns>
        Task<IList<Dropdown>> GetHolidayMenuRegionListAsync(string search, short page, short? regionId);

        /// <summary>
        /// Gets the package hotel room type list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="roomtypeId">The roomtype identifier.</param>
        /// <returns>GetPackageHotelRoomTypeListAsync</returns>
        Task<IList<Dropdown>> GetPackageHotelRoomTypeListAsync(string search, short page, int roomtypeId);

        /// <summary>
        /// Gets the package deal type list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="dealId">The deal identifier.</param>
        /// <returns>Deal</returns>
        Task<IList<Dropdown>> GetPackageDealTypeListAsync(string search, short page, int dealId);

        /// <summary>
        /// Gets the package travel style list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="dealId">The deal identifier.</param>
        /// <returns>Travel Style</returns>
        Task<IList<Dropdown>> GetPackageTravelStyleListAsync(string search, short page, int dealId);

        /// <summary>
        /// Gets the package travel style list asynchronous.
        /// </summary>
        /// <returns>Travel Style Whole list</returns>
        Task<IList<Dropdown>> GetPackageTravelStyleListAsync();

        /// <summary>
        /// Gets the package holiday menu list asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// List of Holiday Menu
        /// </returns>
        Task<IList<Dropdown>> GetPackageHolidayMenuListAsync(string name = "");

        /// <summary>
        /// Gets the name of the flight destination by city.
        /// </summary>
        /// <param name="cityname">The cityname.</param>
        /// <returns>GetFlightDestinationByCityName</returns>
        Task<CityView> GetFlightDestinationByCityName(string cityname);

        /// <summary>
        /// Gets the flight destination by city code.
        /// </summary>
        /// <param name="citycode">The citycode.</param>
        /// <returns>GetFlightDestinationByCityCode</returns>
        Task<CityView> GetFlightDestinationByCityCode(string citycode);

        /// <summary>
        /// Gets the package hotel list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="hotelId">The hotel identifier.</param>
        /// <param name="cityid">The cityid.</param>
        /// <returns>Dropdown</returns>
        Task<IList<Dropdown>> GetPackageHotelListAsync(string search, short page, int hotelId, int cityid);

        /// <summary>
        /// Gets the flight countries.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countrycode">The countrycode.</param>
        /// <returns>
        /// GetFlightCountries
        /// </returns>
        Task<IList<CityView>> GetFlightCountries(string search, short page, string countrycode = "");

        /// <summary>
        /// Gets the flight cities by country.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countrycode">The countrycode.</param>
        /// <param name="citycode">The citycode.</param>
        /// <returns>
        /// GetFlightCitiesByCountry
        /// </returns>
        Task<IList<CityView>> GetFlightCitiesByCountry(string search, short page, string countrycode, string citycode = "");

        /// <summary>
        /// Gets the menu country city asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>Holday menu dropdown for seo</returns>
        Task<IList<Dropdown>> GetMenuCountryCityAsync(string search, short page);

        /// <summary>
        /// Gets the menu country city asynchronous.
        /// </summary>
        /// <param name="typeid">The identifier.</param>
        /// <returns>Holday menu dropdown for seo</returns>
        Task<PackageHotelRoomTypeModel> GetHotelRomeTypeByTypeIDAsync(short typeid);

        /// <summary>
        /// Gets the package country by identifier asynchronous.
        /// </summary>
        /// <param name="countryid">The countryid.</param>
        /// <returns>GetPackageCountryByIdAsync</returns>
        Task<PackageCountryModel> GetPackageCountryByIdAsync(short countryid);

        /// <summary>
        /// Gets the package city by identifier asynchronous.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns>GetPackageCityByIdAsync</returns>
        Task<PackageCityModel> GetPackageCityByIdAsync(short cityId);

        /// <summary>
        /// Gets the package country by identifier asynchronous.
        /// </summary>
        /// <param name="countrycode">The countrycode.</param>
        /// <returns>
        /// GetPackageCountryByIdAsync
        /// </returns>
        Task<PackageCountryModel> GetPackageCountryByCodeAsync(string countrycode);

        /// <summary>
        /// Gets the package city by identifier asynchronous.
        /// </summary>
        /// <param name="cityCode">The city code.</param>
        /// <returns>
        /// GetPackageCityByIdAsync
        /// </returns>
        Task<PackageCityModel> GetPackageCityByCodeAsync(string cityCode);

        /// <summary>
        /// Gets the tour package country by reagion identifier.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>GetTourPackageCountryByReagionId</returns>
        Task<IList<Dropdown>> GetTourPackageCountryByReagionId(string search, short page, int regionId, short countryId = 0);

        /// <summary>
        /// Gets the tour package states by countr identifier.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <returns>GetTourPackageStatesByCountrId</returns>
        Task<IList<Dropdown>> GetTourPackageStatesByCountrId(string search, short page, int? countryId, short stateId = 0);

        /// <summary>
        /// Gets the tour package city by counry idor state identifier asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="cityid">The cityid.</param>
        /// <returns>GetTourPackageCityByCounryIdorStateIdAsync</returns>
        Task<IList<Dropdown>> GetTourPackageCityByCounryIdorStateIdAsync(string search, short page, int? countryId, short stateId = 0, short cityid = 0);

        /// <summary>
        /// Gets the city by counry identifier asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="cityid">The cityid.</param>
        /// <returns>GetCityByCounryIdAsync</returns>
        Task<IList<Dropdown>> GetCityByCounryIdAsync(string search, short page, string countryId, short cityid = 0);

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="cityCodes">The city codes.</param>
        /// <returns>select by city codes</returns>
        Task<IList<CityView>> GetFlightDestination(string[] cityCodes);

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        Task<List<Dropdown>> GetAllPackageTravelStyleListAsync();

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="countryId">The Id</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        Task<VisaModel> GetVisaMasterByCountryIdAsyn(int countryId);

        /// <summary>
        /// GetHotelRomeTypeByTypeIDAsync.
        /// </summary>
        /// <param name="currencyId">The Currency ID.</param>
        /// <returns>Package Hotel RoomType Model</returns>
        Task<CurrencyModel> GetCurrencyByIdAsync(int currencyId);
    }
}