USE [PRAWAITLIST]
GO
/****** Object:  Table [dbo].[ConfigurationSettingsModel]    Script Date: 8/24/2017 8:19:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConfigurationSettingsModel](
	[key] [nvarchar](128) NOT NULL,
	[value] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ConfigurationSettingsModel] PRIMARY KEY CLUSTERED 
(
	[key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
