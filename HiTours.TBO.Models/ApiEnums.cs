using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HiTours.TBO.Models
{
    public enum AuthenticateStatus
    {
        NotSet = 0,
        Successful = 1,
        Failed = 2,
        InCorrectUserName = 3,
        InCorrectPassword = 4,
        PasswordExpired = 5
    }

    public enum JourneyType
    {
        [DisplayName("One Way")]
        OneWay = 1,

        [DisplayName("Return")]
        Return = 2,

        [DisplayName("MultiStop")]
        MultiStop = 3,

        [DisplayName("Advance Search")]
        AdvanceSearch = 4,

        [DisplayName("Air Service Search Special Return")]
        AirServiceSearchSpecialReturn = 5
    }

    public enum RequestType
    {
        NotSet = 0,
        FullCancellation = 1,
        PartialCancellation = 2,
        Reissuance = 3,
    }

    public enum CancellationType
    {
        NotSet = 0,
        NoShow = 1,
        FlightCancelled = 2,
        Others = 3,
    }

    public enum FlightCabin
    {
        All = 1,
        Economy = 2,
        PremiumEconomy = 3,
        Business = 4,
        PremiumBusiness = 5,
        First = 6
    }

    public enum AirLineSource
    {
        [DisplayName("Amadeus/Galileo")]
        GDS,

        [DisplayName("SpiceJet")]
        SG,

        [DisplayName("Indigo")]
        _6E,

        [DisplayName("Go Air")]
        G8,

        [DisplayName("Air Arabia")]
        G9,

        [DisplayName("Fly Dubai")]
        FZ,

        [DisplayName("Air India Express")]
        IX,

        [DisplayName("Air Asia")]
        AK,

        [DisplayName("Air Costa")]
        LB,
    }

    public enum AirLinePreferred
    {
        [DisplayName("AI")]
        AI,

        [DisplayName("9W")]
        _9W,

        [DisplayName("SG")]
        SG,

        [DisplayName("6E")]
        _6E,

        [DisplayName("G8")]
        G8,

        [DisplayName("G9")]
        G9,

        [DisplayName("FZ")]
        FZ,

        [DisplayName("IX")]
        IX,

        [DisplayName("AK")]
        AK,

        [DisplayName("LB")]
        LB
    }

    public enum WayType
    {
        NotSet = 0,
        Segment = 1,
        FullJourney = 2
    }

    public enum Description
    {
        NotSet,
        Included,
        Direct,
        Imported,
        UpGrade,
        ImportedUpgrade
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public enum Salutation
    {
        [DisplayName("Mr")]
        Mr,

        [DisplayName("Mstr")]
        Mstr,

        [DisplayName("Mrs")]
        Mrs,

        [DisplayName("Ms")]
        Ms,

        [DisplayName("Miss")]
        Miss,

        [DisplayName("Master")]
        Master,

        [DisplayName("DR")]
        DR
    }

    public enum PaxType
    {
        Adult = 1,
        Child = 2,
        Infant = 3
    }

    public enum PaymentStatus
    {
        None = 0,
        Done = 1,
        Process = 2,
        Failed = 3
    }
}