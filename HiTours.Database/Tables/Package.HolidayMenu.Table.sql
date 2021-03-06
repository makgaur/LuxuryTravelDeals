
CREATE TABLE [Package].[HolidayMenu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IsRegion] [bit] NULL,
	[ChildMenu] [nvarchar](200) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_HolidayMenu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [Package].[HolidayMenu] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [Package].[HolidayMenu] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [Package].[HolidayMenu] ADD  DEFAULT ((0)) FOR [CreatedBy]
GO
ALTER TABLE [Package].[HolidayMenu] ADD  DEFAULT (getutcdate()) FOR [UpdatedDate]
GO
ALTER TABLE [Package].[HolidayMenu] ADD  DEFAULT ((0)) FOR [UpdatedBy]
GO
