
CREATE TABLE [dbo].[UserDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Gender] [varchar](10) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[EmailId] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NULL,
	[MobileNo] [nvarchar](20) NULL,
	[DateOfBirth] [datetime] NULL,
	[NationalityId] [int] NULL,
	[Address] [nvarchar](200) NULL,
	[ZipCode] [varchar](10) NULL,
	[City] [varchar](50) NULL,
	[CountryId] [int] NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[PassportNo] [nvarchar](50) NULL,
	[CountryofIssueId] [int] NULL,
	[ExpiryDate] [datetime] NULL,
	[DetailType] [varchar](50) NULL,
	[IsGuest] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_UserDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UC_Email] UNIQUE NONCLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserDetails] ADD  DEFAULT ((0)) FOR [IsGuest]
GO
ALTER TABLE [dbo].[UserDetails] ADD  CONSTRAINT [DF_UserDetail_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[UserDetails] ADD  CONSTRAINT [DF_UserDetail_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserDetails] ADD  CONSTRAINT [DF_UserDetail_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
