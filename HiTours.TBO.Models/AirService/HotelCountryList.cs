using System;
using System.Collections.Generic;
using System.Text;

namespace HiTours.TBO.Models
{
   public class HotelCountryList
    {
        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientId { get; set; }
        /// <summary>
        /// Gets or sets the end user ip.
        /// </summary>
        /// <value>
        /// The end user ip.
        /// </value>
        public string EndUserIp { get; set; }
        /// <summary>
        /// Gets or sets the token identifier.
        /// </summary>
        /// <value>
        /// The token identifier.
        /// </value>
        public string TokenId { get; set; }

        public string CountryCode { get; set; }
    }
}
