use eKanban;

DROP FUNCTION IF EXISTS NumberOfFogLamp;  
GO
/*
* FUNCTION    : NumberOfFogLamp()
* PARAMETERS   : N/A
* RETURN VALUE : @numOfRow
* DESCRIPTION  : This is a function to get the total number of fogLamp has been produced
*/
create function NumberOfFogLamp ()
returns int
as
begin
declare @numOfRow int 
set @numOfRow = (select Count(*) from FogLamp)

return @numOfRow
end
GO

DROP FUNCTION IF EXISTS dbo.CalculateYieldForEachWorkstation;  
GO  
/*
* PROCEDURE    : CalculateYieldForEachWorkstation()
* PARAMETERS   : @WorkstationID - workstation id
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to release the assigned employee from the workstation
*/
CREATE FUNCTION CalculateYieldForEachWorkstation 
(  
   @WorkstationID Integer
)  
RETURNS int AS
BEGIN
	DECLARE @return_value float(25) = 0.0, 
	@testTrayID int, 
	@testTrayProductNum int, 
	@numOfProducts float = 0.0, 
	@totalNumOfPassed float = 0.0, 
	@numberOfPassedProduct int;

	-- create cursor to iterate through randomly selected Products
		DECLARE custCursor CURSOR FOR
			SELECT ID, ProductNumber, NumberOfPassedFogLamp FROM TestTray WHERE WorkstationID = @WorkstationID
		OPEN custCursor

		DECLARE @temp int

		FETCH NEXT FROM custCursor INTO @testTrayID, @testTrayProductNum, @numberOfPassedProduct
		WHILE(@@FETCH_STATUS=0)
		BEGIN		
			-- Replenish Parts
			IF @testTrayProductNum = 60 BEGIN
				SET @numOfProducts = @numOfProducts +  Cast(60 as float)
				SET @totalNumOfPassed = @totalNumOfPassed + Cast(@numberOfPassedProduct as float)
			END
			FETCH NEXT FROM custCursor INTO @testTrayID, @testTrayProductNum, @numberOfPassedProduct
		END
		CLOSE custCursor
		DEALLOCATE custCursor
	IF @numOfProducts > 0
	BEGIN
		SET @return_value = (@totalNumOfPassed / @numOfProducts) *  Cast(100 as float);
	END

	RETURN @return_value;
END;
Go

DROP FUNCTION IF EXISTS dbo.CalculateYieldForAllRunningWorkstation;  
GO  
/*
* PROCEDURE    : CalculateYieldForAllRunningWorkstation()
* PARAMETERS   : @WorkstationID - workstation id
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to release the assigned employee from the workstation
*/
CREATE FUNCTION CalculateYieldForAllRunningWorkstation()
RETURNS float(25) AS
BEGIN
	DECLARE @return_value float(25) = 0.0, @workstationID int, @eachYield float, @totalYields float = 0.0, @workstationCount int = 0

	DECLARE custCursor CURSOR FOR
		SELECT ID FROM Workstation WHERE IsAvailable = 0
	OPEN custCursor

	FETCH NEXT FROM custCursor INTO @workstationID
	WHILE(@@FETCH_STATUS=0)
	BEGIN	
		EXEC @eachYield = dbo.CalculateYieldForEachWorkstation @workstationID
		SET @totalYields = @totalYields + @eachYield
		SET @workstationCount = @workstationCount + 1
		FETCH NEXT FROM custCursor INTO @workstationID
	END
	CLOSE custCursor
	DEALLOCATE custCursor

	IF @workstationCount > 0
	BEGIN
		SET @totalYields = @totalYields / @workstationCount
	END
	SET @return_value = @totalYields

    RETURN @return_value
END;
GO

DROP FUNCTION IF EXISTS dbo.GetNumberOfProductProducedForWorkstation;
GO
/*
* PROCEDURE    : GetNumberOfProductProducedForWorkstation()
* PARAMETERS   : @WorkstationID - workstation id
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to return number of product produced in that workstation
*/
CREATE FUNCTION GetNumberOfProductProducedForWorkstation 
(
   @WorkstationID Integer
)
RETURNS float AS
BEGIN
    DECLARE @return_value float;

    SET @return_value = (SELECT NumberOfProductsProduced from Workstation WHERE ID = @WorkstationID);
    RETURN @return_value
END;
GO