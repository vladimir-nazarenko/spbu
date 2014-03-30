USE m12nvv;
GO

IF OBJECT_ID('dbo.Eaten', 'FN') IS NOT NULL DROP FUNCTION dbo.Eaten
GO

CREATE FUNCTION Eaten(@color varchar(1))
RETURNS int
AS
BEGIN
	DECLARE @numberOnBoard int
	SET @numberOnBoard = (SELECT COUNT(*) FROM Chessboard JOIN Figures ON Chessboard.cid = Figures.cid)
	RETURN(@numberOnBoard);
END;