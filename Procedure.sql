USE m12nvv;

IF OBJECT_ID('dbo.CheckPosition', 'P') IS NOT NULL DROP PROCEDURE dbo.CheckPosition;
GO

CREATE PROCEDURE dbo.CheckPosition(@x_old int, @y_old varchar(1), @x_new int, @y_new varchar(1), @result int OUTPUT)
AS
BEGIN
	IF (SELECT COUNT(*) FROM Chessboard WHERE Chessboard.x = @x_old AND Chessboard.y = @y_old) <> 1 SET @result = 0
	IF (SELECT COUNT(*) FROM Chessboard WHERE Chessboard.x = @x_new AND Chessboard.y = @y_new) = 0 
	BEGIN
		UPDATE Chessboard
		SET x = @x_new, y = @y_new 
		WHERE x = @x_old AND y = @y_old
	END
	ELSE
	BEGIN
		DECLARE @figureColor varchar(1)
		IF (SELECT Figures.color FROM Chessboard, Figures WHERE Chessboard.cid = Figures.cid AND x = @x_new AND y = @y_new) = 
		(SELECT Figures.color FROM Chessboard, Figures WHERE Chessboard.cid = Figures.cid AND x = @x_old AND y = @y_old)
		BEGIN
			SET @result = 0
		END
		ELSE
		BEGIN
			DELETE FROM Chessboard WHERE x = @x_new AND y = @y_new
			UPDATE Chessboard
			SET x = @x_new, y = @y_new 
			WHERE x = @x_old AND y = @y_old
			SET @result = 2
		END
	END
END

GO

