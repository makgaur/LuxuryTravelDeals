
CREATE TABLE [dbo].[HotelTerms](
	[HotelTermsId] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[SourceId] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[EntryType] [nvarchar](100) NULL,
	[SequenceNumber] [smallint] NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[SpecialRate] [float] NULL,
	[Type] [nvarchar](30) NULL,
 CONSTRAINT [PK_HotelTerms] PRIMARY KEY CLUSTERED 
(
	[HotelTermsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[HotelTerms] ADD  CONSTRAINT [DF_HotelTerms_HotelTermsId]  DEFAULT (newid()) FOR [HotelTermsId]
GO
ALTER TABLE [dbo].[HotelTerms] ADD  CONSTRAINT [DF_HotelTerms_SpecialRate]  DEFAULT ((0)) FOR [SpecialRate]
GO
