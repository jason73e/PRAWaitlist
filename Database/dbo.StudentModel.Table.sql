USE [PRAWAITLIST]
GO
/****** Object:  Table [dbo].[StudentModel]    Script Date: 8/24/2017 8:19:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentModel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FamilyID] [int] NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[BirthDate] [datetime] NOT NULL,
	[Gender] [nvarchar](max) NOT NULL,
	[CurrentGrade] [int] NOT NULL,
	[ApplyGrade] [int] NOT NULL,
	[ApplyYear] [nvarchar](max) NOT NULL,
	[LocalSchool] [nvarchar](max) NOT NULL,
	[LocalDistrict] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NULL,
	[LearnAboutPRA] [nvarchar](max) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserID] [nvarchar](max) NULL,
	[isActive] [bit] NOT NULL,
	[isPRASibling] [bit] NOT NULL,
	[UStudentID] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.StudentModel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[StudentModel] ADD  CONSTRAINT [DF_StudentModel_CurrentGrade]  DEFAULT ((0)) FOR [CurrentGrade]
GO
ALTER TABLE [dbo].[StudentModel] ADD  CONSTRAINT [DF_StudentModel_ApplyGrade]  DEFAULT ((0)) FOR [ApplyGrade]
GO
ALTER TABLE [dbo].[StudentModel] ADD  CONSTRAINT [DF_StudentModel_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[StudentModel] ADD  CONSTRAINT [DF_StudentModel_UpdateDate]  DEFAULT (getdate()) FOR [UpdateDate]
GO
ALTER TABLE [dbo].[StudentModel] ADD  CONSTRAINT [DF_StudentModel_isActive]  DEFAULT ((1)) FOR [isActive]
GO
ALTER TABLE [dbo].[StudentModel] ADD  DEFAULT ((0)) FOR [isPRASibling]
GO
