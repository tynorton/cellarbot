USE [CellarBot]

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID ( 'sp_searchBeers', 'P' ) IS NOT NULL 
    DROP PROCEDURE sp_searchBeers;
GO
CREATE PROCEDURE [dbo].[sp_searchBeers]
    @SearchValue nvarchar(255)
AS 
BEGIN
    SET NOCOUNT ON;
    SELECT beer_id=Beers.id, beer_name=Beers.name, brewery_id=Breweries.id, brewery_name=Breweries.name
    FROM Beers
	INNER JOIN Breweries
	ON Beers.brewery_id=Breweries.id
    WHERE Beers.name LIKE '%' + @SearchValue +'%' OR Breweries.name LIKE '%' + @SearchValue +'%';
END
GO


