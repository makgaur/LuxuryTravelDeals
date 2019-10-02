// <copyright file="FlightApiController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using HiTours.Core;
    using HiTours.ViewModels;
    using HiTours.ViewModels.FlightApi;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;

    /// <summary>
    /// FlightApiController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class FlightApiController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>
        /// View
        /// </returns>
        public IActionResult Index()
        {
            var oBJSeg = new ApiSegments
            {
                Destination = "BOM",
                Origin = "DEL",
                FlightCabinClass = FlightCabinClass.All,
                PreferredDepartureTime = Convert.ToDateTime("2017-11-24T00: 00: 00"),
                PreferredArrivalTime = Convert.ToDateTime("2017-11-24T00: 00: 00")
            };
            var zNList = new List<ApiSegments>();
            zNList.Add(oBJSeg);
            var newArayList = new string[9];
            newArayList[0] = "SG";
            newArayList[1] = "6E";
            newArayList[2] = "G8";
            newArayList[3] = "G9";
            newArayList[4] = "FZ";
            newArayList[5] = "IX";
            newArayList[6] = "AK";
            newArayList[7] = "LB";
            newArayList[8] = "SG";
            var api = FlightApi.Authenticate();
            var url = Constants.BaseUrl + "BookingEngineService_Air/AirService.svc/rest/Search/";
            var requestData = JsonConvert.SerializeObject(new
            {
                EndUserIp = Constants.PublicIP,
                TokenId = api.TokenId,
                AdultCount = 1,
                ChildCount = 0,
                InfantCount = 0,
                DirectFlight = true,
                OneStopFlight = true,
                JourneyType = ApiJourneyType.OneWay,
                PreferredAirlines = string.Empty,
                Segments = zNList,
                Sources = newArayList
            });
            var apiResponse = FlightApi.GetApiResponse(requestData, url);
            var result = JsonConvert.DeserializeObject<ApiSearchResponse>(apiResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            this.ViewBag.ApiResult = result;
            return this.View(apiResponse);
        }

        /// <summary>
        /// Apibalances this instance.
        /// </summary>
        /// <returns>
        /// Apibalance
        /// </returns>
        public JsonResult Apibalance()
        {
            var api = FlightApi.Authenticate();
            var url = Constants.BaseUrl + "SharedServices/SharedData.svc/rest/GetAgencyBalance";
            var requestData = JsonConvert.SerializeObject(new
            {
                ClientId = Constants.ClientId,
                TokenAgencyId = api.Member.AgencyId,
                TokenMemberId = api.Member.MemberId,
                EndUserIp = Constants.PublicIP,
                TokenId = api.TokenId
            });
            var result = FlightApi.GetApiResponse(requestData, url);
            var resp = JsonConvert.DeserializeObject<ApiBalance>(result);
            return this.Json(resp);
        }

        /// <summary>
        /// Flights the search.
        /// </summary>
        /// <returns>
        /// resp
        /// </returns>
        public JsonResult FlightSearch()
        {
            var oBJSeg = new ApiSegments
            {
                Destination = "BOM",
                Origin = "DEL",
                FlightCabinClass = FlightCabinClass.All,
                PreferredDepartureTime = Convert.ToDateTime("2017-11-24T00: 00: 00"),
                PreferredArrivalTime = Convert.ToDateTime("2017-11-24T00: 00: 00")
            };
            var zNList = new List<ApiSegments>();
            zNList.Add(oBJSeg);
            var newArayList = new string[9];
            newArayList[0] = "SG";
            newArayList[1] = "6E";
            newArayList[2] = "G8";
            newArayList[3] = "G9";
            newArayList[4] = "FZ";
            newArayList[5] = "IX";
            newArayList[6] = "AK";
            newArayList[7] = "LB";
            newArayList[8] = "SG";
            var api = FlightApi.Authenticate();
            var url = Constants.BaseUrl + "BookingEngineService_Air/AirService.svc/rest/Search/";
            var requestData = JsonConvert.SerializeObject(new
            {
                EndUserIp = Constants.PublicIP,
                TokenId = api.TokenId,
                AdultCount = 1,
                ChildCount = 0,
                InfantCount = 0,
                DirectFlight = true,
                OneStopFlight = true,
                JourneyType = ApiJourneyType.OneWay,
                PreferredAirlines = string.Empty,
                Segments = zNList,
                Sources = newArayList
            });
            var result = FlightApi.GetApiResponse(requestData, url);
            var resp = JsonConvert.DeserializeObject<ApiSearchResponse>(result);
            return this.Json(result);
        }
    }
}