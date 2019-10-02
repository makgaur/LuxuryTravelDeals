// <copyright file="Startup.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO.Compression;
    using System.Linq;
    using AutoMapper;
    using HiTours.Api.Common.Caching;
    using HiTours.Api.Common.Caching.Repository;
    using HiTours.Api.Common.Contract;
    using HiTours.Api.Common.Data.Azure;
    using HiTours.Api.Configuration.Business;
    using HiTours.Core;
    using HiTours.Data;
    using HiTours.Data.DataBase.Model;
    using HiTours.Data.Repository;
    using HiTours.Models;
    using HiTours.Services;
    using HiTours.ViewModels;
    using HiTours.Web.Framework;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.Facebook;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.ResponseCompression;
    using Microsoft.AspNetCore.Rewrite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private readonly ILoggerFactory logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The env.</param>
        /// <param name="logger">Logger</param>
        public Startup(IHostingEnvironment env, ILoggerFactory logger)
        {
            this.logger = logger;
            var builder = new ConfigurationBuilder()
                 .SetBasePath(env.ContentRootPath)
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
            }

            builder.AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var azureconnectionstring = this.Configuration.GetConnectionString("AzureConnectionString");
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml", "image/png", "image/jpeg", "image/gif" });
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("Default")));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            ////services.AddScoped<IPCheckFilter>();
            ////services.AddMvc(options =>
            ////{
            ////    options.Filters.Add(new IPCheckFilter(this.logger, this.Configuration));
            ////}).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<FormOptions>(x => x.ValueCountLimit = 52428800);
            services.AddMvc(options =>
            {
                options.Filters.Add(new HandleException());
                options.Filters.Add(new RequireHttpsAttribute());
            }).AddJsonOptions(options =>
                {
                    options.SerializerSettings.DateFormatString = "dd/MM/yyy";
                })
                    .AddViewLocalization()
                    .AddDataAnnotationsLocalization()
                    .AddJsonOptions(option => option.SerializerSettings.ContractResolver = new DefaultContractResolver());
            services.AddApplicationInsightsTelemetry(this.Configuration);
            services.Configure<DomainSetting>(options => this.Configuration.GetSection("DomainSetting").Bind(options));
            services.Configure<HiTours.TBO.Models.UserCredential>(options => this.Configuration.GetSection("TBOCredentials").Bind(options));
            services.Configure<FacebookOptions>(options => this.Configuration.GetSection(nameof(FacebookOptions)).Bind(options));
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/user/";
            })
            .AddCookie(Constants.ApplicationAdminCookies, options =>
            {
                options.LoginPath = new PathString("/admin/account/login/");
            })
            .AddFacebook(options =>
            {
                options.AppId = this.Configuration[$"FacebookOptions:AppId"];
                options.AppSecret = this.Configuration[$"FacebookOptions:AppSecret"];
            })
             .AddGoogle(options =>
             {
                 options.ClientId = this.Configuration[$"GoogleOptions:ClientId"];
                 options.ClientSecret = this.Configuration[$"GoogleOptions:ClientSecret"];
                 ////options.CallbackPath = "/user/google";
             });

            /////////Storage and date base/////
            services.AddTransient<ICacheHandler, NoCacheHandler>();
            services.AddTransient<ITableCacheHandler, AzureTableCacheHandler>();
            services.AddSingleton<ICacheRepository>(new CacheRepository(
             new TableStorageHandler<CacheEntity>(new TableContext<CacheEntity>(azureconnectionstring, StorageValues.CacheEntityTableName)), new BlobStorageHandler(azureconnectionstring, StorageValues.CacheContainerName)));
            services.AddTransient<IHomePageBusiness, HomePageBusiness>();
            this.RegisterServices(services);

            services.AddSingleton(this.RegisterAutoMapping());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        /// <param name="serviceProvider">The service provider.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseAuthentication();
            app.UseResponseCompression();
            app.UseSession();
            app.UseMvc();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always
            });
            app.UseBrowserLink();
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePagesWithReExecute("/Error/StatusCode/{0}");
            app.UseExceptionHandler("/Error/StatusCode/500");
            ////    app.UseStatusCodePagesWithReExecute("/Error/StatusCode/{0}");
            ////}
            app.Use(async (context, next) =>
            {
                string sHost = context.Request.Host.HasValue == true ? context.Request.Host.Value : string.Empty;  ////domain without :80 port .ToString();

                sHost = sHost.ToLower();

                string sPath = context.Request.Path.HasValue == true ? context.Request.Path.Value : string.Empty;

                string sQuerystring = context.Request.QueryString.HasValue == true ? context.Request.QueryString.Value : string.Empty;
                ////----< check https >----

                //// check if the request is *not* using the HTTPS scheme

                if (!context.Request.IsHttps)
                {
                    ////--< is http >--
                    string new_https_Url = "https://" + sHost;

                    if (sPath != string.Empty)
                    {
                        new_https_Url = new_https_Url + sPath;
                    }

                    if (sQuerystring != string.Empty)
                    {
                        new_https_Url = new_https_Url + sQuerystring;
                    }
                    context.Response.Redirect(new_https_Url);
                    return;

                    ////--</ is http >--
                }

                ////----</ check https >----
                ////----< check www >----

                if (sHost.IndexOf("www.") == 0)
                {
                    ////--< is www. >--

                    string new_Url_without_www = "https://" + sHost.Replace("www.", string.Empty);
                    if (sPath != string.Empty)
                    {
                        new_Url_without_www = new_Url_without_www + sPath;
                    }

                    if (sQuerystring != string.Empty)
                    {
                        new_Url_without_www = new_Url_without_www + sQuerystring;
                    }

                    context.Response.Redirect(new_Url_without_www);
                    return;

                    ////--</ is http >--
                }

                ////----</ check www >----
                ////also check images inside the content

                await next();
            });
            var ci = new CultureInfo("en-IN");
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(ci),
                SupportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-IN"),
            }
            });

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            ////var localizationOptions = new RequestLocalizationOptions
            ////{
            ////    SupportedCultures = new List<CultureInfo> { new CultureInfo(Constants.SiteLocale) },
            ////    SupportedUICultures = new List<CultureInfo> { new CultureInfo(Constants.SiteLocale) },
            ////    DefaultRequestCulture = new RequestCulture(Constants.SiteLocale)
            ////};

            ////app.UseRequestLocalization(localizationOptions);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "area",
                  template: "{area:exists}/{controller=dashbord}/{action=index}/{id?}");

                routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}/{name?}");
                routes.MapRoute(
                 name: "promotions",
                 template: "Promotions/{type?}/{keyWord?}",
                 defaults: new { controller = "Home", action = "Promotions" });
            });

            ////this.CreateRoles(serviceProvider).Wait();
        }

        /// <summary>
        /// Registers the automatic mapping.
        /// </summary>
        /// <returns>Register Auto Mapping </returns>
        private IMapper RegisterAutoMapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SeoDetailModel, SeoDetailViewModel>();
                cfg.CreateMap<SeoDetailViewModel, SeoDetailModel>();

                cfg.CreateMap<CategoryModel, CategoryViewModel>();
                cfg.CreateMap<CategoryViewModel, CategoryModel>();

                cfg.CreateMap<PackageModel, PackageViewModel>();
                cfg.CreateMap<PackageViewModel, PackageModel>();

                cfg.CreateMap<UserDetailViewModel, UserDetailModel>();
                cfg.CreateMap<UserDetailModel, UserDetailViewModel>();

                cfg.CreateMap<PackageImageModel, PackageImageViewModel>();
                cfg.CreateMap<PackageImageViewModel, PackageImageModel>();

                cfg.CreateMap<ApplicationUserModel, ApplicationUserViewModels>();
                cfg.CreateMap<ApplicationUserViewModels, ApplicationUserModel>();

                cfg.CreateMap<SpecificPriceModel, SpecificPriceViewModel>();
                cfg.CreateMap<SpecificPriceViewModel, SpecificPriceModel>();

                cfg.CreateMap<HomeBannerModel, HomeBannerViewModel>();
                cfg.CreateMap<HomeBannerViewModel, HomeBannerModel>();

                cfg.CreateMap<PackageCountryModel, PackageCountryViewModel>();
                cfg.CreateMap<PackageCountryViewModel, PackageCountryModel>();

                cfg.CreateMap<PackageStateModel, PackageStateViewModel>();
                cfg.CreateMap<PackageStateViewModel, PackageStateModel>();

                cfg.CreateMap<PackageCityModel, PackageCityViewModel>();
                cfg.CreateMap<PackageCityViewModel, PackageCityModel>();

                cfg.CreateMap<PackageTravelStyleModel, PackageTravelStyleViewModel>();
                cfg.CreateMap<PackageTravelStyleViewModel, PackageTravelStyleModel>();

                cfg.CreateMap<PackageHolidayMenuModel, PackageHolidayMenuViewModel>();
                cfg.CreateMap<PackageHolidayMenuViewModel, PackageHolidayMenuModel>();

                cfg.CreateMap<PackageRegionModel, PackageRegionViewModel>();
                cfg.CreateMap<PackageRegionViewModel, PackageRegionModel>();

                cfg.CreateMap<PackageHotelCategoryModel, PackageHotelCategoryViewModel>();
                cfg.CreateMap<PackageHotelCategoryViewModel, PackageHotelCategoryModel>();

                cfg.CreateMap<PackageHotelRoomTypeModel, PackageHotelRoomTypeViewModel>();
                cfg.CreateMap<PackageHotelRoomTypeViewModel, PackageHotelRoomTypeModel>();

                cfg.CreateMap<PackageHotelModel, HotelierInfoViewModel>();
                cfg.CreateMap<HotelierInfoViewModel, PackageHotelModel>();

                cfg.CreateMap<PackageHotelRoomTypeDescModel, PackageHotelRoomTypeDescViewModel>();
                cfg.CreateMap<PackageHotelRoomTypeDescViewModel, PackageHotelRoomTypeDescModel>();

                cfg.CreateMap<TourPackageModel, TourPackageViewModel>();
                cfg.CreateMap<TourPackageViewModel, TourPackageModel>();

                cfg.CreateMap<TourPackageCityModel, TourPackageCityViewModel>();
                cfg.CreateMap<TourPackageCityViewModel, TourPackageCityModel>();

                cfg.CreateMap<TourPackageBookDateModel, TourPackageBookDateViewModel>();
                cfg.CreateMap<TourPackageBookDateViewModel, TourPackageBookDateModel>();

                cfg.CreateMap<TourPackageTravelStyleModel, TourPackageTravelStyleViewModel>();
                cfg.CreateMap<TourPackageTravelStyleViewModel, TourPackageTravelStyleModel>();

                cfg.CreateMap<TourPackageNightModel, TourPackageNightViewModel>();
                cfg.CreateMap<TourPackageNightViewModel, TourPackageNightModel>();

                cfg.CreateMap<TourPackageNightsValidityModel, TourPackageNightsValidityViewModel>();
                cfg.CreateMap<TourPackageNightsValidityViewModel, TourPackageNightsValidityModel>();

                cfg.CreateMap<TourPackageNightsDepartCityModel, TourPackageNightsDepartCityViewModel>();
                cfg.CreateMap<TourPackageNightsDepartCityViewModel, TourPackageNightsDepartCityModel>();

                cfg.CreateMap<TourPackageImageModel, TourPackageImageViewModel>();
                cfg.CreateMap<TourPackageImageViewModel, TourPackageImageModel>();

                cfg.CreateMap<TourPackageDetailModel, TourPackageDetailViewModel>();
                cfg.CreateMap<TourPackageDetailViewModel, TourPackageDetailModel>();

                cfg.CreateMap<CompanySettingModel, CompanySettingViewModel>();
                cfg.CreateMap<CompanySettingViewModel, CompanySettingModel>();

                cfg.CreateMap<ApplicationUserAddViewModel, ApplicationUserModel>();
                cfg.CreateMap<ApplicationUserModel, ApplicationUserAddViewModel>();

                cfg.CreateMap<PackageCurrencyViewModel, CurrencyModel>();
                cfg.CreateMap<CurrencyModel, PackageCurrencyViewModel>();

                ////cfg.CreateMap<VendorModel, VendorViewModel>();
                ////cfg.CreateMap<VendorViewModel, VendorModel>();

                cfg.CreateMap<PackagePromotionsModel, PackagePromotionsManageViewModel>();
                cfg.CreateMap<PackagePromotionsManageViewModel, PackagePromotionsModel>();

                ////cfg.CreateMap<RoomInventoryAddViewModel, RoomInventoryModel>();
                ////cfg.CreateMap<RoomInventoryModel, RoomInventoryAddViewModel>();

                ////cfg.CreateMap<RatePlanModel, RatePlanAddViewModel>();
                ////cfg.CreateMap<RatePlanAddViewModel, RatePlanModel>();

                cfg.CreateMap<DestinationModel, DestinationAddViewModel>();
                cfg.CreateMap<DestinationAddViewModel, DestinationModel>();

                cfg.CreateMap<DestinationValidityModel, DestinationValidityAddViewModel>();
                cfg.CreateMap<DestinationValidityAddViewModel, DestinationValidityModel>();

                cfg.CreateMap<VisaModel, AddVisaMasterViewModel>();
                cfg.CreateMap<AddVisaMasterViewModel, VisaModel>();

                cfg.CreateMap<InsuranceModel, AddInsuranceMasterViewModel>();
                cfg.CreateMap<AddInsuranceMasterViewModel, InsuranceModel>();

                cfg.CreateMap<CurationsModel, CurationsAddViewModel>();
                cfg.CreateMap<CurationsAddViewModel, CurationsAddViewModel>();

                cfg.CreateMap<BlogPostsModel, BlogPostAddViewModel>();
                cfg.CreateMap<BlogPostAddViewModel, BlogPostsModel>();

                cfg.CreateMap<VendorGroupViewModel, VendorGroupModel>();
                cfg.CreateMap<VendorGroupModel, VendorGroupViewModel>();

                cfg.CreateMap<VendorInformationModel, VendorInformationViewModel>();
                cfg.CreateMap<VendorInformationViewModel, VendorInformationModel>();

                cfg.CreateMap<VendorContactViewModel, VendorContactModel>();
                cfg.CreateMap<VendorContactModel, VendorContactViewModel>();

                cfg.CreateMap<VendorContractViewModel, VendorContractModel>();
                cfg.CreateMap<VendorContractModel, VendorContractViewModel>();

                cfg.CreateMap<VendorBankDetailModel, VendorBankDetailsViewModel>();
                cfg.CreateMap<VendorBankDetailsViewModel, VendorBankDetailModel>();

                cfg.CreateMap<HotelierInformationModel, HotelierInfoViewModel>();
                cfg.CreateMap<HotelierInfoViewModel, HotelierInformationModel>();

                cfg.CreateMap<HotelierContentModel, HotelierContentViewModel>();
                cfg.CreateMap<HotelierContentViewModel, HotelierContentModel>();

                cfg.CreateMap<HotelierImageModel, HotelierImageViewModel>();
                cfg.CreateMap<HotelierImageViewModel, HotelierImageModel>();

                cfg.CreateMap<HotelierRoomConfigurationModel, HotelierRoomConfigurationViewModel>();
                cfg.CreateMap<HotelierRoomConfigurationViewModel, HotelierRoomConfigurationModel>();

                cfg.CreateMap<HotelierRoomImageModel, HotelierRoomImageViewModel>();
                cfg.CreateMap<HotelierRoomImageViewModel, HotelierRoomImageModel>();

                cfg.CreateMap<DealsPackageModel, DealsPackageViewModel>();
                cfg.CreateMap<DealsPackageViewModel, DealsPackageModel>();

                cfg.CreateMap<DealsNightModel, DealsNightViewModel>();
                cfg.CreateMap<DealsNightViewModel, DealsNightModel>();

                cfg.CreateMap<DealsItineraryModel, DealsItineraryViewModel>();
                cfg.CreateMap<DealsItineraryViewModel, DealsItineraryModel>();

                cfg.CreateMap<DealsHighlightModel, DealsHighlightViewModel>();
                cfg.CreateMap<DealsHighlightViewModel, DealsHighlightModel>();

                cfg.CreateMap<DealsPaxCombinationModel, DealsPaxCombinationViewModel>();
                cfg.CreateMap<DealsPaxCombinationViewModel, DealsPaxCombinationModel>();

                cfg.CreateMap<DealsDestinationModel, DealsDestinationViewModel>();
                cfg.CreateMap<DealsDestinationViewModel, DealsDestinationModel>();

                cfg.CreateMap<DealsBookingValidityModel, DealsBookingValidityViewModel>();
                cfg.CreateMap<DealsBookingValidityViewModel, DealsBookingValidityModel>();

                cfg.CreateMap<DealsInclusionModel, DealsInclusionViewModel>();
                cfg.CreateMap<DealsInclusionViewModel, DealsInclusionModel>();

                cfg.CreateMap<DealsTypeModel, DealsTypeViewModel>();
                cfg.CreateMap<DealsTypeViewModel, DealsTypeModel>();

                cfg.CreateMap<DealsRatePlanModel, DealsRatePlanViewModel>();
                cfg.CreateMap<DealsRatePlanViewModel, DealsRatePlanModel>();
                cfg.CreateMap<DealsImageModel, DealsImageViewModel>();
                cfg.CreateMap<DealsImageViewModel, DealsImageModel>();

                cfg.CreateMap<DealsContentModel, DealsContentViewModel>();
                cfg.CreateMap<DealsContentViewModel, DealsContentModel>();

                cfg.CreateMap<DealsReviewModel, DealsReviewViewModel>();
                cfg.CreateMap<DealsReviewViewModel, DealsReviewModel>();

                cfg.CreateMap<DealsCancellationPolicyModel, DealsCancellationPolicyViewModel>();
                cfg.CreateMap<DealsCancellationPolicyViewModel, DealsCancellationPolicyModel>();

                cfg.CreateMap<HotelierReviewModel, HotelierReviewViewModel>();
                cfg.CreateMap<HotelierReviewViewModel, HotelierReviewModel>();

                cfg.CreateMap<HotelierRoomImageModel, HotelierRoomImageViewModel>();
                cfg.CreateMap<HotelierRoomImageViewModel, HotelierRoomImageModel>();

                cfg.CreateMap<CancellationPolicyModel, CancellationPolicyViewModel>();
                cfg.CreateMap<CancellationPolicyViewModel, CancellationPolicyModel>();

                cfg.CreateMap<HotelierCancellationPolicyModel, HotelierCancellationPolicyViewModel>();
                cfg.CreateMap<HotelierCancellationPolicyViewModel, HotelierCancellationPolicyModel>();

                cfg.CreateMap<PromotionModel, PromotionViewModel>();
                cfg.CreateMap<PromotionViewModel, PromotionModel>();

                cfg.CreateMap<VendorGroupModel, VendorGroupViewModel>();
                cfg.CreateMap<VendorGroupViewModel, VendorGroupModel>();

                cfg.CreateMap<HotelierPromotionModel, HotelierPromotionViewModel>();
                cfg.CreateMap<HotelierPromotionViewModel, HotelierPromotionModel>();

                cfg.CreateMap<DealsPromotionModel, DealsPromotionViewModel>();
                cfg.CreateMap<DealsPromotionViewModel, DealsPromotionModel>();

                cfg.CreateMap<DealRoomConfigurationModel, DealRoomConfigViewModel>();
                cfg.CreateMap<DealRoomConfigViewModel, DealRoomConfigurationModel>();

                cfg.CreateMap<DealsRatePlanModel, DealsRatePlanViewModel>();
                cfg.CreateMap<DealsRatePlanViewModel, DealsRatePlanModel>();

                cfg.CreateMap<DealVisaModel, DealVisaViewModel>();
                cfg.CreateMap<DealVisaViewModel, DealVisaModel>();

                cfg.CreateMap<DealsFlightModel, DealsFlightViewModel>();
                cfg.CreateMap<DealsFlightViewModel, DealsFlightModel>();

                cfg.CreateMap<PackageAreaModel, PackageAreaViewModel>();
                cfg.CreateMap<PackageAreaViewModel, PackageAreaModel>();

                cfg.CreateMap<DealsAddOnModel, DealsAddOnViewModel>();
                cfg.CreateMap<DealsAddOnViewModel, DealsAddOnModel>();

                cfg.CreateMap<PopularDestinationModel, PopularDestinationViewModel>();
                cfg.CreateMap<PopularDestinationViewModel, PopularDestinationModel>();

                cfg.CreateMap<BookingInformationModel, BookingInformationViewModel>();
                cfg.CreateMap<BookingInformationViewModel, BookingInformationModel>();

                cfg.CreateMap<BookingPassengerModel, BookingPassengerViewModel>();
                cfg.CreateMap<BookingPassengerViewModel, BookingPassengerModel>();

                cfg.CreateMap<BookingHotelRoomModel, BookingHotelRoomViewModel>();
                cfg.CreateMap<BookingHotelRoomViewModel, BookingHotelRoomModel>();

                cfg.CreateMap<BookingVisaModel, BookingVisaViewModel>();
                cfg.CreateMap<BookingVisaViewModel, BookingVisaModel>();

                cfg.CreateMap<LocationDealModel, LocationDealsAddViewModel>();
                cfg.CreateMap<LocationDealsAddViewModel, LocationDealModel>();

                cfg.CreateMap<DealInventoryModel, DealInventoryViewModel>();
                cfg.CreateMap<DealInventoryViewModel, DealInventoryModel>();
            });

            return config.CreateMapper();
        }

        /// <summary>
        /// Registers the automatic mapping.
        /// </summary>
        /// <param name="services">The services.</param>
        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<IUserDetailService, UserDetailService>();
            services.AddScoped<IPackageImageService, PackageImageService>();
            services.AddScoped<IMasterService, MasterService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IHotelBookingService, HotelBookingService>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<IHomeBannerService, HomeBannerService>();
            services.AddScoped<ISeoDetailServices, SeoDetailServices>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IFlightBookingService, FlightBookingService>();
            services.AddScoped<ITravelStyleService, TravelStyleService>();
            services.AddScoped<IHolidayMenuService, HolidayMenuService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IHotelCategoryService, HotelCategoryService>();
            services.AddScoped<IHotelRoomTypeService, HotelRoomTypeService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<ITourPackageService, TourPackageService>();
            services.AddScoped<IPackageNightsService, PackageNightsService>();
            services.AddScoped<ICompanySettingService, CompanySettingService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IPackagePromotionService, PackagePromotionService>();
            ////services.AddScoped<IInventoryService, InventoryService>();
            ////services.AddScoped<IRatePlanService, RatePlanService>();
            services.AddScoped<IDestinationService, DestinationService>();
            services.AddScoped<IVisaService, VisaService>();
            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<IOptionalTourService, OptionalTourService>();
            services.AddScoped<ICurationsService, CurationsService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IDealService, DealService>();
            services.AddScoped<IHotelierService, HotelierService>();
            services.AddScoped<IVendorService, VendorService>();
            services.AddScoped<ICancellationService, CancellationService>();
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IListingService, ListingService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IHomePageService, HomePageService>();
            services.AddScoped<ITBOService, TBOService>();

            // services.AddScoped<IHostedService, ReminderTask>();
        }
    }
}