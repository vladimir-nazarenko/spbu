USE m12nvv;
GO

IF OBJECT_ID('dbo.Eaten', 'FN') IS NOT NULL DROP FUNCTION dbo.Eaten
GO

CREATE FUNCTION Eaten(@color varchar(1), @type varchar(10))
RETURNS int
AS
BEGIN
	DECLARE @numberOnBoard int, @numberStayed int
	SET @numberOnBoard = (SELECT COUNT(*) FROM Chessboard JOIN Figures ON Chessboard.cid = Figures.cid WHERE color = @color AND type = @type)
	SET @numberStayed =
		CASE @type
			WHEN 'rook' THEN 2 - @numberOnBoard
			WHEN 'knight' THEN 2 - @numberOnBoard
			WHEN 'bishop' THEN 2 - @numberOnBoard
			WHEN 'queen' THEN 1 - @numberOnBoard
			WHEN 'king' THEN 1 - @numberOnBoard
			WHEN 'pawn' THEN 8 - @numberOnBoard
		END
	RETURN(@numberStayed);
END;