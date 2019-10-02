// <copyright file="Constants.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Core
{
    /// <summary>
    /// Constants Fields
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// The combo pagination size
        /// </summary>
        public const int ComboPaginationSize = 10;

        /// <summary>
        /// The route admin
        /// </summary>
        public const string RouteArea = "area";

        /// <summary>
        /// The area admin
        /// </summary>
        public const string AreaAdmin = "admin";

        /// <summary>
        /// The area tbo
        /// </summary>
        public const string AreaTBO = "tbo";

        /// <summary>
        /// The route default
        /// </summary>
        public const string RouteDefault = "default";

        /// <summary>
        /// The application cookies
        /// </summary>
        public const string ApplicationCookies = "Cookies";

        /// <summary>
        /// The application admin cookies
        /// </summary>
        public const string ApplicationAdminCookies = "backend";

        /// <summary>
        /// The default challenge scheme
        /// </summary>
        public const string Facebook = "Facebook";

        /// <summary>
        /// The google
        /// </summary>
        public const string Google = "Google";

        /// <summary>
        /// The site locale
        /// </summary>
        public const string SiteLocale = "en-US";

        /// <summary>
        /// The maximum discount
        /// </summary>
        public const double MaxDiscount = 100;

        /// <summary>
        /// The maximum decimal range
        /// </summary>
        public const double MaxDecimalRange = 99999999.99;

        /// <summary>
        /// The maximum decimal range
        /// </summary>
        public const double Max14DecimalRange = 9999999999.99;

        /// <summary>
        /// The upcoming deals days
        /// </summary>
        public const int UpcomingDealsDays = 10;

        /// <summary>
        /// The admin role
        /// </summary>
        public const string AdminRole = "admin";

        /// <summary>
        /// The User role(Non Admin)
        /// </summary>
        public const string UserRole = "user";

        /// <summary>
        /// The sign in sign up
        /// </summary>
        public const string SignInSignUp = "SignIn / SignUp";

        /// <summary>
        /// The city count
        /// </summary>
        public const int CityCount = 1293;

        /// <summary>
        /// The path image not found
        /// </summary>
        public const string PathImageNotFound = "/images/not-found.jpg";

        /// <summary>
        /// The prefix package deal
        /// </summary>
        public const string PrefixPackageDeal = "D";

        /// <summary>
        /// The post fix package deal
        /// </summary>
        public const string PostFixPackageDeal = "";

        /// <summary>
        /// The base URL
        /// </summary>
        public const string BaseUrl = "http://api.tektravels.com/";

        /// <summary>
        /// The client identifier
        /// </summary>
        public const string ClientId = "ApiIntegrationNew";

        /// <summary>
        /// The public ip
        /// </summary>
        public const string PublicIP = "203.153.42.102";

        /// <summary>
        /// The API user name
        /// </summary>
        public const string ApiUserName = "HiTours";

        /// <summary>
        /// The API password
        /// </summary>
        public const string ApiPassword = "HiTours@123";

        /// <summary>
        /// The razor pay API key
        /// </summary>
        public const string TestRazorPayApiKey = "rzp_test_VYg2ez8nKVlFDN";

        /// <summary>
        /// The razor pay secret key
        /// </summary>
        public const string TestRazorPaySecretKey = "121KJKCgpYEqvPVP7oGaCPnl";

        /// <summary>
        /// The razor pay API live key
        /// </summary>
        public const string LiveRazorPayApiKey = "rzp_live_Zcm8oTF0sTNXHm";

        /// <summary>
        /// The razor pay secret live key
        /// </summary>
        public const string LiveRazorPaySecretKey = "731KxSmcVVuRw6HYRq6UsY34";

        /// <summary>
        /// The razor pay currency
        /// </summary>
        public const string RazorPayCurrency = "INR";

        /// <summary>
        /// The razor pay
        /// </summary>
        public const string RazorPay = "Luxury Travel Deals";

        /// <summary>
        /// The razor pay checkout js
        /// </summary>
        public const string RazorPayCheckoutJs = "https://checkout.razorpay.com/v1/checkout.js";

        /// <summary>
        /// The data image
        /// </summary>
        public const string RazorPayDataImage = "/images/paymentluxuryTravel.png";

        /// <summary>
        /// The razor pay data theme color
        /// </summary>
        public const string RazorPayDataThemeColor = "#660866";

        /// <summary>
        /// The GST in percent
        /// </summary>
        public const decimal GstInPercent = 5;

        /// <summary>
        /// The GST for hotels in percent
        /// </summary>
        public const decimal GstInPercentForHotel = 10;

        /// <summary>
        /// The payment gate way session time minute
        /// </summary>
        public const int PaymentGateWaySessionTimeMinute = 15;

        /// <summary>
        /// The default country
        /// </summary>
        public const int DefaultCountry = 61;

        /// <summary>
        /// The admin email identifier
        /// </summary>
        public const string AdminEmailId = "booking@hitours.in";

        /// <summary>
        /// The SMTP client
        /// </summary>
        public const string SmtpClient = "smtp.office365.com";

        /// <summary>
        /// The network credential email
        /// </summary>
        public const string NetworkCredentialEmail = "noreply@mailer.luxurytravel.deals";

        /// <summary>
        /// The network credential password
        /// </summary>
        public const string NetworkCredentialPwd = "Xur35024";

        /// <summary>
        /// The email from
        /// </summary>
        public const string EmailFrom = "noreply@mailer.luxurytravel.deals";

        /// <summary>
        /// The dynamic
        /// </summary>
        public const string Dynamic = "dynamic";

        /// <summary>
        /// The package
        /// </summary>
        public const string Package = "package";

        /// <summary>
        /// The deal
        /// </summary>
        public const string Deal = "deal";

        /// <summary>
        /// The request call back admin email
        /// </summary>
        public const string RequestCallBackAdminEmail = "hunar@hi.tours";

        /// <summary>
        /// The SMS to
        /// </summary>
        public const string SmsTo = "9999991717";

        /// <summary>
        /// The SMS message
        /// </summary>
        public const string SmsMessage = "Deal is booked";

        /// <summary>
        /// The booking mail subject
        /// </summary>
        public const string BookingMailSubject = "Deal Booking status from Luxury Travel Deals";

        /// <summary>
        /// The booking mail subject
        /// </summary>
        public const string FlightBookingMailSubject = "Flight Booking status from Luxury Travel Deals";

        /// <summary>
        /// The hotel creation
        /// </summary>
        public const string HotelCreation = "hotelcreation";

        /// <summary>
        /// The tour package creation
        /// </summary>
        public const string TourPackageCreation = "tourpackagecreation";
    }
}