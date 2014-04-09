USE m12nvv;
GO

IF OBJECT_ID('dbo.PrintChessboard', 'P') IS NOT NULL DROP PROCEDURE dbo.PrintChessboard
GO

CREATE PROCEDURE PrintChessboard
AS
BEGIN
	SELECT * FROM Chessboard JOIN Figures on Chessboard.cid = Figures.cid;
END