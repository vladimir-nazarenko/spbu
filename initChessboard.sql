USE m12nvv;
GO

IF OBJECT_ID('dbo.InitChessboard', 'P') IS NOT NULL DROP PROCEDURE dbo.InitChessboard
GO

CREATE PROCEDURE InitChessboard
AS
BEGIN
	DELETE FROM Chessboard;

	INSERT INTO 
		Chessboard 
	VALUES
		(2, 'e', 1),
		(5, 'b', 2),
		(7, 'd', 3),
		(3, 'c', 12),
		(3, 'b', 20),
		(7, 'a', 31),
		(6, 'f', 25),
		(5, 'e', 8),
		(3, 'a', 24),
		(4, 'a', 17),
		(2, 'a', 21)
	;
END