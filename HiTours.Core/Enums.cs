// <copyright file="Enums.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Core
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Enums
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// MessageType
        /// </summary>
        public enum MessageType
        {
            /// <summary>
            /// The success
            /// </summary>
            Success,

            /// <summary>
            /// The error
            /// </summary>
            Error,

            /// <summary>
            /// The warning
            /// </summary>
            Warning
        }

        /// <summary>
        /// ViewComponent
        /// </summary>
        public enum ViewComponent
        {
            /// <summary>
            /// The header
            /// </summary>
            Header,

            /// <summary>
            /// The page header
            /// </summary>
            PageHeader,

            /// <summary>
            /// The left menu
            /// </summary>
            LeftMenu,

            /// <summary>
            /// The notification
            /// </summary>
            Notification,

            /// <summary>
            /// The footer
            /// </summary>
            Footer
        }

        /// <summary>
        /// View Component Menu
        /// </summary>
        public enum ViewComponentMenu
        {
            /// <summary>
            /// The style
            /// </summary>
            Style,

            /// <summary>
            /// The holiday
            /// </summary>
            Holiday,
        }

        /// <summary>
        /// RoleType
        /// </summary>
        public enum RoleType
        {
            /// <summary>
            /// The super admin
            /// </summary>
            SuperAdmin = 1,

            /// <summary>
            /// The admin
            /// </summary>
            Admin = 2,

            /// <summary>
            /// The user
            /// </summary>
            User = 3
        }

        /// <summary>
        /// UI Button Type
        /// </summary>
        public enum ButtonType
        {
            /// <summary>
            /// The save
            /// </summary>
            [Button(Text = "Save", Type = "submit", Class = "btn-primary")]
            Submit,

            /// <summary>
            /// The saveand new
            /// </summary>
            [Button(Text = "Save & New", Type = "submit", Class = "btn-primary")]
            SaveandNew,

            /// <summary>
            /// The saveand new
            /// </summary>
            [Button(Text = "Save", Type = "submit", Class = "btn-primary btnSaveAndSame")]
            SaveandReload,

            /// <summary>
            /// The save
            /// </summary>
            [Button(Text = "Save and Next", Type = "submit", Class = "btn-primary btnSaveAndSame")]
            SubmitAndNext,

            /// <summary>
            /// The save
            /// </summary>
            [Button(Text = "Save and Close", Type = "submit", Class = "btn-primary btnSaveAndSame")]
            SubmitAndClose,

            /// <summary>
            /// The view
            /// </summary>
            [Button(Text = "View List", Icon = "fa fa-list")]
            ViewList,

            /// <summary>
            /// The cancel
            /// </summary>
            [Button(Text = "Cancel", Class = "btn-default")]
            Cancel,

            /// <summary>
            /// The reload
            /// </summary>
            [Button(Text = "Reload")]
            Reload,

            /// <summary>
            /// The add new
            /// </summary>
            [Button(Text = "Add New", Icon = "fa fa-plus")]
            AddNew
        }

        /// <summary>
        /// Gender
        /// </summary>
        public enum Gender
        {
            /// <summary>
            /// The male
            /// </summary>
            Male,

            /// <summary>
            /// The female
            /// </summary>
            Female,

            /// <summary>
            /// The other
            /// </summary>
            Other
        }

        /// <summary>
        /// LoginType
        /// </summary>
        public enum LoginType
        {
            /// <summary>
            /// The default
            /// </summary>
            Default,

            /// <summary>
            /// The facebook
            /// </summary>
            Facebook,

            /// <summary>
            /// The google
            /// </summary>
            Google
        }

        /// <summary>
        /// Salutation
        /// </summary>
        public enum Salutation
        {
            /// <summary>
            /// The mr
            /// </summary>
            Mr,

            /// <summary>
            /// The MRS
            /// </summary>
            Mrs
        }

        /// <summary>
        /// PersonType
        /// </summary>
        public enum PersonType
        {
            /// <summary>
            /// The adult
            /// </summary>
            Adult = 1,

            /// <summary>
            /// The child
            /// </summary>
            Child = 2,

            /// <summary>
            /// The infant
            /// </summary>
            Infant = 3
        }

        /// <summary>
        /// Flexible Date
        /// </summary>
        public enum FlexibleDate
        {
            /// <summary>
            /// The exact date
            /// </summary>
            [Display(Name = "Exact date")]
            ExactDate = 0,

            /// <summary>
            /// The flexible
            /// </summary>
            [Display(Name = "Flexible ± 1 days")]
            Flexible1Days = 1,

            /// <summary>
            /// The flexible3 days
            /// </summary>
            [Display(Name = "Flexible ± 3 days")]
            Flexible3Days = 3,

            /// <summary>
            /// The flexible7 days
            /// </summary>
            [Display(Name = "Flexible ± 7 days")]
            Flexible7Days = 7,

            /// <summary>
            /// The flexible14 days
            /// </summary>
            [Display(Name = "Flexible ± 14 days")]
            Flexible14Days = 14,
        }

        /// <summary>
        /// Seo Page Type
        /// </summary>
        public enum SeoPageType
        {
            /// <summary>
            /// The static
            /// </summary>
            [DisplayName("Static Page")]
            Static,

            /// <summary>
            /// The package
            /// </summary>
            [DisplayName("Package")]
            Package,

            /// <summary>
            /// The hotel
            /// </summary>
            [DisplayName("Hotel")]
            Hotel,

            /// <summary>
            /// The style
            /// </summary>
            [DisplayName("Style")]
            Style,

            /// <summary>
            /// The holiday
            /// </summary>
            [DisplayName("Holiday")]
            Holiday
        }

        /// <summary>
        /// RazorPayStatus
        /// </summary>
        public enum RazorPayStatus
        {
            /// <summary>
            /// The process
            /// </summary>
            Process = 1,

            /// <summary>
            /// The done
            /// </summary>
            Done = 2,

            /// <summary>
            /// The failed
            /// </summary>
            Failed = 3
        }

        /// <summary>
        /// Current Deal Type
        /// </summary>
        public enum CurrentDealType
        {
            /// <summary>
            /// The package
            /// </summary>
            Package,

            /// <summary>
            /// The hotel
            /// </summary>
            Hotel
        }

        /// <summary>
        /// Best Time To Call
        /// </summary>
        public enum BestTimeToCall
        {
            /// <summary>
            /// Any
            /// </summary>
            [Display(Name = "Any")]
            Any,

            /// <summary>
            /// The morning
            /// </summary>
            [Display(Name = "Morning (9:00 am to 12:00 pm)")]
            Morning,

            /// <summary>
            /// The afternoon
            /// </summary>
            [Display(Name = "Afternoon (12:00 pm to 3:00 pm)")]
            Afternoon,

            /// <summary>
            /// The evening
            /// </summary>
            [Display(Name = "Evening (5:00 pm onwards)")]
            Evening
        }

        /// <summary>
        /// RateTypeApplied
        /// </summary>
        public enum RateTypeApplied
        {
            /// <summary>
            /// The single
            /// </summary>
            Single,

            /// <summary>
            /// The double
            /// </summary>
            Double,

            /// <summary>
            /// The triple
            /// </summary>
            Triple
        }

        /// <summary>
        /// PersonType
        /// </summary>
        public enum PromoType
        {
            /// <summary>
            /// The adult
            /// </summary>
            FlashDeal = 1,

            /// <summary>
            /// The child
            /// </summary>
            DealoftheMonth = 2,

            /// <summary>
            /// The infant
            /// </summary>
            SpecialPromotion = 3
        }

        /// <summary>
        /// PersonType
        /// </summary>
        public enum FilterSelectTypes
        {
            /// <summary>
            /// SingleSelect
            /// </summary>
            SingleSelect = 1,

            /// <summary>
            /// MultiSelect
            /// </summary>
            MultiSelect = 2
        }

        /// <summary>
        /// PersonType
        /// </summary>
        public enum FilterTypes
        {
            /// <summary>
            /// SingleSelect
            /// </summary>
            Type = 1,

            /// <summary>
            /// SingleSelect
            /// </summary>
            Budget = 2,

            /////// <summary>
            /////// MultiSelect
            /////// </summary>
            ////StarCategory = 2,

            /// <summary>
            /// SingleSelect
            /// </summary>
            Accomodation = 3,

            /// <summary>
            /// MultiSelect
            /// </summary>
            Country = 4,

            /// <summary>
            /// MultiSelect
            /// </summary>
            City = 5,

            /// <summary>
            /// MultiSelect
            /// </summary>
            CityArea = 6,

            /////// <summary>
            /////// MultiSelect
            /////// </summary>
            ////Reviews = 5,

            /// <summary>
            /// MultiSelect
            /// </summary>
            HotelAmeneties = 7,

            /////// <summary>
            /////// MultiSelect
            /////// </summary>
            ////RoomAmeneties = 7,

            /// <summary>
            /// MultiSelect
            /// </summary>
            HotelChain = 8,

            /// <summary>
            /// MultiSelect
            /// </summary>
            TravelStyle = 9,

            /// <summary>
            /// Single Select
            /// </summary>
            Visa = 10,

            /// <summary>
            /// MultiSelect
            /// </summary>
            Flight = 11
        }

        /// <summary>
        /// PersonType
        /// </summary>
        public enum SearchType
        {
            /// <summary>
            /// SingleSelect
            /// </summary>
            FlashDeal = 1,

            /// <summary>
            /// MultiSelect
            /// </summary>
            DealOfTheMonth = 2,

            /// <summary>
            /// MultiSelect
            /// </summary>
            Country = 3,

            /// <summary>
            /// MultiSelect
            /// </summary>
            City = 4,

            /// <summary>
            /// MultiSelect
            /// </summary>
            TravelStyle = 5,

            /// <summary>
            /// MultiSelect
            /// </summary>
            Query = 6,

            /// <summary>
            /// MultiSelect
            /// </summary>
            Product = 7,

            /// <summary>
            /// MultiSelect
            /// </summary>
            Hotel = 8,

            /// <summary>
            /// MultiSelect
            /// </summary>
            State = 9,

            /// <summary>
            /// MultiSelect
            /// </summary>
            All = 10
        }

        /// <summary>
        /// PersonType
        /// </summary>
        public enum SortFilterType
        {
            /// <summary>
            /// SingleSelect
            /// </summary>
            PriceLowToHight = 1,

            /// <summary>
            /// SingleSelect
            /// </summary>
            PriceHighToLow = 6,

            /// <summary>
            /// MultiSelect
            /// </summary>
            DiscountHighToLow = 2,

            /// <summary>
            /// MultiSelect
            /// </summary>
            DiscountLowToHigh = 7,

            /// <summary>
            /// MultiSelect
            /// </summary>
            PopularityHighToLow = 3,

            /// <summary>
            /// MultiSelect
            /// </summary>
            WhatsNew = 4,

            /// <summary>
            /// MultiSelect
            /// </summary>
            NearMe = 5
        }

        /// <summary>
        /// PersonType
        /// </summary>
        public enum DealPromotionType
        {
            /// <summary>
            /// SingleSelect
            /// </summary>
            FlatDiscount = 1,

            /// <summary>
            /// MultiSelect
            /// </summary>
            EarlyBirdOffer = 2,

            /// <summary>
            /// MultiSelect
            /// </summary>
            DayoftheWeek = 3,

            /// <summary>
            /// MultiSelect
            /// </summary>
            LengthOfStay = 4
        }
    }
}