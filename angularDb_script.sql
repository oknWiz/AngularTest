USE [master]
GO

/****** Object:  Database [MyFirstAngular]    Script Date: 31/8/2021 11:28:46 pm ******/
CREATE DATABASE [MyFirstAngular]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyFirstAngular', FILENAME = N'D:\work\db\MyFirstAngular.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MyFirstAngular_log', FILENAME = N'D:\work\db\MyFirstAngular_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [MyFirstAngular] SET COMPATIBILITY_LEVEL = 140
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyFirstAngular].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [MyFirstAngular] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [MyFirstAngular] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [MyFirstAngular] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [MyFirstAngular] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [MyFirstAngular] SET ARITHABORT OFF 
GO

ALTER DATABASE [MyFirstAngular] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [MyFirstAngular] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [MyFirstAngular] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [MyFirstAngular] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [MyFirstAngular] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [MyFirstAngular] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [MyFirstAngular] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [MyFirstAngular] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [MyFirstAngular] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [MyFirstAngular] SET  DISABLE_BROKER 
GO

ALTER DATABASE [MyFirstAngular] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [MyFirstAngular] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [MyFirstAngular] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [MyFirstAngular] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [MyFirstAngular] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [MyFirstAngular] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [MyFirstAngular] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [MyFirstAngular] SET RECOVERY FULL 
GO

ALTER DATABASE [MyFirstAngular] SET  MULTI_USER 
GO

ALTER DATABASE [MyFirstAngular] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [MyFirstAngular] SET DB_CHAINING OFF 
GO

ALTER DATABASE [MyFirstAngular] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [MyFirstAngular] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [MyFirstAngular] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [MyFirstAngular] SET QUERY_STORE = OFF
GO

USE [MyFirstAngular]
GO

ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO

ALTER DATABASE [MyFirstAngular] SET  READ_WRITE 
GO
------------------------------------------------------------------------------------------------

USE [MyFirstAngular]
GO
/****** Object:  Table [dbo].[syslogin]    Script Date: 31/8/2021 11:30:24 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[syslogin](
	[Id] [nvarchar](50) NOT NULL,
	[UserId] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[LoginTime] [datetime] NOT NULL,
	[LogoutTime] [datetime] NULL,
	[IsLogin] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedUser] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedUser] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_syslogin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sysroles]    Script Date: 31/8/2021 11:30:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysroles](
	[RoleId] [nvarchar](50) NOT NULL,
	[RoleCode] [nvarchar](50) NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedUser] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedUser] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_sysroles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[sysuser]    Script Date: 31/8/2021 11:30:25 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sysuser](
	[UserId] [nvarchar](50) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[RoleId] [nvarchar](50) NOT NULL,
	[Contact] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatedUser] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedUser] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_sysuser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TBL_Product]    Script Date: 31/8/2021 11:30:26 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TBL_Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Price] [float] NULL,
	[Description] [nvarchar](500) NULL,
	[Category] [nvarchar](50) NULL,
	[Image] [nvarchar](50) NULL,
	[IsDelete] [bit] NULL,
	[CreatedUser] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedUser] [nvarchar](50) NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_TBL_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[sysroles] ([RoleId], [RoleCode], [RoleName], [IsDelete], [CreatedUser], [CreatedDate], [UpdatedUser], [UpdatedDate]) VALUES (N'1', N'sysadmin', N'Administrator', 0, N'1', CAST(N'2019-11-05T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[sysroles] ([RoleId], [RoleCode], [RoleName], [IsDelete], [CreatedUser], [CreatedDate], [UpdatedUser], [UpdatedDate]) VALUES (N'4', N'user', N'User', 0, N'1', CAST(N'2021-06-15T15:38:15.000' AS DateTime), NULL, NULL)
INSERT [dbo].[sysuser] ([UserId], [FullName], [Username], [Password], [RoleId], [Contact], [Email], [IsDelete], [CreatedUser], [CreatedDate], [UpdatedUser], [UpdatedDate]) VALUES (N'1', N'System Admin', N'admin', N'9dIF2o1cIcQAx/Hc1JsQnw==', N'1', N'12345678', NULL, 0, N'admin', CAST(N'2021-08-28T01:48:31.000' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[TBL_Product] ON 

INSERT [dbo].[TBL_Product] ([ProductId], [Title], [Price], [Description], [Category], [Image], [IsDelete], [CreatedUser], [CreatedDate], [UpdatedUser], [UpdatedDate]) VALUES (1, N'Test1', 233, N'test description2', N'test ca3', NULL, 1, N'admin', CAST(N'2021-08-30T23:52:30.300' AS DateTime), N'admin', CAST(N'2021-08-31T23:05:58.620' AS DateTime))
INSERT [dbo].[TBL_Product] ([ProductId], [Title], [Price], [Description], [Category], [Image], [IsDelete], [CreatedUser], [CreatedDate], [UpdatedUser], [UpdatedDate]) VALUES (2, N'test2', 240.99, N'test description3', N'test33', NULL, 0, N'admin', CAST(N'2021-08-30T23:56:28.073' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[TBL_Product] OFF
