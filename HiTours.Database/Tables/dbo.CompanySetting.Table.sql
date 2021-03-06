
CREATE TABLE [dbo].[CompanySetting](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[FlightMarkup] [decimal](5, 2) NULL,
	[CreatedDate] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedDate] [datetime2](7) NOT NULL,
	[UpdatedBy] [int] NOT NULL,
 CONSTRAINT [PK_CompanySetting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompanySetting] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[CompanySetting] ADD  DEFAULT (getutcdate()) FOR [UpdatedDate]
GO
