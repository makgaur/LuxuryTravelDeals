namespace HiTours.Api.Configuration.Business
{
    using HiTours.Api.Common;
    using HiTours.Api.Configuration.Contract;
    using HiTours.Core;
    using HiTours.Services;
    using HiTours.ViewModels;
    using System.Threading.Tasks;

    public class HomePageBusiness : IHomePageBusiness
    {

        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;
        private readonly ICurationsService curationService;
        private readonly IListingService listingService;
        private readonly IBlogService blogService;
        private readonly IHomeBannerService homeBanner;
        private readonly ICountryService countryService;
        private readonly IHomePageService homePageService;
       

        public HomePageBusiness(IMasterService masterService, ICurationsService curationService, IListingService listingService, IBlogService blogService, IHomeBannerService homeBanner, ICountryService countryService, IHomePageService homePageService)
        {

            this.masterService = masterService;
            this.curationService = curationService;
            this.listingService = listingService;
            this.blogService = blogService;
            this.homeBanner = homeBanner;
            this.countryService = countryService;
            this.homePageService = homePageService;
        }

        public async Task<Response<HomePageConfigurationRS>> GetHomePageConfiguration() 
        {

            var homePageConfigurationRS = new HomePageConfigurationRS();
            homePageConfigurationRS.PageType = Enums.SeoPageType.Static;
            homePageConfigurationRS.TravelStyle= await this.masterService.GetHomeTravelStyleListAsync();
            homePageConfigurationRS.BannersList = await this.homeBanner.GetBannersListAsync();
            homePageConfigurationRS.PopularDestinations = await this.homeBanner.GetPopularDestinationListAsync();
            homePageConfigurationRS.CurationBanner = this.curationService.GetCurationMainPage();
            homePageConfigurationRS.BlogPosts = this.blogService.GetBlogsMainPage();
            homePageConfigurationRS.FlashDeals= await this.listingService.GetTop3FlashDeals();
            homePageConfigurationRS.DealOfMonth= await this.listingService.GetTop3DealsOfTheMonth();
            var locationDealsCurationViews = await this.homePageService.GetActiveLocations();
            if (locationDealsCurationViews.Count > 0)
            {
                for (int i = 0; i < locationDealsCurationViews.Count; i++)
                {
                    locationDealsCurationViews[i].Results = this.listingService.GetTop3CityDeals(locationDealsCurationViews[i].City);
                }
            }

            var locationBasedCuration = locationDealsCurationViews;
            homePageConfigurationRS.LocationBasedCuration = locationBasedCuration;


            return new Response<HomePageConfigurationRS>() { ResultType = ResultType.Success, Result = homePageConfigurationRS };
        }

        public async Task<Response<ListingViewModel>> GetSearchResults(ListingViewModel listingViewModel)
        {
            var result = this.listingService.GetSearchResults(listingViewModel);
            return new Response<ListingViewModel>() { ResultType = ResultType.Success, Result = listingViewModel };
        }
    }
}
