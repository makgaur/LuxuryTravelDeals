// <copyright file="IListingService.cs" company="Luxury Travel Deals">
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
    using HiTours.ViewModels.Deals.Product.Hotel;

    /// <summary>
    /// IDestinationService
    /// </summary>
    public interface IListingService
    {
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="limit">Result Limit</param>
        /// <param name="offset">Result Offset</param>
        /// <param name="searchTerms">Search Criteria Values</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        List<PackageCurationViewModel> GetSearchListing(int limit, int offset, string searchTerms);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="dealIds">Deal Ids</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        List<PackageCurationViewModel> GetFilterSearchResults(List<int> dealIds);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<List<PackageCurationViewModel>> GetTop3FlashDeals();

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        List<PackageCurationViewModel> GetTop6TrendingDeals();

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// GetAllAsync
        /// </returns>
       Task<List<PackageCurationViewModel>> GetTop3DealsOfTheMonth();

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="ids">Result Limit</param>
        /// <param name="offset">Result Offset</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        List<PackageCurationViewModel> GetFlashDealsAsync(string ids, int offset);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="ids">Result Limit</param>
        /// <param name="offset">Result Offset</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        List<PackageCurationViewModel> GetDealsOfMonth(string ids, int offset);

        /////// <summary>
        /////// Gets all asynchronous.
        /////// </summary>
        /////// <param name="searchTermsViewModel">Search Criteria Values</param>
        /////// <returns>
        /////// GetAllAsync
        /////// </returns>
        ////List<PackageCurationViewModel> GetSearchResults(SearchTermViewModel searchTermsViewModel);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="listingViewModel">Search Criteria Values</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        ListingViewModel GetSearchResults(ListingViewModel listingViewModel);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="listingViewModel">Listing View Model</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        ListingViewModel GetSearchReFilterResults(ListingViewModel listingViewModel);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="cityId">City Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        List<PackageCurationViewModel> GetTop3CityDeals(int cityId);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="searchTerm">City Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<Tuple<int, int, bool>> GetValueListTypeOfSearchCityStateAsync(string searchTerm);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="searchTerm">City Id</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<Tuple<int, int, bool>> GetValueListTypeOfSearchCountryTravelStyleAsync(string searchTerm);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="searchTerm">Search Term</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        Task<Tuple<int, int, bool, string>> GetProductUrlAsync(string searchTerm);
    }
}