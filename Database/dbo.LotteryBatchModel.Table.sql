USE [PRAWAITLIST]
GO
/****** Object:  Table [dbo].[LotteryBatchModel]    Script Date: 8/24/2017 8:19:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LotteryBatchModel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BatchName] [nvarchar](max) NOT NULL,
	[SchoolYearID] [int] NOT NULL,
	[BatchType] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
	[UpdateUserID] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.LotteryBatchModel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
