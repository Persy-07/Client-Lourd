USE [master]
GO
/****** Object:  Database [employeDB]    Script Date: 27/03/2024 13:29:28 ******/
CREATE DATABASE [employeDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'employeDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\employeDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'employeDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\employeDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [employeDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [employeDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [employeDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [employeDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [employeDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [employeDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [employeDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [employeDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [employeDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [employeDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [employeDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [employeDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [employeDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [employeDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [employeDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [employeDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [employeDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [employeDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [employeDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [employeDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [employeDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [employeDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [employeDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [employeDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [employeDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [employeDB] SET  MULTI_USER 
GO
ALTER DATABASE [employeDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [employeDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [employeDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [employeDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [employeDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [employeDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [employeDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [employeDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [employeDB]
GO
/****** Object:  Table [dbo].[employes]    Script Date: 27/03/2024 13:29:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[employes](
	[id_employe] [varchar](255) NOT NULL,
	[nom_complet] [nvarchar](255) NOT NULL,
	[genre] [nvarchar](50) NULL,
	[numero_contact] [nvarchar](255) NULL,
	[poste] [nvarchar](255) NULL,
	[image] [nvarchar](max) NULL,
	[salaire] [decimal](10, 2) NULL,
	[date_insertion] [date] NULL,
	[date_mise_a_jour] [date] NULL,
	[date_suppression] [date] NULL,
	[statut] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id_employe] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[utilisateurs]    Script Date: 27/03/2024 13:29:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[utilisateurs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nom_utilisateur] [nvarchar](255) NOT NULL,
	[mot_de_passe] [nvarchar](255) NOT NULL,
	[date_enregistrement] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[employes] ([id_employe], [nom_complet], [genre], [numero_contact], [poste], [image], [salaire], [date_insertion], [date_mise_a_jour], [date_suppression], [statut]) VALUES (N'09876', N'MARWA NACIM', N'Femme', N'098765765', N'Conception UI/UX', N'C:\Users\christian Ondiyo\source\repos\GestionDeEmployer\GestionDeEmployer\GestionDeEmployer\Directory\09876.jpg', CAST(0.00 AS Decimal(10, 2)), CAST(N'2024-03-25' AS Date), NULL, NULL, N'Inactif')
INSERT [dbo].[employes] ([id_employe], [nom_complet], [genre], [numero_contact], [poste], [image], [salaire], [date_insertion], [date_mise_a_jour], [date_suppression], [statut]) VALUES (N'343434', N'Remy Lionel', N'Homme', N'8765465677', N'Back-End Developer', N'C:\Users\christian Ondiyo\source\repos\GestionDeEmployer\GestionDeEmployer\GestionDeEmployer\Directory\343434.jpg', CAST(10000.00 AS Decimal(10, 2)), CAST(N'2024-03-23' AS Date), CAST(N'2024-03-23' AS Date), CAST(N'2024-03-25' AS Date), N'Actif')
INSERT [dbo].[employes] ([id_employe], [nom_complet], [genre], [numero_contact], [poste], [image], [salaire], [date_insertion], [date_mise_a_jour], [date_suppression], [statut]) VALUES (N'5454', N'Christian Ondiyo', N'Homme', N'0758982621', N'Développeur back-end', N'C:\Users\christian Ondiyo\source\repos\GestionDeEmployer\GestionDeEmployer\GestionDeEmployer\Directory\5454.jpg', CAST(1000000.00 AS Decimal(10, 2)), CAST(N'2024-03-25' AS Date), CAST(N'2024-03-26' AS Date), NULL, N'Actif')
INSERT [dbo].[employes] ([id_employe], [nom_complet], [genre], [numero_contact], [poste], [image], [salaire], [date_insertion], [date_mise_a_jour], [date_suppression], [statut]) VALUES (N'654656', N'Perside  Menayame', N'Femme', N'098765457', N'Développeur frontal', N'C:\Users\christian Ondiyo\source\repos\GestionDeEmployer\GestionDeEmployer\GestionDeEmployer\Directory\654656.jpg', CAST(1000000.00 AS Decimal(10, 2)), CAST(N'2024-03-25' AS Date), CAST(N'2024-03-25' AS Date), NULL, N'Actif')
INSERT [dbo].[employes] ([id_employe], [nom_complet], [genre], [numero_contact], [poste], [image], [salaire], [date_insertion], [date_mise_a_jour], [date_suppression], [statut]) VALUES (N'9876', N'Dieu Merci', N'Homme', N'098765678', N'Back-End Developer', N'C:\Users\christian Ondiyo\source\repos\GestionDeEmployer\GestionDeEmployer\GestionDeEmployer\Directory\9876.jpg', CAST(123000.00 AS Decimal(10, 2)), CAST(N'2024-03-23' AS Date), CAST(N'2024-03-23' AS Date), CAST(N'2024-03-25' AS Date), N'Actif')
GO
SET IDENTITY_INSERT [dbo].[utilisateurs] ON 

INSERT [dbo].[utilisateurs] ([id], [nom_utilisateur], [mot_de_passe], [date_enregistrement]) VALUES (1, N'admin', N'123456', CAST(N'2024-03-23' AS Date))
INSERT [dbo].[utilisateurs] ([id], [nom_utilisateur], [mot_de_passe], [date_enregistrement]) VALUES (2, N'christ', N'1234567', CAST(N'2024-03-24' AS Date))
SET IDENTITY_INSERT [dbo].[utilisateurs] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__utilisat__8EE745747438E855]    Script Date: 27/03/2024 13:29:28 ******/
ALTER TABLE [dbo].[utilisateurs] ADD UNIQUE NONCLUSTERED 
(
	[nom_utilisateur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [employeDB] SET  READ_WRITE 
GO
