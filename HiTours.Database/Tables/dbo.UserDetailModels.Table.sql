
CREATE TABLE [dbo].[UserDetailModels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[EmailId] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[MobileNo] [nvarchar](max) NULL,
	[Gender] [nvarchar](max) NULL,
	[DateOfBirth] [datetime] NULL,
	[NationalityId] [int] NULL,
	[Address] [nvarchar](max) NULL,
	[ZipCode] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[CountryId] [int] NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PassportNo] [nvarchar](max) NULL,
	[CountryofIssueId] [int] NULL,
	[ExpiryDate] [datetime] NULL,
	[DetailType] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UserDetailModels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
