// <copyright file="IStateService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;

    /// <summary>
    /// IStateService
    /// </summary>
    public interface IStateService
    {
        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>Insert</returns>
        Task<int> InsertAsync(PackageStateModel state);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>Update</returns>
        Task<int> UpdateAsync(PackageStateModel category);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        List<Tuple<PackageStateModel, int>> GetIndianStateWiseDealCount();

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="stateId">The state identifier.</param>
        /// <returns>GetById</returns>
        Task<PackageStateModel> GetByIdAsync(int stateId);

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
        /// <param name="stateId">The state identifier.</param>
        /// <returns>IsDuplicateAsync</returns>
        Task<bool> IsDuplicateAsync(string name, int stateId);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>DeleteAsync</returns>
        Task<int> DeleteAsync(PackageStateModel state);
    }
}
