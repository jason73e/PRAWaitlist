USE [PRAWAITLIST]
GO
/****** Object:  Table [dbo].[EmailControl]    Script Date: 8/24/2017 8:19:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailControl](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[FromAddress] [varchar](255) NOT NULL,
	[SMTPDeliveryMethod] [varchar](50) NOT NULL,
	[SMTPHost] [varchar](255) NOT NULL,
	[SMTPPort] [int] NOT NULL,
	[SMTPUser] [varchar](255) NOT NULL,
	[SMTPPassword] [varchar](255) NOT NULL,
	[SMTPEnableSSL] [bit] NOT NULL,
	[SMTPSendLimit] [int] NOT NULL,
	[isActive] [bit] NOT NULL,
 CONSTRAINT [PK_EmailControl] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
