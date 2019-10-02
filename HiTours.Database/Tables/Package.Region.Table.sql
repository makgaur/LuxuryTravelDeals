
CREATE TABLE [Package].[Region](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[PhoneCode] [varchar](10) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

GO
/****** Object:  Index [UNIX_Region_Name]    Script Date: 7/23/2018 3:16:06 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UNIX_Region_Name] ON [Package].[Region]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [Package].[Region] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [Package].[Region] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [Package].[Region] ADD  DEFAULT ((0)) FOR [CreatedBy]
GO
ALTER TABLE [Package].[Region] ADD  DEFAULT (getutcdate()) FOR [UpdatedDate]
GO
ALTER TABLE [Package].[Region] ADD  DEFAULT ((0)) FOR [UpdatedBy]
GO
