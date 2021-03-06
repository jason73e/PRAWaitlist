USE [PRAWAITLIST]
GO
/****** Object:  Table [dbo].[SchoolYearModel]    Script Date: 8/24/2017 8:19:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchoolYearModel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StartYear] [int] NOT NULL,
	[EndYear] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.SchoolYearModel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
