SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Check to see if the sproc already exist
IF OBJECT_ID ( 'sp_searchStyles', 'P' ) IS NOT NULL 
    DROP PROCEDURE sp_searchStyles;
GO
CREATE PROCEDURE sp_searchStyles 
    @SearchValue nvarchar(255)
AS 
BEGIN
    SET NOCOUNT ON;
    SELECT id, cat_id, style_name
    FROM Styles
    WHERE style_name LIKE @SearchValue;
END