using HiTours.Api.Common;
using HiTours.Api.Configuration.Contract;
using HiTours.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HiTours.Api.Configuration.Business
{
    public interface IHomePageBusiness
    {

        Task<Response<HomePageConfigurationRS>> GetHomePageConfiguration();
        Task<Response<ListingViewModel>> GetSearchResults(ListingViewModel listingViewModel);
    }
}
