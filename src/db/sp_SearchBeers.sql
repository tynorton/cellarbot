USE [CellarBot]
GO

/****** Object:  StoredProcedure [dbo].[sp_searchBeers]    Script Date: 10/24/2013 11:26:54 PM ******/
DROP PROCEDURE [dbo].[sp_searchBeers]
GO

/****** Object:  StoredProcedure [dbo].[sp_searchBeers]    Script Date: 10/24/2013 11:26:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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


