// <copyright file="HotelPriceDetailModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// HotelPriceDetailModel
    /// </summary>
    public class HotelPriceDetailModel
    {
        /// <summary>
        /// Gets or sets the hotel price detail identifier.
        /// </summary>
        /// <value>
        /// The hotel price detail identifier.
        /// </value>
        [Key]
        public Guid HotelPriceDetailId { get; set; }

        /// <summary>
        /// Gets or sets the hotel price identifier.
        /// </summary>
        /// <value>
        /// The hotel price identifier.
        /// </value>
        public Guid HotelPriceId { get; set; }

        /// <summary>
        /// Gets or sets the meal plan identifier.
        /// </summary>
        /// <value>
        /// The meal plan identifier.
        /// </value>
        public Guid MealPlanId { get; set; }

        /// <summary>
        /// Gets or sets the base meal plan identifier.
        /// </summary>
        /// <value>
        /// The base meal plan identifier.
        /// </value>
        public Guid BaseMealPlanId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is tax included.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is tax included; otherwise, <c>false</c>.
        /// </value>
        public bool IsTaxIncluded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is include git rate.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is include git rate; otherwise, <c>false</c>.
        /// </value>
        public bool IsIncludeGitRate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is include fit rate.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is include fit rate; otherwise, <c>false</c>.
        /// </value>
        public bool IsIncludeFitRate { get; set; }

        /// <summary>
        /// Gets or sets the drfit single.
        /// </summary>
        /// <value>
        /// The drfit single.
        /// </value>
        public double DRFITSingle { get; set; }

        /// <summary>
        /// Gets or sets the drfit double.
        /// </summary>
        /// <value>
        /// The drfit double.
        /// </value>
        public double DRFITDouble { get; set; }

        /// <summary>
        /// Gets or sets the drfit extra bed.
        /// </summary>
        /// <value>
        /// The drfit extra bed.
        /// </value>
        public double DRFITExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the drfit child extra bed.
        /// </summary>
        /// <value>
        /// The drfit child extra bed.
        /// </value>
        public double DRFITChildExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the drfit child without extra bed.
        /// </summary>
        /// <value>
        /// The drfit child without extra bed.
        /// </value>
        public double DRFITChildWithoutExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the drgit single.
        /// </summary>
        /// <value>
        /// The drgit single.
        /// </value>
        public double DRGITSingle { get; set; }

        /// <summary>
        /// Gets or sets the drgit double.
        /// </summary>
        /// <value>
        /// The drgit double.
        /// </value>
        public double DRGITDouble { get; set; }

        /// <summary>
        /// Gets or sets the drgit extra bed.
        /// </summary>
        /// <value>
        /// The drgit extra bed.
        /// </value>
        public double DRGITExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the drgit child extra bed.
        /// </summary>
        /// <value>
        /// The drgit child extra bed.
        /// </value>
        public double DRGITChildExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the drgit child without extra bed.
        /// </summary>
        /// <value>
        /// The drgit child without extra bed.
        /// </value>
        public double DRGITChildWithoutExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the prfit single.
        /// </summary>
        /// <value>
        /// The prfit single.
        /// </value>
        public double PRFITSingle { get; set; }

        /// <summary>
        /// Gets or sets the prfit double.
        /// </summary>
        /// <value>
        /// The prfit double.
        /// </value>
        public double PRFITDouble { get; set; }

        /// <summary>
        /// Gets or sets the prfit extra bed.
        /// </summary>
        /// <value>
        /// The prfit extra bed.
        /// </value>
        public double PRFITExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the prfit child extra bed.
        /// </summary>
        /// <value>
        /// The prfit child extra bed.
        /// </value>
        public double PRFITChildExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the prfit child without extra bed.
        /// </summary>
        /// <value>
        /// The prfit child without extra bed.
        /// </value>
        public double PRFITChildWithoutExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the prgit single.
        /// </summary>
        /// <value>
        /// The prgit single.
        /// </value>
        public double PRGITSingle { get; set; }

        /// <summary>
        /// Gets or sets the prgit double.
        /// </summary>
        /// <value>
        /// The prgit double.
        /// </value>
        public double PRGITDouble { get; set; }

        /// <summary>
        /// Gets or sets the prgit extra bed.
        /// </summary>
        /// <value>
        /// The prgit extra bed.
        /// </value>
        public double PRGITExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the prgit child extra bed.
        /// </summary>
        /// <value>
        /// The prgit child extra bed.
        /// </value>
        public double PRGITChildExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the prgit child without extra bed.
        /// </summary>
        /// <value>
        /// The prgit child without extra bed.
        /// </value>
        public double PRGITChildWithoutExtraBed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is tax on publish rate.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is tax on publish rate; otherwise, <c>false</c>.
        /// </value>
        public bool IsTaxOnPublishRate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is luxary tax applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is luxary tax applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsLuxaryTaxApplicable { get; set; }

        /// <summary>
        /// Gets or sets the luxary tax.
        /// </summary>
        /// <value>
        /// The luxary tax.
        /// </value>
        public double LuxaryTax { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is hotel vat applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is hotel vat applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsHotelVATApplicable { get; set; }

        /// <summary>
        /// Gets or sets the hotel vat.
        /// </summary>
        /// <value>
        /// The hotel vat.
        /// </value>
        public double HotelVAT { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is hotel service tax applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is hotel service tax applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsHotelServiceTaxApplicable { get; set; }

        /// <summary>
        /// Gets or sets the hotel service tax.
        /// </summary>
        /// <value>
        /// The hotel service tax.
        /// </value>
        public double HotelServiceTax { get; set; }

        /// <summary>
        /// Gets or sets the fit single.
        /// </summary>
        /// <value>
        /// The fit single.
        /// </value>
        public double? FITSingle { get; set; }

        /// <summary>
        /// Gets or sets the fit double.
        /// </summary>
        /// <value>
        /// The fit double.
        /// </value>
        public double FITDouble { get; set; }

        /// <summary>
        /// Gets or sets the fit extra bed.
        /// </summary>
        /// <value>
        /// The fit extra bed.
        /// </value>
        public double FITExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the fit child extra bed.
        /// </summary>
        /// <value>
        /// The fit child extra bed.
        /// </value>
        public double FITChildExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the fit child without extra bed.
        /// </summary>
        /// <value>
        /// The fit child without extra bed.
        /// </value>
        public double FITChildWithoutExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the git single.
        /// </summary>
        /// <value>
        /// The git single.
        /// </value>
        public double GITSingle { get; set; }

        /// <summary>
        /// Gets or sets the git double.
        /// </summary>
        /// <value>
        /// The git double.
        /// </value>
        public double GITDouble { get; set; }

        /// <summary>
        /// Gets or sets the git extra bed.
        /// </summary>
        /// <value>
        /// The git extra bed.
        /// </value>
        public double GITExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the git child extra bed.
        /// </summary>
        /// <value>
        /// The git child extra bed.
        /// </value>
        public double GITChildExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the git child without extra bed.
        /// </summary>
        /// <value>
        /// The git child without extra bed.
        /// </value>
        public double GITChildWithoutExtraBed { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        public string Remark { get; set; }

        /// <summary>
        /// Gets or sets the sequence number.
        /// </summary>
        /// <value>
        /// The sequence number.
        /// </value>
        public short SequenceNumber { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        /// <value>
        /// The modified date.
        /// </value>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the hotel other tax.
        /// </summary>
        /// <value>
        /// The hotel other tax.
        /// </value>
        public double HotelOtherTax { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is hotel other tax applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is hotel other tax applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsHotelOtherTaxApplicable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is hotel rate hiked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is hotel rate hiked; otherwise, <c>false</c>.
        /// </value>
        public bool IsHotelRateHiked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is hotel price approve.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is hotel price approve; otherwise, <c>false</c>.
        /// </value>
        public bool IsHotelPriceApprove { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is fit single GST applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is fit single GST applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsFITSingleGSTApplicable { get; set; }

        /// <summary>
        /// Gets or sets the fit single hotel GST detail identifier.
        /// </summary>
        /// <value>
        /// The fit single hotel GST detail identifier.
        /// </value>
        public Guid FITSingleHotelGSTDetailId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is fit double GST applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is fit double GST applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsFITDoubleGSTApplicable { get; set; }

        /// <summary>
        /// Gets or sets the fit double hotel GST detail identifier.
        /// </summary>
        /// <value>
        /// The fit double hotel GST detail identifier.
        /// </value>
        public Guid FITDoubleHotelGSTDetailId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is fit extra bed GST applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is fit extra bed GST applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsFITExtraBedGSTApplicable { get; set; }

        /// <summary>
        /// Gets or sets the fit extra bed hotel GST detail identifier.
        /// </summary>
        /// <value>
        /// The fit extra bed hotel GST detail identifier.
        /// </value>
        public Guid FITExtraBedHotelGSTDetailId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is git single GST applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is git single GST applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsGITSingleGSTApplicable { get; set; }

        /// <summary>
        /// Gets or sets the git single hotel GST detail identifier.
        /// </summary>
        /// <value>
        /// The git single hotel GST detail identifier.
        /// </value>
        public Guid GITSingleHotelGSTDetailId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is git double GST applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is git double GST applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsGITDoubleGSTApplicable { get; set; }

        /// <summary>
        /// Gets or sets the git double hotel GST detail identifier.
        /// </summary>
        /// <value>
        /// The git double hotel GST detail identifier.
        /// </value>
        public Guid GITDoubleHotelGSTDetailId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is git extra bed GST applicable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is git extra bed GST applicable; otherwise, <c>false</c>.
        /// </value>
        public bool IsGITExtraBedGSTApplicable { get; set; }

        /// <summary>
        /// Gets or sets the git extra bed hotel GST detail identifier.
        /// </summary>
        /// <value>
        /// The git extra bed hotel GST detail identifier.
        /// </value>
        public Guid GITExtraBedHotelGSTDetailId { get; set; }
    }
}