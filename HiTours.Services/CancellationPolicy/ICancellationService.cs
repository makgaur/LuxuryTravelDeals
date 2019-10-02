// <copyright file="ICancellationService.cs" company="Luxury Travel Deals">
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
    /// Interface Hotelier Service
    /// </summary>
    /// <seealso cref="ICancellationService" />
    public interface ICancellationService
    {
        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="dealType">Deal Type</param>
        /// <returns>InformationModel</returns>
        Task<List<CancellationPolicyViewModel>> GetCancellationPolicyByDealType(int dealType);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        Task<IList<Dropdown>> GetMarginTypeItems();

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        /// <param name="id">Deal Type</param>
        Task<CancellationPolicyModel> GetCancellationPolicyById(int id);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        /// <param name="model">Deal Type</param>
        Task<int?> UpdateCancellationPolicy(CancellationPolicyModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        /// <param name="model">Deal Type</param>
        Task<int?> AddCancellationPolicy(CancellationPolicyModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        /// <param name="model">Deal Type</param>
        Task<int?> DeleteCancellationPolicy(CancellationPolicyModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <param name="hotelId">Hotel Id</param>
        /// <returns>InformationModel</returns>
        Task<List<HotelierCancellationPolicyViewModel>> GetHotelierCancellationPolicyByHotelId(int hotelId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        /// <param name="model">Deal Type</param>
        Task<int?> DeleteHotelierCancellationPolicy(HotelierCancellationPolicyModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        /// <param name="model">Deal Type</param>
        Task<int?> AddHotelierCancellationPolicy(HotelierCancellationPolicyModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        /// <param name="model">Deal Type</param>
        Task<int?> UpdateHotelierCancellationPolicy(HotelierCancellationPolicyModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        /// <param name="id">Deal Type</param>
        Task<HotelierCancellationPolicyModel> GetHotelierCancellationPolicyById(int id);

        /// <summary>
        /// get cancellations policy
        /// </summary>
        /// <param name="packageId">Pacakege Identifer</param>
        /// <param name="packageTypeId">Package Type Id</param>
        /// <returns>Deals Cacellation policy Model</returns>
        Task<List<DealsCancellationPolicyViewModel>> GetDealsCancellationPolicyByPackageId(int packageId, int packageTypeId);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        /// <param name="model">Deal Type</param>
        Task<int?> DeleteDealsCancellationPolicy(DealsCancellationPolicyModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        /// <param name="model">Deal Type</param>
        Task<int?> AddDealsCancellationPolicy(DealsCancellationPolicyModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        /// <param name="model">Deal Type</param>
        Task<int?> UpdateDealsCancellationPolicy(DealsCancellationPolicyModel model);

        /// <summary>
        /// get hotel information
        /// </summary>
        /// <returns>InformationModel</returns>
        /// <param name="id">Deal Type</param>
        Task<DealsCancellationPolicyModel> GetDealsCancellationPolicyById(int id);
    }
}
