// <copyright file="CurationsService.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using static HiTours.Core.Enums;

    /// <summary>
    /// PackageService
    /// </summary>
    /// <seealso cref="HiTours.Services.ICurationsService" />
    public class CurationsService : ICurationsService
    {
        private readonly IRepository<CurationsModel> curationRepository;
        private readonly IRepository<CurationsGridViewModel> curationGridModelRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurationsService" /> class.
        /// </summary>
        /// <param name="curationRepository">Curations Repo</param>
        /// <param name="curationGridModelRepository">Curation Grid Repo</param>
        public CurationsService(IRepository<CurationsModel> curationRepository, IRepository<CurationsGridViewModel> curationGridModelRepository)
        {
            this.curationGridModelRepository = curationGridModelRepository;
            this.curationRepository = curationRepository;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurationsService" /> class.
        /// </summary>
        /// <param name="model">Data Table</param>
        /// <param name="mode">Curation Mode</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public async Task<DataTableResult> GetAllCurationsAsync(DataTableParameter model, string mode)
        {
            var result = this.curationRepository.Table.Select(x => new CurationsGridViewModel
            {
                Id = x.Id,
                Image = x.Image,
                Line1 = x.Line1,
                Line2 = x.Line2,
                Line3 = x.Line3,
                Line4 = x.Line4,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                IsActive = x.IsActive,
                OneXOne = x.OneXOne,
                OneXTwo = x.OneXTwo,
                TwoXTwo = x.TwoXTwo,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate,
                Url = x.Url
            });

            if (mode == "1x1")
            {
                result = result.Where(x => x.OneXOne);
            }

            if (mode == "1x2")
            {
                result = result.Where(x => x.OneXTwo);
            }

            if (mode == "2x2")
            {
                result = result.Where(x => x.TwoXTwo);
            }

            return await this.curationGridModelRepository.ToPagedListAsync(result, model);
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="id">Data Table</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public async Task<CurationsModel> GetCurationByIdAsync(int id)
        {
            var result = await this.curationRepository.Table.Where(x => x.Id == id).FirstOrDefaultAsync();
            return result;
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public async Task<int> InsertAsync(CurationsModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Curation");
            }

            return await this.curationRepository.InsertAsync(model);
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public async Task<int> UpdateAsync(CurationsModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("Curation");
            }

            return await this.curationRepository.UpdateAsync(model);
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <returns>A <see cref="Task"/> Returns Result </returns>
        public CurationLayoutViewModel GetCurationMainPage()
        {
            var result = new CurationLayoutViewModel
            {
                OneXOne = this.curationRepository.Table.Where(x => x.OneXOne && x.IsActive).Select(x => new CurationsAddViewModel
                {
                    Id = x.Id,
                    Line1 = x.Line1,
                    Line2 = x.Line2,
                    Line3 = x.Line3,
                    Line4 = x.Line4,
                    Image = x.Image,
                    Url = x.Url
                }).Take(3).ToList(),
                OneXTwo = this.curationRepository.Table.Where(x => x.OneXTwo && x.IsActive).Select(x => new CurationsAddViewModel
                {
                    Id = x.Id,
                    Line1 = x.Line1,
                    Line2 = x.Line2,
                    Line3 = x.Line3,
                    Line4 = x.Line4,
                    Image = x.Image,
                    Url = x.Url
                }).FirstOrDefault(),
                TwoXTwo = this.curationRepository.Table.Where(x => x.TwoXTwo && x.IsActive).Select(x => new CurationsAddViewModel
                {
                    Id = x.Id,
                    Line1 = x.Line1,
                    Line2 = x.Line2,
                    Line3 = x.Line3,
                    Line4 = x.Line4,
                    Image = x.Image,
                    Url = x.Url
                }).FirstOrDefault()
            };
            return result;
        }
    }
}