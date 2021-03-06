USE [APITestDatabase]
GO
/****** Object:  Table [dbo].[UserAccount]    Script Date: 2/25/2021 7:21:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccount](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserAccount] ON 

INSERT [dbo].[UserAccount] ([UserId], [Name], [Email], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (1, N'rana', N'rana@gmail.com', N'123', NULL, NULL, NULL, NULL)
INSERT [dbo].[UserAccount] ([UserId], [Name], [Email], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (3, N'rina', N'rina@gmail.com', N'123', NULL, NULL, NULL, NULL)
INSERT [dbo].[UserAccount] ([UserId], [Name], [Email], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (4, N't', N't@gmail.com', N'123', NULL, NULL, NULL, NULL)
INSERT [dbo].[UserAccount] ([UserId], [Name], [Email], [Password], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (5, N'Rohim', N'rohim@gmail.com', N'123', NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserAccount] OFF
GO
