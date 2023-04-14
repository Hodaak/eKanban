/*
* FILE          : create_stored_procedures.cs
* PROJECT       : SENG3070 - Project Kanban
* PROGRAMMER    : Enes Demirsoz, Jessica Sim, Hoda Akrami
* FIRST VERSION : 2022-11-24
* DESCRIPTION:
*    This file is to create stored procedures that are needed for eKanban project in eKanban database 
*/

USE eKanban
GO

DROP PROCEDURE IF EXISTS InsertInitialDataForConfig;  
GO  
/*
* PROCEDURE    : InsertInitialDataForConfig()
* PARAMETERS   : N/A
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to insert initial data into ConfigurationTable
*/
CREATE PROCEDURE InsertInitialDataForConfig
AS
BEGIN
	BEGIN TRY
	DELETE FROM ConfigurationTable
	INSERT INTO ConfigurationTable(configName, configValue) VALUES
	('Harness','55'), ('Reflector', '35'), ('Housing', '24'), ('Lens', '40'),('Bulb','60'), ('Bezel', '75'), 
	('LeftPartCountToReplenish','5'), ('NumberOfWorkstation', '3'), ('NumberOfWorkers', '3'),
	('TestTrayProductLimit', '60'), ('TimeScale', '2'), ('RunnerTimeMinute', '5'), ('TotalNumberOfOrders', '600'),
	('Base', '60' ), ('BaseDifference', '0.1'), ('NewEmployee_Efficiency', '-50%'), ('VeryExperienced_Efficiency', '+15%'),
	('New/Rookie_Productivity', '0.85%' ), ('Experienced/Normal_Productivity', '0.5%' ), ('VeryExperienced/Super_Productivity', '0.15%' );
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO
-- Insert initial data by executing the stored procedure
EXEC InsertInitialDataForConfig




/*
* PROCEDURE    : GetAvailableEmployee()
* PARAMETERS   : N/A
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to get available employee from Employee table
*/
USE eKanban
GO  

DROP PROCEDURE IF EXISTS GetAvailableEmployee;  
GO  
CREATE PROCEDURE GetAvailableEmployee		--virtual table as return
AS
BEGIN
	BEGIN TRY
		Select ID, FullName, SkillLevel from Employee where IsAvailable = 1
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO


DROP PROCEDURE IF EXISTS GetAvailableWorkstations;  
GO  
/*
* PROCEDURE    : GetAvailableWorkstations()
* PARAMETERS   : N/A
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to get available workstations from Workstation table
*/
CREATE PROCEDURE GetAvailableWorkstations		--virtual table as return
AS
BEGIN
	BEGIN TRY
		Select ID, "Name" from Workstation where IsAvailable = 1
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO

DROP PROCEDURE IF EXISTS GetAvailableWorkstationsForAndon;  
GO  
/*
* PROCEDURE    : GetAvailableWorkstations()
* PARAMETERS   : N/A
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to get available workstations from Workstation table
*/
CREATE PROCEDURE GetAvailableWorkstationsForAndon		--virtual table as return
AS
BEGIN
	BEGIN TRY
		Select ID, "Name" from Workstation where IsAndonRunning = 0;
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO

DROP PROCEDURE IF EXISTS CreateInitialWorkstations;  
GO  
/*
* PROCEDURE    : CreateInitialWorkstations()
* PARAMETERS   : N/A
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to create initial workstations to Workstation table
*/
CREATE PROCEDURE CreateInitialWorkstations		--based on numOfWS in config
AS
BEGIN
	BEGIN TRY
		declare @workstationName nvarchar(50)
		declare @workstationNumber int
		declare @numberOfHarness int
		declare @numberOfReflector int
		declare @numberOfHousing int
		declare @numberOfLens int
		declare @numberOfBulb int
		declare @numberOfBezel int
		declare @counter int 

		set @workstationNumber = (select configValue FROM ConfigurationTable where configName = 'NumberOfWorkers');
		set @numberOfHarness = (select configValue FROM ConfigurationTable where configName = 'Harness');
		set @numberOfReflector = (select configValue FROM ConfigurationTable where configName = 'Reflector');
		set @numberOfHousing = (select configValue FROM ConfigurationTable where configName = 'Housing');
		set @numberOfLens = (select configValue FROM ConfigurationTable where configName = 'Lens');
		set @numberOfBulb = (select configValue FROM ConfigurationTable where configName = 'Bulb');
		set @numberOfBezel = (select configValue FROM ConfigurationTable where configName = 'Bezel');
		set @counter=1
				
		WHILE ( @counter <= @workstationNumber)
		BEGIN
			set @workstationName =  (SELECT CONCAT('Workstation', @counter));
			insert into Workstation values (@workstationName, null, @numberOfHarness, @numberOfReflector, @numberOfHousing, @numberOfLens, @numberOfBulb, @numberOfBezel, 0, 1, 0, 0);
			set @counter = @counter + 1;
		END
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO
DELETE FROM FogLamp
DELETE FROM TestTray
DELETE FROM Workstation
EXEC CreateInitialWorkstations


DROP PROCEDURE IF EXISTS CreateInitialEmployee;  
GO  
/*
* PROCEDURE    : CreateInitialEmployee()
* PARAMETERS   : N/A
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to create initial employee to Employee table
*/
CREATE PROCEDURE CreateInitialEmployee		--based on numOfWS in config
AS
BEGIN
	BEGIN TRY
		declare @employeeName nvarchar(50)
		declare @numberOfEmployee varchar(50)
		declare @employeeLevel int
		declare @employeeLevelString varchar(50)
		declare @counter int 
				
		set @counter=1
		set @numberOfEmployee = (select configValue FROM ConfigurationTable where configName = 'NumberOfWorkers');

		WHILE ( @counter <= @numberOfEmployee)
		BEGIN
			set @employeeLevel = (SELECT FLOOR(RAND()*(2-0+1)+0));
			if @employeeLevel = 0
				set @employeeLevelString = 'New Employee';
			if @employeeLevel = 1
				set @employeeLevelString = 'Experienced';
			if @employeeLevel = 2
				set @employeeLevelString = 'Very Experienced';
			set @employeeName =  (SELECT CONCAT('employee', @counter));
			insert into Employee values (@employeeName, @employeeLevelString, 1);
			set @counter = @counter + 1;
		END
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO
DELETE FROM Employee
EXEC CreateInitialEmployee


DROP PROCEDURE IF EXISTS AssignEmployeeToWorkstation;  
GO  
/*
* PROCEDURE    : AssignEmployeeToWorkstation()
* PARAMETERS   : @WorkstationID - workstation id
*				 @EmployeeID - employee id
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to assign an employee to workstation
*/
CREATE PROCEDURE AssignEmployeeToWorkstation @WorkstationID int, @EmployeeID int		--set Availability
AS
BEGIN
	BEGIN TRY
		update Workstation Set WorkerID = @EmployeeID, IsAvailable = 0 where ID = @WorkstationID;
		update Employee Set IsAvailable = 0 where ID = @EmployeeID;
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO


DROP PROCEDURE IF EXISTS HandleTray;  
GO  
/*
* PROCEDURE    : HandleTray()
* PARAMETERS   : @WorkstationID - workstation id
* RETURN VALUE : it returns test tray ID and product number as OUTPUT 
* DESCRIPTION  : This is a stored procedure to handle Test Tray's product number when the new product is created
*/
CREATE PROCEDURE HandleTray	(@WorkstationID int, @productNum int OUTPUT)
AS
BEGIN
	BEGIN TRY
	    Declare @testTrayId int, @count int
		Set @productNum = 1;
		Select @count = COUNT(ID) from TestTray where WorkstationID = @WorkstationID;
		IF(@count = 0)
		BEGIN
			INSERT INTO TestTray Values (@WorkstationID, 1, 0, 0);
			return @@IDENTITY		--returning TESTTRAYID
		End
		ELSE IF(@count > 0)
		BEGIN
			Select TOP 1 @testTrayId = ID, @productNum = ProductNumber from TestTray where WorkstationID = @WorkstationID Order By ID DESC;
			IF(@productNum < 60)
			BEGIN
				--increment productnum
				Update TestTray SET ProductNumber = @productNum + 1 Where ID = @testTrayId
				SET @productNum = @productNum + 1
				return @testTrayId;
			END
			ELSE IF(@productNum >= 60)
			BEGIN
				--create new tray and calculate number of producst failed and passed in the current tray

				-- get employee id
				DECLARE @workerId int;
				SET @workerId = (SELECT WorkerID FROM Workstation WHERE ID = @WorkstationID);

				-- get employee level
				DECLARE @employeeLevel nvarchar(50);
				SET @employeeLevel = (SELECT SkillLevel FROM Employee WHERE ID = @workerId);

				DECLARE @numOfPassed int;
				DECLARE @numOfFailed int;
				EXEC CalculatePassedAndFailedFogLamps @productNum, @employeeLevel, @NumOfPassed = @numOfPassed OUTPUT, @NumOfFailed = @numOfFailed OUTPUT;
				UPDATE TestTray Set NumberOfFailedFogLamp = @numOfFailed, NumberOfPassedFogLamp = @numOfPassed WHERE ID = @testTrayId
				INSERT INTO TestTray Values (@WorkstationID, 1, 0, 0);		--implement separate function for this creating new tray
				SET @productNum = 1
				return @@IDENTITY;
			END
		END
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO


DROP PROCEDURE IF EXISTS CheckIfAnyBinsAreLessThanProvidedNum;  
GO  
/*
* PROCEDURE    : CheckIfAnyBinsAreLessThanProvidedNum()
* PARAMETERS   : @WorkstationID - workstation id
*				 @NumberOfProductToCompareWith - the number to be compared with each bins
* RETURN VALUE : Return 1 if any of the bins are less than @NumberOfProductToCompareWith, otherwise return 0
* DESCRIPTION  : This is a stored procedure that compares the provided number with all the bins and return 1 if it is, otherwiser return 0
*/
CREATE PROCEDURE CheckIfAnyBinsAreLessThanProvidedNum @WorkstationID int, @NumberOfProductToCompareWith int
AS
BEGIN
	BEGIN TRY
		DECLARE @trayID int, @productNum int, @harness int, @bezel int, @bulb int, @housing int, @lens int, @reflector int, @leftPartCountToReplenish int;

		--get amount of parts left inside bin
		SELECT @harness = NumberOfHarnessLeft,
				@bezel = NumberOfBezelLeft,
				@bulb = NumberOfBulbLeft,
				@housing = NumberOfHousingLeft,
				@lens = NumberOfLensLeft,
				@reflector = NumberOfReflectorLeft
		FROM Workstation 
		WHERE ID = @WorkstationID;

		--if any of the bins have less than @leftPartCountToReplenish, set IsNeedToReplenish to TRUE
		IF @harness <= @NumberOfProductToCompareWith OR  
		@bezel <= @NumberOfProductToCompareWith OR 
		@bulb <= @NumberOfProductToCompareWith OR
		@housing <= @NumberOfProductToCompareWith OR
		@lens <= @NumberOfProductToCompareWith OR
		@reflector <= @NumberOfProductToCompareWith
		BEGIN
			RETURN 1;
		END

		RETURN 0;
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO


DROP PROCEDURE IF EXISTS DecreasePartAmount;
GO  
/*
* PROCEDURE    : DecreasePartAmount()
* PARAMETERS   : @WorkstationID - workstation id
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to decrease the amount of parts by 1 when creating new product
*/
CREATE PROCEDURE DecreasePartAmount @WorkstationID int		-- provide WorkstationID
AS
BEGIN
	BEGIN TRY
		DECLARE @leftPartCountToReplenish int, @checkIfAnyBinsAreEmpty int;

		--decrease amount of parts by 1 and increase NumberOfProductsProduced by 1
		Update Workstation 
		Set 
		NumberOfHarnessLeft = NumberOfHarnessLeft - 1, 
		NumberOfBezelLeft = NumberOfBezelLeft - 1,
		NumberOfBulbLeft = NumberOfBulbLeft - 1,
		NumberOfHousingLeft = NumberOfHousingLeft - 1,
		NumberOfLensLeft = NumberOfLensLeft - 1,
		NumberOfReflectorLeft = NumberOfReflectorLeft -1,
		NumberOfProductsProduced = NumberOfProductsProduced + 1
		WHERE
		ID = @WorkstationID;

		--get leftPartCountToReplenish from ConfigurationTable
		SET @leftPartCountToReplenish = (SELECT configValue FROM ConfigurationTable WHERE configName = 'LeftPartCountToReplenish');
		--check if any of the bins are less than or equal to @leftPartCountToReplenish
		EXEC @checkIfAnyBinsAreEmpty = CheckIfAnyBinsAreLessThanProvidedNum @WorkstationID, @leftPartCountToReplenish

		--if any of the bins are less than or equal to @leftPartCountToReplenish, set IsNeedToReplenish to true
		IF @checkIfAnyBinsAreEmpty = 1
		BEGIN
			Update Workstation Set IsNeedToReplenish = 1 WHERE ID = @WorkstationID;
			RETURN 0;
		END
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO


DROP PROCEDURE IF EXISTS CreateNewProduct;  
GO  
/*
* PROCEDURE    : CreateNewProduct()
* PARAMETERS   : @WorkstationID - workstation id
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to create new product(Fog Lamp). It calls HandleTray and DecreasePartAmount procedures
*				 to handle the amount of parts and tray number. Then it creates new product and adds it to FogLamp table
*/
CREATE PROCEDURE CreateNewProduct @WorkstationID int		-- provide WorkstationID
AS
BEGIN
	BEGIN TRY
		DECLARE @trayID int, @productNum int, @checkIfAnyBinsAreEmpty int;

		--check if any of the bins are less or equal to 0
		EXEC @checkIfAnyBinsAreEmpty = CheckIfAnyBinsAreLessThanProvidedNum @WorkstationID, 0;

		--if any of the bins are not empty, create product
		IF @checkIfAnyBinsAreEmpty = 0
		BEGIN
			EXEC @trayID = HandleTray @WorkstationID, @productNum = @productNum OUTPUT;

			EXEC DecreasePartAmount @WorkstationID

			Declare @unitNumber nvarchar(15);
			Set @unitNumber = CONCAT('FL', REPLICATE('0',6-LEN(@trayID)), @trayID, REPLICATE('0',2-LEN(@productNum)), @productNum)
			INSERT INTO FogLamp Values (@trayID, @unitNumber);
		END
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO

DROP PROCEDURE IF EXISTS GetDefaultTimeEfficiencyForWorkstation;  
GO  
/*
* PROCEDURE    : GetDefaultTimeEfficiencyForWorkstation()
* PARAMETERS   : N/A
* RETURN VALUE : @TimeScale - time scale
*				 @Base - base time
*				 @BaseDifference - the difference in base time
*				 @NewEmployee_Efficiency - new employee's efficiency
*				 @VeryExperienced_Efficiency - very experienced employee's efficiency
*				 @New_Productivity - productivity of new employee
*				 @Experienced_Productivity - productivity of experienced employee
*				 @VeryExperienced_Productivity - productivity of very experienced employee
* DESCRIPTION  : This is a stored procedure to get default time and efficiency for each employee levels for the workstation
*/
CREATE PROCEDURE GetDefaultTimeEfficiencyForWorkstation 
(@TimeScale int OUTPUT,
@Base int OUTPUT, 
@BaseDifference float OUTPUT,
@NewEmployee_Efficiency nvarchar(5) OUTPUT,
@VeryExperienced_Efficiency nvarchar(5) OUTPUT,
@New_Productivity nvarchar(10) OUTPUT,
@Experienced_Productivity nvarchar(10) OUTPUT, 
@VeryExperienced_Productivity nvarchar(10) OUTPUT)	
AS
BEGIN
	BEGIN TRY
		SET @TimeScale = (select configValue FROM ConfigurationTable where configName = 'TimeScale');
		SET @Base = (select configValue FROM ConfigurationTable where configName = 'Base');
		SET @BaseDifference = (select configValue FROM ConfigurationTable where configName = 'BaseDifference');
		SET @NewEmployee_Efficiency = (select configValue FROM ConfigurationTable where configName = 'NewEmployee_Efficiency');
		SET @VeryExperienced_Efficiency = (select configValue FROM ConfigurationTable where configName = 'VeryExperienced_Efficiency');
		SET @New_Productivity = (select configValue FROM ConfigurationTable where configName = 'New/Rookie_Productivity');
		SET @Experienced_Productivity = (select configValue FROM ConfigurationTable where configName = 'Experienced/Normal_Productivity');
		SET @VeryExperienced_Productivity = (select configValue FROM ConfigurationTable where configName = 'VeryExperienced/Super_Productivity');
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO

DROP PROCEDURE IF EXISTS GetConfigInfoForRunner;  
GO  
/*
* PROCEDURE    : GetConfigInfoForRunner()
* PARAMETERS   : N/A
* RETURN VALUE : @TimeScale - time scale
*				 @RunnerTime - runner time
* DESCRIPTION  : This is a stored procedure to get configuration information for the Runner application
*/
CREATE PROCEDURE GetConfigInfoForRunner
(@TimeScale int OUTPUT,
@RunnerTime int OUTPUT)
AS
BEGIN
    BEGIN TRY
        SET @TimeScale = (select configValue FROM ConfigurationTable where configName = 'TimeScale');
        SET @RunnerTime = (select configValue FROM ConfigurationTable where configName = 'RunnerTimeMinute');
    END TRY
    BEGIN CATCH
    SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_STATE() AS ErrorState,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
    RETURN 0;
END
GO


DROP PROCEDURE IF EXISTS ReleaseEmployeeAndWorkstation;  
GO  
/*
* PROCEDURE    : ReleaseEmployeeAndWorkstation()
* PARAMETERS   : @WorkstationID - workstation id
*				 @EmployeeID - employee id
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to release the assigned employee from the workstation
*/
CREATE PROCEDURE ReleaseEmployeeAndWorkstation 
(@WorkstationId int, 
@EmployeeId int)	
AS
BEGIN
	BEGIN TRY
		Update Workstation Set IsAvailable = 1 where ID = @WorkstationId;
		Update Employee Set IsAvailable = 1 where ID = @EmployeeId;
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO

DROP PROCEDURE IF EXISTS ReplenishParts;
GO
/*
* PROCEDURE    : ReplenishParts()
* PARAMETERS   : N/A
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to replenish parts of the workstation that has flag turned on
*/
CREATE PROCEDURE ReplenishParts
AS
BEGIN
		declare @partReplenishLimit int
		declare @workstationId int

		declare @numberOfHarness int
		declare @numberOfReflector int
		declare @numberOfHousing int
		declare @numberOfLens int
		declare @numberOfBulb int
		declare @numberOfBezel int

		declare @initialNumberOfHarness int
		declare @initialNumberOfReflector int
		declare @initialNumberOfHousing int
		declare @initialNumberOfLens int
		declare @initialNumberOfBulb int
		declare @initialNumberOfBezel int

		set @partReplenishLimit = (select configValue FROM ConfigurationTable where configName = 'LeftPartCountToReplenish');
		set @initialNumberOfHarness = (select configValue FROM ConfigurationTable where configName = 'Harness');
		set @initialNumberOfReflector = (select configValue FROM ConfigurationTable where configName = 'Reflector');
		set @initialNumberOfHousing = (select configValue FROM ConfigurationTable where configName = 'Housing');
		set @initialNumberOfLens = (select configValue FROM ConfigurationTable where configName = 'Lens');
		set @initialNumberOfBulb = (select configValue FROM ConfigurationTable where configName = 'Bulb');
		set @initialNumberOfBezel = (select configValue FROM ConfigurationTable where configName = 'Bezel');
	
		-- create cursor to iterate through randomly selected Products
		DECLARE custCursor CURSOR FOR
			SELECT ID, NumberOfBezelLeft, NumberOfBulbLeft, NumberOfHarnessLeft, NumberOfHousingLeft,
			NumberOfLensLeft, NumberOfReflectorLeft FROM Workstation WHERE IsNeedToReplenish = 1
		OPEN custCursor
		FETCH NEXT FROM custCursor INTO @workstationId, @numberOfBezel, @numberOfBulb, @numberOfHarness, @numberOfHousing, 
		@numberOfLens, @numberOfReflector

		WHILE(@@FETCH_STATUS=0)
		BEGIN			

			BEGIN TRY
			-- Replenish Parts
			IF @numberOfBezel <= @partReplenishLimit BEGIN
				SET @numberOfBezel = @numberOfBezel + @initialNumberOfBezel
				UPDATE Workstation SET NumberOfBezelLeft = @numberOfBezel, IsNeedToReplenish = 0 WHERE ID = @workstationId
			END
			IF @numberOfBulb <= @partReplenishLimit BEGIN
				SET @numberOfBulb = @numberOfBulb + @initialNumberOfBulb
				UPDATE Workstation SET NumberOfBulbLeft = @numberOfBulb, IsNeedToReplenish = 0 WHERE ID = @workstationId
			END
			IF @numberOfHarness <= @partReplenishLimit BEGIN
				SET @numberOfHarness = @numberOfHarness + @initialNumberOfHarness
				UPDATE Workstation SET NumberOfHarnessLeft = @numberOfHarness, IsNeedToReplenish = 0 WHERE ID = @workstationId
			END
			IF @numberOfHousing <= @partReplenishLimit BEGIN
				SET @numberOfHousing = @numberOfHousing + @initialNumberOfHousing
				UPDATE Workstation SET NumberOfHousingLeft = @numberOfHousing, IsNeedToReplenish = 0 WHERE ID = @workstationId
			END
			IF @numberOfLens <= @partReplenishLimit BEGIN
				SET @numberOfLens = @numberOfLens + @initialNumberOfLens
				UPDATE Workstation SET NumberOfLensLeft = @numberOfLens, IsNeedToReplenish = 0 WHERE ID = @workstationId
			END
			IF @numberOfReflector <= @partReplenishLimit BEGIN
				SET @numberOfReflector = @numberOfReflector + @initialNumberOfReflector
				UPDATE Workstation SET NumberOfReflectorLeft = @numberOfReflector, IsNeedToReplenish = 0 WHERE ID = @workstationId
			END

			END TRY
			BEGIN CATCH
			SELECT
				ERROR_NUMBER() AS ErrorNumber,
				ERROR_STATE() AS ErrorState,
				ERROR_SEVERITY() AS ErrorSeverity,
				ERROR_PROCEDURE() AS ErrorProcedure,
				ERROR_LINE() AS ErrorLine,
				ERROR_MESSAGE() AS ErrorMessage;
			END CATCH
			FETCH NEXT FROM custCursor INTO @workstationId, @numberOfBezel, @numberOfBulb, @numberOfHarness, @numberOfHousing, 
			@numberOfLens, @numberOfReflector
		END
		CLOSE custCursor
		DEALLOCATE custCursor
	RETURN 0;
END
GO

/*
* PROCEDURE    : CalculatePassedAndFailedFogLamps()
* PARAMETERS   : N/A
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to calculate the number of passed and failed foglamp per tray
*/
DROP PROCEDURE IF EXISTS CalculatePassedAndFailedFogLamps;  
GO  
CREATE PROCEDURE CalculatePassedAndFailedFogLamps        --virtual table as return
(@NumberOfProduct int,
@ExperienceLevel  nvarchar(50),
@NumOfPassed int OUTPUT,
@NumOfFailed int OUTPUT)
AS
BEGIN
    DECLARE @DefectRateInt int;
    DECLARE @DefectRateFloat float;
    DECLARE @DefectRateString nvarchar (50)



   BEGIN TRY
    if @ExperienceLevel = 'New Employee'
        set @DefectRateString = (select configValue FROM ConfigurationTable where configName = 'New/Rookie_Productivity');
    if @ExperienceLevel = 'Experienced'
        set @DefectRateString = (select configValue FROM ConfigurationTable where configName = 'Experienced/Normal_Productivity');
    if @ExperienceLevel = 'Very Experienced'
        set @DefectRateString = (select configValue FROM ConfigurationTable where configName = 'VeryExperienced/Super_Productivity');
       
    SET @DefectRateFloat =  (select convert(float, (select SUBSTRING(@DefectRateString, 1, LEN(@DefectRateString)-1))));
    SET @DefectRateInt = (select convert(int, @DefectRateFloat * 100));

   DECLARE @cnt INT = 0;
    DECLARE @random INT = 0;
    SET @NumOfFailed = 0;
    SET @NumOfPassed = 0;

   WHILE @cnt < @NumberOfProduct
    BEGIN
		set @random = (SELECT FLOOR(RAND()*(10000-1+1))+1);

		if @random <= @DefectRateInt
		begin
			SET @NumOfFailed = @NumOfFailed + 1;
		end
		else
		begin
			SET @NumOfPassed = @NumOfPassed + 1;
		end
		SET @cnt = @cnt + 1;
    END;
   END TRY
    BEGIN CATCH
    SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_STATE() AS ErrorState,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
    RETURN 0;
END
GO

/*
* PROCEDURE    : GetPartsCount()
* PARAMETERS   : N/A
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to get number of each parts thathas left
*/
DROP PROCEDURE IF EXISTS GetPartsCount;
GO
CREATE PROCEDURE GetPartsCount        --virtual table as return
(@WorkstationId int,
@NumberOfHarnessLeft int OUTPUT,
@NumberOfReflectorLeft int OUTPUT, 
@NumberOfHousingLeft int OUTPUT,
@NumberOfLensLeft int OUTPUT,
@NumberOfBulbLeft int OUTPUT,
@NumberOfBezelLeft int OUTPUT,
@IsNeedToReplenish int OUTPUT
)
AS
BEGIN
    BEGIN TRY
        Select @NumberOfHarnessLeft = NumberOfHarnessLeft, @NumberOfReflectorLeft = NumberOfReflectorLeft, 
				@NumberOfHousingLeft = NumberOfHousingLeft, @NumberOfLensLeft = NumberOfLensLeft, 
				@NumberOfBulbLeft = NumberOfBulbLeft, @NumberOfBezelLeft = NumberOfBezelLeft, 
				@IsNeedToReplenish = IsNeedToReplenish from Workstation where ID = @WorkstationId
    END TRY
    BEGIN CATCH
    SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_STATE() AS ErrorState,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
    RETURN 0;
END
GO

DROP PROCEDURE IF EXISTS AssignAndonToWorkstation;
GO  
/*
* PROCEDURE    : AssignEmployeeToWorkstation()
* PARAMETERS   : @WorkstationID - workstation id
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to assign an andon simulation to workstation to indicate
*				 this workstation has already running Andon application assigned to it
*/
CREATE PROCEDURE AssignAndonToWorkstation @WorkstationID int		--set Availability
AS
BEGIN
	BEGIN TRY
		update Workstation Set IsAndonRunning = 1 where ID = @WorkstationID;
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO

DROP PROCEDURE IF EXISTS ReleaseWorkstationForAndon;  
GO  
/*
* PROCEDURE    : ReleaseWorkstationForAndon()
* PARAMETERS   : @WorkstationID - workstation id
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to release the assigned andon application from the workstation
*/
CREATE PROCEDURE ReleaseWorkstationForAndon @WorkstationId int
AS
BEGIN
	BEGIN TRY
		Update Workstation Set IsAndonRunning = 0 where ID = @WorkstationId;
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO

DROP PROCEDURE IF EXISTS RetrieveDataForAssemblyKanban;  
GO  
/*
* PROCEDURE    : RetrieveDataForAssemblyKanban()
* PARAMETERS   : @WorkstationID - workstation id
*				 @EmployeeID - employee id
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to release the assigned employee from the workstation
*/
CREATE PROCEDURE RetrieveDataForAssemblyKanban
(@OrderAmount float OUTPUT, 
@TotalNumberOfProductsProduced float OUTPUT,
@NumberOfWorkstationsRunning int OUTPUT,
@ProcessAmount float OUTPUT)	
AS
BEGIN
	BEGIN TRY
		SET @OrderAmount = (SELECT configValue FROM ConfigurationTable WHERE configName = 'TotalNumberOfOrders');
		SET @TotalNumberOfProductsProduced = (SELECT Sum(NumberOfProductsProduced) FROM Workstation WHERE IsAvailable = 0);
		SET @NumberOfWorkstationsRunning = (SELECT COUNT(ID) from Workstation WHERE IsAvailable = 0);
		SET @ProcessAmount = @TotalNumberOfProductsProduced / @OrderAmount * 100
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO

DROP PROCEDURE IF EXISTS GetRunningWorkstations;  
GO  
/*
* PROCEDURE    : GetRunningWorkstations()
* PARAMETERS   : N/A
* RETURN VALUE : N/A
* DESCRIPTION  : This is a stored procedure to get available workstations from Workstation table
*/
CREATE PROCEDURE GetRunningWorkstations		--virtual table as return
AS
BEGIN
	BEGIN TRY
		Select ID, "Name" from Workstation where IsAvailable = 0
	END TRY
	BEGIN CATCH
	SELECT
		ERROR_NUMBER() AS ErrorNumber,
		ERROR_STATE() AS ErrorState,
		ERROR_SEVERITY() AS ErrorSeverity,
		ERROR_PROCEDURE() AS ErrorProcedure,
		ERROR_LINE() AS ErrorLine,
		ERROR_MESSAGE() AS ErrorMessage;
	END CATCH
	RETURN 0;
END
GO

DROP PROCEDURE IF EXISTS GetDefaultBinCapacities;
GO
/*
* PROCEDURE    : GetDefaultBinCapacities()
* PARAMETERS   : N/A
* RETURN VALUE : @HarnessCapacity
                 @ReflectorCapacity
                 @HousingCapacity
                 @LensCapacity
                 @BulbCapacity
                 @BezelCapacity
* DESCRIPTION  : This is a stored procedure to get default capacities for each bin
*/
CREATE PROCEDURE GetDefaultBinCapacities 
(@HarnessCapacity int OUTPUT,
@ReflectorCapacity int OUTPUT,
@HousingCapacity int OUTPUT,
@LensCapacity int OUTPUT,
@BulbCapacity int OUTPUT,
@BezelCapacity int OUTPUT)
AS
BEGIN
    BEGIN TRY
        SET @HarnessCapacity = (select configValue FROM ConfigurationTable where configName = 'Harness');
        SET @ReflectorCapacity = (select configValue FROM ConfigurationTable where configName = 'Reflector');
        SET @HousingCapacity = (select configValue FROM ConfigurationTable where configName = 'Housing');
        SET @LensCapacity = (select configValue FROM ConfigurationTable where configName = 'Lens');
        SET @BulbCapacity = (select configValue FROM ConfigurationTable where configName = 'Bulb');
        SET @BezelCapacity = (select configValue FROM ConfigurationTable where configName = 'Bezel');
    END TRY
    BEGIN CATCH
    SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_STATE() AS ErrorState,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
    RETURN 0;
END
GO