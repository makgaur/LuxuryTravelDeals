// <copyright file="IHolidayMenuService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    /// IHolidayService
    /// </summary>
    public interface IHolidayMenuService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="holidayMenu">The travelstyle.</param>
        /// <returns>
        /// Insert
        /// </returns>
        Task<int> InsertAsync(PackageHolidayMenuModel holidayMenu);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="holidayMenu">The travelstyle.</param>
        /// <returns>
        /// Update
        /// </returns>
        Task<int> UpdateAsync(PackageHolidayMenuModel holidayMenu);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// GetById
        /// </returns>
        Task<PackageHolidayMenuModel> GetByIdAsync(int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>GetAll</returns>
        Task<DataTableResult> GetAllAsync(DataTableParameter model);

        /// <summary>
        /// Determines whether [is duplicate asynchronous] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="holidayMenuId">The tarvelstyle identifier.</param>
        /// <returns>IsDuplicateAsync</returns>
        Task<bool> IsDuplicateAsync(string name, int holidayMenuId);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="holidayMenu">The travelstyle.</param>
        /// <returns>DeleteAsync</returns>
        Task<int> DeleteAsync(PackageHolidayMenuModel holidayMenu);
    }
}