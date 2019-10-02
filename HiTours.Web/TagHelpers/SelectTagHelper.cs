﻿// <copyright file="SelectTagHelper.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Web.Framework.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;
    using Newtonsoft.Json;

    /// <summary>
    /// AutoComplete Select2 to For Select List
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" />
    [HtmlTargetElement("select")]
    public class SelectTagHelper : TagHelper
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SelectTagHelper"/> is disabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if disabled; otherwise, <c>false</c>.
        /// </value>
        [HtmlAttributeName("asp-disabled")]
        public bool Disabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SelectTagHelper"/> is default.
        /// </summary>
        /// <value>
        ///   <c>true</c> if default; otherwise, <c>false</c>.
        /// </value>
        [HtmlAttributeName("select2-stop-render")]
        public bool StopRender { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SelectTagHelper"/> is default.
        /// </summary>
        /// <value>
        ///   <c>true</c> if default; otherwise, <c>false</c>.
        /// </value>
        [HtmlAttributeName("select2-Default")]
        public bool Default { get; set; }

        /// <summary>
        /// Gets or sets the minimum length of the input.
        /// </summary>
        /// <value>
        /// The minimum length of the input.
        /// </value>
        [HtmlAttributeName("select2-MinimumInputLength")]
        public int MinimumInputLength { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SelectTagHelper"/> is multiple.
        /// </summary>
        /// <value>
        ///   <c>true</c> if multiple; otherwise, <c>false</c>.
        /// </value>
        [HtmlAttributeName("select2-Multiple")]
        public bool Multiple { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SelectTagHelper"/> is hold.
        /// </summary>
        /// <value>
        ///   <c>true</c> if hold; otherwise, <c>false</c>.
        /// </value>
        [HtmlAttributeName("select2-hold")]
        public string Hold { get; set; }

        /// <summary>
        /// Gets or sets the placeholder.
        /// </summary>
        /// <value>
        /// The placeholder.
        /// </value>
        [HtmlAttributeName("select2-Placeholder")]
        public string Placeholder { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [HtmlAttributeName("select2-Url")]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the partial URL.
        /// </summary>
        /// <value>
        /// The partial URL.
        /// </value>
        [HtmlAttributeName("select2-partial-Url")]
        public string PartialUrl { get; set; }

        /// <summary>
        /// Gets or sets the partial title.
        /// </summary>
        /// <value>
        /// The partial title.
        /// </value>
        [HtmlAttributeName("select2-partial-title")]
        public string PartialTitle { get; set; }

        /// <summary>
        /// Gets or sets the delay.
        /// </summary>
        /// <value>
        /// The delay.
        /// </value>
        [HtmlAttributeName("select2-Delay")]
        public int Delay { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow clear].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow clear]; otherwise, <c>false</c>.
        /// </value>
        [HtmlAttributeName("select2-AllowClear")]
        public bool AllowClear { get; set; } = true;

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        /// <value>
        /// The tags.
        /// </value>
        [HtmlAttributeName("select2-Tags")]
        public int Tags { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SelectTagHelper"/> is dependent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if dependent; otherwise, <c>false</c>.
        /// </value>
        [HtmlAttributeName("select2-Dependent")]
        public bool Dependent { get; set; }

        /// <summary>
        /// Gets or sets the control.
        /// </summary>
        /// <value>
        /// The control.
        /// </value>
        [HtmlAttributeName("select2-Dependent-Control")]
        public string Control { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [HtmlAttributeName("select2-value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the other controls.
        /// </summary>
        /// <value>
        /// The other controls.
        /// </value>
        [HtmlAttributeName("select2-Dependent-OtherControl")]
        public string OtherControls { get; set; }

        /// <summary>
        /// Gets or sets the selected.
        /// </summary>
        /// <value>
        /// The selected.
        /// </value>
        [HtmlAttributeName("selected")]
        public string Selected { get; set; }

        /// <summary>
        /// Gets or sets the maximum length of the selection.
        /// </summary>
        /// <value>
        /// The maximum length of the selection.
        /// </value>
        [HtmlAttributeName("select2-MaximumSelectionLength")]
        public string MaximumSelectionLength { get; set; }

        /// <summary>
        /// Synchronously executes the <see cref="T:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" /> with the given <paramref name="context" /> and
        /// <paramref name="output" />.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!this.StopRender)
            {
                this.Default = string.IsNullOrEmpty(this.Url);
                var select2Options = new
                {
                    allowClear = this.AllowClear,
                    delay = string.IsNullOrEmpty(this.Url) ? 0 : this.Delay == 0 ? 250 : this.Delay,
                    tags = this.Tags,
                    ajaxCall = string.IsNullOrEmpty(this.Url),
                    placeholder = string.IsNullOrEmpty(this.Placeholder) ? "- Select -" : this.Placeholder,
                    url = string.IsNullOrEmpty(this.Url) ? string.Empty : this.Url,
                    partialUrl = string.IsNullOrEmpty(this.PartialUrl) ? string.Empty : this.PartialUrl,
                    partialTitle = string.IsNullOrEmpty(this.PartialTitle) ? string.Empty : this.PartialTitle,
                    minimumInputLength = this.MinimumInputLength,
                    dependent = this.Dependent && !string.IsNullOrEmpty(this.Url),
                    control = !string.IsNullOrEmpty(this.Control) ? this.Control : string.Empty,
                    otherControls = !string.IsNullOrEmpty(this.OtherControls) ? this.OtherControls : string.Empty,
                    currentValue = !string.IsNullOrEmpty(this.Value) ? this.Value : string.Empty,
                    disabled = this.Disabled,
                    hold = this.Hold,
                    maximumSelectionLength = this.MaximumSelectionLength
                };

                if (this.Disabled)
                {
                    output.Attributes.Add(new TagHelperAttribute("disabled", "disabled"));
                }

                if (!string.IsNullOrEmpty(this.Selected))
                {
                    output.Attributes.Add("data-selected", this.Selected);
                }

                output.Attributes.Add("data-pluggin-select2", JsonConvert.SerializeObject(select2Options));

                base.Process(context, output);
            }
        }
    }
}