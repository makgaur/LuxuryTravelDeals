
CREATE TABLE [dbo].[HotelBooking](
	[RowNo] [bigint] IDENTITY(1,1) NOT NULL,
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [int] NOT NULL,
	[BookingPackageType] [varchar](10) NULL,
	[BookingDate] [datetime] NULL,
	[BlockDate] [datetime] NULL,
	[BookFromDate] [datetime] NOT NULL,
	[BookToDate] [datetime] NOT NULL,
	[PackagesId] [uniqueidentifier] NOT NULL,
	[HotelPriceId] [uniqueidentifier] NOT NULL,
	[RoomCount] [smallint] NOT NULL,
	[RoomPrice] [decimal](18, 2) NOT NULL,
	[IsRateIncreaseByPer] [bit] NULL,
	[RateIncrease] [decimal](18, 2) NULL,
	[BookingPrice] [decimal](18, 2) NOT NULL,
	[PaidAmount] [decimal](18, 2) NULL,
	[GstAmount] [decimal](18, 2) NOT NULL,
	[GstPercent] [decimal](5, 2) NOT NULL,
	[IsCancelled] [bit] NOT NULL,
	[IsUnblocked] [bit] NOT NULL,
	[IsConfirmed] [bit] NOT NULL,
	[Salutation] [varchar](15) NOT NULL,
	[FirstName] [varchar](75) NOT NULL,
	[LastName] [varchar](75) NOT NULL,
	[Email] [varchar](125) NULL,
	[Mobile] [varchar](10) NOT NULL,
	[BillingAddress] [varchar](4000) NULL,
	[PinCode] [varchar](10) NULL,
	[City] [varchar](75) NULL,
	[CountryId] [int] NULL,
	[RoomRemark] [nvarchar](max) NULL,
	[WeekendPrice] [decimal](18, 2) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[Participants] [int] NOT NULL,
 CONSTRAINT [PK__HotelBoo__3214EC06BF447B7D] PRIMARY KEY NONCLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[HotelBooking] ADD  DEFAULT ('Hotel') FOR [BookingPackageType]
GO
ALTER TABLE [dbo].[HotelBooking] ADD  DEFAULT ((0)) FOR [PaidAmount]
GO
ALTER TABLE [dbo].[HotelBooking] ADD  CONSTRAINT [DF__HotelBook__IsCan__4959E263]  DEFAULT ((0)) FOR [IsCancelled]
GO
ALTER TABLE [dbo].[HotelBooking] ADD  CONSTRAINT [DF__HotelBook__IsUnb__4A4E069C]  DEFAULT ((0)) FOR [IsUnblocked]
GO
ALTER TABLE [dbo].[HotelBooking] ADD  CONSTRAINT [DF__HotelBook__IsCon__4B422AD5]  DEFAULT ((0)) FOR [IsConfirmed]
GO
ALTER TABLE [dbo].[HotelBooking] ADD  DEFAULT ((0)) FOR [WeekendPrice]
GO
ALTER TABLE [dbo].[HotelBooking] ADD  CONSTRAINT [DF__HotelBook__Creat__4C364F0E]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[HotelBooking] ADD  CONSTRAINT [DF__HotelBook__Updat__4D2A7347]  DEFAULT (getdate()) FOR [UpdatedDate]
GO
ALTER TABLE [dbo].[HotelBooking] ADD  DEFAULT ((0)) FOR [Participants]
GO
