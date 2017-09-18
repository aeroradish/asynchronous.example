IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InformationInsert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InformationInsert] 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InformationInsert] 

	@Description nvarchar(100) = NULL,
	@Col1 nvarchar(25)  = NULL,
	@Col2 nvarchar(25)  = NULL,
	@Col3 nvarchar(25)  = NULL,
	@Col4 nvarchar(25)  = NULL,
	@Col5 nvarchar(25)  = NULL,
	@Col6 nvarchar(25)  = NULL,
	@Col7 nvarchar(25)  = NULL,
	@Col8 nvarchar(25)  = NULL

AS
BEGIN

INSERT INTO [dbo].[Information]
           ([Description]
           ,[Col1]
           ,[Col2]
           ,[Col3]
           ,[Col4]
           ,[Col5]
           ,[Col6]
           ,[Col7]
           ,[Col8])
     VALUES
           (@Description
           ,@Col1
           ,@Col2
           ,@Col3
           ,@Col4
           ,@Col5
           ,@Col6
           ,@Col7
           ,@Col8
         )


RETURN @@ERROR

END
GO
