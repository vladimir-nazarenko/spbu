USE SocialService;
GO

CREATE PROCEDURE AddTask(@description varchar(200), @roomID int, @workType varchar(20), @deadline date, @empID int OUTPUT)
AS
BEGIN
	DECLARE @buildingID int
	DECLARE @potentialWorkers TABLE (EmpID int, buildID int)
	SET @buildingID = (SELECT Rooms.BuildingID FROM Rooms WHERE Rooms.ID = @roomID)
	INSERT INTO @potentialWorkers 
		SELECT Employees.ID, WorkingBuildingID FROM Employees 
		WHERE Employees.Qualification = @workType;
	SET @empID = 
		(SELECT TOP 1 EmpID 
		FROM @potentialWorkers AS workers 
		LEFT JOIN 
		(SELECT EmployeeID, COUNT(*) AS countOfTasks FROM Tasks GROUP BY EmployeeID) AS leastUsed 
		ON workers.EmpID = leastUsed.EmployeeID ORDER BY countOfTasks, CASE WHEN (buildID = @buildingID) THEN 0 ELSE 1 END)
	IF NOT @empID IS NULL
		INSERT INTO Tasks VALUES (@roomID, @empID, NULL, @deadline, NULL, @workType, @description)
END
GO

CREATE PROCEDURE FireEmployee(@surnameOfFired varchar(20), @surnameOfSubstitute varchar(20), @countOfShifted int OUTPUT)
AS
BEGIN
	IF ((SELECT Employees.Qualification FROM Employees WHERE Surname = @surnameOfFired) <> (SELECT Employees.Qualification FROM Employees WHERE Surname = @surnameOfFired))
		THROW 98000, 'Qualifications of workers are not equal', 1;
	DECLARE @firedID int, @substID int
	SET @firedID = (SELECT TOP 1 ID FROM Employees WHERE Surname = @surnameOfFired)
	SET @substID = (SELECT TOP 1 ID FROM Employees WHERE Surname = @surnameOfSubstitute)
	SET @countOfShifted = (SELECT COUNT(*) FROM Tasks WHERE EmployeeID = @firedID)
	UPDATE Tasks SET Tasks.EmployeeID = @substID
	WHERE Tasks.EmployeeID = @firedID AND Tasks.FinishedDate IS NULL AND DATEDIFF(DAY, GETDATE(), Tasks.DeadlineDate) >= 0 
END
GO

CREATE PROCEDURE Check_swap
AS
BEGIN
	DECLARE @toBeReassigned int
	DECLARE @toChange TABLE(taskID int, oldWorkerID int, newWorkerID int)
	DECLARE @taskPairs TABLE(firstEmpID int, firstTaskID int, secEmpID int, secTaskID int)
	SET @toBeReassigned = 1
	WHILE @toBeReassigned <> 0
	BEGIN
		INSERT INTO @toChange SELECT Tasks.ID, firstEmp.ID, secEmp.ID AS secID FROM Tasks JOIN 
				Employees AS firstEmp ON Tasks.EmployeeID = firstEmp.ID JOIN 
				Rooms ON Rooms.ID = Tasks.RoomID JOIN 
				Buildings ON Buildings.ID = Rooms.BuildingID AND firstEmp.WorkingBuildingID <> Buildings.ID JOIN 
				Employees AS secEmp ON secEmp.WorkingBuildingID = Buildings.ID AND firstEmp.Qualification = secEmp.Qualification 
				WHERE Tasks.FinishedDate IS NULL AND DATEDIFF(DAY, GETDATE(), Tasks.DeadlineDate) >= 0 
		INSERT @taskPairs SELECT A.oldWorkerID, A.taskID, B.oldWorkerID, B.taskID FROM @toChange A JOIN @toChange B ON B.newWorkerID = A.oldWorkerID AND A.oldWorkerID < B.oldWorkerID
		--breakCondition
		SET @toBeReassigned = (SELECT COUNT(*) FROM @taskPairs)
		IF @toBeReassigned <> 0 
		BEGIN
			DECLARE @firstTaskID int, @fstWorkerID int, @secWorkerID int, @secTaskID int
			SELECT TOP 1 @firstTaskID = firstTaskID, @fstWorkerID = firstEmpID, @secTaskID = secTaskID, @secWorkerID = secEmpID FROM @taskPairs
			UPDATE Tasks SET EmployeeID = @secWorkerID WHERE ID = @firstTaskID
			UPDATE Tasks SET EmployeeID = @fstWorkerID WHERE ID = @secTaskID
		END
		DELETE FROM @taskPairs
		DELETE FROM @toChange
	END
END



