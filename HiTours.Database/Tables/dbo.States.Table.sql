
CREATE TABLE [dbo].[States](
	[StateId] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[StateName] [nvarchar](200) NULL,
	[StateCode] [nvarchar](10) NULL,
	[LuxuryTax] [float] NULL,
	[Hotelvat] [float] NULL,
	[MealServiceCharges] [float] NULL,
	[Mealvat] [float] NULL,
	[IsHotelTaxOnPublishRate] [bit] NULL,
	[DVAT] [float] NULL,
	[IsNotIndianHotel] [bit] NULL,
	[TransportTax] [float] NULL,
	[CountryId] [uniqueidentifier] NULL,
	[HotelOtherTax] [float] NULL,
	[MealOtherTax] [float] NULL,
	[Code] [tinyint] NULL,
 CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[States] ADD  CONSTRAINT [DF_States_StateId]  DEFAULT (newid()) FOR [StateId]
GO
ALTER TABLE [dbo].[States] ADD  CONSTRAINT [DF_States_LuxuryTax]  DEFAULT ((0)) FOR [LuxuryTax]
GO
ALTER TABLE [dbo].[States] ADD  CONSTRAINT [DF_States_Hotelvat]  DEFAULT ((0)) FOR [Hotelvat]
GO
ALTER TABLE [dbo].[States] ADD  CONSTRAINT [DF_States_MealServiceCharges]  DEFAULT ((0)) FOR [MealServiceCharges]
GO
ALTER TABLE [dbo].[States] ADD  CONSTRAINT [DF_States_Mealvat]  DEFAULT ((0)) FOR [Mealvat]
GO
ALTER TABLE [dbo].[States] ADD  CONSTRAINT [DF_States_IsHotelTaxOnPublishRate]  DEFAULT ((0)) FOR [IsHotelTaxOnPublishRate]
GO
ALTER TABLE [dbo].[States] ADD  CONSTRAINT [DF_States_DVAT]  DEFAULT ((0)) FOR [DVAT]
GO
ALTER TABLE [dbo].[States] ADD  CONSTRAINT [DF_States_IsNotIndianHotel]  DEFAULT ((0)) FOR [IsNotIndianHotel]
GO
