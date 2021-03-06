
CREATE TABLE [dbo].[FlightDetail](
	[Id] [int] NOT NULL,
	[AirportName] [nvarchar](150) NULL,
	[AiportCode] [nvarchar](150) NULL,
	[CityName] [nvarchar](150) NULL,
	[CityCode] [nvarchar](150) NULL,
	[Countryname] [nvarchar](150) NULL,
	[CountryCode] [nvarchar](150) NULL,
	[Nationalty] [nvarchar](150) NULL,
	[Currency] [nvarchar](150) NULL,
 CONSTRAINT [PK_FlightDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
