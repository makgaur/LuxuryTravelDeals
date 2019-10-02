// <copyright file="AirServiceController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Areas.TBO.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HiTours.TBO.Models;
    using HiTours.Web.Framework;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    /// <summary>
    /// AirServiceController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class AirServiceController : TBOController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AirServiceController"/> class.
        /// </summary>
        /// <param name="domainSetting">The service urls.</param>
        public AirServiceController(IOptions<DomainSetting> domainSetting)
            : base(domainSetting)
        {
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <returns>Search</returns>
        public IActionResult Search()
        {
            var airserach = new AirSearch();
            airserach.JourneyType = (int)JourneyType.OneWay;
            airserach.Segments = new List<Segments>() { new Segments() }.ToArray();

            return this.View(airserach);
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>
        /// Search
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Search(AirSearch search)
        {
            var url = $"{this.serviceUrl}/AirService/SearchForRequest";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(search, Formatting.Indented, this.JsonIgnoreNullable));
            if (apiResponse.IsSuccess)
            {
                var apiResult = JsonConvert.DeserializeObject<AirSearchResponse>(apiResponse.Response);
                if (apiResult != null && apiResult.Response != null && apiResult.Response.ResponseStatus == (int)AuthenticateStatus.Successful)
                {
                    this.AddClipBoard(nameof(Passengers), new { AdultCount = search.AdultCount, ChildCount = search.ChildCount, InfantCount = search.InfantCount });
                    this.AddClipBoard(nameof(apiResult.Response.TraceId), apiResult.Response.TraceId);
                    var apiSearchResult = new List<AirSearchResult>();
                    if (apiResult.Response.Results.Length > 0 && apiResult.Response.Results.FirstOrDefault().Length > 0)
                    {
                        apiSearchResult.AddRange(apiResult.Response.Results.FirstOrDefault());
                    }

                    this.AddClipBoard(nameof(AirSearchResult), apiSearchResult);
                }
            }

            return this.View(search);
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <returns>Search</returns>
        public IActionResult PriceRbd()
        {
            return this.View(new AirServiceSearchPriceRBD());
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns>
        /// Search
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> PriceRbd(AirServiceSearchPriceRBD search)
        {
            if (search.AirSearchResult != null && search.AirSearchResult.Count() > 0)
            {
                foreach (var item in search.AirSearchResult)
                {
                    var segments = new List<AirResultSegments[]>();
                    segments.Add(item.CustomSegments);
                    item.Segments = segments.ToArray();
                }
            }

            var url = $"{this.serviceUrl}/AirService/AdvanceSearchPriceRbd";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(search, Formatting.Indented, this.JsonIgnoreNullable));
            return this.View(search);
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <returns>Search</returns>
        public IActionResult CalendarFare()
        {
            return this.View(new CalendarFare() { Segments = new List<Segments>() { new Segments() }.ToArray() });
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <param name="calendarfare">The search.</param>
        /// <returns>
        /// Search
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CalendarFare(CalendarFare calendarfare)
        {
            var url = $"{this.serviceUrl}/AirService/GetCalendarFare";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(calendarfare, Formatting.Indented, this.JsonIgnoreNullable));
            return this.View(calendarfare);
        }

        /// <summary>
        /// Updates the calendar fare.
        /// </summary>
        /// <returns>UpdateCalendarFare</returns>
        public IActionResult UpdateCalendarFare()
        {
            this.ViewBag.Title = "Update Calendar Fare";
            return this.View("CalendarFare", new CalendarFare() { Segments = new List<Segments>() { new Segments() }.ToArray() });
        }

        /// <summary>
        /// Updates the calendar fare.
        /// </summary>
        /// <param name="calendarfare">The calendarfare.</param>
        /// <returns>UpdateCalendarFare</returns>
        [HttpPost]
        public async Task<IActionResult> UpdateCalendarFare(CalendarFare calendarfare)
        {
            var url = $"{this.serviceUrl}/AirService/UpdateCalendarFareOfDay";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(calendarfare, Formatting.Indented, this.JsonIgnoreNullable));
            this.ViewBag.Title = "Update Calendar Fare";
            return this.View("CalendarFare", calendarfare);
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <returns>Search</returns>
        public IActionResult FareRule()
        {
            return this.View(new FareRuleRequest());
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <param name="calendarfare">The search.</param>
        /// <returns>
        /// Search
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> FareRule(FareRuleRequest calendarfare)
        {
            var url = $"{this.serviceUrl}/AirService/GetFareRule";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(calendarfare));
            return this.View(calendarfare);
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <returns>Search</returns>
        public IActionResult FareQuote()
        {
            return this.View(new FareRuleRequest());
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <param name="calendarfare">The search.</param>
        /// <returns>
        /// Search
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> FareQuote(FareRuleRequest calendarfare)
        {
            var url = $"{this.serviceUrl}/AirService/GetFareQuote";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(calendarfare));
            if (apiResponse.IsSuccess)
            {
                var fareQuoteResult = JsonConvert.DeserializeObject<FareQuoteResponse>(apiResponse.Response);
                if (fareQuoteResult != null && fareQuoteResult.Response != null && fareQuoteResult.Response.ResponseStatus == (int)AuthenticateStatus.Successful)
                {
                    var searchResultIndex = new List<AirSearchResult>();
                    if (fareQuoteResult.Response.Results != null)
                    {
                        searchResultIndex.Add(fareQuoteResult.Response.Results);
                    }

                    this.AddClipBoard($"{nameof(HiTours.TBO.Models.FareRule)}Quote", searchResultIndex);
                }
            }

            return this.View(calendarfare);
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <returns>Search</returns>
        public IActionResult Ssr()
        {
            return this.View(new FareRuleRequest());
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <param name="fareRule">The search.</param>
        /// <returns>
        /// Search
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Ssr(FareRuleRequest fareRule)
        {
            var url = $"{this.serviceUrl}/AirService/GetSsr";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(fareRule));
            if (apiResponse.IsSuccess)
            {
                var ssrApiResult = JsonConvert.DeserializeObject<dynamic>(apiResponse.Response);

                dynamic ssrResult = new
                {
                    ////IsLCC = fareRule.IsLCC,
                    ResultIndex = fareRule.ResultIndex,
                    TraceId = fareRule.TraceId,
                    Baggage = new List<dynamic>(),
                    MealDynamic = new List<dynamic>(),
                    SeatDynamic = new List<dynamic>()
                };

                this.AddClipBoard($"SSRResponse", ssrResult);
            }

            return this.View(fareRule);
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <returns>Search</returns>
        public IActionResult Book()
        {
            return this.View(new Book());
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <param name="book">The search.</param>
        /// <returns>
        /// Search
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Book(Book book)
        {
            var url = $"{this.serviceUrl}/AirService/Book";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(book, this.JsonIgnoreNullable));
            return this.View(book);
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <returns>Search</returns>
        public IActionResult BookDetails()
        {
            return this.View(new BookingDetails());
        }

        /// <summary>
        /// Searches this instance.
        /// </summary>
        /// <param name="bookingdetails">The search.</param>
        /// <returns>
        /// Search
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> BookDetails(BookingDetails bookingdetails)
        {
            var url = $"{this.serviceUrl}/AirService/GetBookingDetails";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(bookingdetails));
            return this.View(bookingdetails);
        }

        /// <summary>
        /// Tickets this instance.
        /// </summary>
        /// <returns>Booking Detail</returns>
        public IActionResult TicketNonLCC()
        {
            return this.View(new Ticket());
        }

        /// <summary>
        /// Tickets the specified ticket model.
        /// </summary>
        /// <param name="ticketModel">The ticket model.</param>
        /// <returns>
        /// The ticket Result
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> TicketNonLCC(Ticket ticketModel)
        {
            var url = $"{this.serviceUrl}/AirService/Ticket";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(ticketModel));
            return this.View(ticketModel);
        }

        /// <summary>
        /// Tickets this instance.
        /// </summary>
        /// <returns>
        /// Booking Detail
        /// </returns>
        public IActionResult TicketLCC()
        {
            return this.View(new Ticket());
        }

        /// <summary>
        /// Tickets the specified ticket model.
        /// </summary>
        /// <param name="ticketModel">The ticket model.</param>
        /// <returns>
        /// The ticket Result
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> TicketLCC(Ticket ticketModel)
        {
            ticketModel.LCCRequest = true;
            var url = $"{this.serviceUrl}/AirService/Ticket";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(ticketModel, this.JsonIgnoreNullable));
            return this.View(ticketModel);
        }

        /// <summary>
        /// Tickets this instance.
        /// </summary>
        /// <returns>
        /// Booking Detail
        /// </returns>
        public IActionResult CancelPnr()
        {
            return this.View(new CancelPnr());
        }

        /// <summary>
        /// Tickets the specified ticket model.
        /// </summary>
        /// <param name="cancelpnr">The ticket model.</param>
        /// <returns>
        /// The ticket Result
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> CancelPnr(CancelPnr cancelpnr)
        {
            var url = $"{this.serviceUrl}/AirService/CancelPnrRequest";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(cancelpnr));
            return this.View(cancelpnr);
        }

        /// <summary>
        /// Tickets this instance.
        /// </summary>
        /// <returns>
        /// Booking Detail
        /// </returns>
        public IActionResult SendChange()
        {
            return this.View(new SendChange());
        }

        /// <summary>
        /// Tickets the specified ticket model.
        /// </summary>
        /// <param name="sendchange">The ticket model.</param>
        /// <returns>
        /// The ticket Result
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> SendChange(SendChange sendchange)
        {
            var url = $"{this.serviceUrl}/AirService/SendChangeRequest";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(sendchange));
            return this.View(sendchange);
        }

        /// <summary>
        /// Tickets this instance.
        /// </summary>
        /// <returns>
        /// Booking Detail
        /// </returns>
        public IActionResult ChangeRequestStatus()
        {
            return this.View(new ChangeRequestStatus());
        }

        /// <summary>
        /// Tickets the specified ticket model.
        /// </summary>
        /// <param name="changerequeststatus">The ticket model.</param>
        /// <returns>
        /// The ticket Result
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> ChangeRequestStatus(ChangeRequestStatus changerequeststatus)
        {
            var url = $"{this.serviceUrl}/AirService/GetChangeRequestStatus";
            var apiResponse = await this.PostAsync(url, JsonConvert.SerializeObject(changerequeststatus));
            return this.View(changerequeststatus);
        }

        /// <summary>
        /// Gets the new passengers row.
        /// </summary>
        /// <param name="jsonResult">The json result.</param>
        /// <param name="fareQuote">The fare quote.</param>
        /// <param name="adults">The adults.</param>
        /// <param name="childs">The childs.</param>
        /// <param name="infants">The infants.</param>
        /// <returns>
        /// GetNewPassengersRow
        /// </returns>
        public IActionResult GetLLCPassengers(string jsonResult, string fareQuote, int adults, int childs, int infants)
        {
            var passengers = new List<Passengers>();
            if (!string.IsNullOrEmpty(jsonResult))
            {
                var searchResult = JsonConvert.DeserializeObject<List<AirSearchResult>>(jsonResult);
                if (searchResult != null && searchResult.Count > 0)
                {
                    var result = searchResult.FirstOrDefault();
                    var fareQuoteResult = fareQuote != null ? JsonConvert.DeserializeObject<List<AirSearchResult>>(fareQuote) : null;

                    for (int adultCount = 0; adultCount < adults; adultCount++)
                    {
                        var fareBreakDown = fareQuoteResult != null && fareQuoteResult.Count() > 0 ? fareQuoteResult.FirstOrDefault().FareBreakdown.Where(x => x.PassengerType == (int)PaxType.Adult).ToArray() : null;

                        passengers.Add(new Passengers()
                        {
                            IsLeadPax = true,
                            PaxType = ((int)PaxType.Adult).ToString(),
                            FareBreakdown = fareBreakDown,
                            ////Fare = result.Fare
                            Fare = fareBreakDown.FirstOrDefault()
                        });
                    }

                    for (int childCount = 0; childCount < childs; childCount++)
                    {
                        var fareBreakDown = fareQuoteResult != null && fareQuoteResult.Count() > 0 ? fareQuoteResult.FirstOrDefault().FareBreakdown.Where(x => x.PassengerType == (int)PaxType.Child).ToArray() : null;
                        passengers.Add(new Passengers()
                        {
                            IsLeadPax = true,
                            PaxType = ((int)PaxType.Child).ToString(),
                            FareBreakdown = fareBreakDown, ////result.FareBreakdown.Where(x => x.PassengerType == (int)PaxType.Child).ToArray(),
                            ////Fare = result.Fare
                            Fare = fareBreakDown.FirstOrDefault()
                        });
                    }

                    for (int infantCount = 0; infantCount < infants; infantCount++)
                    {
                        var fareBreakDown = fareQuoteResult != null && fareQuoteResult.Count() > 0 ? fareQuoteResult.FirstOrDefault().FareBreakdown.Where(x => x.PassengerType == (int)PaxType.Infant).ToArray() : null;
                        passengers.Add(new Passengers()
                        {
                            IsLeadPax = true,
                            PaxType = ((int)PaxType.Infant).ToString(),
                            FareBreakdown = fareBreakDown, ////result.FareBreakdown.Where(x => x.PassengerType == (int)PaxType.Infant).ToArray(),
                            ////Fare = result.Fare
                            Fare = fareBreakDown.FirstOrDefault()
                        });
                    }
                }
            }

            return this.PartialView("_LLCPassengers", passengers);
        }

        /// <summary>
        /// Adds the new fare row.
        /// </summary>
        /// <returns>AddNewFareRow</returns>
        public IActionResult AddNewFareRow()
        {
            return this.PartialView("_Fares", new Fare());
        }

        /// <summary>
        /// Adds the new meal row.
        /// </summary>
        /// <returns>Meal</returns>
        public IActionResult AddNewMealRow()
        {
            return this.PartialView("_Meal", new Meal());
        }

        /// <summary>
        /// Adds the new seat row.
        /// </summary>
        /// <returns>Seat</returns>
        public IActionResult AddNewSeatRow()
        {
            return this.PartialView("_Seat", new Seat());
        }
    }
}