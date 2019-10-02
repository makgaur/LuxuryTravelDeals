// <copyright file="Dropdown.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Core
{
    using Newtonsoft.Json;

    /// <summary>
    /// Dropdown
    /// </summary>
    public class Dropdown
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dropdown"/> class.
        /// Default Constructor
        /// </summary>
        public Dropdown()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dropdown"/> class.
        /// </summary>
        /// <param name="id"> Identifier property</param>
        /// <param name="name">NAme property</param>
        public Dropdown(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [JsonProperty(PropertyName = "text")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the IconClass.
        /// </summary>
        /// <value>
        /// The Icon Class Name.
        /// </value>
        [JsonProperty(PropertyName = "IconClass")]
        public string IconClass { get; set; }
    }
}