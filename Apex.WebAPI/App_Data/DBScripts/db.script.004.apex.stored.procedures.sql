/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 9/18/2016 6:00:45 PM ******/
DROP PROCEDURE [dbo].[GetUser]
GO

/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 9/18/2016 6:00:45 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetUser] 
	@UserId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT	*  
    FROM	users 
    WHERE	userId = @UserId
END

GO

