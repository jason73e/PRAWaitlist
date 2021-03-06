USE [PRAWAITLIST]
GO
/****** Object:  Table [dbo].[LotteryModel]    Script Date: 8/24/2017 8:19:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LotteryModel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LotteryBatchId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	[RandomID] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdateUserID] [nvarchar](max) NULL,
	[NotifyDate] [datetime] NULL,
	[AcceptDate] [datetime] NULL,
	[DeclineDate] [datetime] NULL,
	[Notes] [nvarchar](max) NULL,
	[Status] [nvarchar](max) NULL,
	[ApplyYear] [nvarchar](max) NULL,
	[ApplyGrade] [int] NULL,
 CONSTRAINT [PK_dbo.LotteryModel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
