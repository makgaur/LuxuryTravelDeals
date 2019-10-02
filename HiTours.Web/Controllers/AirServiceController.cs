// <copyright file="AirServiceController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using HiTours.Core;
    using HiTours.Data.DataBase.Model;
    using HiTours.Models;
    using HiTours.Services;
    using HiTours.TBO.Models;
    using HiTours.Web.Framework;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// AirServiceController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class AirServiceController : BaseController
    {
        /// <summary>
        /// Service Credential
        /// </summary>
        protected readonly UserCredential userCredential;

        /// <summary>
        /// The service urls
        /// </summary>
        protected readonly string serviceUrl;

        /// <summary>
        /// The view render service
        /// </summary>
        private readonly IViewRenderService viewRender;

        /// <summary>
        /// The user detail service
        /// </summary>
        private readonly IUserDetailService userDetailService;

        /// <summary>
        /// The package service
        /// </summary>
        private readonly IFlightBookingService flightBookingService;

        /// <summary>
        /// The hotel booking service
        /// </summary>
        private readonly IHotelBookingService hotelBookingService;

        /// <summary>
        /// The master service
        /// </summary>
        private readonly IMasterService masterService;

        /// <summary>
        /// The domain setting
        /// </summary>
        private readonly DomainSetting domainSetting;

        /// <summary>
        /// Initializes a new instance of the <see cref="AirServiceController" /> class.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        /// <param name="hotelBookingService">The hotel booking service.</param>
        /// <param name="domainSetting">The domain setting.</param>
        /// <param name="viewRender">The view render.</param>
        /// <param name="flightBookingService">The flight booking service.</param>
        /// <param name="userDetailService">The user detail service.</param>
        /// <param name="masterService">The master service.</param>
        public AirServiceController(IOptions<UserCredential> userCredential, IHotelBookingService hotelBookingService, IOptions<DomainSetting> domainSetting, IViewRenderService viewRender, IFlightBookingService flightBookingService, IUserDetailService userDetailService, IMasterService masterService)
        {
            this.hotelBookingService = hotelBookingService;
            this.domainSetting = domainSetting.Value;
            this.userCredential = userCredential.Value;
            this.serviceUrl = domainSetting.Value.WebApiServiceUrl;
            this.viewRender = viewRender;
            this.flightBookingService = flightBookingService;
            this.userDetailService = userDetailService;
            this.masterService = masterService;
        }

        /// <summary>
        /// Gets the json ignore nullable.
        /// </summary>
        /// <value>
        /// The json ignore nullable.
        /// </value>
        protected JsonSerializerSettings JsonIgnoreNullable
        {
            get { return new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }; }
        }

        /// <summary>
        /// Adds the new segment.
        /// </summary>
        /// <param name="segment">The segment.</param>
        /// <returns>Add New Segment</returns>
        public IActionResult AddNewSegment(Segments segment)
        {
            segment.CanRemove = true;
            return this.PartialView("_Segments", segment);
        }

        /// <summary>
        /// Searches the flights.
        /// </summary>
        /// <param name="bi">The bid.</param>
        /// <param name="modify">if set to <c>true</c> [modify].</param>
        /// <returns>
        /// Search Air Flights
        /// </returns>
        public async Task<IActionResult> SearchFlights(Guid? bi, bool modify = false)
        {
            var airserach = new AirSearch
            {
                AdultCount = 1,
                JourneyType = (int)JourneyType.Return,
                Segments = new Segments[] { new Segments(), new Segments() }
            };

            if (bi != null && Guid.Parse(bi.Value.ToString()) != Guid.Empty)
            {
                var bookingDetail = await this.hotelBookingService.GetTourBookingSendMailDetailAsync(Guid.Parse(bi.Value.ToString()));
                if (bookingDetail != null)
                {
                    ////airserach.HotelBookingId = Guid.Parse(bi.Value.ToString());

                    if (bookingDetail.PersonDetail != null)
                    {
                        airserach.AdultCount = bookingDetail.PersonDetail.Count(x => x.PersonType == Enums.PersonType.Adult.ToString());
                        airserach.ChildCount = bookingDetail.PersonDetail.Count(x => x.PersonType == Enums.PersonType.Child.ToString());
                        airserach.InfantCount = bookingDetail.PersonDetail.Count(x => x.PersonType == Enums.PersonType.Infant.ToString());
                    }

                    await this.GetSegments(airserach.Segments[0], bookingDetail.DepartCityCode, bookingDetail.HotelCityCode);
                    await this.GetSegments(airserach.Segments[1], bookingDetail.HotelCityCode, bookingDetail.DepartCityCode);

                    if (bookingDetail.BookingFrom != DateTime.MinValue)
                    {
                        airserach.Segments[0].PreferredDepartureTime = bookingDetail.BookingFrom;
                        airserach.Segments[0].PreferredArrivalTime = bookingDetail.BookingFrom;

                        airserach.Segments[1].PreferredDepartureTime = bookingDetail.EndDate;
                        airserach.Segments[1].PreferredArrivalTime = bookingDetail.EndDate;
                    }
                }
            }

            ////airserach.Segments.FirstOrDefault().DestinationList = airserach.Segments.FirstOrDefault().DestinationList ?? new List<SelectListItem>();
            ////airserach.Segments.FirstOrDefault().OriginList = airserach.Segments.FirstOrDefault().OriginList ?? new List<SelectListItem>();

            if (airserach.Segments.FirstOrDefault().DestinationList.Count > 0 &&
                airserach.Segments.FirstOrDefault().OriginList.Count > 0 &&
                !string.IsNullOrEmpty(airserach.Segments.FirstOrDefault().Origin) &&
                !string.IsNullOrEmpty(airserach.Segments.FirstOrDefault().Origin))
            {
                this.ViewBag.AutoSearch = !modify;
            }

            airserach.JourneyType = (int)JourneyType.Return;

            this.ViewBag.ClearSearch = true;
            return this.View(airserach);
        }

        /// <summary>
        /// Searches the flights.
        /// </summary>
        /// <param name="airSearch">The air search.</param>
        /// <returns>Search Air Flights</returns>
        [HttpPost]
        public async Task<IActionResult> SearchFlights(AirSearch airSearch)
        {
            if (airSearch.Segments != null)
            {
                foreach (var item in airSearch.Segments)
                {
                    item.FlightCabinClass = 1;
                    ////item.FlightCabinClass = airSearch.FlightCabinClass;
                    item.PreferredDepartureTime = airSearch.DepartureDateTime != null
                        ? DateTime.Parse(airSearch.DepartureDateTime.ToString())
                        : item.PreferredDepartureTime;
                    item.PreferredArrivalTime = item.PreferredDepartureTime;
                    var origin = await this.masterService.GetFlightDestinationByCityCode(item.Origin);
                    var destination = await this.masterService.GetFlightDestinationByCityCode(item.Destination);
                    if (origin != null)
                    {
                        item.OriginCityName = origin.CityName;
                        item.DisplayOrigin = origin.SearchIn;
                    }

                    if (destination != null)
                    {
                        item.DestinationCityName = destination.CityName;
                        item.DisplayDestination = destination.SearchIn;
                    }
                }

                if (airSearch.JourneyType == (int)JourneyType.Return || airSearch.ReturnDateTime != null)
                {
                    airSearch.JourneyType = (int)JourneyType.Return;
                    airSearch.ReturnFlight = true;
                    ////var oneWay = airSearch.Segments.FirstOrDefault();
                    ////if (oneWay != null)
                    ////{
                    ////    airSearch.ReturnDateTime = airSearch.ReturnDateTime ?? DateTime.MinValue;
                    ////    var returnWay = new Segments
                    ////    {
                    ////        PreferredArrivalTime = DateTime.Parse(airSearch.ReturnDateTime.ToString()),
                    ////        PreferredDepartureTime = DateTime.Parse(airSearch.ReturnDateTime.ToString()),
                    ////        Destination = oneWay.Origin,
                    ////        Origin = oneWay.Destination
                    ////    };
                    ////    airSearch.Segments = new Segments[] { oneWay, returnWay };
                    ////}
                }
            }

            this.ViewBag.SearchRequest = JsonConvert.SerializeObject(airSearch);
            return this.View(airSearch);
        }

        /// <summary>
        /// Flightses this instance.
        /// </summary>
        /// <returns>Get Air Flight List</returns>
        public IActionResult Flights()
        {
            return this.View();
        }

        /// <summary>
        /// Gets the journey type search criteria.
        /// </summary>
        /// <param name="journeyType">Type of the journey.</param>
        /// <returns>Get Search Criteria</returns>
        public IActionResult GetJourneyTypeSearchCriteria(int journeyType)
        {
            var airserach = new AirSearch
            {
                JourneyType = journeyType == 0 ? (int)JourneyType.OneWay : journeyType,
                AdultCount = 1
            };

            if (journeyType == (int)JourneyType.MultiStop)
            {
                airserach.Segments = new List<Segments>() { new Segments(), new Segments() }.ToArray();
            }
            else
            {
                airserach.Segments = new List<Segments>() { new Segments() }.ToArray();
            }

            this.ViewBag.LayoutBlank = false;
            return this.PartialView("SearchFlights", airserach);
        }

        /// <summary>
        /// Gets the flight criteria.
        /// </summary>
        /// <param name="airSearch">The air search.</param>
        /// <returns>Get Flight Criteria</returns>
        public async Task<IActionResult> GetFlightSearchCriteria(AirSearch airSearch)
        {
            if (airSearch.Segments != null)
            {
                foreach (var item in airSearch.Segments)
                {
                    var origin = await this.masterService.GetFlightDestinationByCityCode(item.Origin);
                    var destination = await this.masterService.GetFlightDestinationByCityCode(item.Destination);
                    if (origin != null)
                    {
                        item.OriginCityName = origin.CityName;
                        item.DisplayOrigin = origin.SearchIn;
                    }

                    if (destination != null)
                    {
                        item.DestinationCityName = destination.CityName;
                        item.DisplayDestination = destination.SearchIn;
                    }
                }
            }

            return this.PartialView("FlightSearchCriteria", airSearch);
        }

        /// <summary>
        /// Gets the flights.
        /// </summary>
        /// <param name="airSearch">The air search.</param>
        /// <returns>GetFlights</returns>
        public async Task<IActionResult> GetFlights(AirSearch airSearch)
        {
            var isValidToken = !string.IsNullOrEmpty(airSearch.TokenId);
            if (!isValidToken)
            {
                airSearch.TokenId = await this.GetAuthenticationToken();
                isValidToken = !string.IsNullOrEmpty(airSearch.TokenId);
            }

            airSearch.EndUserIp = this.userCredential.EndUserIp;
            airSearch.Sources = null;
            airSearch.PreferredAirlines = null;

            var cityCodes = new List<string>();
            cityCodes.AddRange(airSearch.Segments.Select(x => x.Origin).ToList());
            cityCodes.AddRange(airSearch.Segments.Select(x => x.Destination).ToList());
            var flightDestinations = await this.masterService.GetFlightDestination(cityCodes.Distinct().ToArray());

            var searchFlightUrl = $"{this.serviceUrl}/AirService/SearchForRequest";
            var searchApiResponse = await this.PostAsync(searchFlightUrl, JsonConvert.SerializeObject(airSearch, Formatting.Indented, this.JsonIgnoreNullable));

            var airSearchResponse = new AirSearchResponse();
            if (searchApiResponse.IsSuccess)
            {
                var result = JsonConvert.DeserializeObject<AirSearchResponse>(searchApiResponse.Response);
                if (result.Response == null)
                {
                    result.Response = new ApiSearchResultResponse();
                }

                var traceid = result.Response.TraceId;

                ////var airSearchResults = new List<AirSearchResult>();
                if (result.Response.Results != null)
                {
                    foreach (var airSearchResults in result.Response.Results)
                    {
                        ////airSearchResults.AddRange(item);
                        if (airSearchResults != null)
                        {
                            foreach (var airSearchResult in airSearchResults)
                            {
                                ////airSearchResult.FlightCabinClass = airSearch.Segments.FirstOrDefault().FlightCabinClass;
                                airSearchResult.FlightCabinClass = 1;

                                if (airSearchResult.FareBreakdown != null && airSearchResult.FareBreakdown.Length > 0)
                                {
                                    foreach (var item in airSearchResult.FareBreakdown)
                                    {
                                        item.MarkupPrice = 0;
                                    }

                                    airSearchResult.TotalPassengers = airSearchResult.AdultCount + airSearchResult.ChildCount + airSearchResult.InfantCount;
                                    airSearchResult.TotalBaseFareAmount = airSearchResult.FareBreakdown.Sum(x => x.BaseFare * x.PassengerCount);
                                    airSearchResult.TotalBaseFareTaxAmount = airSearchResult.FareBreakdown.Sum(x => (x.Tax + x.YQTax) * x.PassengerCount);
                                    airSearchResult.TotalAmount = airSearchResult.TotalBaseFareAmount + airSearchResult.TotalBaseFareTaxAmount;
                                    airSearchResult.JourneyType = airSearch.JourneyType;
                                }
                            }
                        }
                    }
                }

                result.Response.TokenId = airSearch.TokenId;
                if (result.Response.Error != null && result.Response.Error.ErrorMessage.ToLower().Contains("invalid session"))
                {
                    result.Response.SessionExpired = true;
                }

                ////result.Response.Results = new AirSearchResult[1][];
                ////result.Response.Results[0] = airSearchResults.ToArray();
                result.Response.TraceId = traceid;

                if (result.Response != null)
                {
                    result.Response.JourneyType = airSearch.JourneyType;
                    result.Response.InterNationalReturnFlight = (flightDestinations.Count > 0) && flightDestinations.Select(x => x.CountryName).Distinct().Count() > 1;
                }

                return this.PartialView("_Flights", result);
            }
            else
            {
                airSearchResponse.Response = new ApiSearchResultResponse
                {
                    Error = new ApiError()
                    {
                        ErrorMessage = searchApiResponse.Exception,
                        ErrorCode = 500
                    }
                };
            }

            return this.PartialView("_Flights", airSearchResponse);
        }

        /// <summary>
        /// Gets the passengers.
        /// </summary>
        /// <param name="flightBooking">The flight booking.</param>
        /// <param name="hbId">The hb identifier.</param>
        /// <returns>
        /// Get Passenger Details
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> GetPassengers(FlightBook flightBooking, Guid hbId)
        {
            if (!string.IsNullOrEmpty(flightBooking.ResultIndex))
            {
                List<ViewModels.HotelBookingPersonDetailViewModel> personDetail = null;

                if (hbId != Guid.Empty)
                {
                    var bookingDetail = await this.hotelBookingService.GetTourBookingSendMailDetailAsync(hbId);
                    if (bookingDetail != null)
                    {
                        if (bookingDetail.PersonDetail != null)
                        {
                            personDetail = bookingDetail.PersonDetail;
                        }

                        flightBooking.HotelBookingId = hbId;
                        flightBooking.HotelBookingPrice = bookingDetail.PaidAmount;
                        flightBooking.HotelName = bookingDetail.HotelName;
                    }
                }

                flightBooking.Passengers = flightBooking.Passengers ?? new List<Passengers>();
                flightBooking.AirSearchResults = flightBooking.AirSearchResults ?? new List<AirSearchResult>();
                foreach (var resultindex in flightBooking.ResultIndex.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    flightBooking.AirSearchResults.Add(new AirSearchResult() { ResultIndex = resultindex });
                }

                for (int adultCount = 0; adultCount < flightBooking.AdultCount; adultCount++)
                {
                    var adultPerson = new Passengers();
                    adultPerson.IsLeadPax = true;
                    adultPerson.PaxType = ((int)PaxType.Adult).ToString();
                    adultPerson.Index = adultCount + 1;
                    if (hbId != Guid.Empty && personDetail != null && personDetail.Count > 0)
                    {
                        var adultPersons = personDetail.Where(x => x.PersonType == Enums.PersonType.Adult.ToString()).ToList();
                        if (adultPersons[adultCount] != null)
                        {
                            await this.UpdatePersonDetails(adultCount, adultPerson, adultPersons);
                        }
                    }

                    flightBooking.Passengers.Add(adultPerson);
                }

                for (int childCount = 0; childCount < flightBooking.ChildCount; childCount++)
                {
                    var childPassenger = new Passengers();
                    childPassenger.IsLeadPax = true;
                    childPassenger.PaxType = ((int)PaxType.Child).ToString();
                    childPassenger.Index = childCount + 1;
                    if (hbId != Guid.Empty && personDetail != null && personDetail.Count > 0)
                    {
                        var childPersons = personDetail.Where(x => x.PersonType == Enums.PersonType.Child.ToString()).ToList();
                        if (childPersons[childCount] != null)
                        {
                            childPassenger.FirstName = childPersons[childCount].FirstName;
                            childPassenger.LastName = childPersons[childCount].LastName;
                            await this.UpdatePersonDetails(childCount, childPassenger, childPersons);
                        }
                    }

                    flightBooking.Passengers.Add(childPassenger);
                }

                for (int infantCount = 0; infantCount < flightBooking.InfantCount; infantCount++)
                {
                    var infantPassenger = new Passengers();
                    infantPassenger.IsLeadPax = true;
                    infantPassenger.PaxType = ((int)PaxType.Infant).ToString();
                    infantPassenger.Index = infantCount + 1;
                    if (hbId != Guid.Empty && personDetail != null && personDetail.Count > 0)
                    {
                        var infantPersons = personDetail.Where(x => x.PersonType == Enums.PersonType.Infant.ToString()).ToList();
                        if (infantPersons[infantCount] != null)
                        {
                            await this.UpdatePersonDetails(infantCount, infantPassenger, infantPersons);
                        }
                    }

                    flightBooking.Passengers.Add(infantPassenger);
                }

                for (int counter = 1; counter <= flightBooking.AirSearchResults.Count; counter++)
                {
                    var searchResult = flightBooking.AirSearchResults[counter - 1];
                    flightBooking.AutoBooking = (!searchResult.IsLCC) && hbId != Guid.Empty;

                    var fareQuoteResponse = await this.GetFareQuote(searchResult.ResultIndex, flightBooking.TokenId, flightBooking.TraceId);

                    if (fareQuoteResponse.ResponseStatus == (int)AuthenticateStatus.Successful)
                    {
                        if (fareQuoteResponse.Results != null)
                        {
                            this.CalculateFareBreakDown(fareQuoteResponse);
                            flightBooking.AirSearchResults[counter - 1] = fareQuoteResponse.Results;
                            flightBooking.TotalAmount += fareQuoteResponse.Results.TotalAmount;
                            flightBooking.TotalBaseFareTaxAmount += fareQuoteResponse.Results.TotalBaseFareTaxAmount;
                            flightBooking.TotalBaseFareAmount += fareQuoteResponse.Results.TotalBaseFareAmount;
                        }

                        if (counter == flightBooking.AirSearchResults.Count)
                        {
                            flightBooking.StatusFlightQuote = true;
                            flightBooking.TotalPassengers = flightBooking.Passengers.Count;
                        }
                    }
                    else if (fareQuoteResponse.Error.ErrorMessage == "TokenID can not be Null or Empty")
                    {
                        flightBooking = new FlightBook { StatusFlightQuote = false, Message = fareQuoteResponse.Error.ErrorMessage };
                        break;
                    }
                    else if (fareQuoteResponse.Error.ErrorMessage == "Your session (TraceId) is expired.")
                    {
                        this.ViewBag.ClearSearch = true;
                        flightBooking = new FlightBook { StatusFlightQuote = false, Message = fareQuoteResponse.Error.ErrorMessage };
                        break;
                    }
                }

                if (flightBooking.AirSearch != null && flightBooking.AirSearch.Segments != null)
                {
                    var cityCodes = new List<string>();
                    cityCodes.AddRange(flightBooking.AirSearch.Segments.Select(x => x.Origin).ToList());
                    cityCodes.AddRange(flightBooking.AirSearch.Segments.Select(x => x.Destination).ToList());
                    var flightDestinations = await this.masterService.GetFlightDestination(cityCodes.Distinct().ToArray());
                    var interNationalReturnFlight = (flightDestinations.Count > 0) && flightDestinations.Select(x => x.CountryName).Distinct().Count() > 1;

                    flightBooking.Passengers.ForEach(x =>
                    {
                        x.InterNationalReturnFlight = interNationalReturnFlight;
                    });
                }

                return this.PartialView("_FlightBooking", flightBooking);
            }
            else
            {
                return this.PartialView("_FlightBooking", new FlightBook { StatusFlightQuote = false, Message = "Please Provide a Valid Flight Details. <br/> Please Try Again" });
            }
        }

        /// <summary>
        /// Flights the booking.
        /// </summary>
        /// <returns>Book Selected Flight</returns>
        [Authorize]
        public IActionResult FlightBooking()
        {
            return this.View();
        }

        /// <summary>
        /// Searches the flights.
        /// </summary>
        /// <returns>
        /// Search Flights
        /// </returns>
        [Authorize]
        public IActionResult Payment()
        {
            return this.View();
        }

        /// <summary>
        /// Payments the specified flight booking.
        /// </summary>
        /// <param name="flightBooking">The flight booking.</param>
        /// <returns>Payment</returns>
        [HttpPost]
        public async Task<IActionResult> Payment(FlightBook flightBooking)
        {
            flightBooking.AirSearchResults = flightBooking.AirSearchResults ?? new List<AirSearchResult>();

            foreach (var resultindex in flightBooking.ResultIndex.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                flightBooking.AirSearchResults.Add(new AirSearchResult() { ResultIndex = resultindex });
            }

            for (int counter = 1; counter <= flightBooking.AirSearchResults.Count; counter++)
            {
                var searchResult = flightBooking.AirSearchResults[counter - 1];
                var fareQuoteResponse = await this.GetFareQuote(searchResult.ResultIndex, flightBooking.TokenId, flightBooking.TraceId);

                if (fareQuoteResponse.ResponseStatus == (int)AuthenticateStatus.Successful)
                {
                    if (fareQuoteResponse.Results != null)
                    {
                        this.CalculateFareBreakDown(fareQuoteResponse);
                        flightBooking.AirSearchResults[counter - 1] = fareQuoteResponse.Results;
                        flightBooking.TotalAmount += fareQuoteResponse.Results.TotalAmount;
                        flightBooking.TotalBaseFareTaxAmount += fareQuoteResponse.Results.TotalBaseFareTaxAmount;
                        flightBooking.TotalBaseFareAmount += fareQuoteResponse.Results.TotalBaseFareAmount;
                    }

                    if (counter == flightBooking.AirSearchResults.Count)
                    {
                        this.ViewBag.FlightBookPayment = JsonConvert.SerializeObject(flightBooking);
                        flightBooking.TotalPassengers = flightBooking.Passengers.Count;
                    }
                }
                else if (fareQuoteResponse.Error.ErrorMessage == "Your session (TraceId) is expired.")
                {
                    this.ViewBag.ClearSearch = true;
                }
            }

            return this.View(flightBooking);
        }

        /// <summary>
        /// Gets the booking summary.
        /// </summary>
        /// <param name="flightBooking">The flight booking.</param>
        /// <param name="hide">if set to <c>true</c> [hide].</param>
        /// <returns>
        /// Get Booking Summary
        /// </returns>
        public IActionResult GetBookingSummary(FlightBook flightBooking, bool hide)
        {
            if (hide)
            {
                flightBooking.Payment = PaymentStatus.Process;
            }

            return this.PartialView("_BookingSummary", flightBooking);
        }

        /// <summary>
        /// Procceds to payment.
        /// </summary>
        /// <returns>
        /// ProccedToPayment
        /// </returns>
        [HttpGet]
        public IActionResult ProccedToPayment()
        {
            ViewModels.BookingPayment bookingPayment = null;
            if (this.TempData["flightdetails"] != null)
            {
                bookingPayment = JsonConvert.DeserializeObject<ViewModels.BookingPayment>(this.TempData["flightbillingdetails"].ToString());
            }

            bookingPayment = bookingPayment ?? new ViewModels.BookingPayment();

            this.ViewBag.BookingPayment = bookingPayment;

            this.ViewBag.HidePayment = true;

            return this.View("Payment");
        }

        /// <summary>
        /// Confirms the payment.
        /// </summary>
        /// <param name="flightBooking">The flight booking.</param>
        /// <returns>
        /// Confirm payment
        /// </returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProccedToPayment(FlightBook flightBooking)
        {
            var userid = await this.userDetailService.GetUserIdAsync(this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email).Value);
            var invoiceNo = new Random().Next(0, 1000000).ToString("D6");
            var input = new Dictionary<string, object>
                    {
                        { "amount", Convert.ToDecimal(flightBooking.TotalAmount + flightBooking.HotelBookingPrice + flightBooking.MarkUpPrice) * 100 }, // this amount should be same as transaction amount
                        { "currency", Constants.RazorPayCurrency },
                        { "receipt", invoiceNo },
                        { "payment_capture", 1 }
                    };

            this.GetKeys(out string key, out string secret);

            var client = new Razorpay.Api.RazorpayClient(key, secret);
            var order = client.Order.Create(input);
            var orderId = order["id"].ToString();
            var currency = order["currency"].ToString();
            var receipt = order["receipt"].ToString();
            var status = order["status"].ToString();
            var userTransaction = new UserTransactionModel()
            {
                UserId = userid,
                OrderId = orderId,
                Currency = currency,
                ReceiptNo = receipt,
                OrderStatus = status,

                FlightDetail = JsonConvert.SerializeObject(flightBooking, Formatting.Indented, this.JsonIgnoreNullable)
            };

            if (flightBooking.HotelBookingId != Guid.Empty && flightBooking.HotelBookingPrice > 0)
            {
                userTransaction.HotelBookingId = flightBooking.HotelBookingId;
            }

            userTransaction.SetAuditInfo(new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value));
            await this.hotelBookingService.OrderGenerate(userTransaction);

            var userdetail = await this.userDetailService.GetByIdAsync(userTransaction.UserId);
            var bookingPayment = new ViewModels.BookingPayment
            {
                DepositAmount = flightBooking.TotalAmount,
                DataAmount = (int)(flightBooking.TotalAmount * 100),
                DataDescription = string.Empty,
                DataImage = Constants.RazorPayDataImage,
                DataKey = key,
                DataName = Constants.RazorPay,
                DataOrderId = userTransaction.OrderId,
                DataPrefillContact = userdetail.MobileNo,
                DataPrefillEmail = userdetail.EmailId,
                DataPrefillName = userdetail.FirstName + " " + userdetail.LastName,
                DataThemeColor = Constants.RazorPayDataThemeColor,
                JsSrc = Constants.RazorPayCheckoutJs
            };

            flightBooking.Payment = PaymentStatus.Process;

            this.TempData.Put("flightdetails", flightBooking);
            this.TempData.Put("flightbillingdetails", bookingPayment);

            return this.Json(bookingPayment);
        }

        /// <summary>
        /// Confirs the razor payment.
        /// </summary>
        /// <returns>ConfirRazorPayment</returns>
        public async Task<IActionResult> ConfirmPayment()
        {
            string paymentId = this.HttpContext.Request.Form["razorpay_payment_id"];
            string invoice_id = this.HttpContext.Request.Form["invoice_id"];
            this.GetKeys(out string key, out string secret);
            var client = new Razorpay.Api.RazorpayClient(key, secret);
            var attributes = new Dictionary<string, string>
            {
                { "razorpay_payment_id", paymentId },
                { "razorpay_order_id", this.HttpContext.Request.Form["razorpay_order_id"] },
                { "razorpay_signature", this.HttpContext.Request.Form["razorpay_signature"] }
            };
            Razorpay.Api.Utils.verifyPaymentSignature(attributes);

            var outer = JToken.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(client.Payment.Fetch(paymentId)));
            var payment = outer["Attributes"].Value<JObject>();

            var trans = await this.hotelBookingService.GetOrderDetailById(this.Request.Form["razorpay_order_id"]);
            trans.PaymentId = paymentId;
            trans.PaymentStatus = payment["status"].ToString();
            trans.PaymentMethod = payment["method"].ToString();
            trans.CardId = payment["card_id"].ToString();
            trans.WalletName = payment["wallet"].ToString();
            trans.Bank = payment["bank"].ToString();
            trans.ContactNo = payment["contact"].ToString();
            trans.Description = payment["description"].ToString();
            trans.Email = payment["email"].ToString();
            trans.ErrorCode = payment["error_code"].ToString();
            trans.ErrorDescription = payment["error_description"].ToString();
            trans.Fee = (decimal)(string.IsNullOrEmpty(payment["fee"].ToString()) ? 0 : payment["fee"]);
            trans.IsInternational = (bool)payment["international"];
            trans.PaymentDate = DateTime.Now; ////(DateTime)(string.IsNullOrEmpty(payment["created_at"].ToString()) ? null : payment["created_at"]);
            trans.Tax = (decimal)(string.IsNullOrEmpty(payment["tax"].ToString()) ? 0 : payment["tax"]);

            await this.hotelBookingService.UpdateOrderDetail(trans);

            var flightBookResult = new FlightBookResult();
            if (!string.IsNullOrEmpty(trans.FlightDetail))
            {
                var flightBooking = JsonConvert.DeserializeObject<FlightBook>(trans.FlightDetail);
                flightBooking.AirSearchResults = new List<AirSearchResult>();
                foreach (var resultindex in flightBooking.ResultIndex.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    flightBooking.AirSearchResults.Add(new AirSearchResult() { ResultIndex = resultindex });
                }

                for (int counter = 1; counter <= flightBooking.AirSearchResults.Count; counter++)
                {
                    var searchResult = flightBooking.AirSearchResults[counter - 1];
                    var fareQuoteResponse = await this.GetFareQuote(searchResult.ResultIndex, flightBooking.TokenId, flightBooking.TraceId);

                    if (fareQuoteResponse.ResponseStatus == (int)AuthenticateStatus.Successful)
                    {
                        if (fareQuoteResponse.Results != null)
                        {
                            this.CalculateFareBreakDown(fareQuoteResponse);
                            flightBooking.AirSearchResults[counter - 1] = fareQuoteResponse.Results;
                            flightBooking.TotalAmount += fareQuoteResponse.Results.TotalAmount;
                            flightBooking.TotalBaseFareTaxAmount += fareQuoteResponse.Results.TotalBaseFareTaxAmount;
                            flightBooking.TotalBaseFareAmount += fareQuoteResponse.Results.TotalBaseFareAmount;
                        }

                        if (counter == flightBooking.AirSearchResults.Count)
                        {
                            flightBooking.TotalPassengers = flightBooking.Passengers.Count;
                        }
                    }
                    else if (fareQuoteResponse.Error.ErrorMessage == "Your session (TraceId) is expired.")
                    {
                        flightBookResult.Status = false;
                        flightBookResult.Error = new ApiError { ErrorCode = 500, ErrorMessage = "Your session (TraceId) is expired." };
                        break;
                    }
                }

                var flightBooked = await this.AddUserTransaction(flightBooking, trans.RowNo);
                if (flightBooked.Count(x => x.Id == Guid.Empty) == 0)
                {
                    for (int i = 0; i < flightBooking.AirSearchResults.Count; i++)
                    {
                        flightBookResult.Status = false;
                        var searchResult = flightBooking.AirSearchResults[i];
                        foreach (var passenger in flightBooking.Passengers)
                        {
                            var breakdown = searchResult.FareBreakdown.Where(x => x.PassengerType.ToString() == passenger.PaxType).ToArray();
                            if (breakdown != null && breakdown.Length > 0)
                            {
                                passenger.FareBreakdown = breakdown.ToArray();
                            }
                        }

                        if (searchResult.IsLCC)
                        {
                            var ticket = new Ticket()
                            {
                                EndUserIp = this.userCredential.EndUserIp,
                                TokenId = flightBooking.TokenId,
                                TraceId = flightBooking.TraceId,
                                ResultIndex = searchResult.ResultIndex,
                                LCCRequest = true,
                                Passenger = flightBooking.Passengers
                            };
                            await this.BookTicket(flightBookResult, ticket);
                            await this.UpdateBookedFlight(flightBookResult, flightBooked, i);
                            flightBookResult.Booking = null;
                            if (flightBookResult.Error != null && flightBookResult.Error.ErrorCode > 0)
                            {
                                flightBookResult.Status = false;
                                break;
                            }
                        }
                        else
                        {
                            var bookingRequest = new Book()
                            {
                                EndUserIp = this.userCredential.EndUserIp,
                                TokenId = flightBooking.TokenId,
                                TraceId = flightBooking.TraceId,
                                ResultIndex = searchResult.ResultIndex,
                                Passenger = flightBooking.Passengers
                            };
                            var bookingApiUrl = $"{this.serviceUrl}/AirService/Book";
                            var apiResponse = await this.PostAsync(bookingApiUrl, JsonConvert.SerializeObject(bookingRequest, this.JsonIgnoreNullable));
                            if (apiResponse.IsSuccess)
                            {
                                var apiBookingResponseRoot = JsonConvert.DeserializeObject<BookingResponseRoot>(apiResponse.Response);
                                var apiBookingResponse = apiBookingResponseRoot.Response;
                                if (apiBookingResponse.Response != null && apiBookingResponse.ResponseStatus == (int)AuthenticateStatus.Successful)
                                {
                                    var bookingResult = apiBookingResponse.Response;
                                    if (bookingResult.BookingId > 0)
                                    {
                                        var ticket = new Ticket()
                                        {
                                            EndUserIp = this.userCredential.EndUserIp,
                                            TokenId = flightBooking.TokenId,
                                            TraceId = flightBooking.TraceId,
                                            BookingId = Convert.ToInt32(bookingResult.BookingId),
                                            PNR = bookingResult.Pnr
                                        };
                                        await this.BookTicket(flightBookResult, ticket);
                                        await this.UpdateBookedFlight(flightBookResult, flightBooked, i);
                                        flightBookResult.Booking = null;
                                        if (flightBookResult.Error != null && flightBookResult.Error.ErrorCode > 0)
                                        {
                                            flightBookResult.Status = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        flightBookResult.Status = false;
                                        flightBookResult.Error = apiBookingResponse.Error;
                                    }
                                }
                            }
                            else
                            {
                                flightBookResult.Status = false;
                                flightBookResult.Error = new ApiError { ErrorCode = 500, ErrorMessage = apiResponse.Exception };
                            }
                        }
                    }
                }
                else
                {
                    flightBookResult.Status = false;
                    flightBookResult.Error = new ApiError { ErrorCode = 500, ErrorMessage = "Transaction Cannot be Completed" };
                }
            }
            else
            {
                flightBookResult.Status = false;
                flightBookResult.Error = new ApiError { ErrorCode = 500, ErrorMessage = "Flight Request Parameter not found" };
            }

            if (!flightBookResult.Status)
            {
                this.ViewBag.ClearSearch = true;
            }

            ///// send mail to user with deails if bookign success and get pnr no

            if (this.domainSetting.RazorpayLive)
            {
                ////send sms
                await this.SendSms();
            }

            //////  sent mail--------------------
            await this.SendBookingMail(trans.HotelBookingId ?? Guid.Empty);
            if (flightBookResult.Bookings != null && flightBookResult.Bookings.Count > 0)
            {
                //// bind html template feilds with data and mail
                await this.SendFlightBookingMail(flightBookResult);
            }

            return this.View(flightBookResult);
            //// now book flight
        }

        /// <summary>
        /// Makes the payment.
        /// </summary>
        /// <param name="airSearchResult">The air search result.</param>
        /// <returns>
        /// Proced To Book Flight
        /// </returns>
        public IActionResult GetflightDetail(AirSearchResult airSearchResult)
        {
            return this.PartialView("_FlightDetail", airSearchResult);
        }

        /// <summary>
        /// Posts the asynchronous.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="content">The content.</param>
        /// <returns>
        /// Post Request
        /// </returns>
        private async Task<ApiResponse> PostAsync(string url, string content)
        {
            ApiResponse result;

            Uri uri;
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 40, 1);
                if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                {
                    result = new ApiResponse
                    {
                        IsSuccess = false,
                        Exception = "Invalid Requested Url.",
                        Response = JsonConvert.SerializeObject(new { Message = "Invalid Requested Url." })
                    };
                }
                else
                {
                    try
                    {
                        var apiResponse = await client.PostAsync(uri, new StringContent(content, Encoding.Default, "application/json"));
                        var response = await apiResponse.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<ApiResponse>(response);
                    }
                    catch (Exception ex)
                    {
                        result = new ApiResponse
                        {
                            Exception = ex.Message,
                            Request = content,
                            Response = JsonConvert.SerializeObject(new { Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message })
                        };
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the authentication token.
        /// </summary>
        /// <returns>Get Authentication Token</returns>
        private async Task<string> GetAuthenticationToken()
        {
            var authenticationToken = string.Empty;

            var authenticateUrl = $"{this.serviceUrl}/Authentication/Login";
            var apiResponse = await this.PostAsync(authenticateUrl, JsonConvert.SerializeObject(this.userCredential));
            if (apiResponse.IsSuccess)
            {
                var authenticateResult = JsonConvert.DeserializeObject<AuthenticateResponse>(apiResponse.Response);
                if (authenticateResult != null && authenticateResult.Status == (int)AuthenticateStatus.Successful)
                {
                    authenticationToken = authenticateResult.TokenId;
                }
            }

            return authenticationToken;
        }

        /// <summary>
        /// Gets the fair quote result.
        /// </summary>
        /// <param name="searchResultIndex">Index of the search result.</param>
        /// <param name="tokenId">The token identifier.</param>
        /// <param name="traceId">The trace identifier.</param>
        /// <returns>GetFareQuote</returns>
        private async Task<FareQuoteResult> GetFareQuote(string searchResultIndex, string tokenId, string traceId)
        {
            var fareRule = new FareRule
            {
                EndUserIp = this.userCredential.EndUserIp,
                ResultIndex = searchResultIndex,
                TokenId = tokenId,
                TraceId = traceId
            };

            var fareQoteUrl = $"{this.serviceUrl}/AirService/GetFareQuote";
            var fareQuoteApiResponse = await this.PostAsync(fareQoteUrl, JsonConvert.SerializeObject(fareRule));
            if (fareQuoteApiResponse.IsSuccess)
            {
                var fareQuoteResult = JsonConvert.DeserializeObject<FareQuoteResponse>(fareQuoteApiResponse.Response);
                if (fareQuoteResult != null && fareQuoteResult.Response != null)
                {
                    return fareQuoteResult.Response;
                }
            }

            return new FareQuoteResult();
        }

        private void CalculateFareBreakDown(FareQuoteResult fareQuoteResponse)
        {
            fareQuoteResponse.Results.TotalPassengers = fareQuoteResponse.Results.FareBreakdown.Sum(x => x.PassengerCount);
            fareQuoteResponse.Results.TotalBaseFareAmount = fareQuoteResponse.Results.FareBreakdown.Sum(x => x.BaseFare * x.PassengerCount);
            fareQuoteResponse.Results.TotalBaseFareTaxAmount = fareQuoteResponse.Results.FareBreakdown.Sum(x => (x.Tax + x.YQTax) * x.PassengerCount);
            fareQuoteResponse.Results.TotalAmount = fareQuoteResponse.Results.TotalBaseFareAmount + fareQuoteResponse.Results.TotalBaseFareTaxAmount;
        }

        /// <summary>
        /// Books the ticket.
        /// </summary>
        /// <param name="flightBookResult">The result.</param>
        /// <param name="ticket">The ticket.</param>
        /// <returns>Book Ticket for LCC or Non LCC</returns>
        private async Task BookTicket(FlightBookResult flightBookResult, Ticket ticket)
        {
            var ticket4LCCUrl = $"{this.serviceUrl}/AirService/Ticket";
            var apiResponse = await this.PostAsync(ticket4LCCUrl, JsonConvert.SerializeObject(ticket));
            if (apiResponse.IsSuccess)
            {
                var apiTicketResultRoot = JsonConvert.DeserializeObject<TicketLCCResponseRoot>(apiResponse.Response, this.JsonIgnoreNullable);
                var apiTicketResult = apiTicketResultRoot.Response;
                if (apiTicketResult != null && apiTicketResult.Response != null)
                {
                    if (apiTicketResult.ResponseStatus == (int)AuthenticateStatus.Successful)
                    {
                        var ticketResult = apiTicketResult.Response;
                        if (ticketResult != null)
                        {
                            if (ticketResult.BookingId > 0)
                            {
                                flightBookResult.Bookings = flightBookResult.Bookings ?? new List<BookingResult>();
                                var booking = new BookingResult
                                {
                                    BookingId = ticketResult.BookingId,
                                    Pnr = ticketResult.Pnr,
                                    Destination = ticketResult.FlightItinerary?.Destination,
                                    Origin = ticketResult.FlightItinerary?.Origin,
                                    FlightItinerary = ticketResult.FlightItinerary
                                };

                                flightBookResult.Json = JsonConvert.SerializeObject(ticketResult, Formatting.Indented, this.JsonIgnoreNullable);
                                flightBookResult.Booking = booking;
                                flightBookResult.Bookings.Add(booking);

                                flightBookResult.Status = true;
                            }
                            else
                            {
                                flightBookResult.Error = apiTicketResult.Error;
                            }
                        }
                    }
                    else
                    {
                        flightBookResult.Error = apiTicketResult.Error;
                    }
                }
                else
                {
                    flightBookResult.Error = apiTicketResult.Error;
                }
            }
            else
            {
                flightBookResult.Error = new ApiError
                {
                    ErrorCode = 500,
                    ErrorMessage = apiResponse.Exception
                };
            }
        }

        /// <summary>
        /// Adds the user transaction.
        /// </summary>
        /// <param name="flightBooking">The flight booking.</param>
        /// <param name="rownumber">The rownumber.</param>
        /// <returns>
        /// Insert User Transcation
        /// </returns>
        private async Task<List<FlightBookingModel>> AddUserTransaction(FlightBook flightBooking, long rownumber)
        {
            var userid = await this.userDetailService.GetUserIdAsync(this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email).Value);
            ////var userTransaction = new UserTransactionModel()
            ////{
            ////    UserId = userid,
            ////    HotelBookingId = null,
            ////    Currency = string.Empty,
            ////    ReceiptNo = string.Empty,
            ////    Amount = flightBooking.TotalAmount,
            ////    OrderId = string.Empty,
            ////    PaymentId = string.Empty,
            ////    Description = "Flight Booking",
            ////    Fee = flightBooking.TotalBaseFareAmount,
            ////    Tax = flightBooking.TotalBaseFareTaxAmount,
            ////    CreatedBy = userid.ToString(),
            ////    CreatedDate = DateTime.UtcNow
            ////};
            var flightBookingModels = new List<FlightBookingModel>();
            for (int i = 0; i < flightBooking.AirSearchResults.Count; i++)
            {
                flightBookingModels.Add(new FlightBookingModel
                {
                    UserId = userid,
                    UserTransactionId = rownumber,
                    CreatedBy = new Guid(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value),
                    CreatedDate = DateTime.UtcNow
                });
            }

            var records = await this.flightBookingService.InsertAsync(flightBookingModels);

            return records.ToList();
        }

        private async Task UpdateBookedFlight(FlightBookResult flightBookResult, List<FlightBookingModel> flightBooked, int i)
        {
            if (flightBookResult.Status && flightBookResult.Booking != null && flightBooked[i] != null)
            {
                flightBooked[i].Pnr = flightBookResult.Booking.Pnr;
                flightBooked[i].TboBookingId = flightBookResult.Booking.BookingId;
                flightBooked[i].Origin = flightBookResult.Booking.Origin;
                flightBooked[i].Destination = flightBookResult.Booking.Destination;
                flightBooked[i].BookingDate = DateTime.UtcNow;
                flightBooked[i].Response = flightBookResult.Json;
                flightBooked[i].Error = JsonConvert.SerializeObject(flightBookResult.Error, Formatting.Indented, this.JsonIgnoreNullable);
                await this.flightBookingService.UpdateAsync(flightBooked[i]);
            }
        }

        /// <summary>
        /// Sends the SMS.
        /// </summary>
        /// <returns>void</returns>
        private async Task SendSms()
        {
            try
            {
                var webClient = new WebClient();
                string to, message;
                to = Constants.SmsTo;
                message = Constants.SmsMessage;
                string baseURL = "http://sms.hspsms.com/sendSMS?username=HiTours&apikey=b97b2163-becd-4f3f-8500-a1146fb9e488&sendername=HITOUR&smstype=TRANS&numbers=" + to + "&message=" + message;
                await webClient.OpenReadTaskAsync(baseURL);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Sends the booking mail.
        /// </summary>
        /// <param name="hotelBookingId">The hotel booking identifier.</param>
        /// <returns>
        /// void
        /// </returns>
        private async Task SendBookingMail(Guid hotelBookingId)
        {
            try
            {
                var model = await this.hotelBookingService.GetTourBookingSendMailDetailAsync(hotelBookingId);
                model.SiteUrl = this.domainSetting.WebSiteUrl;

                ////Customer Mail
                model.IsContactDetail = false;
                var result = await this.viewRender.RenderToStringAsync("MailTemplate/_Booking", model);
                var subject = Constants.BookingMailSubject;
                SendMail.MailSend(subject, this.Content(result).Content, model.Email.Trim());
                if (this.domainSetting.RazorpayLive)
                {
                    ////Admin Mail
                    model.IsContactDetail = true;
                    var result2 = await this.viewRender.RenderToStringAsync("MailTemplate/_Booking", model);
                    SendMail.MailSend(subject, this.Content(result2).Content, Constants.AdminEmailId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Sends the booking mail.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// void
        /// </returns>
        private async Task SendFlightBookingMail(FlightBookResult model)
        {
            try
            {
                ////Customer Mail
                model.Json = this.domainSetting.WebSiteUrl;
                var claim = this.User.Claims.FirstOrDefault(x => x.Type == System.Security.Claims.ClaimTypes.Email);
                var emailId = claim != null ? claim.Value : string.Empty;

                if (!string.IsNullOrEmpty(emailId))
                {
                    var result = await this.viewRender.RenderToStringAsync("MailTemplate/_FlightBooking", model);
                    var subject = Constants.FlightBookingMailSubject;
                    SendMail.MailSend(subject, this.Content(result).Content, emailId.Trim());
                    if (this.domainSetting.RazorpayLive)
                    {
                        ////Admin Mail
                        var result2 = await this.viewRender.RenderToStringAsync("MailTemplate/_FlightBooking", model);
                        SendMail.MailSend(subject, this.Content(result2).Content, Constants.AdminEmailId);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void GetKeys(out string key, out string secret)
        {
            if (this.domainSetting.RazorpayLive)
            {
                key = Constants.LiveRazorPayApiKey;
                secret = Constants.LiveRazorPaySecretKey;
            }
            else
            {
                key = Constants.TestRazorPayApiKey;
                secret = Constants.TestRazorPaySecretKey;
            }
        }

        private string GetPersonType(string personType)
        {
            string paxType = "1";
            if (personType.ToLower() == Enums.PersonType.Adult.ToString().ToLower())
            {
                paxType = ((int)PaxType.Adult).ToString();
            }
            else if (personType.ToLower() == Enums.PersonType.Child.ToString().ToLower())
            {
                paxType = ((int)PaxType.Child).ToString();
            }
            else if (personType.ToLower() == Enums.PersonType.Infant.ToString().ToLower())
            {
                paxType = ((int)PaxType.Infant).ToString();
            }

            return paxType;
        }

        private async Task UpdatePersonDetails(int adultCount, Passengers person, List<ViewModels.HotelBookingPersonDetailViewModel> persons)
        {
            person.FirstName = persons[adultCount].FirstName;
            person.LastName = persons[adultCount].LastName;

            person.Title = persons[adultCount].Salutation;
            person.FirstName = persons[adultCount].FirstName;
            person.LastName = persons[adultCount].LastName;
            person.PaxType = this.GetPersonType(persons[adultCount].PersonType);
            person.Gender = persons[adultCount].Gender;
            person.DateOfBirth = persons[adultCount].DateOfBirth;
            ////person.PassportNo = persons[adultCount].P
            person.Email = persons[adultCount].Email;
            person.ContactNo = persons[adultCount].Mobile;
            person.AddressLine1 = persons[adultCount].BillingAddress;
            person.City = persons[adultCount].CityCode;
            person.CountryCode = persons[adultCount].CountryCode;
            person.CountryName = persons[adultCount].Country;
            var country = await this.masterService.GetPackageCountryByCodeAsync(persons[adultCount].CountryCode);
            if (country != null)
            {
                person.Countries = new List<SelectListItem> { new SelectListItem { Value = country.SortName, Text = country.Name, Selected = true } };
            }

            var city = await this.masterService.GetPackageCityByCodeAsync(persons[adultCount].CityCode);
            if (city != null)
            {
                person.Cities = new List<SelectListItem> { new SelectListItem { Value = city.Code, Text = city.Name, Selected = true } };
            }
        }

        private async Task GetSegments(Segments segment, string departCityCode, string hotelCityCode)
        {
            segment = segment ?? new Segments();

            var origin = await this.masterService.GetFlightDestinationByCityCode(departCityCode);
            if (origin != null)
            {
                segment.Origin = origin.CityCode;
                segment.DestinationCityName = origin.CityName;
                segment.DisplayOrigin = origin.SearchIn;
                segment.OriginList = new List<SelectListItem> { new SelectListItem { Text = origin.SearchIn, Value = origin.CityCode, Selected = true } };
            }

            var destination = await this.masterService.GetFlightDestinationByCityCode(hotelCityCode);
            if (destination != null)
            {
                segment.Destination = destination.CityCode;
                segment.DestinationCityName = destination.CityName;
                segment.DisplayDestination = destination.SearchIn;
                segment.DestinationList = new List<SelectListItem> { new SelectListItem { Text = destination.SearchIn, Value = destination.CityCode, Selected = true } };
            }
        }
    }
}