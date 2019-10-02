// <copyright file="MasterService.cs" company="Luxury Travel Deals">
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

    /// <summary>
    /// Master Service
    /// </summary>
    /// <seealso cref="HiTours.Services.IMasterService" />
    public class MasterService : IMasterService
    {
        /// <summary>
        /// The category repository
        /// </summary>
        private readonly IRepository<PlanTypeModel> planTypeRepository;

        /// <summary>
        /// The dropdown respository
        /// </summary>
        private readonly IRepository<Dropdown> dropdownRespository;
        private readonly IRepository<CurrencyModel> currencyRespository;

        /// <summary>
        /// The room type repository
        /// </summary>
        private readonly IRepository<RoomTypeModel> roomTypeRepository;

        /// <summary>
        /// The category repository
        /// </summary>
        private readonly IRepository<CategoryModel> categoryRepository;

        /// <summary>
        /// The city repository
        /// </summary>
        private readonly IRepository<CityModel> cityRepository;

        /// <summary>
        /// The city area repository
        /// </summary>
        private readonly IRepository<CityAreaModel> cityAreaRepository;

        /// <summary>
        /// The accomodation repository
        /// </summary>
        private readonly IRepository<AccommodationModel> accomodationRepository;

        /// <summary>
        /// The visa repository
        /// </summary>
        private readonly IRepository<VisaModel> visaRepository;

        /// <summary>
        /// The city repository
        /// </summary>
        private readonly IRepository<HotelModel> hotelRepository;

        /// <summary>
        /// The country repository
        /// </summary>
        private readonly IRepository<CountryModel> countryRepository;

        /// <summary>
        /// The state repository
        /// </summary>
        private readonly IRepository<StateModel> stateRepository;

        /// <summary>
        /// The hotel validity repository
        /// </summary>
        private readonly IRepository<HotelValidityModel> hotelValidityRepository;

        /// <summary>
        /// The package repository
        /// </summary>
        private readonly IRepository<PackageModel> packageRepository;

        /////// <summary>
        /////// The flightDestination repository
        /////// </summary>
        ////private readonly IRepository<FlightDestination> flightDestination;

        private readonly IRepository<CityView> cityView;

        /// <summary>
        /// The hotel price repository
        /// </summary>
        private readonly IRepository<HotelPriceModel> hotelPriceRepository;

        /// <summary>
        /// The category repository
        /// </summary>
        private readonly IRepository<PackageCountryModel> packageCountryRepository;

        /// <summary>
        /// The category repository
        /// </summary>
        private readonly IRepository<PackageStateModel> packageStateRepository;

        /// <summary>
        /// The category repository
        /// </summary>
        private readonly IRepository<PackageHotelCategoryModel> packagecategoryRepository;

        /// <summary>
        /// The category repository
        /// </summary>
        private readonly IRepository<PackageCityModel> packagecityRepository;
        private readonly IRepository<PackageAreaModel> packageAreaRepository;

        /// <summary>
        /// The static page master
        /// </summary>
        private readonly IRepository<StaticPageMasterModel> staticPageMasterRepository;

        /// <summary>
        /// The static page master
        /// </summary>
        private readonly IRepository<PackageRegionModel> packageregionRepository;

        /// <summary>
        /// The static page master
        /// </summary>
        private readonly IRepository<PackageHotelRoomTypeModel> packagehoteRoomTypeRepository;

        /// <summary>
        /// The static page master
        /// </summary>
        private readonly IRepository<PackageDealTypeModel> packagedealTypeRepository;

        /// <summary>
        /// The static page master
        /// </summary>
        private readonly IRepository<PackageTravelStyleModel> packagetravelstyleRepository;

        /// <summary>
        /// The packagetravelstyle repository
        /// </summary>
        private readonly IRepository<PackageHolidayMenuModel> packageHolidayMenuRepository;

        /// <summary>
        /// The static page master
        /// </summary>
        private readonly IRepository<PackageHotelModel> packagehotelRepository;

        /// <summary>
        /// The promotion Type Repository
        /// </summary>
        private readonly IRepository<PromotionTypeModel> promotionTypeRepository;

        /// <summary>
        /// The destination Type Repository
        /// </summary>
        private readonly IRepository<SalutationModel> salutationRepository;

        /// <summary>
        /// The destination Type Repository
        /// </summary>
        private readonly IRepository<DesignationModel> designationRepository;
        private readonly IRepository<PackageMarginTypeModel> marginTypeRepository;

        /// <summary>
        /// The  hotelier Information Repository
        /// </summary>
        private readonly IRepository<HotelierInformationModel> hotelierInformationRepository;

        /// <summary>
        /// The  deal hotel and tour Information Repository
        /// </summary>
        private readonly IRepository<DealsPackageModel> dealsPackageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MasterService" /> class.
        /// </summary>
        /// <param name="hotelierInformationRepository">hotelier information Repository</param>
        /// <param name="dealsPackageRepository">deals package Repository</param>
        /// <param name="currencyRespository">Currency Repository</param>
        /// <param name="packageAreaRepository">Package Area Repository</param>
        /// <param name="marginTypeRepository">Margin Type Repos</param>
        /// <param name="designationRepository">Designation Master Repo</param>
        /// <param name="salutationRepository">Salutation Master Repo</param>
        /// <param name="promotionTypeRepository">Promotion Type Repo</param>
        /// <param name="planTypeRepository">The plan repository.</param>
        /// <param name="packageHolidayMenuRepository">The package holiday menu repository.</param>
        /// <param name="hotelPriceRepository">The hotel price repository.</param>
        /// <param name="packageRepository">The package repository.</param>
        /// <param name="hotelValidityRepository">The hotel validity repository.</param>
        /// <param name="categoryRepository">The category repository.</param>
        /// <param name="stateRepository">The state repository.</param>
        /// <param name="dropdownRepository">The dropdown repository.</param>
        /// <param name="roomTypeRepository">The room type repository.</param>
        /// <param name="cityRepository">The city repository.</param>
        /// <param name="hotelRepository">The hotel repository.</param>
        /// <param name="cityAreaRepository">The city area repository.</param>
        /// <param name="accomodationRepository">The accomodation repository.</param>
        /// <param name="countryRepository">The country repository.</param>
        /// <param name="cityView">The city view.</param>
        /// <param name="packageCountryRepository">The package country repository.</param>
        /// <param name="packageStateRepository">The package state repository.</param>
        /// <param name="packagecategoryRepository">The packagecategory repository.</param>
        /// <param name="packagecityRepository">The packagecity repository.</param>
        /// <param name="staticPageMaster">The static page master.</param>
        /// <param name="packageregionRepository">The packageregion repository.</param>
        /// <param name="packagehoteRoomTypeRepository">The packagehote room type repository.</param>
        /// <param name="packagedealTypeRepository">The packagedeal type repository.</param>
        /// <param name="packagetravelstyleRepository">The packagetravelstyle repository.</param>
        /// <param name="packagehotelRepository">The packagehotel repository.</param>
        /// <param name="visaRepository">visaRepository</param>
        public MasterService(IRepository<HotelierInformationModel> hotelierInformationRepository, IRepository<DealsPackageModel> dealsPackageRepository, IRepository<CurrencyModel> currencyRespository, IRepository<PackageAreaModel> packageAreaRepository, IRepository<PackageMarginTypeModel> marginTypeRepository,  IRepository<VisaModel> visaRepository, IRepository<DesignationModel> designationRepository, IRepository<SalutationModel> salutationRepository, IRepository<PromotionTypeModel> promotionTypeRepository, IRepository<PlanTypeModel> planTypeRepository, IRepository<PackageHolidayMenuModel> packageHolidayMenuRepository, IRepository<HotelPriceModel> hotelPriceRepository, IRepository<PackageModel> packageRepository, IRepository<HotelValidityModel> hotelValidityRepository, IRepository<CategoryModel> categoryRepository, IRepository<StateModel> stateRepository, IRepository<Dropdown> dropdownRepository, IRepository<RoomTypeModel> roomTypeRepository, IRepository<CityModel> cityRepository, IRepository<HotelModel> hotelRepository, IRepository<CityAreaModel> cityAreaRepository, IRepository<AccommodationModel> accomodationRepository, IRepository<CountryModel> countryRepository, IRepository<CityView> cityView, IRepository<PackageCountryModel> packageCountryRepository, IRepository<PackageStateModel> packageStateRepository, IRepository<PackageHotelCategoryModel> packagecategoryRepository, IRepository<PackageCityModel> packagecityRepository, IRepository<StaticPageMasterModel> staticPageMaster, IRepository<PackageRegionModel> packageregionRepository, IRepository<PackageHotelRoomTypeModel> packagehoteRoomTypeRepository, IRepository<PackageDealTypeModel> packagedealTypeRepository, IRepository<PackageTravelStyleModel> packagetravelstyleRepository, IRepository<PackageHotelModel> packagehotelRepository)
        {
            this.hotelierInformationRepository = hotelierInformationRepository;
            this.dealsPackageRepository = dealsPackageRepository;
            this.currencyRespository = currencyRespository;
            this.packageAreaRepository = packageAreaRepository;
            this.marginTypeRepository = marginTypeRepository;
            this.designationRepository = designationRepository;
            this.salutationRepository = salutationRepository;
            this.planTypeRepository = planTypeRepository;
            this.dropdownRespository = dropdownRepository;
            this.roomTypeRepository = roomTypeRepository;
            this.stateRepository = stateRepository;
            this.cityRepository = cityRepository;
            this.hotelRepository = hotelRepository;
            this.cityAreaRepository = cityAreaRepository;
            this.accomodationRepository = accomodationRepository;
            this.countryRepository = countryRepository;
            this.categoryRepository = categoryRepository;
            this.hotelValidityRepository = hotelValidityRepository;
            this.packageRepository = packageRepository;
            this.cityView = cityView;
            this.hotelPriceRepository = hotelPriceRepository;
            this.packageCountryRepository = packageCountryRepository;
            this.packageStateRepository = packageStateRepository;
            this.packagecategoryRepository = packagecategoryRepository;
            this.packagecityRepository = packagecityRepository;
            this.staticPageMasterRepository = staticPageMaster;
            this.packageregionRepository = packageregionRepository;
            this.packagehoteRoomTypeRepository = packagehoteRoomTypeRepository;
            this.packagedealTypeRepository = packagedealTypeRepository;
            this.packagetravelstyleRepository = packagetravelstyleRepository;
            this.packagehotelRepository = packagehotelRepository;
            this.packageHolidayMenuRepository = packageHolidayMenuRepository;
            this.promotionTypeRepository = promotionTypeRepository;
            this.visaRepository = visaRepository;
        }

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="plantypeid">The plantypeid.</param>
        /// <returns>
        /// GetOptions for select list
        /// </returns>
        public async Task<IList<Dropdown>> GetDealTypeSelectListAsync(string search, short page, string plantypeid = "")
        {
            var query = this.planTypeRepository.Table
                            .Where(x => x.PlanType.StartsWith(search))
                            .OrderBy(x => x.PlanType)
                            .Select(x => new Dropdown(x.PlanTypeId.ToString(), x.PlanType));

            if (!string.IsNullOrEmpty(plantypeid) && plantypeid != Guid.Empty.ToString())
            {
                query = query.Where(x => x.Id == plantypeid);
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the category select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="categoryid">The categoryid.</param>
        /// <returns>
        /// Getoption category list
        /// </returns>
        public async Task<IList<Dropdown>> GetCategorySelectListAsync(string search, short page, string categoryid = "")
        {
            ////&& string.IsNullOrEmpty(categoryid) ? (x.IsActive && !x.IsDelete) : x.ID == x.ID
            var query = this.categoryRepository.Table
                            .Where(x => x.Name.StartsWith(search))
                            .OrderBy(x => x.Name).Select(x => x);

            ////.Select(x => new Dropdown { Id = x.ID.ToString(), Name = x.Name });

            if (!string.IsNullOrEmpty(categoryid) && categoryid != Guid.Empty.ToString())
            {
                query = query.Where(x => x.ID.ToString() == categoryid);
            }
            else
            {
                query = query.Where(x => x.IsActive && !x.IsDelete);
            }

            var result = query.Select(x => new Dropdown { Id = x.ID.ToString(), Name = x.Name });

            return await this.dropdownRespository.ToOptionListAsync(result, page);
        }

        /// <summary>
        /// Gets the Promotion Type select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="promotionTypeId">The Promotion Type ID.</param>
        /// <returns>
        /// Get option Promotion Type list
        /// </returns>
        public async Task<IList<Dropdown>> GetPromotionTypeSelectListAsync(string search, short page, string promotionTypeId = "")
        {
            ////&& string.IsNullOrEmpty(categoryid) ? (x.IsActive && !x.IsDelete) : x.ID == x.ID
            var query = this.promotionTypeRepository.Table
                            .OrderBy(x => x.Id).Select(x => x);

            if (!string.IsNullOrEmpty(promotionTypeId))
            {
                query = query.Where(x => x.Id.ToString() == promotionTypeId);
            }

            var result = query.Select(x => new Dropdown { Id = x.Id.ToString(), Name = x.Name });

            return await this.dropdownRespository.ToOptionListAsync(result, page);
        }

        /// <summary>
        /// Gets the options.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="roomtypeid">The roomtypeid.</param>
        /// <returns>
        /// GetOptions for select list
        /// </returns>
        public async Task<IList<Dropdown>> GetRoomTypesSelectListAsync(string search, short page, string roomtypeid = "")
        {
            var query = this.roomTypeRepository.Table
                            .Where(x => x.RoomType.StartsWith(search))
                            .OrderBy(x => x.RoomType)
                            .Select(x => new Dropdown { Id = x.RoomTypeId.ToString(), Name = x.RoomType });

            if (!string.IsNullOrEmpty(roomtypeid) && roomtypeid != Guid.Empty.ToString())
            {
                query = query.Where(x => x.Id == roomtypeid);
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

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
        public async Task<IList<Dropdown>> GetCitySelectListAsync(string search, short page, Guid countryid, string cityid = "")
        {
            var query = from state in this.stateRepository.Table
                        join city in this.cityRepository.Table on state.StateId equals city.StateId
                        where city.CityName.StartsWith(search) && state.CountryId == countryid
                        orderby city.CityName
                        select new Dropdown
                        {
                            Id = city.CityId.ToString(),
                            Name = city.CityName
                        };

            if (!string.IsNullOrEmpty(cityid) && cityid != Guid.Empty.ToString())
            {
                query = query.Where(x => x.Id == cityid);
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the country select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>
        /// Package country list
        /// </returns>
        public async Task<IList<Dropdown>> GetCountrySelectListAsync(string search, short page, string countryId = "")
        {
            var query = this.countryRepository.Table
            .Where(x => x.CountryName.StartsWith(search))
            .OrderBy(x => x.CountryName)
            .Select(x => new Dropdown
            {
                Id = x.CountryId.ToString(),
                Name = x.CountryName
            });

            if (!string.IsNullOrEmpty(countryId) && countryId != Guid.Empty.ToString())
            {
                query = query.Where(x => x.Id == countryId);
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the filter country select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>List of string</returns>
        public async Task<List<Tuple<string, int, int>>> GetFilterCountrySelectListAsync(string search)
        {
            var query = await this.packageCountryRepository.Table
            .Where(x => x.Name.Contains(search) && x.IsActive)
            .OrderBy(x => x.Name)
            .Select(x => new Tuple<string, int, int>(x.Name, x.Id, (int)Enums.SearchType.Country)).ToListAsync();

            return query;
        }

        /////// <summary>
        /////// gets searchviewModels
        /////// </summary>
        /////// <param name="type">search type</param>
        /////// <param name="keyWord">search key word</param>
        /////// <returns>search view model</returns>
        ////public async SearchTermViewModel GetSearchTerm(string type, string keyWord)
        ////{
        ////    SearchTermViewModel searchterm = new SearchTermViewModel();
        ////    return searchterm;
        ////}

        /// <summary>
        /// Gets the filter city select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>List of city</returns>
        public async Task<List<Tuple<string, int, int, string>>> GetFilterCitySelectListAsync(string search)
        {
            var query = await this.packagecityRepository.Table.Where(x => x.Name.Contains(search) && x.IsActive).Join(this.packageCountryRepository.Table, ci => ci.CountryId, co => co.Id, (ci, co) => new { ci, co })
                         .OrderBy(x => x.ci.Name)
                         .Select(x => new Tuple<string, int, int, string>(x.ci.Name, x.ci.Id, (int)Enums.SearchType.City, x.co.Name)).ToListAsync();
            return query;
        }

        /// <summary>
        /// Gets the filter hotel select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>List of hotels</returns>
        public async Task<List<Tuple<string, int, int, string, string>>> GetFilterHotelSelectListAsync(string search)
        {
            var query = await this.hotelierInformationRepository.Table.Where(x => x.Name.Contains(search) && x.IsActive && !x.IsDeleted)
                         .OrderBy(x => x.Name)
                         .Join(this.packageCountryRepository.Table, h => h.Country, c => c.Id, (h, c) => new { h, c })
                         .Join(this.packagecityRepository.Table, hot => hot.h.City, ci => ci.Id, (hot, ci) => new { hot, ci })
                         .Select(x => new Tuple<string, int, int, string, string>(x.hot.h.Name + ", " + x.ci.Name + ", " + x.hot.c.Name, x.hot.h.Id, (int)Enums.SearchType.Hotel, x.hot.c.Name, x.hot.h.Name)).ToListAsync();
            return query;
        }

        /// <summary>
        /// Gets the filter product select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>List of Products</returns>
        public async Task<List<Tuple<string, int, int, string>>> GetFilterProductSelectListAsync(string search)
        {
            var query = await this.dealsPackageRepository.Table.Where(x => x.Name.Contains(search) && x.IsActive && !x.IsDeleted && (x.Type == 1 || x.Type == 2))
                         .OrderBy(x => x.Name)
                         .Select(x => new Tuple<string, int, int, string>(x.Name, x.Id, (int)Enums.SearchType.Product, x.Type == 1 ? "Hotel" : "Holiday")).ToListAsync();
            return query;
        }

        /// <summary>
        /// Gets the filter state select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>List of State</returns>
        public async Task<IList<string>> GetFilterStateSelectListAsync(string search)
        {
            var query = from state in this.packageStateRepository.Table
                         join country in this.packageCountryRepository.Table on state.CountryId equals country.Id
                         where !this.packagecityRepository.Table.Any(ci => ci.Name == state.Name)
                         orderby state.Name
                         select state.Name + ", " + country.Name;
            ////.Except(from city in this.packagecityRepository.Table
            ////                                                            join country in this.packageCountryRepository.Table
            ////                                                            on city.CountryId equals country.Id
            ////                                                            select city.Name + ", " + country.Name);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Gets the filter region select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>List of region</returns>
        public async Task<IList<string>> GetFilterRegionSelectListAsync(string search)
        {
            var query = this.packageregionRepository.Table
           .Where(x => x.Name.StartsWith(search))
           .OrderBy(x => x.Name)
           .Select(x => x.Name);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Gets the filter region select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">Page</param>
        /// <param name="id">Margin Identifier</param>
        /// <returns>List of region</returns>
        public async Task<IList<Dropdown>> GetMarginTypeMaster(string search, short page, int id)
        {
            var query = this.marginTypeRepository.Table.Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name
            });
            if (id > 0)
            {
                query = query.Where(x => x.Id == id.ToString());
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search));
            }

            return await this.marginTypeRepository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the filter region select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">Page</param>
        /// <returns>List of region</returns>
        public async Task<IList<Dropdown>> GetSalutationMaster(string search, short page)
        {
            var query = this.salutationRepository.Table.Select(x => new Dropdown
            {
                Id = x.Name,
                Name = x.Name
            });
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search));
            }

            return await this.salutationRepository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the filter region select list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">Page</param>
        /// <returns>List of region</returns>
        public async Task<IList<Dropdown>> GetDesignationMaster(string search, short page)
        {
            var query = this.designationRepository.Table.Select(x => new Dropdown
            {
                Id = x.Name,
                Name = x.Name
            });
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search));
            }

            return await this.designationRepository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Add record in Vendor Package Relationship Table.
        /// </summary>
        /// <param name="countryId">The Id</param>
        /// <returns>
        /// Vendor id from vendor Package Relationship Table.
        /// </returns>
        /// <exception cref="ArgumentNullException">Vendor</exception>
        public async Task<VisaModel> GetVisaMasterByCountryIdAsyn(int countryId)
        {
            try
            {
                if (countryId == 0)
                {
                    throw new ArgumentNullException("Visa");
                }

                return await this.visaRepository.Table.Where(x => x.CountryId == countryId && x.IsActive == true).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the filter region select list asynchronous.
        /// </summary>
        /// <param name="model">The Salutation Model</param>
        /// <returns>List of region</returns>
        public async Task<int?> AddSalutation(SalutationModel model)
        {
            return await this.salutationRepository.InsertAsync(model);
        }

        /// <summary>
        /// Gets the filter region select list asynchronous.
        /// </summary>
        /// <param name="model">The Salutation Model</param>
        /// <returns>List of region</returns>
        public async Task<int?> AddDesignation(DesignationModel model)
        {
            return await this.designationRepository.InsertAsync(model);
        }

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
        public async Task<IList<Dropdown>> GetHoteSelectListAsync(string search, short page, Guid cityid, string hotelid = "")
        {
            var cityAreas = this.cityAreaRepository.Table.Where(x => x.CityId == cityid);
            if (!cityAreas.Any())
            {
                return new List<Dropdown>();
            }

            var hotels = from h in this.hotelRepository.Table
                         join a in this.accomodationRepository.Table on h.HotelId equals a.AccommodationID
                         join ca in cityAreas on h.CityAreaId equals ca.CityAreaId
                         where a.HotelName.StartsWith(search) && ca.CityId == cityid
                         orderby a.HotelName
                         select new Dropdown
                         {
                             Name = a.HotelName,
                             Id = h.HotelId.ToString()
                         };

            if (!string.IsNullOrEmpty(hotelid) && hotelid != Guid.Empty.ToString())
            {
                hotels = hotels.Where(x => x.Id == hotelid);
            }

            return await this.dropdownRespository.ToOptionListAsync(hotels, page);
        }

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
        public async Task<IList<Dropdown>> GetHotelValditySelectListAsync(string search, short page, Guid hotelid, string hotelvalidityid = "")
        {
            var hotelsValidity = from h in this.hotelValidityRepository.Table
                                 let hotelprice = (from hp in this.hotelPriceRepository.Table
                                                   where hp.ValidityFrom == h.ValidityDateFrom && hp.ValidityTo == h.ValidityDateTo && hp.AccommodationID == h.HotelId
                                                   select hp).Count()
                                 where (h.ValidityDateFrom.ToString("dd MMM yyyy") + " to " + h.ValidityDateTo.ToString("dd MMM yyyy")).ToLower().StartsWith(search.ToLower()) && h.HotelId == hotelid && hotelprice > 0
                                 orderby h.ValidityDateFrom
                                 select new
                                 {
                                     Name = h.ValidityDateFrom.ToString("dd MMM yyyy") + " to " + h.ValidityDateTo.ToString("dd MMM yyyy"),
                                     Id = h.Id.ToString(),
                                     ValidityDateFrom = h.ValidityDateFrom
                                 };

            if (!string.IsNullOrEmpty(hotelvalidityid) && hotelvalidityid != Guid.Empty.ToString())
            {
                hotelsValidity = hotelsValidity.Where(x => x.Id == hotelvalidityid);
            }
            else
            {
                var hotelvalidityids = (from p in this.packageRepository.Table
                                        select p.HotelValidityId.ToString()).ToList();

                hotelsValidity = hotelsValidity.Where(x => !hotelvalidityids.Contains(x.Id) && x.ValidityDateFrom.Date > DateTime.Now.Date);
            }

            var result = hotelsValidity.Select(x => new Dropdown
            {
                Name = x.Name,
                Id = x.Id
            });

            return await this.dropdownRespository.ToOptionListAsync(result, page);
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>GetFlightDestination</returns>
        public async Task<IList<CityView>> GetFlightDestination(string search, short page)
        {
            var query = this.cityView.Table.Select(x => new CityView
            {
                Id = x.CityCode,
                Name = x.CityName,
                CityCode = x.CityCode,
                CityName = x.CityName,
                CountryCode = x.CountryCode,
                CountryName = x.CountryName,
                ShortDetail = x.ShortDetail ?? string.Empty,
                Description = x.Description ?? string.Empty,
                SearchIn = $"{x.CityName.Trim()} ({x.CityCode})"
            });

            query = query.Where(x => x.SearchIn.ToLower().Contains(search.ToLower()) && x.CountryCode.ToUpper().Trim() == "IN");

            page = page == 0 ? (short)1 : page;
            if (page > 0)
            {
                query = query.Skip((page - 1) * Constants.ComboPaginationSize).Take(Constants.ComboPaginationSize);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="cityCodes">The city codes.</param>
        /// <returns>GetFlightDestination</returns>
        public async Task<IList<CityView>> GetFlightDestination(string[] cityCodes)
        {
            cityCodes = cityCodes ?? new List<string>().ToArray();

            var query = this.cityView.Table.Where(x => cityCodes.Contains(x.CityCode)).Select(x => new CityView
            {
                Id = x.CityCode,
                Name = x.CityName,
                CityCode = x.CityCode,
                CityName = x.CityName,
                CountryCode = x.CountryCode,
                CountryName = x.CountryName,
                ShortDetail = x.ShortDetail ?? string.Empty,
                Description = x.Description ?? string.Empty,
                SearchIn = $"{x.CityName.Trim()} ({x.CityCode})"
            });

            return await query.ToListAsync();
        }

        /// <summary>
        /// Gets the flight countries.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countrycode">The countrycode.</param>
        /// <returns>
        /// GetFlightCountries
        /// </returns>
        public async Task<IList<CityView>> GetFlightCountries(string search, short page, string countrycode = "")
        {
            var countryQuery = this.cityView.Table.GroupBy(c => c.CountryCode)
                            .Select(grp => grp.First());

            var query = countryQuery.Distinct().Select(x => new CityView
            {
                Id = x.CityCode,
                Name = x.CityName,
                CityCode = x.CityCode,
                CityName = x.CityName,
                CountryCode = x.CountryCode,
                CountryName = x.CountryName,
                ShortDetail = x.ShortDetail ?? string.Empty,
                Description = x.Description ?? string.Empty,
                SearchIn = $"{x.CountryName.Trim()} ({x.CountryCode})"
            });

            if (!string.IsNullOrEmpty(countrycode))
            {
                query = query.Where(x => x.CountryCode == countrycode);
            }

            query = query.Where(x => x.SearchIn.ToLower().Contains(search.ToLower())).OrderBy(x => !x.CountryCode.ToLower().StartsWith("in"));

            page = page == 0 ? (short)1 : page;
            if (page > 0)
            {
                query = query.Skip((page - 1) * Constants.ComboPaginationSize).Take(Constants.ComboPaginationSize);
            }

            var result = await query.ToListAsync();
            return result;
        }

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
        public async Task<IList<CityView>> GetFlightCitiesByCountry(string search, short page, string countrycode, string citycode = "")
        {
            var query = this.cityView.Table
                .Where(x => x.CountryCode == countrycode)
                .Select(x => new CityView
                {
                    Id = x.CityCode,
                    Name = x.CityName,
                    CityCode = x.CityCode,
                    CityName = x.CityName,
                    CountryCode = x.CountryCode,
                    CountryName = x.CountryName,
                    ShortDetail = x.ShortDetail ?? string.Empty,
                    Description = x.Description ?? string.Empty,
                    SearchIn = $"{x.CityName.Trim()} ({x.CityCode})"
                });

            if (!string.IsNullOrEmpty(citycode))
            {
                query = query.Where(x => x.CityCode.Trim() == citycode.Trim());
            }

            query = query.Where(x => x.SearchIn.ToLower().Contains(search.ToLower())).OrderBy(x => !x.CountryCode.ToLower().StartsWith("in"));

            page = page == 0 ? (short)1 : page;
            if (page > 0)
            {
                query = query.Skip((page - 1) * Constants.ComboPaginationSize).Take(Constants.ComboPaginationSize);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Gets the name of the flight destination by city.
        /// </summary>
        /// <param name="cityname">The cityname.</param>
        /// <returns>GetFlightDestinationByCityName</returns>
        public async Task<CityView> GetFlightDestinationByCityName(string cityname)
        {
            try
            {
                var flightDestination = await this.cityView.Table.FirstOrDefaultAsync(x => x.CityName.ToLower().Trim() == cityname.ToLower().Trim());

                if (flightDestination != null)
                {
                    flightDestination.SearchIn = $"{flightDestination.CityName.Trim()} ({flightDestination.CityCode})";
                }

                return flightDestination;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        /// <summary>
        /// Gets the flight destination by city code.
        /// </summary>
        /// <param name="citycode">The citycode.</param>
        /// <returns>GetFlightDestinationByCityCode</returns>
        public async Task<CityView> GetFlightDestinationByCityCode(string citycode)
        {
            var flightDestination = await this.cityView.Table.FirstOrDefaultAsync(x => x.CityCode == citycode.ToLower().Trim());

            if (flightDestination != null)
            {
                flightDestination.SearchIn = $"{flightDestination.CityName.Trim()} ({flightDestination.CityCode})";
            }

            return flightDestination;
        }

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
        public async Task<IList<Dropdown>> GetPackageCountryListAsync(string search, short page, short? countryId, short regionId = 0)
        {
            var country = this.packageCountryRepository.Table.Where(x => x.Name.StartsWith(search) && x.IsActive);
            if (regionId > 0)
            {
                country = country.Where(x => Convert.ToString(x.RegionId) != null && x.RegionId == regionId);
            }

            var query = country.OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            });

            if (countryId != 0)
            {
                query = query.Where(x => x.Id == countryId.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the holiday menu country list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <returns>
        /// GetHolidayMenuCountryListAsync
        /// </returns>
        public async Task<IList<Dropdown>> GetHolidayMenuCountryListAsync(string search, short page, short countryId, string regionId)
        {
            try
            {
                var country = from c in this.packageCountryRepository.Table
                              join region in this.packageregionRepository.Table on c.RegionId equals region.Id
                              where c.Name.ToLower().StartsWith(search.ToLower()) && c.IsActive
                              select new { c, region.Name };
                if (!string.IsNullOrEmpty(regionId))
                {
                    country = country.Where(x => x.Name == regionId);
                }

                if (countryId != 0)
                {
                    country = country.Where(x => x.c.Id == countryId);
                }

                var query = country.OrderBy(x => x.c.Name)
                .Select(x => new Dropdown(x.c.Name, x.c.Name));

                return await this.dropdownRespository.ToOptionListAsync(query, page);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Gets the package country by identifier asynchronous.
        /// </summary>
        /// <param name="countryid">The countryid.</param>
        /// <returns>GetPackageCountryByIdAsync</returns>
        public async Task<PackageCountryModel> GetPackageCountryByIdAsync(short countryid)
        {
            return await this.packageCountryRepository.Table.FirstOrDefaultAsync(x => x.Id == countryid);
        }

        /// <summary>
        /// Gets the package country by identifier asynchronous.
        /// </summary>
        /// <param name="stateId">The State Id.</param>
        /// <returns>GetPackageCountryByIdAsync</returns>
        public async Task<PackageStateModel> GetPackageStateByIdAsync(int stateId)
        {
            return await this.packageStateRepository.Table.FirstOrDefaultAsync(x => x.Id == stateId);
        }

        /// <summary>
        /// Gets the package city by identifier asynchronous.
        /// </summary>
        /// <param name="cityId">The city identifier.</param>
        /// <returns>GetPackageCityByIdAsync</returns>
        public async Task<PackageCityModel> GetPackageCityByIdAsync(short cityId)
        {
            return await this.packagecityRepository.Table.FirstOrDefaultAsync(x => x.Id == cityId);
        }

        /// <summary>
        /// Gets the package country by identifier asynchronous.
        /// </summary>
        /// <param name="countrycode">The countrycode.</param>
        /// <returns>
        /// GetPackageCountryByIdAsync
        /// </returns>
        public async Task<PackageCountryModel> GetPackageCountryByCodeAsync(string countrycode)
        {
            return await this.packageCountryRepository.Table.FirstOrDefaultAsync(x => x.SortName == countrycode);
        }

        /// <summary>
        /// Gets the package city by identifier asynchronous.
        /// </summary>
        /// <param name="citycode">The city identifier.</param>
        /// <returns>
        /// GetPackageCityByIdAsync
        /// </returns>
        public async Task<PackageCityModel> GetPackageCityByCodeAsync(string citycode)
        {
            return await this.packagecityRepository.Table.FirstOrDefaultAsync(x => x.Code == citycode);
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="regionId">The regionId identifier.</param>
        /// <returns>
        /// CountryList
        /// </returns>
        public async Task<IList<Dropdown>> GetPackagedCountryListByRegionIdAsync(string search, short page, short regionId)
        {
            var query = this.packageCountryRepository.Table
            .Where(x => x.RegionId == regionId && x.Name.StartsWith(search) && x.IsActive)
            .OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            });

            return await this.dropdownRespository.ToListAsync(query);
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetPackageStateListAsync(string search, short page, int? stateId, short countryId)
        {
            var state = this.packageStateRepository.Table.Where(x => x.Name.StartsWith(search));
            if (countryId > 0)
            {
                state = state.Where(x => x.CountryId == countryId);
            }

            var query = state.OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            });

            if (stateId != 0)
            {
                query = query.Where(x => x.Id == stateId.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetPackageStateListByCountryIdAsync(string search, short page, int countryId)
        {
            var query = this.packageStateRepository.Table
            .Where(x => x.CountryId == countryId && x.Name.StartsWith(search))
            .OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            });

            return await this.dropdownRespository.ToListAsync(query);
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="hotecategoryid">The hotecategoryid.</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetPackageCategoryListAsync(string search, short page, int hotecategoryid)
        {
            var query = this.packagecategoryRepository.Table
            .Where(x => x.Name.StartsWith(search) && x.IsActive)
            .OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            });

            if (hotecategoryid != 0)
            {
                query = query.Where(x => x.Id == hotecategoryid.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetPackageRegionListAsync(string search, short page, short? regionId)
        {
            var query = this.packageregionRepository.Table
            .Where(x => x.Name.StartsWith(search) && x.IsActive)
            .OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            });

            if (regionId != 0)
            {
                query = query.Where(x => x.Id == regionId.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the holiday menu region list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <returns>Holiday Menu</returns>
        public async Task<IList<Dropdown>> GetHolidayMenuRegionListAsync(string search, short page, short? regionId)
        {
            var query = this.packageregionRepository.Table
            .Where(x => x.Name.StartsWith(search) && x.IsActive)
            .OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Name,
                Name = x.Name
            });

            if (regionId != 0)
            {
                query = query.Where(x => x.Id == regionId.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="dealId">The deal identifier.</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetPackageDealTypeListAsync(string search, short page, int dealId)
        {
            var query = this.packagedealTypeRepository.Table
            .Where(x => x.Name.StartsWith(search))
            .OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            });

            if (dealId != 0)
            {
                query = query.Where(x => x.Id == dealId.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="dealId">The deal identifier.</param>
        /// <param name="cityid">The cityid.</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetPackageHotelListAsync(string search, short page, int dealId, int cityid)
        {
            var query = this.packagehotelRepository.Table
            .Where(x => x.Name.StartsWith(search) && x.IsActive && x.CityId == (dealId == 0 ? cityid : x.CityId) && !x.IsDeleted)
            .OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            });

            if (dealId != 0)
            {
                query = query.Where(x => x.Id == dealId.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="dealId">The deal identifier.</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetPackageTravelStyleListAsync(string search, short page, int dealId)
        {
            var query = this.packagetravelstyleRepository.Table
            .Where(x => x.Name.StartsWith(search) && x.IsActive)
            .OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            });

            if (dealId != 0)
            {
                query = query.Where(x => x.Id == dealId.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<List<Dropdown>> GetAllPackageTravelStyleListAsync()
        {
            var query = this.packagetravelstyleRepository.Table
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            }).ToListAsync();

            return await query;
        }

        /// <summary>
        /// Gets the package travel style list asynchronous.
        /// </summary>
        /// <returns>List of Travel Style</returns>
        public async Task<IList<Dropdown>> GetPackageTravelStyleListAsync()
        {
            var query = this.packagetravelstyleRepository.Table
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description,
                IconClass = x.IconClass
            });

            return await query.ToListAsync();
        }

        /// <summary>
        /// Gets the package travel style list asynchronous.
        /// </summary>
        /// <returns>List of Travel Style</returns>
        public async Task<List<PackageTravelStyleModel>> GetHomeTravelStyleListAsync()
        {
            try
            {
                var query = this.packagetravelstyleRepository.Table
                .Where(x => x.IsActive);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return null;
            }
        }

        /// <summary>
        /// Gets the package holiday menu list asynchronous.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        /// List of Holiday Menu
        /// </returns>
        public async Task<IList<Dropdown>> GetPackageHolidayMenuListAsync(string name = "")
        {
            var query = this.packageHolidayMenuRepository.Table
            .Where(x => x.IsActive && (!string.IsNullOrEmpty(name) ? x.Name.ToLower() == name.ToLower() : 0 == 0))
            .Select(x => new Dropdown
            {
                Id = x.IsRegion.ToString(),
                Name = x.Name,
                Description = x.ChildMenu
            });

            return await query.ToListAsync();
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="roomtypeId">The roomtype identifier.</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetPackageHotelRoomTypeListAsync(string search, short page, int roomtypeId)
        {
            var query = this.packagehoteRoomTypeRepository.Table
            .Where(x => x.Name.StartsWith(search) && x.IsActive)
            .OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                ////Description = x.Description
            });

            if (roomtypeId != 0)
            {
                query = query.Where(x => x.Id == roomtypeId.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="roomtypeId">The roomtype identifier.</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetPackageHotelRoomTypeListByIdsAsync(string search, short page, short[] roomtypeId)
        {
            var query = this.packagehoteRoomTypeRepository.Table
            .Where(x => x.Name.StartsWith(search) && x.IsActive && roomtypeId.Contains(x.Id))
            .OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                ////Description = x.Description
            });

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="cityId">The city identifier.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetPackageCityListAsync(string search, short page, int cityId, short stateId)
        {
            var city = this.packagecityRepository.Table.Where(x => x.Name.StartsWith(search) && x.IsActive);
            if (stateId > 0)
            {
                city = city.Where(x => x.StateId == stateId);
            }

            var query = city.OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name + "(" + x.Code + ")",
                Description = x.CityDescription
            });

            if (cityId != 0)
            {
                query = query.Where(x => x.Id == cityId.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the tour package country by reagion identifier.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="regionId">The region identifier.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>GetTourPackageCountryByReagionId</returns>
        public async Task<IList<Dropdown>> GetTourPackageCountryByReagionId(string search, short page, int regionId, short countryId = 0)
        {
            var city = this.packageCountryRepository.Table.Where(x => x.Name.StartsWith(search) && x.IsActive && x.RegionId == regionId);

            var query = city.OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            });

            if (countryId != 0)
            {
                query = query.Where(x => x.Id == countryId.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the flight destination.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <returns>
        /// GetFlightDestination
        /// </returns>
        public async Task<IList<Dropdown>> GetTourPackageStatesByCountrId(string search, short page, int? countryId, short stateId = 0)
        {
            var city = this.packageStateRepository.Table.Where(x => x.Name.StartsWith(search) && x.IsActive && x.CountryId == countryId);

            var query = city.OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Description = x.Description
            });

            if (stateId != 0)
            {
                query = query.Where(x => x.Id == stateId.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the package city by country identifier and sate identifier asynchronous.
        /// </summary>GetTourPackageCityByCounryIdorStateIdAsync
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="stateId">The state identifier.</param>
        /// <param name="cityid">The cityid.</param>
        /// <returns>GetPackageCityByCountryIdAndSateIdAsync</returns>
        public async Task<IList<Dropdown>> GetTourPackageCityByCounryIdorStateIdAsync(string search, short page, int? countryId, short stateId = 0, short cityid = 0)
        {
            var city = this.packagecityRepository.Table.Where(x => x.Name.StartsWith(search) && x.IsActive && x.CountryId == countryId);

            if (stateId > 0)
            {
                city = city.Where(x => x.StateId == stateId);
            }

            var query = city.OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name + "(" + x.Code + ")",
                Description = x.CityDescription
            });

            if (cityid != 0)
            {
                query = query.Where(x => x.Id == cityid.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the city by counry identifier asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="cityid">The cityid.</param>
        /// <returns>
        /// GetCityByCounryIdAsync
        /// </returns>
        public async Task<IList<Dropdown>> GetCityByCounryIdAsync(string search, short page, string countryId, short cityid = 0)
        {
            var city = from c in this.packagecityRepository.Table
                       join country in this.packageCountryRepository.Table on c.CountryId equals country.Id
                       where country.Name == countryId && c.IsActive && c.Name.StartsWith(search)
                       select c;

            ////this.packagecityRepository.Table.Where(x => x.Name.StartsWith(search) && x.IsActive && x.CountryId == countryId);

            if (cityid != 0)
            {
                city = city.Where(x => x.Id == cityid);
            }

            var query = city.OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Name,
                Name = x.Name,
            });

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

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
        public async Task<IList<Dropdown>> GetAreaByCityIdAsync(string search, short page, int cityId, int areaId)
        {
            var city = from c in this.packageAreaRepository.Table
                       select c;

            ////this.packagecityRepository.Table.Where(x => x.Name.StartsWith(search) && x.IsActive && x.CountryId == countryId);
            if (!string.IsNullOrEmpty(search))
            {
                city = city.Where(x => x.Name.StartsWith(search) && x.IsActive);
            }

            if (cityId != 0)
            {
                city = city.Where(x => x.City == cityId);
            }

            if (areaId != 0)
            {
                city = city.Where(x => x.Id == areaId);
            }

            var query = city.OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name,
            });

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the package city list asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <param name="cityId">The city identifier.</param>
        /// <param name="countryId">The country identifier.</param>
        /// <returns>
        /// GetPackageCityListByCountryAsync
        /// </returns>
        public async Task<IList<Dropdown>> GetPackageCityListByCountryAsync(string search, short page, int cityId, short countryId)
        {
            var city = this.packagecityRepository.Table.Where(x => x.Name.StartsWith(search) && x.IsActive);
            if (countryId > 0)
            {
                city = city.Where(x => x.CountryId == countryId);
            }

            var query = city.OrderBy(x => x.Name)
            .Select(x => new Dropdown
            {
                Id = x.Id.ToString(),
                Name = x.Name + "(" + x.Code + ")",
                Description = x.Description
            });

            if (cityId != 0)
            {
                query = query.Where(x => x.Id == cityId.ToString());
            }

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the static page master asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>static page master list</returns>
        public async Task<IList<Dropdown>> GetStaticPageMasterAsync(string search, short page)
        {
            var query = this.staticPageMasterRepository.Table
            .OrderBy(x => x.Name).Where(x => x.Name.ToLower().StartsWith(search.ToLower()))
            .Select(x => new Dropdown
            {
                Id = x.PageId.ToString(),
                Name = x.Name
            });

            return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// Gets the menu country city asynchronous.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <param name="page">The page.</param>
        /// <returns>drop down list</returns>
        public async Task<IList<Dropdown>> GetMenuCountryCityAsync(string search, short page)
        {
            var query = CountryCity.List().SelectMany(x => x.Value)
            .OrderBy(x => x.Value).Where(x => x.Value.ToLower().StartsWith(search.ToLower()))
            .Select(x => new Dropdown
            {
                Id = x.Key.ToString(),
                Name = x.Value
            });

            int page1 = page == 0 ? 1 : page;
            if (page1 > 0)
            {
                query = query.Skip((page1 - 1) * Constants.ComboPaginationSize).Take(Constants.ComboPaginationSize);
            }

            var result = await Task.Run(() => query.ToList());

            return result;

            ////return await this.dropdownRespository.ToOptionListAsync(query, page);
        }

        /// <summary>
        /// GetHotelRomeTypeByTypeIDAsync.
        /// </summary>
        /// <param name="typeid">The search.</param>
        /// <returns>Package Hotel RoomType Model</returns>
        public async Task<PackageHotelRoomTypeModel> GetHotelRomeTypeByTypeIDAsync(short typeid)
        {
            if (typeid == 0)
            {
                return null;
            }

            var model = await this.packagehoteRoomTypeRepository.Table.FirstOrDefaultAsync(x => x.Id == typeid);
            return model;
        }

        /// <summary>
        /// GetHotelRomeTypeByTypeIDAsync.
        /// </summary>
        /// <param name="currencyId">The Currency ID.</param>
        /// <returns>Package Hotel RoomType Model</returns>
        public async Task<CurrencyModel> GetCurrencyByIdAsync(int currencyId)
        {
            if (currencyId == 0)
            {
                return null;
            }

            try
            {
                var model = await this.currencyRespository.Table.Where(x => x.Id == currencyId).FirstOrDefaultAsync();
                return model;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
                return null;
            }
        }
    }
}