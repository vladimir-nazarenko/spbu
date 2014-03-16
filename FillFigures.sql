USE m12nvv
IF OBJECT_ID ('dbo.ImportOfColor' ,'P') IS NOT NULL
	DROP PROCEDURE dbo.ImportOfColor;
GO

DELETE FROM Figures;
GO

CREATE PROCEDURE dbo.ImportOfColor (@color char(1), @initCounter int /*OUTPUT*/)
AS
	Declare @cnt int
	INSERT INTO Figures	VALUES (@initCounter, 'king', @color);
	SET @initCounter = @initCounter + 1;
	INSERT INTO Figures	VALUES (@initCounter, 'queen', @color);
	SET @initCounter = @initCounter + 1;	
	SET @cnt = 1;
	
	WHILE (@cnt <= 2)
	BEGIN
		INSERT INTO Figures	VALUES (@initCounter, 'knight', @color);
		SET @initCounter = @initCounter + 1;
		INSERT INTO Figures	VALUES (@initCounter, 'bishop', @color);
		SET @initCounter = @initCounter + 1;
		INSERT INTO Figures	VALUES (@initCounter, 'rock', @color);
		SET @initCounter = @initCounter + 1;
		SET @cnt = @cnt + 1;
	END
	
	SET @cnt = 1;
	
	WHILE (@cnt < 9)
	BEGIN
		INSERT INTO	Figures	VALUES (@initCounter, 'pawn', @color);
		SET @initCounter = @initCounter + 1;
		SET @cnt = @cnt + 1;
	END
GO

Declare @counter int
SET @counter = 1
EXEC dbo.ImportOfColor 'w', @counter;
/*I'm curious how can I avoid this*/
SET @counter = 17
EXEC dbo.ImportOfColor 'b', @counter;