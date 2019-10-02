// <copyright file="UserController.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using AutoMapper;
    using HiTours.Core;
    using HiTours.Models;
    using HiTours.Services;
    using HiTours.TBO.Models;
    using HiTours.ViewModels;
    using HiTours.Web.Framework;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using ApiResponse = TBO.Models.ApiResponse;

    /// <summary>
    /// User Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class UserController : BaseController
    {
        /// <summary>
        /// The domain setting
        /// </summary>
        private readonly DomainSetting domainSetting;

        /// <summary>
        /// The hosting environment
        /// </summary>
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// The user detail service
        /// </summary>
        private readonly IUserDetailService userDetailService;
        private readonly IConfiguration configuration;
        private readonly TBOController tboController;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController" /> class.
        /// </summary>
        /// <param name="stateService">State Service</param>
        /// <param name="homePageService">Home Page Service</param>
        /// <param name="tboService">TBO Service</param>
        /// <param name="configuration">Web Config</param>
        /// <param name="countryService">Country Service</param>
        /// <param name="cityService">City Service</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="userDetailService">The user detail service.</param>
        /// <param name="hostingEnvironment">The hosting environment.</param>
        /// <param name="domainSetting">The domain setting.</param>
        public UserController(IStateService stateService, IHomePageService homePageService, ITBOService tboService, IConfiguration configuration, ICountryService countryService, ICityService cityService, IMapper mapper, IUserDetailService userDetailService, IHostingEnvironment hostingEnvironment, IOptions<DomainSetting> domainSetting)
            : base(mapper, homePageService, cityService, countryService, configuration, stateService)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            this.userDetailService = userDetailService;
            this.domainSetting = domainSetting.Value;
            this.hostingEnvironment = hostingEnvironment;
            this.tboController = new TBOController(tboService, configuration, hostingEnvironment);
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// login and signup view
        /// </returns>
        public IActionResult Index(string returnUrl = "")
        {
            if (this.HttpContext.User.Identity.IsAuthenticated && (this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Actor) == null || this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Actor).Value == string.Empty))
            {
                var roleType = this.User.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role));
                if (roleType != null)
                {
                    if (roleType.Value == Enums.RoleType.User.ToString())
                    {
                        return this.RedirectToRoute(Constants.RouteDefault, new { controller = "Home" });
                    }
                }
            }

            this.ViewBag.ReturnUrl = returnUrl;
            return this.View();
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="model">The return URL.</param>
        /// <returns>
        /// returns Json Result
        /// </returns>
        [Route("user/sendOTP")]
        public async Task<JsonResult> SendOTP(LoginViewModel model)
        {
            var userRecord = await this.userDetailService.CheckMobile(model.MobileNo, model.Mode);
            if (userRecord == null && model.Mode == "signin")
            {
                var result = new
                {
                    Status = false,
                    Key = "Invalid",
                    Message = Messages.InvalidMobile
                };
                return this.Json(result);
            }
            else if (model.Mode == "signin" && userRecord != null && !userRecord.IsActive)
            {
                var result = new
                {
                    Status = false,
                    Key = "InActive",
                    Message = Messages.InActive
                };
                return this.Json(result);
            }
            else
            {
                // await this.SetClaims(userRecord);
                Random random = new Random();
                int otp = random.Next(10000, 99999);
                var msg = "Your one time password for your Luxury Travel Deals account is " + otp + ".Find you happy holiday with Luxury Travel Deals now!";
                await this.SendSms(model.MobileNo.ToString(), msg);
                if (userRecord != null)
                {
                    await this.userDetailService.InsertOTP(userRecord.Id, otp);
                }

                var result = new
                {
                    Status = true,
                    Message = Messages.OtpSent
                };
                return this.Json(result);
            }
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns>Logout User Async</returns>
        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync();
            return this.RedirectToRoute(Constants.RouteDefault, new { controller = "Home" });
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <param name="bookingId">The Booking ID.</param>
        /// <returns>Logout User Async</returns>
        public async Task<IActionResult> MyBookings(int? bookingId)
        {
            if (bookingId.HasValue)
            {
                MyBookingDescriptionViewModel model = await this.userDetailService.GetMyBookingDescriptionByBookingId(bookingId.Value);
                if (model.bookingFlightViewModel != null && model.bookingFlightViewModel.Count > 0)
                {
                    model.ticketsViewModel = new List<TicketLCCResponse>();
                    foreach (var item in model.bookingFlightViewModel)
                    {
                        Ticket ticketRequest = new Ticket
                        {
                            EndUserIp = this.configuration.GetValue<string>("TBOCredentials:EndUserIp"),
                            BookingId = Convert.ToInt64(item.TBOBookingId),
                            PNR = item.PNR,
                            TraceId = item.TraceId,
                            TokenId = await this.tboController.GetTBOLoginToken()
                        };
                        ApiResponse response = this.tboController.PostCustom(
                            this.tboController.GetTboUrl(TboMethods.BookingDetails),
                            JsonConvert.SerializeObject(ticketRequest, this.tboController.JsonIgnoreNullable),
                            TboMethods.BookingDetails);
                        if (response.IsSuccess)
                        {
                            TicketLCCResponse ticketLCCResponse = JsonConvert.DeserializeObject<TicketLCCResponse>(response.Response);
                            model.ticketsViewModel.Add(ticketLCCResponse);
                        }
                    }

                    model.ticketsViewModel = model.ticketsViewModel.OrderBy(x => DateTime.ParseExact(x.Response.FlightItinerary.Segments.FirstOrDefault().Origin.DepTime, "yyyy-MM-ddTHH:mm:ss", null)).ToList();
                }

                return this.View("MyBookingDetail", model);
            }
            else
            {
                var claimInformation = this.HttpContext.User.Claims.FirstOrDefault();
                if (claimInformation != null)
                {
                    var userId = Convert.ToInt32(claimInformation.Value);
                    List<MyBookingsListViewModel> myBookings = await this.userDetailService.GetDealBookingsByUserId(userId);
                    return this.View(myBookings);
                }

                return this.View(new List<MyBookingsListViewModel>());
            }
        }

        /// <summary>
        /// Registers the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="registrationType">Type of the registration.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// Regiseter User Async
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserDetailViewModel model, string registrationType, string returnUrl = null)
        {
            if (this.ModelState.IsValid)
            {
                ////var isAlreadyGuest = false;
                var isNotGuest = string.IsNullOrEmpty(registrationType);
                var record = this.mapper.Map<UserDetailModel>(model);
                UserDetailModel userRecord = null, mobileuserRecord = null;
                if (model.Id == 0)
                {
                    record.CreatedDate = DateTime.Now;
                    record.UpdatedDate = DateTime.Now;
                    record.IsActive = false;
                    record.IsGuest = false;
                    if (!isNotGuest)
                    {
                        record.IsActive = false;
                        record.IsGuest = true;
                    }

                    if (model.EmailId != null && model.EmailId != string.Empty)
                    {
                        userRecord = await this.userDetailService.LoginAsync(model.EmailId, string.Empty, "register", false);
                    }

                    if (model.MobileNo != null && model.MobileNo != string.Empty)
                    {
                        mobileuserRecord = await this.userDetailService.LoginOTPAsync(model.MobileNo, string.Empty, false);
                    }

                    if (userRecord == null && mobileuserRecord == null)
                    {
                        try
                        {
                            userRecord = new UserDetailModel();
                            userRecord.Id = await this.userDetailService.InsertAsync(record);
                            userRecord = await this.userDetailService.GetByIdAsync(userRecord.Id);

                            string company = "Hi Tours";
                            string json = "{\"data\":[{" +
                                    "\"Company\": \"" + company + "\"," +
                                    "\"Last_Name\": \"" + userRecord.LastName + "\"," +
                                    "\"First_Name\": \"" + userRecord.FirstName + "\"," +
                                    "\"Email\": \"" + userRecord.EmailId + "\"," +
                                    "\"Phone\": \"" + userRecord.MobileNo + "\"," +
                                    "}]," +
                                    "\"trigger\": [" +
                                    "\"approval\"," +
                                    "\"workflow\"," +
                                    "\"blueprint\"" +
                                    "]}";

                            var resultZoho = Services.Zoho.ZohoUpdate.GetApiResponse(json, "https://www.zohoapis.com/crm/v2/Leads");
                        }
                        catch (Exception ex)
                        {
                            var msg = ex.ToString();
                        }

                        if (!model.Redirection)
                        {
                            await this.SetClaims(userRecord);
                        }

                        if (userRecord == null)
                        {
                            return this.Json(new { Status = false, Message = "Registration Failed" });
                        }

                        return this.Json(new { Status = true, Message = Messages.RegisterSuccessfull, Redirect = model.Redirection, Id = userRecord.Id });
                    }
                    else if (userRecord != null)
                    {
                        return this.Json(new { Status = false, Message = "Email Id already Exists" });
                    }
                    else
                    {
                        return this.Json(new { Status = false, Message = "Mobile already Exists" });
                    }

                    ////if (isNotGuest)
                    ////{
                    ////    var htmlBody = string.Empty;
                    ////    var filePath = Path.Combine(this.hostingEnvironment.WebRootPath + "/Templates/", "register.html");
                    ////    var currentUrl = this.HttpContext.Request.Host.ToString().ToLower();

                    ////    using (StreamReader sourceReader = System.IO.File.OpenText(filePath))
                    ////    {
                    ////        htmlBody = await sourceReader.ReadToEndAsync();
                    ////        htmlBody = htmlBody.Replace("##HOSTURL##", this.domainSetting.WebSiteUrl);
                    ////        htmlBody = htmlBody.Replace("##User##", record.FirstName);
                    ////        htmlBody = htmlBody.Replace("##href##", "http://" + currentUrl + "/user/ActivateUser?user=" + this.Base64Encode(record.Id.ToString()));
                    ////    }

                    ////    var subject = "Sign-up verification link from Luxury Travel Deals";

                    ////    SendMail.MailSend(subject, htmlBody, record.EmailId.Trim());
                    ////    return this.Json(new { Status = true, Message = Messages.RegisterSuccessfull });
                    ////}
                    ////else
                    ////{
                    ////    await this.SetClaims(userRecord, registrationType);

                    ////    return this.Json(new
                    ////    {
                    ////        Status = true,
                    ////        Message = Messages.LoginSuccessfully,
                    ////        RedirectUrl = returnUrl
                    ////    });
                    ////}
                }
            }

            return this.Json(new { Status = false, Message = "Invalid", Redirect = model.Redirection });
        }

        /// <summary>
        /// Activates the user.
        /// </summary>
        /// <param name="user">The id.</param>
        /// <returns>
        /// ActivateUser
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> ActivateUser(string user)
        {
            user = this.Base64Decode(user);
            if (user != string.Empty && user != null)
            {
                var record = await this.userDetailService.ActivateUser(Convert.ToInt32(user));
                if (record != null)
                {
                    var htmlBody = string.Empty;
                    var filePath = Path.Combine(this.hostingEnvironment.WebRootPath + "/Templates/", "ActivateAccount.html");

                    using (StreamReader sourceReader = System.IO.File.OpenText(filePath))
                    {
                        htmlBody = await sourceReader.ReadToEndAsync();
                        htmlBody = htmlBody.Replace("##HOSTURL##", this.domainSetting.WebSiteUrl);
                        htmlBody = htmlBody.Replace("##User##", record.FirstName);
                    }

                    var subject = "Activation status from Luxury Travel Deals";
                    SendMail.MailSend(subject, htmlBody, record.EmailId.Trim());
                    this.ShowMessage(Messages.AccountActivation);
                }
                else
                {
                    return this.RedirectToAction("index", "home");
                }
            }

            return this.View();
        }

        /// <summary>
        /// Duplicates the emailId.
        /// </summary>
        /// <param name="emailId">The emailId.</param>
        /// <returns>Get Duplicate emailId</returns>
        public async Task<JsonResult> IsDuplicate(string emailId = "")
        {
            return this.Json(await this.userDetailService.IsDuplicateAsync(emailId));
        }

        /// <summary>
        /// Duplicates the mobile.
        /// </summary>
        /// <param name="mobileNo">The emailId.</param>
        /// <returns>Get Duplicate emailId</returns>
        public async Task<JsonResult> IsDuplicateMobile(string mobileNo = "")
        {
            return this.Json(await this.userDetailService.IsDuplicateMobileAsync(mobileNo));
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Login User Async
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var userRecord = await this.userDetailService.LoginAsync(model.Email, model.Password, "login", true);
            if (userRecord == null)
            {
                var result = new
                {
                    Status = false,
                    Message = Messages.LoginFailed
                };
                return this.Json(result);
            }
            else
            {
                await this.SetClaims(userRecord);
                var result = new
                {
                    Status = true,
                    Message = Messages.LoginSuccessfully,
                    RedirectUrl = !string.IsNullOrEmpty(model.ReturnUrl) ? model.ReturnUrl : "/Home",
                    Redirect = model.Redirection,
                    Id = userRecord.Id
                };
                return this.Json(result);
            }
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// SignUp User Async
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUpOtp(UserDetailViewModel model)
        {
            var userRecord = await this.userDetailService.LoginOTPAsync(model.MobileNo, model.OTP, true);
            if (userRecord == null)
            {
                var record = this.mapper.Map<UserDetailModel>(model);
                await this.userDetailService.DeleteAsync(record);
                var result = new
                {
                    Status = false,
                    Message = Messages.InvalidOtp
                };
                return this.Json(result);
            }
            else
            {
                var userDeatils = await this.userDetailService.GetByIdAsync(userRecord.Id);
                if (userDeatils != null)
                {
                    userDeatils.IsActive = true;
                    var updateDeatils = await this.userDetailService.UpdateAsync(userDeatils);

                    string uri = "https://luxurytravel.deals/";
                    var msg = userDeatils.FirstName + ", welcome aboard to happiness! Your account has been created with Luxury Travel Deals. You can access your account/bookings in this link: " + Environment.NewLine + @uri;
                    await this.SendSms(userDeatils.MobileNo.ToString(), msg);

                    if (userDeatils.EmailId != null && userDeatils.EmailId != string.Empty)
                    {
                        var htmlBody = string.Empty;
                        var filePath = Path.Combine(this.hostingEnvironment.WebRootPath + "/Templates/", "Registration_emailer.html");
                        using (StreamReader sourceReader = System.IO.File.OpenText(filePath))
                        {
                            htmlBody = await sourceReader.ReadToEndAsync();
                            htmlBody = htmlBody.Replace("##HOSTURL##", this.domainSetting.WebSiteUrl);
                            htmlBody = htmlBody.Replace("##User##", userDeatils.FirstName);
                            htmlBody = htmlBody.Replace("##BlobUrl##", this.configuration.GetValue<string>("AzureBlobAppSetting:ImageInitializer") + "/");
                        }

                        var subject = "Account Creation - Luxury Travel Deals";
                        SendMail.MailSend(subject, htmlBody, userDeatils.EmailId.Trim());
                    }

                    var result = new
                    {
                        Status = true,
                        Message = Messages.RegisterSuccessfull,
                        Id = userRecord.Id
                    };

                    return this.Json(result);
                }
                else
                {
                    var result = new
                    {
                        Status = false
                    };

                    return this.Json(result);
                }
            }
        }

        /// <summary>
        /// Logins the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// Login User Async
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginOtp(LoginViewModel model)
        {
            var userRecord = await this.userDetailService.LoginOTPAsync(model.MobileNo, model.OTP, true);
            if (userRecord == null)
            {
                var result = new
                {
                    Status = false,
                    Message = Messages.InvalidOtp
                };
                return this.Json(result);
            }
            else
            {
                TimeSpan totalmins = (DateTime.Now - userRecord.OtpExpiryDate).Value;
                if (totalmins.TotalSeconds >= 120)
                {
                    var result = new
                    {
                        Status = false,
                        Message = Messages.OtpExpired
                    };
                    return this.Json(result);
                }
                else
                {
                    await this.SetClaims(userRecord);
                    var result = new
                    {
                        Status = true,
                        Message = Messages.LoginSuccessfully,
                        RedirectUrl = !string.IsNullOrEmpty(model.ReturnUrl) ? model.ReturnUrl : "/Home",
                        Redirect = model.Redirection,
                        Id = userRecord.Id
                    };
                    return this.Json(result);
                }
            }
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// Login With Facebook Account
        /// </returns>
        public IActionResult LoginWithFacebook(string returnUrl = "")
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = this.Url.Action("Facebook", "User", new { returnUrl = returnUrl })
            };

            return this.Challenge(authenticationProperties, Constants.Facebook);
        }

        /// <summary>
        /// Facebooks this instance.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// Signin Facebook
        /// </returns>
        ////[Route("user/signinfacebook")]
        public async Task<IActionResult> Facebook(string returnUrl = "")
        {
            await this.SocialLoginResponseAsync();

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return this.LocalRedirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction("index", "home");
            }
        }

        /// <summary>
        /// Signs the in with google.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// Login with google
        /// </returns>
        public IActionResult SignInWithGoogle(string returnUrl = "")
        {
            var authenticationProperties = new AuthenticationProperties
            {
                RedirectUri = this.Url.Action("Google", "User", new { returnUrl = returnUrl })
            };

            return this.Challenge(authenticationProperties, Constants.Google);
        }

        /// <summary>
        /// Googles this instance.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>
        /// sign in google
        /// </returns>
        ////[Route("signin-google")]
        public async Task<IActionResult> Google(string returnUrl = "")
        {
            await this.SocialLoginResponseAsync();

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return this.LocalRedirect(returnUrl);
            }
            else
            {
                return this.RedirectToAction("index", "home");
            }

            ////var authenticationProperties = new AuthenticationProperties
            ////{
            ////    RedirectUri = this.Url.Action("Index", "Home")
            ////};

            ////return this.Challenge(authenticationProperties, Constants.Google);
        }

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <returns>Forgot Password View</returns>
        public IActionResult ForgotPassword()
        {
            return this.View(new LoginViewModel());
        }

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="redirection">Redirection</param>
        /// <returns>Forgot Password View</returns>
        public IActionResult Welcome(string mode, bool redirection)
        {
            return this.PartialView("_Login", new LoginViewModel { Mode = mode, Redirection = redirection });
        }

        /// <summary>
        /// Forgots the password.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>Status</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(LoginViewModel model)
        {
            if (model.Email != string.Empty && model.Email != null)
            {
                var record = await this.userDetailService.GetPassword(model.Email);
                if (record != null)
                {
                    var htmlBody = string.Empty;
                    var filePath = Path.Combine(this.hostingEnvironment.WebRootPath + "/Templates/", "Forgotpassword.html");

                    using (StreamReader sourceReader = System.IO.File.OpenText(filePath))
                    {
                        htmlBody = await sourceReader.ReadToEndAsync();
                        htmlBody = htmlBody.Replace("##HOSTURL##", this.domainSetting.WebSiteUrl);
                        htmlBody = htmlBody.Replace("##password##", record.Password);
                        htmlBody = htmlBody.Replace("##User##", record.FirstName);
                    }

                    var subject = "Your Acount Login Password";
                    SendMail.MailSend(subject, htmlBody, record.EmailId.Trim());
                    this.ShowMessage(Messages.ForgotPassword, Enums.MessageType.Success);
                }
                else
                {
                    this.ShowMessage(Messages.InvalidUser, Enums.MessageType.Error);
                }
            }

            return this.RedirectToAction("index", "user");
        }

        private async Task SocialLoginResponseAsync()
        {
            if (this.User.Identity.IsAuthenticated && this.User.Identities.FirstOrDefault().AuthenticationType != Constants.ApplicationCookies)
            {
                var emailid = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
                var notDuplicate = await this.userDetailService.IsDuplicateAsync(emailid);

                if (notDuplicate)
                {
                    var userdetail = new UserDetailViewModel
                    {
                        DetailType = this.User.Identities.FirstOrDefault().AuthenticationType,
                        EmailId = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value,
                        FirstName = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value,
                        LastName = this.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname).Value
                    };

                    var record = this.mapper.Map<UserDetailModel>(userdetail);
                    if (userdetail.Id == 0)
                    {
                        record.IsActive = true;
                        record.SetAuditInfo(new Guid("1CFFB9E8-6592-422C-9B8D-2617E2F2F0FC"));
                        var user = await this.userDetailService.InsertAsync(record);
                        await this.SetClaims(record);

                        string company = "Hi Tours";
                        string json = "{\"data\":[{" +
                                "\"Company\": \"" + company + "\"," +
                                "\"Last_Name\": \"" + userdetail.LastName + "\"," +
                                "\"First_Name\": \"" + userdetail.FirstName + "\"," +
                                "\"Email\": \"" + userdetail.EmailId + "\"," +
                                "\"Phone\": \"\"," +
                                "}]," +
                                "\"trigger\": [" +
                                "\"approval\"," +
                                "\"workflow\"," +
                                "\"blueprint\"" +
                                "]}";

                        var resultZoho = Services.Zoho.ZohoUpdate.GetApiResponse(json, "https://www.zohoapis.com/crm/v2/Leads");
                    }
                }
                else
                {
                    var userRecord = await this.userDetailService.GetUserRecordByEmailId(emailid);
                    await this.SetClaims(userRecord);
                }
            }
        }

        /// <summary>
        /// Base64s the encode.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>string</returns>
        private string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Base64s the decode.
        /// </summary>
        /// <param name="base64EncodedData">The base64 encoded data.</param>
        /// <returns>strin</returns>
        private string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        private async Task SetClaims(UserDetailModel userRecord, string guest = "")
        {
            var claimIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            claimIdentity.AddClaim(new Claim(ClaimTypes.Sid, userRecord.Id.ToString()));
            claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userRecord.Id.ToString()));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Email, userRecord.EmailId == null ? string.Empty : userRecord.EmailId));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Role, Enums.RoleType.User.ToString()));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Name, (userRecord.FirstName + " " + userRecord.LastName).Trim()));
            claimIdentity.AddClaim(new Claim(ClaimTypes.Actor, guest));

            await this.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
        }
    }
}