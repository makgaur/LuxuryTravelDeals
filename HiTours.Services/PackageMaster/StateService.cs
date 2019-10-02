// <copyright file="StateService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Api.Common.Data.SQLEF;
    using HiTours.Api.Common.Data.SQLServer;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// StateService
    /// </summary>
    public class StateService : IStateService
    {
        private readonly IRepository<PackageStateModel> stateRepository;
        private readonly IRepository<DealsPackageModel> packageRepository;
        private readonly IRepository<CurrencyModel> currencyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateService"/> class.
        /// </summary>
        /// <param name="currencyRepository">Currency Repos</param>
        /// <param name="stateRepository">The package image repository.</param>
        /// <param name="packageRepository">Package Repository</param>
        public StateService(IRepository<CurrencyModel> currencyRepository, IRepository<PackageStateModel> stateRepository, IRepository<DealsPackageModel> packageRepository)
        {
            this.currencyRepository = currencyRepository;
            this.packageRepository = packageRepository;
            this.stateRepository = stateRepository;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="state">The category.</param>
        /// <returns>
        /// InsertAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> InsertAsync(PackageStateModel state)
        {
            if (state == null)
            {
                throw new ArgumentNullException("category");
            }

            return await this.stateRepository.InsertAsync(state);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>
        /// UpdateAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> UpdateAsync(PackageStateModel category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            return await this.stateRepository.UpdateAsync(category);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="stateId">The state identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<PackageStateModel> GetByIdAsync(int stateId)
        {
            if (stateId == 0)
            {
                return null;
            }

            return await this.stateRepository.Table.FirstOrDefaultAsync(m => m.Id == stateId);
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public List<Tuple<PackageStateModel, int>> GetIndianStateWiseDealCount()
        {
            var dealQuery = this.packageRepository.Table.Include(x => x.DealsDestinationModels).Where(x => x.IsActive && !x.IsDeleted && (x.Type == 1 || x.Type == 2));

            SqlProcedureHandler<DataSet> sqlhandler = new SqlProcedureHandler<DataSet>(SqlHelper.connectionString);
            var res = sqlhandler.GetDataSetAsync("sp_statewisedeal", null);
            List<int> notToInclude = new List<int>();
            foreach (DataRow row in res.Result.Tables[0].Rows)
            {
                notToInclude.Add(Convert.ToInt32(row["Id"]));
            }

            dealQuery = dealQuery.Where(x => !notToInclude.Contains(x.Id));
            var dealsStates = dealQuery.SelectMany(x => x.DealsDestinationModels.Where(y => y.Country == 61).Select(y => y.State)).Distinct().ToList();
            List<Tuple<PackageStateModel, int>> result = new List<Tuple<PackageStateModel, int>>();
            var stateRepo = this.stateRepository.Table;
            if (dealsStates.Count > 0)
            {
                foreach (var item in dealsStates)
                {
                    result.Add(new Tuple<PackageStateModel, int>(stateRepo.Where(x => x.Id == item).FirstOrDefault(), dealQuery.Where(x => x.DealsDestinationModels.Select(y => y.State).Contains(item)).Count()));
                }
            }

            return result.OrderByDescending(x => x.Item2).ToList();
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// GetAllAsync
        /// </returns>
        public async Task<DataTableResult> GetAllAsync(DataTableParameter model)
        {
            var query = this.stateRepository.Table;
            var records = await this.stateRepository.ToPagedListAsync(query, model);
            return records;
        }

        /// <summary>
        /// Determines whether [is duplicate asyc] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <returns>
        /// GetDuplicateAsync
        /// </returns>
        public async Task<bool> IsDuplicateAsync(string name, int stateId)
        {
            var state = await this.stateRepository.Table.FirstOrDefaultAsync(x => x.Id != stateId && x.Name.ToLower().Trim() == name.ToLower().Trim());
            return state == null;
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> DeleteAsync(PackageStateModel state)
        {
            if (state == null)
            {
                throw new ArgumentNullException("roomtype");
            }

            return await this.stateRepository.DeleteAsync(state);
        }
    }
}
