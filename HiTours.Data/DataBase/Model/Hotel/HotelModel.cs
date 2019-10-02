// <copyright file="HotelModel.cs" company="Luxury Travel Deals">
// Copyright (c) Luxury Travel Deals All rights reserved.
// </copyright>

namespace HiTours.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// HotelModel
    /// </summary>
    public class HotelModel
    {
        /// <summary>
        /// Gets or sets the hotel identifier.
        /// </summary>
        /// <value>
        /// The hotel identifier.
        /// </value>
        [Key]
        public Guid HotelId { get; set; }

        /// <summary>
        /// Gets or sets the accommodation group identifier.
        /// </summary>
        /// <value>
        /// The accommodation group identifier.
        /// </value>
        public Guid AccommodationGroupId { get; set; }

        /// <summary>
        /// Gets or sets the sales manager.
        /// </summary>
        /// <value>
        /// The sales manager.
        /// </value>
        public string SalesManager { get; set; }

        /// <summary>
        /// Gets or sets the sales office manager.
        /// </summary>
        /// <value>
        /// The sales office manager.
        /// </value>
        public string SalesOfficeManager { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the sales office address.
        /// </summary>
        /// <value>
        /// The sales office address.
        /// </value>
        public string SalesOfficeAddress { get; set; }

        /// <summary>
        /// Gets or sets the pin code.
        /// </summary>
        /// <value>
        /// The pin code.
        /// </value>
        public string PinCode { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>
        /// The website.
        /// </value>
        public string Website { get; set; }

        /// <summary>
        /// Gets or sets the add date.
        /// </summary>
        /// <value>
        /// The add date.
        /// </value>
        public DateTime AddDate { get; set; }

        /// <summary>
        /// Gets or sets the last modified date.
        /// </summary>
        /// <value>
        /// The last modified date.
        /// </value>
        public DateTime LastModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the hotel star rating identifier.
        /// </summary>
        /// <value>
        /// The hotel star rating identifier.
        /// </value>
        public Guid HotelStarRatingId { get; set; }

        /// <summary>
        /// Gets or sets the type of the building.
        /// </summary>
        /// <value>
        /// The type of the building.
        /// </value>
        public string BuildingType { get; set; }

        /// <summary>
        /// Gets or sets the hotel built date.
        /// </summary>
        /// <value>
        /// The hotel built date.
        /// </value>
        public DateTime HotelBuiltDate { get; set; }

        /// <summary>
        /// Gets or sets the renovated on.
        /// </summary>
        /// <value>
        /// The renovated on.
        /// </value>
        public DateTime RenovatedOn { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="HotelModel"/> is annexe.
        /// </summary>
        /// <value>
        ///   <c>true</c> if annexe; otherwise, <c>false</c>.
        /// </value>
        public bool Annexe { get; set; }

        /// <summary>
        /// Gets or sets the earliestcheckintime.
        /// </summary>
        /// <value>
        /// The earliestcheckintime.
        /// </value>
        public string Earliestcheckintime { get; set; }

        /// <summary>
        /// Gets or sets the receptionlanguages.
        /// </summary>
        /// <value>
        /// The receptionlanguages.
        /// </value>
        public string Receptionlanguages { get; set; }

        /// <summary>
        /// Gets or sets the size of the lobby.
        /// </summary>
        /// <value>
        /// The size of the lobby.
        /// </value>
        public string LobbySize { get; set; }

        /// <summary>
        /// Gets or sets the noof lifts.
        /// </summary>
        /// <value>
        /// The noof lifts.
        /// </value>
        public string NoofLifts { get; set; }

        /// <summary>
        /// Gets or sets the noof floors.
        /// </summary>
        /// <value>
        /// The noof floors.
        /// </value>
        public string NoofFloors { get; set; }

        /// <summary>
        /// Gets or sets the noof indoor pools.
        /// </summary>
        /// <value>
        /// The noof indoor pools.
        /// </value>
        public string NoofIndoorPools { get; set; }

        /// <summary>
        /// Gets or sets the noof outdoorpools.
        /// </summary>
        /// <value>
        /// The noof outdoorpools.
        /// </value>
        public string NoofOutdoorpools { get; set; }

        /// <summary>
        /// Gets or sets the baggage handling provided.
        /// </summary>
        /// <value>
        /// The baggage handling provided.
        /// </value>
        public string BaggageHandlingProvided { get; set; }

        /// <summary>
        /// Gets or sets the baggage handle from.
        /// </summary>
        /// <value>
        /// The baggage handle from.
        /// </value>
        public DateTime BaggageHandleFrom { get; set; }

        /// <summary>
        /// Gets or sets the baggage handle to.
        /// </summary>
        /// <value>
        /// The baggage handle to.
        /// </value>
        public DateTime BaggageHandleTo { get; set; }

        /// <summary>
        /// Gets or sets the total noof rooms.
        /// </summary>
        /// <value>
        /// The total noof rooms.
        /// </value>
        public string TotalNoofRooms { get; set; }

        /// <summary>
        /// Gets or sets the disabled rooms.
        /// </summary>
        /// <value>
        /// The disabled rooms.
        /// </value>
        public string DisabledRooms { get; set; }

        /// <summary>
        /// Gets or sets the no smoking rooms.
        /// </summary>
        /// <value>
        /// The no smoking rooms.
        /// </value>
        public string NoSmokingRooms { get; set; }

        /// <summary>
        /// Gets or sets the early break fast from.
        /// </summary>
        /// <value>
        /// The early break fast from.
        /// </value>
        public DateTime EarlyBreakFastFrom { get; set; }

        /// <summary>
        /// Gets or sets the served in.
        /// </summary>
        /// <value>
        /// The served in.
        /// </value>
        public string ServedIn { get; set; }

        /// <summary>
        /// Gets or sets the breakfast open.
        /// </summary>
        /// <value>
        /// The breakfast open.
        /// </value>
        public string BreakfastOpen { get; set; }

        /// <summary>
        /// Gets or sets the breakfast close.
        /// </summary>
        /// <value>
        /// The breakfast close.
        /// </value>
        public string BreakfastClose { get; set; }

        /// <summary>
        /// Gets or sets the lunch open.
        /// </summary>
        /// <value>
        /// The lunch open.
        /// </value>
        public string LunchOpen { get; set; }

        /// <summary>
        /// Gets or sets the lunch close.
        /// </summary>
        /// <value>
        /// The lunch close.
        /// </value>
        public string LunchClose { get; set; }

        /// <summary>
        /// Gets or sets the dinner open.
        /// </summary>
        /// <value>
        /// The dinner open.
        /// </value>
        public string DinnerOpen { get; set; }

        /// <summary>
        /// Gets or sets the dinner close.
        /// </summary>
        /// <value>
        /// The dinner close.
        /// </value>
        public string DinnerClose { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [seperate group dining room].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [seperate group dining room]; otherwise, <c>false</c>.
        /// </value>
        public bool SeperateGroupDiningRoom { get; set; }

        /// <summary>
        /// Gets or sets the noof conference room.
        /// </summary>
        /// <value>
        /// The noof conference room.
        /// </value>
        public string NoofConferenceRoom { get; set; }

        /// <summary>
        /// Gets or sets the voltage.
        /// </summary>
        /// <value>
        /// The voltage.
        /// </value>
        public string Voltage { get; set; }

        /// <summary>
        /// Gets or sets the room service.
        /// </summary>
        /// <value>
        /// The room service.
        /// </value>
        public string RoomService { get; set; }

        /// <summary>
        /// Gets or sets the room service open.
        /// </summary>
        /// <value>
        /// The room service open.
        /// </value>
        public DateTime RoomServiceOpen { get; set; }

        /// <summary>
        /// Gets or sets the room service close.
        /// </summary>
        /// <value>
        /// The room service close.
        /// </value>
        public DateTime RoomServiceClose { get; set; }

        /// <summary>
        /// Gets or sets the payment condition.
        /// </summary>
        /// <value>
        /// The payment condition.
        /// </value>
        public string PaymentCondition { get; set; }

        /// <summary>
        /// Gets or sets the onarrival.
        /// </summary>
        /// <value>
        /// The onarrival.
        /// </value>
        public string Onarrival { get; set; }

        /// <summary>
        /// Gets or sets the advance.
        /// </summary>
        /// <value>
        /// The advance.
        /// </value>
        public string Advance { get; set; }

        /// <summary>
        /// Gets or sets the credit.
        /// </summary>
        /// <value>
        /// The credit.
        /// </value>
        public string Credit { get; set; }

        /// <summary>
        /// Gets or sets the allotment.
        /// </summary>
        /// <value>
        /// The allotment.
        /// </value>
        public string Allotment { get; set; }

        /// <summary>
        /// Gets or sets the cut off days.
        /// </summary>
        /// <value>
        /// The cut off days.
        /// </value>
        public string CutOffDays { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the hotel image.
        /// </summary>
        /// <value>
        /// The hotel image.
        /// </value>
        public byte[] HotelImage { get; set; }

        /// <summary>
        /// Gets or sets the name of the beneficiary account.
        /// </summary>
        /// <value>
        /// The name of the beneficiary account.
        /// </value>
        public string BeneficiaryAccountName { get; set; }

        /// <summary>
        /// Gets or sets the account no.
        /// </summary>
        /// <value>
        /// The account no.
        /// </value>
        public string AccountNo { get; set; }

        /// <summary>
        /// Gets or sets the name of the bank.
        /// </summary>
        /// <value>
        /// The name of the bank.
        /// </value>
        public string BankName { get; set; }

        /// <summary>
        /// Gets or sets the ifsc code.
        /// </summary>
        /// <value>
        /// The ifsc code.
        /// </value>
        public string IFSCCode { get; set; }

        /// <summary>
        /// Gets or sets the payable at.
        /// </summary>
        /// <value>
        /// The payable at.
        /// </value>
        public string PayableAt { get; set; }

        /// <summary>
        /// Gets or sets the branch address.
        /// </summary>
        /// <value>
        /// The branch address.
        /// </value>
        public string BranchAddress { get; set; }

        /// <summary>
        /// Gets or sets the remarks.
        /// </summary>
        /// <value>
        /// The remarks.
        /// </value>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the name of the ledger.
        /// </summary>
        /// <value>
        /// The name of the ledger.
        /// </value>
        public string LedgerName { get; set; }

        /// <summary>
        /// Gets or sets the hotel rating.
        /// </summary>
        /// <value>
        /// The hotel rating.
        /// </value>
        public short HotelRating { get; set; }

        /// <summary>
        /// Gets or sets the hotel class.
        /// </summary>
        /// <value>
        /// The hotel class.
        /// </value>
        public short HotelClass { get; set; }

        /// <summary>
        /// Gets or sets the address line1.
        /// </summary>
        /// <value>
        /// The address line1.
        /// </value>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the address line2.
        /// </summary>
        /// <value>
        /// The address line2.
        /// </value>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the city area identifier.
        /// </summary>
        /// <value>
        /// The city area identifier.
        /// </value>
        public Guid CityAreaId { get; set; }

        /// <summary>
        /// Gets or sets the payment mode.
        /// </summary>
        /// <value>
        /// The payment mode.
        /// </value>
        public string PaymentMode { get; set; }
    }
}