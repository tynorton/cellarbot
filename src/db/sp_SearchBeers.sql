SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Check to see if the sproc already exist
IF OBJECT_ID ( 'sp_searchBeers', 'P' ) IS NOT NULL 
    DROP PROCEDURE sp_searchBeers;
GO
CREATE PROCEDURE sp_searchBeers
    @SearchValue nvarchar(255)
AS 
BEGIN
    SET NOCOUNT ON;
    SELECT id, name
    FROM Beers
    WHERE name LIKE '%' + @SearchValue +'%';
END