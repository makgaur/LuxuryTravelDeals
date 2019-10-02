// <copyright file="HomeController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Api.Common;
    using HiTours.Api.Common.Caching;
    using HiTours.Api.Configuration.Business;
    using HiTours.Core;
    using HiTours.Models;
    using HiTours.ViewModels;
    using HiTours.ViewModels.Deals;
    using HiTours.Web.Framework;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Services;

    /// <summary>
    /// Home Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    ////[ServiceFilter(typeof(IPCheckFilter))]
    public class HomeController : BaseController
    {
        private const int SearchExpiryInSeconds = 9000;
        private readonly ITableCacheHandler tableCacheHandler;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IPackageService package;
        private readonly DomainSetting domainSetting;
        private readonly IMasterService masterService;
        private readonly IListingService listingService;
        private readonly IHomePageService homePageService;
        private readonly IViewRenderService viewRenderService;
        private readonly IDealService dealService;
        private readonly IHomePageBusiness homePageBusiness;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController" /> class.
        /// </summary>
        /// <param name="tableCacheHandler">to store result in blob cache</param>
        /// <param name="homePageBusiness">Cache Data from storage</param>
        /// <param name="stateService">State Service</param>
        /// <param name="configuration">Web Config</param>
        /// <param name="homePageService">Home Page Service</param>
        /// <param name="countryService">The Country Service.</param>
        /// <param name="cityService">The City Service.</param>
        /// <param name="listingService">Listing Service</param>
        /// <param name="blogService">Blog Service</param>
        /// <param name="curationService">Curation Service</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="masterService">The master service.</param>
        /// <param name="domainSetting">The domain setting.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="viewRenderService">The view render service.</param>
        /// <param name="userDetailService">The user detail service.</param>
        /// <param name="package">The package.</param>
        /// <param name="hotelBookingService">The hotel booking service.</param>
        /// <param name="homeBanner">The home banner.</param>
        /// <param name="dealService">Deal Service</param>
        public HomeController(ITableCacheHandler tableCacheHandler, IHomePageBusiness homePageBusiness, IStateService stateService, IConfiguration configuration, IHomePageService homePageService, ICountryService countryService, ICityService cityService, IListingService listingService, IBlogService blogService, ICurationsService curationService, IMapper mapper, IMasterService masterService, IOptions<DomainSetting> domainSetting, IHostingEnvironment hostingEnvironment, IViewRenderService viewRenderService, IUserDetailService userDetailService, IPackageService package, IHotelBookingService hotelBookingService, IHomeBannerService homeBanner, IDealService dealService)
            : base(mapper, homePageService, cityService, countryService, configuration, stateService)
        {
            this.tableCacheHandler = tableCacheHandler;
            this.homePageBusiness = homePageBusiness;
            this.homePageService = homePageService;
            this.listingService = listingService;
            this.package = package;
            this.hostingEnvironment = hostingEnvironment;
            this.viewRenderService = viewRenderService;
            this.domainSetting = domainSetting.Value;
            this.masterService = masterService;
            this.dealService = dealService;
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="value">Name of Country/City/Name you want to display.</param>
        /// <param name="key">Group Of the Display Name.</param>
        /// <returns>
        /// view for home
        /// </returns>
        public async Task<IActionResult> Index(string value, string key)
        {
            if (value != null)
            {
                this.ViewBag.Display = value;
                this.ViewBag.Group = key;
            }

            try
            {
                var searchKey = KeyCreator.Create(DateTime.Now.ToString("dd/MM/yyyy"));

                var homepageConfigRs =
                await
                    this.tableCacheHandler.GetFromCacheAsync(
                        searchKey,
                        () => this.homePageBusiness.GetHomePageConfiguration(),
                        SearchExpiryInSeconds);

                var result = homepageConfigRs.Result;
                this.ViewBag.PageType = result.PageType;
                this.ViewBag.TravelStyle = result.TravelStyle;
                this.ViewBag.BannersList = result.BannersList;
                this.ViewBag.PopularDestinations = result.PopularDestinations;
                this.ViewBag.CurationBanner = result.CurationBanner;
                this.ViewBag.BlogPosts = result.BlogPosts;
                this.ViewBag.FlashDeals = result.FlashDeals;
                this.ViewBag.DealOfMonth = result.DealOfMonth;
                this.ViewBag.LocationBasedCuration = result.LocationBasedCuration;
            }
            catch (Exception ex)
            {
                var msg = ex.ToString();
            }

            return this.View();
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="ids">search type</param>
        /// <param name="offset">search string</param>
        /// <returns>view for home</returns>
        public IActionResult GetFlashDeals(string ids, int offset)
        {
            var data = this.listingService.GetFlashDealsAsync(ids, offset);
            return this.PartialView("_CurrentDeals", data);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="ids">search type</param>
        /// <param name="offset">search string</param>
        /// <returns>view for home</returns>
        public IActionResult GetDealsOfMonth(string ids, int offset)
        {
            var data = this.listingService.GetDealsOfMonth(ids, offset);
            return this.PartialView("_CurrentDeals", data);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="type">search type</param>
        /// <param name="keyWord">search string</param>
        /// <returns>view for home</returns>
        public async Task<IActionResult> Promotions(string type, string keyWord)
        {
            ////this.ViewBag.Item = await this.masterService.GetSearchTerm();
            this.ViewBag.PageType = Enums.SeoPageType.Static;
            this.ViewBag.TravelStyle = await this.masterService.GetPackageTravelStyleListAsync();
            return this.View("Index");
        }

        /// <summary>
        /// Currents the deals.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="searchBy">The search by.</param>
        /// <param name="searchTerms">The search terms.</param>
        /// <returns>
        /// Partial view
        /// </returns>
        public async Task<PartialViewResult> CurrentDeals(short limit, int offset, int searchBy, string searchTerms)
        {
            var packageDetails = await this.package.GetAllPackageAsync(limit, offset, searchBy, searchTerms);
            return this.PartialView("_Deals", packageDetails);
        }

        /// <summary>
        /// Currents the deals.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="searchTerms">The search terms.</param>
        /// <returns>
        /// Partial view
        /// </returns>
        public PartialViewResult SearchDeals(short limit, int offset, string searchTerms)
        {
            var result = this.listingService.GetSearchListing(limit, offset, searchTerms);
            return this.PartialView("_SearchListing", result);
        }

        /// <summary>
        /// Recentlies the view.
        /// </summary>
        /// <param name="dealsId">The deals identifier.</param>
        /// <returns>
        /// partial view
        /// </returns>
        public async Task<PartialViewResult> RecentlyView(string[] dealsId)
        {
            var recentlyViewDeals = await this.package.GetRecentelyViewDeals(dealsId);

            return this.PartialView("_RecentlyView", recentlyViewDeals);
        }

        /// <summary>
        /// Abouts the us.
        /// </summary>
        /// <returns>About Us</returns>
        public IActionResult AboutUs()
        {
            this.ViewBag.PageType = Enums.SeoPageType.Static;
            return this.View("AboutUsPage");
        }

         /// <summary>
         /// Abouts the us 2.
         /// </summary>
         /// <returns>About Us</returns>
        public IActionResult AboutUsPage()
        {
            this.ViewBag.PageType = Enums.SeoPageType.Static;
            return this.View();
        }

        /// <summary>
        /// Contacts the us.
        /// </summary>
        /// <returns>Contact Us</returns>
        public IActionResult ContactUs()
        {
            this.ViewBag.PageType = Enums.SeoPageType.Static;
            return this.View();
        }

        /// <summary>
        /// Contacts the us.
        /// </summary>
        /// <param name="conemail">The conemail.</param>
        /// <param name="confullname">The confullname.</param>
        /// <param name="conarea">The conarea.</param>
        /// <param name="conphone">Phone Number</param>
        /// <returns>
        /// json response
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> ContactUs(string conemail, string confullname, string conarea, string conphone)
        {
            if (!string.IsNullOrEmpty(conemail) && !string.IsNullOrEmpty(confullname) && !string.IsNullOrEmpty(conarea) && !string.IsNullOrEmpty(conphone))
            {
                var htmlBody = string.Empty;
                var filePath = Path.Combine(this.hostingEnvironment.WebRootPath + "/Templates/", "Feedback.html");
                var currentUrl = this.HttpContext.Request.Host.ToString().ToLower();
                using (StreamReader sourceReader = System.IO.File.OpenText(filePath))
                {
                    htmlBody = await sourceReader.ReadToEndAsync();
                    htmlBody = htmlBody.Replace("##HOSTURL##", this.domainSetting.WebSiteUrl);
                    htmlBody = htmlBody.Replace("##Name##", confullname);
                    htmlBody = htmlBody.Replace("##Email##", conemail);
                    htmlBody = htmlBody.Replace("##Phone##", conphone);
                    htmlBody = htmlBody.Replace("##Feedback##", conarea);
                }

                var subject = "Share Feedback";

                SendMail.MailSend(subject, htmlBody, Constants.RequestCallBackAdminEmail);

                return this.Json(new { Status = true, Message = "Sent successfully" });
            }

            return this.Json(new { Status = false, Message = "Invalid" });
        }

        /// <summary>
        /// Policies this instance.
        /// </summary>
        /// <returns>Plicies</returns>
        public IActionResult Policies()
        {
            return this.View();
        }

        /// <summary>
        /// Careers this instance.
        /// </summary>
        /// <returns>Careers</returns>
        public IActionResult Careers()
        {
            return this.View();
        }

        /// <summary>
        /// Blogs this instance.
        /// </summary>
        /// <returns>Carees</returns>
        public IActionResult Blog()
        {
            this.ViewBag.PageType = Enums.SeoPageType.Static;
            return this.View();
        }

        /// <summary>
        /// Terms the and condtion.
        /// </summary>
        /// <returns>TermAndCondtion</returns>
        public IActionResult TermAndCondtion()
        {
            this.ViewBag.PageType = Enums.SeoPageType.Static;
            return this.View();
        }

        /// <summary>
        /// Helps the i want to book.
        /// </summary>
        /// <returns>view</returns>
        public IActionResult HelpIWantToBook()
        {
            this.ViewBag.PageType = Enums.SeoPageType.Static;
            return this.View();
        }

        /// <summary>
        /// Requests the call back.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>view</returns>
        public async Task<IActionResult> RequestCallBack(RequestCallBackViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var jj = model.BestTimeToCall.ToDisplayName();
                var htmlBody = string.Empty;
                var filePath = Path.Combine(this.hostingEnvironment.WebRootPath + "/Templates/", "RequestCallBack.html");
                var currentUrl = this.HttpContext.Request.Host.ToString().ToLower();
                using (StreamReader sourceReader = System.IO.File.OpenText(filePath))
                {
                    htmlBody = await sourceReader.ReadToEndAsync();
                    htmlBody = htmlBody.Replace("##HOSTURL##", this.domainSetting.WebSiteUrl);
                    htmlBody = htmlBody.Replace("##Name##", model.Name);
                    htmlBody = htmlBody.Replace("##Phone##", model.Mobile);
                    htmlBody = htmlBody.Replace("##Email##", model.Email);
                    htmlBody = htmlBody.Replace("##BestTime##", model.BestTimeToCall.ToDisplayName());
                    htmlBody = htmlBody.Replace("##RequestFrom##", model.PageUrl);
                }

                var subject = "Request to call back";

                SendMail.MailSend(subject, htmlBody, Constants.RequestCallBackAdminEmail);

                ////return this.Json(new { Status = true, Message = Messages.Requestcallback });
                return this.View("thankyou");
            }

            return this.Json(new { Status = false, Message = "Invalid" });
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="first_Name">First Name</param>
        /// <param name="last_Name">Last Name</param>
        /// <param name="email">Email</param>
        /// <param name="mobile">Mobile</param>
        /// <param name="phone">Phone</param>
        /// <param name="no_Of_Pax">No of Pax</param>
        /// <param name="no_Of_Nights">No of Nights</param>
        /// <param name="no_of_Rooms">No of Rooms</param>
        /// <param name="no_of_Adult">No of Adult</param>
        /// <param name="no_of_Children">No of Children</param>
        /// <param name="no_of_Infant">No of Infant</param>
        /// <param name="dealId">dealId</param>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>view for home</returns>
        [Route("SendLead")]
        public async Task<IActionResult> SendLead(string first_Name, string last_Name, string email, string mobile, string phone, int no_Of_Pax, int no_Of_Nights, int no_of_Rooms, int no_of_Adult, int no_of_Children, int no_of_Infant, int dealId, string startDate, string endDate)
        {
            bool mailStatus = false;
            if (!string.IsNullOrEmpty(email))
            {
                DateTime dealStart = DateTime.Parse(startDate);
                DateTime dealEnd = DateTime.Parse(endDate);

                SendLeadViewModel sendLeadViewModel = await this.dealService.GetSendDealinfo(dealId);
                sendLeadViewModel.SiteUrl = this.domainSetting.WebSiteUrl;
                sendLeadViewModel.LeadName = first_Name;
                sendLeadViewModel.first_Name = first_Name;
                sendLeadViewModel.last_Name = last_Name;
                sendLeadViewModel.email = email;
                sendLeadViewModel.mobile = mobile;
                sendLeadViewModel.phone = phone;
                sendLeadViewModel.no_Of_Pax = no_Of_Pax;
                sendLeadViewModel.no_Of_Nights = Convert.ToInt32((dealEnd - dealStart).TotalDays);
                sendLeadViewModel.no_of_Rooms = no_of_Rooms;
                sendLeadViewModel.no_of_Adult = no_of_Adult;
                sendLeadViewModel.no_of_Children = no_of_Children;
                sendLeadViewModel.no_of_Infant = no_of_Infant;
                sendLeadViewModel.dealId = dealId;
                sendLeadViewModel.startDate = dealStart;
                sendLeadViewModel.endDate = dealEnd;
                var result = await this.viewRenderService.RenderToStringAsync("MailTemplate/_SendLead", sendLeadViewModel);
                var subject = Constants.BookingMailSubject;
                mailStatus = SendMail.MailSend(subject, this.Content(result).Content, email);
            }
            else
            {
                return this.Json(new { Status = false, Message = "Email id wrong." });
            }

            if (mailStatus)
            {
                return this.Json(new { Status = true, Message = "Success" });
            }
            else
            {
                return this.Json(new { Status = false, Message = "Failed" });
            }
        }

        /// <summary>
        /// Thanks you.
        /// </summary>
        /// <returns>view</returns>
        public IActionResult ThankYou()
        {
            return this.View();
        }

        private FileInfo[] GetAllFiles()
        {
            FileInfo[] files;
            var imagesLink = new List<string>();
            var wwwrootPath = this.hostingEnvironment.WebRootPath;
            var path = wwwrootPath + "/BannerImages";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            files = directoryInfo.GetFiles();
            return files;
        }
    }
}