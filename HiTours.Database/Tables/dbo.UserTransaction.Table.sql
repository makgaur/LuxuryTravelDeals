
CREATE TABLE [dbo].[UserTransaction](
	[RowNo] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[HotelBookingId] [uniqueidentifier] NULL,
	[Currency] [char](3) NOT NULL,
	[ReceiptNo] [varchar](20) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[OrderId] [varchar](64) NULL,
	[PaymentId] [varchar](64) NULL,
	[IsInternational] [bit] NOT NULL,
	[PaymentMethod] [varchar](20) NULL,
	[Description] [varchar](500) NULL,
	[CardId] [varchar](50) NULL,
	[Bank] [varchar](50) NULL,
	[WalletName] [varchar](50) NULL,
	[Email] [varchar](100) NULL,
	[ContactNo] [varchar](20) NULL,
	[Fee] [decimal](18, 2) NULL,
	[Tax] [decimal](18, 2) NULL,
	[ErrorCode] [varchar](20) NULL,
	[ErrorDescription] [varchar](500) NULL,
	[PaymentDate] [datetime] NULL,
	[PaymentStatus] [varchar](20) NULL,
	[OrderStatus] [varchar](20) NULL,
	[FlightDetail] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK__tmp_ms_x__FFEE5BA2548F2382] PRIMARY KEY CLUSTERED 
(
	[RowNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserTransaction] ADD  CONSTRAINT [DF__tmp_ms_xx__IsInt__1C5231C2]  DEFAULT ((0)) FOR [IsInternational]
GO
ALTER TABLE [dbo].[UserTransaction]  WITH CHECK ADD  CONSTRAINT [FK_UserTransaction_HotelBooking] FOREIGN KEY([HotelBookingId])
REFERENCES [dbo].[HotelBooking] ([Id])
GO
ALTER TABLE [dbo].[UserTransaction] CHECK CONSTRAINT [FK_UserTransaction_HotelBooking]
GO
ALTER TABLE [dbo].[UserTransaction]  WITH CHECK ADD  CONSTRAINT [FK_UserTransaction_UserDetails] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserDetails] ([Id])
GO
ALTER TABLE [dbo].[UserTransaction] CHECK CONSTRAINT [FK_UserTransaction_UserDetails]
GO
