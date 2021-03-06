
CREATE TABLE [dbo].[PackageImages](
	[Id] [uniqueidentifier] NOT NULL,
	[PackageId] [uniqueidentifier] NOT NULL,
	[ImageName] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[SequenceNo] [smallint] NULL,
 CONSTRAINT [PK_PackageImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PackageImages]  WITH CHECK ADD  CONSTRAINT [FK_PackageImages_Packages] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Packages] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PackageImages] CHECK CONSTRAINT [FK_PackageImages_Packages]
GO
