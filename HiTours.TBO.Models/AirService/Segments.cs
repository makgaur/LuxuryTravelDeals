using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public class Segments
    {
        [Required]
        [Display(Name = "Origin")]
        public string Origin { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public string Destination { get; set; }

        [Required]
        [Display(Name = "Flight Cabin Class")]
        public int FlightCabinClass { get; set; }

        [Required]
        [Display(Name = "Departure Time")]
        public DateTime PreferredDepartureTime { get; set; }

        [Required]
        [Display(Name = "Arrival Time")]
        public DateTime PreferredArrivalTime { get; set; }

        [JsonIgnore]
        public int JourneyType { get; set; }

        [JsonIgnore]
        public bool CanRemove { get; set; }

        /// <summary>
        /// Gets or sets the origin list.
        /// </summary>
        /// <value>
        /// The origin list.
        /// </value>
        [JsonIgnore]
        public List<SelectListItem> OriginList { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// Gets or sets the destination list.
        /// </summary>
        /// <value>
        /// The destination list.
        /// </value>
        [JsonIgnore]
        public List<SelectListItem> DestinationList { get; set; } = new List<SelectListItem>();

        /// <summary>
        /// Gets or sets the display destination.
        /// </summary>
        /// <value>
        /// The display destination.
        /// </value>
        public string DisplayDestination { get; set; }
        /// <summary>
        /// Gets or sets the dispaly origin.
        /// </summary>
        /// <value>
        /// The dispaly origin.
        /// </value>
        public string DisplayOrigin { get; set; }

        /// <summary>
        /// Gets or sets the name of the origin.
        /// </summary>
        /// <value>
        /// The name of the origin.
        /// </value>
        public string OriginCityName { get; set; }

        /// <summary>
        /// Gets or sets the name of the destination city.
        /// </summary>
        /// <value>
        /// The name of the destination city.
        /// </value>
        public string DestinationCityName { get; set; }
    }
}