USE [master]
GO
/****** Object:  Database [Diploma-1parcial-2025]    Script Date: 9 may. 2025 09:18:15 p. m. ******/
CREATE DATABASE [Diploma-1parcial-2025]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Diploma-1parcial-2025', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Diploma-1parcial-2025.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Diploma-1parcial-2025_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\Diploma-1parcial-2025_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [Diploma-1parcial-2025] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Diploma-1parcial-2025].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Diploma-1parcial-2025] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET ARITHABORT OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET  MULTI_USER 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Diploma-1parcial-2025] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Diploma-1parcial-2025] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Diploma-1parcial-2025] SET QUERY_STORE = ON
GO
ALTER DATABASE [Diploma-1parcial-2025] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [Diploma-1parcial-2025]
GO
/****** Object:  Table [dbo].[Player]    Script Date: 9 may. 2025 09:18:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player](
	[PlayerId] [int] IDENTITY(1,1) NOT NULL,
	[NickName] [nvarchar](30) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PlayerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GameSession]    Script Date: 9 may. 2025 09:18:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GameSession](
	[SessionId] [int] IDENTITY(1,1) NOT NULL,
	[PlayerId] [int] NOT NULL,
	[StartedAt] [datetime] NOT NULL,
	[FinishedAt] [datetime] NULL,
	[FinalScore] [int] NULL,
	[FinalLives] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shot]    Script Date: 9 may. 2025 09:18:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shot](
	[ShotId] [bigint] IDENTITY(1,1) NOT NULL,
	[SessionId] [int] NOT NULL,
	[WeaponId] [tinyint] NOT NULL,
	[DistanceKm] [float] NOT NULL,
	[Hit] [bit] NOT NULL,
	[Lane] [tinyint] NOT NULL,
	[ShotTime] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ShotId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_PlayerStats]    Script Date: 9 may. 2025 09:18:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   VIEW [dbo].[vw_PlayerStats]
AS
SELECT  p.PlayerId,
        p.NickName,
        COUNT(DISTINCT s.SessionId)  AS GamesPlayed,
        SUM(CASE WHEN sh.Hit = 1 THEN 1 ELSE 0 END) AS TotalHits,
        COUNT(sh.ShotId)             AS TotalShots,
        CAST(100.0*SUM(CASE WHEN sh.Hit=1 THEN 1 ELSE 0 END)
/ NULLIF(COUNT(sh.ShotId),0) AS DECIMAL(5,2)) AS AccuracyPct
FROM   Player p
LEFT  JOIN GameSession s ON s.PlayerId = p.PlayerId
LEFT  JOIN Shot sh       ON sh.SessionId = s.SessionId
GROUP BY p.PlayerId, p.NickName;
GO
/****** Object:  Table [dbo].[Weapon]    Script Date: 9 may. 2025 09:18:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Weapon](
	[WeaponId] [tinyint] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[MinKm] [float] NOT NULL,
	[MaxKm] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[WeaponId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GameSession] ON 
GO
INSERT [dbo].[GameSession] ([SessionId], [PlayerId], [StartedAt], [FinishedAt], [FinalScore], [FinalLives]) VALUES (1, 1, CAST(N'2025-05-09T20:49:54.360' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[GameSession] ([SessionId], [PlayerId], [StartedAt], [FinishedAt], [FinalScore], [FinalLives]) VALUES (2, 2, CAST(N'2025-05-09T20:51:47.390' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[GameSession] ([SessionId], [PlayerId], [StartedAt], [FinishedAt], [FinalScore], [FinalLives]) VALUES (3, 2, CAST(N'2025-05-09T20:55:21.873' AS DateTime), CAST(N'2025-05-09T20:55:36.470' AS DateTime), 3, 0)
GO
INSERT [dbo].[GameSession] ([SessionId], [PlayerId], [StartedAt], [FinishedAt], [FinalScore], [FinalLives]) VALUES (4, 2, CAST(N'2025-05-09T20:55:45.020' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[GameSession] ([SessionId], [PlayerId], [StartedAt], [FinishedAt], [FinalScore], [FinalLives]) VALUES (5, 2, CAST(N'2025-05-09T21:05:56.317' AS DateTime), CAST(N'2025-05-09T21:06:51.377' AS DateTime), 30, 0)
GO
INSERT [dbo].[GameSession] ([SessionId], [PlayerId], [StartedAt], [FinishedAt], [FinalScore], [FinalLives]) VALUES (6, 2, CAST(N'2025-05-09T21:13:10.767' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[GameSession] ([SessionId], [PlayerId], [StartedAt], [FinishedAt], [FinalScore], [FinalLives]) VALUES (7, 1, CAST(N'2025-05-09T21:14:31.847' AS DateTime), NULL, NULL, NULL)
GO
INSERT [dbo].[GameSession] ([SessionId], [PlayerId], [StartedAt], [FinishedAt], [FinalScore], [FinalLives]) VALUES (8, 2, CAST(N'2025-05-09T21:17:09.147' AS DateTime), NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[GameSession] OFF
GO
SET IDENTITY_INSERT [dbo].[Player] ON 
GO
INSERT [dbo].[Player] ([PlayerId], [NickName], [CreatedAt]) VALUES (1, N'facu', CAST(N'2025-05-09T20:49:54.360' AS DateTime))
GO
INSERT [dbo].[Player] ([PlayerId], [NickName], [CreatedAt]) VALUES (2, N'fac', CAST(N'2025-05-09T20:51:47.387' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Player] OFF
GO
SET IDENTITY_INSERT [dbo].[Shot] ON 
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (3, 3, 3, 172.52124645892337, 1, 4, CAST(N'2025-05-09T20:55:25.760' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (4, 3, 3, 158.92351274787512, 1, 0, CAST(N'2025-05-09T20:55:27.510' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (5, 3, 3, 181.01983002832853, 1, 3, CAST(N'2025-05-09T20:55:28.590' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (6, 4, 3, 165.72237960339925, 1, 3, CAST(N'2025-05-09T20:55:48.983' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (7, 4, 3, 169.12181303116131, 1, 1, CAST(N'2025-05-09T20:55:50.420' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (8, 4, 3, 150.42492917846997, 1, 3, CAST(N'2025-05-09T20:55:52.247' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (9, 4, 2, 24.645892351274508, 1, 4, CAST(N'2025-05-09T20:55:56.063' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (10, 4, 3, 182.71954674220956, 1, 4, CAST(N'2025-05-09T20:55:56.167' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (11, 4, 1, 2.5495750708212448, 1, 3, CAST(N'2025-05-09T20:55:57.967' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (12, 4, 3, 162.32294617563719, 1, 3, CAST(N'2025-05-09T20:55:58.057' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (13, 5, 3, 153.82436260623203, 1, 2, CAST(N'2025-05-09T21:06:00.550' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (14, 5, 3, 99.433427762039273, 1, 0, CAST(N'2025-05-09T21:06:03.050' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (15, 5, 2, 22.946175637393488, 1, 0, CAST(N'2025-05-09T21:06:05.950' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (16, 5, 3, 97.733711048158256, 1, 0, CAST(N'2025-05-09T21:06:06.077' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (17, 5, 2, 16.147308781869405, 1, 4, CAST(N'2025-05-09T21:06:09.083' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (18, 5, 3, 92.6345609065152, 1, 4, CAST(N'2025-05-09T21:06:09.170' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (19, 5, 3, 70.538243626061984, 1, 4, CAST(N'2025-05-09T21:06:11.060' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (20, 5, 1, 7.6487252124643037, 1, 1, CAST(N'2025-05-09T21:06:13.740' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (21, 5, 2, 31.44475920679859, 1, 3, CAST(N'2025-05-09T21:06:14.790' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (22, 5, 3, 62.0396600566569, 1, 4, CAST(N'2025-05-09T21:06:15.740' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (23, 5, 3, 160.62322946175615, 1, 0, CAST(N'2025-05-09T21:06:16.917' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (24, 5, 3, 187.81869688385265, 1, 1, CAST(N'2025-05-09T21:06:17.907' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (25, 5, 2, 39.943342776203679, 1, 1, CAST(N'2025-05-09T21:06:22.140' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (26, 5, 2, 43.342776203965713, 1, 2, CAST(N'2025-05-09T21:06:23.573' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (27, 5, 2, 29.74504249291757, 1, 0, CAST(N'2025-05-09T21:06:25.330' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (28, 5, 2, 45.042492917846729, 1, 3, CAST(N'2025-05-09T21:06:26.537' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (29, 5, 3, 75.637393767705035, 1, 1, CAST(N'2025-05-09T21:06:27.493' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (30, 5, 2, 29.74504249291757, 1, 2, CAST(N'2025-05-09T21:06:32.813' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (31, 5, 3, 56.940509915013848, 1, 2, CAST(N'2025-05-09T21:06:33.813' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (32, 5, 3, 182.71954674220956, 1, 2, CAST(N'2025-05-09T21:06:34.517' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (33, 5, 3, 50.14164305948978, 1, 4, CAST(N'2025-05-09T21:06:35.460' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (34, 5, 3, 174.2209631728044, 1, 4, CAST(N'2025-05-09T21:06:36.180' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (35, 5, 3, 196.31728045325781, 1, 4, CAST(N'2025-05-09T21:06:40.267' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (36, 5, 3, 191.21813031161472, 1, 4, CAST(N'2025-05-09T21:06:43.363' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (37, 6, 2, 31.44475920679859, 1, 1, CAST(N'2025-05-09T21:13:17.257' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (38, 6, 3, 75.637393767705035, 1, 0, CAST(N'2025-05-09T21:13:17.940' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (39, 6, 3, 123.22946175637351, 1, 1, CAST(N'2025-05-09T21:13:18.563' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (40, 6, 3, 167.42209631728028, 1, 1, CAST(N'2025-05-09T21:13:19.260' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (41, 6, 3, 187.81869688385265, 1, 0, CAST(N'2025-05-09T21:13:20.380' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (42, 6, 3, 196.31728045325781, 1, 1, CAST(N'2025-05-09T21:13:21.730' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (43, 6, 3, 177.62039660056647, 1, 3, CAST(N'2025-05-09T21:13:23.560' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (44, 6, 3, 150.42492917846997, 1, 2, CAST(N'2025-05-09T21:13:25.570' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (45, 6, 3, 189.51841359773368, 1, 2, CAST(N'2025-05-09T21:13:26.347' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (46, 6, 3, 131.72804532577862, 1, 1, CAST(N'2025-05-09T21:13:28.923' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (47, 6, 3, 175.92067988668543, 1, 1, CAST(N'2025-05-09T21:13:29.610' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (48, 6, 3, 198.01699716713884, 1, 2, CAST(N'2025-05-09T21:13:30.690' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (49, 7, 3, 155.52407932011306, 1, 3, CAST(N'2025-05-09T21:14:36.050' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (50, 7, 2, 26.345609065155529, 1, 2, CAST(N'2025-05-09T21:14:39.930' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (51, 7, 3, 101.13314447592029, 1, 2, CAST(N'2025-05-09T21:14:40.053' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (52, 7, 3, 79.036827195467069, 1, 4, CAST(N'2025-05-09T21:14:41.970' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (53, 7, 3, 118.13031161473046, 1, 1, CAST(N'2025-05-09T21:14:42.730' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (54, 7, 3, 145.32577903682687, 1, 1, CAST(N'2025-05-09T21:14:43.747' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (55, 7, 3, 177.62039660056647, 1, 4, CAST(N'2025-05-09T21:14:44.643' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (56, 7, 3, 189.51841359773368, 1, 0, CAST(N'2025-05-09T21:14:45.917' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (57, 7, 3, 181.01983002832853, 1, 1, CAST(N'2025-05-09T21:14:47.587' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (58, 8, 3, 166.57223796033836, 1, 3, CAST(N'2025-05-09T21:17:20.547' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (59, 8, 3, 179.32011331444588, 1, 0, CAST(N'2025-05-09T21:17:22.613' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (60, 8, 3, 185.26912181302939, 1, 4, CAST(N'2025-05-09T21:17:23.800' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (61, 8, 3, 192.9178470254939, 1, 2, CAST(N'2025-05-09T21:17:24.810' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (62, 8, 3, 195.4674220963154, 1, 3, CAST(N'2025-05-09T21:17:26.113' AS DateTime))
GO
INSERT [dbo].[Shot] ([ShotId], [SessionId], [WeaponId], [DistanceKm], [Hit], [Lane], [ShotTime]) VALUES (63, 8, 1, 5.9490084985828373, 1, 1, CAST(N'2025-05-09T21:17:35.210' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Shot] OFF
GO
INSERT [dbo].[Weapon] ([WeaponId], [Name], [MinKm], [MaxKm]) VALUES (1, N'Cañón Corto', 0, 10)
GO
INSERT [dbo].[Weapon] ([WeaponId], [Name], [MinKm], [MaxKm]) VALUES (2, N'Cañón Ultrasónico', 10, 50)
GO
INSERT [dbo].[Weapon] ([WeaponId], [Name], [MinKm], [MaxKm]) VALUES (3, N'Láser Biónico', 50, 200)
GO
/****** Object:  Index [IX_Session_PlayerDate]    Script Date: 9 may. 2025 09:18:16 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Session_PlayerDate] ON [dbo].[GameSession]
(
	[PlayerId] ASC,
	[StartedAt] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Player__01E67C8B1020C6E0]    Script Date: 9 may. 2025 09:18:16 p. m. ******/
ALTER TABLE [dbo].[Player] ADD UNIQUE NONCLUSTERED 
(
	[NickName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Shot_Hit]    Script Date: 9 may. 2025 09:18:16 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Shot_Hit] ON [dbo].[Shot]
(
	[Hit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Shot_Session]    Script Date: 9 may. 2025 09:18:16 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_Shot_Session] ON [dbo].[Shot]
(
	[SessionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Weapon__737584F6FF32B971]    Script Date: 9 may. 2025 09:18:16 p. m. ******/
ALTER TABLE [dbo].[Weapon] ADD UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GameSession] ADD  DEFAULT (getdate()) FOR [StartedAt]
GO
ALTER TABLE [dbo].[Player] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Shot] ADD  DEFAULT (getdate()) FOR [ShotTime]
GO
ALTER TABLE [dbo].[GameSession]  WITH CHECK ADD FOREIGN KEY([PlayerId])
REFERENCES [dbo].[Player] ([PlayerId])
GO
ALTER TABLE [dbo].[Shot]  WITH CHECK ADD FOREIGN KEY([SessionId])
REFERENCES [dbo].[GameSession] ([SessionId])
GO
ALTER TABLE [dbo].[Shot]  WITH CHECK ADD FOREIGN KEY([WeaponId])
REFERENCES [dbo].[Weapon] ([WeaponId])
GO
/****** Object:  StoredProcedure [dbo].[spEnsurePlayer]    Script Date: 9 may. 2025 09:18:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- 5.1  Nuevo jugador (si no existe)
CREATE   PROCEDURE [dbo].[spEnsurePlayer]
    @Nick  NVARCHAR(30),
    @OutId INT OUTPUT
AS
SET NOCOUNT ON;
IF EXISTS(SELECT 1 FROM Player WHERE NickName = @Nick)
    SELECT @OutId = PlayerId FROM Player WHERE NickName = @Nick;
ELSE
BEGIN
    INSERT Player(NickName) VALUES(@Nick);
    SET @OutId = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[spFinishSession]    Script Date: 9 may. 2025 09:18:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 5.4  Cerrar sesión
CREATE   PROCEDURE [dbo].[spFinishSession]
    @SessionId   INT,
    @FinalScore  INT,
    @FinalLives  INT
AS
UPDATE GameSession
   SET FinishedAt = GETDATE(),
       FinalScore = @FinalScore,
       FinalLives = @FinalLives
 WHERE SessionId = @SessionId;
GO
/****** Object:  StoredProcedure [dbo].[spRecordShot]    Script Date: 9 may. 2025 09:18:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 5.3  Registrar disparo
CREATE   PROCEDURE [dbo].[spRecordShot]
    @SessionId  INT,
    @WeaponId   TINYINT,
    @Distance   FLOAT,
    @Hit        BIT,
    @Lane       TINYINT
AS
INSERT Shot(SessionId, WeaponId, DistanceKm, Hit, Lane)
VALUES(@SessionId,@WeaponId,@Distance,@Hit,@Lane);
GO
/****** Object:  StoredProcedure [dbo].[spStartSession]    Script Date: 9 may. 2025 09:18:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- 5.2  Iniciar sesión de juego
CREATE   PROCEDURE [dbo].[spStartSession]
    @PlayerId  INT,
    @OutSessId INT OUTPUT
AS
SET NOCOUNT ON;
INSERT GameSession(PlayerId) VALUES(@PlayerId);
SET @OutSessId = SCOPE_IDENTITY();
GO
USE [master]
GO
ALTER DATABASE [Diploma-1parcial-2025] SET  READ_WRITE 
GO
