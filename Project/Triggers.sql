USE SocialService;
GO

CREATE TRIGGER smartDelete ON Rooms AFTER DELETE AS
BEGIN
	IF (SELECT COUNT(*) FROM Rooms WHERE BuildingID = deleted.BuildingID) = 0
	BEGIN
		UPDATE Employees SET WorkingBuildingID = NULL WHERE WorkingBuildingID = deleted.BuildingID
		DELETE FROM Buildings WHERE ID = deleted.BuildingID
	END
END
GO

CREATE TRIGGER TaskAddition ON Tasks INSTEAD OF INSERT AS
BEGIN
	UPDATE inserted SET DeadlineDate = DATEADD(DAY, 2, GETDATE()) WHERE DeadlineDate IS NULL
	INSERT INTO Tasks VALUES (inserted)
END
	
