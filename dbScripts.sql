CREATE DATABASE [dbPetWeb]
GO

USE [dbPetWeb]
GO
/****** Object:  Table [dbo].[Pet]    Script Date: 5/11/2023 12:42:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pet](
	[PetId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_Pet] PRIMARY KEY CLUSTERED 
(
	[PetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Pet] ON 

INSERT [dbo].[Pet] ([PetId], [Name], [Type]) VALUES (1, N'Mascota 1', 1)
INSERT [dbo].[Pet] ([PetId], [Name], [Type]) VALUES (2, N'Mascota 2', 1)
INSERT [dbo].[Pet] ([PetId], [Name], [Type]) VALUES (3, N'Mascota 3', 1)
INSERT [dbo].[Pet] ([PetId], [Name], [Type]) VALUES (4, N'Mascota 4', 1)
INSERT [dbo].[Pet] ([PetId], [Name], [Type]) VALUES (5, N'Mascota 5', 1)

SET IDENTITY_INSERT [dbo].[Pet] OFF
GO
ALTER TABLE [dbo].[Pet] ADD  CONSTRAINT [DF_Pet_Type]  DEFAULT ((0)) FOR [Type]
GO
/****** Object:  StoredProcedure [dbo].[sp_Pet_Delete]    Script Date: 5/11/2023 12:42:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Pet_Delete] 
@PetId Int,
@Success Int Output
AS
BEGIN
	BEGIN TRY
	SET NOCOUNT ON
		BEGIN TRAN
			DELETE [dbPetWeb].dbo.[Pet]
			WHERE [PetId] = @PetId
			IF @@ROWCOUNT > 0
			BEGIN
				SET @Success = 1
			END
			ELSE
			BEGIN 
				SET @Success = 0
			END
		COMMIT TRAN
	SET NOCOUNT OFF
	END TRY
	BEGIN CATCH
		SET @Success = 0
		IF(@@TRANCOUNT > 0) ROLLBACK TRAN;
		THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[sp_Pet_Insert]    Script Date: 5/11/2023 12:42:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Pet_Insert] 
@PetId Int Output,
@Name Varchar(50),
@Type Int
AS
BEGIN
	BEGIN TRY
	SET NOCOUNT ON
		BEGIN TRAN
			INSERT INTO [dbPetWeb].dbo.[Pet]([Name],[Type])
			VALUES (@Name, @Type)
			SELECT @PetId = @@IDENTITY
		COMMIT TRAN
	SET NOCOUNT OFF
	END TRY
	BEGIN CATCH
		IF(@@TRANCOUNT > 0) ROLLBACK TRAN;
		THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[sp_Pet_SelectPage]    Script Date: 5/11/2023 12:42:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Pet_SelectPage] 
@PetId Int,
@Name Varchar(50),
@Type Int,
@Offset Int,
@Size Int
AS
BEGIN
	BEGIN TRY
	SET NOCOUNT ON
		BEGIN TRAN
			SELECT 
			[PetId],
			[Name],
			[Type]
			FROM [dbo].[Pet]
			WHERE ([PetId] = @PetId OR @PetId IS NULL) 
			AND ([Name] LIKE '%'+@Name+'%' OR @Name IS NULL) 
			AND ([Type] = @Type OR @Type IS NULL) 
			ORDER BY [PetId]
			OFFSET @Offset ROWS
			FETCH NEXT @Size ROWS ONLY
			OPTION (RECOMPILE)
		COMMIT TRAN
	SET NOCOUNT OFF
	END TRY
	BEGIN CATCH
		IF(@@TRANCOUNT > 0) ROLLBACK TRAN;
		THROW
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[sp_Pet_SelectPageCount]    Script Date: 5/11/2023 12:42:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Pet_SelectPageCount] 
@PetId Int,
@Name Varchar(50),
@Type Int
AS
BEGIN
	SET NOCOUNT ON
	SELECT Count(*)
	FROM [dbo].[Pet]
	WHERE ([PetId] = @PetId OR @PetId IS NULL) 
	AND ([Name] LIKE '%'+@Name+'%' OR @Name IS NULL) 
	AND ([Type] = @Type OR @Type IS NULL) 
	SET NOCOUNT OFF
END

GO
/****** Object:  StoredProcedure [dbo].[sp_Pet_Update]    Script Date: 5/11/2023 12:42:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_Pet_Update] 
@PetId Int,
@Name Varchar(50),
@Type Int
AS
BEGIN
	BEGIN TRY
	IF @Name = '' 
	BEGIN
		SET @Name = NULL
	END
		BEGIN TRAN
			UPDATE [dbPetWeb].dbo.[Pet]
			SET [Name] = ISNULL(@Name,[Name]),
			[Type] = ISNULL(@Type,[Type])
			WHERE [PetId] = @PetId
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		IF(@@TRANCOUNT > 0) ROLLBACK TRAN;
		THROW
	END CATCH
END

GO
