using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace HiTours.TBO.Models
{
    public class HotelSearch
    {
        [JsonProperty("CheckInDate")]
        public string CheckInDate { get; set; }

        [JsonProperty("NoOfNights")]
        public string NoOfNights { get; set; }

        [JsonProperty("CountryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("CityId")]
        public string CityId { get; set; }

        [JsonProperty("ResultCount")]
        public object ResultCount { get; set; }

        [JsonProperty("PreferredCurrency")]
        public string PreferredCurrency { get; set; }

        [JsonProperty("GuestNationality")]
        public string GuestNationality { get; set; }

        [JsonProperty("NoOfRooms")]
        public string NoOfRooms { get; set; }

        [JsonProperty("RoomGuests")]
        public RoomGuest[] RoomGuests { get; set; }

        [JsonProperty("PreferredHotel")]
        public string PreferredHotel { get; set; }

        [JsonProperty("MaxRating")]
        public long MaxRating { get; set; }

        [JsonProperty("MinRating")]
        public long MinRating { get; set; }

        [JsonProperty("ReviewScore")]
        public object ReviewScore { get; set; }

        [JsonProperty("IsNearBySearchAllowed")]
        public bool IsNearBySearchAllowed { get; set; }

        [JsonProperty("EndUserIp")]
        public string EndUserIp { get; set; }

        [JsonProperty("TokenId")]
        public string TokenId { get; set; }
    }

    public partial class RoomGuest
    {
        [JsonProperty("NoOfAdults")]
        public long NoOfAdults { get; set; }

        [JsonProperty("NoOfChild")]
        public long NoOfChild { get; set; }

        [JsonProperty("ChildAge")]
        public object ChildAge { get; set; }
    }
}
