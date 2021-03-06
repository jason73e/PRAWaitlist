USE [PRAWAITLIST]
GO
/****** Object:  Table [dbo].[FamilyModel]    Script Date: 8/24/2017 8:19:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FamilyModel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FamilyName] [nvarchar](50) NOT NULL,
	[Address1] [nvarchar](150) NOT NULL,
	[Address2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NOT NULL,
	[StateID] [nvarchar](3) NOT NULL,
	[ZipCode] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserID] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.FamilyModel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[FamilyModel] ADD  CONSTRAINT [DF_FamilyModel_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[FamilyModel] ADD  CONSTRAINT [DF_FamilyModel_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
ALTER TABLE [dbo].[FamilyModel] ADD  CONSTRAINT [DF_FamilyModel_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
