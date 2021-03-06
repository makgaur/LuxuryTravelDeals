
CREATE TABLE [Package].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateId] [int] NULL,
	[Code] [varchar](6) NULL,
	[CountryId] [smallint] NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[ShortDetail] [varchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[CityDescription] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [int] NULL,
	[CityCode] [varchar](6) NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Index [IX_City_CountryId]    Script Date: 7/23/2018 3:16:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_City_CountryId] ON [Package].[City]
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_City_StateId]    Script Date: 7/23/2018 3:16:06 PM ******/
CREATE NONCLUSTERED INDEX [IX_City_StateId] ON [Package].[City]
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

GO
/****** Object:  Index [packagecity_Countryid]    Script Date: 7/23/2018 3:16:06 PM ******/
CREATE NONCLUSTERED INDEX [packagecity_Countryid] ON [Package].[City]
(
	[CountryId] ASC
)
INCLUDE ( 	[Id],
	[Code],
	[Name],
	[ShortDetail],
	[Description]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [Package].[City] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [Package].[City] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [Package].[City] ADD  DEFAULT ((0)) FOR [CreatedBy]
GO
ALTER TABLE [Package].[City] ADD  DEFAULT (getutcdate()) FOR [UpdatedDate]
GO
ALTER TABLE [Package].[City] ADD  DEFAULT ((0)) FOR [UpdatedBy]
GO
ALTER TABLE [Package].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_State] FOREIGN KEY([StateId])
REFERENCES [Package].[State] ([Id])
GO
ALTER TABLE [Package].[City] CHECK CONSTRAINT [FK_City_State]
GO
ALTER TABLE [Package].[City]  WITH CHECK ADD  CONSTRAINT [FK_PackageCity_Country] FOREIGN KEY([CountryId])
REFERENCES [Package].[Country] ([Id])
GO
ALTER TABLE [Package].[City] CHECK CONSTRAINT [FK_PackageCity_Country]
GO
