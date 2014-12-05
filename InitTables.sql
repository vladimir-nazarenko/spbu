GO
USE [m12nvv]
GO
/****** Object:  Table [dbo].[Figures]    Script Date: 02/21/2014 10:51:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF OBJECT_ID ('dbo.Chessboard') IS NOT NULL
	DROP TABLE dbo.Chessboard;
GO
IF OBJECT_ID ('dbo.Figures') IS NOT NULL
	DROP TABLE dbo.Figures;
GO

CREATE TABLE [dbo].[Figures](
	[cid] [int] NOT NULL,
	[type] [varchar](10) NULL,
	[color] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[cid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Chessboard]    Script Date: 02/21/2014 10:51:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Chessboard](
	[x] [int] NULL,
	[y] [char](1) NULL,
	[cid] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[cid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[x] ASC,
	[y] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Check [CK__Chessboard__x__37A5467C]    Script Date: 02/21/2014 10:51:29 ******/
ALTER TABLE [dbo].[Chessboard]  WITH CHECK ADD CHECK  (([x] like '[1-8]'))
GO
/****** Object:  Check [CK__Chessboard__y__38996AB5]    Script Date: 02/21/2014 10:51:29 ******/
ALTER TABLE [dbo].[Chessboard]  WITH CHECK ADD CHECK  (([y] like '[a-h]'))
GO
/****** Object:  Check [CK__Figures__color__276EDEB3]    Script Date: 02/21/2014 10:51:29 ******/
ALTER TABLE [dbo].[Figures]  WITH CHECK ADD CHECK  (([color]='w' OR [color]='b'))
GO
/****** Object:  Check [CK__Figures__type__286302EC]    Script Date: 02/21/2014 10:51:29 ******/
ALTER TABLE [dbo].[Figures]  WITH CHECK ADD CHECK  (([type]='pawn' OR [type]='knight' OR [type]='bishop' OR [type]='rock' OR [type]='queen' OR [type]='king'))
GO
/****** Object:  ForeignKey [FK__Chessboard__cid__36B12243]    Script Date: 02/21/2014 10:51:29 ******/
ALTER TABLE [dbo].[Chessboard]  WITH CHECK ADD FOREIGN KEY([cid])
REFERENCES [dbo].[Figures] ([cid])
GO
