using HiTours.Data.DataBase.Model;
using HiTours.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using static HiTours.Core.Enums;

namespace HiTours.Api.Configuration.Contract
{
   public class HomePageConfigurationRS
    {
        public SeoPageType PageType { get; set; }
        public List<PackageTravelStyleModel> TravelStyle { get; set; }

        public IList<HomeBannerViewModel> BannersList { get; set; }

        public List<PopularDestinationViewModel> PopularDestinations { get; set; }


        public CurationLayoutViewModel CurationBanner { get; set; }


        public BlogsLayoutViewModel BlogPosts { get; set; }

        public List<PackageCurationViewModel> FlashDeals { get; set; }


        public List<PackageCurationViewModel> DealOfMonth { get; set; }

        public List<LocationDealsCurationViewModel> LocationBasedCuration { get; set; }



    }
}
