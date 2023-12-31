USE [master]
GO
/****** Object:  Database [SDS_Exam]    Script Date: 07/09/2023 4:19:52 pm ******/
CREATE DATABASE [SDS_Exam]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SDS_Exam', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SDS_Exam.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SDS_Exam_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SDS_Exam_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SDS_Exam] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SDS_Exam].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SDS_Exam] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SDS_Exam] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SDS_Exam] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SDS_Exam] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SDS_Exam] SET ARITHABORT OFF 
GO
ALTER DATABASE [SDS_Exam] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SDS_Exam] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SDS_Exam] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SDS_Exam] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SDS_Exam] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SDS_Exam] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SDS_Exam] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SDS_Exam] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SDS_Exam] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SDS_Exam] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SDS_Exam] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SDS_Exam] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SDS_Exam] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SDS_Exam] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SDS_Exam] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SDS_Exam] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SDS_Exam] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SDS_Exam] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SDS_Exam] SET  MULTI_USER 
GO
ALTER DATABASE [SDS_Exam] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SDS_Exam] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SDS_Exam] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SDS_Exam] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SDS_Exam] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SDS_Exam] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SDS_Exam] SET QUERY_STORE = ON
GO
ALTER DATABASE [SDS_Exam] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SDS_Exam]
GO
/****** Object:  Table [dbo].[Recyclable_Item]    Script Date: 07/09/2023 4:19:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recyclable_Item](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RecyclableTypeId] [int] NOT NULL,
	[Weight] [decimal](10, 2) NOT NULL,
	[ComputedRate] [decimal](10, 2) NOT NULL,
	[ItemDescription] [nvarchar](150) NULL,
 CONSTRAINT [PK_RecyclableItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recyclable_Type]    Script Date: 07/09/2023 4:19:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recyclable_Type](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](100) NOT NULL,
	[Rate] [decimal](10, 2) NOT NULL,
	[MinKg] [decimal](10, 2) NOT NULL,
	[MaxKg] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_Recyclable_Type] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteRecyclableItem]    Script Date: 07/09/2023 4:19:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteRecyclableItem]
(
	@Id int, 
	@ReturnMessage VARCHAR(50) OUTPUT
)
AS
BEGIN

	BEGIN TRY
		BEGIN TRANSACTION
			DELETE FROM dbo.Recyclable_Item
				WHERE Id = @Id
		COMMIT TRANSACTION
		SET @ReturnMessage = 'Product deleted successfully.'
	END TRY

	BEGIN CATCH
		ROLLBACK
		SET @ReturnMessage = ERROR_MESSAGE()
	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteRecyclableType]    Script Date: 07/09/2023 4:19:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteRecyclableType]
(
	@Id int, 
	@ReturnMessage VARCHAR(50) OUTPUT
)
AS
BEGIN

	BEGIN TRY
		BEGIN TRANSACTION
			DELETE FROM dbo.Recyclable_Type
				WHERE Id = @Id
		COMMIT TRANSACTION
		SET @ReturnMessage = 'Product deleted successfully.'
	END TRY

	BEGIN CATCH
		ROLLBACK
		SET @ReturnMessage = ERROR_MESSAGE()
	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllRecyclableItems]    Script Date: 07/09/2023 4:19:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllRecyclableItems]
AS
BEGIN
	SELECT Id, RecyclableTypeId, Weight, ComputedRate, ItemDescription FROM dbo.Recyclable_Item WITH(NOLOCK)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllRecyclableTypes]    Script Date: 07/09/2023 4:19:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllRecyclableTypes]
AS
BEGIN
	SELECT Id, Type, Rate, MinKg, MaxKg FROM dbo.Recyclable_Type WITH(NOLOCK)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetOptionsOfRecyclableTypes]    Script Date: 07/09/2023 4:19:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetOptionsOfRecyclableTypes]
AS
BEGIN
	SELECT Id, Type, Rate FROM dbo.Recyclable_Type WITH(NOLOCK)
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetRecyclableItemById]    Script Date: 07/09/2023 4:19:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetRecyclableItemById]
(
@Id int
)
AS
BEGIN
	SELECT Id, RecyclableTypeId, Weight, ComputedRate, ItemDescription FROM dbo.Recyclable_Item
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetRecyclableTypeById]    Script Date: 07/09/2023 4:19:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetRecyclableTypeById]
(
@Id int
)
AS
BEGIN
	SELECT Id, Type, Rate, MinKg, MaxKg FROM dbo.Recyclable_Type
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertRecyclableItem]    Script Date: 07/09/2023 4:19:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertRecyclableItem]
(
	@RecyclableTypeId int,
	@Weight decimal(10,2),
	@ComputedRate decimal(10,2),
	@ItemDescription nvarchar(150) = NULL
)
AS
BEGIN

	BEGIN TRY
		BEGIN TRAN
			INSERT INTO dbo.Recyclable_Item(RecyclableTypeId, Weight, ComputedRate, ItemDescription)
			VALUES(@RecyclableTypeId, @Weight, @ComputedRate, @ItemDescription)
		COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE()
	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertRecyclableType]    Script Date: 07/09/2023 4:19:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertRecyclableType]
(
	@Type varchar(100),
	@Rate decimal(10,2),
	@MinKg decimal(10,2),
	@MaxKg decimal(10,2)
)
AS
BEGIN

	BEGIN TRY
		BEGIN TRAN
			INSERT INTO dbo.Recyclable_Type(Type, Rate, MinKg, MaxKg)
			VALUES(@Type, @Rate, @MinKg, @MaxKg)
		COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE()
	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateRecyclableItem]    Script Date: 07/09/2023 4:19:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateRecyclableItem]
(
	@Id int,
	@RecyclableTypeId int,
	@Weight decimal(10,2),
	@ComputedRate decimal(10,2),
	@ItemDescription nvarchar(150) = NULL
)
AS
BEGIN

	BEGIN TRY
		BEGIN TRAN
			UPDATE dbo.Recyclable_Item
			SET RecyclableTypeId= @RecyclableTypeId, 
				Weight			= @Weight, 
				ComputedRate	= @ComputedRate, 
				ItemDescription	= @ItemDescription
			WHERE Id = @Id
		COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE()
	END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateRecyclableType]    Script Date: 07/09/2023 4:19:52 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateRecyclableType]
(
	@Id int,
	@Type varchar(100),
	@Rate decimal(10,2),
	@MinKg decimal(10,2),
	@MaxKg decimal(10,2)
)
AS
BEGIN

	BEGIN TRY
		BEGIN TRAN
			UPDATE dbo.Recyclable_Type
			SET Type	= @Type, 
				Rate	= @Rate, 
				MinKg	= @MinKg, 
				MaxKg	= @MaxKg
			WHERE Id = @Id
		COMMIT TRAN
	END TRY

	BEGIN CATCH
		ROLLBACK TRAN
		SELECT ERROR_MESSAGE()
	END CATCH

END
GO
USE [master]
GO
ALTER DATABASE [SDS_Exam] SET  READ_WRITE 
GO
