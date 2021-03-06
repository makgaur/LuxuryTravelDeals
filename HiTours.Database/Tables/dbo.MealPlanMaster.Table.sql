
CREATE TABLE [dbo].[MealPlanMaster](
	[MealPlanId] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[MealPlan] [nvarchar](10) NULL,
	[Breakfast] [bit] NULL,
	[Lunch] [bit] NULL,
	[Dinner] [bit] NULL,
	[Description] [nvarchar](500) NULL,
	[SequenceNumber] [smallint] NULL,
	[IsTaxInclude] [bit] NULL,
 CONSTRAINT [PK_MealPlanMaster] PRIMARY KEY CLUSTERED 
(
	[MealPlanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MealPlanMaster] ADD  CONSTRAINT [DF_MealPlanMaster_MealPlanId]  DEFAULT (newid()) FOR [MealPlanId]
GO
ALTER TABLE [dbo].[MealPlanMaster] ADD  CONSTRAINT [DF_MealPlanMaster_Breakfast]  DEFAULT ((0)) FOR [Breakfast]
GO
ALTER TABLE [dbo].[MealPlanMaster] ADD  CONSTRAINT [DF_MealPlanMaster_Lunch]  DEFAULT ((0)) FOR [Lunch]
GO
ALTER TABLE [dbo].[MealPlanMaster] ADD  CONSTRAINT [DF_MealPlanMaster_Dinner]  DEFAULT ((0)) FOR [Dinner]
GO
