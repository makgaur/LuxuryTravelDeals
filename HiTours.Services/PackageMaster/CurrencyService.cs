// <copyright file="CurrencyService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.ViewModels;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// CountryService
    /// </summary>
    public class CurrencyService : ICurrencyService
    {
        private readonly IRepository<CurrencyModel> currencyRepository;
        private readonly IRepository<PackageCurrencyViewModel> currencyViewModelRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrencyService" /> class.
        /// </summary>
        /// <param name="currencyRepository">The package image repository.</param>
        /// <param name="currencyViewModelRepository">Package Currency View Model Repo.</param>
        public CurrencyService(IRepository<CurrencyModel> currencyRepository, IRepository<PackageCurrencyViewModel> currencyViewModelRepository)
        {
            this.currencyRepository = currencyRepository;
            this.currencyViewModelRepository = currencyViewModelRepository;
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
            try
            {
                var query = this.currencyRepository.Table.Include(r => r.PackageCountry).Select(x => new
                PackageCurrencyViewModel
                {
                    Id = x.Id,
                    CountryName = x.PackageCountry.Name,
                    Code = x.Code,
                    Country = x.Country,
                    Symbol = x.Symbol,
                    Name = x.Name,
                    ExchangeRate = x.ExchangeRate == null ? 0 : Convert.ToDecimal(x.ExchangeRate),
                    IsActive = x.IsActive
                });
                var list = await this.currencyViewModelRepository.ToPagedListAsync(query, model);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="currencyId">The state identifier.</param>
        /// <returns>
        /// GetByIdAsync
        /// </returns>
        public async Task<CurrencyModel> GetByIdAsync(int currencyId)
        {
            if (currencyId == 0)
            {
                return new CurrencyModel { Id = 0 };
            }

            return await this.currencyRepository.Table.Select(x => new CurrencyModel
            {
                Code = x.Code,
                Country = x.Country,
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive,
                Symbol = x.Symbol,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate,
                ExchangeRate = x.ExchangeRate
            }).FirstOrDefaultAsync(m => m.Id == currencyId);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="currency">The Currency Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Package Currency</exception>
        public async Task<int> UpdateAsync(CurrencyModel currency)
        {
            try
            {
                if (currency == null)
                {
                    throw new ArgumentNullException("PackageCurrency");
                }

                return await this.currencyRepository.UpdateAsync(currency);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="currency">The Currency Records.</param>
        /// <returns>
        /// Update Record Async
        /// </returns>
        /// <exception cref="ArgumentNullException">Package Currency</exception>
        public async Task<int> AddAsync(CurrencyModel currency)
        {
            try
            {
                if (currency == null)
                {
                    throw new ArgumentNullException("PackageCurrency");
                }

                return await this.currencyRepository.InsertAsync(currency);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="currency">The category.</param>
        /// <returns>
        /// DeleteAsync
        /// </returns>
        /// <exception cref="ArgumentNullException">category</exception>
        public async Task<int> DeleteAsync(CurrencyModel currency)
        {
            if (currency == null)
            {
                throw new ArgumentNullException("currency");
            }

            return await this.currencyRepository.DeleteAsync(currency);
        }
    }
}
