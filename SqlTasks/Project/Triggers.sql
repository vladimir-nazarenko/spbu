USE SocialService;
GO

CREATE TRIGGER SmartDelete ON Rooms AFTER DELETE AS
BEGIN
	DECLARE @roomCount TABLE(buildingID int, cnt int)
	INSERT @roomCount SELECT Buildings.ID, SUM(CASE WHEN Rooms.ID IS NULL THEN 0 ELSE 1 END) FROM Buildings LEFT JOIN Rooms ON BuildingID = Buildings.ID GROUP BY Buildings.ID
	DECLARE @zeroIds TABLE(id int) 
	DECLARE @modified TABLE(id int)
	INSERT @modified SELECT Buildings.ID FROM Buildings WHERE Buildings.ID IN (SELECT BuildingID FROM deleted)
	INSERT @zeroIds SELECT buildingID FROM @roomCount rc WHERE cnt = 0
	UPDATE Employees SET WorkingBuildingID = NULL WHERE WorkingBuildingID IN (SELECT * FROM @zeroIds) AND WorkingBuildingID IN (SELECT * FROM @modified)
	DELETE FROM Buildings WHERE ID IN (SELECT * FROM @zeroIds) AND ID IN (SELECT * FROM @modified)
END
GO

CREATE TRIGGER TaskAddition ON Tasks AFTER INSERT AS
BEGIN
	UPDATE Tasks SET AssignDate = GETDATE() WHERE ID IN (SELECT ID FROM inserted)
	UPDATE Tasks SET DeadlineDate = DATEADD(DAY, 2, GETDATE()) WHERE ID IN (SELECT ID FROM inserted) AND DeadlineDate IS NULL
END

	
