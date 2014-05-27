USE SocialService;
GO

CREATE FUNCTION AddTask(@description varchar(200), @roomID int, @workType varchar(20), @deadline date)
RETURNS int
AS
BEGIN
DECLARE @buildingID int
DECLARE @potentialWorkers TABLE (EmpID int)
SET @buildingID = (SELECT Rooms.BuildingID FROM Rooms WHERE Rooms.ID = @roomID)
INSERT INTO @potentialWorkers 
	SELECT Employees.ID FROM Employees 
	WHERE Employees.Qualification = @workType 
	ORDER BY CASE WHEN (WorkingBuildingID = @buildingID) THEN 0 ELSE 1 END;
RETURN 
	(SELECT TOP 1 EmployeeID, COUNT(*) as countOfTasks FROM @potentialWorkers JOIN Tasks ON EmpID = Tasks.EmployeeID GROUP BY EmployeeID ORDER BY countOfTasks)
END
GO

CREATE PROCEDURE FireEmployee(@surnameOfFired varchar(20), @surnameOfSubstitute varchar(20), @countOfShifted int OUTPUT)
AS
BEGIN
	IF ((SELECT Employees.Qualification FROM Employees WHERE Surname = @surnameOfFired) <> (SELECT Employees.Qualification FROM Employees WHERE Surname = @surnameOfFired))
		THROW 98000, 'Qualifications of workers are not equal', 1;
	DECLARE @firedID int, @substID int
	SET @firedID = (SELECT ID FROM Employees WHERE Surname = @surnameOfFired)
	SET @substID = (SELECT ID FROM Employees WHERE Surname = @surnameOfSubstitute)
	SET @countOfShifted = (SELECT COUNT(*) FROM Tasks WHERE EmployeeID = @firedID)
	UPDATE Tasks SET Tasks.EmployeeID = @substID
	WHERE Tasks.EmployeeID = @firedID AND Tasks.FinishedDate IS NULL AND DATEDIFF(DAY, GETDATE(), Tasks.DeadlineDate) >= 0 
END
GO

CREATE PROCEDURE Check_swap
AS
BEGIN
	DECLARE @toChange TABLE(taskID int, newWorkerID int)
	DECLARE @change TABLE(taskID int, newWorkerID int)
	INSERT INTO @toChange SELECT Tasks.ID, secEmp.ID AS secID FROM Tasks JOIN 
				  Employees AS firstEmp ON Tasks.EmployeeID = firstEmp.ID JOIN 
				  Rooms ON Rooms.ID = Tasks.RoomID JOIN 
				  Buildings ON Buildings.ID = Rooms.BuildingID AND firstEmp.WorkingBuildingID <> Buildings.ID JOIN 
				  Employees AS secEmp ON secEmp.WorkingBuildingID = Buildings.ID AND firstEmp.Qualification = secEmp.Qualification 
				  WHERE Tasks.FinishedDate IS NULL AND DATEDIFF(DAY, GETDATE(), Tasks.DeadlineDate) >= 0 
	DECLARE change_cursor CURSOR
    FOR SELECT * FROM @toChange
	OPEN change_cursor
	WHILE @@FETCH_STATUS = 0
	BEGIN
		FETCH NEXT FROM change_cursor INTO @change;
		UPDATE Tasks SET EmployeeID = (SELECT newWorkerID FROM @change) WHERE ID = (SELECT taskID FROM @change)
	END
	CLOSE change_cursor
END

