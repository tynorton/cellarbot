SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Check to see if the sproc already exist
IF OBJECT_ID ( 'sp_searchBreweries', 'P' ) IS NOT NULL 
    DROP PROCEDURE sp_searchBreweries;
GO
CREATE PROCEDURE sp_searchBreweries
    @SearchValue nvarchar(255)
AS 
BEGIN
    SET NOCOUNT ON;
    SELECT id, name
    FROM Breweries
    WHERE name LIKE '%' + @SearchValue +'%';
END