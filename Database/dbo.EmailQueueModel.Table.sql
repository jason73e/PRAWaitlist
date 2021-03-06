USE [PRAWAITLIST]
GO
/****** Object:  Table [dbo].[EmailQueueModel]    Script Date: 8/24/2017 8:19:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailQueueModel](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[QueueDate] [datetime] NOT NULL,
	[StatusModel] [nvarchar](max) NULL,
	[StatusDate] [datetime] NOT NULL,
	[RecipientCount] [int] NOT NULL,
	[MessageTo] [nvarchar](max) NULL,
	[MessageSubject] [nvarchar](max) NULL,
	[MessageCC] [nvarchar](max) NULL,
	[MessageBCC] [nvarchar](max) NULL,
	[MessageBody] [nvarchar](max) NULL,
	[MessageIsHtml] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.EmailQueueModel] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
