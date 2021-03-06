USE [PRAWAITLIST]
GO
/****** Object:  Table [dbo].[ParentModel]    Script Date: 8/24/2017 8:19:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParentModel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FamilyID] [int] NOT NULL,
	[pType] [int] NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[EmailAddress] [nvarchar](max) NOT NULL,
	[Phone1] [nvarchar](max) NOT NULL,
	[Phone1Type] [int] NOT NULL,
	[Phone2] [nvarchar](max) NOT NULL,
	[Phone2Type] [int] NOT NULL,
	[Address1] [nvarchar](max) NOT NULL,
	[Address2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NOT NULL,
	[StateID] [nvarchar](3) NOT NULL,
	[ZipCode] [nvarchar](50) NOT NULL,
	[isPreferredContact] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserID] [nvarchar](max) NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.ParentModel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ParentModel] ADD  CONSTRAINT [DF_ParentModel_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[ParentModel] ADD  CONSTRAINT [DF_ParentModel_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
ALTER TABLE [dbo].[ParentModel] ADD  CONSTRAINT [DF_ParentModel_isActive]  DEFAULT ((1)) FOR [isActive]
GO
