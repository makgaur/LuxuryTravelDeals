// <copyright file="SeoDetailServices.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Seo Detail Services
    /// </summary>
    public class SeoDetailServices : ISeoDetailServices
    {
        private readonly IRepository<SeoDetailModel> seoDetailRepository;
        private readonly IRepository<DealsSeoDetail> dealSeoDetailRepository;
        private readonly IRepository<DealsPackageModel> dealPackageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeoDetailServices"/> class.
        /// </summary>
        /// <param name="seoDetailRepository">The seo detail repository.</param>
        /// <param name="dealSeoDetailRepository">The Deal seo detail repository.</param>
        /// <param name="dealPackageRepository">The Deal Package repository.</param>
        public SeoDetailServices(
            IRepository<SeoDetailModel> seoDetailRepository,
            IRepository<DealsSeoDetail> dealSeoDetailRepository,
            IRepository<DealsPackageModel> dealPackageRepository)
        {
            this.dealPackageRepository = dealPackageRepository;
            this.dealSeoDetailRepository = dealSeoDetailRepository;
            this.seoDetailRepository = seoDetailRepository;
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="seoDetail">The seo detail.</param>
        /// <returns>integer value</returns>
        public async Task<int> InsertAsync(SeoDetailModel seoDetail)
        {
            if (seoDetail == null)
            {
                throw new ArgumentNullException(nameof(seoDetail));
            }

            return await this.seoDetailRepository.InsertAsync(seoDetail);
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="seoDetail">The seo detail.</param>
        /// <returns>integer value</returns>
        public async Task<int> UpdateAsync(SeoDetailModel seoDetail)
        {
            if (seoDetail == null)
            {
                throw new ArgumentNullException(nameof(seoDetail));
            }

            return await this.seoDetailRepository.UpdateAsync(seoDetail);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="pageType">Type of the page.</param>
        /// <param name="pageId">The page identifier.</param>
        /// <returns>
        /// Seo
        /// </returns>
        public async Task<SeoDetailModel> GetByIdAsync(string pageType, string pageId)
        {
            if (string.IsNullOrEmpty(pageType) || string.IsNullOrEmpty(pageId))
            {
                return null;
            }

            return await this.seoDetailRepository.Table.FirstOrDefaultAsync(m => (pageType != Constants.Dynamic ? m.PageType == pageType : m.PageType == m.PageType) && m.PageId == pageId);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="url">The page identifier.</param>
        /// <returns>
        /// Seo
        /// </returns>
        public async Task<SeoDetailModel> GetSeoDetail(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            int dealId = this.dealPackageRepository.Table.Where(x => x.Url.Contains(url) && x.IsActive && !x.IsDeleted).Select(x => x.Id).FirstOrDefault();
            if (dealId != 0)
            {
                if (this.dealSeoDetailRepository.Table.Where(x => x.DealId == dealId).Count() > 0)
                {
                    return await this.dealSeoDetailRepository.Table.Where(x => x.DealId == dealId).Select(x => new SeoDetailModel
                    {
                        MetaDescription = x.MetaDescription,
                        MetaKeyword = x.MetaKeyword,
                        HeaderMetaCode = x.HeaderMetaCode,
                        Title = x.Title
                    }).FirstOrDefaultAsync();
                }
                else
                {
                    return new SeoDetailModel
                    {
                        MetaKeyword = "Luxury Travel, Luxury Travel Deals, Happy Holidays, Happiness, Awesome, Awesome Destinations, Authentic, Authentic Experiences, Holidays, Customised Holidays, Travel Deals, Luxury Holidays, Great Deals.",
                        Title = "Luxury Travel Deals - Happy holidays with awesome hotels and authentic experiences worldwide",
                        MetaDescription = "Great deals on awesome luxury hotels and customised tours worldwide. Flights Included. No Cost EMIs available. Or Deposit Now, Pay Later. Happy holidays to you!",
                        HeaderMetaCode = string.Empty
                    };
                }
            }
            else
            {
                return new SeoDetailModel
                {
                    MetaKeyword = "Luxury Travel, Luxury Travel Deals, Happy Holidays, Happiness, Awesome, Awesome Destinations, Authentic, Authentic Experiences, Holidays, Customised Holidays, Travel Deals, Luxury Holidays, Great Deals.",
                    Title = "Luxury Travel Deals - Happy holidays with awesome hotels and authentic experiences worldwide",
                    MetaDescription = "Great deals on awesome luxury hotels and customised tours worldwide. Flights Included. No Cost EMIs available. Or Deposit Now, Pay Later. Happy holidays to you!",
                    HeaderMetaCode = string.Empty
                };
            }
        }
    }
}