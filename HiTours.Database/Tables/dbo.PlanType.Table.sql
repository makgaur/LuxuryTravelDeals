
CREATE TABLE [dbo].[PlanType](
	[PlanTypeId] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[PlanType] [nvarchar](50) NULL,
	[Color] [int] NULL,
 CONSTRAINT [PK_PlanType] PRIMARY KEY CLUSTERED 
(
	[PlanTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PlanType] ADD  CONSTRAINT [DF_PlanType_PlanTypeId]  DEFAULT (newid()) FOR [PlanTypeId]
GO
