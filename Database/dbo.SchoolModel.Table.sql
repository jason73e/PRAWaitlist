USE [PRAWAITLIST]
GO
/****** Object:  Table [dbo].[SchoolModel]    Script Date: 8/24/2017 8:19:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SchoolModel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolName] [nvarchar](max) NOT NULL,
	[StateName] [nvarchar](max) NOT NULL,
	[StateAbbr] [nvarchar](max) NOT NULL,
	[SchoolID] [nvarchar](max) NOT NULL,
	[AgencyName] [nvarchar](max) NOT NULL,
	[AgencyID] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.SchoolModel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
