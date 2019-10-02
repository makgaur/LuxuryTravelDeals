using HiTours.Api.Common;
using HiTours.Models;
using HiTours.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.Api.Configuration.Business
{
   public  class HomePageSearchBusiness: IHomePageSearchBusiness
    {
        /// <summary>
        /// The master service
        /// </summary>
        private readonly IHomePageService homePageService;

        public HomePageSearchBusiness(IHomePageService homePageService)
        {

            this.homePageService = homePageService;
           
        }






    }
}
