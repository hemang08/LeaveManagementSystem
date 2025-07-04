USE [master]
GO
/****** Object:  Database [LeaveManagementSystem]    Script Date: 22-06-2025 00:53:22 ******/
CREATE DATABASE [LeaveManagementSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LeaveManagementSystem', FILENAME = N'C:\Users\jabua\LeaveManagementSystem.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LeaveManagementSystem_log', FILENAME = N'C:\Users\jabua\LeaveManagementSystem_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [LeaveManagementSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LeaveManagementSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LeaveManagementSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [LeaveManagementSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LeaveManagementSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LeaveManagementSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET  ENABLE_BROKER 
GO
ALTER DATABASE [LeaveManagementSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LeaveManagementSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LeaveManagementSystem] SET  MULTI_USER 
GO
ALTER DATABASE [LeaveManagementSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LeaveManagementSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LeaveManagementSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LeaveManagementSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LeaveManagementSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LeaveManagementSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [LeaveManagementSystem] SET QUERY_STORE = OFF
GO
USE [LeaveManagementSystem]
GO
/****** Object:  Table [dbo].[LeaveRequest]    Script Date: 22-06-2025 00:53:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveRequest](
	[LeaveRequestId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [nvarchar](128) NOT NULL,
	[LeaveTypeId] [int] NOT NULL,
	[FromDate] [date] NOT NULL,
	[ToDate] [date] NOT NULL,
	[StatusId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[LeaveRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaveStatusMaster]    Script Date: 22-06-2025 00:53:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveStatusMaster](
	[LeaveStatusId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LeaveStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaveTypeMaster]    Script Date: 22-06-2025 00:53:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveTypeMaster](
	[LeaveTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LeaveTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[LeaveStatusMaster] ON 

INSERT [dbo].[LeaveStatusMaster] ([LeaveStatusId], [Name]) VALUES (1, N'Pending')
INSERT [dbo].[LeaveStatusMaster] ([LeaveStatusId], [Name]) VALUES (2, N'Approved')
INSERT [dbo].[LeaveStatusMaster] ([LeaveStatusId], [Name]) VALUES (3, N'Rejected')
INSERT [dbo].[LeaveStatusMaster] ([LeaveStatusId], [Name]) VALUES (4, N'Cancelled')
SET IDENTITY_INSERT [dbo].[LeaveStatusMaster] OFF
GO
SET IDENTITY_INSERT [dbo].[LeaveTypeMaster] ON 

INSERT [dbo].[LeaveTypeMaster] ([LeaveTypeId], [Name]) VALUES (1, N'Sick Leave')
INSERT [dbo].[LeaveTypeMaster] ([LeaveTypeId], [Name]) VALUES (2, N'Casual Leave')
INSERT [dbo].[LeaveTypeMaster] ([LeaveTypeId], [Name]) VALUES (3, N'Paid Leave')
INSERT [dbo].[LeaveTypeMaster] ([LeaveTypeId], [Name]) VALUES (4, N'Maternity Leave')
INSERT [dbo].[LeaveTypeMaster] ([LeaveTypeId], [Name]) VALUES (5, N'Paternity Leave')
SET IDENTITY_INSERT [dbo].[LeaveTypeMaster] OFF
GO
ALTER TABLE [dbo].[LeaveRequest] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[LeaveRequest] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[LeaveRequest]  WITH CHECK ADD FOREIGN KEY([LeaveTypeId])
REFERENCES [dbo].[LeaveTypeMaster] ([LeaveTypeId])
GO
ALTER TABLE [dbo].[LeaveRequest]  WITH CHECK ADD FOREIGN KEY([StatusId])
REFERENCES [dbo].[LeaveStatusMaster] ([LeaveStatusId])
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteLeaveRequest]    Script Date: 22-06-2025 00:53:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_DeleteLeaveRequest]
    @LeaveRequestId BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        UPDATE LeaveRequest
        SET IsDeleted = 1,
		ModifiedDate = GETUTCDATE()
        WHERE LeaveRequestId = @LeaveRequestId;

        SELECT 1 AS Success, 'Leave request deleted.' AS Message;
    END TRY
    BEGIN CATCH
        SELECT 0 AS Success, ERROR_MESSAGE() AS Message;
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetLeaveRequestById]    Script Date: 22-06-2025 00:53:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetLeaveRequestById]
    @LeaveRequestId BIGINT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT *
        FROM LeaveRequest
        WHERE LeaveRequestId = @LeaveRequestId AND IsDeleted = 0;

    END TRY
    BEGIN CATCH
        SELECT 0 AS Success, ERROR_MESSAGE() AS Message;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetLeaveRequestList]    Script Date: 22-06-2025 00:53:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetLeaveRequestList] 
    @Page INT,
    @PageSize INT,
    @SortColumn NVARCHAR(50),
    @SortDirection NVARCHAR(4)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        DECLARE @Offset INT = (@Page - 1) * @PageSize;
        DECLARE @TotalCount INT;

        -- Get total count
        SELECT @TotalCount = COUNT(*)
        FROM LeaveRequest
        WHERE IsDeleted = 0;

        -- Base query
        SELECT 
            @TotalCount AS TotalCount,
            LR.LeaveRequestId,
            LR.EmployeeName,
            LTM.Name AS LeaveType,
            LR.FromDate,
            LR.ToDate,
            LSM.Name AS Status
        FROM LeaveRequest LR
        INNER JOIN LeaveTypeMaster LTM ON LR.LeaveTypeId = LTM.LeaveTypeId
        INNER JOIN LeaveStatusMaster LSM ON LR.StatusId = LSM.LeaveStatusId
        WHERE LR.IsDeleted = 0
        ORDER BY
            CASE WHEN @SortColumn = 'EmployeeName' AND @SortDirection = 'ASC' THEN LR.EmployeeName END ASC,
            CASE WHEN @SortColumn = 'EmployeeName' AND @SortDirection = 'DESC' THEN LR.EmployeeName END DESC,
            CASE WHEN @SortColumn = 'LeaveType' AND @SortDirection = 'ASC' THEN LTM.Name END ASC,
            CASE WHEN @SortColumn = 'LeaveType' AND @SortDirection = 'DESC' THEN LTM.Name END DESC,
            CASE WHEN @SortColumn = 'Status' AND @SortDirection = 'ASC' THEN LSM.Name END ASC,
            CASE WHEN @SortColumn = 'Status' AND @SortDirection = 'DESC' THEN LSM.Name END DESC,
            CASE WHEN @SortColumn = 'FromDate' AND @SortDirection = 'ASC' THEN LR.FromDate END ASC,
            CASE WHEN @SortColumn = 'FromDate' AND @SortDirection = 'DESC' THEN LR.FromDate END DESC,
            CASE WHEN @SortColumn = 'ToDate' AND @SortDirection = 'ASC' THEN LR.ToDate END ASC,
            CASE WHEN @SortColumn = 'ToDate' AND @SortDirection = 'DESC' THEN LR.ToDate END DESC,
			CASE WHEN @SortColumn = '' AND @SortDirection = '' THEN LR.LeaveRequestId END DESC
        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

    END TRY
    BEGIN CATCH
        SELECT 0 AS Success, ERROR_MESSAGE() AS Message;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLeaveStatus]    Script Date: 22-06-2025 00:53:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SP_GetLeaveStatus]
AS
BEGIN
	SELECT LeaveStatusId,Name FROM LeaveStatusMaster
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetLeaveStatusList]    Script Date: 22-06-2025 00:53:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_GetLeaveStatusList]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT LeaveStatusId, Name FROM LeaveStatusMaster;
    END TRY
    BEGIN CATCH
        SELECT 'Error' AS Status, ERROR_MESSAGE() AS Message;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GetLeaveType]    Script Date: 22-06-2025 00:53:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_GetLeaveType]
AS
BEGIN
	SELECT LeaveTypeId,Name FROM LeaveTypeMaster
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetLeaveTypeList]    Script Date: 22-06-2025 00:53:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetLeaveTypeList]
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT LeaveTypeId, Name FROM LeaveTypeMaster;
    END TRY
    BEGIN CATCH
        SELECT 'Error' AS Status, ERROR_MESSAGE() AS Message;
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[sp_SaveLeaveRequest]    Script Date: 22-06-2025 00:53:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_SaveLeaveRequest]
    @LeaveRequestId BIGINT = NULL,
    @EmployeeName NVARCHAR(128),
    @LeaveTypeId INT,
    @FromDate DATE,
    @ToDate DATE,
    @StatusId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF @LeaveRequestId IS NULL OR @LeaveRequestId = 0
        BEGIN
            INSERT INTO LeaveRequest (
                EmployeeName,
                LeaveTypeId,
                FromDate,
                ToDate,
                StatusId,
                CreatedDate,
                IsDeleted
            )
            VALUES (
                @EmployeeName,
                @LeaveTypeId,
                @FromDate,
                @ToDate,
                @StatusId,
                GETUTCDATE(),
                0
            );

            SELECT 1 AS Success,'Leave request created successfully.' AS [Message];
        END
        ELSE
        BEGIN
            IF EXISTS (SELECT 1 FROM LeaveRequest WHERE LeaveRequestId = @LeaveRequestId AND IsDeleted = 0)
            BEGIN
                UPDATE LeaveRequest
                SET 
                    EmployeeName = @EmployeeName,
                    LeaveTypeId = @LeaveTypeId,
                    FromDate = @FromDate,
                    ToDate = @ToDate,
                    StatusId = @StatusId,
                    ModifiedDate = GETUTCDATE()
                WHERE LeaveRequestId = @LeaveRequestId;

                SELECT 1 AS Success,'Leave request updated successfully.' AS [Message];
            END
            ELSE
            BEGIN
                SELECT 0 AS Success, 'Leave request not found or has been deleted.' AS [Message];
            END
        END
    END TRY
    BEGIN CATCH
        SELECT 0 AS Success, ERROR_MESSAGE() AS [Message];
    END CATCH
END;
GO
USE [master]
GO
ALTER DATABASE [LeaveManagementSystem] SET  READ_WRITE 
GO
